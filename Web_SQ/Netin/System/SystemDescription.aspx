<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
   Inherits="Nt.Pages.System.SystemDescription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-home">
        <div class="position">
            <span class="fr">当前位置：管理首页</span>
        </div>
        <div class="body">
            <div class="message-wrap">
                <span class="welcome">您好，欢迎您使用 NETIN7 网站管理系统</span>
            </div>
            <div class="easy-door">
                <ul>
                    <li>
                        <a href="/Netin/SinglePage/SinglePage.aspx">
                            <img src="/Netin/Content/images/btn1.jpg" alt="页面管理" />
                            <span>页面管理</span>
                        </a>
                    </li>
                    <li>
                        <a href="/Netin/News/List.aspx">
                            <img src="/Netin/Content/images/btn2.jpg" alt="新闻管理" />
                            <span>新闻管理</span>
                        </a>
                    </li>
                    <li>
                        <a href="/Netin/Product/List.aspx">
                            <img src="/Netin/Content/images/btn3.jpg" alt="产品管理" />
                            <span>产品管理</span>
                        </a>
                    </li>
                    <li>
                        <a href="/Netin/Book/List.aspx">
                            <img src="/Netin/Content/images/btn4.jpg" alt="预定/留言管理" />
                            <span>预定/留言管理</span>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="bottom">
            <div class="mail">
                <span class="title">客户服务信箱：</span>
                <span class="mail2">kefu1@naite.com.cn</span>
            </div>
            <div class="phone">
                <span class="title">客户咨询热线：</span>
                <span class="phone2">0411-82528287   82527885</span>
            </div>
            <div class="QQ">
                <span class="title">客服QQ</span>
                <span><a href="http://wpa.qq.com/msgrd?v=3&uin=449238915&site=qq&menu=yes" target="_blank">
                    <img src="/Netin/Content/images/QQon.jpg" alt="" /></a></span>
                <span><a href="http://wpa.qq.com/msgrd?v=3&uin=707496807&site=qq&menu=yes" target="_blank">
                    <img src="/Netin/Content/images/QQoff.jpg" alt="" /></a></span>
            </div>
        </div>
    </div>
</asp:Content>
