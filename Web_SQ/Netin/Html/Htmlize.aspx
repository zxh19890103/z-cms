<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" CodeFile="Htmlize.aspx.cs" Inherits="Admin_Html_Htmlize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">
        /*
        type:
        0-导航
        1-首页
        2-新闻
        3-产品
        4-下载
        5-课程
        6-二级页
        7-招聘
        8-新闻列表
        9-产品列表
        10-下载列表
        11-课程列表
        12-招聘列表
        */
        function htmlize(type) {
            ntAjax(
                {
                    method: 'Htmlize',
                    data: '{"type":"' + type + '"}',
                    success: function (msg) {
                        //ntAlert('静态化正在进行,这需要一些时间，请耐心等待!');
                    }
                }
                )
        }

        //清除静态化
        function dehtmlize(type) {
            showLoading();
            ntAjax(
                {
                    method: 'DeHtmlize',
                    data: '{"type":"' + type + '"}',
                    success: function (msg) {
                        var json = $.parseJSON(msg.d);
                        ntAlert(json.message, function () { removeLoading(); });
                    }
                }
                )
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-tips">
            1.执行静态化之前请务必在网站设置中开启静态化<br />
            2.当你的新闻或产品内容有更新的时候，您可以在这里进行相应的静态化生成操作
        </div>
        <div class="admin-main">
            <div class="admin-main-title">静态化</div>

            <table class="admin-table">
                <tr class="admin-table-header">
                    <td class="td-end">
                        <div class="html-msg">
                            <iframe frameborder="0" scrolling="no" width="100%" height="30" src="getCurrentInfo.ashx"></iframe>
                        </div>
                        <div class="admin-html">
                            <ul>
                                <li>
                                    <a href="javascript:;" onclick="htmlize(0);">导航静态化</a>
                                    |
                                    <a href="javascript:;" onclick="dehtmlize(0);">清除导航静态化</a>
                                </li>
                                <li>
                                    <a href="javascript:;" onclick="htmlize(1);">首页静态化</a>
                                    |
                                    <a href="javascript:;" onclick="dehtmlize(1);">清除首页静态化</a>
                                </li>
                                <li>
                                    <a href="javascript:;" onclick="htmlize(2);">新闻静态化</a>
                                    |
                                    <a href="javascript:;" onclick="dehtmlize(2);">清除新闻静态化</a>
                                </li>
                                <li>
                                    <a href="javascript:;" onclick="htmlize(3);">产品静态化</a>
                                    |
                                    <a href="javascript:;" onclick="dehtmlize(3);">清除产品静态化</a>
                                </li>
                                <li>
                                    <a href="javascript:;" onclick="htmlize(4);">下载静态化</a>
                                    |
                                    <a href="javascript:;" onclick="dehtmlize(4);">清除下载静态化</a>
                                </li>
                                <li>
                                    <a href="javascript:;" onclick="htmlize(5);">课程静态化</a>
                                    |
                                    <a href="javascript:;" onclick="dehtmlize(5);">清除课程静态化</a>
                                </li>
                                <li>
                                    <a href="javascript:;" onclick="htmlize(6);">二级页静态化</a>
                                    |
                                    <a href="javascript:;" onclick="dehtmlize(6);">清除二级页静态化</a>
                                </li>
                                <li>
                                    <a href="javascript:;" onclick="htmlize(7);">招聘静态化</a>
                                    |
                                    <a href="javascript:;" onclick="dehtmlize(7);">清除招聘静态化</a>
                                </li>
                                <li>
                                    <a href="javascript:;" onclick="htmlize(8);">新闻列表静态化</a>
                                    |
                                    <a href="javascript:;" onclick="dehtmlize(8);">清除新闻列表静态化</a>
                                </li>
                                <li>
                                    <a href="javascript:;" onclick="htmlize(9);">产品列表静态化</a>
                                    |
                                    <a href="javascript:;" onclick="dehtmlize(9);">清除产品列表静态化</a>
                                </li>
                                <li>
                                    <a href="javascript:;" onclick="htmlize(10);">下载列表静态化</a>
                                    |
                                    <a href="javascript:;" onclick="dehtmlize(10);">清除下载列表静态化</a>
                                </li>
                                <li>
                                    <a href="javascript:;" onclick="htmlize(11);">课程列表静态化</a>
                                    |
                                    <a href="javascript:;" onclick="dehtmlize(11);">清除课程列表静态化</a>
                                </li>
                                <li>
                                    <a href="javascript:;" onclick="htmlize(12);">招聘列表静态化</a>
                                    |
                                    <a href="javascript:;" onclick="dehtmlize(12);">清除招聘列表静态化</a>
                                </li>
                            </ul>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

