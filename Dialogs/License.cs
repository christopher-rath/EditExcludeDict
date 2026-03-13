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

namespace Edit_Exclude_Dict
{
    /// <summary>
    /// This form displays the GNU Lesser General Public License v2.1 under
    /// which this AddIn is distributed.  The license text is loaded from
    /// Resources/GNU_LGPL.rtf.
    /// </summary>
    public partial class License : Form
    {
        /// <summary>
        /// The constructor loads the license text from Resources/GNU_LGPL.rtf
        /// and displays it.
        /// </summary>
        public License()
        {
            string licenseStr;
            var licenseStrFlPath = @"Edit_Exclude_Dict.Resources.GNU_LGPL.rtf";

            InitializeComponent();

            try
            {
                var asm = Assembly.GetExecutingAssembly();
                using (var s = asm.GetManifestResourceStream(licenseStrFlPath))
                using (var r = new StreamReader(s))
                {
                    licenseStr = r.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                licenseStr = @"Unexpected error (" + e.Message
                    + @") reading About string from Properties file,\par{\tab{" + licenseStrFlPath + @"}}";
            }

            rtbLicense.DetectUrls = true;
            rtbLicense.Rtf = licenseStr;
        }

        /// <summary>
        /// Close the form and return to the previous form (About).  This [OK] button
        /// is also registered as the form's Cancel button , so that the user can also
        /// close the form by pressing the [Esc] key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
