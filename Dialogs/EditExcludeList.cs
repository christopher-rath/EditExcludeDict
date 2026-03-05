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
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // TODO: need to save the edited list back to the Exclude Dictionary files.
            this.DialogResult = DialogResult.OK;
        }
    }
}
