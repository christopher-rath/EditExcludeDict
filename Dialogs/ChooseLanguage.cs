#region Copyright
// Edit Exclude Dict -- A Microsoft Word add-in to edit the UProof Exclude Dictionary word lists.
// Author: Christopher Rath <christopher@rath.ca>
// Archived at: https://github.com/christopher-rath/EditExcludeDict
// Copyright 2024-2026 © Christopher Rath
// Distributed under the GNU Lesser General Public License v2.1
//     (see the license text).
// Warranty: None, see the license.
#endregion
using EditExcludeDict.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using static Edit_Exclude_Dict.ThisAddIn;

namespace Edit_Exclude_Dict
{
    public partial class ChooseLanguage : Form
    {
        private readonly IniFile iniFile = new IniFile(Constants.sIniFileNm);
        // Guard flag to ignore ListView events while the form is initializing/loading data.
        private bool isInitializing = true;
        private readonly bool SelectLangGrpsSaved = true;
        private readonly bool RememberSelectionSaved = true;
        private readonly string SelectedLanguagesSaved = string.Empty;
#pragma warning disable IDE0044 // Add readonly modifier is not valid because these variables
        // are not only changed in the constructor.
        private string SelectedLanguages = string.Empty;
        private string[] SelectedLangsList;
#pragma warning restore IDE0044 // Add readonly modifier

        public ChooseLanguage()
        {
            List<string> availableDicts = new List<string>();
            string[,] tmpArray;

            // Prevent ItemChecked handler from responding while we populate the ListView.
            isInitializing = true;

            InitializeComponent();
            this.Shown += new System.EventHandler(this.lvLanguageLists_Shown);

            // Load the .ini values.
            cbSelectLanguageGrps.Checked = iniFile.GetBool(Constants.sIniSectionHead, Constants.sIniIsSelectLanguageGroups,
                    Constants.bIniIsSelectLanguageGroupsDefault);
            SelectLangGrpsSaved = cbSelectLanguageGrps.Checked;
            cbRemeberSelection.Checked = iniFile.GetBool(Constants.sIniSectionHead, Constants.sIniIsRememberSelection,
                    Constants.bIniIsRememberSelectionDefault);
            RememberSelectionSaved = cbRemeberSelection.Checked;
            SelectedLanguages = iniFile.GetString(Constants.sIniSectionHead, Constants.sIniSelectedLanguages, string.Empty);
            SelectedLanguagesSaved = SelectedLanguages;
            if (SelectedLanguages.Length > 0)
            {
                SelectedLangsList = SelectedLanguages.Split(',');
            }

            // Load the listbox with the Exclude Dictionary language lists that are available to edit.
            availableDicts = ExcludeDictionaries.Instance.GetAvailableDictFiles();
            tmpArray = new string[availableDicts.Count, 3];
            for (int i = 0; i < availableDicts.Count; i++)
            {
                tmpArray[i, 0] = ExcludeDictionaries.Instance.GetLCIDPrefix(availableDicts[i]);
                tmpArray[i, 1] = ExcludeDictionaries.Instance.GetLCID(availableDicts[i]);
                tmpArray[i, 2] = availableDicts[i];
            }
            // Add the three columns from the tmpArray to the lvLanguageLists ListView's three columns.
            lvLanguageLists.Columns.Add("Lang", -2);
            lvLanguageLists.Columns.Add("LCID", -2);
            lvLanguageLists.Columns.Add("Filename", -2);
            for (int i = 0; i <= tmpArray.GetUpperBound(0); i++)
            {
                string[] row = { tmpArray[i, 0], tmpArray[i, 1], tmpArray[i, 2] };
                ListViewItem item = new ListViewItem(row);
                lvLanguageLists.Items.Add(item);
                // If this language is one that was previously selected, then set it to checked.
                if (isLanguageIniSelected(tmpArray[i, 0] + tmpArray[i, 1]))
                {
                    item.Checked = true;
                }
            }
        }

