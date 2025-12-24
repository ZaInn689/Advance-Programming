using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace digital_profile_builder
{
    public partial class Form1 : Form
    {
        string fname, uname, email, gender, hobby = "";
        int age;
        List<string> hobbies = new List<string>();
        List<Occupation> occupations;

        string defaultImagePath = @"//Mac/Home/Desktop/image.png";
        string selectedImagePath = "";

        public Form1()
        {
            InitializeComponent();
            occupation();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox.Image = Image.FromFile(defaultImagePath);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
           // backgroundclr();
            // themeclr();
            cmbxOccupation.DataSource = occupations;
            cmbxOccupation.DisplayMember = "OcType";
            cmbxOccupation.SelectedIndex = -1;
            cmbxOccupation.DropDownStyle = ComboBoxStyle.DropDown;
            lblWelcome.Text = $"Welcome! Today is {DateTime.Now:dddd, MMMM dd, yyyy} " +
                      $"and the current time is {DateTime.Now:hh:mm tt}";
            //String Interpolation
        }

        //private void themeclr()
        //{
        //    if (colorDialog2.ShowDialog() == DialogResult.OK)
        //        this.ForeColor = colorDialog2.Color;
        //}

        private void btnValidate_Click(object sender, EventArgs e)
        {
            fname = txtFullName.Text;
            uname = txtUsername.Text;
            email = txtEmail.Text;
            int.TryParse(txtAge.Text, out age); // age=int.Parse(txtAge.Text);
            if (rdoMale.Checked)
                gender = "Male";
            else if (rdoFemale.Checked)
                gender = "Female";
            else
                gender = "Not Selected";
            hobbie();
            string oc = getOccupation();
            if (valid())
            {
                MessageBox.Show("confirming profile completion");
                groupBox1.Text = $"Full Name: {fname}\n" +
                                  $"Username: {uname}\n" +
                                  $"Email: {email}\n" +
                                  $"Age: {age}\n" +
                                   $"Gender: {gender}\n" +
                                    $"Hobbies: {hobby}\n" +
                                    $"Occupation: {oc}\n" +
                                       $"Skills: {getSkills()}\n";
                // $"Occupation: {oc}\n" +
                grPhoto();
                //string photoInfo = string.IsNullOrEmpty(selectedImagePath) ? "No photo uploaded" : selectedImagePath;

                //string userInfo = $"Full Name: {fname}\n" +
                //                  $"Username: {uname}\n" +
                //                  $"Email: {email}\n" +
                //                  $"Age: {age}\n" +
                //                   $"Gender: {gender}\n" +
                //                    $"Hobbies: {hobby}\n" +
                //                    $"Occupation: {oc}\n" +
                //                       $"Skills: {getSkills()}\n" +
                //                         // $"Occupation: {oc}\n" +
                //                  $"Photo: {photoInfo}";

                //MessageBox.Show(userInfo, "Profile Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please correct your information.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string getOccupation()
        {
            if (cmbxOccupation.SelectedItem != null)
            {
                Occupation o = (Occupation)cmbxOccupation.SelectedItem;
                return o.OcType;
            }
            else
            if (cmbxOccupation.Text != null)// else if (!string.IsNullOrEmpty(cmbxOccupation.Text))
            {
                string occ = cmbxOccupation.Text.Trim();//return cmbxOccupation.Text.Trim();
                return occ;
            }
            return "Not selected";
        }
        private void occupation()
        {
            occupations = new List<Occupation>
            {
                new Occupation {OcType="Student" },
                 new Occupation {OcType="Engineer" },
                  new Occupation {OcType="Designer" },
                   new Occupation {OcType="Teacher" }
                   // new Occupation {OcType="Others" }
            };
        }

        private void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog();
            dia.Title = "Select Image";
            dia.Filter = "Image Files|*.jpg;*.jpeg;*.png;";

            if (dia.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = dia.FileName;

                pictureBox.Image = Image.FromFile(selectedImagePath);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
               // grPhoto();
            }
        }
        private void grPhoto()
        {
            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                groupPic.Image = Image.FromFile(selectedImagePath);
                groupPic.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        //private void backgroundclr()
        //{
        //    if (colorDialog2.ShowDialog() == DialogResult.OK)
        //        this.BackColor = colorDialog2.Color;
        //}
        private bool valid()
        {
            if (fname == "" && uname == "" && email == "" && age == 0)
                return false;
            if (!vfname(fname) || !vuname(uname) || !vemail(email) || !vage(age))
                return false;

            return true;
        }
        private void hobbie()
        {
            listBox.Items.Clear();
            hobbies.Clear();
            if (chkArt.Checked)
            {
                listBox.Items.Add("Art");
                hobbies.Add("Art");
            }
            if (chkCoding.Checked)
            {
                listBox.Items.Add("Coding");
                hobbies.Add("Coding");
            }
            if (chkReading.Checked)
            {
                listBox.Items.Add("Reading");
                hobbies.Add("Reading");
            }
            if (chkSports.Checked)
            {
                listBox.Items.Add("Sports");
                hobbies.Add("Sports");
            }
            if (chkTraveling.Checked)
            {
                listBox.Items.Add("Traveling");
                hobbies.Add("Traveling");
            }
            hobby = hobbies.Count > 0 ? string.Join(",", hobbies) : "None";
        }

        private void chkReading_CheckedChanged(object sender, EventArgs e)
        {
            hobbie();
        }

        private void chkSports_CheckedChanged(object sender, EventArgs e)
        {
            hobbie();
        }

        private void chkArt_CheckedChanged(object sender, EventArgs e)
        {
            hobbie();
        }

        private void chkTraveling_CheckedChanged(object sender, EventArgs e)
        {
            hobbie();
        }

        private void chkCoding_CheckedChanged(object sender, EventArgs e)
        {
            hobbie();
        }

        private void button1_Click(object sender, EventArgs e)//Add Skill
        {
            string s = txtSkills.Text.Trim();
            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("Please enter a skill");
                return;
            }
            bool alreadyExists = false;
            foreach (var k in listBoxSkills.Items)
            {
                if (s == k.ToString())
                {
                    MessageBox.Show("it's already exit");
                    alreadyExists = true;

                    break;
                }
            }
            if (!alreadyExists)
            {
                listBoxSkills.Items.Add(s);
                txtSkills.Clear();
                txtSkills.Focus();
            }
        }
        private string getSkills()
        {
            if (listBoxSkills.Items.Count == 0) return "None";
            List<string> skillsList = new List<string>();
            foreach (var item in listBoxSkills.Items)
                skillsList.Add(item.ToString());
            return string.Join(", ", skillsList);
        }

        private void btnThemeColour_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
                this.ForeColor = colorDialog2.Color;
        }

        private void rdoMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBackGroundclr_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
                this.BackColor = colorDialog2.Color;
        }

        private void btnResetButton_Click(object sender, EventArgs e)
        {
            txtFullName.Clear();
            txtUsername.Clear();
            txtEmail.Clear();
            txtAge.Clear();
            txtSkills.Clear();

            chkArt.Checked = false;
            chkCoding.Checked = false;
            chkReading.Checked = false;
            chkSports.Checked = false;
            chkTraveling.Checked = false;

            listBox.Items.Clear();
            hobbies.Clear();
            hobby = "";

            listBoxSkills.Items.Clear();

            cmbxOccupation.SelectedIndex = -1;
            cmbxOccupation.Text = "";

            rdoMale.Checked = false;
            rdoFemale.Checked = false;

            this.ForeColor = SystemColors.ControlText;
            this.BackColor = SystemColors.Control;

            pictureBox.Image = Image.FromFile(defaultImagePath);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            groupPic.Image = null;
            selectedImagePath = "";
            groupBox1.Text = "Profile Information";
            MessageBox.Show("Form has been reset successfully!");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome! Today is {DateTime.Now:dddd, MMMM dd, yyyy} " +
                              $"and the current time is {DateTime.Now:hh:mm:ss tt}";
        }
        //Timer Event (if you want the time to update every second)
        //Set Enabled = true and Interval = 1000 (1 second).
        private bool vage(int age)
        {
            return age >= 13 && age <= 100;
        }

        private bool vemail(string email)
        {
            //if (Regex.IsMatch(txtEmail.Text, @"([\w]+)@([\w]+)\.([\w]+)$") == true)
            //    return true;
            //return false;
            return Regex.IsMatch(txtEmail.Text, @"([\w]+)@([\w]+)\.([\w]+)$");
            //email = email.Trim().ToLower();
            //if (email.Contains('@') && email.Contains('.'))
            //    return true;
            //return false;
        }
        private bool vuname(string uname)
        {
            //    if (Regex.IsMatch(txtUsername.Text, @"^[a-zA-Z0-9 ]+$")==true)
            //        return true;
            //    return false;
            return Regex.IsMatch(uname, @"^[A-Za-z0-9]+$");
            //uname = uname.Trim().ToLower();

            //foreach (char n in uname)
            //{
            //    if ((n >= 'a' && n <= 'z') || (n >= '0' && n <= '9'))
            //        continue;
            //    return false;
            //}

            //return true;
        }
        private bool vfname(string fname)
        {
            //if (Regex.IsMatch(txtUsername.Text, @"^[a-zA-Z ]+$")==true)
            //    return true;
            //return false;
            return Regex.IsMatch(fname, @"^[A-Za-z ]+$");
            //fname = fname.Trim().ToLower();

            //foreach (char n in fname)
            //{
            //    if ((n >= 'a' && n <= 'z') || n == ' ')
            //        continue;
            //    return false;
            //}

            //return true;
        }

    }
}

