using System.Windows.Forms;

namespace VisualizationSystem.View.UserControls.Setting
{
    partial class ParametersSettings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParametersSettings));
            this.dataGridViewVariableParameters = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.AddRowButton = new System.Windows.Forms.Button();
            this.ParamLog = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.unloadAll = new System.Windows.Forms.ToolStripButton();
            this.loadAll = new System.Windows.Forms.ToolStripButton();
            this.getInformationButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteRowButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVariableParameters)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewVariableParameters
            // 
            this.dataGridViewVariableParameters.AllowUserToAddRows = false;
            this.dataGridViewVariableParameters.AllowUserToDeleteRows = false;
            this.dataGridViewVariableParameters.AllowUserToResizeColumns = false;
            this.dataGridViewVariableParameters.AllowUserToResizeRows = false;
            this.dataGridViewVariableParameters.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewVariableParameters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewVariableParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVariableParameters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5,
            this.Column4});
            this.dataGridViewVariableParameters.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridViewVariableParameters.Location = new System.Drawing.Point(20, 40);
            this.dataGridViewVariableParameters.Name = "dataGridViewVariableParameters";
            this.dataGridViewVariableParameters.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewVariableParameters.Size = new System.Drawing.Size(719, 357);
            this.dataGridViewVariableParameters.TabIndex = 0;
            this.dataGridViewVariableParameters.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewVariableParameters_CellMouseDown);
            this.dataGridViewVariableParameters.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewVariableParameters_EditingControlShowing);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "№";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Индекс";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Название";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.Width = 250;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Тип";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Значение";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.Width = 155;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(136, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuItem1.Text = "Записать";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuItem2.Text = "Читать";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuItem3.Text = "Развернуть";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // AddRowButton
            // 
            this.AddRowButton.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.AddRowButton.FlatAppearance.BorderSize = 0;
            this.AddRowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddRowButton.Font = new System.Drawing.Font("Cambria", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddRowButton.ForeColor = System.Drawing.Color.Silver;
            this.AddRowButton.Location = new System.Drawing.Point(623, 402);
            this.AddRowButton.Name = "AddRowButton";
            this.AddRowButton.Size = new System.Drawing.Size(113, 34);
            this.AddRowButton.TabIndex = 2;
            this.AddRowButton.Text = "Добавить";
            this.AddRowButton.UseVisualStyleBackColor = false;
            this.AddRowButton.Click += new System.EventHandler(this.AddRowButton_Click);
            // 
            // ParamLog
            // 
            this.ParamLog.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ParamLog.Location = new System.Drawing.Point(20, 440);
            this.ParamLog.Name = "ParamLog";
            this.ParamLog.ReadOnly = true;
            this.ParamLog.Size = new System.Drawing.Size(719, 96);
            this.ParamLog.TabIndex = 4;
            this.ParamLog.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSave,
            this.toolStripButtonOpen,
            this.unloadAll,
            this.loadAll,
            this.getInformationButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(758, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSave.Text = "Сохранить";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpen.Text = "Открыть";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // unloadAll
            // 
            this.unloadAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.unloadAll.Image = ((System.Drawing.Image)(resources.GetObject("unloadAll.Image")));
            this.unloadAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.unloadAll.Name = "unloadAll";
            this.unloadAll.Size = new System.Drawing.Size(23, 22);
            this.unloadAll.Text = "Прочитать все";
            this.unloadAll.Click += new System.EventHandler(this.unloadAll_Click);
            // 
            // loadAll
            // 
            this.loadAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadAll.Image = ((System.Drawing.Image)(resources.GetObject("loadAll.Image")));
            this.loadAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadAll.Name = "loadAll";
            this.loadAll.Size = new System.Drawing.Size(23, 22);
            this.loadAll.Text = "Записать все";
            this.loadAll.Click += new System.EventHandler(this.loadAll_Click);
            // 
            // getInformationButton
            // 
            this.getInformationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.getInformationButton.Image = ((System.Drawing.Image)(resources.GetObject("getInformationButton.Image")));
            this.getInformationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.getInformationButton.Name = "getInformationButton";
            this.getInformationButton.Size = new System.Drawing.Size(23, 22);
            this.getInformationButton.Text = "получить информацию об аппаратуре";
            this.getInformationButton.Click += new System.EventHandler(this.getInformationButton_Click);
            // 
            // DeleteRowButton
            // 
            this.DeleteRowButton.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.DeleteRowButton.FlatAppearance.BorderSize = 0;
            this.DeleteRowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteRowButton.Font = new System.Drawing.Font("Cambria", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteRowButton.ForeColor = System.Drawing.Color.Silver;
            this.DeleteRowButton.Location = new System.Drawing.Point(504, 402);
            this.DeleteRowButton.Name = "DeleteRowButton";
            this.DeleteRowButton.Size = new System.Drawing.Size(113, 34);
            this.DeleteRowButton.TabIndex = 6;
            this.DeleteRowButton.Text = "Удалить";
            this.DeleteRowButton.UseVisualStyleBackColor = false;
            this.DeleteRowButton.Click += new System.EventHandler(this.DeleteRowButton_Click);
            // 
            // ParametersSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.DeleteRowButton);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ParamLog);
            this.Controls.Add(this.AddRowButton);
            this.Controls.Add(this.dataGridViewVariableParameters);
            this.Name = "ParametersSettings";
            this.Size = new System.Drawing.Size(758, 541);
            this.Load += new System.EventHandler(this.ParametersSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVariableParameters)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewVariableParameters;
        private System.Windows.Forms.Button AddRowButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private RichTextBox ParamLog;
        private ToolStrip toolStrip1;
        private ToolStripMenuItem toolStripMenuItem3;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column4;
        private Button DeleteRowButton;
        private ToolStripButton toolStripButtonSave;
        private ToolStripButton toolStripButtonOpen;
        private ToolStripButton unloadAll;
        private ToolStripButton loadAll;
        private ToolStripButton getInformationButton;
    }
}