        /// <summary>
        /// Called when the user has decided to abandon editing the Exclude Dictionary lists and
        /// wants to close the form without saving any changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// When the user has selected the "languages" (that is, the Exclude Dictionaries) to
        /// edit, then the [Next] button is clicked to open the EditExcludeList form.  This
        /// method alters its behaviour based upon the return value from the EditExcludeList
        /// form:
        ///  * if the user clicked [Cancel] on the EditExcludeList form, then we cancel out
        ///    of the entire process just as if [Cancel] was pressed on this form;
        ///  * if the user clicked [Retry] on the EditExcludeList form, then we do nothing and
        ///    allow the user to re-choose languages;
        ///  * and, if the user clicked [OK] on the EditExcludeList form, then we just close
        ///    this form and end the process, since the EditExcludeList form will have already
        ///    saved the changes to the Exclude Dictionary lists.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            iniFile.SetString(Constants.sIniSectionHead, Constants.sIniComment, Constants.sIniCommentTxt);
            iniFile.SetBool(Constants.sIniSectionHead, Constants.sIniIsSelectLanguageGroups, cbSelectLanguageGrps.Checked);
            iniFile.SetBool(Constants.sIniSectionHead, Constants.sIniIsRememberSelection, cbRemeberSelection.Checked);
            if (cbRemeberSelection.Checked)
            {
                // Build a comma-separated list of the selected languages to save to the .ini file.
                // As we build the list, also select/unselect the languages in the excludeDictionaries
                // object.
                SelectedLanguages = string.Empty;
                ExcludeDictionaries.Instance.ClearSelectedLanguages();
                foreach (ListViewItem item in lvLanguageLists.Items)
                {
                    if (item.Checked)
                    {
                        if (SelectedLanguages.Length > 0)
                        {
                            SelectedLanguages += ",";
                        }
                        SelectedLanguages += item.SubItems[0].Text + item.SubItems[1].Text; // LCID prefix + LCID
                        ExcludeDictionaries.Instance.SelectDict(item.SubItems[2].Text); // Filename
                    }
                }
            }
            else
            {
                // This is the list of selected languages for the .ini file.  It has nothing to do
                // with how the selected langauges are handled on the next EditExcludeList form.
                SelectedLanguages = string.Empty;
            }
            iniFile.SetString(Constants.sIniSectionHead, Constants.sIniSelectedLanguages, SelectedLanguages);

            using (EditExcludeList editExcludeList = new EditExcludeList())
            {
                switch (editExcludeList.ShowDialog())
                {
                    case DialogResult.Cancel:
                        // Restore .ini values that were retrieved when the form opened.
                        iniFile.SetBool(Constants.sIniSectionHead, Constants.sIniIsSelectLanguageGroups, SelectLangGrpsSaved);
                        iniFile.SetBool(Constants.sIniSectionHead, Constants.sIniIsRememberSelection, RememberSelectionSaved);
                        iniFile.SetString(Constants.sIniSectionHead, Constants.sIniSelectedLanguages, SelectedLanguagesSaved);
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

        /// <summary>
        /// Once this form has finished loading all its data, we set the isInitializing flag to false
        /// to enable the ItemChecked handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvLanguageLists_Shown(object sender, EventArgs e)
        {
            // Finished populating - enable the ItemChecked handler.
            isInitializing = false;
        }

        /// <summary>
        /// Each time the user checks or unchecks a langage on the form, this event handler is
        /// called.  We use the isInitializing flag to ignore these events while the form is
        /// loading, but once the form is loaded, we can react to the user checking/unchecking
        /// languages.
        ///
        /// When a language is checked, if SelectLanguageGroups is set then we check all the
        /// other entres for that langauge group (and the inverse when a language is unchecked).
        /// If SelectLanguageGroups is not set, then no other action is needed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvLanguageLists_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!isInitializing)
            {
                // ItemChecked fires when the checkbox state changes. Use this to react to
                // user checking/unchecking language lists.
                //string filename = e.Item.SubItems[2].Text; // Assuming the filename is in the third column.
                bool isChecked = e.Item.Checked;

                // If the "Select Language Groups" option is checked, then we need to check/uncheck all
                // other entries for the same language group.
                if (cbSelectLanguageGrps.Checked)
                {
                    string lcidPrefix = e.Item.SubItems[0].Text; // The LCID prefix is in the first column.
                    foreach (ListViewItem item in lvLanguageLists.Items)
                    {
                        if (item.SubItems[0].Text == lcidPrefix)
                        {
                            item.Checked = isChecked;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Call this method to determine if the specified LCID is one that was previously
        /// selected (that is, it's in the list that was loaded from the .ini file.
        /// </summary>
        /// <param name="anLCID">An LCID string (for example, "EN0409").</param>
        /// <returns></returns>
        private bool isLanguageIniSelected(string anLCID)
        {
            bool rtnVal = false;

            for (int i = 0; i <= SelectedLangsList.GetUpperBound(0); i++)
            {
                if (SelectedLangsList[i] == anLCID)
                {
                    rtnVal = true;
                    break;
                }
            }

            return rtnVal;
        }
    }
}
