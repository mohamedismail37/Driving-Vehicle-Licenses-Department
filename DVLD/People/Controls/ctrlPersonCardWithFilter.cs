using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer;
using DVLD;
using DVLD.People;
using System.Linq.Expressions;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCardWithFilter: UserControl
    {

        // Define a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;

        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;

            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }

        /// He Putted here
        /// Show_Add_Person Properties...

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {
            cbFilter.SelectedIndex = 0;
            txtFilter.Text = PersonID.ToString();
            FindNow();
        }

        private void FindNow()
        {
            if (cbFilter.Text == "Person ID")
            {
                ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilter.Text));
            }
            else if (cbFilter.Text == "National No.")
            {
                ctrlPersonCard1.LoadPersonInfo(txtFilter.Text);
            }

            if (OnPersonSelected != null && FilterEnabled)
            {
                // Raise the event with a parameter
                OnPersonSelected(ctrlPersonCard1.PersonID);
            }
        }
        private void btnSearchPerson_Click(object sender, EventArgs e)
        {
            // He did some validations here


            FindNow();

        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.DataBack += _AddNewPerson_DataBack; // Subscribe to the event
            frm.ShowDialog();
            
        }

        private void _AddNewPerson_DataBack(object sender, int PersonID)
        {
            // Handle the data received

            cbFilter.SelectedIndex = 1;
            txtFilter.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
        }
    }
}
