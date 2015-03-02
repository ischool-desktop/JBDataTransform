using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBDataTransform.VObjects
{
    class RetakeSubjectScore : ValueObjectBase
    {
        public const string Command =
                @"SELECT sc14year 學年度, sc14smter 學期, sc14grade 年級, sc14stuno 學號, sc14lsdat 科目, sc14score 成績
                FROM sc14
                WHERE sc14stuno in (SELECT sc02stuno FROM sc02 join sc04 ON sc02.sc02stuno=sc04.sc04stuno and sc02year='103' and sc02smter='1')
                    --AND sc14stuno = '111001'";
    }
}
