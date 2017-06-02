<%@ Control Language="C#" AutoEventWireup="true" Inherits="Nt.Pages.Shared.UcDownload" %>
<div id="nt-download-data">
    路径:<input type="text" readonly="readonly" class="input-text" name="FileUrl" id="FileUrl" value="<%=FileUrl %>" />
    <br />
    <br />
    文件大小(单位:Byte):<input type="text" readonly="readonly" class="input-int32" maxlength="5" name="FileSize" id="FileSize" value="<%=FileSize %>" />
    <br />
    <a href="javascript:;" onclick="openUploadDialog(uploadFile,'上传附件')" class="admin-ajax-upload" title="上传附件"></a>
    <a href="javascript:;" onclick="removeData()" class="admin-delete" title="清除"></a>
</div>
<script type="text/javascript">
    /*执行上传*/
    function uploadFile() {
        var thisForm = null;
        if (useParentContextOrNot())
            thisForm = parent.document.getElementById('nt-upload-form');
        else
            thisForm = document.getElementById('nt-upload-form');
        if (thisForm.ntImgFile.value == '') {
            ntAlert('请选择您要上传的文件');
            return false;
        }

        var data = {};
        data.fileUrl = $('#nt-download-data').find('#FileUrl').val();
        data.method = 'upload';
        $(thisForm).ajaxSubmit({
            url: '/Netin/Handlers/Download/DownloadHandler.ashx',
            data: data,
            forceSync: true,
            dataType: 'json',
            beforeSubmit: function () {
                showLoading();
                closeUploadDialog();
            },
            success: function (json, statusText) {
                if (json.error)
                    ntAlert(json.message, function () {
                        removeLoading();
                    });
                else {
                    $('#nt-download-data').data('deleted', 0);
                    $('#nt-download-data').find('input[name="FileUrl"]').val(json.FileUrl);
                    $('#nt-download-data').find('input[name="FileSize"]').val(json.FileSize);
                    removeLoading();
                }
            },
            error: function () { ntAlert('操作错误!'); removeLoading(); }
        });
        return false;
    }

    /*删除已经上传的图片*/
    function removeData() {
        if ($('#nt-download-data').data('deleted')) {
            ntAlert('附件已被删除!');
            return false;
        }
        ntConfirm('您是否要删除此附件?',
            function () {
                var data = {};
                data.fileUrl = $('#nt-download-data').find('#FileUrl').val();
                if (data.fileUrl == '') {
                    ntAlert('附件已经被删除');
                    return false;
                }
                data.method = 'del';
                $.post(
                    '/Netin/Handlers/Download/DownloadHandler.ashx',
                    data,
                    function (json) {
                        if (json.error)
                            ntAlert(json.message, function () {

                            });
                        else {
                            ntAlert("清除成功!", function () {
                                $('#nt-download-data').data('deleted', 1);
                                $('#nt-download-data').find('#FileUrl').val('');
                                $('#nt-download-data').find('#FileSize').val(0);
                            });
                        }
                    }
                    )
            }, function () {

            })
    }
</script>
