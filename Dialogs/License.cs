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
    public partial class License : Form
    {
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
