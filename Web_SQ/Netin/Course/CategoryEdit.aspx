<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" 
  Inherits="Nt.Pages.Course.CategoryEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <%=EditTitlePrefix %>班级类别
            </div>
            <form action="<%=LocalUrl %>" method="post" id="EditForm" name="EditForm" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">

                    <tr>
                        <td class="left">父类别：</td>
                        <td class="right">
                            <label><%=ParentName %></label>
                            <input name="Parent" type="hidden" class="input-text" value="<%=ParentID %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">类别名称：</td>
                        <td class="right">
                            <input type="text" class="input-text no-comma" name="Name" maxlength="255" value="<%=Model.Name %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">实用人群：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="FitPeople"><%=Model.FitPeople %></textarea>
                            <span class="admin-tips">请勿超过1024个字符。</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">师资力量：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="EduTeachers"><%=Model.EduTeachers %></textarea>
                            <span class="admin-tips">请勿超过1024个字符。</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">班级类别：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="CourseType" maxlength="255" value="<%=Model.CourseType %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">教学目的：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="EduAim"><%=Model.EduAim %></textarea>
                            <span class="admin-tips">请勿超过1024个字符。</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">是否显示：</td>
                        <td class="right">
                            <%=HtmlHelper.CheckBox(EnsureEdit?Model.Display:true, "Display", new{})%>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">排序：</td>
                        <td class="right">
                            <input type="text" class="input-int32" maxlength="5"  name="DisplayOrder" value="<%=Model.DisplayOrder==0?MaxID:Model.DisplayOrder %>" />
                            <span class="admin-tips">排序值越大，显示顺序越靠前</span>
                        </td>
                    </tr>

                    <tr style="height: 60px;">
                        <td align="center" colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=Model.Id %>" />
                            <input type="hidden" name="ClickRate" value="<%=Model.ClickRate %>" />
                            <input type="hidden" name="Language_Id" value="<%=WorkingLang.Id %>" />
                            <input type="hidden" name="Crumbs" value="0" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

