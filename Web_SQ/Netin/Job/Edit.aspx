<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
Inherits="Nt.Pages.Job.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <%=EditTitlePrefix %>职位信息
            </div>
            <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl%>" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">
                    <tr>
                        <td class="left">职位名：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="JobName" maxlength="255" value="<%=Model.JobName %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="left">招聘人数：</td>
                        <td class="right">
                            <input type="text" name="RecruitCount" class="input-int32" maxlength="5"  value="<%=Model.RecruitCount %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">薪水：</td>
                        <td class="right">
                            <input type="text" name="Salary" maxlength="225" class="input-text" value="<%=Model.Salary %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">Hr：</td>
                        <td class="right">
                            <input type="text" name="Hr" maxlength="225" class="input-text" value="<%=Model.Hr %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">电话：</td>
                        <td class="right">
                            <input type="text" name="Phone" maxlength="225" class="input-text" value="<%=Model.Phone %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">邮箱：</td>
                        <td class="right">
                            <input type="text" name="Email" maxlength="225" class="input-text" value="<%=Model.Email %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">工作地点：</td>
                        <td class="right">
                            <input type="text" name="WorkPlace" maxlength="225" class="input-text" value="<%=Model.WorkPlace %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">开始日期：</td>
                        <td class="right">
                            <input type="text" name="StartDate" maxlength="225" class="time input-datetime" value="<%=Model.StartDate.ToString("yyyy-MM-dd") %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">结束日期：</td>
                        <td class="right">
                            <input type="text" name="EndDate" maxlength="225" class="time input-datetime" value="<%=Model.EndDate.ToString("yyyy-MM-dd") %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">排序：</td>
                        <td class="right">
                            <input type="text" class="input-int32" maxlength="5"  name="DisplayOrder" value="<%=Model.DisplayOrder==0?MaxID:Model.DisplayOrder %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">是否显示：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(EnsureEdit?Model.Display:true,"Display", new { })%>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">备注：</td>
                        <td class="right">
                            <textarea cols="2" rows="3" name="Note"><%=Model.Note %></textarea>
                        </td>
                    </tr>

                      <tr>
                        <td class="left">职责：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="Duties"><%=Model.Duties %></textarea><span>*请勿超过1024个字符。</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">需求：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="Requirements"><%=Model.Requirements %></textarea><span>*请勿超过1024个字符。</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">添加时间：</td>
                        <td class="right">
                            <input type="text" name="AddDate" class="time input-datetime" value="<%=Model.AddDate.ToString("yyyy-MM-dd") %>" />
                            <span>*请勿更改时间格式</span></td>
                    </tr>

                    <tr style="height: 60px;">
                        <td align="center" colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                            <input type="hidden" name="Language_Id" value="<%=LanguageID%>" />
                            <input type="hidden" name="ClickRate" value="<%=Model.ClickRate %>" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

