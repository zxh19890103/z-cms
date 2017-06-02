<%@ Control Language="C#" AutoEventWireup="true" Inherits="Nt.Framework.NtUserControl" %>
<!--Js Edit Area Start-->
<div id="nt-dialog-edit">
    <form action="../Handlers/Common/JsHandler.ashx" method="post" id="NtDialogForm" name="NtDialogForm">
        <table class="admin-table">
            <tr class="table-header">
                <th colspan="2">添加\编辑 JS脚本</th>
            </tr>
            <tr>
                <td class="left">脚本:</td>
                <td class="right">
                    <textarea cols="1" rows="2" name="Script"></textarea>
                </td>
            </tr>
            <tr>
                <td class="left">备注：</td>
                <td class="right">
                    <textarea cols="1" rows="2" name="Note" title="请勿超过1024个字符"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="td-end">
                    <input type="button" class="admin-button" value="保存" onclick="ajaxCommonSave();" />
                    <input type="reset" class="admin-button" value="重置" />
                    <input type="button" class="admin-button" value="取消" onclick="ajaxCommonCancel();" />
                    <input type="hidden" name="Id" value="" />
                    <input type="hidden" name="method" value="PUT" />
                    <input type="hidden" name="Display" value="True" />
                    <input type="hidden" name="DisplayOrder" value="0" />
                </td>
            </tr>
        </table>
    </form>
</div>
<!--Js Edit Area End-->

<script type="text/javascript">
    var reg4removeHtml = /<[^>].*?>/g;
    var ajaxUrl = '../Handlers/Common/JsHandler.ashx';
    
    /*赋值*/
    function setData(json) {
        document.NtDialogForm.Script.value = json.model.Script;
        document.NtDialogForm.Note.value = json.model.Note;
        $('#NtDialogForm').find('input[name="Display"]').val(json.model.Display);
        $('#NtDialogForm').find('input[name="DisplayOrder"]').val(json.model.DisplayOrder);
    }

    /*表单验证*/
    function validateForm() {
        if (document.NtDialogForm.Script.value == '') {
            ntAlert('脚本不能为空', function () {
                document.NtDialogForm.Script.focus();
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
