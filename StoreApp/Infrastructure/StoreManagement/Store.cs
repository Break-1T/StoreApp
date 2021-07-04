using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Infrastructure.DbManagement;

namespace StoreApp.Infrastructure.StoreManagement
{
    class Store
    {
        public Store()
        {
            DataBaseControl = new DbControl();
        }

        public DbControl DataBaseControl { get; set; }
    }
}
