/// <reference path="jquery-1.7.2.min.js" />
/// <reference path="admin.datepicker.js" />
/// <reference path="validate.form.func.js" />


/*初始化一些基本的设置*/
$(function () {
    /*bool*/
    $("input.input-bool").change(function () {
        syncValforBoolInput(this);
        /*发现Jquery获取已经被勾上的checkbox，永远都是"checked" */
    })

    /*int32*/
    $("input.input-int32").keydown(
        function (evt) {
            ensureInt32(evt);
        })
    /*decimal*/
    $("input.input-decimal").keydown(
       function (evt) {
           ensureDecimal(evt);
       })

    /*no comma*/
    $('input.input-no-comma').keydown(
        function (evt) {
            ensureNoComma(evt);
        }
       )

    /*calendar*/
    $("input.input-datetime").attr('readonly', 'readonly');
    $("input.input-datetime").focus(function () {
        HS_setDate(this);
    })

    /*input state tracking*/
    $('input.input-state-tracking').each(function () {
        $(this).data('input.state', 0);//unchange
    })

    /*initialize state*/
    $('input.input-state-tracking').change(function () {
        $(this).data('input.state', 1);//changed
    })
})

//onkeydown，make sure that the chars input are numeric
function ensureInt32(evt) {
    var iKeyCode = window.event ? event.keyCode : evt.which;
    if (((iKeyCode >= 48) && (iKeyCode <= 57))
        || ((iKeyCode >= 96) && (iKeyCode <= 105))
        || ((iKeyCode >= 37) && (iKeyCode <= 40))
        || iKeyCode == 8 || iKeyCode == 127
        || iKeyCode == 46) {
    }
    else {
        ntAlert('这里必须填写数字!', function () {
            if (window.event) //IE
                event.srcElement.focus();
            else
                evt.target.focus();
        });
        if (window.event) //IE
            event.returnValue = false;
        else //Firefox
            evt.preventDefault();
    }
}

//onkeydown，make sure that the chars input are decimal
function ensureDecimal(evt) {
    var iKeyCode = window.event ? event.keyCode : evt.which;
    if (((iKeyCode >= 48) && (iKeyCode <= 57))
        || ((iKeyCode >= 96) && (iKeyCode <= 105))
        || ((iKeyCode >= 37) && (iKeyCode <= 40))
        || iKeyCode == 8 || iKeyCode == 127
        || iKeyCode == 46 || iKeyCode == 110) {
    }
    else {
        ntAlert('这里必须填写数字或小数点!', function () {
            if (window.event) //IE
                event.srcElement.focus();
            else
                evt.target.focus();
        });
        if (window.event) //IE
            event.returnValue = false;
        else //Firefox
            evt.preventDefault();
    }
}

//onkeydown  make sure that the string input contains no comma(in english)
function ensureNoComma(evt) {
    var iKeyCode = window.event ? event.keyCode : evt.which;
    if (iKeyCode == 44) {
        if (window.event) //IE
            event.returnValue = false;
        else //Firefox
            evt.preventDefault();
    }
}

//onchange 
function syncValforBoolInput(sender) {
    var id = $(sender).attr('id');
    if (sender.checked)
        $('input[for="' + id + '"]').val('True');
    else
        $('input[for="' + id + '"]').val('False');
}

//onchange
function changeState(sender) {
    $(sender).data('input.state', 1);
}

function acceptAllChanges(context) {
    if (context)
        $('input.input-state-tracking', context).each(function () {
            $(this).data('input.state', 0);//unchange
        })
    else
        $('input.input-state-tracking').each(function () {
            $(this).data('input.state', 0);//unchange
        })
}

/*保存备注*/
function saveNote(id, note, tableName) {
    ntAjax(
        {
            method: "SaveNote",
            data: '{"id":"' + id + '","note":"' + note + '","tableName":"' + tableName + '"}',
            success: function (msg) {
                var json = $.parseJSON(msg.d);
                ntAlert(json.message);
            }
        }
        )
}

