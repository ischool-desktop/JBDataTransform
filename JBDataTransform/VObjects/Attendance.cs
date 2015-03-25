using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBDataTransform.VObjects
{
    class Attendance : ValueObjectBase
    {
        public const string Command = @"
                    SELECT          ed01sindt '日期', ed01year '學年度', ed01smter '學期', ed01stuno '學號', ed01stcno '啟始節數', ed01ttcct '總節數', ed01pptpe '缺曠假別'
                    FROM              ed01
                    WHERE ed01stuno in (SELECT sc04stuno
	                    FROM sc02 join sc04 ON sc02.sc02stuno=sc04.sc04stuno and sc02year='103' and sc02smter='1') --測班座時，要把 ) 拿掉。
                        --WHERE (cast((sc02grade)+3 as varchar) + substring(sc02clsno,4,2))='404' AND sc02setno='21')
                    AND ed01pptpe!='0' --消假的
                    ORDER BY ed01sindt";

        /// <summary>
        /// 日期
        /// </summary>
        public string Date
        {
            get { return Util.SpliteDate(Self.日期, 1911); }
        }

        /// <summary>
        /// 缺曠假別
        /// </summary>
        public string AbsenceType
        {
            get { return GetAbsenceTypeString(Self.缺曠假別); }
        }

        /// <summary>
        /// 如果回傳 0 代表是特殊的節次，例如午休，他無法轉換成數字。
        /// </summary>
        public int StartPeriod
        {
            get { return Util.GetDigit(Self.啟始節數); }
        }

        public string StartPeriodString
        {
            get { return Self.啟始節數; }
        }

        public int PeriodCount
        {
            get { return Util.GetDigit(Self.總節數); }
        }

        public static Dictionary<string, string> Mapping = null;

        public static string GetAbsenceTypeString(string type)
        {
            if (Mapping == null)
            {
                Mapping = new Dictionary<string, string>();

                Mapping.Add("A", "事假");
                Mapping.Add("B", "病假");
                Mapping.Add("C", "喪假");
                Mapping.Add("D", "曠課");
                Mapping.Add("E", "遲到");
                Mapping.Add("F", "未到");
                Mapping.Add("G", "未到");
                Mapping.Add("H", "公假");
                Mapping.Add("J", "未到");
            }

            if (Mapping.ContainsKey(type))
                return Mapping[type];
            else
                return string.Empty;
        }

        public static Dictionary<string, string> pMapping = null;

        public static string GetChinesePeriodString(string str)
        {
            if (pMapping == null)
            {
                pMapping = new Dictionary<string, string>();
                pMapping.Add("A", "早修");
                pMapping.Add("B", "升旗");
                pMapping.Add("1", "一");
                pMapping.Add("2", "二");
                pMapping.Add("3", "三");
                pMapping.Add("4", "四");
                pMapping.Add("M", "午休");
                pMapping.Add("5", "五");
                pMapping.Add("6", "六");
                pMapping.Add("7", "七");
                pMapping.Add("8", "八");
                pMapping.Add("9", "九");
            }

            if (pMapping.ContainsKey(str))
                return pMapping[str];
            else
                return string.Empty;
        }

        /*
        節次
        1
        2
        3
        4
        5
        6
        7
        8
        9
        A 早讀
        B 朝會
        M 午休

        假別
        0 消假
        A 事假
        B 病假
        C 喪假
        D 曠課
        E 遲到
        F (早讀)未到
        G(朝會)未到
        H 公假
        J (午休)未到
        */
    }
}