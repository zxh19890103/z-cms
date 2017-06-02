<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Course.Edit" %>

<%@ Register Src="~/Netin/Shared/UcEditor.ascx" TagPrefix="uc1" TagName="UcEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <uc1:UcEditor runat="server" ID="UcEditor" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                <%=EditTitlePrefix %>课程
            </div>
            <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl%>" onsubmit="<%=OnSubmitCall() %>">
                <table class="admin-table">
                    <tr>
                        <td class="left">班级分类：</td>
                        <td class="right">
                            <%=HtmlHelper.DropdownList(CourseCategories, new {_class="category-drop-down",name="CourseCategory_Id" })%>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">标题：</td>
                        <td class="right">
                            <input type="text" class="input-text" name="Title" value="<%=Model.Title %>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">开班日期：</td>
                        <td class="right">
                            <input type="text" name="CourseStartDate" maxlength="1024" class="time input-datetime" value="<%=Model.CourseStartDate.ToString("yyyy-MM-dd") %>" />
                            <span class="admin-tips">请勿更改时间格式</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">时间：</td>
                        <td class="right">
                            <input type="text" name="CourseTimeSpan" maxlength="1024" class="input-text" value="<%=Model.CourseTimeSpan%>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">时段：</td>
                        <td class="right">
                            <input type="text" name="CourseDuration" class="input-text" value="<%=Model.CourseDuration%>" />
                        </td>
                    </tr>

                    <tr>
                        <td class="left">授课老师：</td>
                        <td class="right">
                            <input type="text" name="CourseTeachers" maxlength="1024" class="input-text" value="<%=Model.CourseTeachers%>" /></td>
                    </tr>

                    <tr>
                        <td class="left">使用教材：</td>
                        <td class="right">
                            <input type="text" name="CourseBooks" maxlength="1024" class="input-text" value="<%=Model.CourseBooks%>" /></td>
                    </tr>

                    <tr>
                        <td class="left">目标：</td>
                        <td class="right">
                            <input type="text" name="CourseTarget" maxlength="1024" class="input-text" value="<%=Model.CourseTarget%>" /></td>
                    </tr>

                    <tr>
                        <td class="left">推广关键词：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="MetaKeywords"><%=Model.MetaKeywords %></textarea><span class="admin-tips">请勿超过1024个字符，并且没有Html标签</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">推广描述：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="MetaDescription"><%=Model.MetaDescription %></textarea><span class="admin-tips">请勿超过1024个字符，并且没有Html标签</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">内容简述：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="Short"><%=Model.Short%></textarea>
                            <span class="admin-tips">请勿超过1024个字符，并且没有Html标签</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">内容：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" name="Body" style="width: 800px; height: 400px; visibility: hidden;"><%=Server.HtmlEncode(Model.Body) %></textarea>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">排序：</td>
                        <td class="right">
                            <input type="text" class="input-int32" maxlength="5"  name="DisplayOrder" value="<%=Model.DisplayOrder==0?MaxID:Model.DisplayOrder %>" />
                            <span class="admin-tips">排序值越大，显示顺序越靠前</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">属性：</td>
                        <td class="right">
                            <table class="admin-checkbox-list" cellspacing="5">
                                <tr>
                                    <td>
                                        <%=HtmlHelper.CheckBox("显示", Model.Display,"Display", new { })%>
                                    </td>
                                    <td>
                                        <%=HtmlHelper.CheckBox("推荐", Model.Recommended,"Recommended", new { })%>
                                    </td>
                                    <td>
                                        <%=HtmlHelper.CheckBox("置顶", Model.SetTop, "SetTop",new { })%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">添加时间：</td>
                        <td class="right">
                            <input type="text" name="AddDate" class="time input-datetime" value="<%=Model.AddDate.ToString("yyyy-MM-dd") %>" />
                            <span class="admin-tips">请勿更改时间格式</span></td>
                    </tr>

                    <tr style="height: 60px;">
                        <td align="center" colspan="2" class="td-end">
                            <input type="submit" class="admin-button" value="保存" />
                            <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                            <input type="hidden" name="Id" value="<%=NtID %>" />
                            <input type="hidden" name="Language_Id" value="<%=WorkingLang.Id%>" />
                            <input type="hidden" name="ClickRate" value="<%=Model.ClickRate %>" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

