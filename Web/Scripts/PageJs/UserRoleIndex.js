
var UserRoleIndexControl = Pfizer.UserRoleIndexControl;
UserRoleIndexControl = Class.create();
var CurrentPage = null;
Object.extend(UserRoleIndexControl.prototype, {
    name: "UserRoleIndexControl",

   
    init: function (options) {
        this.initDOM();
        this.initEvent();
        this.pageLoad();
    },

    initDOM: function () {
        this.EmployeeName = $("#UserInfoName");
        this.EmployeeId = $("#UserInfoId");
        this.searchUserUrl = "/Home/GetEmployee";
    },

    LoadFunction: function () {
        this.EmployeeName.autocomplete({
            source: function (request, response) {
                if (this.EmployeeName.val().length > 1) {
                    $.ajax({
                        url: this.searchUserUrl,
                        type: "GET",
                        dataType: "json",
                        data: AddAntiForgeryToken({ EmployeeName: this.EmployeeName.val() }),
                        success: function (data) {
                            response($.map(data, function (item) {
                                return { label: item.text, value: item.value };
                            }));
                        }
                    });
                } else { return; }
            } .bind(this),
            focus: function (event, ui) {
                this.EmployeeName.val(ui.item.label);
                return false;
            } .bind(this),
            select: function (event, ui) {
                this.EmployeeName.val(ui.item.label);
                this.EmployeeId.val(ui.item.value);
                return false;
            } .bind(this)
        }).bind(this);
    },

    destroyDOM: function () {

    },

    initEvent: function () {
        this.EmployeeName.bind("change", this.OnEmployeeNameChange.bindAsEventListener(this));
    },

    OnEmployeeNameChange: function () {
        var value = this.EmployeeName.val();
        if (String.isNullOrEmpty(value)) {
            this.EmployeeId.val("");
        }
    },

    destroyEvent: function () {
    },

    pageLoad: function () {
        this.LoadFunction();
    },

    dispose: function () {
        this.destroyEvent();
        this.destroyDOM();
    }
});

$(document).ready(function () {
    var client = new UserRoleIndexControl();
    CurrentPage = client;
});

