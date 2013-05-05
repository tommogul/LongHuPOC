////////////////////////

/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="jquery-ui-1.8.11.js" />

function showLoading() {
    $("#divLoading").show();

    $("#divLoading").get(0).style.position = "absolute";
    $("#divLoading").get(0).style.top = "50%"; $("#divLoading").get(0).style.left = "50%";
    $("#divLoading").get(0).style.marginTop = "-25px"; $("#divLoading").get(0).style.marginLeft = "-110px";
    var scroll = Globals.getScrollPosition();
    var browserSize = Globals.browserDimensions();
    if ($("#divLoading").offset().top < scroll.y) {
        window.scrollTo(scroll.x, $("#divLoading").offset().top - browserSize.height / 2);
    }
}

function hideLoading() {
    $("#divLoading").hide();
}

$(document).ready(function () {
    // Hide sub menu items
    $("#NavigationMenu .level2").each(function () {
        $(this).hide();
    });

    $('#NavigationMenu ul li').hover(function () {
        $(this).addClass('highlighted');
    }, function () {
        $(this).removeClass('highlighted');
    });

    // Register event handlers to level1 menu items
    $("#NavigationMenu .level1 li").each(function () {
        if ($(this).children().size() > 0) {
            $(this).hover(function () {
                $(this).find(".level2").show();
            }, function () {
                $(this).find(".level2").hide();
            });
        }
    });

    $("#NavigationMenu a").click(function () {
        if ($(this).attr("href") != "javascript:void(0);") {
            showLoading();
        }
    });
});

/////////////////////////

/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="jquery-ui-1.8.16.custom.js" />
(function ($) {
    $.extend({
        dynamicForm: function (action) {
            return $('<form name="DynamicForm" method="post" />').attr("action", action).appendTo("body");
        }
    });

    $.fn.extend({
        addHidden: function (name, value) {
            return $(this).append('<input type="hidden" name="' + name + '" value="' + value + '" />');
        }
    });

    $.fn.numeral = function () {
        $(this).css("ime-mode", "disabled");
        this.bind("keypress", function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (!$.browser.msie && (e.keyCode == 0x8)) {
                return;
            }
            return code >= 48 && code <= 57;
        });
        this.bind("blur", function () {
            if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
                this.value = this.value.substr(0, this.value.length - 1);
            } else if (isNaN(this.value)) {
                this.value = "";
            }
        });
        this.bind("paste", function () {
            var s = clipboardData.getData('text');
            if (!/\D/.test(s));
            value = s.replace(/^0*/, '');
            return false;
        });
        this.bind("dragenter", function () {
            return false;
        });
        this.bind("keyup", function () {
            if (/(^0+)/.test(this.value)) {
                this.value = this.value.replace(/^0*/, '');
            }
        });
    };

    $.fn.maxlength = function (settings) {
        if (typeof settings == 'string') {
            settings = { feedback: settings };
        }

        settings = $.extend({}, $.fn.maxlength.defaults, settings);

        function length(el) {
            var parts = el.value;
            if (settings.words)
                parts = el.value.length ? parts.split(/\s+/) : { length: 0 };
            return parts.length;
        }

        return this.each(function () {
            var field = this,
	        	$field = $(field),
	        	$form = $(field.form),
	        	limit = settings.useInput ? $form.find('input[name=maxlength]').val() : $field.attr('maxlength'),
	        	$charsLeft = $form.find(settings.feedback);

            function limitCheck(event) {
                var len = length(this),
	        	    exceeded = len >= limit,
	        		code = event.keyCode;

                if (!exceeded)
                    return;

                switch (code) {
                    case 8:  // allow delete
                    case 9:
                    case 17:
                    case 36: // and cursor keys
                    case 35:
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                    case 46:
                    case 65:
                        return;

                    default:
                        return settings.words && code != 32 && code != 13 && len == limit;
                }
            }


            var updateCount = function () {
                var len = length(field),
	            	diff = limit - len;

                $charsLeft.html(diff || "0");

                // truncation code
                if (settings.hardLimit && diff < 0) {
                    field.value = settings.words ?
                    // split by white space, capturing it in the result, then glue them back
	            		field.value.split(/(\s+)/, (limit * 2) - 1).join('') :
	            		field.value.substr(0, limit);

                    updateCount();
                }
            };

            $field.keyup(updateCount).change(updateCount);
            if (settings.hardLimit) {
                $field.keydown(limitCheck);
            }

            updateCount();
        });
    };

    $.fn.maxlength.defaults = {
        useInput: false,
        hardLimit: true,
        feedback: '.charsLeft',
        words: false
    };

    $.fn.disableSelection = function () {
        return this.each(function () {
            $(this).attr('unselectable', 'on')
               .css({
                   '-moz-user-select': 'none',
                   '-webkit-user-select': 'none',
                   'user-select': 'none',
                   '-ms-user-select': 'none'
               })
               .each(function () {
                   this.onselectstart = function () { return false; };
               });
        });
    };
})(jQuery);

$.datepicker.setDefaults({
    beforeShow: function (input, inst) {
        inst.dpDiv.css({
            zIndex: 9999
        });
    }
});

