/// <reference path="jquery-1.7.2.min.js" />
/// <reference path="validate.form.func.js" />

var nt = nt || {};
nt.reg4Email = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
nt.reg4Phone = /^(\(\d{3,4}\)|d{3,4}-)?\d{7,8}$/;
nt.reg4PostCode = /^[a-zA-Z0-9 ]{3,12}$/;
nt.reg4Passwd = /^(\w){6,20}$/;//只能输入6-20个字母、数字、下划线 
nt.reg4Digit = /^[0-9]{1,9}$/;
nt.reg4RegisterUserName = /^[a-zA-Z]{1}([a-zA-Z0-9]|[._]){4,19}$/; //只能输入5-20个以字母开头、可带数字、“_”、“.”的字串 
nt.reg4TrueName = /^[a-zA-Z]{1,30}$/; //只能输入1-30个以字母开头的字串 


/*****************************产品或下载 开始************************************/
/*产品或下载表单验证*/
function validateProductForm() {

    if (document.EditForm.Title.value == '') {
        ntAlert('标题不能为空!');
        return false;
    }

    if (!nt.reg4Digit.test(document.EditForm.DisplayOrder.value)) {
        ntAlert('排序字段只能为数字!');
        return false;
    }

    if (document.EditForm.FileUrl && document.EditForm.FileUrl.value == '') {
        ntAlert('请上传附件!');
        return false;
    }
    return true;
}

/*产品 分类 表单验证*/
function validateProductCategoryForm() {
    if (document.EditForm.Name.value == '') {
        ntAlert('类别名不能为空!');
        document.EditForm.Name.focus();
        return false;
    }
    return true;
}
/***************************产品或下载 结束**************************************/

/*****************************新闻 开始************************************/
/*新闻表单验证*/
function validateNewsForm() {

    if (document.EditForm.Title.value == '') {
        ntAlert('标题不能为空!');
        document.EditForm.Title.focus();
        return false;
    }

    if (document.EditForm.Body.value == '') {
        ntAlert('新闻内容不能为空!');
        document.EditForm.Body.focus();
        return false;
    }

    if (!nt.reg4Digit.test(document.EditForm.DisplayOrder.value)) {
        ntAlert('排序字段只能为数字!');
        document.EditForm.DisplayOrder.focus();
        return false;
    }

    return true;
}

/*新闻 分类 表单验证*/
function validateNewsCategoryForm() {
    if (document.EditForm.Name.value == '') {
        ntAlert('类别名不能为空!');
        document.EditForm.Name.focus();
        return false;
    }

    if (!nt.reg4Digit.test(document.EditForm.DisplayOrder.value)) {
        ntAlert('排序字段只能为数字!');
        document.EditForm.DisplayOrder.focus();
        return false;
    }

    return true;
}
/*******************************新闻 结束****************************************/

/**********************************单页面 开始*********************************************/
/*二级页面的表单验证*/
function validateSinglePageForm() {

    if (document.EditForm.Title.value == '') {
        ntAlert('标题不能为空!');
        document.EditForm.Title.focus();
        return false;
    }

    if (document.EditForm.Body.value == '') {
        ntAlert('内容不能为空!');
        document.EditForm.Body.focus();
        return false;
    }
    return true;
}

/*专题页的表单验证*/
function validateSpecialPageForm() {

    if (document.EditForm.Path.value == '') {
        ntAlert('文件路径不能为空!');
        document.EditForm.Path.focus();
        return false;
    }

    return true;
}

/**********************************单页面 结束****************************************/


/*导航表单验证*/
function validateNavigationForm() {
    if (document.EditForm.Name.value == '') {
        ntAlert('类别名不能为空!');
        document.EditForm.Name.focus();
        return false;
    }

    if (!nt.reg4Digit.test(document.EditForm.DisplayOrder.value)) {
        ntAlert('排序字段只能为数字!');
        document.EditForm.DisplayOrder.focus();
        return false;
    }

    return true;
}

/**************************会员 开始**************************/
/*会员表单验证*/
function validateMemberForm() {

    if (!nt.reg4RegisterUserName.test(document.EditForm.LoginName.value)) {
        ntAlert('会员名只能输入5-20个以字母开头、可带数字、“_”、“.”的字串!');
        document.EditForm.LoginName.focus();
        return false;
    }

    if (document.EditForm.Password
        && !nt.reg4Passwd.test(document.EditForm.Password.value)) {
        ntAlert('密码只能输入6-20个字母、数字、下划线 !');
        document.EditForm.Password.focus();
        return false;
    }

    if (document.EditForm.Password
        && document.EditForm.Password.value != document.getElementsByName('Password.Again')[0].value) {
        ntAlert('两次输入的密码不相同!请重新输入!');
        document.EditForm.Password.focus();
        return false;
    }

    return true;
}

/*会员设置表单验证*/
function validateMemberSettingsForm() {

    return true;
}

