<%@ Control Language="C#" AutoEventWireup="true" 
     Inherits="Nt.Pages.Product.Uc.UcProductField" %>
<table class="admin-table" id="table-product-fields">
    <tr class="admin-table-header">
        <th>参数名</th>
        <th class="th-end">值</th>
    </tr>
    <%
        if (DataSource != null && DataSource.Rows.Count > 0)
        {
            int i = 0;
            foreach (System.Data.DataRow item in DataSource.Rows)
            {%>
    <tr class="<%=(i%2==0)?"tr-even":"tr-odd"%> table-data-row">
        <td>
            <label><%=item["Name"] %></label>
            <input type="hidden" name="ProductField_Id" value="<%=item["field_Id"] %>" />
        </td>
        <td class="td-end">
            <input type="text" class="input-text" maxlength="255" name="FieldValue<%=item["field_Id"] %>" value="<%=item["Value"] %>" />
        </td>
    </tr>
    <%
                i++;
            }
        }
        else
        {%>
    <tr class="table-data-row">
        <td colspan="2" class="td-end">无增添参数
        </td>
    </tr>
    <%} %>
</table>
<script type="text/javascript">
    function changeCategory(id) {
        if (!ntfns) {
            ntAlert('缺少数据!');
            return;
        }
        var target = null;
        for (var o in ntfns) {
            if (ntfns[o].c == id) {
                target = ntfns[o];
                break;
            }
        }
        $('#table-product-fields').find('tr.table-data-row').remove();//remove old
        //add new
        var html = '';
        for (var i = 0; i < target.fc; i++) {
            var style = i % 2 == 0 ? 'tr-even' : 'tr-odd';
            html += '<tr class="' + style + ' table-data-row"><td>';
            html += '<label>' + target.fns[i] + '</label>';
            html += '<input type="hidden" name="ProductField_Id" value="' + target.fids[i] + '" />';
            html += '</td>';
            html += '<td class="td-end">';
            html += '<input type="text" class="input-text" maxlength="255" name="FieldValue' + target.fids[i] + '" value="" />';
            html += '</td></tr>';
            $('#table-product-fields').append(html);
            html = '';
        }

    }
</script>
