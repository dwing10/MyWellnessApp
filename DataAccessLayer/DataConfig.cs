﻿using MyWellnessApp.DataAccessLayer.SQL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWellnessApp.DataAccessLayer
{
    public class DataConfig
    {
        // set data type
        public static DataType dataType = DataType.SQL;

        public IDataService SetDataService()
        {
            switch (dataType)
            {
                case DataType.SQL:
                    return new DataServiceSql();
                default:
                    throw new Exception();
            }
        }
    }
}
