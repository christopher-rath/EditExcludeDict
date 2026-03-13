#region Copyright
// Edit Exclude Dict -- A Microsoft Word add-in to edit the UProof Exclude Dictionary word lists.
// Author: Christopher Rath <christopher@rath.ca>
// Archived at: https://github.com/christopher-rath/EditExcludeDict
// Copyright 2024-2026 © Christopher Rath
// Distributed under the GNU Lesser General Public License v2.1
//     (see the license text).
// Warranty: None, see the license.
#endregion
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
            this.lblTitle = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.cbRemeberSelection = new System.Windows.Forms.CheckBox();
            this.cbSelectLanguageGrps = new System.Windows.Forms.CheckBox();
            this.lvLanguageLists = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblTitle.Enabled = false;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Multiline = true;
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.ReadOnly = true;
            this.lblTitle.Size = new System.Drawing.Size(500, 43);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "The following languages have an Exclude Dictionary on your computer.  Select the " +
    "lists to be edited during this session.";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(328, 424);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(423, 424);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 32);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "&Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // cbRemeberSelection
            // 
            this.cbRemeberSelection.AutoSize = true;
            this.cbRemeberSelection.Location = new System.Drawing.Point(24, 441);
            this.cbRemeberSelection.Name = "cbRemeberSelection";
            this.cbRemeberSelection.Size = new System.Drawing.Size(260, 24);
            this.cbRemeberSelection.TabIndex = 5;
            this.cbRemeberSelection.Text = "Remember Language Selection";
            this.cbRemeberSelection.UseVisualStyleBackColor = true;
            // 
            // cbSelectLanguageGrps
            // 
            this.cbSelectLanguageGrps.AutoSize = true;
            this.cbSelectLanguageGrps.Location = new System.Drawing.Point(24, 410);
            this.cbSelectLanguageGrps.Name = "cbSelectLanguageGrps";
            this.cbSelectLanguageGrps.Size = new System.Drawing.Size(239, 24);
            this.cbSelectLanguageGrps.TabIndex = 6;
            this.cbSelectLanguageGrps.Text = "Select Groups of Languages";
            this.cbSelectLanguageGrps.UseVisualStyleBackColor = true;
            // 
            // lvLanguageLists
            // 
            this.lvLanguageLists.CheckBoxes = true;
            this.lvLanguageLists.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvLanguageLists.HideSelection = false;
            this.lvLanguageLists.LabelWrap = false;
            this.lvLanguageLists.Location = new System.Drawing.Point(12, 58);
            this.lvLanguageLists.Name = "lvLanguageLists";
            this.lvLanguageLists.ShowGroups = false;
            this.lvLanguageLists.Size = new System.Drawing.Size(490, 343);
            this.lvLanguageLists.TabIndex = 7;
            this.lvLanguageLists.UseCompatibleStateImageBehavior = false;
            this.lvLanguageLists.View = System.Windows.Forms.View.Details;
            this.lvLanguageLists.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvLanguageLists_ItemChecked);
            // 
            // ChooseLanguage
            // 
            this.AcceptButton = this.btnNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(514, 474);
            this.Controls.Add(this.lvLanguageLists);
            this.Controls.Add(this.cbSelectLanguageGrps);
            this.Controls.Add(this.cbRemeberSelection);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChooseLanguage";
            this.Text = "Choose Language";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.TextBox lblTitle;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.CheckBox cbRemeberSelection;
        private System.Windows.Forms.CheckBox cbSelectLanguageGrps;
        #endregion

        private System.Windows.Forms.ListView lvLanguageLists;
    }
}
