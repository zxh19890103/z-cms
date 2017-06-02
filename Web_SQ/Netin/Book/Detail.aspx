<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Book.Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        function saveBookNote(id){
            saveNote(id,
                document.getElementById('BookNote').value
                ,'Nt_Book');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <!--预定查看-->
            <div class="admin-main-title">
                预定查看
            </div>
            <table class="admin-table">
                <tr>
                    <td class="left-two">预定类别：</td>
                    <td class="right-two"><%=GetCatalogName(Model.Type) %></td>
                    <td class="left-two">标题：</td>
                    <td class="right-two td-end"><%=Model.Title %></td>
                </tr>
                <tr>
                    <td class="left-two">姓名：</td>
                    <td class="right-two"><%=Model.Name %></td>
                    <td class="left-two">联系方式：</td>
                    <td class="right-two td-end">Tel:<%=Model.Tel %><br />
                        Cell:<%=Model.Mobile %><br />
                        Email:<%=Model.Email %>
                    </td>
                </tr>
                <tr>
                    <td class="left-two">性别：</td>
                    <td class="right-two"><%=Model.Gender?"男":"女"%></td>
                    <td class="left-two">籍贯：</td>
                    <td class="right-two td-end"><%=Model.NativePlace %></td>
                </tr>
                <tr>
                    <td class="left-two">民族：</td>
                    <td class="right-two"><%=Model.Nation %></td>
                    <td class="left-two">身份证号：</td>
                    <td class="right-two td-end"><%=Model.PersonID %></td>
                </tr>
                <tr>
                    <td class="left-two">学历：</td>
                    <td class="right-two"><%=Model.EduDegree %></td>
                    <td class="left-two">出生日期：</td>
                    <td class="right-two td-end"><%=Model.BirthDate.ToString("yyyy-MM-dd") %></td>
                </tr>
                <tr>
                    <td class="left-two">所在地邮编：</td>
                    <td class="right-two"><%=Model.ZipCode %></td>
                    <td class="left-two">政治身份：</td>
                    <td class="right-two td-end"><%=Model.PoliticalRole %></td>
                </tr>
                <tr>
                    <td class="left-two">住址：</td>
                    <td class="right-two"><%=Model.Address %></td>
                    <td class="left-two">毕业学校：</td>
                    <td class="right-two td-end"><%=Model.GraduatedFrom %></td>
                </tr>
                <tr>
                    <td class="left-two">在校成绩：</td>
                    <td class="right-two"><%=Model.Grade %></td>
                    <td class="left-two">公司名称：</td>
                    <td class="right-two td-end"><%=Model.Company %></td>
                </tr>
                <tr>
                    <td class="left">预订详情：</td>
                    <td class="right" colspan="3"><%=Model.Body %></td>
                </tr>
                <tr>
                    <td class="left-two">添加时间：</td>
                    <td class="right-two"><%=Model.AddDate.ToString("yyyy-MM-dd") %></td>
                    <td class="left-two">备注：</td>
                    <td class="right-two td-end">
                        <textarea cols="1" rows="2" id="BookNote"><%=Model.Note%></textarea>
                        <br />
                        <br />
                        <input type="button" class="admin-button" value="保存" onclick="saveBookNote(<%=Model.Id%>);" />
                        <input type="button" class="admin-button" value="返回" onclick="window.location.href='List.aspx';" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

