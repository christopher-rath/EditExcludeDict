// place near top of file with other using directives
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;

namespace Edit_Exclude_Dict
{
    [ComVisible(true)]
    public class Ribbon1 : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        public Ribbon1()
        {
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
           return GetResourceText("Edit_Exclude_Dict.Ribbon1.xml");
        }

        #endregion

        #region Ribbon Callbacks
        // Create callback methods here. For more information about adding callback methods, visit https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        public void OnEDEdit(Office.IRibbonControl control)
        {
            Word.Range currentRange = Globals.ThisAddIn.Application.Selection.Range;
            currentRange.Text = "This text was added by the Ribbon.";
        }        

        public void OnEELAbout(Office.IRibbonControl control)
        {
            About aboutForm = new About();
            aboutForm.ShowDialog();
        }

        // callback used by Ribbon1.xml: getImage="GetEDEditImage"
        public stdole.IPictureDisp GetEDEditImage(Office.IRibbonControl control)
        {
            // Access .ico as an embedded resource:
            var asm = Assembly.GetExecutingAssembly();
            using (Stream s = asm.GetManifestResourceStream("Edit_Exclude_Dict.Resources.EditExcludeLists.ico")) // adjust resource name if different
            {
                if (s != null)
                {
                    using (Icon ico = new Icon(s))
                    {
                        return PictureConverter.Convert(ico.ToBitmap());
                    }
                }
            }
            return null;
        }
        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }

    // helper to convert Image -> IPictureDisp
    public class PictureConverter : AxHost
    {
        private PictureConverter() : base(string.Empty) { }
        public static stdole.IPictureDisp Convert(Image image)
        {
            return (stdole.IPictureDisp)AxHost.GetIPictureDispFromPicture(image);
        }
    }
}
