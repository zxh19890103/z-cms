<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Product.ProductFieldSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                产品参数设置
                &nbsp; &nbsp;
                <a href="javascript:;" onclick="addNew();" title="添加">添加</a>
            </div>
            <table class="admin-table" id="admin-common-table">
                <tr class="admin-table-header">
                    <td class="td-end" colspan="5" align="left">
                        <%=HtmlHelper.DropdownList(Categories, 
                        new { name="ProductCategory_Id",_class="category-drop-down",onchange="changeCategory(this.value);"})%>
                    </td>
                </tr>
                <tr class="admin-table-header">
                    <th>编号</th>
                    <th>字段名</th>
                    <th>排序</th>
                    <th>生效</th>
                    <th class="th-end td-edit-del">操作</th>
                </tr>
                <asp:Repeater ID="XRepeater" runat="server">
                    <ItemTemplate>
                        <tr id="nt-common-row-<%#Eval("Id") %>">
                            <td><%#Eval("Id") %></td>
                            <td>
                                <label class="admin-label-text"><%#Eval("Name")  %></label>
                                <input type="text" maxlength="255" class="input-text admin-input-text" name="Name" value="<%#Eval("Name")  %>" />
                            </td>
                            <td>
                                <label class="admin-label-int32"><%#Eval("DisplayOrder")  %></label>
                                <input type="text" maxlength="5" class="input-int32 admin-input-int32" onkeydown="ensureInt32(event)" name="DisplayOrder" value="<%#Eval("DisplayOrder")  %>" />
                            </td>
                            <td>
                                <label class="admin-label-bool"><%#Convert.ToBoolean(Eval("Display"))?"是":"否"%></label>
                                <input id='ck<%#Eval("Id") %>' class="input-bool admin-input-bool" type="checkbox" <%#Convert.ToBoolean(Eval("Display"))?"checked=\"checked\"":"" %> onchange="syncValforBoolInput(this);" />
                                <input type="hidden" value="<%#Eval("Display") %>" name="Display" for="ck<%#Eval("Id") %>" />
                            </td>
                            <td class="td-end">
                                <a href="javascript:;" onclick="<%#(Convert.ToInt32(Eval("ProductCategory_Id"))==ProductCategoryId)?"updateRow(this,"+Eval("Id")+");":"ntAlert('当前修改操作无效.');" %>" class="admin-edit" title="修改"></a>
                                <a href="javascript:;" class="admin-ajax-cancel" onclick="cancelUpdate();" title="取消"></a>
                                <a href="javascript:;" onclick="deleteRow(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        function changeCategory(id){
            window.location.href=window.location.pathname+'?ProductCategory_Id='+id;
        }

        function addNewComplete(json) {
            var m = json.model;
            var ckHtml = json.ckHtml;
            var html = [
               '<tr id="nt-common-row-' + m.Id + '">',
               '<td>' + m.Id + '</td>',
               '<td>',
               '<label class="admin-label-text">' + m.Name + '</label>',
               '<input type="text" maxlength="255" class="input-text admin-input-text" name="Name" value="' + m.Name + '" />',
               '</td>',
               '<td>',
               '<label class="admin-label-int32">' + m.DisplayOrder + '</label>',
               '<input type="text" maxlength="5" class="input-int32 admin-input-int32" onkeydown="ensureInt32(event)" name="DisplayOrder" value="' + m.DisplayOrder + '" />',
                '</td><td>',
                '<label class="admin-label-bool">是</label>',
           '<input id="ck' + m.Id + '" class="input-bool admin-input-bool" type="checkbox" checked="checked" onchange="syncValforBoolInput(this);">',
           '<input type="hidden" value="True" name="Display" for="ck' + m.Id + '">',
                '</td><td class="td-end">',
                '<a href="javascript:;" onclick="updateRow(this,' + m.Id + ')" class="admin-edit" title="修改"></a>&nbsp;&nbsp;',
                '<a href="javascript:;" class="admin-ajax-cancel" onclick="cancelUpdate();" title="取消"></a>&nbsp;&nbsp;',
                '<a href="javascript:;" onclick="deleteRow(' + m.Id + ')" class="admin-delete" title="删除"></a></td></tr>'
            ].join('');
            $('#admin-common-table').append(html);
        }

        function updateConfig(data) {
            var row = $('#nt-common-row-' + data.Id);
            data.DisplayOrder = row.find('input[name="DisplayOrder"]').val();
            data.Display = row.find('input[name="Display"]').val();
            data.Name = row.find('input[name="Name"]').val();
            data.Name=data.Name.replace("'","‘");//replace all ' in english with ’ in chinese
            data.ProductCategory_Id=<%=ProductCategoryId%>;
            return data;
        }

        function insertConfig(data) {
            data.ProductCategory_Id=<%=ProductCategoryId%>;
            return data;
        }

        /*ajaxUrl, addNewComplete, validate, updateConfig*/
        var fieldsMgr = new nt.commonAjaxMgr("/Netin/Handlers/ProductFieldHandler.ashx", addNewComplete, function (row) {
            if (!/^\d{1,5}$/.test(row.find('input[name="DisplayOrder"]').val().trim())) {
                ntAlert('排序字段必须为数字!');
                return false;
            }
            return true;
        }, updateConfig,insertConfig);
    </script>
</asp:Content>

