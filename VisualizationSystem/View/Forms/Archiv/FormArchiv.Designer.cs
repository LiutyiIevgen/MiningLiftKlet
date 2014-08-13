namespace VisualizationSystem.View.Forms.Archiv
{
    partial class FormArchiv
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuArchiv = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSignals = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuArchiv.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1023, 566);
            this.panel1.TabIndex = 0;
            // 
            // menuArchiv
            // 
            this.menuArchiv.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile});
            this.menuArchiv.Location = new System.Drawing.Point(0, 0);
            this.menuArchiv.Name = "menuArchiv";
            this.menuArchiv.Size = new System.Drawing.Size(1023, 24);
            this.menuArchiv.TabIndex = 1;
            this.menuArchiv.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSignals,
            this.toolStripMenuItemLog});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItemFile.Text = "File";
            // 
            // toolStripMenuItemSignals
            // 
            this.toolStripMenuItemSignals.Name = "toolStripMenuItemSignals";
            this.toolStripMenuItemSignals.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemSignals.Text = "Сигналы";
            this.toolStripMenuItemSignals.Click += new System.EventHandler(this.toolStripMenuItemSignals_Click);
            // 
            // toolStripMenuItemLog
            // 
            this.toolStripMenuItemLog.Name = "toolStripMenuItemLog";
            this.toolStripMenuItemLog.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemLog.Text = "Журнал";
            this.toolStripMenuItemLog.Click += new System.EventHandler(this.toolStripMenuItemLog_Click);
            // 
            // FormArchiv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 590);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuArchiv);
            this.MainMenuStrip = this.menuArchiv;
            this.Name = "FormArchiv";
            this.Text = "Архив";
            this.Load += new System.EventHandler(this.FormArchiv_Load);
            this.menuArchiv.ResumeLayout(false);
            this.menuArchiv.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuArchiv;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSignals;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLog;
    }
}