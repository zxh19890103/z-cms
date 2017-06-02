<%@ Page Language="C#"  Inherits="Nt.Web.BasePage"%>

<%@ Register Src="~/shared/top.ascx" TagPrefix="uc1" TagName="top" %>
<%@ Register Src="~/shared/foot.ascx" TagPrefix="uc1" TagName="foot" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<metahttp-equiv="X-UA-Compatible"content="IE=8"/>
<title>北京士奇投资</title>
<link rel="stylesheet" type="text/css" href="css/style.css"/>
<script type="text/javascript" src="scripts/jquery.min.js"></script>
<!-- wedo -->
<script type="text/javascript" src="scripts/jquery.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('.box .box1').mouseover(function () {
            $(this).stop().animate({ "top": "-290px" }, 1);
        })
        $('.box .box1').mouseout(function () {
            $(this).stop().animate({ "top": "0" }, 2);
        })
    })
</script>
<!--end-->


<script type="text/javascript">
    $(function () {
        var sWidth = $("#focus").width(); //获取焦点图的宽度（显示面积）
        var len = $("#focus ul li").length; //获取焦点图个数
        var index = 0;
        var picTimer;

        //以下代码添加数字按钮和按钮后的半透明条，还有上一页、下一页两个按钮
        var btn = "<div class='btnBg'></div><div class='btn'>";
        for (var i = 0; i < len; i++) {
            btn += "<span></span>";
        }
        btn += "</div><div class='preNext pre'></div><div class='preNext next'></div>";
        $("#focus").append(btn);
        $("#focus .btnBg").css("opacity", 0);

        //为小按钮添加鼠标滑入事件，以显示相应的内容
        $("#focus .btn span").css("opacity", 0.4).mouseenter(function () {
            index = $("#focus .btn span").index(this);
            showPics(index);
        }).eq(0).trigger("mouseenter");

        //上一页、下一页按钮透明度处理
        $("#focus .preNext").css("opacity", 0.2).hover(function () {
            $(this).stop(true, false).animate({ "opacity": "0.5" }, 300);
        }, function () {
            $(this).stop(true, false).animate({ "opacity": "0.2" }, 300);
        });

        //上一页按钮
        $("#focus .pre").click(function () {
            index -= 1;
            if (index == -1) { index = len - 1; }
            showPics(index);
        });

        //下一页按钮
        $("#focus .next").click(function () {
            index += 1;
            if (index == len) { index = 0; }
            showPics(index);
        });

        //本例为左右滚动，即所有li元素都是在同一排向左浮动，所以这里需要计算出外围ul元素的宽度
        $("#focus ul").css("width", sWidth * (len));

        //鼠标滑上焦点图时停止自动播放，滑出时开始自动播放
        $("#focus").hover(function () {
            clearInterval(picTimer);
        }, function () {
            picTimer = setInterval(function () {
                showPics(index);
                index++;
                if (index == len) { index = 0; }
            }, 3000); //此4000代表自动播放的间隔，单位：毫秒
        }).trigger("mouseleave");

        //显示图片函数，根据接收的index值显示相应的内容
        function showPics(index) { //普通切换
            var nowLeft = -index * sWidth; //根据index值计算ul元素的left值
            $("#focus ul").stop(true, false).animate({ "left": nowLeft }, 300); //通过animate()调整ul元素滚动到计算出的position
            //$("#focus .btn span").removeClass("on").eq(index).addClass("on"); //为当前的按钮切换到选中的效果
            $("#focus .btn span").stop(true, false).animate({ "opacity": "0.4" }, 300).eq(index).stop(true, false).animate({ "opacity": "1" }, 300); //为当前的按钮切换到选中的效果
        }
    });

</script>
    
<!--ie6  支持png 图片 背景透明--> 
<script type="text/javascript" src="images/iepngfix_tilebg.js"></script>
<style>
    img, div, input
    {
        behavior: url("images/iepngfix.htc");
    }
</style>
<!--end-->
</head>

<body>
    
    <uc1:top runat="server" ID="top" />

