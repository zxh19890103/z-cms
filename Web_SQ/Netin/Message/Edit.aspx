<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Message.Edit" %>

<%@ Register Src="~/Netin/Shared/MemberInfo.ascx" TagPrefix="uc1" TagName="MemberInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        function saveMessageNote(id){
            saveNote(id,
               document.getElementById('MessageNote').value
               ,'Nt_Message');
        }

        function doReply(id){
            openWindow('MessageReply.aspx?Message_Id='+id,'');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <%
        MemberInfo.MemberID = Model.Member_Id;
    %>
    <div class="admin-main-wrap">
        <div class="admin-main">
            <!--留言查看并回复-->
            <div class="admin-main-title">
                留言查看并回复
                &nbsp;
                &nbsp;
                <a href="List.aspx">返回留言列表</a>
            </div>
            <table class="admin-table">
                <tr>
                    <td class="left">标题：</td>
                    <td class="right"><%=Model.Title %></td>
                </tr>
                <tr>
                    <td class="left">联系人：</td>
                    <td class="right"><%=Model.LinkMan %></td>
                </tr>
                <tr>
                    <td class="left">留言状态：</td>
                    <td class="right" id="messStatus"><%=Service.GetStatusName(Model.Status) %></td>
                </tr>
                <tr>
                    <td class="left">留言日期：</td>
                    <td class="right"><%=Model.AddDate.ToString("yyyy-MM-dd") %></td>
                </tr>
                <tr>
                    <td class="left">留言内容：</td>
                    <td class="right"><%=Model.Body %></td>
                </tr>
                <tr>
                    <td class="left">留言类别：</td>
                    <td class="right"><%=GetCatalogName(Model.Type) %></td>
                </tr>
                <tr>
                    <td class="left">备注：</td>
                    <td class="right">
                        <textarea cols="1" rows="2" id="MessageNote"><%=Model.Note%></textarea>
                        <br />
                        <input type="button" class="admin-button" value="保存" onclick="saveMessageNote(<%=Model.Id%>);" />
                        <!--回复-->
                        &nbsp;&nbsp;
                        <a href="javascript:;" onclick="doReply(<%=Model.Id %>)">留言回复</a>
                    </td>
                </tr>
            </table>
        </div>
        <uc1:MemberInfo runat="server" ID="MemberInfo" />
    </div>
</asp:Content>

