using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;


namespace WindowsFormsApplication1
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        private void user_Load(object sender, EventArgs e)
        {
            Title.BackColor = Color.Transparent;
            textOfbox(passwordlog); textOfbox(usernamelog);
            textOfbox(lastname); textOfbox(email); textOfbox(firstname);
            textOfbox(username); textOfbox(password); textOfbox(repassword);
            textOfbox(phoneNum); textOfbox(emailcheck);
            pictureBox2.MouseClick += new MouseEventHandler(pic2_Click);
            pictureBox1.MouseClick += new MouseEventHandler(pic1_Click);
            showpassword.MouseClick += new MouseEventHandler(pic_Click);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            connection.Open();
            if (usernamelog.Text == "" || (usernamelog.Text.Trim() == "Username" && usernamelog.ForeColor == Color.DarkGray))
                usernamelog.Focus();
            else if (passwordlog.Text == "" || (passwordlog.ForeColor == Color.DarkGray && passwordlog.Text.Trim() == "Password"))
                passwordlog.Focus();         
            else 
            {
                SqlCommand command = new SqlCommand("Login", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserName", usernamelog.Text));
                command.Parameters.Add(new SqlParameter("@Password", passwordlog.Text));
                if(admin.Checked)
                command.Parameters.Add(new SqlParameter("@Admin", '1'));
                else
                    command.Parameters.Add(new SqlParameter("@Admin", '0'));

                var returnParam1 = new SqlParameter {
                                                          ParameterName = "@@exist",
                                                          Direction = ParameterDirection.Output,
                                                          Size = 5
                                                    };
                command.Parameters.Add(returnParam1);
                command.ExecuteNonQuery();

                if (command.Parameters["@@exist"].Value.ToString() == "Exist")
                {
                    connection.Close();
                    if (!admin.Checked)
                    {
                        this.Hide();
                        userPage f = new userPage(usernamelog.Text);
                        f.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        this.Hide();
                        adminpage f = new adminpage(usernamelog.Text);
                        f.ShowDialog();
                        this.Close();
                    }
                }

                else
                    MessageBox.Show("Invalid Username or password.");
                connection.Close();
            }


        }

        private void loginlink_Click(object sender, EventArgs e)
        {
            reset();
            AcceptButton = sumbit;
            while (loginform.Left < 100)
            {
                if (loginform.Left <100 )
                    loginform.Left += 150;
                if (signupform.Left < 800)
                    signupform.Left += 200;
            }
        }

       
       

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            signupform.Visible = false;
            loginform.Visible = false;
            forgettnBox.Visible = true;
            forgettnBox.Location = new Point(220, 100);


        }


        public void pic_Click(object sender, EventArgs e)
        {

            if (passwordlog.PasswordChar == '*' )
            {
                passwordlog.PasswordChar = '\0';
                showpassword.Image = CarInsuranceSystem.Properties.Resources.show;
            }

            else  {
                passwordlog.PasswordChar = '*';
               showpassword.Image = CarInsuranceSystem.Properties.Resources.hide;
            
        }

        }
        public void textOfbox(TextBox tb)
        {
            string x = tb.Text;
            tb.KeyDown += (sernder, KeyDown) => this.TextGotFocus(tb, KeyDown, x);
            tb.KeyUp += (sernder, KeyUp) => this.TextLostFocus(tb, KeyUp, x);
        }
        public void TextGotFocus(object sender, EventArgs e, String x)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == x && tb.ForeColor == Color.DarkGray)
            {
                tb.Text = ""; tb.ForeColor = Color.Gray;
                tb.BackColor = Color.FromArgb(255, 254, 234);
                if (tb == passwordlog)
                {
                    tb.PasswordChar = '*';
                    showpassword.Image = CarInsuranceSystem.Properties.Resources.hide;
                }
                else if (tb == password || tb == repassword)
                    tb.PasswordChar = '*';

                
            }
        }
        public void TextLostFocus(object sender, EventArgs e, String x)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = x; tb.ForeColor = Color.DarkGray;
                if (tb == passwordlog || tb == password || tb == repassword)
                    tb.PasswordChar = '\0';
            }
        }

       


        private void recordData_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            connection.Open();
            SqlCommand checkEmail = new SqlCommand("SELECT count(*) FROM Car_Owner WHERE Email='" + email.Text.ToString() + "';", connection);
            SqlCommand checkusername = new SqlCommand("SELECT count(*) FROM Car_Owner WHERE UserName='" + username.Text.ToString() + "';", connection);
            int mail = (int)checkEmail.ExecuteScalar();
            int userCheck = (int)checkusername.ExecuteScalar();
            bool saveData = true;
            Regex emailForm = new Regex(@"^\w+([-_.]\w+)*@\w+([-.]\w)*\.\w+([-.]\w+)*$");
            error.Visible = false; p1.Visible = false; p2.Visible = false; p3.Visible = false;
            p4.Visible = false; p5.Visible = false;
            p6.Visible = false; p7.Visible = false;
            p8.Visible = false;  p14.Visible = false;
           
            if (!female.Checked && !male.Checked)
            {
                saveData = false; error.Text = "Please, Select a gender.";
                error.Visible = true; saveData = false;
            }
             if (PaymentMethod.SelectedIndex < 0)
            { error.Visible = true; saveData = false; p8.Visible = true; error.Text = "Please, Select a paymant Method."; PaymentMethod.Focus(); }
            if (typeOfinsurance.SelectedIndex < 0)
            { error.Visible = true; saveData = false; p7.Visible = true; error.Text = "Please, Select a Status."; PaymentMethod.Focus(); }

            if (key.SelectedIndex < 0)
            { error.Visible = true; saveData = false; p6.Visible = true; error.Text = "Please, Select a key."; key.Focus(); }

            if (phoneNum.Text.Trim() == "" || (phoneNum.Text.Trim() == "Phone number" && phoneNum.ForeColor == Color.DarkGray))
            {
                phoneNum.Focus(); saveData = false;
                phoneNum.BackColor = Color.AliceBlue; p6.Visible = true;
                error.Visible = true; error.Text = "Please, Enter a Phone Number.";

            }
            else if (phoneNum.Text.Length < 11)
            {
                error.Visible = true; p6.Visible = true;
                error.Text = "Invalid Phone Number";
                phoneNum.Focus(); saveData = false;
            }
            if (repassword.Text.Trim() == "" || (repassword.Text.Trim() == "Re-enter password" && repassword.ForeColor == Color.DarkGray) || repassword.Text.Trim() != password.Text.Trim())
            {
                repassword.Focus(); saveData = false; p5.Visible = true;
                repassword.BackColor = Color.AliceBlue;
            }


            if (password.Text.Trim() == "" || (password.Text.Trim() == "Password" && password.ForeColor == Color.DarkGray))
            { password.Focus(); password.BackColor = Color.AliceBlue; saveData = false; p4.Visible = true; }
            else if (password.Text.Length < 8)
            { error.Visible = true; error.Text = "Password is at least 8 Characters. "; p4.Visible = true; }
            if (username.Text.Trim() == "" || (username.Text.Trim() == "Username" && username.ForeColor == Color.DarkGray))
            { username.Focus(); username.BackColor = Color.AliceBlue; saveData = false; p3.Visible = true; }
            else if (userCheck != 0)
            { error.Text = "This Username's already exist."; error.Visible = true; saveData = false; p3.Visible = true; }

            if (email.Text.Trim() == "" || (email.Text.Trim() == "Email" && email.ForeColor == Color.DarkGray))
            {
                email.Focus(); email.BackColor = Color.AliceBlue; saveData = false; p2.Visible = true;
                error.Visible = false;
            }
            else if (!emailForm.IsMatch(email.Text.Trim()))
            { error.Text = "Please, Enter Valid email."; error.Visible = true; saveData = false; p2.Visible = true; email.Focus(); }
            else if (mail != 0)
            { error.Text = "This email's already exist."; error.Visible = true; saveData = false; p2.Visible = true; email.Focus(); }
            if (lastname.Text.Trim() == "" || (lastname.Text.Trim() == "LastName" && lastname.ForeColor == Color.DarkGray))
            {
                lastname.Focus(); lastname.BackColor = Color.AliceBlue; saveData = false; p1.Visible = true;
                error.Text = "Please, Enter Full data."; error.Visible = true;
            }

            if (firstname.Text.Trim() == "" || (firstname.Text.Trim() == "FirstName" && firstname.ForeColor == Color.DarkGray))
            {
                firstname.Focus(); firstname.BackColor = Color.AliceBlue; saveData = false; p14.Visible = true; 
                error.Text = "Please, Enter Full data."; error.Visible = true;

            }

            if (saveData)
            {
                string gender = (female.Checked) ? "Female" : "Male";
                SqlCommand command = new SqlCommand("Signup", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@FirstName", firstname.Text.Trim()));
                command.Parameters.Add(new SqlParameter("@LastName", lastname.Text.Trim()));
                command.Parameters.Add(new SqlParameter("@UserName", username.Text.Trim()));
                command.Parameters.Add(new SqlParameter("@Password", password.Text.Trim()));
                command.Parameters.Add(new SqlParameter("@Email", email.Text.Trim()));
                command.Parameters.Add(new SqlParameter("@Phone_Num1", phoneNum.Text.Trim()));
                command.Parameters.Add(new SqlParameter("@TypeOfInsurance", typeOfinsurance.SelectedItem));
                command.Parameters.Add(new SqlParameter("@payment", PaymentMethod.SelectedItem.ToString().Trim()));
                command.Parameters.Add(new SqlParameter("@Gender", gender.Trim()));
                command.ExecuteNonQuery();
                reset();
                this.Refresh();
                MessageBox.Show("SignedUp successfully.");



            }
            connection.Close();



        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void admin_CheckedChanged(object sender, EventArgs e)
        {
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.ForeColor = System.Drawing.Color.LightCoral;
            label3.Size = new System.Drawing.Size(50, 35);
            label3.Location = new System.Drawing.Point(30, 0);
            label3.Text = "Welcome sir/miss";
            check.Visible = false;
            signup.Visible = false;
            repassword_change.Location = new Point(95, 255);
        }

        private void user_CheckedChanged(object sender, EventArgs e)
        {
            label3.Font = new System.Drawing.Font("Adobe Caslon Pro", 27.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.ForeColor = System.Drawing.Color.LightCoral;
            label3.Location = new System.Drawing.Point(96, 0);
            label3.Size = new System.Drawing.Size(122, 63);
            label3.Text = "Log In";
            check.Visible = true;
            signup.Visible = true;
            repassword_change.Location = new Point(188, 241);
        }

        private void signup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AcceptButton = recordData;
            while ( signupform.Left > 80)
            {
                if (loginform.Left >-300)
                    loginform.Left -= 100;
                if (signupform.Left >80)
                    signupform.Left -= 30;
            }
        }
        public void reset()
        {
            error.Visible = false; p1.Visible = false; p2.Visible = false; p3.Visible = false;
            p4.Visible = false; p5.Visible = false;
            p6.Visible = false; p7.Visible = false;
            p8.Visible = false;  p14.Visible = false; p8.Visible = false; p8.Visible = false;
            firstname.Text = "FirstName"; firstname.ForeColor = Color.DarkGray; 
            lastname.Text = "LastName"; lastname.ForeColor = Color.DarkGray;
            email.Text = "Email"; email.ForeColor = Color.DarkGray;
            password.Text = "Password"; passwordlog.ForeColor = Color.DarkGray; password.PasswordChar = '\0';
            repassword.Text = "Re-enter password"; repassword_change.ForeColor = Color.DarkGray; repassword.PasswordChar = '\0';
            username.Text = "Username"; username.ForeColor = Color.DarkGray;
            phoneNum.Text = "Phone Number"; phoneNum.ForeColor = Color.DarkGray;
            firstname.BackColor = SystemColors.Info; lastname.BackColor = SystemColors.Info;
            password.BackColor = SystemColors.Info; repassword.BackColor = SystemColors.Info;
            username.BackColor = SystemColors.Info; email.BackColor = SystemColors.Info;
            phoneNum.BackColor = SystemColors.Info; 
            key.SelectedIndex = -1; PaymentMethod.SelectedIndex = -1;
            typeOfinsurance.SelectedIndex = -1;
            female.Checked = false; male.Checked = false;
            pictureBox1.Image = CarInsuranceSystem.Properties.Resources.show;
            pictureBox2.Image = CarInsuranceSystem.Properties.Resources.show;
        }

        private void pic1_Click(object sender, EventArgs e)
        {
            if (repassword.PasswordChar == '*')
            {
                repassword.PasswordChar = '\0';
               pictureBox1.Image = CarInsuranceSystem.Properties.Resources.show;
            }

            else
            {
                repassword.PasswordChar = '*';
                pictureBox1.Image = CarInsuranceSystem.Properties.Resources.hide;

            }
        }

      

        private void pic2_Click(object sender, EventArgs e)
        {
            if (password.PasswordChar == '*')
            {
                password.PasswordChar = '\0';
                pictureBox2.Image = CarInsuranceSystem.Properties.Resources.show;
            }

            else
            {
                password.PasswordChar = '*';
                pictureBox2.Image = CarInsuranceSystem.Properties.Resources.hide;

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            forgettnBox.Visible = false;
            signupform.Visible = true;
            loginform.Visible = true;
        }

        private void Send_Click(object sender, EventArgs e)
        {
            if (emailcheck.Text == "")
                emailcheck.Focus();
            else
            {
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                connection.Open();

                SqlCommand command = new SqlCommand(@"select count('*') from Car_Owner where Email='" + emailcheck.Text.Trim() +
                    "'; ", connection);
                int count = (int)command.ExecuteScalar();
                if (count == 1)
                {
                    command = new SqlCommand(@"select UserName, Password from Car_Owner where Email='" + emailcheck.Text.Trim() +
                     "'; ", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    MailMessage mail = new MailMessage("habibakhaled606@gmail.com", emailcheck.Text.Trim()
                        , "C.I.S", "Your UserName : " + reader["UserName"].ToString() + "\n Your Password: " + reader["Password"].ToString());
                    reader.Close();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    SmtpServer.Credentials = new NetworkCredential("habibakhaled606@gmail.com", "Aa11019990");
                    SmtpServer.Port = 587;
                    SmtpServer.EnableSsl = true;
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.Send(mail);
                }

                else
                    MessageBox.Show("Invalid Email.");
            }
        }
    }
}
