using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBDataTransform.VObjects
{
    class Discipline : ValueObjectBase
    {
        public const string Command =
              @"SELECT ed02year '學年度',ed02smter '學期',ed02stuno '學號',ed02sindt '日期',ed02pptp1 '類型',ed02ttct1 '次數', ed02resn1 '事由',ed02ckflg '生效否', ed02txdte '銷過日期', ed02delsn '銷過事由', ed02lxdtm '登錄日期'
                    FROM ed02
                    WHERE ed02stuno in (SELECT sc04stuno
	                                        FROM sc02 join sc04 ON sc02.sc02stuno=sc04.sc04stuno and sc02year='103' and sc02smter='1')
						                    --WHERE sc02stuno in (210026))
                    ORDER BY ed02sindt,ed02stuno";

        /// <summary>
        /// 獎懲類型。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string TypeString(string type)
        {
            switch (type)
            {
                case "1":
                    return "大功";
                case "2":
                    return "小功";
                case "3":
                    return "嘉獎";
                case "4":
                    return "大過";
                case "5":
                    return "小過";
                case "6":
                    return "警告";
                case "7":
                    return "留校查看";
                default:
                    throw new Exception("獎懲類別不正確。");
            }
        }
    }
}
