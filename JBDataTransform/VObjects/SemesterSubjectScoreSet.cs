using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JBDataTransform.VObjects
{
    class SemesterSubjectScoreSet : ValueObjectBase
    {
        public const string Command =
                    @"SELECT sc11stuno 學號, sc11year 學年度, sc11smter 學期, sc11grade 年級, sc11scr01 學業成績, sc11order 學業成績名次, 
                        sc11lsdat 科目清單, sc11score 成績清單, sc11msdat 必選修清單, sc11bsdat 學分數清單, sc11dzdat 分項清單 
                    FROM sc11 
                    WHERE sc11stuno in (SELECT sc04stuno 學號
                            FROM sc02 join sc04 ON sc02.sc02stuno=sc04.sc04stuno and sc02year='103' and sc02smter='1')
	                    AND NOT (sc11stuno='210079' and sc11totsc=1915 and sc11order=44) --去掉重覆
	                    AND NOT (sc11stuno='111331' and sc11avgsc=51.83 and sc11clsno is null) --去掉重覆
                        --AND sc11stuno = '111001'";

        public int SchoolYear { get { return Util.GetDigit(Self.學年度); } }

        public int Semester { get { return Util.GetDigit(Self.學期); } }

        public int Grade { get { return Util.GetDigit(Self.年級); } }

        public List<SubjectScore> Subjects { get; set; }

        /// <summary>
        /// 解析科目成績資料。
        /// </summary>
        /// <returns></returns>
        public void ParseSubjectScoreData(Dictionary<string, Subject> subjectMap)
        {
            List<SubjectScore> subjects = new List<SubjectScore>();

            using (StringReader subjs = new StringReader(Self.科目清單),
                scores = new StringReader(Self.成績清單),
                reqs = new StringReader(Self.必選修清單),
                credits = new StringReader(Self.學分數清單),
                 entrys = new StringReader(Self.分項清單)
                )
            {
                while (subjs.Peek() > 0)
                {
                    SubjectScore ss = new SubjectScore();
                    ss.SchoolYear = SchoolYear;
                    ss.Semester = Semester;
                    ss.Grade = Grade;

                    string subj = subjs.ReadLine();
                    string score = scores.ReadLine();
                    string req = reqs.ReadLine();
                    string credit = credits.ReadLine();
                    string entry = entrys.ReadLine();

                    string[] parts = null;

                    parts = subj.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    ss.Code = parts[0].Trim();

                    string subjName = Util.GetSubjectName(subjectMap, ss.Code, parts[1]);

                    Tuple<string, int> sname = Util.ParseSubjectName(subjName);

                    if (sname == null) //沒有科目名稱，接下來就不用執行了。
                        continue;

                    ss.Name = sname.Item1;
                    ss.Level = sname.Item2;

                    //60=-1=
                    parts = score.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    decimal originScore, makeupScore;
                    if (!decimal.TryParse(parts[0], out originScore)) //沒有原始成績，接下來不執行。
                        continue;

                    ss.OriginScore = originScore; //原始成績。

                    //補考成績。
                    if (decimal.TryParse(parts[1], out makeupScore) && makeupScore != -1)
                        ss.MakeupScore = makeupScore;

                    parts = req.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts[0] == "1") //必選修
                        ss.Required = "必修";
                    else if (parts[0] == "2" || parts[0] == "3") //2 是「必選」。
                        ss.Required = "選修";

                    if (ss.Required == "必修") //校部訂
                        ss.Decision = "部訂";
                    else
                        ss.Decision = "校訂";

                    parts = credit.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    ss.Credit = decimal.Parse(parts[0]); //學分

                    ss.Passed = (parts[0] == parts[1]); //是否取得學分(後面還有重修會影響此屬性)

                    parts = entry.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    ss.EntryGroup = parts[1];

                    subjects.Add(ss);
                }
            }

            Subjects = subjects;
        }

        /// <summary>
        /// 尋找與處理重修成績。
        /// </summary>
        /// <param name="subjects"></param>
        /// <param name="retakeList"></param>
        public static void CalculateRetakeScore(List<SemesterSubjectScoreSet> subjectSetList, List<SubjectScore> retakeList)
        {
            List<SubjectScore> removed = new List<SubjectScore>();

            IOrderedEnumerable<SemesterSubjectScoreSet> sslist =
                from ss in subjectSetList
                orderby ss.Grade, ss.SchoolYear, ss.Semester
                select ss;

            foreach (SemesterSubjectScoreSet ssss in sslist)
            {
                foreach (SubjectScore ress in retakeList)
                {
                    foreach (SubjectScore ss in ssss.Subjects.ToArray())
                    {
                        if (ss.SchoolYear != ress.SchoolYear)
                            continue;

                        if (ss.Semester != ress.Semester)
                            continue;

                        if (ss.Grade != ress.Grade)
                            continue;

                        if (ss.Name != ress.Name)
                            continue;

                        if (ss.Level != ress.Level)
                            continue;

                        ssss.Subjects.Remove(ss);
                        removed.Add(ss);
                    }
                }
            }

            if (retakeList.Count != removed.Count)
                Console.WriteLine("科目成績有問題：" + subjectSetList[0].Self.學號); //到這表示有問題…

            foreach (SemesterSubjectScoreSet ssss in sslist)
            {
                foreach (SubjectScore ress in retakeList) //要決定用哪邊的資料。
                {
                    foreach (SubjectScore ss in ssss.Subjects.ToArray())
                    {
                        if (ss.Name != ress.Name)
                            continue;

                        if (ss.Level != ress.Level)
                            continue;

                        ss.RetakeScore = ress.OriginScore;//重修成績。

                        if (ss.RetakeScore >= 60)
                            ss.Passed = true;
                    }
                }
            }

        }
    }
}
