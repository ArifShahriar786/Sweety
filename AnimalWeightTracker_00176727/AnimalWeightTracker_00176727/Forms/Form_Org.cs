﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalWeightTracker_00176727.Forms
{
    public partial class Form_Org : Form
    {
        //DataBase Connection
        Models.DB_WeightTrackerEntities db = new Models.DB_WeightTrackerEntities();

        public Form_Org()
        {
            InitializeComponent();
        }

        private void btnFOrgTypeSrch_Click(object sender, EventArgs e)
        {
            Forms.Form_OrgType frmOrgType = new Forms.Form_OrgType();
            frmOrgType.ShowDialog();
        }

        private void btnOrgClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Org_Load(object sender, EventArgs e)
        {
            var orgTypeFK = db.OrganizationTypes.ToList();
            cmbOrgTypeFK.DataSource = orgTypeFK;
            cmbOrgTypeFK.DisplayMember = "Org_Type";
            cmbOrgTypeFK.ValueMember = "Org_Type_Id";
        }

        private void btnOrgSave_Click(object sender, EventArgs e)
        {
            Models.Organization org = new Models.Organization();
            org.Name = txtOrgName.Text;
            org.Address = txtOrgAddress.Text;
            org.Org_Type_Id = Int32.Parse(cmbOrgTypeFK.SelectedValue.ToString());

            db.Organizations.Add(org);
            db.SaveChanges();

            txtOrgId.Text = org.Org_Id.ToString();

            MessageBox.Show("Data Saved Successfully ...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
