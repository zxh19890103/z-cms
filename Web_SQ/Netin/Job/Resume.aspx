<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout2.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Job.Resumes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        function viewResumeDetail(id) {
            openWindow('ResumeDetail.aspx?Id=' + id, '查看简历');
        }

        function openResumeStatusSelection(sender,current,id)
        {
            if($(sender).data('current'))
                current=$(sender).data('current');
            var data=<%=NtUtility.GetJsObjectArrayFromList(Service.ResumeStatusProvider)%>;
            openSelectionWindow('简历状态设置',
                data
                ,current,function(v,t){
                    if(v!=current){
                        ntAjax({
                            method:'SetEnumValue',
                            data:'{"tab":"Nt_Resume","field":"Status","id":"'+id+'","value":"'+v+'"}',
                            success:function(msg){
                                var json=$.parseJSON(msg.d);
                                ntAlert(json.message);
                                $(sender).text(t);
                                $(sender).data('current',v);
                            }
                        })
                    }
                })
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                简历查看
            </div>
            <table class="admin-table">
                <tr class="admin-table-header">
                    <th class="td-select">选择</th>
                    <th>姓名</th>
                    <th>性别</th>
                    <th>电话</th>
                    <th>学历</th>
                    <th>简历状态</th>
                    <th class="td-published-date">投递日期</th>
                    <th class="th-end td-edit-del">操作</th>
                </tr>
                <asp:Repeater ID="XRepeater" runat="server">
                    <ItemTemplate>
                        <tr class="tr-even">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" /></td>
                            <td><%#Eval("Name") %></td>
                            <td><%#HtmlHelper.BoolLabel("男", "女", Eval("Gender"), new {})%></td>
                            <td><%#Eval("Phone") %></td>
                            <td><%#Service.GetEduDegreeName(Eval("EduDegree")) %></td>
                            <td><a href="javascript:;" onclick="openResumeStatusSelection(this,<%#Eval("Status") %>,<%#Eval("ID") %>);">
                                <%#Service.GetStatusName(Eval("Status")) %></a></td>
                            <td><%#Eval("AddDate","{0:yyyy-MM-dd}") %></td>
                            <td class="td-end">
                                <a href="javascript:;" onclick="viewResumeDetail(<%#Eval("Id") %>)" class="admin-view-detail" title="查看"></a>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>

                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="tr-odd">
                            <td>
                                <input class="ck-item" type="checkbox" value="<%#Eval("Id") %>" /></td>
                            <td><%#Eval("Name") %></td>
                            <td><%#HtmlHelper.BoolLabel("男", "女", Eval("Gender"), new {})%></td>
                            <td><%#Eval("Phone") %></td>
                            <td><%#Service.GetEduDegreeName(Eval("EduDegree"))  %></td>
                            <td><a href="javascript:;" onclick="openResumeStatusSelection(this,<%#Eval("Status") %>,<%#Eval("ID") %>);">
                                <%#Service.GetStatusName(Eval("Status")) %></a></td>
                            <td><%#Eval("AddDate","{0:yyyy-MM-dd}") %></td>
                            <td class="td-end">
                                <a href="javascript:;" onclick="viewResumeDetail(<%#Eval("Id") %>)" class="admin-view-detail" title="查看"></a>
                                <a href="javascript:;" onclick="del(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        <tr class="admin-table-footer">
                            <td>
                                <input type="checkbox" onclick="selectall(this)" /></td>
                            <td colspan="6"></td>
                            <td class="td-end">
                                <input type="button" class="admin-button" value="批量删除" onclick="delSelected()" /></td>
                        </tr>
                        <tr class="admin-table-footer">
                            <td colspan="8" style="text-align: center;" class="td-end">
                                <input type="button" class="admin-button" value="关闭窗口" onclick="window.close();" />
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>

            </table>
        </div>
    </div>
</asp:Content>

