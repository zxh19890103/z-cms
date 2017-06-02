using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nt.BLL;
using Nt.Model;
using Nt.DAL;

namespace Nt.Framework
{
    public class NtPageForEditAsTree<M>:NtPageForEdit<M>
         where M : BaseTreeModel, new()
    {
        protected override void Insert()
        {
            _id=CommonFactoryAsTree.Insert(Model);
            Logger.Log(string.Format("用户{0}向表{2}插入一个Id为{1}的记录",
                WorkingUser.UserName, _id, _service.TableName));
        }
    }
}