<div class="clear"></div>
<div class="banner">
 <div class="wrapper">
	<div id="focus">
		<ul>
			<li><a href="#" target="_blank"><img src="/images/banner_1.png" width="1400" height="386" /></a></li>
			<li><a href="#" target="_blank"><img src="/images/banner_1.png" width="1400" height="386" /></a></li>
			<li><a href="#" target="_blank"><img src="/images/banner_1.png" width="1400" height="386" /></a></li>
		</ul>
	</div>
</div>
</div>
<div class="clear"></div>
<div class="Home_Wedo">
	<div class="Home_wedo_in">
    	<div class="wedo_title">
     		<img src="/images/wedo_title_img.png" width="207" height="152" />
        </div>
        <div class="wedo_list">
        	<div id="in_ct">
<!----------in_ct开始---------->
	<div class="in_ct">	
    	<!----------in_ct1 开始---------->
            <div class="in_ct1">
            	<ul class="box" id="wedo_list">

                <!--一排-->
                    <li>
                        <div class="box1">
                            <div class="toll_img">
                            	<img src="images/in_img1.png" width="325" height="179"/>
                                <p>战略管理 <strong>Strategic Management</strong></p>
                                <span>
                                管理咨询演示文案管理咨询演示文案管理咨管理咨询演
								案询演示文案管理咨询演示文案管理咨询演示文案
                                </span>
                            </div>
                          <div class="toll_info">
                            	<a href="/mgr/news.aspx?sortid=7" target="_blank">
                                	<img src="images/in_img1_ho.png" height="179" width="325" />
                                      <p>战略管理 <strong>Strategic Management</strong></p>
                               		 <span>
                               			 管理咨询演示文案管理咨询演示文案管理咨管理咨询演
										 案询演示文案管理咨询演示文案管理咨询演示文案
                               		 </span>
                                </a>
                           </div>
                        </div>
                    </li>
                    
                         <li>
                        <div class="box1">
                            <div class="toll_img">
                            	<img src="images/in_img2.png" width="325" height="179"/>
                                <p>企业改制 <strong>Corporate restructuring</strong></p>
                                <span>
                                管理咨询演示文案管理咨询演示文案管理咨管理咨询演
								案询演示文案管理咨询演示文案管理咨询演示文案
                                </span>
                            </div>
                          <div class="toll_info">
                            	<a href="/mgr/news.aspx?sortid=8" target="_blank">
                                	<img src="images/in_img2_ho.png" height="179" width="325" />
                                      <p>企业改制 <strong>Corporate restructuring</strong></p>
                               		 <span>
                               			 管理咨询演示文案管理咨询演示文案管理咨管理咨询演
										 案询演示文案管理咨询演示文案管理咨询演示文案
                               		 </span>
                                </a>
                           </div>
                        </div>
                    </li>
                    
                         <li>
                        <div class="box1">
                            <div class="toll_img">
                            	<img src="images/in_img3.png" width="325" height="179"/>
                                <p>公司治理 <strong>Corporate Governance</strong></p>
                                <span>
                                管理咨询演示文案管理咨询演示文案管理咨管理咨询演
								案询演示文案管理咨询演示文案管理咨询演示文案
                                </span>
                            </div>
                          <div class="toll_info">
                            	<a href="/mgr/news.aspx?sortid=9" target="_blank">
                                	<img src="images/in_img3_ho.png" height="179" width="325" />
                                      <p>公司治理 <strong>Corporate Governance</strong></p>
                               		 <span>
                               			 管理咨询演示文案管理咨询演示文案管理咨管理咨询演
										 案询演示文案管理咨询演示文案管理咨询演示文案
                               		 </span>
                                </a>
                           </div>
                        </div>
                    </li>
                    <!--2 排-->
                      <li>
                        <div class="box1">
                            <div class="toll_img">
                            	<img src="images/in_img4.png" width="325" height="179"/>
                                <p>组织管理 <strong>Organization and management</strong></p>
                                <span>
                                管理咨询演示文案管理咨询演示文案管理咨管理咨询演
								案询演示文案管理咨询演示文案管理咨询演示文案
                                </span>
                            </div>
                          <div class="toll_info">
                            	<a href="/mgr/news.aspx?sortid=10" target="_blank">
                                	<img src="images/in_img4_ho.png" height="179" width="325" />
                                      <p>组织管理 <strong>Organization and management</strong></p>
                               		 <span>
                               			 管理咨询演示文案管理咨询演示文案管理咨管理咨询演
										 案询演示文案管理咨询演示文案管理咨询演示文案
                               		 </span>
                                </a>
                           </div>
                        </div>
                    </li>
                    
                         <li>
                        <div class="box1">
                            <div class="toll_img">
                            	<img src="images/in_img5.png" width="325" height="179"/>
                                <p>集团管控 <strong>Group control</strong></p>
                                <span>
                                管理咨询演示文案管理咨询演示文案管理咨管理咨询演
								案询演示文案管理咨询演示文案管理咨询演示文案
                                </span>
                            </div>
                          <div class="toll_info">
                            	<a href="/mgr/news.aspx?sortid=24" target="_blank">
                                	<img src="images/in_img5_ho.png" height="179" width="325" />
                                      <p>集团管控 <strong>Group control</strong></p>
                               		 <span>
                               			 管理咨询演示文案管理咨询演示文案管理咨管理咨询演
										 案询演示文案管理咨询演示文案管理咨询演示文案
                               		 </span>
                                </a>
                           </div>
                        </div>
                    </li>
                    
                         <li>
                        <div class="box1">
                            <div class="toll_img">
                            	<img src="images/in_img6.png" width="325" height="179"/>
                                <p>人力资源管理 <strong>Human Resource Management</strong></p>
                                <span>
                                管理咨询演示文案管理咨询演示文案管理咨管理咨询演
								案询演示文案管理咨询演示文案管理咨询演示文案
                                </span>
                            </div>
                          <div class="toll_info">
                            	<a href="/mgr/news.aspx?sortid=25" target="_blank">
                                	<img src="images/in_img6_ho.png" height="179" width="325" />
                                      <p>人力资源管理 <strong>Human Resource Management</strong></p>
                               		 <span>
                               			 管理咨询演示文案管理咨询演示文案管理咨管理咨询演
										 案询演示文案管理咨询演示文案管理咨询演示文案
                               		 </span>
                                </a>
                           </div>
                        </div>
                    </li>
                    
                    <!--3排-->
                      <li>
                        <div class="box1">
                            <div class="toll_img">
                            	<img src="images/in_img7.png" width="325" height="179"/>
                                <p>制度与流程 <strong>Systems and processes</strong></p>
                                <span>
                                管理咨询演示文案管理咨询演示文案管理咨管理咨询演
								案询演示文案管理咨询演示文案管理咨询演示文案
                                </span>
                            </div>
                          <div class="toll_info">
                            	<a href="/mgr/news.aspx?sortid=26" target="_blank">
                                	<img src="images/in_img7_ho.png" height="179" width="325" />
                                      <p>制度与流程 <strong>Systems and processes</strong></p>
                               		 <span>
                               			 管理咨询演示文案管理咨询演示文案管理咨管理咨询演
										 案询演示文案管理咨询演示文案管理咨询演示文案
                               		 </span>
                                </a>
                           </div>
                        </div>
                    </li>
                    
                         <li>
                        <div class="box1">
                            <div class="toll_img">
                            	<img src="images/in_img8.png" width="325" height="179"/>
                                <p>运营管理 <strong>Operations Management</strong></p>
                                <span>
                                管理咨询演示文案管理咨询演示文案管理咨管理咨询演
								案询演示文案管理咨询演示文案管理咨询演示文案
                                </span>
                            </div>
                          <div class="toll_info">
                            	<a href="/mgr/news.aspx?sortid=27" target="_blank">
                                	<img src="images/in_img8_ho.png" height="179" width="325" />
                                      <p>运营管理 <strong>Operations Management</strong></p>
                               		 <span>
                               			 管理咨询演示文案管理咨询演示文案管理咨管理咨询演
										 案询演示文案管理咨询演示文案管理咨询演示文案
                               		 </span>
                                </a>
                           </div>
                        </div>
                    </li>
                    
                         <li>
                        <div class="box1">
                            <div class="toll_img">
                            	<img src="images/in_img9.png" width="325" height="179"/>
                                <p>企业文化 <strong>Corporate Culture</strong></p>
                                <span>
                                管理咨询演示文案管理咨询演示文案管理咨管理咨询演
								案询演示文案管理咨询演示文案管理咨询演示文案
                                </span>
                            </div>
                          <div class="toll_info">
                            	<a href="/mgr/news.aspx?sortid=28" target="_blank">
                                	<img src="images/in_img9_ho.png" height="179" width="325" />
                                      <p>企业文化 <strong>Corporate Culture</strong></p>
                               		 <span>
                               			 管理咨询演示文案管理咨询演示文案管理咨管理咨询演
										 案询演示文案管理咨询演示文案管理咨询演示文案
                               		 </span>
                                </a>
                           </div>
                        </div>
                    </li>
                </ul> 
            </div>
        <!----------in_ct1 结束---------->
	</div>
