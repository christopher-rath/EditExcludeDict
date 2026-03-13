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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Edit_Exclude_Dict.ThisAddIn;

namespace Edit_Exclude_Dict
{
    /// <summary>
    /// This Class is used to access the UProof Exclude Dictionary word lists.  This Class tracks
    /// the individual .lex files and remembers which ones the user has selected to edit.  Once
    /// the user has selected a set to edit, this Class will both return the consolidated word list
    /// (via a get method) and accept the updated word list to save back to the .lex files (via
    /// a put method).
    /// </summary>
    public sealed class ExcludeDictionaries
    {
        private List<string> dictFiles = new List<string>(); // List of the Exclude Dictionary .lex filenames that are available to edit.
        private List<string> selectedDictFiles = new List<string>();

#pragma warning disable CA2211
        // The single instance of ExcludeDictionaries.
        public static ExcludeDictionaries Instance = new ExcludeDictionaries();
#pragma warning restore CA2211

        /// <summary>
        /// This ExcludeDictionaries class uses private constructor to implement the
        /// Singleton design pattern.  The single instance of this class is referenced
        /// via ExcludeDictionaries.Instance.
        /// </summary>
        private ExcludeDictionaries()
        {
            // Check that the UProof folder exists and throw an error if it doesn't.
            if (!Directory.Exists(Constants.sUProofDictFolder))
            {
                throw new DirectoryNotFoundException($"The UProof dictionary folder was not found at the expected location: {Constants.sUProofDictFolder}");
            }
            else
            {
                foreach (string f in Directory.GetFiles(Constants.sUProofDictFolder))
                {
                    if (Path.GetFileName(f).StartsWith(Constants.sExcludeListFilePrefix) && Path.GetFileName(f).EndsWith(Constants.sExcludeListFileSuffix))
                    {
                        // Only save the filename because the full path is a known constant.
                        dictFiles.Add(Path.GetFileName(f));
                    }
                }
                dictFiles.Sort();
            }
        }

        /// <summary>
        /// Get the list of available Exclude Dictionary .lex files that
        /// can be selected for editing.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAvailableDictFiles()
        {
            return dictFiles;
        }

        /// <summary>
        /// Clear the list of selected dictionary files.
        /// </summary>
        public void ClearSelectedLanguages()
        {
            selectedDictFiles.Clear();
        }

        /// <summary>
        /// Add a dictionary file to the list of selected dictionary files to edit; but only
        /// if it is in the list of available dictionary files.
        /// </summary>
        /// <param name="filename">The filename to add to the list of selected dictionaries.</param>
        /// <returns>True if the dictionary was added.  False if it was not added.</returns>
        public bool SelectDict(string filename)
        {
            if (dictFiles.Contains(filename))
            {
                selectedDictFiles.Add(filename);
                return true;
            }
            else
            {
                return false; 
            }
        }

