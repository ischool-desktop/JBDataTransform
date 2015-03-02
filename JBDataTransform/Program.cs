using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JBDataTransform
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Aspose.Cells.License lic = new Aspose.Cells.License();

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Properties.Resources.Aspose_Total))
            {
                lic.SetLicense(ms);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
