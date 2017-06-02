<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Download.Edit" %>

<%@ Register Src="~/Netin/Product/Uc/ProductPicture.ascx" TagPrefix="uc1" TagName="ProductPicture" %>
<%@ Register Src="~/Netin/Shared/UcDownload.ascx" TagPrefix="uc1" TagName="UcDownload" %>
<%@ Register Src="~/Netin/Shared/UcEditor.ascx" TagPrefix="uc1" TagName="UcEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <style type="text/css">
        /*Example for a Menu Style*/
        .easytab-menu { border-bottom: 1px solid #d7d7d7; height: 40px; background: url(../Content/Images/tab-background.jpg) repeat-x; }
            .easytab-menu ul { margin: 0px; padding: 0px; list-style: none; text-align: center; }
            .easytab-menu li { display: inline; line-height: 40px; }
                .easytab-menu li a { color: #F59500; text-decoration: none; padding: 5px 5px 6px 5px; }
                    .easytab-menu li a.tabactive { border-left: 1px solid #d7d7d7; border-right: 1px solid #d7d7d7; color: #000000; font-weight: bold; position: relative; }
        #contentthree1, #contentthree2, #contentthree3 { border: 1px solid #ececec; text-align: center; padding: 6px 0px; font-size: 12px; margin-bottom: 5px; }
        #contentthree2, #contentthree3 { display: none; }
    </style>
    <script src="../Scripts/admin.product.picture.js" type="text/javascript"></script>
    <script type="text/javascript">
        var nt = nt || {};
        nt.currentTabId = 1;
        function easytabs(num) {
            if (nt.currentTabId == num)
                return;
            $('#contentthree' + num).css('display', 'block');
            $('#contentthree' + nt.currentTabId).css('display', 'none');
            $('#linkthree' + num).addClass('tabactive');
            $('#linkthree' + nt.currentTabId).removeClass('tabactive');
            nt.currentTabId = num;
        }
    </script>
    <uc1:UcEditor runat="server" ID="UcEditor" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <%
        UcDownload.FileUrl = Model.FileUrl;
        UcDownload.FileSize = Model.FileSize;
        ProductPicture.DataSource = ProductPictures;
        ProductPicture.DataBind();
    %>
    <div class="admin-main-wrap">
        <form method="post" name="EditForm" id="EditForm" action="<%=LocalUrl%>" onsubmit="<%=OnSubmitCall() %>">
            <div id="product-edit-tabs">
                <!--tabs-->
                <div class="easytab-menu">
                    <ul>
                        <li><a href="#" onclick="easytabs(1);" title="" id="linkthree1" class="tabactive">基本信息</a></li>
                        <li><a href="#" onclick="easytabs(2);" title="" id="linkthree2">图片</a></li>
                    </ul>
                </div>

                <!--基本信息-->
                <div id="contentthree1" class="product-info-base">
                    <div class="admin-main">
                        <div class="admin-main-title">
                            下载基本信息
                            <input type="submit" class="admin-button-head-save" value="保存" />
                            <input type="button" class="admin-button-head-back" onclick="<%=GoBackScript()%>" value="返回" />
                        </div>
                        <table class="admin-table">

                            <tr>
                                <td class="left">分类：</td>
                                <td class="right">
                                    <%=HtmlHelper.DropdownList(ProductCategories, new {_class="category-drop-down",name="ProductCategory_Id" })%>
                                </td>
                            </tr>

                            <tr>
                                <td class="left">标题：</td>
                                <td class="right">
                                    <input type="text" class="input-text" id="Title" name="Title" maxlength="512" value="<%=Model.Title %>" /><a href="javascript:;" onclick="copyTitle();">复制标题</a>
                                </td>
                            </tr>

                            <tr>
                                <td class="left">推广关键词：</td>
                                <td class="right">
                                    <textarea cols="30" rows="5" id="MetaKeywords" name="MetaKeywords"><%=Model.MetaKeyWords %></textarea>
                                    <span class="admin-tips">请勿超过1024个字符，并且没有Html标签</span>
                                </td>
                            </tr>

                            <tr>
                                <td class="left">推广描述：</td>
                                <td class="right">
                                    <textarea cols="30" rows="5" id="MetaDescription" name="MetaDescription"><%=Model.MetaDescription %></textarea>
                                    <span class="admin-tips">请勿超过1024个字符，并且没有Html标签</span>
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
                                <td class="left">附件：</td>
                                <td class="right">
                                    <uc1:UcDownload runat="server" ID="UcDownload" />
                                </td>
                            </tr>

                            <tr>
                                <td class="left">排序：</td>
                                <td class="right">
                                    <input type="text" class="input-int32" maxlength="5" name="DisplayOrder" value="<%=Model.DisplayOrder==0?MaxID:Model.DisplayOrder %>" />
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
                                    <input type="text" name="AddDate" class="input-datetime time" value="<%=Model.AddDate.ToString("yyyy-MM-dd") %>" />
                                    <span class="admin-tips">请勿更改时间格式</span></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <script type="text/javascript">
                    /*max num of product pictures*/
                    function makeSureUploadNotExceed() {
                        var maxProPicNum=<%=ProductPicture.MaxPicturesCount%>;
                        var currentProPicCount=$('#table-product-pictures tr').size()-1;
                        if(currentProPicCount<maxProPicNum)
                            return true;
                        ntAlert('您上传的图片数量已经超过最大设置!');
                        return false;
                    }
                </script>
                <!--产品图片-->
                <div id="contentthree2" class="product-info-images">
                    <div class="admin-tips">
                        1.图片的标题和Alt有助于推广，请认真填写<br />
                        2.设置为缩略图的图片将会显示在首页或列表页<br />
                        3.如果没有设置任何图片为缩略图，那么系统将选取排序最大的那张图片作为缩略图<br />
                        4.请保证您所上传的图片的尺寸大体符合比例<br />
                        5.如果您上传的图片严重不合比例，前台的图片将无法正确显示
                    </div>
                    <div class="admin-main">
                        <div class="admin-main-title">
                            下载图片
                            <input type="submit" class="admin-button-head-save" value="保存" />
                            <input type="button" class="admin-button-head-back" onclick="<%=GoBackScript()%>" value="返回" />
                            <a href="javascript:;" onclick="if(makeSureUploadNotExceed()){addNew();}" title="添加">添加</a>
                        </div>
                        <uc1:ProductPicture runat="server" ID="ProductPicture" />
                    </div>
                </div>

                <!--保存-->
                <div>
                    <center>
                        <input type="submit" class="admin-button" value="保存" />
                        <input type="button" class="admin-button" onclick="<%=GoBackScript()%>" value="返回" />
                        <input type="hidden" name="Id" value="<%=NtID %>" />
                        <input type="hidden" name="ClickRate" value="<%=Model.ClickRate %>" />
                        <input type="hidden" name="IsDownloadable" value="True" />
                        <input type="hidden" name="Language_Id" value="<%=WorkingLang.Id %>" />
                        <input type="hidden" name="DownloadedRate" value="<%=Model.DownloadedRate%>" />
                    </center>
                </div>
            </div>
        </form>
    </div>
</asp:Content>