////using System;
////using System.Collections.Generic;
////using System.ComponentModel;
////using System.Data;
////using System.Drawing;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using System.Windows.Forms;

////namespace digital_profile_builder
////{
////    public partial class Form1 : Form
////    {
////        string fname, uname, email, gender, hobby = "";
////        int age;
////        List<string> hobbies = new List<string>();
////        Color themeColor = Color.Blue; // Default theme color
////        Color backgroundColor = Color.White; // Default background color

////        string selectedImagePath = "";

////        public Form1()
////        {
////            InitializeComponent();
////        }

////        private void Form1_Load(object sender, EventArgs e)
////        {
////            ApplyThemeColor();
////            ApplyBackgroundColor();
////            UpdateHobbiesList();
////        }

////        private void btnValidate_Click(object sender, EventArgs e)
////        {
////            fname = txtFullName.Text;
////            uname = txtUsername.Text;
////            email = txtEmail.Text;
////            int.TryParse(txtAge.Text, out age);
////            gender = rdoMaleFemale.Checked ? "Male" :   "Female"  ;

////            UpdateHobbiesList();

////            if (ValidateInput())
////            {
////                string photoInfo = string.IsNullOrEmpty(selectedImagePath) ? "No photo uploaded" : selectedImagePath;

////                string userInfo = $"Full Name: {fname}\n" +
////                                  $"Username: {uname}\n" +
////                                  $"Email: {email}\n" +
////                                  $"Age: {age}\n" +
////                                  $"Gender: {gender}\n" +
////                                  $"Hobbies: {hobby}\n" +
////                                  $"Theme Color: {themeColor.Name}\n" +
////                                  $"Background Color: {backgroundColor.Name}\n" +
////                                  $"Photo: {photoInfo}";

