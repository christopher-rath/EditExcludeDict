#region Copyright
// Edit Exclude Dict -- A Microsoft Word add-in to edit the UProof Exclude Dictionary word lists.
// Author: Christopher Rath <christopher@rath.ca>
// Archived at: https://github.com/christopher-rath/EditExcludeDict
// Copyright 2024-2026 © Christopher Rath
// Distributed under the GNU Lesser General Public License v2.1
//     (see the license text).
// Warranty: None, see the license.
#endregion
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static Edit_Exclude_Dict.ThisAddIn;

namespace Edit_Exclude_Dict
{
    public partial class About : Form
    {
        public About()
        {
            string aboutStr;
            var aboutStrFlPath = @"Edit_Exclude_Dict.Resources.About_EditExcludeDict.rtf";

            InitializeComponent();
            this.rtbAbout.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbAbout_LinkClicked);

            try
            {
                var asm = Assembly.GetExecutingAssembly();
                using (var s = asm.GetManifestResourceStream(aboutStrFlPath))
                using (var r = new StreamReader(s))
                {
                    aboutStr = r.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                aboutStr = @"Unexpected error (" + e.Message 
                    + @") reading About string from Properties file,\par{\tab{" + aboutStrFlPath + @"}}";
            }

            // Note: URLs in the about text do not automatically render in the About dialog as clickable links.
            // This is a known issue: https://github.com/dotnet/winforms/issues/3632
            //
            // 2023-12-24 (v0.7): I've implemented code to manually detect URLs as the panel is loaded.
            aboutStr = aboutStr.Replace(Constants.sVersionToRepl, Constants.sVersion);
            aboutStr = aboutStr.Replace(Constants.sPlatformToRepl, Constants.sPlatform);
            aboutStr = aboutStr.Replace(Constants.sCopyrightToRepl, Constants.sCopyright);
            rtbAbout.DetectUrls = true;
            rtbAbout.Rtf = aboutStr;
        }

        
        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnViewLicense_Click(object sender, EventArgs e)
        {
            License licenseForm = new License();
            licenseForm.ShowDialog();
            btnOK.Select();
        }

        private void rtbAbout_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {
            // Use Process.Start to open the URL in the default browser
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
