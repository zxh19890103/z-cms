<%@ Page Language="C#" MasterPageFile="~/Netin/Layout.master"
    AutoEventWireup="true" Inherits="Nt.Pages.Course.CopyCategory" %>

<asp:Content runat="server" ContentPlaceHolderID="CPH_Head" ID="Content1">
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="CPH_Body" ID="Content2">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                Copy Course Category Data In Current Language 
                Version To Specified Language Versions
                &nbsp;
                <a href="Category.aspx">返回</a>
            </div>
            <form method="post" id="EditForm" name="EditForm" action="<%=LocalUrl %>">
                <table class="admin-table">
                    <tr>
                        <td class="td-end">
                            <%
                                foreach (System.Data.DataRow r in AvailableLanguages.Rows)
                                {
                                    if (r["Id"].ToString() == LanguageID.ToString())
                                        continue;
                                    Response.Write(string.Format(
                                        "<input type=\"checkbox\" name=\"{0}\" value=\"{1}\" id=\"lang-{1}\"/><label for=\"lang-{1}\">{2}</label><br/><br/>",
                                        ConstStrings.COPY_TREE_TARGET_LANGUAGES, r["id"], r["Name"]));
                                }
                            %>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-end">
                            <input type="button" class="admin-button" value="复制" onclick="EditForm.submit();" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>
