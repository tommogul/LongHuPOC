//===================================================================
// 文件名:		RoleAction.js
// 版权:		Copyright (C) 2012 Pfizer
// 创建人:		long.chen
// 创建日期:	2012-10-8 15:00
// 描述:		主页
// 备注:
//===================================================================

var RoleActionControl = Pfizer.RoleActionControl;
RoleActionControl = Class.create();
var CurrentPage = null;
Object.extend(RoleActionControl.prototype, {
    name: "RoleActionControl",

    //初始化
    init: function (options) {
        this.initDOM();
        this.initEvent();
        this.pageLoad();

    },

    initDOM: function () {
        this.DropDownListRoleInfo = $("[name='RoleInfoId']");
        this.Url = "/RoleAction/Create?RoleId=";
        this.treeView = $("#divtreeview");
    },

    destroyDOM: function () {

    },

    initEvent: function () {
        this.DropDownListRoleInfo.bind("change", this.OnSelectChange.bindAsEventListener(this));
    },

    destroyEvent: function () {
    },

    pageLoad: function () {
        this.LoadTreeview();
    },

    dispose: function () {
        this.destroyEvent();
        this.destroyDOM();
    },

    OnSelectChange: function () {
        var RoleId = this.DropDownListRoleInfo.val();
        if (!String.isNullOrEmpty(RoleId)) {
            window.location.href = this.Url + RoleId;
        }
    },

    LoadTreeview: function () {
        this.treeView.treeview({
            persist: "location",
            collapsed: true,
            unique: true
        });
    }
});

$(document).ready(function () {
    var client = new RoleActionControl();
    CurrentPage = client;
});



