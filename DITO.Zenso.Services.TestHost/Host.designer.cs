namespace DITO.Zenso.Services.TestHost
{
    partial class Host
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Host));
            this.tsOptions = new System.Windows.Forms.ToolStrip();
            this.tsbStart = new System.Windows.Forms.ToolStripButton();
            this.tsbStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbClean = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.tsOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsOptions
            // 
            this.tsOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbStart,
            this.tsbStop,
            this.toolStripSeparator2,
            this.tsbClean,
            this.toolStripSeparator1,
            this.tsbExit});
            this.tsOptions.Location = new System.Drawing.Point(0, 0);
            this.tsOptions.Name = "tsOptions";
            this.tsOptions.Size = new System.Drawing.Size(855, 25);
            this.tsOptions.TabIndex = 1;
            this.tsOptions.Text = "toolStrip1";
            // 
            // tsbStart
            // 
            this.tsbStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbStart.Image = ((System.Drawing.Image)(resources.GetObject("tsbStart.Image")));
            this.tsbStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStart.Name = "tsbStart";
            this.tsbStart.Size = new System.Drawing.Size(43, 22);
            this.tsbStart.Text = "Iniciar";
            this.tsbStart.ToolTipText = "Iniciar";
            this.tsbStart.Click += new System.EventHandler(this.GeneralActionClickHandler);
            // 
            // tsbStop
            // 
            this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbStop.Image")));
            this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStop.Name = "tsbStop";
            this.tsbStop.Size = new System.Drawing.Size(52, 22);
            this.tsbStop.Text = "Detener";
            this.tsbStop.ToolTipText = "Detener";
            this.tsbStop.Click += new System.EventHandler(this.GeneralActionClickHandler);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbClean
            // 
            this.tsbClean.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClean.Image = ((System.Drawing.Image)(resources.GetObject("tsbClean.Image")));
            this.tsbClean.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClean.Name = "tsbClean";
            this.tsbClean.Size = new System.Drawing.Size(83, 22);
            this.tsbClean.Text = "Limpiar Texto";
            this.tsbClean.Click += new System.EventHandler(this.GeneralActionClickHandler);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExit
            // 
            this.tsbExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbExit.Image")));
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(33, 22);
            this.tsbExit.Text = "Salir";
            this.tsbExit.Click += new System.EventHandler(this.GeneralActionClickHandler);
            // 
            // rtbOutput
            // 
            this.rtbOutput.BackColor = System.Drawing.Color.GhostWhite;
            this.rtbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOutput.Location = new System.Drawing.Point(0, 25);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.ReadOnly = true;
            this.rtbOutput.Size = new System.Drawing.Size(855, 189);
            this.rtbOutput.TabIndex = 3;
            this.rtbOutput.Text = "";
            // 
            // Host
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 214);
            this.Controls.Add(this.rtbOutput);
            this.Controls.Add(this.tsOptions);
            this.MaximizeBox = false;
            this.Name = "Host";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Services Host";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GeneralActionClickHandler);
            this.Shown += new System.EventHandler(this.GeneralActionClickHandler);
            this.tsOptions.ResumeLayout(false);
            this.tsOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsOptions;
        private System.Windows.Forms.ToolStripButton tsbStart;
        private System.Windows.Forms.ToolStripButton tsbStop;
        private System.Windows.Forms.ToolStripButton tsbClean;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;



    }
}

