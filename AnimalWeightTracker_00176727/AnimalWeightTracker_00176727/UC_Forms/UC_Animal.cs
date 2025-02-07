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
    public partial class UC_Animal : UserControl
    {
        //DataBase Connection
        Models.DB_WeightTrackerEntities db = new Models.DB_WeightTrackerEntities();

        public UC_Animal()
        {
            InitializeComponent();
        }

        private void UC_Animal_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var aniData = db.Animals.Select(d => new { d.Animal_Id, d.Name, d.Age, d.Gender, d.Height, d.Weight, d.Species_Id, d.Breed, d.Org_Id }).ToList();
            var spData = db.Species.Select(d => new { d.Species_Id, d.Name, d.Description }).ToList();
            var crsData = db.Courses.Select(d => new { d.Course_Id, d.Name, d.Start_Date, d.End_Date, d.Goal, d.BMI, d.Animal_Id, d.Staff_Id }).ToList();
            var mlData = db.Meals.Select(d => new { d.Meal_Id, d.Name, d.Unit_In_Gram, d.Calories }).ToList();
            var exrData = db.Exercises.Select(d => new { d.Exr_Id, d.Name, d.Duration, d.Calories_Burn, d.Type}).ToList();
            var actData = db.Activities.Select(d => new { d.Activity_Id, d.Level }).ToList();

            dgvAnimal.DataSource = aniData;
            dgvSpecies.DataSource = spData;
            dgvCourse.DataSource = crsData;
            dgvMeal.DataSource = mlData;
            dgvExr.DataSource = exrData;
            dgvActivity.DataSource = actData;

            lblAnimalTotal.Text = aniData.Count.ToString();
            lblSpeciesTotal.Text = spData.Count.ToString();
            lblCourseTotal.Text = crsData.Count.ToString();
            lblMealTotal.Text = mlData.Count.ToString();
            lblExrTotal.Text = exrData.Count.ToString();
            lblActivityTotal.Text = actData.Count.ToString();

            var spDataFK = db.Species.ToList();
            cmbAnimalSpecies.DataSource = spDataFK;
            cmbAnimalSpecies.DisplayMember = "Name";
            cmbAnimalSpecies.ValueMember = "Species_Id";

            var orgDataFK = db.Organizations.ToList();
            cmbAnimalOrg.DataSource = orgDataFK;
            cmbAnimalOrg.DisplayMember = "Name";
            cmbAnimalOrg.ValueMember = "Org_Id";

            var aniDataFK = db.Animals.ToList();
            cmbCourseAnimal.DataSource = aniDataFK;
            cmbCourseAnimal.DisplayMember = "Name";
            cmbCourseAnimal.ValueMember = "Animal_Id";

            var stfDataFK = db.Staffs.ToList();
            cmbCourseStaff.DataSource = stfDataFK;
            cmbCourseStaff.DisplayMember = "Name";
            cmbCourseStaff.ValueMember = "Staff_Id";

        }

        private void btnAnimalInsert_Click(object sender, EventArgs e)
        {
            Models.Animal ani = new Models.Animal();
            ani.Name = txtAnimalName.Text;
            ani.Age = txtAnimalAge.Text;
            ani.Gender = cmbAnimalGender.Text;
            ani.Height = decimal.Parse(txtAnimalHeight.Text);
            ani.Weight = decimal.Parse(txtAnimalWeight.Text);
            ani.Species_Id = Int32.Parse(cmbAnimalSpecies.SelectedValue.ToString());
            ani.Breed = txtAnimalBreed.Text;
            ani.Org_Id = Int32.Parse(cmbAnimalOrg.SelectedValue.ToString());

            db.Animals.Add(ani);
            db.SaveChanges();

            txtAnimalId.Text = ani.Animal_Id.ToString();

            LoadData();

            MessageBox.Show("Data Inserted Successfully ...", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvAnimal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtAnimalId.Text = dgvAnimal.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtAnimalName.Text = dgvAnimal.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtAnimalAge.Text = dgvAnimal.Rows[e.RowIndex].Cells[2].Value.ToString();
                cmbAnimalGender.Text = dgvAnimal.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtAnimalHeight.Text = dgvAnimal.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtAnimalWeight.Text = dgvAnimal.Rows[e.RowIndex].Cells[5].Value.ToString();
                cmbAnimalSpecies.SelectedValue = dgvAnimal.Rows[e.RowIndex].Cells[6].Value;
                txtAnimalBreed.Text = dgvAnimal.Rows[e.RowIndex].Cells[7].Value.ToString();
                cmbAnimalOrg.SelectedValue = dgvAnimal.Rows[e.RowIndex].Cells[8].Value;
            }
            catch (Exception)
            {
                
            }   
        }

        private void btnAnimalReset_Click(object sender, EventArgs e)
        {
            txtAnimalId.Text = "";
            txtAnimalName.Text = "";
            txtAnimalAge.Text = "";
            cmbAnimalGender.SelectedIndex = 0;
            txtAnimalHeight.Text = "";
            txtAnimalWeight.Text = "";
            cmbAnimalSpecies.SelectedIndex = 0;
            txtAnimalBreed.Text = "";
            cmbAnimalOrg.SelectedIndex = 0;
        }

        private void btnAnimalUpdate_Click(object sender, EventArgs e)
        {
            int aniId = Int32.Parse(txtAnimalId.Text);
            var aniData = db.Animals.Where(d => d.Animal_Id == aniId).FirstOrDefault();

            if (aniData != null)
            {
                aniData.Name = txtAnimalName.Text;
                aniData.Age = txtAnimalAge.Text;
                aniData.Gender = cmbAnimalGender.Text;
                aniData.Height = decimal.Parse(txtAnimalHeight.Text);
                aniData.Weight = decimal.Parse(txtAnimalWeight.Text);
                aniData.Species_Id = Int32.Parse(cmbAnimalSpecies.SelectedValue.ToString());
                aniData.Breed = txtAnimalBreed.Text;
                aniData.Org_Id = Int32.Parse(cmbAnimalOrg.SelectedValue.ToString());

                db.SaveChanges();

                LoadData();

                MessageBox.Show("Data Updated Successfully ...", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAnimalDelete_Click(object sender, EventArgs e)
        {
            int aniId = Int32.Parse(txtAnimalId.Text);
            var aniData = db.Animals.Where(d => d.Animal_Id == aniId).FirstOrDefault();

            if (aniData != null)
            {
                db.Animals.Remove(aniData);
                db.SaveChanges();

                LoadData();

            }
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

        private void btnSpeciesInsert_Click(object sender, EventArgs e)
        {
            Models.Species sp = new Models.Species();
            sp.Name = txtSpeciesName.Text;
            sp.Description = txtSpeciesDes.Text;

            db.Species.Add(sp);
            db.SaveChanges();

            txtSpeciesId.Text = sp.Species_Id.ToString();

            LoadData();

            MessageBox.Show("Data Inserted Successfully ...", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvSpecies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtSpeciesId.Text = dgvSpecies.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtSpeciesName.Text = dgvSpecies.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtSpeciesDes.Text = dgvSpecies.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch (Exception)
            {

            }
            
        }

        private void btnSpeciesReset_Click(object sender, EventArgs e)
        {
            txtSpeciesId.Text = "";
            txtSpeciesName.Text = "";
            txtSpeciesDes.Text = "";
        }

        private void btnSpeciesUpdate_Click(object sender, EventArgs e)
        {
            int spId = Int32.Parse(txtSpeciesId.Text);
            var spData = db.Species.Where(d => d.Species_Id == spId).FirstOrDefault();

            if (spData != null)
            {
                spData.Name = txtSpeciesName.Text;
                spData.Description = txtSpeciesDes.Text;

                db.SaveChanges();

                LoadData();

                MessageBox.Show("Data Updated Successfully ...", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSpeciesDelete_Click(object sender, EventArgs e)
        {
            int spId = Int32.Parse(txtSpeciesId.Text);
            var spData = db.Species.Where(d => d.Species_Id == spId).FirstOrDefault();

            if (spData != null)
            {
                db.Species.Remove(spData);
                db.SaveChanges();

                LoadData();
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

        private void btnCourseInsert_Click(object sender, EventArgs e)
        {
            Models.Course crs = new Models.Course();
            crs.Name = txtCourseName.Text;
            crs.Start_Date = DateTime.Parse(dtCourseSDate.Text);
            crs.End_Date = DateTime.Parse(dtCourseEDate.Text);
            crs.Goal = txtCourseGoal.Text;

            crs.Animal_Id = Int32.Parse(cmbCourseAnimal.SelectedValue.ToString());
            crs.Staff_Id = Int32.Parse(cmbCourseStaff.SelectedValue.ToString());

            var dataAni = db.Animals.Where(d => d.Animal_Id == crs.Animal_Id).FirstOrDefault();
            Decimal w = Decimal.Parse(dataAni.Weight.ToString());
            Decimal h = Decimal.Parse(dataAni.Height.ToString());
            Decimal bmi = w / (h * h);

            txtCourseBMI.Text = (w / (h * h)).ToString();

            crs.BMI = bmi;

            db.Courses.Add(crs);
            db.SaveChanges();

            txtCourseId.Text = crs.Course_Id.ToString();

            LoadData();

            MessageBox.Show("Data Inserted Successfully ...", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvCourse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtCourseId.Text = dgvCourse.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtCourseName.Text = dgvCourse.Rows[e.RowIndex].Cells[1].Value.ToString();
                dtCourseSDate.Text = dgvCourse.Rows[e.RowIndex].Cells[2].Value.ToString();
                dtCourseEDate.Text = dgvCourse.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtCourseGoal.Text = dgvCourse.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtCourseBMI.Text = dgvCourse.Rows[e.RowIndex].Cells[5].Value.ToString();
                cmbCourseAnimal.SelectedValue = dgvCourse.Rows[e.RowIndex].Cells[6].Value;
                cmbCourseStaff.SelectedValue = dgvCourse.Rows[e.RowIndex].Cells[7].Value;
            }
            catch (Exception)
            {

            }
        }

        private void btnCourseReset_Click(object sender, EventArgs e)
        {
            txtCourseId.Text = "";
            txtCourseName.Text = "";
            dtCourseSDate.Text = "";
            dtCourseEDate.Text = "";
            txtCourseGoal.Text = "";
            txtCourseBMI.Text = "";
            cmbCourseAnimal.SelectedIndex = 0;
            cmbCourseStaff.SelectedIndex = 0;
        }

        private void btnCourseUpdate_Click(object sender, EventArgs e)
        {
            int crsId = Int32.Parse(txtCourseId.Text);
            var crsData = db.Courses.Where(d => d.Course_Id == crsId).FirstOrDefault();

            if (crsData != null)
            {
                crsData.Name = txtCourseName.Text;
                crsData.Start_Date = DateTime.Parse(dtCourseSDate.Text);
                crsData.End_Date = DateTime.Parse(dtCourseEDate.Text);
                crsData.Goal = txtCourseGoal.Text;
                crsData.BMI = decimal.Parse(txtCourseBMI.Text);
                crsData.Animal_Id = Int32.Parse(cmbCourseAnimal.SelectedValue.ToString());
                crsData.Staff_Id = Int32.Parse(cmbCourseStaff.SelectedValue.ToString());

                db.SaveChanges();

                LoadData();

                MessageBox.Show("Data Updated Successfully ...", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCourseDelete_Click(object sender, EventArgs e)
        {
            int crsId = Int32.Parse(txtCourseId.Text);
            var crsData = db.Courses.Where(d => d.Course_Id == crsId).FirstOrDefault();

            if (crsData != null)
            {
                db.Courses.Remove(crsData);
                db.SaveChanges();

                LoadData();
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

        private void btnMealInsert_Click(object sender, EventArgs e)
        {
            Models.Meal ml = new Models.Meal();
            ml.Name = txtMealName.Text;
            ml.Unit_In_Gram = Int32.Parse(txtMealUInGrm.Text);
            ml.Calories = Int32.Parse(txtMealCalories.Text);

            db.Meals.Add(ml);
            db.SaveChanges();

            txtMealId.Text = ml.Meal_Id.ToString();

            LoadData();

            MessageBox.Show("Data Inserted Successfully ...", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvMeal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMealId.Text = dgvMeal.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtMealName.Text = dgvMeal.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtMealUInGrm.Text = dgvMeal.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtMealCalories.Text = dgvMeal.Rows[e.RowIndex].Cells[3].Value.ToString();

                    int MlUGrm = Int32.Parse(txtMealUInGrm.Text);
                    int MlCal = Int32.Parse(txtMealCalories.Text);
                    int CalPerGram = MlCal / MlUGrm;
                    lblCalPerGram.Text = CalPerGram.ToString();

            }
            catch (Exception)
            {

            }
        }

        private void btnMealReset_Click(object sender, EventArgs e)
        {
            txtMealId.Text = "";
            txtMealName.Text = "";
            txtMealUInGrm.Text = "";
            txtMealCalories.Text = "";
        }

        private void btnMealUpdate_Click(object sender, EventArgs e)
        {
            int mlId = Int32.Parse(txtMealId.Text);
            var mlData = db.Meals.Where(d => d.Meal_Id == mlId).FirstOrDefault();

            if (mlData != null)
            {
                mlData.Name = txtMealName.Text;
                mlData.Unit_In_Gram = Int32.Parse(txtMealUInGrm.Text);
                mlData.Calories = Int32.Parse(txtMealCalories.Text);

                db.SaveChanges();

                LoadData();
            }
        }

        private void btnMealDelete_Click(object sender, EventArgs e)
        {
            int mlId = Int32.Parse(txtMealId.Text);
            var mlData = db.Meals.Where(d => d.Meal_Id == mlId).FirstOrDefault();

            if (mlData != null)
            {
                db.Meals.Remove(mlData);
                db.SaveChanges();

                LoadData();
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

            if (cmbMealSrchBy.SelectedIndex ==1)
            {
                var mlData = db.Meals.Where(d => d.Name.Contains(txtMealSrch.Text)).Select(d => new { d.Meal_Id, d.Name, d.Unit_In_Gram, d.Calories }).ToList();

                dgvMeal.DataSource = mlData;
            }
        }

        private void btnExrInsert_Click(object sender, EventArgs e)
        {
            Models.Exercise exr = new Models.Exercise();
            exr.Name = txtExrName.Text;
            exr.Duration = Int32.Parse(txtExrDuration.Text);
            exr.Calories_Burn = Int32.Parse(txtExrClBurn.Text);
            exr.Type = txtExrType.Text;

            db.Exercises.Add(exr);
            db.SaveChanges();

            txtExrId.Text = exr.Exr_Id.ToString();

            LoadData();

            MessageBox.Show("Data Inserted Successfully ...", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvExr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtExrId.Text = dgvExr.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtExrName.Text = dgvExr.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtExrDuration.Text = dgvExr.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtExrClBurn.Text = dgvExr.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtExrType.Text = dgvExr.Rows[e.RowIndex].Cells[4].Value.ToString();

                int ExrDuration = Int32.Parse(txtExrDuration.Text);
                int ExrClBurn = Int32.Parse(txtExrClBurn.Text);
                int CalBurnPerMin = ExrClBurn / ExrDuration;
                lblCalBurnPerMin.Text = CalBurnPerMin.ToString();

            }
            catch (Exception)
            {

            }
        }

        private void btnExrReset_Click(object sender, EventArgs e)
        {
            txtExrId.Text = "";
            txtExrName.Text = "";
            txtExrDuration.Text = "";
            txtExrClBurn.Text = "";
            txtExrType.Text = "";
        }

        private void btnExrUpdate_Click(object sender, EventArgs e)
        {
            int exrId = Int32.Parse(txtExrId.Text);
            var exrData = db.Exercises.Where(d => d.Exr_Id == exrId).FirstOrDefault();

            if (exrData != null)
            {
                exrData.Name = txtExrName.Text;
                exrData.Duration = Int32.Parse(txtExrDuration.Text);
                exrData.Calories_Burn = Int32.Parse(txtExrClBurn.Text);
                exrData.Type = txtExrType.Text;

                db.SaveChanges();

                LoadData();

                MessageBox.Show("Data Updated Successfully ...", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExrDelete_Click(object sender, EventArgs e)
        {
            int exrId = Int32.Parse(txtExrId.Text);
            var exrData = db.Exercises.Where(d => d.Exr_Id == exrId).FirstOrDefault();

            if (exrData != null)
            {
                db.Exercises.Remove(exrData);
                db.SaveChanges();

                LoadData();
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

        private void btnActivityInsert_Click(object sender, EventArgs e)
        {
            Models.Activity act = new Models.Activity();
            act.Level = txtActivityLevel.Text;

            db.Activities.Add(act);
            db.SaveChanges();

            txtActivityId.Text = act.Activity_Id.ToString();

            LoadData();

            MessageBox.Show("Data Inserted Successfully ...", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvActivity_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtActivityId.Text = dgvActivity.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtActivityLevel.Text = dgvActivity.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch (Exception)
            {

            }
        }

        private void btnActivityReset_Click(object sender, EventArgs e)
        {
            txtActivityId.Text = "";
            txtActivityLevel.Text = "";
        }

        private void btnActivityUpdate_Click(object sender, EventArgs e)
        {
            int actId = Int32.Parse(txtActivityId.Text);
            var actData = db.Activities.Where(d => d.Activity_Id == actId).FirstOrDefault();

            if (actData != null)
            {
                actData.Level = txtActivityLevel.Text;

                db.SaveChanges();

                LoadData();

                MessageBox.Show("Data Updated Successfully ...", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnActivityDelete_Click(object sender, EventArgs e)
        {
            int actId = Int32.Parse(txtActivityId.Text);
            var actData = db.Activities.Where(d => d.Activity_Id == actId).FirstOrDefault();

            if (actData != null)
            {
                db.Activities.Remove(actData);
                db.SaveChanges();

                LoadData();
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

        private void txtMealCalories_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int MlUGrm = Int32.Parse(txtMealUInGrm.Text);
                int MlCal = Int32.Parse(txtMealCalories.Text);
                int CalPerGram = MlCal / MlUGrm;
                lblCalPerGram.Text = CalPerGram.ToString();
            }
            catch (Exception)
            {

            }
        }

        private void txtExrClBurn_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int ExrDuration = Int32.Parse(txtExrDuration.Text);
                int ExrClBurn = Int32.Parse(txtExrClBurn.Text);
                int CalBurnPerMin = ExrClBurn / ExrDuration;
                lblCalBurnPerMin.Text = CalBurnPerMin.ToString();
            }
            catch (Exception)
            {

            }
        }

        private void btnFAnimalSrch_Click(object sender, EventArgs e)
        {
            Forms.Form_Animal frmAnimal = new Forms.Form_Animal();
            frmAnimal.ShowDialog();
        }

        private void btnFOrgSrch_Click(object sender, EventArgs e)
        {
            Forms.Form_Org frmOrg = new Forms.Form_Org();
            frmOrg.ShowDialog();
        }

        private void btnFStaffSrch_Click(object sender, EventArgs e)
        {
            Forms.Form_Staff frmStf = new Forms.Form_Staff();
            frmStf.ShowDialog();
        }

        private void btnFSpeciesSrch_Click(object sender, EventArgs e)
        {
            Forms.Form_Species frmSps = new Forms.Form_Species();
            frmSps.ShowDialog();
        }
    }
}
