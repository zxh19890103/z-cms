<%@ Control Language="C#" AutoEventWireup="true" Inherits="Nt.Framework.NtUserControl" %>
<!--Content Edit Area Start-->
<div id="nt-dialog-edit">
    <form action="../Handlers/Common/SearchKeywordHandler.ashx" method="post" id="NtDialogForm" name="NtDialogForm">
        <table class="admin-table">
            <tr class="table-header">
                <th colspan="2">添加\编辑 搜索关键词</th>
            </tr>
            <tr>
                <td class="left">关键词:</td>
                <td class="right">
                    <input type="text" name="KeyWord" value="" maxlength="255" class="input-text" /></td>
            </tr>
            <tr>
                <td class="left">备注：</td>
                <td class="right">
                    <textarea cols="1" rows="2" name="Note"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="td-end">
                    <input type="button" class="admin-button" value="保存" onclick="ajaxCommonSave();" />
                    <input type="reset" class="admin-button" value="重置" />
                    <input type="button" class="admin-button" value="取消" onclick="ajaxCommonCancel();" />
                    <input type="hidden" name="Id" value="" />
                    <input type="hidden" name="Language_Id" value="<%=WorkingLang.Id %>" />
                    <input type="hidden" name="method" value="PUT" />
                    <input type="hidden" name="Display" value="True" />
                    <input type="hidden" name="DisplayOrder" value="0" />
                </td>
            </tr>
        </table>
    </form>
</div>
<!--Keyword Edit Area End-->
<script type="text/javascript">

    var reg4removeHtml = /<[^>].*?>/g;
    var ajaxUrl = '../Handlers/Common/SearchKeywordHandler.ashx';

    /*赋值*/
    function setData(json) {
        $('#NtDialogForm').find('input[name="KeyWord"]').val(json.model.KeyWord);
        document.NtDialogForm.Note.value = json.model.Note;
        $('#NtDialogForm').find('input[name="Display"]').val(json.model.Display);
        $('#NtDialogForm').find('input[name="DisplayOrder"]').val(json.model.DisplayOrder);
    }

    /*表单验证*/
    function validateForm() {
        if (document.NtDialogForm.KeyWord.value == '') {
            ntAlert('关键词不能为空', function () {
                document.NtDialogForm.KeyWord.focus();
            });
            return false;
        }
        return true;
    }

    /*预处理*/
    function beforeSerialize() {
        var nbody = document.NtDialogForm.Note.value;
        document.NtDialogForm.Note.value = nbody.replace(reg4removeHtml, ' ').substr(0, 1024);
    }

    var dialog = new nt.ntDialog(ajaxUrl, setData, validateForm, beforeSerialize, 'NtDialogForm');
</script>