////                MessageBox.Show(userInfo, "Profile Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
////            }
////            else
////            {
////                MessageBox.Show("Please correct your information.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////            }
////        }

////        private void btnUploadPhoto_Click(object sender, EventArgs e)
////        {
////            OpenFileDialog dia = new OpenFileDialog();
////            dia.Title = "Select Image";
////            dia.Filter = "Image Files|*.jpg;*.jpeg;*.png;";

////            if (dia.ShowDialog() == DialogResult.OK)
////            {
////                selectedImagePath = dia.FileName;
////                pictureBox.Image = Image.FromFile(selectedImagePath);
////                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
////            }
////        }

////        // Theme Color Selection
////        private void btnThemeColor_Click(object sender, EventArgs e)
////        {
////            ColorDialog colorDialog = new ColorDialog();
////           // colorDialog.Title = "Select Theme Color";
////            colorDialog.Color = themeColor;

////            if (colorDialog.ShowDialog() == DialogResult.OK)
////            {
////                themeColor = colorDialog.Color;
////                ApplyThemeColor();
////            }
////        }

////        // Background Color Selection for PictureBox
////        private void btnBackgroundColor_Click(object sender, EventArgs e)
////        {
////            ColorDialog colorDialog = new ColorDialog();
////            //colorDialog.Title = "Select Background Color";
////            colorDialog.Color = backgroundColor;

