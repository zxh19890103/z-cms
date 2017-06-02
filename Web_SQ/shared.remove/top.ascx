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
                        <li><a href="/about/page.aspx?id=1">公司介绍</a> </li>
                        <li><a href="/about/page.aspx?id=2">组织框架</a> </li>
                        <li><a href="/about/product.aspx">士奇团队</a> </li>
                        <li><a href="/about/page.aspx?id=3">企业文化</a> </li>
                        <li><a href="/about/page.aspx?id=4">投资者关系</a> </li>
                        <li><a href="/about/page.aspx?id=5">联系我们</a> </li>
                    </ul>
                </li>
                <li><a href="/news">新闻中心 </a>
                    <ul>
                        <li><a href="/news/news.aspx?sortid=2">士奇动态</a> </li>
                        <li><a href="/news/news.aspx?sortid=3">业界动态</a> </li>
                        <li><a href="/news/news.aspx?sortid=4">媒体聚焦</a> </li>
                        <li><a href="/news/news.aspx?sortid=5">专题新闻</a> </li>
                    </ul>
                </li>
                <li><a href="/mgr">管理咨询 </a>
                    <ul>
                        <li><a href="/mgr/news.aspx?sortid=7">战略管理资讯</a> </li>
                        <li><a href="/mgr/news.aspx?sortid=8">企业改制资讯</a> </li>
                        <li><a href="/mgr/news.aspx?sortid=9">公司治理资讯</a> </li>
                        <li><a href="/mgr/news.aspx?sortid=10">组织管理资讯</a> </li>
                    </ul>
                </li>
                <li><a href="/investment">投资咨询 </a>
                    <ul>
                        <li><a href="/investment/news.aspx?sortid=12">财务顾问</a> </li>
                        <li><a href="/investment/news.aspx?sortid=13">股权投资PE/VC</a> </li>
                        <li><a href="/investment/news.aspx?sortid=14">市值管理</a> </li>
                    </ul>
                </li>
                <li><a href="/research">研究中心 </a>
                    <ul>
                        <li><a href="/research/news.aspx?sortid=16">研究报告</a> </li>
                        <li><a href="/research/product.aspx?sortid=2">研究院</a> </li>
                        <li><a href="/research/news.aspx?sortid=17">政策法规</a> </li>
                        <li><a href="/research/product.aspx?sortid=3">合作机构</a> </li>

                    </ul>
                </li>
                <li><a href="/case">咨询案例</a>
                    <ul>
                        <li><a href="/case/news.aspx?sortid=19">管理资讯</a> </li>
                        <li><a href="/case/news.aspx?sortid=21">投资资讯</a> </li>
                        <li><a href="/case/news.aspx?sortid=22">行业资讯</a> </li>
                        <li><a href="/case/news.aspx?sortid=23">甄选经典</a> </li>
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
    if (CurrentPage.ChannelNo > 1)
    {
%>
<div class="clear"></div>
<div class="banner_other">
    <img src="../images/banner<%=CurrentPage.ChannelNo %>.png" width="1400" height="327" alt="" />
</div>
<div class="clear"></div>
<%} %>
