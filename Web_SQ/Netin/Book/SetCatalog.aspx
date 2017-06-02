<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Book.CatalogSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">

    <script type="text/javascript">

        var nt = nt || {};
        nt.currentRow = 0;

        function addNew() {
            nt.currentRow++;
            var c = '';
            if (nt.currentRow % 2 == 0)
                c = 'tr-even';
            else
                c = 'tr-odd';

            var html = [
            '<tr class="' + c + '" id="admin-table-row-' + (nt.currentRow) + '">',
            '<td><input type="text" class="input-int32" maxlength="5"  onkeydown="ensureInt32(event);"  value="'+nt.currentRow+'" name="TypeId" /></td>',
            '<td><input type="text" class="input-text" value="" name="Name" /></td>',
            '<td class="td-end"><a href="javascript:;" onclick="deleteRow(' + nt.currentRow + ');" class="admin-delete" title="删除"></a></td></tr>'
            ].join('');

            $('#admin-table-weblink').append(html);
        }

        function deleteRow(id) {
            $('#admin-table-row-' + id).remove();
            nt.currentRow--;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">

    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                预订\留言类别
                <a href="javascript:;" onclick="addNew();" title="添加">添加</a>
            </div>
            <form action="<%=LocalUrl %>" method="post" id="EditForm" name="EditForm">
                <table class="admin-table" id="admin-table-weblink">
                    <tr class="admin-table-header">
                        <th style="width: 40%">分类ID</th>
                        <th style="width: 40%">名称</th>
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
                            <input type="text" class="input-int32" maxlength="5"  value="<%=item.Id%>" name="TypeId" /></td>
                        <td>
                            <input type="text" class="input-text" value="<%=item.Name%>" name="Name" /></td>
                        <td class="td-end">
                            <a href="javascript:;" onclick="deleteRow(<%=i%>);" class="admin-delete" title="删除"></a></td>
                    </tr>
                    <%
                            }
                            else
                            {
                    %>
                    <tr id="admin-table-row-<%=j%>" class="tr-odd">
                        <td>
                            <input type="text" class="input-int32" maxlength="5"  value="<%=item.Id%>" name="TypeId" /></td>
                        <td>
                            <input type="text" class="input-text" value="<%=item.Name%>" name="Name" /></td>
                        <td class="td-end">
                            <a href="javascript:;" onclick="deleteRow(<%=i%>);" class="admin-delete" title="删除"></a></td>
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
            </form>
            <script type="text/javascript">
                nt.currentRow=<%=i-1%>;
            </script>
        </div>
    </div>

</asp:Content>

