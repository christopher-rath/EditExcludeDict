using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;

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
            public const string sAppName = @"Exclude Dictionary";
            public const string sPlatform = @"Windows 11";
            public const string sCopyright = @"2024{\'96}2026"; // "{\'96}" is an RTF en dash character.
            public const string sVersion = @"2.0"; // --> Also update in "AssemblyInfo.cs" <--
            public const string sVersionToRepl = @"[@Version String@]";
            public const string sPlatformToRepl = @"[@Platform String@]";
            public const string sCopyrightToRepl = @"[@Copyright Date@]";
            public const string sAppIniFileNm = @".ExcludeDict.ini";
            public const string sAuthorEmail = @"christopher@rath.ca";

        }
    }
}
