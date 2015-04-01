using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace OneAdminIntegration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            web.WebView.AfterReceiveHeaders += WebView_AfterReceiveHeaders;
        }

        void WebView_AfterReceiveHeaders(object sender, EO.WebBrowser.ResponseEventArgs e)
        {
            foreach (string key in e.Response.RawHeaders.AllKeys)
            {
                txtHistory.AppendText(key + "\n");
            }
        }

        private void go_Click(object sender, EventArgs e)
        {
            //web.Url = new Uri(url.Text);
            web.WebView.Url = url.Text;

            txtHistory.AppendText("\nGo：\n" + web.WebView.Url + "\n");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(web.Version.ToString());
            //web.ScriptErrorsSuppressed = false;
        }

        private void web_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //Clipboard.SetText(e.Url.ToString());
            //MessageBox.Show(e.Url.ToString());
            txtHistory.AppendText("\nNavigating：\n" + e.Url.ToString() + "\n");
        }

        private void web_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //MessageBox.Show("Navigated：\n" + e.Url.ToString());
            txtHistory.AppendText("\nNavigated：\n" + e.Url.ToString() + "\n");
        }
    }
}