/*目录迁移*/
function treeMigrate(from, to) {
    ntAjax(
        {
            method: "TreeMigrate",
            data: '{"from":"' + from + '","to":"' + to + '"}',
            success: function (msg) {
                var json = $.parseJSON(msg.d);
                if (json.error)
                    ntAlert(json.message);
                else
                    window.location.href = window.location.href;
            }
        }
        )
}

var nt = nt || {};
/*options setData, ajaxUrl,validateForm,beforeSerialize,formID*/
nt.ntDialog = function (ajaxUrl, setData, validateForm, beforeSerialize, formID) {

    var self = this;
    self.ajaxUrl = ajaxUrl;
    self.formID = formID || 'NtDialogForm';
    self.formSelector = '#' + self.formID;
    self.setData = setData || function (json) { };
    self.validateForm = validateForm || function () { return true; };
    self.beforeSerialize = beforeSerialize || function () { };

    self.showDialog = function () {
        nt.useParentContext = 'no';//here we do not use parent window as the context.
        centerizeElement("#nt-dialog-edit");
        $("#nt-dialog-edit").show();
        showMask();
    }

    self.closeDialog = function () {
        $("#nt-dialog-edit").hide();
        document.getElementById(self.formID).reset();
        removeMask();
        nt.useParentContext = 'auto';
    }

    /*add*/
    self.ajaxCommonAdd = function () {
        $(self.formSelector).find("input[name='Id']").val(-8964);
        $(self.formSelector).find("input[name='method']").val('PUT');
        self.showDialog();
    }

    self.ajaxCommonEdit = function (id) {
        $(self.formSelector).find("input[name='Id']").val(id);
        $(self.formSelector).find("input[name='method']").val('POST');
        var data = {};
        data.Id = id;
        data.method = 'GET';
        $.get(
            self.ajaxUrl + '?' + Math.random(),
            data,
            function (json) {
                if (json.error)
                    ntAlert(json.message);
                else {
                    self.setData(json);
                    self.showDialog();
                }
            },
            'json'
            )
    }

    self.ajaxCommonCancel = function () { self.closeDialog(); }

    self.ajaxCommonSave = function () {
        var settings = {};
        settings.resetForm = true;
        settings.error = function () { ntAlert('操作错误!'); }
        settings.dataType = 'json';
        settings.url = ajaxUrl;
        settings.beforeSerialize = self.beforeSerialize;
        settings.beforeSubmit = function () {
            if (validateForm && !validateForm())
                return false;
            return true;
        }
        settings.success = function (json) {
            ajaxCommonCancel();
            removeMask();
            if (json.error) {
                ntAlert(json.message);
            }
            else {
                ntAlert(json.message, function () {
                    window.location.reload();
                });
            }
        }
        $(self.formSelector).ajaxSubmit(settings);
    }

    self.ajaxCommonDel = function (id) { del(id); }

    window.ajaxCommonAdd = self.ajaxCommonAdd;
    window.ajaxCommonDel = self.ajaxCommonDel;
    window.ajaxCommonSave = self.ajaxCommonSave;
    window.ajaxCommonCancel = self.ajaxCommonCancel;
    window.ajaxCommonEdit = self.ajaxCommonEdit;
};

