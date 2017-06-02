<%@ Page Language="C#" MasterPageFile="~/Netin/Layout.master" AutoEventWireup="true" CodeFile="hezuojigou.aspx.cs" Inherits="Netin_Common_hezuojigou" %>

<asp:Content ContentPlaceHolderID="CPH_Head" runat="server">

    <script type="text/javascript">

        $.fn.modCkBox = function () {

            this.click(function () {
                if (this.checked)
                    this.value = '1';
                else
                    this.value = '0';
            });
            return this;
        }

        /*获取当前页的url*/
        function getUrl() {
            return window.location.pathname + '?' + Math.random();
        }

        /*确保正整数*/
        function ensureInt(s) {
            if (/^[\d]{1,}$/.test(s.value)) {
                s.oldvalue = s.value;
                s.changed = true;
            }
            else {
                s.value = s.oldvalue;
            }
        }

        /*
        url:路径
        args:传到服务器的参数
        c:成功时回调函数
        err:失败时回调函数
        */
        function loadList(dholder, url, args, suc, err) {
            var url = url || window.location.pathname;
            var data = args || { method: 'fetchlist' };
            var c = c || function (t) {
                if (t === 'error') {
                    if (typeof err === 'funtion') err();
                    alert('载入列表错误!');
                }
                else {
                    $(dholder).html(t);
                    if (typeof suc === 'funtion')
                        suc();
                }
            };
            $.get(url, data, c, 'text');
        }

        /*载入列表*/
        function list() {
            $('#nt-list').load(getUrl(), { method: 'fetchlist' }, function () {
                $('input.nt-list-order', '#nt-list').each(function (i, n) {
                    n.oldvalue = n.value;
                    $(n).change(function () {
                        ensureInt(this);
                    });
                });

                $('a.nt-list-display', '#nt-list').each(function (i, n) {
                    $(n).click(function () {
                        $.post(getUrl(), { method: 'setdisplay', id: $(this).attr('data-item-id') }, function (j) {
                            if (j.error) { alert(j.msg); }
                            else {
                                $(n).text(j.yes);
                            }
                        }, 'json');
                    });
                });
            });
        }

        /*
        编辑某项
        */
        function edit(id) {
            $.getJSON(getUrl(), { id: id, method: 'getone' }, function (j) {
                for (var k in j) {
                    if (k === 'error' || k === 'msg')
                        continue;
                    var ctrl = EditForm[k];
                    if (ctrl != null && ctrl != undefined) {
                        if (ctrl.type == 'checkbox') {
                            ctrl.checked = j[k].toString().toLowerCase() === 'true';
                            ctrl.value = ctrl.checked ? 1 : 0;
                        }
                        else
                            ctrl.value = j[k].toString();
                    }
                }

                window.location.href = '#EditForm';

            });
        }

        /*
        添加
        */
        function reset() {
            EditForm.Id.value = 0;
            EditForm.reset();
        }

        /*保存*/
        function save() {
            $('#EditForm').ajaxSubmit({
                url: getUrl(),
                data: { method: 'saveone' },
                forceSync: true,
                dataType: 'json',
                beforeSubmit: function () { },
                success: function (j) {
                    if (j.error)
                        alert(j.msg);
                    else {
                        alert(j.msg);
                        reset();
                        list();
                    }
                },
                error: function () { alert('error'); }
            });
            return false;
        }

        /*
        删除
        */
        function delOne(id) {
            if (confirm('您确定删除吗？')) {
                $.post(getUrl(), { id: id, method: 'delone' }, function (j) { alert(j.msg); list(); }, 'json');
            }
        }

        /*
        选择多个
        */
        function selectAll(sender) {
            $('input.nt-ck-id', '#nt-list').attr('checked', sender.checked);
        }

        /*获取选中的id*/
        function getSelected() {
            var ids = [];
            $('input.nt-ck-id', '#nt-list').each(function (i, n) {
                if (n.checked) ids.push(n.value);
            });
            return ids;
        }

        /*重新排序*/
        function reOrder() {
            var ids = [], orders = [];
            $('input.nt-list-order', '#nt-list').each(function (i, n) {
                if (n.changed) {
                    ids.push($(n).attr('data-item-id'));
                    orders.push(n.value);
                }
            });
            if (ids.length == 0) { alert('no choice!'); return; }
            $.post(getUrl(), { ids: ids.join(','), orders: orders.join(','), method: 'reorder' }, function (j) { alert(j.msg); list(); }, 'json');
        }

        /*删除多个*/
        function delMuti() {
            if (confirm('您确定删除吗？')) {
                var ids = getSelected();
                $.post(getUrl(), { ids: ids.join(','), method: 'delmuti' }, function (j) { alert(j.msg); list(); }, 'json');
            }
        }

        $(function () {
            $(EditForm.Display).modCkBox();
            list();
        })

    </script>


</asp:Content>

<asp:Content ContentPlaceHolderID="CPH_Body" runat="server">

    <div class="admin-main-wrap">
        <div class="admin-main">
            <div class="admin-main-title">
                合作机构管理
            </div>
            <div id="nt-list">
            </div>

            <div class="nt-edit-area">

                <form action="hezuojigou.aspx" method="post" id="EditForm" name="EditForm">
                    <table class="admin-table">

                        <tr>
                            <td class="left">标题：</td>
                            <td class="right">
                                <input name="Title" type="text" class="input-text" value="" />
                            </td>
                        </tr>

                        <tr>
                            <td class="left">图片：</td>
                            <td class="right">
                                <input name="Img" type="text" class="input-text" value="" />
                                <input name="File_Img" type="file" value="" />
                            </td>
                        </tr>

                        <tr>
                            <td class="left">链接：</td>
                            <td class="right">
                                <input name="Url" type="text" class="input-text" value="" />
                            </td>
                        </tr>

                        <tr>
                            <td class="left">排序：</td>
                            <td class="right">
                                <input name="DisplayOrder" type="text" class="input-int32" value="0" />
                            </td>
                        </tr>

                        <tr>
                            <td class="left">显示：</td>
                            <td class="right">
                                <input name="Display" type="checkbox" value="1" checked="checked" />
                            </td>
                        </tr>

                        <tr>
                            <td class="left"></td>
                            <td class="right">
                                <input name="Id" type="hidden" value="0" />
                                <input class="admin-button" value="保存" type="button" onclick="return save();" />
                                <input class="admin-button" value="重置" type="button" onclick="reset();" />
                            </td>
                        </tr>

                    </table>
                </form>
            </div>
        </div>
    </div>

</asp:Content>
