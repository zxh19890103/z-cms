/// <reference path="jquery-1.7.2.min.js" />

var nt = nt || {};
nt.upload = upload;
nt.confirm_ok_callback = null;
nt.confirm_cancel_callback = null;
nt.alert_ok_callback = null;
//'auto','yes','no'
nt.useParentContext = 'auto';

window.onresize = function () {
    if (useParentContextOrNot()) {

    } else {
        if ($('#mask').css('display') == 'block') {
            showMask();
        }
        if ($('#nt-alert-box').css('display') == 'block')
            centerizeElement('#nt-alert-box');
        if ($('#nt-confirm-box').css('display') == 'block')
            centerizeElement('#nt-confirm-box');
        if ($('#nt-dialog-edit') && $('#nt-dialog-edit').css('display') == 'block')
            centerizeElement('#nt-dialog-edit');
    }
}

/*将弹出窗口置于屏幕的中心位置*/
function centerizeElement(selector) {
    var top = 0;
    var left = 0;
    var width = $(selector).width();
    var height = $(selector).height();
    top = (document.documentElement.clientHeight - height) / 2;
    left = (document.documentElement.clientWidth - width) / 2;
    $(selector).css({ 'top': top, 'left': left });
}

/*警告窗*/
function ntAlert(msg, fn) {
    if (((nt.useParentContext == 'auto') && (parent != this))
        || (nt.useParentContext == 'yes')) {
        parent.ntAlert(msg, fn);
    }
    else {
        $('#nt-alert-content').text(msg);
        showMask();
        centerizeElement('#nt-alert-box');
        $('#nt-alert-box').show();
        nt.alert_ok_callback = fn;
    }
}

/*when close the alert window*/
function closeAlertBox() {
    $('#nt-alert-box').hide();
    if (($('#nt-dialog-edit') && $('#nt-dialog-edit').css('display') == 'block')
        || $('#nt-upload-box') && $('#nt-upload-box').css('display') == 'block')
    { } else { removeMask(); }

    if (nt.alert_ok_callback)
        nt.alert_ok_callback();
    nt.alert_ok_callback = null;
}

function ntConfirm(msg, fn1, fn2) {
    if (useParentContextOrNot()) {
        parent.ntConfirm(msg, fn1, fn2);
    }
    else {
        $('#nt-confirm-content').text(msg);
        showMask();
        centerizeElement('#nt-confirm-box');
        $('#nt-confirm-box').show();
        nt.confirm_ok_callback = fn1;
        nt.confirm_cancel_callback = fn2;
    }
}

//triggered while ok button clicked on alert box
function ntOk() {
    removeMask();
    $('#nt-confirm-box').hide();
    if (nt.confirm_ok_callback)
        nt.confirm_ok_callback();
    nt.confirm_ok_callback = null;
}

//triggered while cancel button clicked on confirm box
function ntCancel() {
    removeMask();
    $('#nt-confirm-box').hide();
    if (nt.confirm_cancel_callback)
        nt.confirm_cancel_callback();
    nt.confirm_cancel_callback = null;
}
///ini function to upload
function upload() {
    ntAlert('请提供upload方法!');
}

$(function () {

    /**
    upload box writing begin
    */
    var uploadDialogHtml = '';

    var browserInfo = getBrowserInfo();

    if (browserInfo.ie || browserInfo.chrome || browserInfo.safari) {
        /*draw upload dialog html if ie
        if the current browser is ie under 8.0,uploading file will works only while file input button's click event fired,
        or uploading will be refused;
        */
        uploadDialogHtml = [
            '<div id="nt-upload-box" class="nt-upload-box" style="display:none;">',
            '<div class="nt-upload-box-title">上传图片</div>',
            '<form id="nt-upload-form" name="nt-upload-form" method="post" enctype="multipart/form-data">',
            '<input type="file" id="ntImgFile" name="imgFile" dir="rtl" /><br /><br />',
            '<input type="button" class="admin-button" value="确定" onclick="nt.upload();" />&nbsp;',
            '<input type="button" class="admin-button" value="取消" onclick="closeUploadDialog()" />',
            '</form></div>'
        ].join('');
    } else {
        /*draw upload dialog html if not ie*/
        uploadDialogHtml = [
           '<div id="nt-upload-box" class="nt-upload-box" style="display:none;">',
           '<div class="nt-upload-box-title">上传图片</div>',
           '<form id="nt-upload-form" name="nt-upload-form" method="post" enctype="multipart/form-data">',
           '<input class="upload-button" type="button" name="imgFile" onclick="ntImgFile.click();" value="浏览" />&nbsp;&nbsp;',
           '<input type="text" class="input-text" id="fileUrl"  />',
           '<input class="file-button" type="file" id="ntImgFile" name="imgFile" onchange="fileUrl.value=this.value;"  /><br /><br />',
           '<input type="button" class="admin-button" value="确定" onclick="nt.upload();" />&nbsp;',
           '<input type="button" class="admin-button" value="取消" onclick="closeUploadDialog()" />',
           '</form></div>'
        ].join('');
    }

    /*append upload dialog html to body*/
    $(document.body).append(uploadDialogHtml);

    /*end upload box writing*/

    /*提示框*/
    var ntAlertHtml = [
        '<table id="nt-alert-box" class="nt-alert-box" style="display:none;">',
        '<tr class="top"><td class="top-left"></td><td class="nt-alert-box-title">系统提示</td><td class="top-right"></td></tr>',
        '<tr class="body"><td class="body-left"></td><td class="body-middle"><div id="nt-alert-content" class="nt-alert-content"></div>',
        '<div class="nt-alert-button"><input type="button" class="admin-button" value="确定" onclick="closeAlertBox();"/></div></td>',
        '<td class="body-right"></td></tr>',
        '<tr class="foot"><td class="foot-left"></td><td class="nt-alert-box-foot"></td><td class="foot-right"></td></tr>',
        '</table>'
    ].join('');

    /*append alert dialog html to body*/
    $(document.body).append(ntAlertHtml);

    /*确认框*/
    var ntConfirmHtml = [
        '<table id="nt-confirm-box" class="nt-confirm-box" style="display:none;">',
        '<tr class="top"><td class="top-left"></td><td class="nt-confirm-box-title">系统提示</td><td class="top-right"></td></tr>',
        '<tr class="body"><td class="body-left"></td><td class="body-middle"><div id="nt-confirm-content" class="nt-confirm-content"></div>',
        '<div class="nt-confirm-button"><input type="button" class="admin-button" value="确定" onclick="ntOk();"/>',
        '&nbsp;&nbsp;',
        '<input type="button" class="admin-button" value="取消" onclick="ntCancel()"/></div></td>',
        '<td class="body-right"></td></tr>',
        '<tr class="foot"><td class="foot-left"></td><td class="nt-confirm-box-foot"></td><td class="foot-right"></td></tr>',
        '</table>'
    ].join('');

    /*append confirm dialog html to body*/
    $(document.body).append(ntConfirmHtml);

    /*mask*/
    var maskBox = '<div id="mask" class="nt-mask" style="display:none;"></div>';
    $(document.body).append(maskBox);

    /*loading animation*/
    var loadingBox = '<div id="nt-loading" class="nt-loading" style="display:none;"><img src="/Netin/Content/Images/loading-2.gif" alt="正在执行操作,请稍等..."/></div>';
    $(document.body).append(loadingBox);
})

