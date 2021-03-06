﻿namespace JordvarmeMonitor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.XuWeBrowser = new System.Windows.Forms.WebBrowser();
            this.XuLogin = new System.Windows.Forms.Button();
            this.XuSchema = new System.Windows.Forms.Button();
            this.XuAnalyzeDom = new System.Windows.Forms.Button();
            this.XuTimer = new System.Windows.Forms.Timer(this.components);
            this.XuTimer1 = new System.Windows.Forms.Button();
            this.XuStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // XuWeBrowser
            // 
            this.XuWeBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XuWeBrowser.Location = new System.Drawing.Point(0, 0);
            this.XuWeBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.XuWeBrowser.Name = "XuWeBrowser";
            this.XuWeBrowser.Size = new System.Drawing.Size(1067, 874);
            this.XuWeBrowser.TabIndex = 0;
            this.XuWeBrowser.Url = new System.Uri("https://cmi.ta.co.at/webi/CMI004096/schema.html#1", System.UriKind.Absolute);
            this.XuWeBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.XuWeBrowser_DocumentCompleted);
            // 
            // XuLogin
            // 
            this.XuLogin.Location = new System.Drawing.Point(25, 27);
            this.XuLogin.Name = "XuLogin";
            this.XuLogin.Size = new System.Drawing.Size(75, 23);
            this.XuLogin.TabIndex = 1;
            this.XuLogin.Text = "Login";
            this.XuLogin.UseVisualStyleBackColor = true;
            this.XuLogin.Click += new System.EventHandler(this.XuLogin_Click);
            // 
            // XuSchema
            // 
            this.XuSchema.Location = new System.Drawing.Point(25, 93);
            this.XuSchema.Name = "XuSchema";
            this.XuSchema.Size = new System.Drawing.Size(75, 23);
            this.XuSchema.TabIndex = 2;
            this.XuSchema.Text = "Schema";
            this.XuSchema.UseVisualStyleBackColor = true;
            this.XuSchema.Click += new System.EventHandler(this.XuSchema_Click);
            // 
            // XuAnalyzeDom
            // 
            this.XuAnalyzeDom.Location = new System.Drawing.Point(25, 163);
            this.XuAnalyzeDom.Name = "XuAnalyzeDom";
            this.XuAnalyzeDom.Size = new System.Drawing.Size(75, 23);
            this.XuAnalyzeDom.TabIndex = 3;
            this.XuAnalyzeDom.Text = "DOM";
            this.XuAnalyzeDom.UseVisualStyleBackColor = true;
            this.XuAnalyzeDom.Click += new System.EventHandler(this.XuAnalyzeDom_Click);
            // 
            // XuTimer
            // 
            this.XuTimer.Interval = 2000;
            this.XuTimer.Tick += new System.EventHandler(this.XuTimer_Tick);
            // 
            // XuTimer1
            // 
            this.XuTimer1.Location = new System.Drawing.Point(25, 239);
            this.XuTimer1.Name = "XuTimer1";
            this.XuTimer1.Size = new System.Drawing.Size(75, 23);
            this.XuTimer1.TabIndex = 4;
            this.XuTimer1.Text = "Timer";
            this.XuTimer1.UseVisualStyleBackColor = true;
            this.XuTimer1.Click += new System.EventHandler(this.XuTimer1_Click);
            // 
            // XuStop
            // 
            this.XuStop.Location = new System.Drawing.Point(25, 300);
            this.XuStop.Name = "XuStop";
            this.XuStop.Size = new System.Drawing.Size(75, 23);
            this.XuStop.TabIndex = 5;
            this.XuStop.Text = "Stop T";
            this.XuStop.UseVisualStyleBackColor = true;
            this.XuStop.Click += new System.EventHandler(this.XuStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 874);
            this.Controls.Add(this.XuStop);
            this.Controls.Add(this.XuTimer1);
            this.Controls.Add(this.XuAnalyzeDom);
            this.Controls.Add(this.XuSchema);
            this.Controls.Add(this.XuLogin);
            this.Controls.Add(this.XuWeBrowser);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser XuWeBrowser;
        private System.Windows.Forms.Button XuLogin;
        private System.Windows.Forms.Button XuSchema;
        private System.Windows.Forms.Button XuAnalyzeDom;
        private System.Windows.Forms.Timer XuTimer;
        private System.Windows.Forms.Button XuTimer1;
        private System.Windows.Forms.Button XuStop;
    }
}

