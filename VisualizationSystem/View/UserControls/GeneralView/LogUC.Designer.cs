using System.Windows.Forms;

namespace VisualizationSystem.View.UserControls.GeneralView
{
    partial class LogUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GeneralLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // GeneralLog
            // 
            this.GeneralLog.BackColor = System.Drawing.Color.WhiteSmoke;
            this.GeneralLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GeneralLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GeneralLog.Location = new System.Drawing.Point(0, 0);
            this.GeneralLog.Name = "GeneralLog";
            this.GeneralLog.ReadOnly = true;
            this.GeneralLog.Size = new System.Drawing.Size(500, 419);
            this.GeneralLog.TabIndex = 0;
            this.GeneralLog.Text = "";
            // 
            // LogUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.GeneralLog);
            this.Name = "LogUC";
            this.Size = new System.Drawing.Size(500, 419);
            this.Load += new System.EventHandler(this.LogUC_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox GeneralLog;
    }
}