/*
addNewComplete:atfer insert one record,edit your html.
validate while post data to server.
*/
nt.pictureMgr = function (addNewComplete, validate) {
    var self = this;
    self.NO_IMAGE = '/upload/product-pictures/no-image.gif';
    self.ajaxUrl = '/Netin/Handlers/ProductPictureHandler.ashx';
    self.currentRow = -100;
    self.addNewComplete = addNewComplete || function () {
        ntAlert('please provide a function after “add a new record” complement!');
    }
    self.validate = validate || function (row) {
        ntAlert('please provide a function to validate !');
        return false;
    }
    /*begin uploading image data*/
    self.uploadPicture = function () {
        var thisForm = null;
        if (useParentContextOrNot())
            thisForm = parent.document.getElementById('nt-upload-form');
        else
            thisForm = document.getElementById('nt-upload-form');
        if (thisForm.imgFile.value == '') {
            ntAlert('请选择您要上传的文件');
            return false;
        }
        var data = {};
        data.Id = self.currentRow;
        data.method = 'UPLOAD';
        $(thisForm).ajaxSubmit({
            url: self.ajaxUrl,
            data: data,
            forceSync: true,
            dataType: 'json',
            beforeSubmit: function () {
                closeUploadDialog();
                showLoading();
            },
            success: function (json, statusText) {
                if (json.error)
                    ntAlert(json.message, function () {
                        removeLoading();
                    });
                else {
                    var r = $('#nt-picture-row-' + self.currentRow);
                    r.find('img').attr('src', json.ThumnailUrl);
                    r.find('input[name="Picture.PictureUrl"]').val(json.PictureUrl);
                    r.data('deleted', 0);
                    removeLoading();
                }
            },
            error: function () {
                ntAlert('Error!', function () {
                    removeLoading();
                });
            }
        });
        return false;
    }

    /*add a new record public*/
    self.addNew = function () {
        showLoading();
        var data = {};
        data.method = 'INSERT';
        var rand = Math.random();
        $.get(
            self.ajaxUrl + '?' + rand, //under ie 8 there exist a problem of cache
            data,
            function (json) {
                if (json.error)
                    ntAlert(json.message, function () {
                        removeLoading();
                    });
                else {
                    self.addNewComplete(json);
                    var row = $('#nt-picture-row-' + json.model.Id);
                    row.data('deleted', 1);//while a new record added,there is no picture,so we set it deleted
                    removeLoading();
                }
            },
            'json'
        );
    }

    /*this method should be public*/
    self.uploadNew = function (id) {
        self.currentRow = id;
        openUploadDialog(self.uploadPicture, '上传图片');
    }

    /*delete specified image from server*/
    self.deleteThis = function (id) {
        var row = $('#nt-picture-row-' + id);
        if (row.data('deleted')) {
            ntAlert('图片已经被删除!');
            return;
        }
        ntConfirm("您确定删除此图片吗?", function () {
            var data = {};
            data.method = "DELETE_PICTURE";
            data.id = id;
            $.post(
                self.ajaxUrl,
                data,
                function (json) {
                    if (json.error)
                        ntAlert(json.message);
                    else {
                        ntAlert(json.message, function () {
                            row.find('img').attr('src', self.NO_IMAGE);
                            row.find('input[name="Picture.PictureUrl"]').val(self.NO_IMAGE);
                            row.data('deleted', 1);
                        });
                    }
                },
                'json'
                )
        }, function () {

        })
    }

    /*update one row*/
    self.updateRow = function (sender, id) {
        self.currentRow = id;
        var row = $('#nt-picture-row-' + id);
        if (row.data('doSave')) {
            if (!self.validate(row))
                return;
            var data = {};
            data.id = id;
            var a = row.find('input[name="Picture.PictureUrl"]'),
                b = row.find('input[name="Picture.SeoAlt"]'),
                c = row.find('input[name="Picture.Title"]'),
                d = row.find('input[name="Picture.Display"]'),
                e = row.find('input[name="Picture.DisplayOrder"]'),
                f = row.find('input[name="Picture.Text"]');

            data.PictureUrl = a ? a.val() : ''; data.SeoAlt = b ? b.val() : ''; data.Title = c ? c.val() : ''; data.Display = d ? d.val() : ''; data.DisplayOrder = e ? e.val() : ''; data.Text = f ? f.val() : '';
            data.method = 'UPDATE';
            $.post(
                 self.ajaxUrl,
                 data,
                 function (json) {
                     if (json.error)
                         ntAlert(json.message);
                     else {
                         ntAlert(json.message);
                     }
                     row.find('input.admin-input-text').each(function () {
                         $(this).hide().siblings('label.admin-label-text').text(this.value).show();
                     })

                     row.find('input.admin-input-int32').each(function () {
                         $(this).hide().siblings('label.admin-label-int32').text(this.value).show();
                     })

                     row.find('input.admin-input-bool').each(function () {
                         $(this).hide().siblings('label.admin-label-bool').text(this.checked ? '是' : '否').show();
                     })

                     $(sender).siblings('a.admin-ajax-cancel').css('display', 'none');

                     row.data('doSave', 0);
                     $(sender).removeClass('admin-ajax-save');
                     $(sender).attr('title', '修改');
                 },
                 'json'
                )
        } else {
            row.find('label.admin-label-text').hide();
            row.find('label.admin-label-int32').hide();
            row.find('label.admin-label-bool').hide();
            row.find('input.admin-input-text').show();
            row.find('input.admin-input-int32').show();
            row.find('input.admin-input-bool').show();
            $(sender).siblings('a.admin-ajax-cancel').css('display', 'inline-block');
            row.data('doSave', 1);
            $(sender).addClass('admin-ajax-save');
            $(sender).attr('title', '保存');
        }
    }

    /*delete one row  from both server and client*/
    self.deleteRow = function (id) {
        ntConfirm('您确定删除此行?', function () {
            var row = $('#nt-picture-row-' + id);
            var data = {};
            data.id = id;
            data.method = 'DELETE_REC';
            showLoading();
            $.post(
                self.ajaxUrl,
                data,
                function (json) {
                    if (json.error)
                        ntAlert(json.message, function () {
                            removeLoading();
                        });
                    else {
                        row.remove();
                        removeLoading();
                    }
                },
                'json'
                )
        })
    }

    self.cancelUpdate = function () {
        var row = $('#nt-picture-row-' + self.currentRow);
        row.find('label.admin-label-text').show();
        row.find('input.admin-input-text').hide();
        row.find('label.admin-label-int32').show();
        row.find('input.admin-input-int32').hide();
        row.find('label.admin-label-bool').show();
        row.find('input.admin-input-bool').hide();
        row.find('a.admin-ajax-cancel').css('display', 'none');
        row.data('doSave', 0);
        row.find('a.admin-edit').removeClass('admin-ajax-save');
        $(sender).attr('title', '修改');
    }

    window.uploadNew = self.uploadNew;
    window.deleteThis = self.deleteThis;
    window.deleteRow = self.deleteRow;
    window.updateRow = self.updateRow;
    window.addNew = self.addNew;
    window.uploadPicture = self.uploadPicture;
    window.cancelUpdate = self.cancelUpdate;
}

