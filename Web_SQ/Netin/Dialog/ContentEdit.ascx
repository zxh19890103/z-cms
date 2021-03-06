﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="Nt.Framework.NtUserControl" %>
<!--Content Edit Area Start-->
<div id="nt-dialog-edit">
    <form action="../Handlers/Common/ContentHandler.ashx" method="post" id="NtDialogForm" name="NtDialogForm">
        <table class="admin-table">
            <tr class="table-header">
                <th colspan="2">添加\编辑 内容</th>
            </tr>
            <tr>
                <td class="left">标题:</td>
                <td class="right">
                    <input type="text" name="Title" value="" maxlength="512" class="input-text" /></td>
            </tr>
            <tr>
                <td class="left">文本:</td>
                <td class="right">
                    <textarea cols="1" rows="2" name="Text" title="请勿超过1024个字符"></textarea>
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
                    <input type="hidden" name="Language_Id" value="<%=WorkingLang.Id %>" />
                    <input type="hidden" name="method" value="PUT" />
                    <input type="hidden" name="Display" value="True" />
                </td>
            </tr>
        </table>
    </form>
</div>
<!--Content Edit Area End-->
<script type="text/javascript">
    var reg4removeHtml = /<[^>].*?>/g;
    var ajaxUrl = '../Handlers/Common/ContentHandler.ashx';

    /*赋值*/
    function setData(json) {
        $('#NtDialogForm').find('input[name="Title"]').val(json.model.Title);
        document.NtDialogForm.Text.value = json.model.Text;
        document.NtDialogForm.Note.value = json.model.Note;
        $('#NtDialogForm').find('input[name="Display"]').val(json.model.Display);
    }

    /*表单验证*/
    function validateForm() {
        if (document.NtDialogForm.Title.value == '') {
            ntAlert('标题不能为空', function () {
                document.NtDialogForm.Title.focus();
            });
            return false;
        }

        if (document.NtDialogForm.Text.value == '') {
            ntAlert('内容不能为空', function () {
                document.NtDialogForm.Text.focus();
            });
            return false;
        }
        return true;
    }

    /*预处理*/
    function beforeSerialize() {
        var nbody = document.NtDialogForm.Text.value;
        document.NtDialogForm.Text.value = nbody.replace(reg4removeHtml, ' ').substr(0, 1024);
        nbody = document.NtDialogForm.Note.value;
        document.NtDialogForm.Note.value = nbody.replace(reg4removeHtml, ' ').substr(0, 1024);
    }

    var dialog = new nt.ntDialog(ajaxUrl, setData, validateForm, beforeSerialize, 'NtDialogForm');

</script>
