using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Springer.EntityModel.Enum
{
    public enum StringEnum
    {
        NoSN = '0',//没有设备
        YesSN = '1',//存在设备
        NoData = '2',  //没有数据       
        Success='3',//成功
        Fail='4',//失败
        Error = '9'
    }
}
