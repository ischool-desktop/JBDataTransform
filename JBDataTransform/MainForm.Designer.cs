namespace JBDataTransform
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.btnStudent = new System.Windows.Forms.Button();
            this.btnSubjectScore = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "UserID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(28, 156);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(104, 23);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "班級(測試)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::JBDataTransform.Properties.Settings.Default, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPassword.Location = new System.Drawing.Point(80, 117);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(183, 22);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.Text = global::JBDataTransform.Properties.Settings.Default.Password;
            // 
            // txtUserID
            // 
            this.txtUserID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::JBDataTransform.Properties.Settings.Default, "UserID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtUserID.Location = new System.Drawing.Point(80, 83);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(183, 22);
            this.txtUserID.TabIndex = 5;
            this.txtUserID.Text = global::JBDataTransform.Properties.Settings.Default.UserID;
            // 
            // txtDatabase
            // 
            this.txtDatabase.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::JBDataTransform.Properties.Settings.Default, "Database", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDatabase.Location = new System.Drawing.Point(80, 51);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(183, 22);
            this.txtDatabase.TabIndex = 3;
            this.txtDatabase.Text = global::JBDataTransform.Properties.Settings.Default.Database;
            // 
            // txtServer
            // 
            this.txtServer.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::JBDataTransform.Properties.Settings.Default, "Server", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtServer.Location = new System.Drawing.Point(80, 20);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(183, 22);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = global::JBDataTransform.Properties.Settings.Default.Server;
            // 
            // btnStudent
            // 
            this.btnStudent.Location = new System.Drawing.Point(28, 185);
            this.btnStudent.Name = "btnStudent";
            this.btnStudent.Size = new System.Drawing.Size(104, 23);
            this.btnStudent.TabIndex = 9;
            this.btnStudent.Text = "學生";
            this.btnStudent.UseVisualStyleBackColor = true;
            this.btnStudent.Click += new System.EventHandler(this.btnStudent_Click);
            // 
            // btnSubjectScore
            // 
            this.btnSubjectScore.Location = new System.Drawing.Point(28, 214);
            this.btnSubjectScore.Name = "btnSubjectScore";
            this.btnSubjectScore.Size = new System.Drawing.Size(104, 23);
            this.btnSubjectScore.TabIndex = 10;
            this.btnSubjectScore.Text = "科目學期成績";
            this.btnSubjectScore.UseVisualStyleBackColor = true;
            this.btnSubjectScore.Click += new System.EventHandler(this.btnSubjectScore_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(138, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "學生缺曠資料";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnAttendance_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(138, 185);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "學生獎懲資料";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 252);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSubjectScore);
            this.Controls.Add(this.btnStudent);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtServer);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "喬柏系統匯出";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnStudent;
        private System.Windows.Forms.Button btnSubjectScore;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

