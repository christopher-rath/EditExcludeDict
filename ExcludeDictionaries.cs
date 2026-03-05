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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    internal class ExcludeDictionaries
    {
        private List<string> dictFiles = new List<string>();
        public ExcludeDictionaries()
        {
            foreach (string f in Directory.GetFiles(Constants.sUProofDictFolder))
            {
                // TODO: Load previously selected languages from the .ini file.

                // Check that the UProof folder exists and throw an error if it doesn't.
                if (!Directory.Exists(Constants.sUProofDictFolder))
                {
                    throw new DirectoryNotFoundException($"The UProof dictionary folder was not found at the expected location: {Constants.sUProofDictFolder}");
                }
                else if (Path.GetFileName(f).StartsWith(Constants.sExcludeListFilePrefix) && Path.GetFileName(f).EndsWith(Constants.sExcludeListFileSuffix))
                {
                    dictFiles.Add(f);
                }

                
            }
        }

        /// <summary>
        /// Extract the extended LCID from the ExcludeList*.lex filename.
        /// For example: "ExcludeDictionaryGE0407.lex" returns "GE0407".
        /// </summary>
        /// <param name="filePath">The full pathname of the .lex file from which the extended LCID is to be extracted.</param>
        /// <returns></returns>
        private string GetExtendedLCID(string filePath)
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
                theLCID.Replace(Constants.sExcludeListFilePrefix, string.Empty);
                theLCID.Replace(Constants.sExcludeListFileSuffix, string.Empty);
                return theLCID;
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
        private string GetLCID(string filePath)
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
                theLCID.Replace(Constants.sExcludeListFileSuffix, string.Empty);
                theLCID.Substring(2, 4);
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
        private string GetLCIDPrefix(string filePath)
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
                thePrefix.Replace(Constants.sExcludeListFilePrefix, string.Empty);
                thePrefix.Substring(0, 2);

                return thePrefix;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
