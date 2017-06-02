<%@ Control Language="C#" ClassName="foot" Inherits="Nt.Web.BaseUserControl" %>

<div class="clear"></div>
<!--goto top-->
<script type="text/javascript" src="/scripts/scrolltop.js"></script>
<div style="DISPLAY: none" id="goTopBtn">
    <img border="0" src="../images/lanren_top.jpg" alt="" />
</div>
<script type="text/javascript">goTopEx();</script>
<!-- end -->
<div class="foot">
    <div class="foot_in">
        <div class="foot_content">
            <div class="home_foot_nav fl">

                <ul id="home_foot_nav">
                    <li><a href="/about">关于士奇</a>
                        <ul>
                            <!--#include file="../html.inc/aboutus.menu.htm"-->
                        </ul>
                    </li>
                    <li><a href="/news">新闻中心 </a>
                        <ul>
                            <%
                                MyUtility.C.CacheNamePattern = "foot-part-{0}";
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


            </div>
            <div class="home_contactUs fl">
                <img src="/images/home_map.png" width="319" height="128" />
                <p>地址：北京士奇投资所在</p>
                <p>电话：010-89898989</p>
                <p>email：beijingshiqi@163.com</p>
                <span>
                    <a href="#">隐私政策</a> / <a href="#">使用条款</a>/<a href="#">合作伙伴</a>/<a href="#">加盟士奇</a>/<a href="/link.aspx">友情连接</a>
                </span>
            </div>
        </div>
        <div class="Copyright">
            京ICP备09011550号　京公网安备110108006050&nbsp;Copyright &copy; www.shiqi.com ,2014.士奇投资版权所有&nbsp;&nbsp;<a href="http://www.naite.com.cn">奈特原动力</a>&nbsp;技术支持
        
        </div>
    </div>
</div>
