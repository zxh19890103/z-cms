<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
Inherits="Nt.Pages.Job.List" %>

<%@ Register Src="~/Netin/Shared/UcNtPager.ascx" TagPrefix="uc1" TagName="UcNtPager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        function viewResume(id){
            openWindow('Resume.aspx?Job_Id='+id,'简历查看');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                职位列表
                &nbsp;<a href="Edit.aspx"  title="添加">添加</a>
            </div>
            <table class="admin-table">
                <asp:Repeater ID="XRepeater" runat="server">
                    <HeaderTemplate>
                        <tr class="admin-table-header">
                            <th class="td-select">选择</th>
                            <th class="td-order">排序</th>
                            <th class="td-display">是否显示</th>
                            <th>职位名</th>
                            <th class="td-published-date">发布日期</th>
                            <th class="td-published-date">开始日期</th>
                            <th class="td-published-date">结束日期</th>
                            <th class="td-edit-del th-end">操作</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" />
                            </td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>"><%#Eval("JobName") %></a>
                            </td>
                            <td>
                                <%#Eval("AddDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td>
                                <%#Eval("StartDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td>
                                <%#Eval("EndDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td class="td-end">
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <a href="javascript:;" onclick="viewResume(<%#Eval("Id") %>)" class="admin-view-resume" title="查看简历"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" />
                            </td>
                            <td>
                                <input type="text" name="order-item" maxlength="5" itemid="<%#Eval("Id") %>" class="input-int32 input-state-tracking" value="<%#Eval("DisplayOrder") %>" />
                            </td>
                            <td>
                                <%#HtmlHelper.BoolLabel("显示", "隐藏", Eval("Display"), new { itemid = Eval("Id"), onclick="display(this)",_class="lbl-ajax"})%>
                            </td>
                            <td>
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>"><%#Eval("JobName") %></a>
                            </td>
                            <td>
                                <%#Eval("AddDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td>
                                <%#Eval("StartDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td>
                                <%#Eval("EndDate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td class="td-end">
                                <a href="Edit.aspx?Id=<%#Eval("Id") %>" class="admin-edit" title="修改"></a>
                                 <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                <a href="javascript:;" onclick="viewResume(<%#Eval("Id") %>)" class="admin-view-resume" title="查看简历"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        <tr class="admin-table-footer">
                            <td class="td-select">
                                <input type="checkbox" onclick="selectall(this)" />
                            </td>
                            <td class="td-order">
                                <input type="button" class="admin-button" value="更新排序" onclick="reOrder()" />
                            </td>
                            <td colspan="5">
                                <uc1:UcNtPager runat="server" ID="UcNtPager" />
                            </td>
                            <td class="td-edit-del td-end">
                                <input type="button" class="admin-button" value="批量删除" onclick="delSelected()" />
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

