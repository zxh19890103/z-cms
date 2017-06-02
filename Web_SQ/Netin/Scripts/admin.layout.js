/// <reference path="jquery-1.7.2.min.js" />
$(document).ready(function () {
    $('#layout_middle_bar>img').click(function () {
        if ($(this).data('hidden')) {
            $('#layout_menu').show(150);
            $('#layout_content').css('width', '84.5%');
            $(this).data('hidden', 0);
            $('#hide-btn').attr('src', '/Netin/Content/Images/hide_btn.jpg')
        } else {
            $('#layout_menu').hide(100);
            $('#layout_content').css('width', '99.5%');
            $(this).data('hidden', 1);
            $('#hide-btn').attr('src', '/Netin/Content/Images/show_btn.jpg')
        }
    })


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


    var maskBox = '<div id="mask" class="nt-mask" style="display:none;"></div>';
    $(document.body).append(maskBox);

    var loadingBox = '<div id="nt-loading" class="nt-loading" style="display:none;"><img src="/Netin/Content/Images/loading-2.gif" alt="正在执行操作,请稍等..."/></div>';
    $(document.body).append(loadingBox);

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
        '<table id="nt-confirm-box" class="nt-confirm-box"  style="display:none;">',
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
})

var nt = nt || {};
nt.upload = null;
nt.confirm_ok_callback = null;
nt.confirm_cancel_callback = null;
nt.alert_ok_callback = null;

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
    $('#nt-alert-content').text(msg);
    showMask();
    centerizeElement('#nt-alert-box');
    $('#nt-alert-box').show();
    nt.alert_ok_callback = fn;
}
/*when close the alert window*/
function closeAlertBox() {
    $('#nt-alert-box').hide();
    if ($('#nt-upload-box') && $('#nt-upload-box').css('display') == 'block')
    { } else { removeMask(); }

    if (nt.alert_ok_callback) {
        nt.alert_ok_callback.call();//ie下好用
        nt.alert_ok_callback = null;
    }
}

//确定对话框
function ntConfirm(msg, fn1, fn2) {
    $('#nt-confirm-content').text(msg);
    showMask();
    centerizeElement('#nt-confirm-box');
    $('#nt-confirm-box').show();
    nt.confirm_ok_callback = fn1;
    nt.confirm_cancel_callback = fn2;
}

//ntConfirm对话框确定时
function ntOk() {
    removeMask();
    $('#nt-confirm-box').hide();
    if (nt.confirm_ok_callback)
        nt.confirm_ok_callback();
    nt.confirm_ok_callback = null;
}

//ntConfirm对话框取消时
function ntCancel() {
    removeMask();
    $('#nt-confirm-box').hide();
    if (nt.confirm_cancel_callback)
        nt.confirm_cancel_callback();
    nt.confirm_cancel_callback = null;
}

/*mask loading begin*/
function showMask() {
    $('#mask').css('height', document.documentElement.scrollHeight);
    $('#mask').css('width', document.documentElement.scrollWidth);
    $('#mask').show();
    $('#mask').stop().animate({
        opacity: 0.7
    }, 80);
}

/*remove this element*/
function removeMask() {
    $('#mask').stop().animate({ opacity: 0 }, 40, '', function () {
        $(this).hide();
    });
}

//start loading status
function showLoading() {
    centerizeElement('#nt-loading');
    $('#nt-loading').show();
    showMask();
}

//stop loading status
function removeLoading() {
    $('#nt-loading').hide();
    removeMask();
}

//close the upload dialog
function closeUploadDialog() {
    $('#nt-upload-box').hide();
    $('#nt-upload-box').find('input[type="file"]').val('');
    removeMask();
}

/*open upload dialog*/
function openUploadDialog(funcUpload, title) {
    if (!title)
        title = '上传图片';
    $('#nt-upload-box').find('.nt-upload-box-title').text(title);
    nt.upload = funcUpload || function () { ntAlert(''); };
    centerizeElement('#nt-upload-box');
    $('#nt-upload-box').show();
    showMask();
}

///ini function to upload
function upload() {
    ntAlert('请提供upload方法!');
}

/*open window*/
function openWindow(url, title) {
    window.open(url, title, 'height=500, width=800, top=0, left=0, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=no, status=no');
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



window.onresize = function () {
    //if ($('#mask').css('display') == 'block') {
    //    showMask();
    //}
    //if ($('#nt-alert-box').css('display') == 'block')
    //    centerizeElement('#nt-alert-box');
    //if ($('#nt-confirm-box').css('display') == 'block')
    //    centerizeElement('#nt-confirm-box');
}