using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.BLL;
using System.Web.UI.WebControls;
using Nt.Model;
using Nt.BLL.Extension;
using Nt.DAL;
using Nt.DAL.Helper;

namespace Nt.Framework
{
    public class NtPageForEdit<M> : global::Nt.Framework.NtPage, IEditPage
        where M : BaseViewModel, new()
    {

        #region Service
        protected BaseService<M> _service = null;
        protected MemberRoleService _memberRoleService = null;
        #endregion

        #region Model

        M _model = null;
        /// <summary>
        /// 实体
        /// </summary>
        public M Model
        {
            get
            {
                return _model;
            }
        }

        #endregion

        #region Props

        /// <summary>
        /// 会员角色提供
        /// </summary>
        List<ListItem> _memberRoles = null;
        public List<ListItem> MemberRoles
        {
            get { return _memberRoles; }
            set { _memberRoles = value; }
        }

        /// <summary>
        /// 编辑页的标题前缀
        /// </summary>
        public string EditTitlePrefix
        {
            get
            {
                return EnsureEdit ? "编辑" : "添加";
            }
        }

        protected int _id = IMPOSSIBLE_ID;
        /// <summary>
        /// 当页面为编辑时，实体的的ID
        /// </summary>
        public int NtID
        {
            get
            {
                if (_id == IMPOSSIBLE_ID)
                    Int32.TryParse(Request.QueryString["Id"], out _id);
                if (_id == 0)
                    return IMPOSSIBLE_ID;
                return _id;
            }
        }

        private string _formActionUrl = string.Empty;
        /// <summary>
        /// 表单action值
        /// </summary>
        public string FormActionUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_formActionUrl))
                    return Request.Url.PathAndQuery;
                return _formActionUrl;
            }
            set { _formActionUrl = value; }
        }

        /// <summary>
        /// 一个bool值，指示当前页面是否是编辑
        /// </summary>
        public bool EnsureEdit
        {
            get { return NtID != IMPOSSIBLE_ID; }
        }

        private string _listUrl = "List.aspx";
        /// <summary>
        /// 与此编辑页面相对应的列表页的相对路径
        /// </summary>
        public string ListUrl
        {
            get
            {
                if (_listUrl.StartsWith("/"))
                    return _listUrl;
                else
                {
                    return string.Format("/netin/{0}/{1}?page={2}", CurrentPermissionRecord.Category, _listUrl, Request.QueryString["page"]);
                }
            }
            set { _listUrl = value; }
        }

        int _maxID = -1;
        public int MaxID
        {
            get
            {
                if (_maxID == -1)
                {
                    object v = SqlHelper.ExecuteScalar(string.Format(
                    "Select Max(ID) From [{0}]", _service.TableName));
                    if (v == DBNull.Value)
                        _maxID = 0;
                    else
                        _maxID = Convert.ToInt32(v);
                }
                return _maxID;
            }
        }

        #endregion

        #region Utilities
        /// <summary>
        /// 去列表页
        /// </summary>
        public virtual void GotoListPage()
        {
            Response.Redirect(ListUrl, true);
        }

        /// <summary>
        /// 先弹出一个警告框然后跳转到列表页
        /// </summary>
        /// <param name="msg">不可出现英文单引号</param>
        public virtual void GotoListPage(string msg)
        {
            //string url = ListUrl;
            Goto(ListUrl, msg);
        }

        /// <summary>
        /// 返回列表页的js(onclick)
        /// </summary>
        /// <returns></returns>
        public string GoBackScript()
        {
            return string.Format("window.location.href='{0}';return false;", ListUrl);
        }

        /// <summary>
        /// 重置表单
        /// </summary>
        /// <returns></returns>
        public string ResetForm(string formName = "EditForm")
        {
            return string.Format("document.{0}.reset();return false;", formName);
        }

        public string OnSubmitCall()
        {
            var midpart = _service.TableName.Substring(3);
            string funcName = string.Format("validate{0}Form", midpart);
            return string.Format("if('undefined'!=typeof editor)editor.sync();if({0}){{return {0}();}}else{{return true;}}", funcName);
        }

        #endregion

        #region HttpPost  Insert And Update

        /// <summary>
        /// 添加一个 记录
        /// </summary>
        protected virtual void Insert()
        {
            _id = _service.Insert(_model);
            Logger.Log(string.Format("用户{0}向表{2}插入一个Id为{1}的记录",
                WorkingUser.UserName, _id, _service.TableName));
        }

        /// <summary>
        /// 更新一个 记录
        /// </summary>
        protected virtual void Update()
        {
            _service.Update(_model);
            Logger.Log(string.Format("用户{0}更新了表{2}中的Id为{1}的记录",
                WorkingUser.UserName, NtID, _service.TableName));
        }
        #endregion

        #region HttpGet

        /// <summary>
        /// 当页面为编辑时对页面所需数据的初始化操作，必须重写
        /// </summary>
        void InitDataToUpdate()
        {
            _model = CommonFactory.GetById<M>(NtID);
            if (_model == null)
                GotoListPage("当前查询的记录不存在!");
        }


        /// <summary>
        /// 当页面为添加时对页面所需数据的初始化操作，必须重写
        /// </summary>
        void InitDataToInsert()
        {
            _model = new M();
            _model.InitData();
        }

        #endregion

        #region virtual function

        /// <summary>
        /// 在子类中复写此方法并初始化一些必要的数据
        /// </summary>
        protected virtual void InitRequiredData() { }

        /// <summary>
        /// 插入数据前
        /// </summary>
        protected virtual void BeginConfigInsert() { }
        /// <summary>
        /// 插入数据后
        /// </summary>
        protected virtual void EndConfigInsert() { }
        /// <summary>
        /// 更新数据前
        /// </summary>
        protected virtual void BeginConfigUpdate() { }
        /// <summary>
        /// 更新数据后
        /// </summary>
        protected virtual void EndConfigUpdate() { }

        /// <summary>
        /// 当编辑时向页面提供数据之前
        /// </summary>
        protected virtual void BeginInitDataToUpdate() { }
        /// <summary>
        /// 当编辑时向页面提供数据之后
        /// </summary>
        protected virtual void EndInitDataToUpdate() { }
        /// <summary>
        /// 当添加时向页面提供数据之前
        /// </summary>
        protected virtual void BeginInitDataToInsert() { }
        /// <summary>
        /// 当添加时向页面提供数据之后
        /// </summary>
        protected virtual void EndInitDataToInsert() { }

        protected virtual bool NtValidateForm()
        {
            return true;
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            InitRequiredData();
            //_memberRoleService = new MemberRoleService();
            //_memberRoles = _memberRoleService.GetAvailableMemberRoles();
            if (_service == null)
                _service = NtEngine.GetService<M>();
            if (IsHttpPost)
            {
                _model = new M();
                _model.InitDataFromPage();//从页面获取数据

                if (NtValidateForm())
                {
                    if (EnsureEdit)
                    {
                        BeginConfigUpdate();
                        Update();
                        EndConfigUpdate();
                    }
                    else
                    {
                        BeginConfigInsert();
                        Insert();
                        EndConfigInsert();
                    }
                    GotoListPage("保存成功!");//回到列表页
                }

            }

            /*以下是初次加载页面时所做的时*/
            if (EnsureEdit)
            {
                BeginInitDataToUpdate();
                InitDataToUpdate();
                EndInitDataToUpdate();
            }
            else
            {
                BeginInitDataToInsert();
                InitDataToInsert();
                EndInitDataToInsert();
            }
            base.OnLoad(e);
        }

    }
}