/*
ajaxUrl:an valid url of a handler page
addNewComplete:after insert a new record,you should edit html on page
validate:while update a record
updateDataProvider:posted data
*/
nt.commonAjaxMgr = function (ajaxUrl, addNewComplete, validate, updateConfig, insertConfig) {
    var self = this;
    self.ajaxUrl = ajaxUrl;
    self.currentRow = -100;
    self.addNewComplete = addNewComplete || function () {
        ntAlert('please provide a function after “add a new record” complement!');
    }
    self.validate = validate || function (row) {
        ntAlert('please provide a function to validate !');
        return false;
    }
    self.updateConfig = updateConfig || function (data) {
        ntAlert('please provide a function to update data !');
        return null;
    }
    self.insertConfig = insertConfig || function (data) {
        ntAlert('please provide a function to insert data !');
        return null;
    }

    /*add a new record public*/
    self.addNew = function () {
        showLoading();
        var data = {};
        data.method = 'PUT';
        if (!self.insertConfig(data)) {
            ntAlert('请提供必要的参数!');
            return;
        }
        $.get(
            self.ajaxUrl + '?' + Math.random(),
            data,
            function (json) {
                if (json.error) {
                    ntAlert(json.message);
                    removeLoading();
                }
                else {
                    self.addNewComplete(json);
                    removeLoading();
                }
            },
            'json'
        );
    }

    /*update one row*/
    self.updateRow = function (sender, id) {
        self.currentRow = id;
        var row = $('#nt-common-row-' + id);
        if (row.data('doSave')) {
            if (!self.validate(row))
                return;
            var data = {};
            data.Id = id;
            data.method = 'POST';
            if (!self.updateConfig(data)) {
                ntAlert('没有提供跟新的数据!');
                return;
            }
            $.post(
                 self.ajaxUrl,
                 data,
                 function (json) {
                     if (json.error)
                         ntAlert(json.message);
                     else {
                         ntAlert(json.message);
                     }
                     row.find('input.admin-input-text').each(function () {
                         $(this).hide().siblings('label.admin-label-text').text(this.value).show();
                     })

                     row.find('input.admin-input-int32').each(function () {
                         $(this).hide().siblings('label.admin-label-int32').text(this.value).show();
                     })

                     row.find('input.admin-input-bool').each(function () {
                         $(this).hide().siblings('label.admin-label-bool').text(this.checked ? '是' : '否').show();
                     })
                     $(sender).siblings('a.admin-ajax-cancel').css('display', 'none');
                     row.data('doSave', 0);
                     $(sender).removeClass('admin-ajax-save');
                     $(sender).attr('title', '修改');
                 },
                 'json'
                )
        } else {
            row.find('label.admin-label-text').hide();
            row.find('label.admin-label-int32').hide();
            row.find('label.admin-label-bool').hide();
            row.find('input.admin-input-text').show();
            row.find('input.admin-input-int32').show();
            row.find('input.admin-input-bool').show();
            $(sender).siblings('a.admin-ajax-cancel').css('display', 'inline-block');
            row.data('doSave', 1);
            $(sender).addClass('admin-ajax-save');
            $(sender).attr('title', '保存');
        }
    }

    /*delete one row  from both server and client*/
    self.deleteRow = function (id) {
        ntConfirm('您确定删除此行?', function () {
            var row = $('#nt-common-row-' + id);
            var data = {};
            data.id = id;
            data.method = 'DELETE';
            showLoading();
            $.post(
                self.ajaxUrl,
                data,
                function (json) {
                    if (json.error)
                        ntAlert(json.message, function () {
                            removeLoading();
                        });
                    else {
                        row.remove();
                        removeLoading();
                    }
                },
                'json'
                )
        })
    }

    self.cancelUpdate = function () {
        var row = $('#nt-common-row-' + self.currentRow);
        row.find('label.admin-label-text').show();
        row.find('input.admin-input-text').hide();
        row.find('label.admin-label-int32').show();
        row.find('input.admin-input-int32').hide();
        row.find('label.admin-label-bool').show();
        row.find('input.admin-input-bool').hide();
        row.find('a.admin-ajax-cancel').css('display', 'none');
        row.data('doSave', 0);
        row.find('a.admin-edit').removeClass("admin-ajax-save");
        $(sender).attr('title', '修改');
    }

    window.deleteRow = self.deleteRow;
    window.updateRow = self.updateRow;
    window.addNew = self.addNew;
    window.cancelUpdate = self.cancelUpdate;
}

