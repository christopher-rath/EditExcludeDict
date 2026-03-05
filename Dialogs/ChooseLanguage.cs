#region Copyright
// Edit Exlude Dict -- A Microsoft Word add-in to edit the UProof Exclude Dictionary word lists.
// Author: Christopher Rath <christopher@rath.ca>
// Archived at: https://github.com/christopher-rath/EditExcludeDict
// Copyright 2024-2026 © Christopher Rath
// Distributed under the GNU Lesser General Public License v2.1
//     (see the license text).
// Warranty: None, see the license.
#endregion
using Microsoft.Office.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edit_Exclude_Dict
{
    public partial class ChooseLanguage : Form
    {
        private readonly IniFile iniFile = new IniFile(ThisAddIn.Constants.sIniFileNm);

        public ChooseLanguage()
        {
            InitializeComponent();
            this.Load += ChooseLanguage_Load;
        }

        private void ChooseLanguage_Load(object sender, EventArgs e)
        {
            // Populate the list box with language options.
            //lbLanguageLists.Items.Add("English (United States)");
            cbSelectLanguageGrps.Checked = iniFile.GetBool(ThisAddIn.Constants.sIniSectionHead, ThisAddIn.Constants.sIniIsSelectLanguageGroups, true);
            cbRemeberSelection.Checked = iniFile.GetBool(ThisAddIn.Constants.sIniSectionHead, ThisAddIn.Constants.sIniIsRememberSelection, true);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            iniFile.SetString(ThisAddIn.Constants.sIniSectionHead, ThisAddIn.Constants.sIniComment, ThisAddIn.Constants.sIniCommentTxt);
            iniFile.SetBool(ThisAddIn.Constants.sIniSectionHead, ThisAddIn.Constants.sIniIsSelectLanguageGroups, cbSelectLanguageGrps.Checked);
            iniFile.SetBool(ThisAddIn.Constants.sIniSectionHead, ThisAddIn.Constants.sIniIsRememberSelection, cbRemeberSelection.Checked);
            Close(); // TODO: replace this with a call to the next dialog.
        }
    }
}
