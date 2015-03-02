using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBDataTransform.VObjects
{
    class SubjectScore
    {
        public int SchoolYear { get; set; }

        public int Semester { get; set; }

        public int Grade { get; set; }

        /// <summary>
        /// 科目代碼，ischool 中沒有此資料。
        /// </summary>
        public string Code { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public decimal OriginScore { get; set; }

        public decimal? MakeupScore { get; set; }

        public decimal? RetakeScore { get; set; }

        public bool Passed { get; set; }

        public string PassString
        {
            get { return Passed ? "是" : "否"; }
        }
        public decimal Credit { get; set; }

        /// <summary>
        /// 分項類別。
        /// </summary>
        public string EntryGroup { get; set; }

        /// <summary>
        /// 分項類別字串。
        /// </summary>
        public string EntryGroupString
        {
            get
            {
                if (EntryGroup == "智育類")
                    return "學業";
                else if (EntryGroup == "體育類")
                    return "體育";
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// 必選修。
        /// </summary>
        public string Required { get; set; }

        /// <summary>
        /// 校部訂。
        /// </summary>
        public string Decision { get; set; }

        /// <summary>
        /// 學年度、學期、科目名稱、科目級別
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}", Name, Level, OriginScore, MakeupScore, RetakeScore);
        }
    }
}