/*会员角色表单验证*/
function validateMemberRoleForm() {

    if (!nt.reg4RegisterUserName.test(document.EditForm.Name.value)) {
        ntAlert('会员组名称只能输入5-20个以字母开头、可带数字、“_”、“.”的字串!');
        document.EditForm.Name.focus();
        return false;
    }

    if (nt.reg4TrueName.test(document.EditForm.SystemName.value)) {
        ntAlert('会员组系统名称只能输入1-30个以字母开头的字串 !');
        document.EditForm.SystemName.focus();
        return false;
    }

    return true;
}

/*********************会员 结束************************/


/*********************用户 开始************************/

/*用户表单验证*/
function validateUserForm() {

    if (!nt.reg4RegisterUserName.test(document.EditForm.UserName.value)) {
        ntAlert('用户名只能输入5-20个以字母开头、可带数字、“_”、“.”的字串!');
        document.EditForm.UserName.focus();
        return false;
    }

    if (document.EditForm.Password
        && !nt.reg4Passwd.test(document.EditForm.Password.value)) {
        ntAlert('密码只能输入6-20个字母、数字、下划线 !');
        document.EditForm.Password.focus();
        return false;
    }

    if (document.EditForm.Password
        && document.EditForm.Password.value != document.getElementsByName('Password.Again')[0].value) {
        ntAlert('两次输入的密码不相同!请重新输入!');
        document.EditForm.Password.focus();
        return false;
    }

    return true;
}

/*用户角色表单验证*/
function validateUserLevelForm() {

    if (!nt.reg4RegisterUserName.test(document.EditForm.Name.value)) {
        ntAlert('用户角色名称只能输入5-20个以字母开头、可带数字、“_”、“.”的字串!');
        document.EditForm.Name.focus();
        return false;
    }

    if (nt.reg4TrueName.test(document.EditForm.SystemName.value)) {
        ntAlert('用户角色系统名称只能输入1-30个以字母开头的字串 !');
        document.EditForm.SystemName.focus();
        return false;
    }
    return true;
}

/*********************用户 结束************************/

/*语言版本表单验证*/
function validateLanguageForm() {

    if (document.EditForm.Name.value == '') {
        ntAlert('名称不能为空!');
        return false;
    }

    if (!nt.reg4Digit.test(document.EditForm.DisplayOrder.value)) {
        ntAlert('排序字段只能为数字!');
        return false;
    }
    return true;
}

/*Banner版本表单验证*/
function validateBannerForm() {
    if (document.EditForm.Picture_Id.value <= 0) {
        ntAlert("请上传图片");
        return false;
    }

    if (!nt.reg4Digit.test(document.EditForm.DisplayOrder.value)) {
        ntAlert('排序字段只能为数字!');
        return false;
    }

    return true;
}

/*网站设置表单验证*/
function validateWebsiteInfoSettingsForm() {
    if (document.EditForm.WebsiteName.value == '') {
        ntAlert('网站名称不能为空!');
        document.EditForm.WebsiteName.focus();
        return false;
    }
    return true;
}

/*网站设置表单验证*/
function validateBookAdminNoticeForm() {
    if (document.EditForm.Title.value == '') {
        ntAlert('标题不能为空!');
        document.EditForm.Title.focus();
        return false;
    }
    return true;
}

function validateMessageAdminNoticeForm() {
    if (document.EditForm.Title.value == '') {
        ntAlert('标题不能为空!');
        document.EditForm.Title.focus();
        return false;
    }
    return true;
}

/*网站设置表单验证*/
function validateMessageSettingsForm() {

    return true;
}

/*网站设置表单验证*/
function validateJobSettingsForm() {

    return true;
}

/*网站设置表单验证*/
function validateJobForm() {
    if (document.EditForm.JobName.value == '') {
        ntAlert('职位名不能为空!');
        document.EditForm.JobName.focus();
        return false;
    }

    if (!nt.reg4Digit.test(document.EditForm.DisplayOrder.value)) {
        ntAlert('排序字段只能为数字!');
        return false;
    }

    return true;
}

/*网站设置表单验证*/
function validateOrderSettingsForm() {

    return true;
}

/*网站设置表单验证*/
function validateOrderForm() {
    if (document.EditForm.Title.value == '') {
        ntAlert('标题不能为空!');
        document.EditForm.Title.focus();
        return false;
    }
    return true;
}

/*课程系统 验证 开始*/

function validateCourseCategoryForm() {
    if (document.EditForm.Name.value == '') {
        ntAlert('课程名称不能为空!');
        document.EditForm.Name.focus();
        return false;
    }
    return true;
}

function validateCourseForm() {
    if (document.EditForm.Title.value == '') {
        ntAlert('标题不能为空!');
        document.EditForm.Title.focus();
        return false;
    }

    if (document.EditForm.Body.value == '') {
        ntAlert('内容不能为空!');
        document.EditForm.Body.focus();
        return false;
    }

    if (!nt.reg4Digit.test(document.EditForm.DisplayOrder.value)) {
        ntAlert('排序字段只能为数字!');
        return false;
    }

    return true;
}

/*课程系统 验证 结束*/
