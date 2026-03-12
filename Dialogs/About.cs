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
    /// <summary>
    /// Open the About dialog and display the AddIn's about text (loaded from
    /// Resources/About_EditExcludeDict.rtf).
    /// </summary>
    public partial class About : Form
    {
        /// <summary>
        /// The class constructor loads the about text from About_EditExcludeDict.rtf,
        /// updates the version, platform, and copyright strings in the about text, and
        /// then displays the text.
        ///
        /// In addition, it sets up the event handler for clicks on links in the about
        /// text.
        /// </summary>
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

            aboutStr = aboutStr.Replace(Constants.sVersionToRepl, Constants.sVersion);
            aboutStr = aboutStr.Replace(Constants.sPlatformToRepl, Constants.sPlatform);
            aboutStr = aboutStr.Replace(Constants.sCopyrightToRepl, Constants.sCopyright);
            // Toggling DetectUrls to true simply formats URLs as links; code behind to
            // handle the clicks is also required (see "rtbAbout_LinkClicked" method below).
            rtbAbout.DetectUrls = true; 
            rtbAbout.Rtf = aboutStr;
        }

        /// <summary>
        /// When the [OK] button is clicked, close the dialog.  Note that the dialog's
        /// Properties are set so that this button is identified as the 'Cancel' action,
        /// which means that the user may also close the dialog by pressing the [Esc] key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Open the License dialog when the [View License] button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewLicense_Click(object sender, EventArgs e)
        {
            License licenseForm = new License();
            licenseForm.ShowDialog();
            // When we return from the License Form, restore focus to the [OK]
            // button so that the user may either press the [Enter] key or [Esc]
            // key to close the dialog.
            btnOK.Select();
        }

        /// <summary>
        /// When a URL in the About text is clicked, open the URL in the user's default browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtbAbout_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
        {
            // Use Process.Start to open the URL in the default browser
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
