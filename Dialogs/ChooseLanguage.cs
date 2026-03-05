#region Copyright
// Edit Exlude Dict -- A Microsoft Word add-in to edit the UProof Exclude Dictionary word lists.
// Author: Christopher Rath <christopher@rath.ca>
// Archived at: https://github.com/christopher-rath/EditExcludeDict
// Copyright 2024-2026 © Christopher Rath
// Distributed under the GNU Lesser General Public License v2.1
//     (see the license text).
// Warranty: None, see the license.
#endregion
using EditExcludeDict.Dialogs;
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
using static Edit_Exclude_Dict.ThisAddIn;

namespace Edit_Exclude_Dict
{
    public partial class ChooseLanguage : Form
    {
        private readonly IniFile iniFile = new IniFile(Constants.sIniFileNm);
        private bool SelectLangGrpsSaved = true;
        private bool RememberSelectionSaved = true;

        public ChooseLanguage()
        {
            InitializeComponent();

            // Populate the list box with language options.
            //lbLanguageLists.Items.Add("English (United States)");
            cbSelectLanguageGrps.Checked = iniFile.GetBool(Constants.sIniSectionHead, Constants.sIniIsSelectLanguageGroups,
                    Constants.bIniIsSelectLanguageGroupsDefault);
            SelectLangGrpsSaved = cbSelectLanguageGrps.Checked;
            cbRemeberSelection.Checked = iniFile.GetBool(Constants.sIniSectionHead, Constants.sIniIsRememberSelection,
                    Constants.bIniIsRememberSelectionDefault);
            RememberSelectionSaved = cbRemeberSelection.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            iniFile.SetString(Constants.sIniSectionHead, Constants.sIniComment, Constants.sIniCommentTxt);
            iniFile.SetBool(Constants.sIniSectionHead, Constants.sIniIsSelectLanguageGroups, cbSelectLanguageGrps.Checked);
            iniFile.SetBool(Constants.sIniSectionHead, Constants.sIniIsRememberSelection, cbRemeberSelection.Checked);

            using (EditExcludeList editExcludeList = new EditExcludeList())
            {
                switch (editExcludeList.ShowDialog())
                {
                    case DialogResult.Cancel:
                        // Restore .ini values that were retrieved when the form opened.
                        iniFile.SetBool(Constants.sIniSectionHead, Constants.sIniIsSelectLanguageGroups, SelectLangGrpsSaved);
                        iniFile.SetBool(Constants.sIniSectionHead, Constants.sIniIsRememberSelection, RememberSelectionSaved);
                        Close();
                        break;
                    case DialogResult.Retry:
                        // We don't do anything other than leave the ChooseLanguage form open, so
                        // that the user may change their selections and click [Next] again.
                        break;
                    case DialogResult.OK:
                        // The EditExcludeList form saved the lists back out to the Exclude Dictionary
                        // files, so we just Close this form.
                        Close();
                        break;
                    default:
                        Debug.Fail("Unexpected dialog result from EditExcludeList form.");
                        Close();
                        break;
                }
            }
        }
    }
}
