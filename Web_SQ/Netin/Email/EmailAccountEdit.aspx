<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
Inherits="Nt.Pages.Email.EmailAccountEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        /*更改邮箱账户密码*/
        function changeEmailAccountPassword(id){
            showLoading();
            ntAjax({
                method:"ChangeEmailAccountPassword",
                data:'{"id":"'+id+'","password":"'+$('input[name="Password"]').val()+'"}',
                success:function(msg){
                    var json=$.parseJSON(msg.d);
                    ntAlert(json.message,function(){
                        removeLoading();
                    });
                }
            }
                )
        }

        /*发送邮件测试*/
        function trySendMail(id){
            showLoading();
            ntAjax({
                method:'TrySendMail',
                data:'{"accountId":"'+id+'","mailAddress":"'+$('.mail-address input[name="MailAddress"]').val()+'"}',
                success:function(msg) {
                    var json=$.parseJSON(msg.d);
                    ntAlert(json.message,function(){
                        removeLoading();
                    });
                    
                }
            })
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <%=EditTitlePrefix %>邮箱账号
            </div>
            <form action="<%=LocalUrl %>" method="post" id="EditForm" name="EditForm" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">
                    <tr>
                        <td class="left">邮箱地址：</td>
                        <td class="right">
                            <input class="input-text" name="Email" maxlength="225" value="<%=Model.Email %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">显示名称：</td>
                        <td class="right">
                            <input class="input-text" name="DisplayName" maxlength="225" value="<%=Model.DisplayName %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">主机：</td>
                        <td class="right">
                            <input class="input-text" name="Host" maxlength="225" value="<%=Model.Host %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">端口：</td>
                        <td class="right">
                            <input class="input-int32" maxlength="5"  name="Port" maxlength="225" value="<%=Model.Port %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">用户名：</td>
                        <td class="right">
                            <input class="input-text" name="UserName" maxlength="225" value="<%=Model.UserName %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">密码：</td>
                        <td class="right">
                            <input class="input-text" name="Password" type="password" maxlength="225" value="<%=Model.Password %>" />
                            <%if (EnsureEdit)
                              {%>
                            <input class="admin-button" type="button" value="更改密码" onclick="changeEmailAccountPassword(<%=NtID%>)" />
                            <%} %>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">使用SSL：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox("", Model.EnableSsl, "EnableSsl",new {_class="admin-checkbox" })%>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">使用默认凭证：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox("", Model.UseDefaultCredentials, "UseDefaultCredentials",new { _class="admin-checkbox"})%>
                        </td>
                    </tr>

                    <tr style="height: 60px;">
                        <td align="center" colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                            <input type="hidden" name="IsDefault" value="<%=Model.IsDefault %>" />
                        </td>
                    </tr>

                    <%if (EnsureEdit)
                      { %>
                    <tr>
                        <td align="left" colspan="2" class="td-end">
                            <div class="try-send-mail">
                                <div class="title">发送邮件测试</div>
                                <div class="mail-address">
                                    发送至:<input type="text" name="MailAddress" class="input-text" value="" />
                                    <br />
                                    <br />
                                    <input type="button" class="admin-button" value="发送" onclick="trySendMail(<%=NtID%>);" />
                                </div>
                            </div>
                        </td>
                    </tr>
                    <%} %>
                </table>
            </form>
        </div>
    </div>

</asp:Content>