<!----------in_ct结束---------->
</div>
        </div>
    </div>
</div>
<div class="clear"></div>
<div class="home_Investment">
	<div class="home_Investment_in">
    		<div class="home_Investment_title">
            	<h2>投资咨询</h2>
                <p>Investment Advisory</p>
            </div>
            <ul class="home_Investment_list" id="home_Investment_list">
            	<li>
                 <div class="Invest_info">
                <a href="/investment/news.aspx?sortid=12">
                	<img src="images/Invest_img1.png" width="325" height="179"/>
                                <p>财务顾问 <strong>Financial Advisor</strong></p>
                                <span>
                                管理咨询演示文案管理咨询演示文案管理咨管理咨询演
								案询演示文案管理咨询演示文案管理咨询演示文案
                        </span>
                </a>
                </div>
                </li>
            	<li>
                 <div class="Invest_info">
                <a href="/investment/news.aspx?sortid=13">
                	<img src="images/Invest_img2.png" width="325" height="179"/>
                                <p>股权投资 PE / VC <strong>Equity Investment</strong></p>
                                <span>
                                管理咨询演示文案管理咨询演示文案管理咨管理咨询演
								案询演示文案管理咨询演示文案管理咨询演示文案
                        </span>
                </a>
                </div>
                </li>	<li>
                 <div class="Invest_info">
                <a href="/investment/news.aspx?sortid=14">
                	<img src="images/Invest_img3.png" width="325" height="179"/>
                                <p>市值管理 <strong>Market Value Management</strong></p>
                                <span>
                                管理咨询演示文案管理咨询演示文案管理咨管理咨询演
								案询演示文案管理咨询演示文案管理咨询演示文案
                        </span>
                </a>
                </div>
                </li>
            </ul>
            <script>
                $('#home_Investment_list li:last').css('padding-right', '0px');

            </script>
    </div>
