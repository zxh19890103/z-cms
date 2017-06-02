<%@ Control Language="C#" AutoEventWireup="true" Inherits="Nt.Pages.Dialog.BookReplyEdit" %>
<!--Content Edit Area Start-->
<div id="nt-dialog-edit">
    <form action="../Handlers/BookReplyHandler.ashx" method="post" id="NtDialogForm" name="NtDialogForm">
        <table class="admin-table">
            <tr class="table-header">
                <th colspan="2">添加\编辑 留言回复</th>
            </tr>
            <tr>
                <td class="left">回复人:</td>
                <td class="right">
                    <input type="text" name="ReplyMan" maxlength="255" value="" class="input-text" /></td>
            </tr>
            <tr>
                <td class="left">回复内容:</td>
                <td class="right">
                    <textarea cols="1" rows="2" name="ReplyContent"></textarea>
                </td>
            </tr>
            <tr>
                <td class="td-end" colspan="2">
                    <input type="button" class="admin-button" value="保存" onclick="ajaxCommonSave();" />
                    <input type="reset" class="admin-button" value="重置" />
                    <input type="button" class="admin-button" value="取消" onclick="ajaxCommonCancel();" />
                    <input type="hidden" name="Id" value="" />
                    <input type="hidden" name="Book_Id" value="<%=BookID%>" />
                    <input type="hidden" name="method" value="PUT" />
                    <input type="hidden" name="Display" value="True" />
                    <input type="hidden" name="DisplayOrder" value="0" />
                    <input type="hidden" name="ReplyDate" value="<%=DateTime.Now %>" />
                </td>
            </tr>
        </table>
    </form>
</div>
<!--Content Edit Area End-->
<script type="text/javascript">

    var reg4removeHtml = /<[^>].*?>/g;
    var ajaxUrl = '../Handlers/BookReplyHandler.ashx';
    
    /*赋值*/
    function setData(json) {
        $('#NtDialogForm').find('input[name="ReplyMan"]').val(json.model.ReplyMan);
        document.NtDialogForm.ReplyContent.value = json.model.ReplyContent;
        $('#NtDialogForm').find('input[name="Display"]').val(json.model.Display);
        $('#NtDialogForm').find('input[name="DisplayOrder"]').val(json.model.DisplayOrder);
        $('#NtDialogForm').find('input[name="Book_Id"]').val(json.model.Book_Id);
    }

    /*表单验证*/
    function validateForm() {
        if (document.NtDialogForm.ReplyContent.value == '') {
            ntAlert('内容不能为空', function () {
                document.NtDialogForm.ReplyContent.focus();
            });
            return false;
        }
        return true;
    }

    /*预处理*/
    function beforeSerialize() {
        nbody = document.NtDialogForm.ReplyContent.value;
        document.NtDialogForm.ReplyContent.value = nbody.replace(reg4removeHtml, ' ').substr(0, 1024);
    }

    var dialog = new nt.ntDialog(ajaxUrl, setData, validateForm, beforeSerialize, 'NtDialogForm');

</script>
