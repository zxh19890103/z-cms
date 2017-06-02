<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
   Inherits="Nt.Pages.Seo.SiteMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        function GenSitemap(t) {
            showLoading();
            ntAjax({
                method: 'GenSitemap',
                data: '{"type":"' + t + '"}',
                success: function (msg) {
                    var json = $.parseJSON(msg.d);
                    if (json.error) {
                        removeLoading();
                        ntAlert(json.message, function () {
                        })
                    } else {
                        removeLoading();
                        ntAlert(json.message, function () {
                            var bog = t == 1 ? '百度' : '谷歌';
                            var html = [
                            '<div>' + bog + '网站地图生成成功,共<font color="red">' + json.countOfFound + '</font>条记录被检索.</div>',
                            '<div><a target="_blank" href="' + json.sitemapPath + '"><font color="red">查看Sitemap.xml</font></a>,',
                            '<a target="_blank"  href="' + json.postUrl + '">提交至' + bog + '</a>',
                            '</div>'
                            ].join('');
                            $('#sitemap-generated-message').html(html);
                        });
                    }
                }
            })
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-tips">
            请勿重复提交您的网站，否则容易被搜索引擎视为作弊行为。
        </div>
        <div id="sitemap-generated-message"></div>
        <div class="admin-main">
            <div class="admin-main-title">网站地图</div>
            <table class="admin-table">
                <tr>
                    <td height="150" align="center">
                        <img alt="" src="/Netin/Content/Images/logo.baidu.jpg" width="215" vspace="15" /></td>
                    <td align="center">
                        <img alt="" src="/Netin/Content/Images/logo.google.jpg" width="215" vspace="15" /></td>
                    <td align="center" class="td-end">
                        <img alt="" src="/Netin/Content/Images/logo.sogou.gif" width="215" vspace="15" /></td>
                </tr>
                <tr>
                    <td height="26" align="center">
                        <a href="http://zhanzhang.baidu.com/sitesubmit/index/" class="admin-a-button" target="_blank">提交网站入口</a>
                        <a href="javascript:;" class="admin-a-button" onclick="GenSitemap(1);">生成baidu地图</a></td>
                    <td align="center">
                        <a href="http://www.google.com/submit_content.html" class="admin-a-button" target="_blank">提交网站入口</a>
                        <a href="javascript:;" class="admin-a-button" onclick="GenSitemap(2);">生成google地图</a></td>
                    <td align="center" class="td-end">
                        <a href="http://www.sogou.com/feedback/urlfeedback.php" class="admin-a-button" target="_blank">提交网站入口</a></td>
                </tr>
                <tr>
                    <td height="150" align="center">
                        <img alt="" src="/Netin/Content/Images/logo.bing.jpg" width="222" vspace="15" /></td>
                    <td align="center">
                        <img alt="" src="/Netin/Content/Images/logo.360.png" width="222" vspace="15" /></td>
                    <td align="center" class="td-end">
                        <img alt="" src="/Netin/Content/Images/logo.soso.jpg" width="222" vspace="15" /></td>
                </tr>
                <tr>
                    <td height="26" align="center"><a href="http://www.bing.com/toolbox/submit-site-url/" class="admin-a-button" target="_blank">提交网站入口</a></td>
                    <td align="center"><a href="http://hao.360.cn/url.html" class="admin-a-button" target="_blank">提交网站入口</a></td>
                    <td align="center" class="td-end"><a href="http://www.sousuoyinqingtijiao.com/soso/" class="admin-a-button" target="_blank">提交网站入口</a></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

