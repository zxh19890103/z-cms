<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.News.CategoryEdit" %>

<%@ Register Src="~/Netin/Shared/UcPicture.ascx" TagPrefix="uc1" TagName="UcPicture" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">

    <link rel="stylesheet" href="../Editor/themes/default/default.css" />
    <link rel="stylesheet" href="../Editor/plugins/code/prettify.css" />
    <script charset="utf-8" src="../Editor/kindeditor-min.js" type="text/javascript"></script>
    <script charset="utf-8" src="../Editor/lang/zh_CN.js" type="text/javascript"></script>
    <script charset="utf-8" src="../Editor/plugins/code/prettify.js" type="text/javascript"></script>


    <script type="text/javascript">
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('textarea[name="Description"]', {
                cssPath: '../Editor/plugins/code/prettify.css',
                uploadJson: "../Editor/asp.net/upload_json.ashx",
                fileManagerJson: "../Editor/asp.net/file_manager_json.ashx",
                allowFileManager: false,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name="EditForm"]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name="EditForm"]')[0].submit();
                    });
                }
            });
            prettyPrint();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <%=EditTitlePrefix %>新闻类别
            </div>
            <form action="<%=LocalUrl %>" method="post" id="EditForm" name="EditForm" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">

                    <tr>
                        <td class="left">父类别：</td>
                        <td class="right">
                            <label><%=ParentName %></label>
                            <input name="Parent" type="hidden" class="input-text" value="<%=ParentID %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">类别名称：</td>
                        <td class="right">
                            <input type="text" class="input-text no-comma" name="Name" maxlength="255" value="<%=Model.Name %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">是否显示：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(EnsureEdit?Model.Display:true, "Display", new{})%>
                        </td>
                    </tr>


                    <tr>
                        <td class="left">排序：</td>
                        <td class="right">
                            <input type="text" class="input-int32" maxlength="5" name="DisplayOrder" value="<%=Model.DisplayOrder==0?MaxID:Model.DisplayOrder %>" />
                        </td>
                    </tr>

                    <!--begin 添加字段-->

                    <tr>
                        <td class="left">标题：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Title" maxlength="1024" value="<%=Model.Title %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">英文标题：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="EnTitle" maxlength="1024" value="<%=Model.EnTitle %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">图片：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="PictureUrl" maxlength="256" value="<%=Model.PictureUrl %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">链接：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Url" maxlength="256" value="<%=Model.Url %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">描述：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" style="width: 800px; height: 250px; visibility: hidden;" name="Description"><%=Model.Description %></textarea>
                        </td>
                    </tr>

                    <!--end 添加字段-->


                    <tr style="height: 60px;">
                        <td align="center" colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=Model.Id %>" />
                            <input type="hidden" name="ClickRate" value="<%=Model.ClickRate %>" />
                            <input type="hidden" name="Language_Id" value="<%=WorkingLang.Id %>" />
                            <input type="hidden" name="Crumbs" value="0" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

