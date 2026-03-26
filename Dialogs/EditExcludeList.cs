#region Copyright
// Edit Exclude Dict -- A Microsoft Word add-in to edit the UProof Exclude Dictionary word lists.
// Author: Christopher Rath <christopher@rath.ca>
// Archived at: https://github.com/christopher-rath/EditExcludeDict
// Copyright 2024-2026 © Christopher Rath
// Distributed under the GNU Lesser General Public License v2.1
//     (see the license text).
// Warranty: None, see the license.
#endregion
using Edit_Exclude_Dict;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EditExcludeDict.Dialogs
{
    /// <summary>
    /// Present the consolidated list of words from the Exclude Dictionary(ies) the
    /// user has selected for editing.  The user may choose to return to the
    /// ChooseLanguages form, cancel out completely, or save the edited list of words.
    ///
    /// Caution: do not set the AcceptButton property to [Enter], or the user won't be
    /// able to add new words to the list.
    /// </summary>
    public partial class EditExcludeList : Form
    {
        private bool WordListEdited = false; // Track whether the user has made changes to the word list.

        /// <summary>
        /// The constructor retrieves the list of words to edit from the ExcludeDictionaries
        /// class.
        /// </summary>
        public EditExcludeList()
        {
            List<string> wordListToEdit;

            InitializeComponent();

            // Retrieve the list of words to edit from the ExcludeDictionaries class.
            // Then copy that list into the tbWordList textbox for the user to edit.
            wordListToEdit = ExcludeDictionaries.Instance.GetConsolidatedWordList();
            tbWordList.Text = string.Join(Environment.NewLine, wordListToEdit);
            tbWordList.Select(0, 0); // Move the cursor to the start of the textbox.
            WordListEdited = false;
        }

        /// <summary>
        /// Cancel completely out of the editing process and return to Word.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool confirmCancel = false;

            if (WordListEdited)
            {
                confirmCancel = false;
                confirmCancel = MessageBox.Show("You may have unsaved changes to the word list. If you cancel "
                                            + "you will lose those changes.  Do you want to cancel the "
                                            + "editing process?", "Confirm Cancel", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Warning) == DialogResult.Yes;
            }
            else
            {
                confirmCancel = true;
            }
            if (confirmCancel)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Return to the previous form (ChooseLanguages); however, if the word list has
        /// been edited, then ask the user to confirm before exiting this dialog.
        /// </summary>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            bool confirmBack = false;

            if (WordListEdited)
            {
                confirmBack = false;
                confirmBack = MessageBox.Show("You may have unsaved changes to the word list. If you go back "
                                            + "you will lose those changes.  Do you want to return to the "
                                            + "Choose Language dialog?", "Confirm Back", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Warning) == DialogResult.Yes;
            }
            else
            {
                confirmBack = true;
            }
            if (confirmBack)
            {
                DialogResult = DialogResult.Retry;
            }
            else
            {
                DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Save the edited list of words back to the ExcludeDictionaries class and
        /// return to Word; however, if the word list has been edited, then ask the
        /// user to confirm before exiting this dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> wordListToSave = new List<string>();

            // Get the edited list of words from the tbWordList textbox, split it into a
            // list, and save it back to the ExcludeDictionaries class.
            foreach (string word in tbWordList.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                wordListToSave.Add(word.Trim());
            }

            ExcludeDictionaries.Instance.PutConsolidatedWordList(wordListToSave);

            WordListEdited = false;
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// If the user types anything in the WordList textbox, then set the WordListEdited
        /// flag to true.  Note that there is no "undo" for this process; so, a user may manually
        /// undo the change but this flag will remain true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbWordList_TextChanged(object sender, EventArgs e)
        {
            WordListEdited = true;
        }
    }
}
