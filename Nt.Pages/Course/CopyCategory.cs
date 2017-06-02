﻿using Nt.BLL;
using Nt.DAL;
using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Pages.Course
{
    public class CopyCategory : NtPage
    {
        protected override void OnLoad(EventArgs e)
        {
            if (IsHttpPost)
            {
                string langids = Request.Form[ConstStrings.COPY_TREE_TARGET_LANGUAGES];
                if (!string.IsNullOrEmpty(langids))
                {

                    int[] arr_ids = NtUtility.SeparateToIntArray(',', langids);
                    CourseCategoryService service = new CourseCategoryService();
                    service.CopyAllDataTo(arr_ids, "course", null);
                    Goto("Category.aspx", "课程目录拷贝成功!");
                }
                else
                {
                    Alert("参数错误!");
                }
            }

            base.OnLoad(e);
        }

        DataTable _availableLanguages;
        public DataTable AvailableLanguages
        {
            get
            {
                if (_availableLanguages == null)
                {
                    _availableLanguages = CommonFactory.GetList("Nt_Language");
                }
                return _availableLanguages;
            }
        }

        public override PermissionRecord CurrentPermissionRecord
        {
            get
            {
                return PermissionRecordProvider.CourseCategoryManage;
            }
        }
    }
}