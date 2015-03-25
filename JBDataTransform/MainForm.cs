using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aspose.Cells;
using JBDataTransform.VObjects;

namespace JBDataTransform
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string connstr = GetConnectionString();
            string classCmd = Class.GetCommandText("103", "1");

            using (DataSource ds = new DataSource(connstr))
            {
                List<Class> classes = ds.Execute<Class>(classCmd);
                dynamic cls = classes[0];
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            string connstr = GetConnectionString();

            string cmd = Student.Command;
            using (DataSource ds = new DataSource(connstr))
            {
                List<Student> records = ds.Execute<Student>(cmd);

                if (records.Count <= 0)
                {
                    Console.WriteLine("沒有資料！\n" + cmd);
                    return;
                }

                Workbook wb = new Workbook();
                wb.Worksheets.Clear();
                Worksheet ws = wb.Worksheets[wb.Worksheets.Add()];

                Dictionary<string, int> columns = Util.FillFields(ws, records[0].DataFields);

                Util.FillData<Student>(ws, columns, records, (stu, field) =>
                {
                    if (field == "生日")
                        return stu.Birthdate;

                    if (field == "性別")
                        return stu.Gender;

                    if (field == "班級")
                        return stu.ClassName;

                    return null;
                });

                wb.Save("student.xls", SaveFormat.Excel97To2003);
            }

            MessageBox.Show("完成！");
        }

        private string GetConnectionString()
        {
            string connstr = string.Format("Server={0};Database={1};User Id={2};Password={3};",
                txtServer.Text, txtDatabase.Text, txtUserID.Text, txtPassword.Text);
            return connstr;
        }

        private void btnSubjectScore_Click(object sender, EventArgs e)
        {
            string connstr = GetConnectionString();

            string cmd = SemesterSubjectScoreSet.Command;
            string cmd2 = RetakeSubjectScore.Command;
            string cmd3 = Subject.Command;

            using (DataSource ds = new DataSource(connstr))
            {
                List<SemesterSubjectScoreSet> studentSubjScoreSets = ds.Execute<SemesterSubjectScoreSet>(cmd);
                List<RetakeSubjectScore> retakes = ds.Execute<RetakeSubjectScore>(cmd2);
                List<Subject> subjects = ds.Execute<Subject>(cmd3);

                Dictionary<string, Subject> subjectMap = new Dictionary<string, Subject>();
                foreach (Subject subj in subjects)
                    subjectMap.Add(subj.Self.代碼, subj);

                //重修學生清單。
                Dictionary<string, List<SubjectScore>> studRetakes = new Dictionary<string, List<SubjectScore>>();
                foreach (RetakeSubjectScore retake in retakes)
                {
                    #region Convert
                    string studNum = retake.Self.學號;
                    SubjectScore ss = new SubjectScore();

                    if (!studRetakes.ContainsKey(studNum))
                        studRetakes.Add(studNum, new List<SubjectScore>());

                    ss.SchoolYear = Util.GetDigit(retake.Self.學年度);
                    ss.Semester = Util.GetDigit(retake.Self.學期);
                    ss.Grade = Util.GetDigit(retake.Self.年級);

                    string[] parts = null;

                    parts = retake.Self.科目.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    ss.Code = parts[0].Trim();

                    string subjName = Util.GetSubjectName(subjectMap, ss.Code, parts[1]);

                    Tuple<string, int> sname = Util.ParseSubjectName(subjName);

                    if (sname == null)
                        continue;

                    ss.Name = sname.Item1;
                    ss.Level = sname.Item2;

                    parts = retake.Self.成績.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    decimal originScore;
                    if (!decimal.TryParse(parts[0], out originScore)) //沒有原始成績，接下來不執行。
                        continue;

                    ss.OriginScore = originScore; //原始成績。

                    studRetakes[studNum].Add(ss);
                    #endregion
                }

                if (studentSubjScoreSets.Count <= 0 || retakes.Count <= 0)
                {
                    Console.WriteLine("沒有資料！\n" + cmd);
                    return;
                }

                Workbook wb = new Workbook();
                wb.Worksheets.Clear();
                Worksheet ws = wb.Worksheets[wb.Worksheets.Add()];
                Worksheet wsEntry = wb.Worksheets[wb.Worksheets.Add()];

                Dictionary<string, List<SemesterSubjectScoreSet>> students = new Dictionary<string, List<SemesterSubjectScoreSet>>();
                foreach (SemesterSubjectScoreSet ssss in studentSubjScoreSets)
                {
                    string stunum = ssss.Self.學號;
                    if (!students.ContainsKey(stunum))
                        students.Add(stunum, new List<SemesterSubjectScoreSet>());

                    ssss.ParseSubjectScoreData(subjectMap);

                    students[stunum].Add(ssss);
                }

                foreach (KeyValuePair<string, List<SemesterSubjectScoreSet>> each in students)
                {
                    if (!studRetakes.ContainsKey(each.Key))
                        continue;

                    SemesterSubjectScoreSet.CalculateRetakeScore(each.Value, studRetakes[each.Key]);
                }

                string[] subjFields = new string[] {
                     "學號","學年度","學期","成績年級","科目","科目級別","原始成績","補考成績",
                    "重修成績","必選修","校部訂","取得學分","學分數","分項類別"};

                string[] entryFields = new string[]{
                    "學號","學年度","學期","成績年級","學業"
                };

                Dictionary<string, int> subjColumns = Util.FillFields(ws, subjFields);
                Dictionary<string, int> entryColumns = Util.FillFields(wsEntry, entryFields);

                int subjRowIdx = 1, entryRowIdx = 1;
                foreach (SemesterSubjectScoreSet ssss in studentSubjScoreSets)
                {
                    wsEntry.Cells[entryRowIdx, entryColumns["學號"]].PutValue(ssss.Self.學號);
                    wsEntry.Cells[entryRowIdx, entryColumns["學年度"]].PutValue(ssss.SchoolYear);
                    wsEntry.Cells[entryRowIdx, entryColumns["學期"]].PutValue(ssss.Semester);
                    wsEntry.Cells[entryRowIdx, entryColumns["成績年級"]].PutValue(ssss.Grade);
                    wsEntry.Cells[entryRowIdx, entryColumns["學業"]].PutValue(ssss.Self.學業成績);

                    entryRowIdx++;

                    foreach (SubjectScore subjScore in ssss.Subjects)
                    {
                        ws.Cells[subjRowIdx, subjColumns["學號"]].PutValue(ssss.Self.學號);
                        ws.Cells[subjRowIdx, subjColumns["學年度"]].PutValue(ssss.SchoolYear);
                        ws.Cells[subjRowIdx, subjColumns["學期"]].PutValue(ssss.Semester);
                        ws.Cells[subjRowIdx, subjColumns["成績年級"]].PutValue(ssss.Grade);
                        ws.Cells[subjRowIdx, subjColumns["科目"]].PutValue(subjScore.Name);
                        ws.Cells[subjRowIdx, subjColumns["科目級別"]].PutValue(subjScore.Level);
                        ws.Cells[subjRowIdx, subjColumns["原始成績"]].PutValue(subjScore.OriginScore);
                        ws.Cells[subjRowIdx, subjColumns["補考成績"]].PutValue(subjScore.MakeupScore);
                        ws.Cells[subjRowIdx, subjColumns["重修成績"]].PutValue(subjScore.RetakeScore);
                        ws.Cells[subjRowIdx, subjColumns["必選修"]].PutValue(subjScore.Required);
                        ws.Cells[subjRowIdx, subjColumns["校部訂"]].PutValue(subjScore.Decision);
                        ws.Cells[subjRowIdx, subjColumns["取得學分"]].PutValue(subjScore.PassString);
                        ws.Cells[subjRowIdx, subjColumns["學分數"]].PutValue(subjScore.Credit);
                        ws.Cells[subjRowIdx, subjColumns["分項類別"]].PutValue(subjScore.EntryGroupString);

                        subjRowIdx++;
                    }
                }

                wb.Save("SemesterSubjectEntryScore.xls", SaveFormat.Excel97To2003);
            }

            MessageBox.Show("完成！");
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            string connstr = GetConnectionString();

            string cmd = Attendance.Command;
            using (DataSource ds = new DataSource(connstr))
            {
                List<Attendance> records = ds.Execute<Attendance>(cmd);

                if (records.Count <= 0)
                {
                    Console.WriteLine("沒有資料！\n" + cmd);
                    return;
                }

                Workbook wb = new Workbook();
                wb.Worksheets.Clear();
                Worksheet ws = wb.Worksheets[wb.Worksheets.Add()];

                //學生系統編號	學號	班級	座號	科別	姓名	學年度	學期	日期	缺曠假別	缺曠節次
                string[] fields = new string[] { "學號", "學年度", "學期", "日期", "缺曠節次", "缺曠假別" };
                Dictionary<string, int> columns = Util.FillFields(ws, fields);

                int rowIdx = 1;
                foreach (Attendance rec in records)
                {
                    int startPeriod = rec.StartPeriod;

                    if (startPeriod <= 0)
                    {
                        string ps = Attendance.GetChinesePeriodString(rec.StartPeriodString);

                        ws.Cells[rowIdx, columns["學號"]].PutValue(rec.Self.學號);
                        ws.Cells[rowIdx, columns["學年度"]].PutValue(rec.Self.學年度);
                        ws.Cells[rowIdx, columns["學期"]].PutValue(rec.Self.學期);
                        ws.Cells[rowIdx, columns["日期"]].PutValue(rec.Date);
                        ws.Cells[rowIdx, columns["缺曠節次"]].PutValue(ps);
                        ws.Cells[rowIdx, columns["缺曠假別"]].PutValue(rec.AbsenceType);

                        rowIdx++;
                    }
                    else
                    {
                        for (int i = 0; i < rec.PeriodCount; i++)
                        {
                            string ps = Attendance.GetChinesePeriodString((startPeriod + i) + "");

                            ws.Cells[rowIdx, columns["學號"]].PutValue(rec.Self.學號);
                            ws.Cells[rowIdx, columns["學年度"]].PutValue(rec.Self.學年度);
                            ws.Cells[rowIdx, columns["學期"]].PutValue(rec.Self.學期);
                            ws.Cells[rowIdx, columns["日期"]].PutValue(rec.Date);
                            ws.Cells[rowIdx, columns["缺曠節次"]].PutValue(ps);
                            ws.Cells[rowIdx, columns["缺曠假別"]].PutValue(rec.AbsenceType);

                            rowIdx++;
                        }
                    }
                }

                string fn = "attendance.xls";

                wb.Save(fn, SaveFormat.Excel97To2003);
                Process.Start(fn);
            }

            MessageBox.Show("完成！");
        }
    }
}
