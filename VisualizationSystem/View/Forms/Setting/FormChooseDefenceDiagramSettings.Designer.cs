namespace VisualizationSystem.View.Forms.Setting
{
    partial class FormChooseDefenceDiagramSettings
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
            this.ChooseDiagramComboBox = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelChooseDiagram = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ChooseDiagramComboBox
            // 
            this.ChooseDiagramComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ChooseDiagramComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChooseDiagramComboBox.FormattingEnabled = true;
            this.ChooseDiagramComboBox.Location = new System.Drawing.Point(12, 43);
            this.ChooseDiagramComboBox.Name = "ChooseDiagramComboBox";
            this.ChooseDiagramComboBox.Size = new System.Drawing.Size(279, 21);
            this.ChooseDiagramComboBox.TabIndex = 3;
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Font = new System.Drawing.Font("Cambria", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOK.ForeColor = System.Drawing.Color.Silver;
            this.buttonOK.Location = new System.Drawing.Point(37, 75);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(68, 35);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Cambria", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.ForeColor = System.Drawing.Color.Silver;
            this.buttonCancel.Location = new System.Drawing.Point(130, 75);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(141, 35);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отменить";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelChooseDiagram
            // 
            this.labelChooseDiagram.AutoSize = true;
            this.labelChooseDiagram.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelChooseDiagram.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelChooseDiagram.Location = new System.Drawing.Point(51, 9);
            this.labelChooseDiagram.Name = "labelChooseDiagram";
            this.labelChooseDiagram.Size = new System.Drawing.Size(208, 22);
            this.labelChooseDiagram.TabIndex = 8;
            this.labelChooseDiagram.Text = "Выберите тахограмму";
            // 
            // FormChooseDefenceDiagramSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(302, 125);
            this.Controls.Add(this.labelChooseDiagram);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.ChooseDiagramComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChooseDefenceDiagramSettings";
            this.Text = "Выбор тахограммы";
            this.Load += new System.EventHandler(this.FormChooseDefenceDiagramSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ChooseDiagramComboBox;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelChooseDiagram;
    }
}