////            if (colorDialog.ShowDialog() == DialogResult.OK)
////            {
////                backgroundColor = colorDialog.Color;
////                ApplyBackgroundColor();
////            }
////        }

////        private void ApplyThemeColor()
////        {
////            // Apply theme color to various controls
////            this.BackColor = Color.FromArgb(240, 240, 240); // Light gray background

////            btnValidate.BackColor = themeColor;
////            btnValidate.ForeColor = Color.White;
////            btnUploadPhoto.BackColor = themeColor;
////            btnUploadPhoto.ForeColor = Color.White;


////            // Refresh the form
////            this.Invalidate();
////        }

////        private void ApplyBackgroundColor()
////        {
////            // Apply background color to PictureBox
////            pictureBox.BackColor = backgroundColor;
////        }

////        private void UpdateHobbiesList()
////        {
////            listBox.Items.Clear();
////            hobbies.Clear();

////            if (chkArt.Checked)
////            {
////                listBox.Items.Add("🎨 Art");
////                hobbies.Add("Art");
////            }
////            if (chkCoding.Checked)
////            {
////                listBox.Items.Add("💻 Coding");
////                hobbies.Add("Coding");
////            }
////            if (chkReading.Checked)
////            {
////                listBox.Items.Add("📚 Reading");
////                hobbies.Add("Reading");
////            }
////            if (chkSports.Checked)
////            {
////                listBox.Items.Add("⚽ Sports");
////                hobbies.Add("Sports");
////            }
////            if (chkTraveling.Checked)
////            {
////                listBox.Items.Add("✈️ Traveling");
////                hobbies.Add("Traveling");
////            }

////            hobby = hobbies.Count > 0 ? string.Join(", ", hobbies) : "None";
////        }

////        // Checkbox event handlers
////        private void chkReading_CheckedChanged(object sender, EventArgs e)
////        {
////            UpdateHobbiesList();
////        }

////        private void chkSports_CheckedChanged(object sender, EventArgs e)
////        {
////            UpdateHobbiesList();
////        }

////        private void chkArt_CheckedChanged(object sender, EventArgs e)
////        {
////            UpdateHobbiesList();
////        }

////        private void chkTraveling_CheckedChanged(object sender, EventArgs e)
////        {
////            UpdateHobbiesList();
////        }

////        private void chkCoding_CheckedChanged(object sender, EventArgs e)
////        {
////            UpdateHobbiesList();
////        }

////        private bool ValidateInput()
////        {
////            if (string.IsNullOrWhiteSpace(fname) ||
////                string.IsNullOrWhiteSpace(uname) ||
////                string.IsNullOrWhiteSpace(email) ||
////                age == 0 ||
////                string.IsNullOrEmpty(gender))
////            {
////                return false;
////            }

////            if (!ValidateFullName(fname) || !ValidateUsername(uname) || !ValidateEmail(email) || !ValidateAge(age))
////                return false;

////            return true;
////        }

////        private bool ValidateAge(int age)
////        {
////            return age >= 13 && age <= 100;
////        }

////        private bool ValidateEmail(string email)
////        {
////            email = email.Trim().ToLower();
////            return email.Contains('@') && email.Contains('.') && email.IndexOf('@') < email.LastIndexOf('.');
////        }

////        private bool ValidateUsername(string uname)
////        {
////            uname = uname.Trim();

////            foreach (char n in uname)
////            {
////                if ((n >= 'a' && n <= 'z') || (n >= 'A' && n <= 'Z') || (n >= '0' && n <= '9'))
////                    continue;
////                return false;
////            }
////            return true;
////        }

////        private bool ValidateFullName(string fname)
////        {
////            fname = fname.Trim();

////            foreach (char n in fname)
////            {
////                if ((n >= 'a' && n <= 'z') || (n >= 'A' && n <= 'Z') || n == ' ')
////                    continue;
////                return false;
////            }
////            return true;
////        }

////        // Reset all colors to default
////        private void btnResetColors_Click(object sender, EventArgs e)
////        {
////            themeColor = Color.Blue;
////            backgroundColor = Color.White;
////            ApplyThemeColor();
////            ApplyBackgroundColor();
////        }
////    }
////}


