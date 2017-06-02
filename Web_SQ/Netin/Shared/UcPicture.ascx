<%@ Control Language="C#" AutoEventWireup="true" Inherits="Nt.Pages.Shared.UcPicture" %>
<div id="nt-picture-data">
    <span class="admin-tips">请按指定尺寸比例上传图片，否则图片将无法正确显示
    </span>
    <br />
    <br />
    <img height="150" src="<%=PictureUrl %>" alt="" />
    <br />
    <a href="javascript:;" onclick="openUploadDialog(uploadPicture,'上传图片')" class="admin-ajax-upload" title="上传"></a>
    <a href="javascript:;" onclick="removeData()" class="admin-delete" title="清除"></a>
    <input type="hidden" name="Picture_Id" id="Picture_Id" value="<%=Picture_Id %>" />
    <input type="hidden" name="PictureUrl" id="PictureUrl" value="<%=PictureUrl %>" />
</div>
<script type="text/javascript">
    /*执行上传*/
    function uploadPicture() {
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
        data.Picture_Id = $('#nt-picture-data').find('#Picture_Id').val();
        data.method = 'upload';
        $(thisForm).ajaxSubmit({
            url: '/Netin/Handlers/Picture/UploadHandler.ashx',
            data: data,
            forceSync: true,
            dataType: 'json',
            beforeSubmit: function () {
                closeUploadDialog();
                showLoading();
            },
            success: function (json, statusText) {
                if (json.error)
                    ntAlert(json.message, function () { removeLoading(); });
                else {
                    $('#nt-picture-data').data('deleted', 0);
                    $('#nt-picture-data').find('img').attr('src', json.PictureUrl);
                    $('#nt-picture-data').find('input[name="Picture_Id"]').val(json.Picture_Id);
                    $('#nt-picture-data').find('input[name="PictureUrl"]').val(json.PictureUrl);
                    removeLoading();
                }
            },
            error: function () { ntAlert('操作错误!'); removeLoading(); }
        });
        return false;
    }

    /*删除已经上传的图片*/
    function removeData() {
        if ($('#nt-picture-data').data('deleted')) {
            ntAlert('图片已被删除!');
            return false;
        }
        ntConfirm('您是否要删除此图片?',
            function () {
                var data = {};
                data.Picture_Id = $('#nt-picture-data').find('#Picture_Id').val();
                data.method = 'del';
                $.post(
                    '/Netin/Handlers/Picture/UploadHandler.ashx',
                    data,
                    function (json) {
                        if (json.error)
                            ntAlert(json.message, function () {

                            });
                        else {
                            ntAlert("清除成功!", function () {
                                $('#nt-picture-data').data('deleted', 1);
                                $('#nt-picture-data').find('img').attr('src', '/Upload/Product-Pictures/no-image.gif');
                                $('#nt-picture-data').find('input[name="Picture_Id"]').val(0);
                            });
                        }
                    }
                    )
            }, function () {

            })
    }
</script>
