using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourierNewton.Common.Database
{
    public class DatabaseConstants
    {

        //Database is located in the folder of Databases which is located in the two folder above of BaseDirectory.  
        private static string ConnectionStringTemplateOfDatabase202008261632 = @"Data Source=|BaseDirectory|../../Databases/Database202008261632.db;Version=3;";
        public readonly static string ConnectionStringOfDatabase202008261632 =
            ConnectionStringTemplateOfDatabase202008261632.Replace("|BaseDirectory|", AppDomain.CurrentDomain.BaseDirectory);

    }
}
