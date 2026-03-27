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
using System.Runtime.InteropServices;

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
#region SetTextBoxPadding
        // Declarations needed to support the SetTextBoxPadding() method, below.
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, ref RECT rect);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT { public int Left, Top, Right, Bottom; }
#endregion SetTextBoxPadding
        // Track whether the user has made changes to the word list.
        private bool WordListEdited = false; 

        /// <summary>
        /// The constructor retrieves the list of words to edit from the ExcludeDictionaries
        /// class.
        /// </summary>
        public EditExcludeList()
        {
            List<string> wordListToEdit;

            InitializeComponent();

            SetTextBoxPadding(tbWordList, 10, 5, 0, 0); // Left, Top, Right, Bottom
            // Retrieve the list of words to edit from the ExcludeDictionaries class.
            // Then copy that list into the tbWordList textbox for the user to edit.
            wordListToEdit = ExcludeDictionaries.Instance.GetConsolidatedWordList();
            tbWordList.Text = string.Join(Environment.NewLine, wordListToEdit);
            tbWordList.Select(0, 0); // Move the cursor to the start of the textbox.
            tbWordList.ScrollToCaret();
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

        /// <summary>
        /// Implement basic Emacs keybindings to move the cursor around within
        /// the TextBox: [A]<, [A]>, ^f, ^b, ^a, ^e, ^n, ^p
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbWordList_KeyDown(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
            int curCaretIndex = tb.SelectionStart;
            int curLine = tb.GetLineFromCharIndex(curCaretIndex);
            int curLineLength = tb.Lines[curLine].Length;
            int curLineStartCharIndex = tb.GetFirstCharIndexFromLine(curLine);
            int curLineEndCharIndex = curLineStartCharIndex + curLineLength;
            // Calculate the relative column position from the start of the current line
            int curColumn = curCaretIndex - curLineStartCharIndex;

            if (e.Alt && e.Shift)
            {
                    switch (e.KeyCode)
                    {
                        case Keys.Oemcomma: // [Shift][Alt][<] Start of buffer
                            tb.Select(0, 0);
                            tb.ScrollToCaret();
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            break;
                        case Keys.OemPeriod: // [Shift][Alt][>] End of buffer
                        tb.Select(tb.Text.Length, 0);
                            tb.ScrollToCaret();
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            break;
                    }
            }
            else if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.F: // [Ctrl][F] Forward char
                        if (tb.SelectionStart < tb.Text.Length)
                        {
                            tb.SelectionStart++;
                            tb.ScrollToCaret();
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.B: // [Ctrl][B] Backward char
                        if (tb.SelectionStart > 0)
                        {
                            tb.SelectionStart--;
                            tb.ScrollToCaret();
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.A: // [Ctrl][A] Beginning of line
                        tb.SelectionStart = curLineStartCharIndex;
                        tb.ScrollToCaret();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.E: // [Ctrl][E] End of line
                        tb.Select(curLineEndCharIndex, 0);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.N: // [Ctrl][N] Next line 
                        // Check if there is a next line
                        if (curLine < tb.Lines.Length - 1)
                        {
                            int nextLineIndex = curLine + 1;
                            int nextLineStartCharIndex = tb.GetFirstCharIndexFromLine(nextLineIndex);

                            // Determine the target index in the next line.
                            // If the next line is shorter than the current column position, go to the
                            // end of the next line.
                            int nextLineLength = tb.Lines[nextLineIndex].Length;
                            int targetIndex = (curColumn <= nextLineLength) ? nextLineStartCharIndex + curColumn : nextLineStartCharIndex + nextLineLength;

                            // Move the caret
                            tb.Select(targetIndex, 0);
                            tb.ScrollToCaret(); // Ensure the new position is visible

                            // Crucially, set e.Handled and e.SuppressKeyPress to true 
                            // to stop the default TextBox down arrow behavior
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.P: // Previous line
                        // Check to see that we're not already on the first line.
                        if (curLine > 0)
                        {
                            // Calculate the index of the first character of the previous line
                            int prevLine = curLine - 1;
                            int prevLineStartIndex = tb.GetFirstCharIndexFromLine(prevLine);

                            // Determine the character index on the previous line that corresponds to the column
                            // Need to consider if the previous line is shorter than the current column position
                            int prevLineLength = (prevLine < tb.Lines.Length) ? tb.Lines[prevLine].Length : 0;
                            int targetIndexInPrevLine = prevLineStartIndex + Math.Min(curColumn, prevLineLength);

                            // Move the caret to the target index
                            tb.Select(targetIndexInPrevLine, 0);
                            tb.ScrollToCaret(); // Ensure the caret is visible
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Because a WPF TextBox does not expose the inner padding property of a Win32 textbox,
        /// the only way to adjust the padding property (to add whitespace between the words in the
        /// textbox and its border) is to use Platform Invoke (P/Invoke) to send a native Windows
        /// message (EM_SETRECT) to the TextBox control's underlying Win32 handle; which allows you
        /// to define the text's bounding rectangle.
        /// </summary>
        /// <param name="textBox">The TextBox widget to be adjusted.</param>
        /// <param name="left">The amount of padding on the left.</param>
        /// <param name="top">The amount of padding on the top.</param>
        /// <param name="right">The amount of padding on the right.</param>
        /// <param name="bottom">The amount of padding on the bottom.</param>
        private void SetTextBoxPadding(TextBox textBox, int left, int top, int right, int bottom)
        {
            RECT rect = new RECT { Left = left,
                                   Top = top,
                                   Right = textBox.ClientSize.Width - right,
                                   Bottom = textBox.ClientSize.Height - bottom };
            SendMessage(textBox.Handle, 0xB3, 0, ref rect); // EM_SETRECT
        }
    }
}
