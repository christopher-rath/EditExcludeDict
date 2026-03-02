using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Edit_Exclude_Dict.ThisAddIn;

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
