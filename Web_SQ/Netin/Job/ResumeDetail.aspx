<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout2.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Job.ResumeDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        function saveResumeNote(id){
            saveNote(id,document.getElementById('ResumeNote').value,'Nt_Resume');
        }

        function saveResumeReply(id){
            ntAjax({
                method: "SaveResumeReply",
                data: '{"id":"' + id + '","reply":"' + document.getElementById('ResumeReply').value + '"}',
                success: function (msg) {
                    var json = $.parseJSON(msg.d);
                    ntAlert(json.message)
                }
            }
        )
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
            <!--预定查看-->
            <div class="admin-main-title">
                简历详细
                &nbsp;
                &nbsp;
                <a href="javascript:;" onclick="openResumeStatusSelection(this,<%=Model.Status %>,<%=Model.Id %>)">
                    <%=Service.GetStatusName(Model.Status) %></a>
            </div>
            <table class="admin-table">

                <tr>
                    <td class="left-two">姓名：</td>
                    <td class="right-two"><%=Model.Name %></td>
                    <td class="left-two">性别：</td>
                    <td class="right-two td-end"><%=Model.Gender?"男":"女" %></td>
                </tr>
                <tr>
                    <td class="left-two">身高：</td>
                    <td class="right-two"><%=Model.Height %></td>
                    <td class="left-two">生日：</td>
                    <td class="right-two td-end">
                        <%=Model.BirthDay.ToString("yyyy-MM-dd") %>
                    </td>
                </tr>

                <tr>
                    <td class="left-two">婚姻状况：</td>
                    <td class="right-two"><%=Service.GetMarritalStatusName(Model.MarritalStatus) %></td>
                    <td class="left-two">职业：</td>
                    <td class="right-two td-end"><%=Model.Proffession %></td>
                </tr>

                <tr>
                    <td class="left-two">薪资要求：</td>
                    <td class="right-two"><%=Model.Salary %></td>
                    <td class="left-two">工作经历描述：</td>
                    <td class="right-two td-end"><%=Model.Work_History %></td>
                </tr>

                <tr>
                    <td class="left-two">家庭住址：</td>
                    <td class="right-two"><%=Model.HomeAddress%></td>
                    <td class="left-two">现住址：</td>
                    <td class="right-two td-end"><%=Model.Address %></td>
                </tr>

                <tr>
                    <td class="left-two">所在地邮编：</td>
                    <td class="right-two"><%=Model.ZipCode %></td>
                    <td class="left-two">联系方式：</td>
                    <td class="right-two td-end">固话:<%=Model.Phone%><br />
                        手机:<%=Model.MobilePhone %><br />
                        邮箱:<%=Model.Email %>
                    </td>
                </tr>

                <tr>
                    <td class="left-two">毕业学校：</td>
                    <td class="right-two"><%=Model.GraduatedFrom %></td>
                    <td class="left-two">毕业日期：</td>
                    <td class="right-two td-end"><%=Model.GraduatedDate.ToString("yyyy-MM-dd") %></td>
                </tr>

                <tr>
                    <td class="left-two">学历：</td>
                    <td class="right-two"><%=Service.GetEduDegreeName(Model.EduDegree) %></td>
                    <td class="left-two">专业：</td>
                    <td class="right-two td-end"><%=Model.Major %></td>
                </tr>

                <tr>
                    <td class="left-two">添加时间：</td>
                    <td class="right-two"><%=Model.AddDate.ToString("yyyy-MM-dd") %></td>
                    <td class="left-two">备注：</td>
                    <td class="right-two td-end">
                        <textarea cols="1" rows="2" id="ResumeNote"><%=Model.Note%></textarea>
                        <br />
                        <input type="button" class="admin-button" value="保存" onclick="saveResumeNote(<%=Model.Id%>);" />
                    </td>
                </tr>

                <tr>
                    <td class="left-two">附件：</td>
                    <td class="right-two"><a target="_blank" href="<%=string.IsNullOrEmpty(Model.AttachedObject)?"javascript:;":Model.AttachedObject %>">附件</a></td>
                    <td class="left-two">回复：</td>
                    <td class="right-two td-end">
                        <textarea cols="1" rows="2" id="ResumeReply"><%=Model.ReplyContent%></textarea>
                        <input type="button" class="admin-button" value="保存" onclick="saveResumeReply(<%=Model.Id%>);" />
                    </td>
                </tr>

                <tr class="admin-table-footer">
                    <td colspan="4" style="text-align: center;" class="td-end">
                        <input type="button" class="admin-button" value="关闭窗口" onclick="window.close();" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

