using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmPersonDetails: Form
    {
        int personID = -1;
        public frmPersonDetails()
        {
            InitializeComponent();
        }

        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            ucPersonInfo.UpdatePersonInfo(PersonID);
            personID = PersonID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPersonDetails_Load(object sender, EventArgs e)
        {
            ucPersonInfo.UpdatePersonInfo(personID);
        }
    }
}
