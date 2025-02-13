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
    public partial class Form_Species : Form
    {
        //DataBase Connection
        Models.DB_WeightTrackerEntities db = new Models.DB_WeightTrackerEntities();

        public Form_Species()
        {
            InitializeComponent();
        }

        private void btnSpeciesClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSpeciesSave_Click(object sender, EventArgs e)
        {
            Models.Species sp = new Models.Species();
            sp.Name = txtSpeciesName.Text;
            sp.Description = txtSpeciesDes.Text;

            db.Species.Add(sp);
            db.SaveChanges();

            txtSpeciesId.Text = sp.Species_Id.ToString();

            MessageBox.Show("Data Saved Successfully ...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
