<%@ Control Language="C#" ClassName="top" Inherits="Nt.Web.BaseUserControl" %>

<div class="header">
    <div class="header_in">
        <div class="logoer">
            <div class="logo fl">
                <img src="/images/logo_img.png" />
            </div>
            <div class="logo_Tel fr ">
                <p>0411-86666692</p>
                <form>
                    <input type="search" value="搜索感兴趣的内容" class="search"
                        onfocus="if(this.value=='搜索感兴趣的内容') {this.value='';}"
                        onblur="if(this.value=='') {this.value='搜索感兴趣的内容';}" />
                    <input type="image" class="search_bt" src="/images/search_bt.png" />
                </form>
            </div>
        </div>
        <div class="nav fr">

            <ul id="nav">
                <li><a href="/">首&nbsp;&nbsp;页 </a></li>
                <li><a href="/about">关于士奇</a>
                    <ul>
                        <!--#include file="../html.inc/aboutus.menu.htm"-->
                    </ul>
                </li>
                <li><a href="/news">新闻中心 </a>
                    <ul>
                        <%
                            MyUtility.C.CacheNamePattern = "top-part-{0}";
                            MyUtility.C.OutNewsClasses(1, "<li><a href=\"/news/news.aspx?sortid={id}&pid=1\">{name}</a> </li>"); %>
                    </ul>
                </li>
                <li><a href="/mgr">管理咨询 </a>
                    <ul>
                        <%MyUtility.C.OutNewsClasses(6, "<li><a href=\"/mgr/news.aspx?sortid={id}&pid=6\">{name}</a> </li>"); %>
                    </ul>
                </li>
                <li><a href="/investment">投资咨询 </a>
                    <ul>
                        <%MyUtility.C.OutNewsClasses(11, "<li><a href=\"/investment/news.aspx?sortid={id}&pid=11\">{name}</a> </li>"); %>
                    </ul>
                </li>
                <li><a href="/research">研究中心 </a>
                    <ul>
                        <li><a href="/research/product.aspx">研究团队</a> </li>
                        <%MyUtility.C.OutNewsClasses(15, "<li><a href=\"/research/news.aspx?sortid={id}&pid=15\">{name}</a> </li>"); %>
                        <li><a href="/research/hezuojigou.aspx">合作机构</a> </li>
                    </ul>
                </li>
                <li><a href="/case">咨询案例</a>
                    <ul>
                        <%MyUtility.C.OutNewsClasses(18, "<li><a href=\"/case/news.aspx?sortid={id}&pid=18\">{name}</a> </li>"); %>
                    </ul>
                </li>
            </ul>
            <script>
                $('#nav li:last-child').css('background', 'none');
            </script>
        </div>
    </div>
</div>
<%
    //not home
    if (CurrentPage.ChannelNo > 1)
    {
%>
<div class="clear"></div>
<div class="banner_other">
    <img src="../images/banner<%=CurrentPage.ChannelNo %>.png" width="1400" height="327" alt="" />
</div>
<div class="clear"></div>
<%} %>
