<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Seo.WebsiteLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
    <script type="text/javascript">

        var nt = nt || {};
        nt.currentRow = 0;

        function addNew() {
            nt.currentRow++;
            var c='';
            if(nt.currentRow%2==0)
                c='tr-even';
            else
                c='tr-odd';

            var html=[
            '<tr class="'+c+'" id="admin-table-row-'+(nt.currentRow)+'">',
            '<td><input type="text" class="input-text" value="" name="Word" /></td>',
            '<td><input type="text" class="input-text" value="" name="Url" /></td>',
            '<td class="td-end">',
            '<a href="javascript:;" onclick="deleteRow('+nt.currentRow+');">删除</a>',
             '&nbsp;&nbsp;',
            '<a href="javascript:;" onclick="ntAlert(\'您还没有保存该项\');">应用链接</a>',
            '&nbsp;&nbsp;',
            '<a href="javascript:;" onclick="ntAlert(\'您还没有保存该项\');">取消链接</a>',
            '</td></tr>'
            ].join('');
            $('#admin-table-weblink').append(html);
        }
        s
        function deleteRow(id) {
            ntConfirm('您确定在不取消链接的情况下删除该项?',function(){
                $('#admin-table-row-'+id).remove();
                nt.currentRow--;
            });
        }

        function addAllLinks(){
            showLoading();
            ntAjax({
                method:"HandleWebsiteLinks",
                data:'{"way":"add-2014"}',
                success:function(msg){
                    var json = $.parseJSON(msg.d);
                    ntAlert(json.message,function(){
                        removeLoading();
                    });
                }
            });
        }

        function cancelAllLinks(){
            showLoading();
            ntAjax({
                method:"HandleWebsiteLinks",
                data:'{"way":"cancel-2014"}',
                success:function(msg){
                    var json = $.parseJSON(msg.d);
                    ntAlert(json.message,function(){
                        removeLoading();
                    });
                }
            });
        }

        function addLink(id){
            showLoading();
            ntAjax({
                method:"HandleWebsiteLinks",
                data:'{"way":"add-'+id+'"}',
                success:function(msg){
                    var json = $.parseJSON(msg.d);
                    ntAlert(json.message,function(){
                        removeLoading();
                    });
                }
            });
        }

        function cancelLink(id){
            showLoading();
            ntAjax({
                method:"HandleWebsiteLinks",
                data:'{"way":"cancel-'+id+'"}',
                success:function(msg){
                    var json = $.parseJSON(msg.d);
                    ntAlert(json.message,function(){
                        removeLoading();
                    });
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-tips">
            1.添加完站内链接关键词之后，请首先点击保存按钮以保存您添加的关键词<br />
            2.保存成功之后，您可以点击添加链接按钮，这样你的新闻或产品内容里所有的关键词将会附加指定的链接
        </div>
        <div class="admin-main">
            <div class="admin-main-title">
                站内链接
                <a href="javascript:;" onclick="addNew();" title="添加">添加</a>
            </div>
            <form action="<%=LocalUrl %>" method="post" id="EditForm" name="EditForm">
                <table class="admin-table" id="admin-table-weblink">
                    <tr class="admin-table-header">
                        <th style="width: 40%">链接词</th>
                        <th style="width: 40%">链接</th>
                        <th class="th-end">删除</th>
                    </tr>
                    <%
                        int i = 0;
                        int j = 0;
                        foreach (var item in DataSource)
                        {
                            if (i % 2 == 0)
                            {
                    %>
                    <tr id="admin-table-row-<%=i%>" class="tr-even">
                        <td>
                            <input type="text" class="input-text" value="<%=item.Word%>" name="Word" /></td>
                        <td>
                            <input type="text" class="input-text" value="<%=item.Url%>" name="Url" /></td>
                        <td class="td-end">
                            <a href="javascript:;" onclick="deleteRow(<%=i%>);">删除</a>
                            &nbsp;&nbsp;
                            <a href="javascript:;" onclick="addLink(<%=i%>);">应用链接</a>
                            &nbsp;&nbsp;
                            <a href="javascript:;" onclick="cancelLink(<%=i%>);">取消链接</a>
                        </td>
                    </tr>
                    <%
                            }
                            else
                            {
                    %>
                    <tr id="admin-table-row-<%=j%>" class="tr-odd">
                        <td>
                            <input type="text" class="input-text" value="<%=item.Word%>" name="Word" /></td>
                        <td>
                            <input type="text" class="input-text" value="<%=item.Url%>" name="Url" /></td>
                        <td class="td-end">
                            <a href="javascript:;" onclick="deleteRow(<%=i%>);">删除</a>
                            &nbsp;&nbsp;
                            <a href="javascript:;" onclick="addLink(<%=i%>);">应用链接</a>
                            &nbsp;&nbsp;
                            <a href="javascript:;" onclick="cancelLink(<%=i%>);">取消链接</a>
                        </td>
                    </tr>
                    <%
                            }
                            i++;
                            j++;
                        } %>
                </table>

                <div class="admin-save">
                    <input type="submit" class="admin-button" value="保存" />
                    <input type="reset" class="admin-button" value="重置" />
                </div>
                <div class="admin-save">
                    <a href="javascript:;" onclick="addAllLinks();">应用所有链接</a>
                    &nbsp;&nbsp;
                    <a href="javascript:;" onclick="cancelAllLinks();">取消所有链接</a>
                </div>
            </form>
            <script type="text/javascript">
                nt.currentRow=<%=i-1%>;
            </script>
        </div>
    </div>
</asp:Content>

