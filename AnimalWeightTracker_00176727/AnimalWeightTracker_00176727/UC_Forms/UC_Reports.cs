﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalWeightTracker_00176727.UC_Forms
{
    public partial class UC_Reports : UserControl
    {
        //DataBase Connection
        Models.DB_WeightTrackerEntities db = new Models.DB_WeightTrackerEntities();

        public UC_Reports()
        {
            InitializeComponent();
        }

        private void UC_Reports_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var aniData = db.Animals.Select(d => new { d.Animal_Id, d.Name, d.Age, d.Gender, d.Height, d.Weight, d.Species_Id, d.Breed, d.Org_Id }).ToList();
            var spData = db.Species.Select(d => new { d.Species_Id, d.Name, d.Description }).ToList();
            var crsData = db.Courses.Select(d => new { d.Course_Id, d.Name, d.Start_Date, d.End_Date, d.Goal, d.BMI, d.Animal_Id, d.Staff_Id }).ToList();
            var mlData = db.Meals.Select(d => new { d.Meal_Id, d.Name, d.Unit_In_Gram, d.Calories }).ToList();
            var exrData = db.Exercises.Select(d => new { d.Exr_Id, d.Name, d.Duration, d.Calories_Burn, d.Type }).ToList();
            var actData = db.Activities.Select(d => new { d.Activity_Id, d.Level }).ToList();
            var dLogData = db.Daily_Log.Select(d => new { d.Daily_Log_Id, d.Course_Id, d.Activity_Id, d.Date, d.Progress }).ToList();
            var dMealData = db.Daily_Meal.Select(d => new { d.Daily_Meal_Id, d.Daily_Log_Id, d.Meal_Id, d.Meal_Intake }).ToList();
            var dExrData = db.Daily_Exercise.Select(d => new { d.Daily_Exr_Id, d.Daily_Log_Id, d.Exr_Id, d.Duration }).ToList();
            var dMsrData = db.Daily_Measurement.Select(d => new { d.Daily_Msr_Id, d.Daily_Log_Id, d.Waist_Size, d.Weight, d.Shift }).ToList();

            dgvAnimal.DataSource = aniData;
            dgvSpecies.DataSource = spData;
            dgvCourse.DataSource = crsData;
            dgvMeal.DataSource = mlData;
            dgvExr.DataSource = exrData;
            dgvActivity.DataSource = actData;
            dgvDLog.DataSource = dLogData;
            dgvDMeal.DataSource = dMealData;
            dgvDExr.DataSource = dExrData;
            dgvDMsr.DataSource = dMsrData;

            lblAnimalTotal.Text = aniData.Count.ToString();
            lblSpeciesTotal.Text = spData.Count.ToString();
            lblCourseTotal.Text = crsData.Count.ToString();
            lblMealTotal.Text = mlData.Count.ToString();
            lblExrTotal.Text = exrData.Count.ToString();
            lblActivityTotal.Text = actData.Count.ToString();
            lblDLogTotal.Text = dLogData.Count.ToString();
            lblDMealTotal.Text = dMealData.Count.ToString();
            lblDExrTotal.Text = dExrData.Count.ToString();
            lblDMsrTotal.Text = dMsrData.Count.ToString();
        }

        private void btnAnimalSrch_Click(object sender, EventArgs e)
        {
            if (cmbAnimalSrchBy.SelectedIndex == 0)
            {
                try
                {
                    int aniId = Int32.Parse(txtAnimalSrch.Text);
                    var aniData = db.Animals.Where(d => d.Animal_Id == aniId).Select(d => new { d.Animal_Id, d.Name, d.Age, d.Gender, d.Height, d.Weight, d.Species_Id, d.Breed, d.Org_Id }).ToList();

                    dgvAnimal.DataSource = aniData;
                }
                catch (Exception)
                {

                }

            }

            if (cmbAnimalSrchBy.SelectedIndex == 1)
            {
                var aniData = db.Animals.Where(d => d.Name.Contains(txtAnimalSrch.Text)).Select(d => new { d.Animal_Id, d.Name, d.Age, d.Gender, d.Height, d.Weight, d.Species_Id, d.Breed, d.Org_Id }).ToList();

                dgvAnimal.DataSource = aniData;
            }

        }

        private void btnSpeciesSrch_Click(object sender, EventArgs e)
        {
            if (cmbSpeciesSrchBy.SelectedIndex == 0)
            {
                try
                {
                    int spId = Int32.Parse(txtSpeciesSrch.Text);
                    var spData = db.Species.Where(d => d.Species_Id == spId).Select(d => new { d.Species_Id, d.Name, d.Description }).ToList();

                    dgvSpecies.DataSource = spData;
                }
                catch (Exception)
                {

                }
            }

            if (cmbSpeciesSrchBy.SelectedIndex == 1)
            {
                var spData = db.Species.Where(d => d.Name.Contains(txtSpeciesSrch.Text)).Select(d => new { d.Species_Id, d.Name, d.Description }).ToList();

                dgvSpecies.DataSource = spData;
            }
        }

        private void btnCourseSrch_Click(object sender, EventArgs e)
        {
            if (cmbCourseSrchBy.SelectedIndex == 0)
            {
                try
                {
                    int crsId = Int32.Parse(txtCourseSrch.Text);
                    var crsData = db.Courses.Where(d => d.Course_Id == crsId).Select(d => new { d.Course_Id, d.Name, d.Start_Date, d.End_Date, d.Goal, d.BMI, d.Animal_Id, d.Staff_Id }).ToList();

                    dgvCourse.DataSource = crsData;
                }
                catch (Exception)
                {

                }
            }

            if (cmbCourseSrchBy.SelectedIndex == 1)
            {
                var crsData = db.Courses.Where(d => d.Name.Contains(txtCourseSrch.Text)).Select(d => new { d.Course_Id, d.Name, d.Start_Date, d.End_Date, d.Goal, d.BMI, d.Animal_Id, d.Staff_Id }).ToList();

                dgvCourse.DataSource = crsData;
            }
        }

        private void btnMealSrch_Click(object sender, EventArgs e)
        {
            if (cmbMealSrchBy.SelectedIndex == 0)
            {
                try
                {
                    int mlId = Int32.Parse(txtMealSrch.Text);
                    var mlData = db.Meals.Where(d => d.Meal_Id == mlId).Select(d => new { d.Meal_Id, d.Name, d.Unit_In_Gram, d.Calories }).ToList();

                    dgvMeal.DataSource = mlData;
                }
                catch (Exception)
                {

                }
            }

            if (cmbMealSrchBy.SelectedIndex == 1)
            {
                var mlData = db.Meals.Where(d => d.Name.Contains(txtMealSrch.Text)).Select(d => new { d.Meal_Id, d.Name, d.Unit_In_Gram, d.Calories }).ToList();

                dgvMeal.DataSource = mlData;
            }
        }

        private void btnExrSrch_Click(object sender, EventArgs e)
        {
            if (cmbExrSrchBy.SelectedIndex == 0)
            {
                try
                {
                    int exrId = Int32.Parse(txtExrSrch.Text);
                    var exrData = db.Exercises.Where(d => d.Exr_Id == exrId).Select(d => new { d.Exr_Id, d.Name, d.Duration, d.Calories_Burn, d.Type }).ToList();

                    dgvExr.DataSource = exrData;
                }
                catch (Exception)
                {

                }
            }

            if (cmbExrSrchBy.SelectedIndex == 1)
            {
                var exrData = db.Exercises.Where(d => d.Name.Contains(txtExrSrch.Text)).Select(d => new { d.Exr_Id, d.Name, d.Duration, d.Calories_Burn, d.Type }).ToList();

                dgvExr.DataSource = exrData;
            }

            if (cmbExrSrchBy.SelectedIndex == 2)
            {
                var exrData = db.Exercises.Where(d => d.Type.Contains(txtExrSrch.Text)).Select(d => new { d.Exr_Id, d.Name, d.Duration, d.Calories_Burn, d.Type }).ToList();

                dgvExr.DataSource = exrData;
            }
        }

        private void btnActivitySrch_Click(object sender, EventArgs e)
        {
            if (cmbActivitySrchBy.SelectedIndex == 0)
            {
                try
                {
                    int actId = Int32.Parse(txtActivitySrch.Text);
                    var actData = db.Activities.Where(d => d.Activity_Id == actId).Select(d => new { d.Activity_Id, d.Level }).ToList();

                    dgvActivity.DataSource = actData;
                }
                catch (Exception)
                {

                }
            }

            if (cmbActivitySrchBy.SelectedIndex == 1)
            {
                var actData = db.Activities.Where(d => d.Level.Contains(txtActivitySrch.Text)).Select(d => new { d.Activity_Id, d.Level }).ToList();

                dgvActivity.DataSource = actData;
            }
        }

        private void btnDLogSrch_Click(object sender, EventArgs e)
        {
            if (cmbDLogSrchBy.SelectedIndex == 0)
            {
                try
                {
                    int dLogId = Int32.Parse(txtDLogSrch.Text);
                    var dLogData = db.Daily_Log.Where(d => d.Daily_Log_Id == dLogId).Select(d => new { d.Daily_Log_Id, d.Course_Id, d.Activity_Id, d.Date, d.Progress }).ToList();

                    dgvDLog.DataSource = dLogData;
                }
                catch (Exception)
                {

                }
            }
        }

        private void btnDMealSrch_Click(object sender, EventArgs e)
        {
            if (cmbDMealSrchBy.SelectedIndex == 0)
            {
                try
                {
                    int dMealId = Int32.Parse(txtDMealSrch.Text);
                    var dMealData = db.Daily_Meal.Where(d => d.Daily_Meal_Id == dMealId).Select(d => new { d.Daily_Meal_Id, d.Daily_Log_Id, d.Meal_Id, d.Meal_Intake }).ToList();

                    dgvDMeal.DataSource = dMealData;
                }
                catch (Exception)
                {

                }
            }
        }

        private void btnDExrSrch_Click(object sender, EventArgs e)
        {
            if (cmbDExrSrchBy.SelectedIndex == 0)
            {
                try
                {
                    int dExrId = Int32.Parse(txtDExrSrch.Text);
                    var dExrData = db.Daily_Exercise.Where(d => d.Daily_Exr_Id == dExrId).Select(d => new { d.Daily_Exr_Id, d.Daily_Log_Id, d.Exr_Id, d.Duration }).ToList();

                    dgvDExr.DataSource = dExrData;
                }
                catch (Exception)
                {

                }
            }
        }

        private void btnDMsrSrch_Click(object sender, EventArgs e)
        {
            if (cmbDMsrSrchBy.SelectedIndex == 0)
            {
                try
                {
                    int dMsrId = Int32.Parse(txtDMsrSrch.Text);
                    var dMsrData = db.Daily_Measurement.Where(d => d.Daily_Msr_Id == dMsrId).Select(d => new { d.Daily_Msr_Id, d.Daily_Log_Id, d.Waist_Size, d.Weight, d.Shift }).ToList();

                    dgvDMsr.DataSource = dMsrData;
                }
                catch (Exception)
                {

                }
            }
            if (cmbDMsrSrchBy.SelectedIndex == 1)
            {
                var dMsrData = db.Daily_Measurement.Where(d => d.Shift.Contains(txtDMsrSrch.Text)).Select(d => new { d.Daily_Msr_Id, d.Daily_Log_Id, d.Waist_Size, d.Weight, d.Shift }).ToList();

                dgvDMsr.DataSource = dMsrData;
            }
        }
    }
}