/*
options:method,data,success
*/
function ntAjax(options) {
    var filepath = window.location.pathname;
    $.ajax({
        type: "POST",
        url: filepath + "/" + options.method,
        contentType: "application/json",
        data: options.data,
        async: false,
        dataType: "json",
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ntAlert("操作出错!" + textStatus);
            removeLoading();
        },
        success: options.success,
        complete: function (XMLHttpRequest, textStatus) {
        }
    });
}

/*Json解析 used unusually*/
function ntJson(text) {
    var match;
    if ((match = /\{[\s\S]*\}|\[[\s\S]*\]/.exec(text))) {
        text = match[0];
    }
    var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g;
    cx.lastIndex = 0;
    if (cx.test(text)) {
        text = text.replace(cx, function (a) {
            return '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
        });
    }
    if (/^[\],:{}\s]*$/.
	test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@').
	replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').
	replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) {
        return eval('(' + text + ')');
    }
    throw 'JSON parse error';
}

/*退出*/
function logout() {
    ntAjax({
        method: 'Logout',
        data: '{}',
        success: function () {
        }
    })
    top.location.href = '/Netin/Login.aspx';
}

///refresh the mainFrame
function refresh() {
    parent.mainFrame.location.href = parent.mainFrame.location.href;
}

/*get the information of current browser*/
function getBrowserInfo() {
    var Sys = {};
    var ua = navigator.userAgent.toLowerCase();
    var s;
    (s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
    (s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
    (s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
    (s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
    (s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;
    return Sys;
}

/*mask loading begin*/
function showMask() {
    if (useParentContextOrNot()) {
        parent.showMask();
    }
    else {
        $('#mask').css('height', document.documentElement.scrollHeight);
        $('#mask').css('width', document.documentElement.scrollWidth);
        $('#mask').show();
        $('#mask').stop().animate({
            opacity: 0.7
        }, 80);
    }
}

/*remove this element*/
function removeMask() {
    if (useParentContextOrNot()) {
        parent.removeMask();
    }
    else {
        $('#mask').stop().animate({ opacity: 0 }, 40, '', function () {
            $(this).hide();
        });
    }
}

//show loading animation
function showLoading() {
    if (useParentContextOrNot()) {
        parent.showLoading();
    }
    else {
        centerizeElement('#nt-loading');
        $('#nt-loading').show();
        showMask();
    }
}

//remove loading animation
function removeLoading() {
    if (useParentContextOrNot()) {
        parent.removeLoading();
    }
    else {
        $('#nt-loading').hide();
        removeMask();
    }
}
/*mask loading end*/

//close the upload dialog
function closeUploadDialog() {
    if (useParentContextOrNot()) {
        parent.closeUploadDialog();
    } else {
        $('#nt-upload-box').hide();
        $('#nt-upload-box').find('input[type="file"]').val('');
        removeMask();
    }
}

/*open upload dialog*/
function openUploadDialog(funcUpload, title) {
    if (useParentContextOrNot()) {
        parent.openUploadDialog(funcUpload, title);
    }
    else {
        $('#nt-upload-box').find('.nt-upload-box-title').text(title);
        nt.upload = funcUpload;
        centerizeElement('#nt-upload-box');
        $('#nt-upload-box').show();
        showMask();
    }
}

/*open window*/
function openWindow(url, title) {
    window.open(url, title, 'height=500, width=800, top=0, left=0, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');
}
/*a value that indicate whether the current window context is a parent one*/
function useParentContextOrNot() {
    return ((nt.useParentContext == 'auto') && (parent != this))
        || (nt.useParentContext == 'yes');
}