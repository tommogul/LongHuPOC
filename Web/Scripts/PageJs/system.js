//=============================================================================
// 文件名:		system.js
// 版权:		Copyright (C) 2010 Pfizer
// 创建人:		min.jiang
// 创建日期:	2011-2-16
// 描述:		此文件修改请通知作者
// 备注:       String, Object，Event, Globals, StringBuilder, Template类扩展和实现 有待完善
var Class = {
    create: function () {
        return function () {
            this.init.apply(this, arguments);
        };
    }
};
//提供一种通过拷贝所有源以象属性和函数到目标函数实现继承的方法
Object.extend = function (destination, source) {
    for (var property in source) {
        destination[property] = source[property];
    }
    return destination;
};
//返回function的实例，这个实例和源function的结构一样，但是它已被绑定给了参数中提供的object，就是说，function中的this指针指向参数object。
Function.prototype.bind = function () {
    var __method = this, args = Array.from(arguments), object = args.shift();
    return function () {
        if (typeof Array.from == 'function') {
            return __method.apply(object, args.concat(Array.from(arguments)));
        }
    }
};
//用法和上面的bind一样，区别在于用来绑定事件。
Function.prototype.bindAsEventListener = function (object) {
    var __method = this, args = Array.from(arguments), object = args.shift();
    return function (event) {
        if (typeof Array.from == 'function') {
            return __method.apply(object, [event || window.event].concat(args));
        }
    }
};
////////////////////////////////////////// Object 扩展 start/////////////////////////////////
Object.extend(String.prototype, {
    trim: function () {
        return this.replace(/(^[\s　]*)|([\s　]*$)/g, "");
    },
    toArray: function () {
        return this.split('');
    },
    startsWith: function (pattern) {
        return this.indexOf(pattern) === 0;
    },

    endsWith: function (pattern) {
        var d = this.length - pattern.length;
        return d >= 0 && this.lastIndexOf(pattern) === d;
    },

    empty: function () {
        return this.trim() == '';
    },
    toArray: function () {
        return this.split('');
    }
});
Object.isNull = function (obj) {
    return typeof (obj) == "undefined" || obj == null;
};
////////////////////////////////////////// Object 扩展 end/////////////////////////////////

////////////////////////////////////////// String 扩展 start/////////////////////////////////
////////////////////////////////////////// string ext/////////////////////////////////
Object.extend(String, {
    interpret: function (value) {
        return value == null ? '' : String(value);
    },
    specialChar: {
        '\b': '\\b',
        '\t': '\\t',
        '\n': '\\n',
        '\f': '\\f',
        '\r': '\\r',
        '\\': '\\\\'
    }
});