        /// <summary>
        /// Removes the specified dictionary file from the collection of selected dictionary files if it is currently
        /// selected.
        /// </summary>
        /// <remarks>Use this method to unselect a dictionary file that was previously selected. If the
        /// specified file is not currently selected, the method performs no action and returns false.
        /// </remarks>
        /// <param name="filename">The name of the dictionary file to remove from the selection. The value is case-insensitive and should match
        /// the filename used when selecting the dictionary.</param>
        /// <returns>true if the dictionary file was successfully removed from the selection; otherwise, false.</returns>
        public bool UnselectDict(string filename)
        {
            if (selectedDictFiles.Contains(filename))
            {
                selectedDictFiles.Remove(filename);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Extract the extended LCID from the ExcludeList*.lex filename.
        /// For example: "ExcludeDictionaryGE0407.lex" returns "GE0407".
        /// </summary>
        /// <param name="filePath">The full pathname of the .lex file from which the extended LCID is to be extracted.</param>
        /// <returns></returns>
        public string GetExtendedLCID(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }
            else if (Path.GetFileName(filePath).StartsWith(Constants.sExcludeListFilePrefix)
                    && Path.GetFileName(filePath).EndsWith(Constants.sExcludeListFileSuffix))
            {
                string theExLCID = string.Empty;

                theExLCID = Path.GetFileName(filePath);
                theExLCID = theExLCID.Replace(Constants.sExcludeListFilePrefix, string.Empty);
                theExLCID = theExLCID.Replace(Constants.sExcludeListFileSuffix, string.Empty);

                return theExLCID;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Extract the LCID from the ExcludeList*.lex filename.
        /// For example: "ExcludeDictionaryGE0407.lex" returns "0407"
        /// </summary>
        /// <param name="filePath">The full pathname of the .lex file from which the LCID is to be extracted.</param>
        /// <returns>Four digit LCID.</returns>
        public string GetLCID(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }
            else if (Path.GetFileName(filePath).StartsWith(Constants.sExcludeListFilePrefix)
                    && Path.GetFileName(filePath).EndsWith(Constants.sExcludeListFileSuffix))
            {
                string theLCID = string.Empty;

                theLCID = Path.GetFileName(filePath);
                theLCID = theLCID.Replace(Constants.sExcludeListFilePrefix, string.Empty);
                theLCID = theLCID.Replace(Constants.sExcludeListFileSuffix, string.Empty);
                theLCID = theLCID.Substring(2, 4);

                return theLCID;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Extract the LCID prefix from the ExcludeList*.lex filename.
        /// For example: "ExcludeDictionaryGE0407.lex" returns "GE"
        /// <param name="filePath">The full pathname of the .lex file from which the LCID prefix is to be extracted.</param>
        /// <returns>The two character LCID prefix.</returns>
        public string GetLCIDPrefix(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }
            else if (Path.GetFileName(filePath).StartsWith(Constants.sExcludeListFilePrefix)
                    && Path.GetFileName(filePath).EndsWith(Constants.sExcludeListFileSuffix))
            {
                string thePrefix = string.Empty;

                thePrefix = Path.GetFileName(filePath);
                thePrefix = thePrefix.Replace(Constants.sExcludeListFilePrefix, string.Empty);
                thePrefix = thePrefix.Substring(0, 2);

                return thePrefix;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Concatenate contents of all of the selected Exclude Dictionary .lex files into
        /// a single consolidated list of words.  Remove duplicates and sort the list before
        /// returning it.
        /// </summary>
        /// <returns>A List<> of the consolidated, deduped, and sorted words from all of the selected Exclude Dictionaries.</returns>
        public List<string> GetConsolidatedWordList()
        {
            List<string> consolidatedWordList = new List<string>();
            foreach (string f in selectedDictFiles)
            {
                string fullPath = Path.Combine(Constants.sUProofDictFolder, f);
                if (File.Exists(fullPath))
                {
                    try
                    {
                        string[] words = File.ReadAllLines(fullPath);
                        consolidatedWordList.AddRange(words);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error reading file {fullPath}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"File not found: {fullPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // Remove duplicates and sort the list.
            consolidatedWordList = consolidatedWordList.Distinct().ToList();
            consolidatedWordList.Sort();
            consolidatedWordList.RemoveAll(x => string.IsNullOrEmpty(x));
            return consolidatedWordList;
        }

        /// <summary>
        /// Take the specified list of words and save it back to the selected Exclude
        /// Dictionary .lex files.
        /// </summary>
        /// <param name="WordListToSave"></param>
        public void PutConsolidatedWordList(List<string> WordListToSave)
        {
            // Sort the list before writing it out.
            WordListToSave.Sort();

            foreach (string f in selectedDictFiles)
            {
                string fullPath = Path.Combine(Constants.sUProofDictFolder, f);
                if (File.Exists(fullPath))
                {
                    try
                    {
                        File.WriteAllLines(fullPath, WordListToSave);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error writing to file {fullPath}: {ex.Message}\n\nThis is"
                                    + " not a recoverable error, work in progress will be abandoned!", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"File not found: {fullPath}\n\nThis is not a recoverable error,"
                                    + " work in progress will be abandoned!", "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }
    }
}
