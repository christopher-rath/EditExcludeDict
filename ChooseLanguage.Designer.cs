namespace Edit_Exclude_Dict
{
    partial class ChooseLanguage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseLanguage));
            this.lbLanguageLists = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.cbRemeberSelection = new System.Windows.Forms.CheckBox();
            this.cbSelectLanguageGrps = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbLanguageLists
            // 
            this.lbLanguageLists.FormattingEnabled = true;
            this.lbLanguageLists.ItemHeight = 20;
            this.lbLanguageLists.Location = new System.Drawing.Point(12, 60);
            this.lbLanguageLists.Name = "lbLanguageLists";
            this.lbLanguageLists.Size = new System.Drawing.Size(776, 304);
            this.lbLanguageLists.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(762, 43);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "The following languages have an Exclude Dictionary on your computer.  Select the " +
    "lists to be edited during this session.";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(600, 390);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(699, 390);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 32);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "&Next";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // cbRemeberSelection
            // 
            this.cbRemeberSelection.AutoSize = true;
            this.cbRemeberSelection.Location = new System.Drawing.Point(24, 407);
            this.cbRemeberSelection.Name = "cbRemeberSelection";
            this.cbRemeberSelection.Size = new System.Drawing.Size(260, 24);
            this.cbRemeberSelection.TabIndex = 5;
            this.cbRemeberSelection.Text = "Remember Language Selection";
            this.cbRemeberSelection.UseVisualStyleBackColor = true;
            // 
            // cbSelectLanguageGrps
            // 
            this.cbSelectLanguageGrps.AutoSize = true;
            this.cbSelectLanguageGrps.Location = new System.Drawing.Point(24, 376);
            this.cbSelectLanguageGrps.Name = "cbSelectLanguageGrps";
            this.cbSelectLanguageGrps.Size = new System.Drawing.Size(239, 24);
            this.cbSelectLanguageGrps.TabIndex = 6;
            this.cbSelectLanguageGrps.Text = "Select Groups of Languages";
            this.cbSelectLanguageGrps.UseVisualStyleBackColor = true;
            // 
            // ChooseLanguage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 440);
            this.Controls.Add(this.cbSelectLanguageGrps);
            this.Controls.Add(this.cbRemeberSelection);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lbLanguageLists);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChooseLanguage";
            this.Text = "Choose Language";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbLanguageLists;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.CheckBox cbRemeberSelection;
        private System.Windows.Forms.CheckBox cbSelectLanguageGrps;
    }
}