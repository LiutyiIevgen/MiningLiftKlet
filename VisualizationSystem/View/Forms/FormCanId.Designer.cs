namespace VisualizationSystem.View.Forms
{
    partial class FormCanId
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
            this.buttonAddress = new System.Windows.Forms.Button();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonAddress
            // 
            this.buttonAddress.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.buttonAddress.FlatAppearance.BorderSize = 0;
            this.buttonAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddress.Font = new System.Drawing.Font("Cambria", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddress.ForeColor = System.Drawing.Color.Silver;
            this.buttonAddress.Location = new System.Drawing.Point(60, 90);
            this.buttonAddress.Name = "buttonAddress";
            this.buttonAddress.Size = new System.Drawing.Size(57, 31);
            this.buttonAddress.TabIndex = 5;
            this.buttonAddress.Text = "OK";
            this.buttonAddress.UseVisualStyleBackColor = false;
            this.buttonAddress.Click += new System.EventHandler(this.buttonAddress_Click);
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxAddress.Location = new System.Drawing.Point(39, 47);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(100, 22);
            this.textBoxAddress.TabIndex = 4;
            this.textBoxAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSettingsParol_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(23, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Введите адрес";
            // 
            // FormCanId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(174, 138);
            this.Controls.Add(this.buttonAddress);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCanId";
            this.Text = "Адрес контроллера";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddress;
        public System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label label1;

    }
}