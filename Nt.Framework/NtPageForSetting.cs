using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.Model.SettingModel;
using Nt.BLL;
using Nt.BLL.Extension;

namespace Nt.Framework
{
    public class NtPageForSetting<S> : NtPage
        where S : BaseSettingModel, new()
    {

        #region service
        SettingService<S> _service;
        #endregion

        #region Model

        S _model;
        public S Model
        {
            get { return _model; }
        }

        #endregion

        #region Utility
        
        /// <summary>
        /// 重置表单
        /// </summary>
        /// <returns></returns>
        public string ResetForm()
        {
            return string.Format("document.EditForm.reset();return false;");
        }

        public string OnSubmitCall()
        {
            var midpart = typeof(S).Name;
            string funcName = string.Format("validate{0}Form", midpart);
            return string.Format("if('undefined'!=typeof editor)editor.sync();return {0}();", funcName);
        }

        #endregion

        #region Get

        void Get()
        {
            BeginGet();
            _model = _service.ResolveSetting();
            EndGet();
        }

        #endregion

        #region Post

        void Post()
        {
            _model = new S();
            _model.InitDataFromPage();
            BeginPost();
            _service.SaveSetting(_model);
            EndPost();
        }

        #endregion

        #region virtual function

        protected virtual void BeginPost() { }
        protected virtual void EndPost() { }

        protected virtual void BeginGet() { }
        protected virtual void EndGet() { }

        protected virtual void InitRequiredData() { }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitRequiredData();
            if (_service == null)
                _service = new SettingService<S>();
            if (IsHttpPost)
            {
                Post();
                ReLoadByScript("保存成功!");
            }
            else
                Get();
        }

    }
}
