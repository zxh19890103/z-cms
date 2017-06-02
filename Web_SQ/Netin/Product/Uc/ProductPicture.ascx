<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Nt.Pages.Product.Uc.UcProductPicture" %>

<%
    int thumbID = 0;
    Nt.Pages.Product.Edit page = Page as Nt.Pages.Product.Edit;
    if (page == null)
        thumbID = (Page as Nt.Pages.Download.Edit).Model.ThumbnailID;
    else
        thumbID = page.Model.ThumbnailID;
%>

<table class="admin-table" id="table-product-pictures">
    <tr class="admin-table-header">
        <th>图片</th>
        <th>标题</th>
        <th>图片Alt</th>
        <th>生效</th>
        <th>排序</th>
        <th>设置为缩略图</th>
        <th class="th-end td-edit-del">操作</th>
    </tr>
    <%
        int i = 0;
        if (DataSource != null && DataSource.Rows.Count > 0)
        {
            foreach (System.Data.DataRow r in DataSource.Rows)
            { %>
    <tr id="nt-picture-row-<%=r["Id"] %>">
        <td>
            <img height="<%=NtPage.ThumbnailSize %>" width="120" src="<%=r["PictureUrl"] %>" alt="<%=r["SeoAlt"] %>" />
            <br />
            <a href="javascript:void(0);" onclick="uploadNew(<%=r["Id"] %>)" class="admin-ajax-upload" title="上传"></a>
            &nbsp;&nbsp;
                    <a href="javascript:void(0);" onclick="deleteThis(<%=r["Id"] %>)" class="admin-delete" title="删除"></a>
            <input type="hidden" name="Picture.Id" value="<%=r["Id"]%>" />
            <input type="hidden" name="PictureIds" value="<%=r["Id"]%>" />
            <input type="hidden" name="Picture.PictureUrl" value="<%=r["PictureUrl"]%>" />
        </td>
        <td>
            <label class="admin-label-text"><%=r["Title"]  %></label>
            <input type="text" maxlength="255" class="input-text admin-input-text" name="Picture.Title" value="<%=r["Title"]  %>" /></td>
        <td>
            <label class="admin-label-text"><%=r["SeoAlt"]  %></label>
            <input type="text" maxlength="512" class="input-text admin-input-text" name="Picture.SeoAlt" value="<%=r["SeoAlt"]  %>" /></td>
        <td style="text-align: center;">
            <label class="admin-label-bool"><%=Convert.ToBoolean(r["Display"])?"是":"否"%></label>
            <input id="ck<%=r["Id"] %>" class="input-bool admin-input-bool" type="checkbox" <%=Convert.ToBoolean(r["Display"])?"checked=\"checked\"":"" %> onchange="syncValforBoolInput(this);" />
            <input type="hidden" value="<%=r["Display"] %>" name="Picture.Display" for="ck<%=r["Id"] %>" />
        </td>
        <td style="text-align: center;">
            <label class="admin-label-int32"><%=r["DisplayOrder"]   %></label>
            <input type="text" maxlength="5" class="input-int32 admin-input-int32" onkeydown="ensureInt32(event)" name="Picture.DisplayOrder" value="<%=r["DisplayOrder"]  %>" /></td>
        <td>
            <input type="radio" name="SetThumbnail" value="<%=r["Id"] %>"
                <%if (thumbID == Convert.ToInt32(r["id"])) { Response.Write("checked=\"checked\""); } %> /></td>
        <td class="td-end">
            <a href="javascript:;" onclick="updateRow(this,<%=r["Id"]%>)" class="admin-edit" title="修改"></a>
            &nbsp;&nbsp;
                    <a href="javascript:;" class="admin-ajax-cancel" onclick="cancelUpdate();" title="取消"></a>&nbsp;&nbsp;
                    <a href="javascript:;" onclick="deleteRow(<%=r["Id"] %>);" class="admin-delete" title="删除"></a>
        </td>
    </tr>
    <%
                  i++;
            }
        }
    %>
</table>

<script type="text/javascript">

    function addNewComplete(json) {
        var m = json.model;
        var ckHtml = json.ckHtml;
        var html = [
           '<tr id="nt-picture-row-' + m.Id + '">',
           '<td>',
           '<img height="80" width="120" src="' + m.PictureUrl + '" alt="' + m.SeoAlt + '" /> <br />',
           '<a href="javascript:;" onclick="uploadNew(' + m.Id + ')" title="上传" class="admin-ajax-upload"></a>&nbsp;&nbsp;',
           '<a href="javascript:;" onclick="deleteThis(' + m.Id + ')" class="admin-delete" title="删除"></a>',
           '<input type="hidden" name="Picture.Id" value="' + m.Id + '" />',
           '<input type="hidden" name="PictureIds" value="' + m.Id + '" />',
           '<input type="hidden" name="Picture.PictureUrl" value="' + m.PictureUrl + '" />',
           '</td>',
           '<td>',
           '<label class="admin-label-text">' + m.Title + '</label>',
           '<input type="text" maxlength="255" class="input-text admin-input-text" name="Picture.Title" value="' + m.Title + '" />',
           '</td>',
            '<td>',
           '<label class="admin-label-text">' + m.SeoAlt + '</label>',
           '<input type="text" maxlength="512" class="input-text admin-input-text" name="Picture.SeoAlt" value="' + m.SeoAlt + '" />',
           '</td>',
           '<td style="text-align:center;">',
           '<label class="admin-label-bool">是</label>',
           '<input id="ck' + m.Id + '" class="input-bool admin-input-bool" type="checkbox" checked="checked" onchange="syncValforBoolInput(this);">',
           '<input type="hidden" value="True" name="Picture.Display" for="ck' + m.Id + '">',
           '</td>',
           '<td style="text-align:center;">',
           '<label class="admin-label-int32">' + m.DisplayOrder + '</label>',
           '<input type="text" maxlength="5" class="input-int32 admin-input-int32" onkeydown="ensureInt32(event)" name="Picture.DisplayOrder" value="' + m.DisplayOrder + '" />',
           '</td>',
           '<td><input type="radio" name="SetThumbnail" value="' + m.Id + '"/></td>',
           ' <td class="td-end">',
           '<a href="javascript:;" class="admin-edit" onclick="updateRow(this,' + m.Id + ')" title="修改"></a>&nbsp;&nbsp;',
           '<a href="javascript:;" class="admin-ajax-cancel" onclick="cancelUpdate();" title="取消"></a>&nbsp;&nbsp;',
           '<a href="javascript:;" onclick="deleteRow(' + m.Id + ')" class="admin-delete" title="删除"></a>',
           ' </td>'
        ].join('');
        $('#table-product-pictures').append(html);
    }

    var picMgr = new nt.pictureMgr(addNewComplete, function (row) {
        if (!/^\d{1,5}$/.test(row.find('input[name="Picture.DisplayOrder"]').val())) {
            ntAlert('排序字段必须为数字!');
            return false;
        }
        return true;
    });
</script>
