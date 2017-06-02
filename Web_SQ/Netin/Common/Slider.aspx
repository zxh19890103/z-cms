<%@ Page Title="" Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true"
    Inherits="Nt.Pages.Common.Slider" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPH_Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_Body" runat="Server">
    <div class="admin-main-wrap">
        <div class="admin-tips">
            1.图片的标题和Alt有助于推广，请认真填写<br />
            2.请保证图片链接地址的完整性，如http://www.naite.com.cn<br />
            3.请保证您所上传的图片的尺寸大体符合指定比例<br />
            4.如果您上传的图片严重不合比例，前台的图片将无法正确显示<br />
            5.如要修改某张图片的相关属性，请点击该图片所在行最右侧的编辑按钮，修改完成后请点击保存按钮以保存您的修改<br />
            6.最后请务必点击最下面的确定按钮
        </div>
        <div class="admin-main">
            <div class="admin-main-title">
                Slider管理
            &nbsp;
            &nbsp;
            <a href="javascript:;" onclick="addNew()" title="添加">添加</a>
            </div>
            <form action="<%=LocalUrl %>" method="post">
                <table class="admin-table" id="table-product-pictures">
                    <tr class="admin-table-header">
                        <th>图片</th>
                        <th>标题</th>
                        <th>链接</th>
                        <th>图片Alt</th>
                        <th>生效</th>
                        <th>排序</th>
                        <th class="th-end td-edit-del">操作</th>
                    </tr>
                    <asp:Repeater ID="XRepeater" runat="server">
                        <ItemTemplate>
                            <tr id='nt-picture-row-<%#Eval("Id") %>'>
                                <td>
                                    <img width="100" height="<%=ThumbnailSize %>" src="<%#Eval("PictureUrl") %>" alt="<%#Eval("SeoAlt") %>" />
                                    <br />
                                    <a href="javascript:;" onclick="uploadNew(<%#Eval("Id") %>)" class="admin-ajax-upload" title="上传"></a>
                                    &nbsp;&nbsp;
                    <a href="javascript:;" onclick="deleteThis(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                    <input type="hidden" name="Picture.Id" value="<%#Eval("Id")%>" />
                                    <input type="hidden" name="Picture.PictureUrl" value="<%#Eval("PictureUrl")%>" />
                                </td>
                                <td>
                                    <label class="admin-label-text"><%#Eval("Title")  %></label>
                                    <input type="text" maxlength="255" class="input-text admin-input-text" name="Picture.Title" value="<%#Eval("Title")  %>" /></td>
                                <td>
                                    <label class="admin-label-text"><%#Eval("Text")  %></label>
                                    <input type="text" maxlength="512" class="input-text admin-input-text" name="Picture.Text" value="<%#Eval("Text")  %>" /></td>
                                <td>
                                    <label class="admin-label-text"><%#Eval("SeoAlt")  %></label>
                                    <input type="text" maxlength="512" class="input-text admin-input-text" name="Picture.SeoAlt" value="<%#Eval("SeoAlt")  %>" /></td>
                                <td>
                                    <label class="admin-label-bool"><%#Convert.ToBoolean(Eval("Display"))?"是":"否"%></label>
                                    <input id='ck<%#Eval("Id") %>' class="input-bool admin-input-bool" type="checkbox" <%#Convert.ToBoolean(Eval("Display"))?"checked=\"checked\"":"" %> onchange="syncValforBoolInput(this);" />
                                    <input type="hidden" value="<%#Eval("Display") %>" name="Picture.Display" for="ck<%#Eval("Id") %>" />
                                </td>
                                <td>
                                    <label class="admin-label-int32"><%#Eval("DisplayOrder")  %></label>
                                    <input type="text" maxlength="5" class="input-int32 admin-input-int32" onkeydown="ensureInt32(event)" name="Picture.DisplayOrder" value="<%#Eval("DisplayOrder")  %>" />
                                </td>
                                <td class="td-end">
                                    <a href="javascript:;" onclick="updateRow(this,<%#Eval("Id")%>)" class="admin-edit" title="修改"></a>
                                    &nbsp;&nbsp;
                    <a href="javascript:;" class="admin-ajax-cancel" onclick="cancelUpdate();" title="取消"></a>&nbsp;&nbsp;
                    <a href="javascript:;" onclick="deleteRow(<%#Eval("Id") %>)" class="admin-delete" title="删除"></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div class="admin-edit-save">
                    <input type="submit" class="admin-button" value="保存" />
                </div>
            </form>
        </div>
    </div>
    <script type="text/javascript">
        function addNewComplete(json) {
            var m = json.model;
            var ckHtml = json.ckHtml;
            var html = [
               '<tr id="nt-picture-row-' + m.Id + '">',
               '<td>',
               '<img width="100" height="<%=ThumbnailSize %>"  src="' + m.PictureUrl + '" alt="' + m.SeoAlt + '" /> <br />',
               '<a href="javascript:;" onclick="uploadNew(' + m.Id + ')" class="admin-ajax-upload" title="上传"></a>&nbsp;&nbsp;',
               '<a href="javascript:;" onclick="deleteThis(' + m.Id + ')" class="admin-delete" title="删除"></a>',
               '<input type="hidden" name="Picture.Id" value="' + m.Id + '" />',
               '<input type="hidden" name="Picture.PictureUrl" value="' + m.PictureUrl + '" />',
               '</td>',
               '<td>',
               '<label class="admin-label-text">' + m.Title + '</label>',
               '<input type="text" maxlength="255" class="input-text admin-input-text" name="Picture.Title" value="' + m.Title + '" />',
               '</td>',
               '<td>',
               '<label class="admin-label-text">' + m.Text + '</label>',
               '<input type="text" maxlength="512" class="input-text admin-input-text" name="Picture.Text" value="' + m.Text + '" /></td>',
               '<td>',
               '<label class="admin-label-text">' + m.SeoAlt + '</label>',
               '<input type="text" maxlength="512" class="input-text admin-input-text" name="Picture.SeoAlt" value="' + m.SeoAlt + '" />',
               '</td>',
              '<td>',
               '<label class="admin-label-bool">是</label>',
           '<input id="ck' + m.Id + '" class="input-bool admin-input-bool" type="checkbox" checked="checked" onchange="syncValforBoolInput(this);">',
           '<input type="hidden" value="True" name="Picture.Display" for="ck' + m.Id + '">',
               '</td>',
               '<td>',
                '<label class="admin-label-int32">' + m.DisplayOrder + '</label>',
           '<input type="text" maxlength="5" class="input-int32 admin-input-int32" onkeydown="ensureInt32(event)" name="Picture.DisplayOrder" value="' + m.DisplayOrder + '" />',
               '</td>',
               ' <td class="td-end">',
               '<a href="javascript:;" onclick="updateRow(this,' + m.Id + ')" class="admin-edit" title="修改"></a>&nbsp;&nbsp;',
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
</asp:Content>

