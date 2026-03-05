#region Copyright
// Edit Exlude Dict -- A Microsoft Word add-in to edit the UProof Exclude Dictionary word lists.
// Author: Christopher Rath <christopher@rath.ca>
// Archived at: https://github.com/christopher-rath/EditExcludeDict
// Copyright 2024-2026 © Christopher Rath
// Distributed under the GNU Lesser General Public License v2.1
//     (see the license text).
// Warranty: None, see the license.
#endregion
using Microsoft.Office.Tools.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;

namespace Edit_Exclude_Dict
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new Ribbon1();
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
        public static class Constants
        {
            // Application strings.
            public const string sAppName = @"Exclude Dictionary";
            public const string sPlatform = @"Windows 11";
            public const string sCopyright = @"2024{\'96}2026"; // "{\'96}" is an RTF en dash character.
            public const string sVersion = @"2.0"; // --> Also update in "AssemblyInfo.cs" <--
            public const string sVersionToRepl = @"[@Version String@]";
            public const string sPlatformToRepl = @"[@Platform String@]";
            public const string sCopyrightToRepl = @"[@Copyright Date@]";
            public const string sAppIniFileNm = @".ExcludeDict.ini";
            public const string sAuthorEmail = @"christopher@rath.ca";
            // Strings for the .ini file.
            public const string sIniSectionHead = @"ExcludeDict Settings";
            public const string sIniComment = @"Comment";
            public const string sIniCommentTxt = @"Each of the variables in this ExcludeDict.ini file will be normalised.";
            public const string sIniIsSelectLanguageGroups = @"isSelectLanguageGroups";
            public const string sIniIsRememberSelection = @"isRememberSelection";
            public const string sIiSelectedLanguages = @"SelectedLanguages";
            //public static readonly string sHomeFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            public static readonly string sWordStartupFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Word\Startup");
            public static readonly string sIniFileNm = sWordStartupFolder + Path.DirectorySeparatorChar + Constants.sAppIniFileNm;
            // UProof Dictionary strings.
            public const string sExcludeListFilePrefix = @"ExcludeDictionary";
            public const string sExcludeListFileSuffix = @".lex";
            public static readonly string sUProofDictFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\UProof");
        }
    }
}
