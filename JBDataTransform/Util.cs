using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Cells;
using JBDataTransform.VObjects;

namespace JBDataTransform
{
    static class Util
    {
        /*
        ^([0-9]{3})([0-9]{2})([0-9]{2})
        0860208

        ^([0-9]{2})([0-9]{2})([0-9]{2})
        850816
         */

        /// <summary>
        /// 0860208, 850816 => 086/02/08 and 85/08/16
        /// </summary>
        /// <param name="input">輸入字串。</param>
        /// <param name="addYear">增加年數。</param>
        /// <returns></returns>
        public static string SpliteDate(string input, int addYear)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            Regex r = null;

            if (input.Length == 7)
                r = new Regex("^([0-9]{3})([0-9]{2})([0-9]{2})");
            else if (input.Length == 6)
                r = new Regex("^([0-9]{2})([0-9]{2})([0-9]{2})");
            else
                return input;

            Match mm = r.Match(input);

            if (mm.Success)
            {
                string y, m, d;

                y = mm.Groups[1].Value;
                m = mm.Groups[2].Value;
                d = mm.Groups[3].Value;

                int add;
                if (int.TryParse(y, out add))
                {
                    add += addYear;
                    y = add.ToString();
                }

                return string.Format("{0}/{1}/{2}", y, m, d);
            }
            else
                return string.Empty;
        }

        public static Dictionary<string, int> FillFields(Worksheet sheet, string[] fields)
        {
            Dictionary<string, int> output = new Dictionary<string, int>();
            int idx = 0;
            foreach (string field in fields)
            {
                sheet.Cells[0, idx].PutValue(field);
                output.Add(field, idx);
                idx++;
            }

            return output;
        }

        public static void FillData<T>(Worksheet sheet,
            Dictionary<string, int> columnMap,
            List<T> records,
            Func<T, string, string> getValue,
            int startRowIndex = 1)
            where T : ValueObjectBase
        {
            int rowIdx = startRowIndex;

            foreach (T record in records)
            {
                foreach (string column in columnMap.Keys)
                {
                    string val = getValue(record, column);
                    if (val == null)
                        val = record.GetData(column);

                    sheet.Cells[rowIdx, columnMap[column]].PutValue(val);
                }

                rowIdx++;
            }
        }

        public static string GetSubjectName(Dictionary<string, Subject> subjectMap, string code, string name)
        {
            string subjName = name;

            if (subjectMap.ContainsKey(code))
                subjName = subjectMap[code].Name;

            return subjName;
        }

        public static Tuple<string, int> ParseSubjectName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            string level = ToNarrow(name.Substring(name.Length - 1, 1));
            string subj = name.Trim();

            if (Char.IsDigit(level, 0) || level == "上" || level == "下")
            {
                subj = name.Remove(name.Length - 1).Trim();

                if (string.IsNullOrWhiteSpace(subj))
                    throw new Exception("無科目名稱");

                if (level == "上")
                    level = "1";
                else if (level == "下")
                    level = "2";

                return new Tuple<string, int>(subj, int.Parse(level));
            }
            else
            {
                return new Tuple<string, int>(subj, 1);

            }
        }


        ///<summary>
        ///字串轉半形
        ///</summary>
        ///<paramname="input">任一字元串</param>
        ///<returns>半形字元串</returns>
        public static string ToNarrow(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        public static int GetDigit(string val)
        {
            int sy;

            if (int.TryParse(val, out sy))
                return sy;
            else
                return 0;
        }
    }
}
