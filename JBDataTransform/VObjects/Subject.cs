using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBDataTransform.VObjects
{
    class Subject : ValueObjectBase
    {
        public const string Command = "select sc15lesno 代碼,sc15lesnm 名稱 from sc15";

        public string Code { get { return Self.代碼; } }

        public string Name { get { return Self.名稱; } }
    }
}
