using Nt.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Nt.Pages.Product.Uc
{
    public class UcProductPicture : NtUserControl
    {
        public int ProductID { get; set; }

        DataTable _data;
        public DataTable DataSource
        {
            get { return _data; }
            set
            {
                _data = value;
            }
        }

        /// <summary>
        /// max number of pictures you can upload for each product
        /// </summary>
        public int MaxPicturesCount
        {
            get
            {
                string count= global::System.Configuration.
                    ConfigurationManager.AppSettings["product.pictures.max.count"];
                if (string.IsNullOrEmpty(count))
                { 
                    return 100;
                }
                return Convert.ToInt32(count);
            }
        }
    }
}