</div>
<div class="clear"></div>
<div class="home_energy">
	<div class="home_energy_in">
    	<div class="home_energy_list">
        
      <script type="text/javascript">
          $(document).ready(function () {
              $('.boxgrid.peek').hover(function () {
                  $(".cover", this).stop().animate({ top: '231px' }, { queue: false, duration: 160 });
              }, function () {
                  $(".cover", this).stop().animate({ top: '0px' }, { queue: false, duration: 160 });
              });
          });
      </script>
        		<ul id="home_energy_list">
                <li>
                <div class="boxgrid peek">
					<a href="/news/" target="_BLANK">
                		<img src="images/energy_ho_img_1.png"/>
                  	</a>
					<a href="/news/" target="_BLANK">
                    	<img class="cover" src="images/energy_img_1.png" />
                    </a>
				</div>
                </li>
                   <li>
                <div class="boxgrid peek">
					<a href="/research/" target="_BLANK">
                		<img src="images/energy_ho_img_2.png"/>
                  	</a>
					<a href="/research/" target="_BLANK">
                    	<img class="cover" src="images/energy_img_2.png" />
                    </a>
				</div>
                </li>
                   <li>
                <div class="boxgrid peek">
					<a href="/case/" target="_BLANK">
                		<img src="images/energy_ho_img_3.png"/>
                  	</a>
					<a href="/case/" target="_BLANK">
                    	<img class="cover" src="images/energy_img_3.png" />
                    </a>
				</div>
                </li>
                </ul>
        </div>
    </div>

</div>

    <uc1:foot runat="server" ID="foot" />

</body>
</html>