/*
selectionCancel
*/
nt.selectionCancel = function () {
    $('#admin-selector').remove();
    removeMask();
    nt.useParentContext = 'auto';
};

/*
title:
data:like [{text:'xx',value:78},{text:'vv',value:120},...]
current is a value,for example 78.
ok is a function which will be called after one item selected
*/
function openSelectionWindow(title, data, current, ok) {
    nt.useParentContext = 'no';
    var self = this;
    var html = '<div class="admin-selector" id="admin-selector">';
    html += '<div class="title">' + title + '</div><div class="list">';
    if (typeof (data) == 'string') {
        html += data;
    } else {
        html += '<ul>';
        for (var i in data) {
            if (current == data[i].value)
                html += '<li class="current">';
            else
                html += '<li>';
            html += '<input type="hidden" value="' + data[i].value + '"/>';
            html += data[i].text;
            html += '</li>';
        }
        html += '</ul>';
    }
    html += '</div><div class="foot"><input type="button" class="admin-button" value="关闭" onclick="nt.selectionCancel();"/></div>';
    html += '</div>';
    $(document.body).append(html);
    $('#admin-selector li').click(function () {
        var value = $(this).find('input').val();
        if (current == value)
            return;
        var text = $(this).text();
        if (ok && typeof (ok) == 'function') {
            ok(value, text);
        }
        nt.selectionCancel();
    });
    centerizeElement('#admin-selector');
    showMask();
}

/*复制标题*/
function copyTitle() {
    var title = document.getElementById('Title').value;
    document.getElementById('MetaKeywords').value = title;
    document.getElementById('MetaDescription').value = title;
}