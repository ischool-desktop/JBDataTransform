namespace OneAdminIntegration
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.go = new System.Windows.Forms.Button();
            this.url = new System.Windows.Forms.TextBox();
            this.txtHistory = new System.Windows.Forms.TextBox();
            this.web = new EO.WebBrowser.WinForm.WebControl();
            this.webView1 = new EO.WebBrowser.WebView();
            this.SuspendLayout();
            // 
            // go
            // 
            this.go.Location = new System.Drawing.Point(878, 608);
            this.go.Name = "go";
            this.go.Size = new System.Drawing.Size(75, 23);
            this.go.TabIndex = 1;
            this.go.Text = "GO";
            this.go.UseVisualStyleBackColor = true;
            this.go.Click += new System.EventHandler(this.go_Click);
            // 
            // url
            // 
            this.url.Dock = System.Windows.Forms.DockStyle.Top;
            this.url.Location = new System.Drawing.Point(0, 0);
            this.url.Name = "url";
            this.url.Size = new System.Drawing.Size(956, 22);
            this.url.TabIndex = 2;
            // 
            // txtHistory
            // 
            this.txtHistory.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtHistory.Location = new System.Drawing.Point(12, 454);
            this.txtHistory.Multiline = true;
            this.txtHistory.Name = "txtHistory";
            this.txtHistory.Size = new System.Drawing.Size(377, 177);
            this.txtHistory.TabIndex = 3;
            // 
            // web
            // 
            this.web.BackColor = System.Drawing.Color.White;
            this.web.Dock = System.Windows.Forms.DockStyle.Top;
            this.web.Location = new System.Drawing.Point(0, 22);
            this.web.Name = "web";
            this.web.Size = new System.Drawing.Size(956, 426);
            this.web.TabIndex = 4;
            this.web.Text = "webControl1";
            this.web.WebView = this.webView1;
            // 
            // webView1
            // 
            this.webView1.AllowDropLoad = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 638);
            this.Controls.Add(this.web);
            this.Controls.Add(this.txtHistory);
            this.Controls.Add(this.url);
            this.Controls.Add(this.go);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button go;
        private System.Windows.Forms.TextBox url;
        private System.Windows.Forms.TextBox txtHistory;
        private EO.WebBrowser.WinForm.WebControl web;
        private EO.WebBrowser.WebView webView1;
    }
}

