/// <reference path="jquery-1.6.4.min.js" />
/// <reference path="admin.common.js" />

$(function () {
    $('.admin-table .tr-even,.tr-odd').hover(function () {
        $(this).css('background-color', '#F6E389');
    },
    function () {
        $(this).css('background-color', '');
    })
})

/*type=>0:settop,1:recommend,2:display,3:hot,4:delete*/
function delSelected(msg) {
    var str_ids = getSelectedIds();
    if (str_ids == "") {
        ntAlert("没有选中任何项.");
        return;
    }
    msg = msg || "您确定删除所选吗?";
    ntConfirm(msg, function () {
        ntAjax({
            method: 'BatchDelete',
            data: '{"ids":"' + str_ids + '"}',
            success: function (msg) {
                var json = $.parseJSON(msg.d);
                ntAlert(json.message, function () {
                    if (json.error == 0)
                        window.location.reload();
                });
            }
        });
    });
}

function del(id, msg) {
    msg = msg || "您确定删除吗?";
    ntConfirm(msg,
        function () {
            ntAjax({
                method: 'BatchDelete',
                data: '{"ids":"' + id + '"}',
                success: function (msg) {
                    var json = $.parseJSON(msg.d);
                    ntAlert(json.message, function () {
                        if (json.error == 0)
                            this.window.location.reload();
                    });
                }
            });
        });
}


function selectall(sender) {
    var b = $(sender).attr('checked');
    if (b)
        $("input.ck-item").attr("checked", true);
    else
        $("input.ck-item").attr("checked", false);
}


/*type=>0:settop,1:recommend,2:display,3:hot,4:active,5published*/
function baseSet(sender, type) {
    var id = $(sender).attr('itemid');
    ntAjax({
        method: 'Batch',
        data: '{"ids":"' + id + '","type":"' + type + '"}',
        success: function (msg) {
            var json = $.parseJSON(msg.d);
            ntAlert(json.message, function () {
                var current = $(sender).attr('current');
                $(sender).text($(sender).attr('stringResources').split('|')[current]);
                $(sender).attr('current', 1 - current);
                if (current == 1)
                    $(sender).css('color', 'red');
                else
                    $(sender).css('color', '');
            });
        }
    });
}
//置顶
function setTop(sender) {
    baseSet(sender, 0);
}
//推荐
function recommend(sender) {
    baseSet(sender, 1);
}
//显示
function display(sender) {
    baseSet(sender, 2);
}
//热点
function hot(sender) {
    baseSet(sender, 3);
}
//启用
function active(sender) {
    baseSet(sender, 4);
}
//发布
function publish(sender) {
    baseSet(sender, 5);
}
//重排序
function reOrder() {
    var ids = '';
    var orders = '';
    $('input[name="order-item"]')
        .each(function () {
            if ($(this).data('input.state') == 1) {
                if (ids == '')
                    ids = $(this).attr('itemid');
                else
                    ids += ',' + $(this).attr('itemid');
                if (orders == '')
                    orders = $(this).val();
                else
                    orders += ',' + $(this).val();
            }
        })
    if (ids == '') {
        ntAlert('没有任何修改');
        return;
    }
    ntAjax({
        method: 'ReOrder',
        data: '{"ids":"' + ids + '","orders":"' + orders + '"}',
        success: function (msg) {
            var json = $.parseJSON(msg.d);
            ntAlert(json.message, function () {
                acceptAllChanges();
            });
        }
    })
}


function getSelectedIds() {
    var str_ids = "";
    $("input.ck-item").each(function (evt) {
        if ($(this).attr("checked")) {
            if (str_ids == "")
                str_ids += $(this).val();
            else
                str_ids += "," + $(this).val();
        }
    })
    return str_ids;
}

//翻页
function goto(page) {
    if (page == -2147483648)
        return;
    var thisForm = document.forms.namedItem("PagerForm");
    thisForm.Page.value = page;
    thisForm.submit();
}

//清楚搜索条件
function clearSearch() {
    var filepath = window.location.pathname;
    window.location.href = filepath;
}

//将新闻或产品批量转移至指定分类
function batchMigrate() {
    var str_ids = getSelectedIds();
    var to = "";
    if ($("select[name='ToCategoryId']").length > 0)
        to = $("select[name='ToCategoryId']").val();
    else {
        ntAlert("转移失败");
        return;
    }
    if (str_ids == "") {
        ntAlert("没有选中任何项");
        return;
    }
    ntAjax({
        method: 'BatchMigrate',
        data: '{"ids":"' + str_ids + '","to":"' + to + '"}',
        success: function (msg) {
            var json = $.parseJSON(msg.d);
            ntAlert(json.message, function () {
                this.window.location.reload();
            });
        }
    })
}