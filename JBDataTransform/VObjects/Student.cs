using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBDataTransform.VObjects
{
    class Student : ValueObjectBase
    {
        public const string Command =
            @"SELECT sc04idno 身分證號, sc04stuno 學號, sc04stunm 姓名, sc04sex 性別,sc04brdte 生日, 
                sc04fathr 家長姓名, sc04tlno 聯絡電話, sc04faxno '其它電話:1',sc04zap '戶籍:郵遞區號', sc04addr '戶籍:完整地址', 
                sc04agiwn 入學核淮文號, sc04doein 入學核准字號, sc04agidt 入學核淮日期, sc04lstno 最後學號,sc04iyear 入學年度, 
                sc01grade 年級, sc01clsno 班級編碼, sc01.sc01clsnm 班級, sc02setno 座號 
            FROM sc02 JOIN sc04 ON sc02.sc02stuno=sc04.sc04stuno AND sc02year='103' AND sc02smter='1' 
                	JOIN sc01 ON sc01.sc01clsno=sc02.sc02clsno AND sc01year='103' AND sc01smter='1' 
            --WHERE sc04stuno = '111001'
            ORDER BY 年級,班級,座號";

        public string ClassName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Self.班級編碼) || string.IsNullOrWhiteSpace(Self.年級))
                    return string.Empty;

                int grade;

                if (!int.TryParse(Self.年級, out grade))
                    return string.Empty;

                string code = Self.班級編碼;

                return string.Format("{0}{1}", grade + 3, code.Substring(code.Length - 2, 2));
            }
        }

        public string Birthdate
        {
            get { return Util.SpliteDate(Self.生日, 1911); }
        }

        public string Gender
        {
            get
            {
                if (Self.性別 == "1")
                    return "男";
                else if (Self.性別 == "2")
                    return "女";
                else
                    return string.Empty;
            }
        }
    }
}
