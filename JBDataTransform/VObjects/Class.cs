using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBDataTransform.VObjects
{
    class Class : ValueObjectBase
    {
        private const string Command = "SELECT sc01clsno \"代碼\",sc01year \"學年度\",sc01smter \"學期\", sc01grade \"年級\", sc01clsnm \"名稱\" from sc01 where sc01year='{0}' and sc01smter='{1}'";

        public static string GetCommandText(string schoolYear, string semester)
        {
            return string.Format(Command, schoolYear, semester);
        }
    }
}
