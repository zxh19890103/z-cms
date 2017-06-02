<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.News.Edit" %>

<%@ Register Src="~/Netin/Shared/UcEditor.ascx" TagPrefix="uc1" TagName="UcEditor" %>

<%@ Register Src="~/Netin/Shared/UcEditorPic.ascx" TagPrefix="uc1" TagName="UcEditorPic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <uc1:UcEditor runat="server" ID="UcEditor" />
	
	<uc1:UcEditorPic runat="server" ID="UcEditorPic" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl%>" onsubmit="<%=OnSubmitCall() %>">
                <div class="admin-main-title">
                    <%=EditTitlePrefix %>新闻
                    <input type="submit" class="admin-button-head-save" value="保存" />
                    <input type="button" class="admin-button-head-back" onclick="<%=GoBackScript()%>" value="返回" />
                </div>

                <table class="admin-table">
                    <tr>
                        <td class="left">分类：</td>
                        <td class="right">
                            <%=HtmlHelper.DropdownList(NewsCategories, new {_class="category-drop-down",name="NewsCategory_Id",onchange="changezd(this);" })%>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">标题：</td>
                        <td class="right">
                            <input type="text" class="input-text" id="Title" name="Title" maxlength="512" value="<%=Model.Title %>" />
                            <a href="javascript:;" onclick="copyTitle();">复制标题</a>
                        </td>
                    </tr>
                     <tr>
                        <td class="left">推广关键词：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" id="MetaKeywords" name="MetaKeywords"><%=Model.MetaKeyWords %></textarea><span class="admin-tips">请勿超过1024个字符，并且没有Html标签</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">推广描述：</td>
                        <td class="right">
                            <textarea cols="30" rows="5" id="MetaDescription" name="MetaDescription"><%=Model.MetaDescription %></textarea><span class="admin-tips">请勿超过1024个字符，并且没有Html标签</span>
                        </td>
                    </tr>


					<!-- sortid  6-14,24-28 显示 -->
                    
						 <style>
							.xzzd{display:none;}
						 </style>
						 <script>
						 
						 function changezd(s)
						 {
						   var x = s.value;
						   if( (x>5 && x<15)||(x>23 && x<29)||(x>31 && x<36) )
						   {
							$('.xzzd').show();
						   }
						   else
						   {
						   $('.xzzd').hide();
						   }
						    
						 }
						 
						 </script>
						 
					<tr class="xzzd">
                        <td class="left">标题翻译：</td>
                        <td class="right">
                            <input type="text" class="input-text" id="Title2" name="Title2" maxlength="512" value="<%=Model.Title2 %>" />
                             <span class="admin-tips">请勿超过256个字符，并且没有Html标签</span>
                        </td>
                    </tr>
					<tr class="xzzd">
                        <td class="left">小标题：</td>
                        <td class="right">
                            <input type="text" class="input-text" id="Title3" name="Title3" maxlength="512" value="<%=Model.Title3 %>" />
                             <span class="admin-tips">请勿超过256个字符，并且没有Html标签</span>
                        </td>
                    </tr>
					<tr class="xzzd">
                        <td class="left">小标题翻译：</td>
                        <td class="right">
                            <input type="text" class="input-text" id="Title4" name="Title4" maxlength="512" value="<%=Model.Title4 %>" />
                           <span class="admin-tips">请勿超过256个字符，并且没有Html标签</span>
                        </td>
                    </tr>
					<tr class="xzzd">
                    <td class="left">缩略图：</td>
                       <td class="right">
                            <textarea cols="30" rows="5" name="NewsPicture" style="width: 800px; height: 250px; visibility: hidden;"><%=Server.HtmlEncode(Model.NewsPicture) %></textarea>
						<span class="admin-tips">请勿上传文字，只能上传图片，且为一张,尺寸306*118(像素)</span>
						</td>
                    </tr>
					
					<!-- end-->
					
					
					
					
					
					
                   
                    
                    <tr>
                        <td class="left">发布者：</td>
                        <td class="right">
                            <input type="text" class="input-text" id="Author" name="Author" value="<%=Model.Author %>" />
                            <a href="javascript:;" title="以公司名作为发布者" onclick="document.getElementById('Author').value=$(this).text();"><%=WebsiteSettings.CompanyName==""?"无内容":WebsiteSettings.CompanyName %></a>
                        </td>
                    </tr>

                    <tr>
                        <td class="left">源出处：</td>
                        <td class="right">
                            <input type="text" class="input-text" id="Source" name="Source" value="<%=Model.Source %>" />
                            <a href="javascript:;" title="以本网站网址作为源出处" onclick="document.getElementById('Source').value=$(this).text();"><%=WebsiteSettings.WebsiteUrl==""?"无内容":WebsiteSettings.WebsiteUrl %></a>
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
                                        <%=HtmlHelper.CheckBox("显示",EnsureEdit?Model.Display:true,"Display", new { })%>
                                    </td>
                                    <td>
                                        <%=HtmlHelper.CheckBox("推荐", EnsureEdit?Model.Recommended:true,"Recommended", new { })%>
                                    </td>
                                    <td>
                                        <%=HtmlHelper.CheckBox("置顶", EnsureEdit?Model.SetTop:true, "SetTop",new { })%>
                                    </td>
                                </tr>
                            </table>
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
                            <input type="hidden" name="Language_Id" value="<%=WorkingLang.Id%>" />
                            <input type="hidden" name="ClickRate" value="<%=Model.ClickRate %>" />
                            <input type="hidden" name="EditDate" value="<%=DateTime.Now %>" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</asp:Content>

