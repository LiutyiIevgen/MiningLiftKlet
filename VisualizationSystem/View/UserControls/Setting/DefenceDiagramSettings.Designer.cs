namespace VisualizationSystem.View.UserControls.Setting
{
    partial class DefenceDiagramSettings
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelSolveDefenceDiagram = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainerCodtDomain = new System.Windows.Forms.SplitContainer();
            this.CodtDomainComboBox = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCodtDomain)).BeginInit();
            this.splitContainerCodtDomain.Panel1.SuspendLayout();
            this.splitContainerCodtDomain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(758, 448);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DarkGray;
            this.tabPage1.Controls.Add(this.panelSolveDefenceDiagram);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(750, 422);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Расчёт ЗД";
            // 
            // panelSolveDefenceDiagram
            // 
            this.panelSolveDefenceDiagram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSolveDefenceDiagram.Location = new System.Drawing.Point(3, 3);
            this.panelSolveDefenceDiagram.Name = "panelSolveDefenceDiagram";
            this.panelSolveDefenceDiagram.Size = new System.Drawing.Size(744, 416);
            this.panelSolveDefenceDiagram.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.DarkGray;
            this.tabPage2.Controls.Add(this.splitContainerCodtDomain);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(750, 422);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "codtDomain";
            // 
            // splitContainerCodtDomain
            // 
            this.splitContainerCodtDomain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerCodtDomain.Location = new System.Drawing.Point(3, 3);
            this.splitContainerCodtDomain.Name = "splitContainerCodtDomain";
            this.splitContainerCodtDomain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerCodtDomain.Panel1
            // 
            this.splitContainerCodtDomain.Panel1.Controls.Add(this.CodtDomainComboBox);
            this.splitContainerCodtDomain.Size = new System.Drawing.Size(744, 416);
            this.splitContainerCodtDomain.SplitterDistance = 25;
            this.splitContainerCodtDomain.TabIndex = 1;
            // 
            // CodtDomainComboBox
            // 
            this.CodtDomainComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CodtDomainComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CodtDomainComboBox.FormattingEnabled = true;
            this.CodtDomainComboBox.Location = new System.Drawing.Point(192, 3);
            this.CodtDomainComboBox.Name = "CodtDomainComboBox";
            this.CodtDomainComboBox.Size = new System.Drawing.Size(374, 21);
            this.CodtDomainComboBox.TabIndex = 2;
            this.CodtDomainComboBox.SelectedIndexChanged += new System.EventHandler(this.CodtDomainComboBox_SelectedIndexChanged);
            // 
            // DefenceDiagramSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.tabControl1);
            this.Name = "DefenceDiagramSettings";
            this.Size = new System.Drawing.Size(758, 448);
            this.Load += new System.EventHandler(this.DefenceDiagramSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainerCodtDomain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCodtDomain)).EndInit();
            this.splitContainerCodtDomain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainerCodtDomain;
        private System.Windows.Forms.ComboBox CodtDomainComboBox;
        private System.Windows.Forms.Panel panelSolveDefenceDiagram;


    }
}
