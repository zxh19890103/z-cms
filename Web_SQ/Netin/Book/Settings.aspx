<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
Inherits="Nt.Pages.Book.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" Runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                预订/留言设置
            </div>
            <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl %>" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">
                    <tr>
                        <td class="left">开启审核：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.EnableVerification, "EnableVerification", new { })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">使用验证码：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.EnableCheckCode, "EnableCheckCode", new { })%>
                        </td>
                    </tr>
                      <tr>
                        <td class="left">是否启用浮动咨询：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.EnableFloatQueryBox, "EnableFloatQueryBox", new { })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">是否发送到邮箱：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.EnableSendEmail, "EnableSendEmail", new { })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">接收邮箱地址：</td>
                        <td class="right">
                           <input type="text"  class="input-text" name="EmailAddressToReceiveEmail" value="<%=Model.EmailAddressToReceiveEmail %>"/>
                        </td>
                    </tr>
                     <tr>
                        <td class="left">屏蔽网址：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.FiltrateUrl, "FiltrateUrl", new { })%>
                        </td>
                    </tr>
                     <tr>
                        <td class="left">屏蔽敏感词：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.FiltrateSensitiveWords, "FiltrateSensitiveWords", new { })%>
                        </td>
                    </tr>
                     <tr>
                        <td class="left">自定义敏感词：</td>
                        <td class="right">
                            <div>如果留言中有如下词语，则视为无效留言 ，每个过滤字符用回车分割开。</div>
                            <textarea cols="2" rows="3" name="SensitiveWords"><%=Model.SensitiveWords %></textarea>
                        </td>
                    </tr>
                    <tr style="height: 60px;">
                        <td colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="reset" class="admin-button" value="重置" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