var compareByDate = function (date1, date2) {
    var one_minute = 1000 * 60;
    var time1 = Math.ceil(date1.getTime() / one_minute);
    var time2 = Math.ceil(date2.getTime() / one_minute);
    return time1 - time2;
};

var parseDate = function (dateStr, dateSplit, timeSplit) {
    var dateParts = dateStr.split(" ")[0].split((dateSplit == null ? "-" : dateSplit));
    var timeParts = dateStr.split(" ")[1].split((timeSplit == null ? ":" : timeSplit));
    return new Date(dateParts[0], (dateParts[1] - 1), dateParts[2], timeParts[0], timeParts[1], timeParts[2]);
};

var addDays = function (date, days) {
    var one_day = 1000 * 60 * 60 * 24;
    return new Date(date.getTime() + one_day * days);
};

var addHours = function (date, hours) {
    var one_hour = 1000 * 60 * 60;
    return new Date(date.getTime() + one_hour * hours);
};

$(document).ready(function () {
    $(".datefield").datepicker();
    $("#divPopup").dialog({
        width: $(window).width() - 200,
        height: $(window).height() - 50,
        resizable: false,
        modal: true,
        autoOpen: false,
        close: function () {
            if (IsPopupSaved == "True") {
                RefreshView();
            }
            var frmPopup = document.getElementById("frmPopup");
            frmPopup.src = "/Content/static/loading.html";
        }
    });
    hideLoading();

    window.winPopup = null;
});
var IsPopupSaved = "False";
var ShowType = "List"
function Popup(url, d_width, d_height) {
    if (ShowType == "List") {
        var frmPopup = document.getElementById("frmPopup");
        var urlConnecter = url.indexOf("?") > -1 ? "&" : "?";
        frmPopup.src = url + urlConnecter + "timeStamp=" + new Date().getTime();
        if (d_width != undefined) {
            $("#divPopup").dialog({ width: d_width });
        } else {
            var d_width = $(window).width() - 200;
            if (d_width < 1000) {
                d_width = $(window).width() - 20;
            }
            $("#divPopup").dialog({ width: d_width });
        }
        if (d_height != undefined) {
            $("#divPopup").dialog({ height: d_height });
        } else {
            $("#divPopup").dialog({ height: $(window).height() - 50 });
        }
        $("#divPopup").dialog("open");
    } else {
        var pop_width = screen.width - 140;
        if (pop_width < 1000) {
            pop_width = screen.width - 20;
        }
        var pop_height = screen.height - 200;
        var pop_left = (screen.width - pop_width) / 2;
        var pop_top = (screen.height - pop_height) / 2;
        if (d_width != undefined) {
            pop_width = d_width;
        }
        if (d_height != undefined) {
            pop_height = d_height;
        }
        if (window.winPopup != null) {
            window.winPopup.close();
        }
        window.winPopup = window.open(url, "myWindow", "status=1,,scrollbars=yes,left=" + pop_left + ",top=" + pop_top + ",width=" + pop_width + ",height=" + pop_height + ",resizable=0");
    }
}
function ShowMessage(message, afterFunction) {
    $(document).ready(function () {
        $("#message").html(message);
        $("#dialog-message").dialog({
            bgiframe: true,
            modal: true,
            close: function () {
                if (afterFunction != undefined) {
                    afterFunction();
                }
            },
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
    });
}
function ClosePopup() {
    $("#divPopup").dialog("close");
}
function CloseThis() {
    if (opener != null && opener.ClosePopup != undefined) {
        if (opener.ShowType == undefined ||
            opener.ShowType == "List") {
            opener.ClosePopup();
        } else {
            if (opener.IsPopupSaved == "True") {
                opener.RefreshView();
            }
            window.close();
        }
    }
    if (parent != null && parent.ClosePopup != undefined) {
        if (parent.ShowType == undefined ||
            parent.ShowType == "List") {
            parent.ClosePopup();
        } else {
            if (parent.IsPopupSaved == "True") {
                parent.RefreshView();
            }
            window.close();
        }
    }
    return false;
}
function RefreshParent() {
    if (opener != null && opener.IsPopupSaved != undefined) {
        opener.IsPopupSaved = "True";
    }
    if (parent != null && parent.IsPopupSaved != undefined) {
        parent.IsPopupSaved = "True";
    }
}
function async_autocomplete(url, displayFieldId, valueFieldId) {
    $("#" + displayFieldId).autocomplete({
        minLength: 2,
        source: function (request, response) {
            $.ajax({
                url: url,
                dataType: "json",
                data: {
                    keywords: $("#" + displayFieldId).val()
                },
                success: function (data) {
                    response(data);
                }
            });
        },
        select: function (event, ui) {
            $("#" + displayFieldId).val(ui.item.text);
            $("#" + valueFieldId).val(ui.item.value);
            return false;
        },
        focus: function (event, ui) {
            $("#" + displayFieldId).val(ui.item.text);
            $("#" + valueFieldId).val(ui.item.value);
            return false;
        },
        open: function (event, ui) {
            $(this).autocomplete("widget").css({
                "width": 400
            });
        }
    });
}
function updateTimeStamp(url) {
    if (url.indexOf("timeStamp=") < 0) {
        var urlConnecter = url.indexOf("?") > -1 ? "&" : "?";
        return url + urlConnecter + "timeStamp=" + new Date().getTime();
    }
    else {
        return url.substr(0, url.indexOf("timeStamp=")) + "timeStamp=" + new Date().getTime();
    }
}
var updateDynamicParam = null;
function AddAntiForgeryToken(data) {
    data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
    return data;
}


///  page  /
function searchListView(actionType, pageIndex, IsAsc, SortBy) {
    var url = "/" + $("#hidControllerName").val() + "/SearchListPartialView";
    var condition = { sort: 1 };
    var FormCondition = $("#hidPagerCondition").val();
    if (FormCondition != "") {
        eval(FormCondition);
    }
    if (actionType == "PageIndex") {
        condition["PageIndex"] = pageIndex;
    }
    else if (actionType == "Sort") {
        condition["SortBy"] = SortBy;
        condition["IsAsc"] = IsAsc;
    }
    else if (actionType == "Delete") {
        //do nothing, only reseach data
    }
    var param = $.toJSON(condition);
    $.ajax({
        url: url,
        type: "POST",
        dataType: "html",
        data: condition,
        beforeSend: function (XMLHttpRequest) {
            showLoading();
        },
        success: function (data) {
            hideLoading();
            $("#tbListView").html(data);
            bindListView();
        }
            ,
        error: function (XmlHttpRequest, textStatus, errorThrown) {
            hideLoading();
            var errorMsg = XmlHttpRequest.responseText;
            alert($("#hidCommonErrorMsg").val());
        }
    });
}
function pager_form_submit(pageIndex, isAjax) {
    searchListView("PageIndex", pageIndex);
}
function bindPager() {
    $('#pager a.enabled').click(function () {
        pager_form_submit($(this).attr("data"), false);
    });
    $('#pager .num').numeral();
    $('#pager .num').change(function () {
        if ($(this).val() != "") {
            pager_form_submit($(this).val() - 1, false);
        }
    });
}

function sort_submit(IsAsc, SortBy) {
    searchListView("Sort", null, IsAsc, SortBy);
}
function bindListSort() {
    $("tr[class='header'] th[mth='Sort']").each(function (i) {

        $(this).click(function () {
            sort_submit($(this).attr("IsAsc"), $(this).attr("SortBy"));
        });
    });
}
function bindListView() {
    bindPager();
    bindListSort();
    deleteData();
    resetGridTableHeight();
}

$(document).ready(function () {

    bindListView();
    duplicateSubmitForm();



});
function resetGridTableHeight() {
    $(".gridViewOverflow").height($(".gridViewOverflow").height() + 30);
}
function deleteData() {
    $(".deleteLink").click(function (event) {
        event.preventDefault();
        var recordIdToDelete = $(this).attr("data-id");
        var delConfirmMessage = $(this).attr("data-delConfirmMessage");
        var controllerName = "/" + $("#hidControllerName").val() + "/";


        if (recordIdToDelete != '') {
            var $dialog = $('<div></div>')
              .html(delConfirmMessage)
              .dialog({
                  autoOpen: true,
                  title: 'Are you sure?',
                  width: 400,
                  resizable: false,
                  draggable: false,
                  modal: true,
                  height: 160,
                  buttons: {
                      "Delete it!": function () {
                          $.post(controllerName + "Delete", { "id": recordIdToDelete }, function (data) {
                              hideLoading();
                              $dialog.dialog("close");
                              if (typeof (data) == "undefined" || data == null || !data.hasOwnProperty("IsSuccess")) {

                                  //$('#row-' + recordIdToDelete).fadeOut(1000);
                                  searchListView("Delete", null, null, null);
                              }
                              else {
                                  var $dialogOk = $('<div></div>')
                                       .html(data.Message)
                                       .dialog({
                                           modal: true,
                                           buttons: {
                                               Ok: function () {
                                                   $(this).dialog("close");
                                               }
                                           }
                                       });
                                  $dialogOk.dialog('open');
                              }
                          })
                          .error(function () { alert($("#hidCommonErrorMsg").val()); });
                      },
                      Cancel: {
                          text: 'Cancel',
                          "class": 'cancel',
                          click: function () { $(this).dialog("close"); }
                      }
                  }
              });
            $dialog.dialog('open');
        }
    });
}
function duplicateSubmitForm() {
    $(":submit").removeAttr("disabled");
    var btn = $(":submit[mth='DisableDuplicateSubmitForm']");
    var controllerName = "/" + $("#hidControllerName").val() + "/";
    btn.each(function () {
        $(this).click(function () {
            $(":submit").attr("disabled", "disabled");
            var form = $(this).parents('form:first');
            if (form.valid()) {
                var actionName = $(this).attr("name");
                if (actionName != undefined && actionName != null && actionName != '') {
                    form.attr("action", controllerName + actionName);
                }
                showLoading();
                form.submit();

                return true;
            }
            else {
                $(":submit").removeAttr("disabled");
            }
        });
    });
}