Object.extend(String.prototype, {
    trim: function () {
        return this.replace(/(^[\s　]*)|([\s　]*$)/g, "");
    },

    lTrim: function () {
        return this.replace(/(^[\s　]*)/g, "");
    },

    rTrim: function () {
        return this.replace(/([\s　]*$)/g, "");
    },

    bytelength: function () {
        var doubleByteChars = this.match(/[^\x00-\xff]/ig);
        return this.length + (doubleByteChars == null ? 0 : doubleByteChars.length);
    },

    cut: function (n) {
        if (n > this.length) {
            return this;
        }
        return this.substring(0, n);
    },

    formatWithWBR: function () {
        var args = arguments, size = 10;
        if (args.length > 0) {
            var argument = parseInt(args[0]);
            if (!isNaN(argument) && argument > 0) {
                size = argument;
            }
        }
        var text = this, output = [], start = 0, rowStart = 0, nextChar;
        for (var i = 1, count = text.length; i < count; i++) {
            nextChar = text.charAt(i);
            if (/\s/.test(nextChar)) {
                rowStart = i;
            } else {
                if ((i - rowStart) == size) {
                    output.push(text.substring(start, i));
                    output.push("<wbr>");
                    start = rowStart = i;
                }
            }
        }
        output.push(text.substr(start));
        return output.join("");
    },

    gsub: function (pattern, replacement) {
        var result = '', source = this, match;
        replacement = arguments.callee.prepareReplacement(replacement);

        while (source.length > 0) {
            if (match = source.match(pattern)) {
                result += source.slice(0, match.index);
                result += String.interpret(replacement(match));
                source = source.slice(match.index + match[0].length);
            } else {
                result += source, source = '';
            }
        }
        return result;
    },

    sub: function (pattern, replacement, count) {
        replacement = this.gsub.prepareReplacement(replacement);
        count = count === undefined ? 1 : count;

        return this.gsub(pattern, function (match) {
            if (--count < 0) return match[0];
            return replacement(match);
        });
    },

    scan: function (pattern, iterator) {
        this.gsub(pattern, iterator);
        return this;
    },

    truncate: function (length, truncation) {
        length = length || 30;
        truncation = truncation === undefined ? '...' : truncation;
        return this.length > length ?
      this.slice(0, length - truncation.length) + truncation : this;
    },

    strip: function () {
        return this.replace(/^\s+/, '').replace(/\s+$/, '');
    },

    stripTags: function () {
        return this.replace(/<\/?[^>]+>/gi, '');
    },

    stripScripts: function () {
        return this.replace(new RegExp(Prototype.ScriptFragment, 'img'), '');
    },

    extractScripts: function () {
        var matchAll = new RegExp(Prototype.ScriptFragment, 'img');
        var matchOne = new RegExp(Prototype.ScriptFragment, 'im');
        return (this.match(matchAll) || []).map(function (scriptTag) {
            return (scriptTag.match(matchOne) || ['', ''])[1];
        });
    },

    evalScripts: function () {
        return this.extractScripts().map(function (script) { return eval(script) });
    },

    escapeHTML: function () {
        var self = arguments.callee;
        self.text.data = this;
        return self.div.innerHTML;
    },

    unescapeHTML: function () {
        var div = document.createElement('div');
        div.innerHTML = this.stripTags();
        return div.childNodes[0] ? (div.childNodes.length > 1 ?
      $A(div.childNodes).inject('', function (memo, node) { return memo + node.nodeValue }) :
      div.childNodes[0].nodeValue) : '';
    },

    toQueryParams: function (separator) {
        var match = this.strip().match(/([^?#]*)(#.*)?$/);
        if (!match) return {};

        return match[1].split(separator || '&').inject({}, function (hash, pair) {
            if ((pair = pair.split('='))[0]) {
                var key = decodeURIComponent(pair.shift());
                var value = pair.length > 1 ? pair.join('=') : pair[0];
                if (value != undefined) value = decodeURIComponent(value);

                if (key in hash) {
                    if (hash[key].constructor != Array) hash[key] = [hash[key]];
                    hash[key].push(value);
                }
                else hash[key] = value;
            }
            return hash;
        });
    },

    toArray: function () {
        return this.split('');
    },

    succ: function () {
        return this.slice(0, this.length - 1) +
      String.fromCharCode(this.charCodeAt(this.length - 1) + 1);
    },

    times: function (count) {
        var result = '';
        for (var i = 0; i < count; i++) result += this;
        return result;
    },

    camelize: function () {
        var parts = this.split('-'), len = parts.length;
        if (len == 1) return parts[0];

        var camelized = this.charAt(0) == '-'
      ? parts[0].charAt(0).toUpperCase() + parts[0].substring(1)
      : parts[0];

        for (var i = 1; i < len; i++)
            camelized += parts[i].charAt(0).toUpperCase() + parts[i].substring(1);

        return camelized;
    },

    capitalize: function () {
        return this.charAt(0).toUpperCase() + this.substring(1).toLowerCase();
    },

    underscore: function () {
        return this.gsub(/::/, '/').gsub(/([A-Z]+)([A-Z][a-z])/, '#{1}_#{2}').gsub(/([a-z\d])([A-Z])/, '#{1}_#{2}').gsub(/-/, '_').toLowerCase();
    },

    dasherize: function () {
        return this.gsub(/_/, '-');
    },

    inspect: function (useDoubleQuotes) {
        var escapedString = this.gsub(/[\x00-\x1f\\]/, function (match) {
            var character = String.specialChar[match[0]];
            return character ? character : '\\u00' + match[0].charCodeAt().toPaddedString(2, 16);
        });
        if (useDoubleQuotes) return '"' + escapedString.replace(/"/g, '\\"') + '"';
        return "'" + escapedString.replace(/'/g, '\\\'') + "'";
    },

    toJSON: function () {
        return this.inspect(true);
    },

    unfilterJSON: function (filter) {
        return this.sub(filter || Prototype.JSONFilter, '#{1}');
    },

    isJSON: function () {
        var str = this.replace(/\\./g, '@').replace(/"[^"\\\n\r]*"/g, '');
        return (/^[,:{}\[\]0-9.\-+Eaeflnr-u \n\r\t]*$/).test(str);
    },

    evalJSON: function (sanitize) {
        var json = this.unfilterJSON();
        try {
            if (!sanitize || json.isJSON()) return eval('(' + json + ')');
        } catch (e) { }
        throw new SyntaxError('Badly formed JSON string: ' + this.inspect());
    },

    include: function (pattern) {
        return this.indexOf(pattern) > -1;
    },

    startsWith: function (pattern) {
        return this.indexOf(pattern) === 0;
    },

    endsWith: function (pattern) {
        var d = this.length - pattern.length;
        return d >= 0 && this.lastIndexOf(pattern) === d;
    },

    empty: function () {
        return this.trim() == '';
    },

    blank: function () {
        return /^\s*$/.test(this);
    }
});
String.isNullOrEmpty = function (str) {
    return str == undefined || str == null || str.empty();
};
String.format = function () {
    var args = arguments, argsCount = args.length;
    if (argsCount == 0) {
        return "";
    }
    if (argsCount == 1) {
        return args[0];
    }
    var reg = /{(\d+)?}/g, arg, result;
    if (args[1] instanceof Array) {
        arg = args[1];
        result = args[0].replace(reg, function ($0, $1) {
            return arg[parseInt($1)];
        });
    } else {
        arg = args;
        result = args[0].replace(reg, function ($0, $1) {
            return arg[parseInt($1) + 1];
        });
    }
    return result;
};

String.prototype.gsub.prepareReplacement = function (replacement) {
    if (typeof replacement == 'function') return replacement;
    var template = new Template(replacement);
    return function (match) { return template.evaluate(match) };
};
////////////////////////////////////////// String 扩展 end/////////////////////////////////

////////////////////////////////////////// FunctionExt 扩展 start/////////////////////////////////
FunctionExt = {
    createCallback: function (functionObj/*args...*/) {
        // make args available, in function below
        var args = arguments;
        var method = functionObj;
        return function () {
            return method.apply(window, args);
        };
    },

    createDelegate: function (functionObj, obj, args, appendArgs) {
        var method = functionObj;
        return function () {
            var callArgs = args || arguments;
            if (appendArgs === true) {
                callArgs = Array.prototype.slice.call(arguments, 0);
                callArgs = callArgs.concat(args);
            } else if (typeof appendArgs == "number") {
                callArgs = Array.prototype.slice.call(arguments, 0); // copy arguments first
                var applyArgs = [appendArgs, 0].concat(args); // create method call params
                Array.prototype.splice.apply(callArgs, applyArgs); // splice them in
            }
            return method.apply(obj || window, callArgs);
        };
    },

    defer: function (functionObj, millis, obj, args, appendArgs) {
        var fn = this.createDelegate(functionObj, obj, args, appendArgs);
        if (millis) {
            return setTimeout(fn, millis);
        }
        fn();
        return 0;
    }
};
////////////////////////////////////////// FunctionExt 扩展 end/////////////////////////////////



////////////////////////////////////////// Globals function//////////////////////////
var Globals = {
    //Add By Andy
    //For ManageHeader
    ScriptContentFragment: '<script.*?>((\n|\r|.)*?)<\/script>',
    //<script.*?src=\"((\n|\r|.)*?)\"><\/script>
    ScriptSrcFragment: '<script.*?src=\"([\?=\/\.A-Za-z0-9_]*)\".*?><\/script>',

    //取得样式内容的正则表达式
    StyleContentFragment: '<style.*?>((\n|\r|.)*?)<\/style>',
    StyleHrefFragment: '<link.*?href=\"([\/\.A-Za-z0-9_]*)\".*?><\/link>',

    // 用于遮蔽IE6下弹出层下出现下拉框的情况, 返回添加的iframe元素；以便调用closeIE6Fliter
    addIE6Filter: function (width, height, left, top, zindex) {
        if ($.browser.msie && $.browser.version < 7) {
            var filterTemplate = new Template("<iframe style=\"position: absolute; z-index: #{zindex}; width:#{width}px;height:#{height}px; top: #{top}px; left: #{left}px;border:0px solid #000;filter:alpha(opacity=0);-moz-opacity:0\"></iframe>");
            return $(filterTemplate.eval({
                width: width + 2,
                height: height + 2,
                left: left,
                top: top,
                zindex: Object.isNull(zindex) ? 1 : zindex
            })).appendTo($("#m_contentend"));
        }
    },
    // 移除IE6的层遮蔽
    closeIE6Fliter: function (iframe) {
        if (!Object.isNull(iframe)) {
            iframe.remove();
            iframe = null;
        }
    },

    // 获取浏览器滚动条坐标
    getScrollPosition: function () {
        var pagePosition = { x: 0, y: 0 };
        if (typeof (window.pageYOffset) == 'number') {
            pagePosition.x = window.pageXOffset;
            pagePosition.y = window.pageYOffset;
        } else if (document.body && (document.body.scrollLeft || document.body.scrollTop)) {
            pagePosition.x = document.body.scrollLeft;
            pagePosition.y = document.body.scrollTop;
        } else if (document.documentElement) {
            pagePosition.x = document.documentElement.scrollLeft;
            pagePosition.y = document.documentElement.scrollTop;
        }
        return pagePosition;
    },
    // 获取浏览器窗口大小
    browserDimensions: function () {
        var dimensions = { width: 0, height: 0 };
        if ($.browser.msie) {
            dimensions.height = document.documentElement.clientHeight;
            dimensions.width = document.documentElement.clientWidth;
        } else {
            dimensions.height = window.innerHeight;
            dimensions.width = document.width || document.body.offsetWidth;
        }
        return dimensions;
    },
    //获取请求参数
    getParam: function (sArgName) {
        var sHref = document.location.href;
        var args = sHref.split("?");
        var retval = "";
        if (args[0] == sHref) /*参数为空*/
        {
            return retval; /*无需做任何处理*/
        }
        var str = args[1];
        args = str.split("&");
        for (var i = 0; i < args.length; i++) {
            str = args[i];
            var arg = str.split("=");
            if (arg.length <= 1) continue;
            if (arg[0] == sArgName) retval = arg[1];
        }
        return retval;

    },
    cookie: function (name, subName, value, options) {// Globals.cookie start
        /**
        * Cookie plugin
        *
        * Copyright (c) 2011 min.jiang
        * Dual licensed under the MIT and GPL licenses:
        * http://www.opensource.org/licenses/mit-license.php
        * http://www.gnu.org/licenses/gpl.html
        * 使用举例：
        //注： 写入时，subName参数请传递空值或null
        //写入Cookies-值为字符串，即不包含子键
        Globals.cookie("singleKey", "", "singleKey-value", { expires: 1, path: "/", secure: false })
        //读取Cookies-根据主键
        alert("singleKey:" + Globals.cookie("singleKey"));

        //写入Cookies-值为对象，则每个属性名为子键的名称，属性值为子键值
        var subNameObj = { subName1: "aaa", subName2: "bbb", subName3: "ccc" };
        Globals.cookie("multiKey", "", subNameObj, { expires: 1, path: "/", secure: false });
        //读取Cookies-根据主键
        alert("multiKey:" + Globals.cookie("multiKey"));
        //读取Cookies-根据主键和子键
        alert("multiKey,subName1:" + Globals.cookie("multiKey", "subName1"));
        *
        */
        if (typeof value != 'undefined') { // name and value given, set cookie
            options = options || {};
            if (value === null) {
                value = '';
                options.expires = -1;
            }
            var expires = '';
            if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
                var date;
                if (typeof options.expires == 'number') {
                    date = new Date();
                    date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
                } else {
                    date = options.expires;
                }
                expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE
            }
            // CAUTION: Needed to parenthesize options.path and options.domain
            // in the following expressions, otherwise they evaluate to undefined
            // in the packed version for some reason...
            var hostStr = window.location.host.split(".");
            var hostName = hostStr.length >= 2 ? String.format(";domain={0}.{1}", hostStr[hostStr.length - 2], hostStr[hostStr.length - 1]) : "";

            var path = options.path ? '; path=' + (options.path) : ';path=/';
            var domain = options.domain ? '; domain=' + (options.domain) : hostName;
            var secure = options.secure ? '; secure' : '';

            //If value is an object, each property will be a sub key;
            if (typeof value == "object") {
                var k = 0;
                var tempResult = "";
                for (var tempValue in value) {
                    if (k > 0) {
                        tempResult += "&";
                    }
                    tempResult += tempValue + "=" + encodeURIComponent(value[tempValue]);
                    k++;
                }
                value = tempResult;
            }
            else {
                value = encodeURIComponent(value);
            }

            document.cookie = [name, '=', value, expires, path, domain, secure].join('');
        } else { // only name given, get cookie
            var cookieValue = null;
            if (document.cookie && document.cookie != '') {
                var cookies = document.cookie.split(';');
                for (var i = 0; i < cookies.length; i++) {
                    var cookie = jQuery.trim(cookies[i]);
                    // Does this cookie string begin with the name we want?
                    if (cookie.substring(0, name.length + 1) == (name + '=')) {
                        cookieValue = decodeURIComponent(cookie.substring(name.length + 1));

                        //Search sub key
                        if (typeof subName != 'undefined' && subName != null && subName != "") {
                            var subCookies = cookieValue.toString().split('&');
                            for (var j = 0; j < subCookies.length; j++) {
                                var subCookie = jQuery.trim(subCookies[j]);
                                if (subCookie.substring(0, subName.length + 1) == (subName + '=')) {
                                    cookieValue = decodeURIComponent(subCookie.substring(subName.length + 1));
                                    break;
                                }
                            }
                        }

                        break;
                    }

                }
            }
            return cookieValue;
        }

    } // Globals.cookie end



};
////////////////////////////////////////// Globals.searchObj end/////////////////////////////////


////////////////////////////////////////// Template 扩展 start/////////////////////////////////
var Template = Class.create();
Template.Pattern = /(^|.|\r|\n)(#\{(.*?)\})/;
Template.prototype = {
    init: function (template, pattern) {
        this.template = template.toString();
        this.pattern = pattern || Template.Pattern;
    },

    eval: function (object) {
        return this.template.gsub(this.pattern, function (match) {
            var before = match[1];
            if (before == '\\') return match[2];
            return before + String.interpret(object[match[3]]);
        });
    }
};
////////////////////////////////////////// Template 扩展 end/////////////////////////////////

////////////////////////////////////////// Array 扩展 start/////////////////////////////////
Array.from = function (iterable) {
    if (!iterable) return [];
    if (iterable.toArray) {
        return iterable.toArray();
    } else {
        var results = [];
        for (var i = 0, length = iterable.length; i < length; i++)
            results.push(iterable[i]);
        return results;
    }
};
////////////////////////////////////////// Array 扩展 end/////////////////////////////////

////////////////////////////////////////// Event 扩展 start/////////////////////////////////
if (!window.Event) {
    var Event = new Object();
}
Object.extend(Event, {
    element: function (event) {
        return $(event.target || event.srcElement);
    },
    pointerX: function (event) {
        return event.pageX || (event.clientX +
      (document.documentElement.scrollLeft || document.body.scrollLeft));
    },

    pointerY: function (event) {
        return event.pageY || (event.clientY +
      (document.documentElement.scrollTop || document.body.scrollTop));
    }
});
////////////////////////////////////////// Event 扩展 end/////////////////////////////////

var Pfizer = {
    version: '0.1',
    author: "min.jiang"

};

////////////////////////////////////////// ValidatorClass 扩展 start/////////////////////////////////
//===================================================================
// 示例代码:
//            validator对象可直接使用
//            validator.valid(txtInput.value, "notEmpty & real");

//===================================================================

var ValidatorClass = Pfizer.ValidatorClass = Class.create();
Object.extend(ValidatorClass.prototype, {
    name: "ValidatorClass",
    //初始化
    init: function () {
    },

    validType: {
        notEmpty: null,
        email: /^\w+((-\w+)|(\.\w+)|-)*\@\w+((\.|-)\w+)*\.\w+$/,
        // 字符串
        textRange: 20, // 校验字符长度，validator.valid(txtInput.value, "textRange", 5, 20);
        loginName: /[^@]+@{1}[^@\.]+\.+[^@]+|[0-9a-zA-Z\u4e00-\u9fa5]*/,
        passengerName: /^[a-zA-Z]{1,20}\/[a-zA-Z]{1,20}((| )[a-zA-Z]{1,20}){0,1}$/,  // 乘机人姓名Luo/zhi或者Luoz/asf sdf或者asfd/asd/ad
        nickName: /^([a-zA-Z]|[\u4e00-\u9fa5]|\/| )+$/,   // 联系人姓名不能包含数字与符号
        enString: /^[a-zA-Z]+$/, // 验证是否只包含英文字符
        cnString: /^[\u4e00-\u9fa5]+$/, // 验证是否全部包含中文字符
        nonSpecialSign: /^[^~!@#$%^&*！￥…]*$/, // 不包含特殊字符
        // 日期
        date: null,  // 判断是否是日期格式yyyy-mm-dd
        dateEn: null, // 判断是否是日期格式mm/dd/yyyy
        dateRange: null,   // 判断是否在日前区间内，validator.valid(txtInput.value, "dateRange", "2001-1-1", "2009-1-1");
        //文件名
        fileName: /^[a-z]:(\\[^\\\/:\*\?"><|]+)*\\?$/i,
        directoryName: /^[a-z]:(\\[^\\\/:\*\?"><|]+)+$/i,
        picFileName: /\.(jpg)|(jpeg)|(png)|(gif)$/i,
        attachFileName: /\.(doc)|(xls)|(txt)|(zip)|(rar)$/i,
        // 数字
        number: /^\d+$/,  // 数字
        zipCode: /^\d{6}$/, // 邮编
        idCard: null,    // 验证身份证有效性
        year: /^[1-2][0-9]{3}$/, // 年代
        integer: /^-?[0-9]{1,9}$/, // 整数
        //positiveInteger: /^[1-9][0-9]{0,10}$/,
        //notNegativeInteger: /^(0|([1-9][0-9]{0,10}))$/,
        real: /^-?[0-9]{1,28}(\.[0-9]*)?$/,
        //positiveReal: /^\d+(\.\d+)?$/,
        //notNegativeReal: /^(0|(\d+(\.\d+)?))$/,
        phone: /^\d{3,4}-\d{7,8}((\s|;)+\d{3,4}-\d{7,8})*$/,
        mobile: /^1[0-9]{10}$/ //以13|15|18开头的11数字
    },

    /////////////////////////////////////////////////////
    // 说明：校验通用方法
    // 参数：value：   需要校验的值
    //        validTyp: 需要校验的类型，格式如："notEmpty & real"
    //        params:   其他参数
    // 示例：validator.valid(txtInput.value, "notEmpty & real");
    /////////////////////////////////////////////////////
    valid: function (value, validType, params) {
        if (arguments.length < 2) { alert("validator.valid()缺少参数"); }

        var result = true;

        var arrValidateType = validType.trim().split("&");
        for (var i = 0; i < arrValidateType.length; i++) {
            if (!result) return false;

            var type = arrValidateType[i].trim();
            switch (type) {
                case "notEmpty":
                    result = !String.isNullOrEmpty(value);
                    break;
                default:
                    result = this.regularValidate(value, this.validType[type]);
                    break;

            }
        }

        return result;
    },
    regularValidate: function (value, reg) {
        reg.lastIndex = -1;
        if (value.length > 0) {
            return reg.test(value);
        }

        return true;
    }


});
var validator = new ValidatorClass();
////////////////////////////////////////// ValidatorClass 扩展 end/////////////////////////////////



////////////////////////////////////////// TipWindow 扩展 start/////////////////////////////////
//=============================================================================

var TipWindow = Pfizer.TipWindow;
TipWindow = Class.create();

Object.extend(TipWindow.prototype, {
    name: "TipWindow",
    template: new Template("<div class=\"com_way\" style=\"display:none; position: absolute; z-index: 2010;width: #{width}px;#{height}\"><div m=\"z\" class=\"z\" style=\"width: #{twidth}px;\"></div><div m=\"bj\" class=\"bj\"></div><div class=\"clear\"></div><div class=\"bk\" style=\"width: #{cwidth}px;#{cheight}\"><div class=\"bk_1\" style=\"width: #{bwidth}px; #{bheight}\">#{content}</div></div><div m=\"nz\" class=\"none\" style=\"width: #{twidth}px;\"></div><div m=\"nbj\" class=\"none\"></div><div class=\"clear\"></div></div>"),

    options: {
        htmlContent: "",              // 呈现html内容
        eventElement: null,           // 触发事件object
        buildHtmlContent: null,       // 构造显示内容事件，用于异步获得显示内容时
        width: 381,                   // 窗口宽度
        height: 0,                     //  窗口高度，不设置则为自适应
        defer: false,                  // 是否延迟出现
        autoClose: true,            // 是否鼠标移开自动关闭
        floatType: null,            // 是否固定位置：leftTop， leftBottom，rightTop，rightBottom，null(自动判断位置)
        initEvt: function () { }             // bind窗口内元素的事件, 参数windowElement为窗口父元素: function(windowElement){}
    },

    //初始化
    init: function (options) {
        Object.extend(Object.extend(this, this.options), options);

        // mouseover时鼠标停留一定时间才弹出
        this.eventElement.bind("mouseout", this.onMouseOutElement.bindAsEventListener(this));
        if (this.defer) {
            this.showTimer = FunctionExt.defer(function () {
                if (!Object.isNull(this.buildHtmlContent)) { this.buildHtmlContent(this); }
                else { this.show(); }
                clearTimeout(this.showTimer);
                this.showTimer = null;
            }, 600, this);
        }
        else {
            if (!Object.isNull(this.buildHtmlContent)) { this.buildHtmlContent(this); }
            else { this.show(); }
        }
    },

    show: function () {
        this.initDOM();
        this.initEvent();
        this.pageLoad();
        this.initEvt(this.windowElement);
    },

    initDOM: function () {
        this.contentEndRegion = $("#m_contentend");
        var content = this.template.eval({
            width: this.width,
            height: this.height == 0 ? "" : String.format("height:{0}px;", this.height),
            twidth: this.width - 29,
            cwidth: this.width - 2,
            cheight: this.height == 0 ? "" : String.format("height:{0}px;", this.height - 8),
            bwidth: this.width - 32,
            bheight: this.height == 0 ? "" : String.format("height:{0}px;", this.height - 36),
            content: this.htmlContent
        });
        this.windowElement = $(content).appendTo(this.contentEndRegion);
    },

    destroyDOM: function () {
        this.htmlContent = "";
        this.windowElement = null;
        this.eventElement = null;
        this.contentEndRegion = null;
    },

    initEvent: function () {
        this.windowElement.bind("mouseout", this.onMouseOutRegion.bindAsEventListener(this));
        this.windowElement.bind("mouseover", this.onMouseOverRegion.bindAsEventListener(this));
        FunctionExt.defer(this.onOutClick.bindAsEventListener(this), 100);
    },

    destroyEvent: function () {
        $(document).unbind("click", this.onOutClickHandler);
        this.windowElement.unbind("mouseout");
        this.windowElement.unbind("mouseover");
        this.eventElement.unbind("mouseout");
    },

    onMouseOutElement: function () {
        if (this.showTimer != null) {
            clearTimeout(this.showTimer);
            this.showTimer = null;
            this.eventElement.unbind("mouseout");
            //this.dispose();
        }
        // 自动消失
        this.setOutTimer();
    },

    onMouseOverRegion: function (evt) {
        if (this.outTimer != null) {
            clearTimeout(this.outTimer);
            this.outTimer = null;
        }
    },

    onMouseOutRegion: function (evt) {
        this.setOutTimer();
    },

    setOutTimer: function () {
        this.outTimer = FunctionExt.defer(function () {
            if (this.autoClose) { this.close(); }
        } .bind(this), 500, this);
    },

    onOutClick: function () {
        //        this.onOutClickHandler = function(evt) {
        //            var element = Event.element(evt);
        //            if (element.index(this.eventElement) == 0)
        //            { $(document).one("click", this.onOutClickHandler); }
        //            else if (this.windowElement.find(":descendant").index(element) == -1) { if (this.autoClose) { this.close(); } }
        //        } .bindAsEventListener(this);
        //        $(document).one("click", this.onOutClickHandler);
    },

    pageLoad: function () {
        // 设置大小位置
        var top, left;
        var showZone = this.getWindowZone(this.eventElement, this.windowElement);
        showZone = Object.isNull(this.floatType) ? showZone : this.floatType;
        switch (showZone) {
            case "leftTop":
                top = this.eventElement.offset().top - this.windowElement.height();
                left = this.eventElement.offset().left - this.width + this.eventElement.width() / 2 + 24;
                this.windowElement.find("div[m='z']").attr("class", "none");
                this.windowElement.find("div[m='bj']").attr("class", "none");
                this.windowElement.find("div[m='nz']").attr("class", "z_br");
                this.windowElement.find("div[m='nbj']").attr("class", "bj_br");
                this.windowElement.find("div.bk").attr("class", "bk_top");
                this.windowElement.find("div.bk_1").attr("class", "bk_top_1");
                break;
            case "leftBottom":
                top = this.eventElement.offset().top + this.eventElement.height() + 3;
                left = this.eventElement.offset().left - this.width + this.eventElement.width() / 2 + 24;
                break;
            case "rightTop":
                top = this.eventElement.offset().top - this.windowElement.height();
                left = this.eventElement.offset().left + this.eventElement.outerWidth() / 2 - 24;
                this.windowElement.find("div[m='z']").attr("class", "none");
                this.windowElement.find("div[m='bj']").attr("class", "none");
                this.windowElement.find("div[m='nz']").attr("class", "z_bl");
                this.windowElement.find("div[m='nbj']").attr("class", "bj_bl");
                this.windowElement.find("div.bk").attr("class", "bk_top");
                this.windowElement.find("div.bk_1").attr("class", "bk_top_1");
                break;
            case "rightBottom":
                top = this.eventElement.offset().top + this.eventElement.height() + 3;
                left = this.eventElement.offset().left + this.eventElement.outerWidth() / 2 - 24;
                this.windowElement.find("div[m='z']").attr("class", "z_tl");
                this.windowElement.find("div[m='bj']").attr("class", "bj_tl");
                this.windowElement.find("div[m='nz']").attr("class", "none");
                this.windowElement.find("div[m='nbj']").attr("class", "none");
                break;
        }

        this.windowElement[0].style.top = top + "px";
        this.windowElement[0].style.left = left + "px";
        this.ie6FilterIFrame = Globals.addIE6Filter(this.windowElement.width() + 10, this.windowElement.height(), left, top);
        this.windowElement.fadeIn("normal");


    },

    getWindowZone: function (eventElement, windowElement) {
        var zone = { leftTop: "leftTop", leftBottom: "leftBottom", rightTop: "rightTop", rightBottom: "rightBottom" };
        var scroll = Globals.getScrollPosition();
        var browserRegion = Globals.browserDimensions();
        var isRight = true;
        if (browserRegion.width - (eventElement.offset().left - scroll.x) < windowElement.width() &&
			eventElement.offset().left - scroll.x > windowElement.width()) { isRight = false; }

        if (isRight) {
            if (browserRegion.height - (eventElement.offset().top - scroll.y) > windowElement.height())
            { return zone.rightBottom }
            else if (eventElement.offset().top - scroll.y > windowElement.height()) { return zone.rightTop; }
            else { return zone.rightBottom }
        }
        else {
            if (browserRegion.height - (eventElement.offset().top - scroll.y) > windowElement.height())
            { return zone.leftBottom }
            else if (eventElement.offset().top - scroll.y > windowElement.height()) { return zone.leftTop; }
            else { return zone.leftBottom }
        }
    },



    close: function () {
        this.dispose();
    },

    dispose: function () {
        if (this.windowElement) {
            this.windowElement.fadeOut("normal");
            FunctionExt.defer(function () {
                if (this.windowElement) {
                    Globals.closeIE6Fliter(this.ie6FilterIFrame);
                    this.windowElement.remove();
                    this.destroyEvent();
                    this.destroyDOM();
                }
            } .bind(this), 500);
        }
    }
});

var ErrorTipWindow = Pfizer.ErrorTipWindow;
ErrorTipWindow = Class.create();

Object.extend(ErrorTipWindow.prototype, {
    name: "ErrorTipWindow",
    templateRegion: "<div style=\"display:none; position: absolute; z-index: 5000; \" class=\"com_bug\"><div class=\"w\">{0}</div></div>",

    options: {
        errorMsg: "",              // 错误信息
        align: "right",            // 布局：right,left
        eventElement: null,         // 触发事件object
        autoClose: true,           // 是否鼠标移开自动关闭
        width:20
    },

    //初始化
    init: function (options) {
        Object.extend(Object.extend(this, this.options), options);
        this.align = String.isNullOrEmpty(this.align) ? "right" : this.align;
        this.initDOM();
        this.initEvent();

        this.pageLoad();
    },

    initDOM: function () {
        this.contentEndRegion = $("#m_contentend");
        this.windowElement = $(String.format(this.templateRegion, this.errorMsg)).appendTo(this.contentEndRegion);
    },

    destroyDOM: function () {
        this.errorMsg = "";
        this.windowElement = null;
        this.eventElement = null;
        this.contentEndRegion = null;
    },

    initEvent: function () {
        this.windowElement.bind("mouseout", this.onMouseOutRegion.bindAsEventListener(this));
        this.windowElement.bind("mouseover", this.onMouseOverRegion.bindAsEventListener(this));
        FunctionExt.defer(this.onOutClick.bindAsEventListener(this), 100);
    },

    destroyEvent: function () {
        $(document).unbind("click", this.onOutClickHandler);
        this.windowElement.unbind("mouseout");
        this.windowElement.unbind("mouseover");
    },

    onOutClick: function () {
        //        this.onOutClickHandler = function(evt) {
        //            var element = Event.element(evt);
        //            if (this.windowElement.find(":descendant").index(element) == -1) { this.dispose(); }
        //            else { $(document).one("click", this.onOutClickHandler); }
        //        } .bindAsEventListener(this);
        //        $(document).one("click", this.onOutClickHandler);
    },

    onMouseOverRegion: function (evt) {
        var element = Event.element(evt);
        if (this.outTimer != null) {
            clearTimeout(this.outTimer);
            this.outTimer = null;
        }
    },

    onMouseOutRegion: function (evt) {
        var element = Event.element(evt);
        this.outTimer = FunctionExt.defer(function () {
            //            this.dispose();
            if (this.autoClose) { this.dispose(); }
        }, 10, this);
    },

    //    pageLoad: function () {
    //        // 设置大小位置
    //        var top = this.eventElement.offset().top + this.eventElement.height() - 17;
    //        var left = this.eventElement.offset().left + this.eventElement.width() + 5;
    //        this.windowElement[0].style.top = top + "px";
    //        if (this.align.toLowerCase() == "right") {
    //            this.windowElement[0].style.left = left + "px";
    //        }
    //        else {
    //            left = this.eventElement.offset().left - this.windowElement.width() - 5;
    //            this.windowElement[0].style.left = left + "px";
    //        }
    //        this.ie6FilterIFrame = Globals.addIE6Filter(this.windowElement.width(), this.windowElement.height(), left, top);
    //        this.windowElement.show();
    //        this.scrollToCenter(this.eventElement);

    //        this.eventElement.addClass("com_ErrorBox");
    //        if (this.eventElement.is("input")) { this.eventElement.select(); }
    //    },

    pageLoad: function () {
        // 设置大小位置
        var top, left;
        var showZone = this.getWindowZone(this.eventElement, this.windowElement);
        //showZone = Object.isNull(this.floatType) ? showZone : this.floatType;
        switch (showZone) {
            case "leftTop":
                top = this.eventElement.offset().top - this.windowElement.height();
                left = this.eventElement.offset().left - this.width + this.eventElement.width() / 2 + 24;
                break;
            case "leftBottom":
                top = this.eventElement.offset().top + this.eventElement.height() + 3;
                left = this.eventElement.offset().left - this.width + this.eventElement.width() / 2 + 24;
                break;
            case "rightTop":
                top = this.eventElement.offset().top - this.windowElement.height();
                left = this.eventElement.offset().left + this.eventElement.outerWidth() / 2 - 24;
                break;
            case "rightBottom":
                top = this.eventElement.offset().top + this.eventElement.height() + 3;
                left = this.eventElement.offset().left + this.eventElement.outerWidth() / 2 - 24;
                break;
        }
        this.windowElement[0].style.top = top + "px";
        this.windowElement[0].style.left = left + "px";
        this.windowElement.show();
        this.scrollToCenter(this.eventElement);
        if (this.eventElement.is("input")) { this.eventElement.select(); }


    },

    getWindowZone: function (eventElement, windowElement) {
        var zone = { leftTop: "leftTop", leftBottom: "leftBottom", rightTop: "rightTop", rightBottom: "rightBottom" };
        var scroll = Globals.getScrollPosition();
        var browserRegion = Globals.browserDimensions();
        var isRight = true;
        if (browserRegion.width - (eventElement.offset().left - scroll.x) < windowElement.width() &&
			eventElement.offset().left - scroll.x > windowElement.width()) { isRight = false; }

        if (isRight) {
            if (browserRegion.height - (eventElement.offset().top - scroll.y) > windowElement.height())
            { return zone.rightBottom }
            else if (eventElement.offset().top - scroll.y > windowElement.height()) { return zone.rightTop; }
            else { return zone.rightBottom }
        }
        else {
            if (browserRegion.height - (eventElement.offset().top - scroll.y) > windowElement.height())
            { return zone.leftBottom }
            else if (eventElement.offset().top - scroll.y > windowElement.height()) { return zone.leftTop; }
            else { return zone.leftBottom }
        }
    },
    // 滚动到屏幕中间
    scrollToCenter: function (element) {
        var scroll = Globals.getScrollPosition();
        var browserSize = Globals.browserDimensions();
        if (element.offset().top < scroll.y) {
            window.scrollTo(scroll.x, element.offset().top - browserSize.height / 2);
        }
    },

    dispose: function () {
        if (this.windowElement) {
            this.windowElement.hide();
            this.eventElement.removeClass("com_ErrorBox");
            FunctionExt.defer(function () {
                if (this.windowElement) {
                    Globals.closeIE6Fliter(this.ie6FilterIFrame);
                    this.windowElement.remove();
                    this.destroyEvent();
                    this.destroyDOM();
                }
            } .bind(this), 500);
        }
    }
});

function $error(element, msg, align) {
    //    if (!Object.isNull(this.tempErrorWindow)) {
    //        this.tempErrorWindow = null;
    //    }
    //    this.tempErrorWindow = new ErrorTipWindow({
    //        errorMsg: msg,
    //        align: align,
    //        eventElement: element,
    //        autoClose: true
    //    });


    //    if (Object.isNull(this.tempErrorWindow) || Object.isNull(this.tempErrorWindow.windowElement)) {
    //        this.tempErrorWindow = new ErrorTipWindow({
    //            errorMsg: msg,
    //            align: align,
    //            eventElement: element,
    //            autoClose: true
    //        });
    //    }
    //    else {
    //        element.addClass("com_ErrorBox");
    //        element.one("click", function(evt) {
    //            FunctionExt.defer(function() {
    //                if (Object.isNull(msg)) return;
    //                if (Object.isNull(this.tempErrorWindow) || Object.isNull(this.tempErrorWindow.windowElement)) {
    //                    this.tempErrorWindow = new ErrorTipWindow({
    //                        errorMsg: msg,
    //                        align: align,
    //                        eventElement: element
    //                    });
    //                }
    //            } .bind(this), 600);
    //        } .bind(this));
    //    }

    if (!Object.isNull(this.tempErrorWindow) && !Object.isNull(this.tempErrorWindow.windowElement)) {
        this.tempErrorWindow.dispose();
    }

    this.tempErrorWindow = new ErrorTipWindow({
        errorMsg: msg,
        align: align,
        eventElement: element,
        autoClose: true
    });
}
////////////////////////////////////////// TipWindow 扩展 end/////////////////////////////////





/***
* StringBuilder
***/
function StringBuilder() {
    this.strings = [];
}

StringBuilder.prototype.append = function (text) {
    this.strings.push(text);
};

StringBuilder.prototype.toString = function () {
    if (arguments.length == 0) {
        return this.strings.join("");
    }
    else {
        return this.strings.join(arguments[0]);
    }
};

StringBuilder.prototype.clear = function () {
    this.strings.clear();
};

StringBuilder.prototype.backspace = function () {
    this.strings.pop();
};
/////////////////////////////////////////////////////////////////////////////////////




//=============================================================================
// 文件名:		Dialog.js
// 版权:		Copyright (C) 2011 Pfizer
// 创建人:		min.jiang
// 创建日期:	2011-3-28
// 描述:		DialogWindow
// 备注:        
// 示例代码:
//        var wind = new Dialog({
//            title: "对话框的标题",
//            htmlContent: "<div>content</div>",
//            width: 480,
//            height: 240,
//            initEvent: function(windowElement) {
//               // 这里绑定窗口内元素的事件处理
//            } .bind(this)
//        });
//        wind.show();
//============================================================================= 

var Dialog = Pfizer.Dialog;
Dialog = Class.create();

Object.extend(Dialog.prototype, {
    name: "Dialog",
    windowTemplate: new Template("<div class=\"com_dialog com_widget com_widget-content com_corner-all com_draggable\" style=\"#{width};#{height};\">#{titleRegion}<div class=\"com_dialog-content com_widget-content\">#{content}</div></div>"),
    titleTemplate: "<div class=\"com_dialog-titlebar com_widget-header com_corner-all com_helper-clearfix\"><span class=\"com_dialog-title\">{0}</span><a class=\"com_dialog-titlebar-close com_corner-all\" href=\"#?\" method=\"close\">	<span class=\"com_icon com_icon-closethick\">close</span></a></div>",

    options: {
        title: "对话框",                    // 标题, 标题为空就不会出现标题栏
        htmlContent: "",              // 呈现html内容
        initEvent: null,              // bind窗口内元素的事件, 参数windowElement为窗口父元素: function(windowElement){}
        width: 380,
        height: 0,
        IsEnableClose: true
    },

    // *********************************所有设置说明*******************************
    //    窗口的背景层设置
    //    layerOptions: {
    //        Color: "#fff", //背景色
    //        Opacity: 50, //透明度(0-100)
    //        zIndex: 1000//层叠顺序
    //    },
    //    窗口的设置
    //    WindowOptions: {
    //        Fixed: true, //是否固定定位
    //        Over: true, //是否显示覆盖层
    //        Center: true, //是否居中
    //        onShow: function() { } //显示时执行
    //    },
    //    窗口的拖动设置
    //    DragOptions: {
    //        Limit: true, //是否设置限制(为true时下面参数有用,可以是负数)
    //        mxLeft: 0, //左边限制
    //        mxRight: 0, //右边限制
    //        mxTop: 0, //上边限制
    //        mxBottom: 0, //下边限制
    //        mxContainer: document.documentElement, //指定限制在容器内
    //        onMove: function() { }, //移动时执行
    //        Lock: false//是否锁定
    //    },

    //初始化
    init: function (options) {
        Object.extend(Object.extend(this, this.options), options);
        this.initDOM();

        this.window = new LightBox(this.windowElement, this.overlayer);
        if ($.browser.msie && $.browser.version <= 6) {
            this.window.Fixed = false;
        }
        if (this.windowElement.find("div.com_dialog-titlebar:eq(0)").length > 0) { // 是否有标题栏
            this.drag = new Drag(this.windowElement, this.windowElement.find("div.com_dialog-titlebar:eq(0)"));
        }
        this.overLay = this.window.OverLay;
        this.overLay.Color = "#cccccc";

        // 处理内部事件
        this.windowElement.find("[method='close']").bind("click", function (evt) { this.close(); } .bindAsEventListener(this));
        this.initEvent(this.windowElement);


    },

    initDOM: function () {
        this.contentEndRegion = $("#m_contentend");
        //add by min.jiang
        if (!this.IsEnableClose) {
            this.titleTemplate = "<div class=\"com_dialog-titlebar com_widget-header com_corner-all com_helper-clearfix\"><span class=\"com_dialog-title\">{0}</span></div>";
        }
        var content = this.windowTemplate.eval({
            titleRegion: String.isNullOrEmpty(this.title) ? "<div></div>" : String.format(this.titleTemplate, this.title),
            content: this.htmlContent,
            width: "width:" + this.width + "px",
            height: this.height > 0 ? "height:" + this.height + "px" : ""
        });
        this.windowElement = $(content).appendTo(this.contentEndRegion);
        this.overlayer = $("<div></div>").appendTo(this.contentEndRegion);
    },

    destroyDOM: function () {
        this.htmlContent = "";
        this.windowElement.remove();
        this.overlayer.remove();
        this.windowElement = null;
        this.contentEndRegion = null;
    },

    show: function () {
        this.window.Show();
    },

    close: function () {
        this.window.Close();
        this.destroyDOM();
    }
});

var OverLay = Class.create();
OverLay.prototype = {
    init: function (overlay, options) {
        this.Lay = overlay.get(0); //覆盖层

        //ie6设置覆盖层大小程序
        this._size = function () { };

        this.SetOptions(options);

        this.Color = this.options.Color;
        this.Opacity = parseInt(this.options.Opacity);
        this.zIndex = parseInt(this.options.zIndex);

        this.Set();
    },
    //设置默认属性
    SetOptions: function (options) {
        this.options = {//默认值
            Color: "#fff", //背景色
            Opacity: 50, //透明度(0-100)
            zIndex: 1000//层叠顺序
        };
        Object.extend(this.options, options || {});
    },
    //创建
    Set: function () {
        this.Lay.style.display = "none";
        this.Lay.style.zIndex = this.zIndex;
        this.Lay.style.left = this.Lay.style.top = 0;

        if (this.isIE6()) {
            this.Lay.style.position = "absolute";
            this._size = function () {
                this.Lay.style.width = Math.max(document.documentElement.scrollWidth, document.documentElement.clientWidth) + "px";
                this.Lay.style.height = Math.max(document.documentElement.scrollHeight, document.documentElement.clientHeight) + "px";
            } .bind(this);
            //遮盖select
            this.Lay.innerHTML = '<iframe style="position:absolute;top:0;left:0;width:100%;height:100%;filter:alpha(opacity=0);"></iframe>'
        } else {
            this.Lay.style.position = "fixed";
            this.Lay.style.width = this.Lay.style.height = "100%";
        }
    },
    //显示
    Show: function () {
        //设置样式
        this.Lay.style.backgroundColor = this.Color;
        //设置透明度
        if ($.browser.msie) {
            this.Lay.style.filter = "alpha(opacity:" + this.Opacity + ")";
        } else {
            this.Lay.style.opacity = this.Opacity / 100;
        }
        //兼容ie6
        if (this.isIE6()) { this._size(); window.attachEvent("onresize", this._size); }

        this.Lay.style.display = "block";
    },

    isIE6: function () {
        return $.browser.msie && $.browser.version == 6;
    },

    //关闭
    Close: function () {
        this.Lay.style.display = "none";
        if (this.isIE6()) { window.detachEvent("onresize", this._size); }
    }
};



var LightBox = Class.create();
LightBox.prototype = {
    init: function (box, overlay, options) {

        this.Box = box.get(0); //显示层

        this.OverLay = new OverLay(overlay, options); //覆盖层

        this.SetOptions(options);

        this.Fixed = !!this.options.Fixed;
        this.Over = !!this.options.Over;
        this.Center = !!this.options.Center;
        this.onShow = this.options.onShow;

        this.Box.style.zIndex = this.OverLay.zIndex + 1;
        this.Box.style.display = "none";

        //兼容ie6用的属性
        if (this.isIE6()) { this._top = this._left = 0; this._select = []; }
    },
    //设置默认属性
    SetOptions: function (options) {
        this.options = {//默认值
            Fixed: true, //是否固定定位
            Over: true, //是否显示覆盖层
            Center: true, //是否居中
            onShow: function () { } //显示时执行
        };
        Object.extend(this.options, options || {});
    },
    //兼容ie6的固定定位程序
    _fixed: function () {
        var iTop = document.documentElement.scrollTop - this._top + this.Box.offsetTop, iLeft = document.documentElement.scrollLeft - this._left + this.Box.offsetLeft;
        //居中时需要修正
        if (this.Center) { iTop += this.Box.offsetHeight / 2; iLeft += this.Box.offsetWidth / 2; }

        this.Box.style.top = iTop + "px"; this.Box.style.left = iLeft + "px";

        this._top = document.documentElement.scrollTop; this._left = document.documentElement.scrollLeft;
    },
    //显示
    Show: function (options) {
        //固定定位
        if (this.Fixed) {
            if (this.isIE6()) {
                //兼容ie6
                this.Box.style.position = "absolute";
                this._top = document.documentElement.scrollTop; this._left = document.documentElement.scrollLeft;
                window.attachEvent("onscroll", this._fixed.bind(this));
            } else {
                this.Box.style.position = "fixed";
            }
        } else {
            this.Box.style.position = "absolute";
        }
        //覆盖层
        if (this.Over) {
            //显示覆盖层，覆盖层自带select隐藏
            this.OverLay.Show();
        } else {
            //ie6需要把不在Box上的select隐藏
            if (this.isIE6()) {
                this._select = [];
                var oThis = this;
                Each(document.getElementsByTagName("select"), function (o) {
                    if (oThis.Box.contains ? !oThis.Box.contains(o) : !(oThis.Box.compareDocumentPosition(o) & 16)) {
                        o.style.visibility = "hidden"; oThis._select.push(o);
                    }
                })
            }
        }

        this.Box.style.display = "block";

        //居中
        if (this.Center) {
            this.Box.style.top = this.Box.style.left = "50%";
            //显示后才能获取
            var iTop = -this.Box.offsetHeight / 2, iLeft = -this.Box.offsetWidth / 2;
            //ie6或不是固定定位要修正
            if (!this.Fixed || this.isIE6()) { iTop += document.documentElement.scrollTop; iLeft += document.documentElement.scrollLeft; }
            this.Box.style.marginTop = iTop + "px"; this.Box.style.marginLeft = iLeft + "px";
        }

        this.onShow();
    },

    isIE6: function () {
        return $.browser.msie && $.browser.version == 6;
    },

    //关闭
    Close: function () {
        this.Box.style.display = "none";
        this.OverLay.Close();
        // if (this.isIE6()) { window.detachEvent("onscroll", this._fixed); Each(this._select, function(o) { o.style.visibility = "visible"; }); }
    }
};



function addEventHandler(oTarget, sEventType, fnHandler) {
    if (oTarget.addEventListener) {
        oTarget.addEventListener(sEventType, fnHandler, false);
    } else if (oTarget.attachEvent) {
        oTarget.attachEvent("on" + sEventType, fnHandler);
    } else {
        oTarget["on" + sEventType] = fnHandler;
    }
};

function removeEventHandler(oTarget, sEventType, fnHandler) {
    if (oTarget.removeEventListener) {
        oTarget.removeEventListener(sEventType, fnHandler, false);
    } else if (oTarget.detachEvent) {
        oTarget.detachEvent("on" + sEventType, fnHandler);
    } else {
        oTarget["on" + sEventType] = null;
    }
};

if (!$.browser.msie) {
    HTMLElement.prototype.__defineGetter__("currentStyle", function () {
        return this.ownerDocument.defaultView.getComputedStyle(this, null);
    });
}

//拖放程序
var Drag = Class.create();
Drag.prototype = {
    //拖放对象,触发对象
    init: function (obj, drag, options) {
        var oThis = this;

        this._obj = obj.get(0); //拖放对象
        this.Drag = drag.get(0); //触发对象
        this._x = this._y = 0; //记录鼠标相对拖放对象的位置
        //事件对象(用于移除事件)
        this._fM = function (e) { oThis.Move(window.event || e); }
        this._fS = function () { oThis.Stop(); }

        this.SetOptions(options);

        this.Limit = !!this.options.Limit;
        this.mxLeft = parseInt(this.options.mxLeft);
        this.mxRight = parseInt(this.options.mxRight);
        this.mxTop = parseInt(this.options.mxTop);
        this.mxBottom = parseInt(this.options.mxBottom);
        this.mxContainer = this.options.mxContainer;

        this.onMove = this.options.onMove;
        this.Lock = !!this.options.Lock;

        this._obj.style.position = "absolute";
        addEventHandler(this.Drag, "mousedown", function (e) { oThis.Start(window.event || e); });
    },
    //设置默认属性
    SetOptions: function (options) {
        this.options = {//默认值
            Limit: true, //是否设置限制(为true时下面参数有用,可以是负数)
            mxLeft: 0, //左边限制
            mxRight: 0, //右边限制
            mxTop: 0, //上边限制
            mxBottom: 0, //下边限制
            mxContainer: document.documentElement, //指定限制在容器内
            onMove: function () { }, //移动时执行
            Lock: false//是否锁定
        };
        Object.extend(this.options, options || {});
    },
    //准备拖动
    Start: function (oEvent) {
        if (this.Lock) { return; }
        //记录鼠标相对拖放对象的位置
        this._x = oEvent.clientX - this._obj.offsetLeft;
        this._y = oEvent.clientY - this._obj.offsetTop;
        //mousemove时移动 mouseup时停止
        addEventHandler(document, "mousemove", this._fM);
        addEventHandler(document, "mouseup", this._fS);
        //使鼠标移到窗口外也能释放
        if ($.browser.msie) {
            addEventHandler(this.Drag, "losecapture", this._fS);
            this.Drag.setCapture();
        } else {
            addEventHandler(window, "blur", this._fS);
        }
    },
    //拖动
    Move: function (oEvent) {
        //判断是否锁定
        if (this.Lock) { this.Stop(); return; }
        //清除选择(ie设置捕获后默认带这个)
        window.getSelection && window.getSelection().removeAllRanges();
        //当前鼠标位置减去相对拖放对象的位置得到offset位置
        var iLeft = oEvent.clientX - this._x, iTop = oEvent.clientY - this._y;
        //设置范围限制
        if (this.Limit) {
            //如果设置了容器,因为容器大小可能会变化所以每次都要设
            if (!!this.mxContainer) {
                this.mxLeft = this.mxTop = 0;
                this.mxRight = this.mxContainer.clientWidth;
                this.mxBottom = this.mxContainer.clientHeight;
            }
            //获取超出长度
            var iRight = iLeft + this._obj.offsetWidth - this.mxRight, iBottom = iTop + this._obj.offsetHeight - this.mxBottom;
            //这里是先设置右边下边再设置左边上边,可能会不准确
            if (iRight > 0) iLeft -= iRight;
            if (iBottom > 0) iTop -= iBottom;
            if (this.mxLeft > iLeft) iLeft = this.mxLeft;
            if (this.mxTop > iTop) iTop = this.mxTop;
        }
        //设置位置
        //由于offset是把margin也算进去的所以要减去
        this._obj.style.left = iLeft - (parseInt(this._obj.currentStyle.marginLeft) || 0) + "px";
        this._obj.style.top = iTop - (parseInt(this._obj.currentStyle.marginTop) || 0) + "px";
        //附加程序
        this.onMove();
    },
    //停止拖动
    Stop: function () {
        //移除事件
        removeEventHandler(document, "mousemove", this._fM);
        removeEventHandler(document, "mouseup", this._fS);
        if ($.browser.msie) {
            removeEventHandler(this.Drag, "losecapture", this._fS);
            this.Drag.releaseCapture();
        } else {
            removeEventHandler(window, "blur", this._fS);
        }
    }
};







Object.clone = function (obj) {
    var objClone;
    if (Object.isNull(obj)) { return null; }
    if (obj.constructor == Object) { objClone = new obj.constructor(); }
    else { objClone = new obj.constructor(obj.valueOf()); }
    for (var key in obj) {
        if (objClone[key] != obj[key]) {
            if (typeof (obj[key]) == 'object') {
                objClone[key] = Object.clone(obj[key]);
            }
            else {
                objClone[key] = obj[key];
            }
        }
    }
    objClone.toString = obj.toString;
    objClone.valueOf = obj.valueOf;
    return objClone;
};



