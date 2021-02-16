
var alertTemplate =
    '<div id="dialog-alert" class="modal" role="dialog">' +
    '<div class="modal-dialog">' +
    '<div class="modal-content">' +
    '<div class="modal-header">' +
    '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
    '<h4 class="modal-title">پیام سیستم</h4>' +
    '</div>' +
    '<div class="modal-body">' +
    '<p id="dialog-alert-title"></p>' +
    '</div>' +
    '<div class="modal-footer">' +
    '<button id="alert-ok-button" class="btn btn-primary">بستن</button>' +
    '</div>' +
    '</div>' +
    '</div>' +
    '</div>';

var confirmTemplate =
    '<div id="dialog-confirm" class="modal" style="overflow-y: hidden;" role="dialog">' +
    '<div class="modal-dialog">' +
    '<div class="modal-content">' +
    '<div class="modal-header">' +
    '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
    '<h4 class="modal-title">پیام سیستم</h4>' +
    '</div>' +
    '<div class="modal-body">' +
    '<p id="dialog-confirm-title"></p>' +
    '</div>' +
    '<div class="modal-footer">' +
    '<button id="confirm-cancel-button" class="btn btn-primary">خیر</button>&nbsp;&nbsp;' +
    '<button id="confirm-ok-button" class="btn btn-primary">بله</button>' +
    '</div>' +
    '</div>' +
    '</div>' +
    '</div>';

function confirmMessage(message, callback) {
    if (!$("#dialog-confirm").length) {
        $("body").append(confirmTemplate);
    }

    $("#dialog-confirm-title").html(message);
    $("#dialog-confirm").modal({ backdrop: 'static', keyboard: false });

    $("#confirm-ok-button").on("click", null).off("click").on("click", function () {
        $("#dialog-confirm").modal('hide');
        if (callback)
            callback();
    });
    $("#confirm-cancel-button").on("click", function () {
        $("#dialog-confirm").modal('hide');
    });
}
function callbackAlert(message, callback) {
    if (!$("#dialog-alert").length) {
        $("body").append(alertTemplate);
    }

    $("#dialog-alert-title").html(message);
    $("#dialog-alert").modal('show');

    $("#alert-ok-button").on("click", null).off("click").on("click", function () {
        $("#dialog-alert").modal('hide');
        if (callback)
            callback();
    });
}

function loadRemoteSelect(target, url, params, key, value, firstItemValue, callback) {
    firstItemValue = firstItemValue ? firstItemValue : "...";
    $(target).empty();
    $(target).append("<option value=\"\">" + firstItemValue + "</option>");
    $.get(url, params, function (res) {
        if (res.isValid == true) {
            var data = res.data;
            for (var i = 0; i < data.list.length; i++) {
                var item = data.list[i];
                $(target).append("<option value=\"" + item[key] + "\">" + item[value] + "</option>")
            }
            if (callback)
                callback(res.data);
        }
        else {
            alert(res.message);
        }
    });
}

function getEntity(form) {
    var entity = {};
    var context = form ? form : document;
    $(context).find("[name]").each(function (index, element) {
        var key = $(element).attr("name");
        var value = "";

        var tagName = $(element).tagName().toLowerCase();
        if (tagName == "input") {
            var type = $(element).attr("type");
            switch (type) {
                case "text":
                default:
                    value = $(element).val();

                    if ($(element).hasClass("money-separator"))
                        value = removeSeparator(value);

                    break;
                case "checkbox":
                    if ($(context).find("input[type=checkbox][name=" + key + "]").length > 1) {
                        value = $(context).find("input[type=checkbox][name=" + key + "]:checked").map(function () {
                            return $(this).val();
                        }).get();
                    }
                    else {
                        value = $(context).find("input[type=checkbox][name=" + key + "]").prop("checked");
                    }
                    break;
                case "radio":
                    value = $(context).find("input[type=radio][name=" + key + "]:checked").val();
                    break;
            }
        } else if (tagName == "select") {
            value = $(element).find('option:selected').val();
        } else if (tagName == "textarea") {
            value = $(element).val();
        } else {
            value = $(element).html();
        }
        entity[key] = value;
    });

    return entity;
}

function setEntity(entity, form) {
    var context = form ? form : document;
    $(context).find("[name]").each(function (index, element) {
        var key = $(element).attr("name");
        var value = entity[key];
        var tagName = $(element).tagName().toLowerCase();
        if (tagName == "input") {
            var type = $(element).attr("type");
            switch (type) {
                case "text":
                default:
                    $(element).val(value);

                    if ($(element).hasClass("money-separator"))
                        moneySeparator(element);

                    break;
                case "checkbox":
                case "radio":
                    var val = $(element).val();
                    if (typeof value == "boolean") {
                        if (value && val == "false") {
                            $(element).prop("checked", false);
                        }
                        else {
                            $(element).prop("checked", true);
                        }
                    }
                    else if (typeof value == "object") {
                        if (value != null && findInArray(value, val)) {
                            $(element).prop("checked", true);
                        } else {
                            $(element).prop("checked", false);
                        }
                    }
                    else {
                        if (value != null && value == val) {
                            $(element).prop("checked", true);
                        } else {
                            $(element).prop("checked", false);
                        }
                    }
                    break;
            }
        } else if (tagName == "select" || tagName == "textarea") {
            if (value != null)
                $(element).val(value.toString());
            else
                $(element).val("");
        } else {
            $(element).html(value);
        }
    });
}
