namespace Edit_Exclude_Dict
{
    partial class About
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
            this.rtbAbout = new System.Windows.Forms.RichTextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnViewLicense = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbAbout
            // 
            this.rtbAbout.Location = new System.Drawing.Point(12, 12);
            this.rtbAbout.Name = "rtbAbout";
            this.rtbAbout.ReadOnly = true;
            this.rtbAbout.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbAbout.Size = new System.Drawing.Size(776, 389);
            this.rtbAbout.TabIndex = 2;
            this.rtbAbout.Text = "";
            this.rtbAbout.TextChanged += new System.EventHandler(this.rtbAbout_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(712, 410);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 32);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnViewLicense
            // 
            this.btnViewLicense.Location = new System.Drawing.Point(12, 410);
            this.btnViewLicense.Name = "btnViewLicense";
            this.btnViewLicense.Size = new System.Drawing.Size(119, 32);
            this.btnViewLicense.TabIndex = 1;
            this.btnViewLicense.Text = "View License";
            this.btnViewLicense.UseVisualStyleBackColor = true;
            this.btnViewLicense.Click += new System.EventHandler(this.btnViewLicense_Click);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnViewLicense);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.rtbAbout);
            this.Name = "About";
            this.Text = "About";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbAbout;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnViewLicense;
    }
}
