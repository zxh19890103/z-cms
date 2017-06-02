<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Common.WebsiteSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                网站基本设置
            </div>
            <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl %>" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">
                    <tr>
                        <td class="left">网站名称：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="WebsiteName" value="<%=Model.WebsiteName%>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">网站关键词：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="Keywords"><%=Model.Keywords%></textarea>
                            <span class="admin-tips">请勿超过1024个字符</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">网站描述：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="Description"><%=Model.Description %></textarea>
                            <span class="admin-tips">请勿超过1024个字符</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">公司名称：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="CompanyName" value="<%=Model.CompanyName%>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">公司地点：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="CompanyAddress" value="<%=Model.CompanyAddress%>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">网站地址：</td>
                        <td class="right">
                            <input type="text" class="input-text" id="WebsiteUrl" name="WebsiteUrl" value="<%=Model.WebsiteUrl%>" />
                            <span class="admin-tips">请输入完整的网站地址,如
                                <a href="javascript:;" title="复制默认网址" onclick="copyLocalUrl();">http://www.naite.com.cn</a></span>
                            <script type="text/javascript">
                                function copyLocalUrl() {
                                    var url = '<%=WebHelper.CurrentRootUrl%>';
                                    document.getElementById('WebsiteUrl').value = url;
                                }
                            </script>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">Email：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Email" value="<%=Model.Email%>" />
                            <span class="admin-tips">多个Email请用英文逗号隔开</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">座机：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Phone" value="<%=Model.Phone%>" />
                            <span class="admin-tips">多个号码请用英文逗号隔开</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">手机：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Mobile" value="<%=Model.Mobile%>" />
                            <span class="admin-tips">多个号码请用英文逗号隔开</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">QQ：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="QQ" value="<%=Model.QQ%>" />
                            <span class="admin-tips">多个QQ号码请用英文逗号隔开</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">传真：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Fax" value="<%=Model.Fax%>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">联系人：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Linkman" value="<%=Model.Linkman%>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">邮编：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="ZipCode" value="<%=Model.ZipCode%>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">ICP：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="ICP" value="<%=Model.ICP%>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">开启静态化：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(Model.SetHtmlOn, "SetHtmlOn", new { })%>
                            <span class="admin-tips">开启静态化功能之后，请务必到网站优化栏目里定期进行相应的静态生成操作</span>
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

