#region Copyright
// Edit Exlude Dict -- A Microsoft Word add-in to edit the UProof Exclude Dictionary word lists.
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditExcludeDict.Dialogs
{
    public partial class EditExcludeList : Form
    {
        public EditExcludeList()
        {
            List<string> wordListToEdit;

            InitializeComponent();

            // Retrieve the list of words to edit from the ExcludeDictionaries class.
            // Then copy that list into the tbWordList textbox for the user to edit.
            wordListToEdit = ExcludeDictionaries.Instance.GetConsolidatedWordList();
            tbWordList.Text = string.Join(Environment.NewLine, wordListToEdit);
            tbWordList.Select(0, 0); // Move the cursor to the start of the textbox.
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
        }

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

            DialogResult = DialogResult.OK;
        }
    }
}
