using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class adminpage : Form
    {
        //timer
        //
        Timer timer = new Timer();
        long time = 0;
        //
        //top
        //
        Panel topbar = new Panel();
        Label title = new Label();
        Label clock = new Label();
        PictureBox power = new PictureBox();
        //
        //bottom
        Panel shape = new Panel();
        //
        //left
        //
        Panel menu = new Panel();
        PictureBox appear = new PictureBox();
        PictureBox profilePicture = new PictureBox();
        PictureBox viewcars = new PictureBox();
        Label viewcarslabel = new Label();
        PictureBox members = new PictureBox();
        Label memberslabel = new Label();
        PictureBox addcar = new PictureBox();
        Label addcarlabel = new Label();
        PictureBox notifi = new PictureBox();
        Label notification = new Label();
        PictureBox profile = new PictureBox();
        Label profilelabel = new Label();
        PictureBox question = new PictureBox();
        Label questionlabel = new Label();
        PictureBox settings = new PictureBox();
        Label settingslabel = new Label();
        Panel mark = new Panel();
        //
        //
        //
        //View cars
        DataGridView cars = new DataGridView();
        Button update = new Button();
        //
        //
        //add car
        Button add = new Button();
        TextBox platenumber = new TextBox();
        TextBox color = new TextBox();
        TextBox model = new TextBox();
        ComboBox username_addcar = new ComboBox();
        ComboBox manfactureyear = new ComboBox();
        Label error = new Label();
        //
        //
        //profile
        PictureBox image = new PictureBox();
        TextBox firstname = new TextBox();
        TextBox lastname = new TextBox();
        TextBox email = new TextBox();
        TextBox password = new TextBox();
        TextBox phonenum1 = new TextBox();
        TextBox phonenum2 = new TextBox();
        ComboBox type = new ComboBox();
        ComboBox gender = new ComboBox();
        Button savepro = new Button();
        Button clear = new Button();
        //
        //notification
        DataGridView notificationadmin = new DataGridView();
        DataGridViewTextBoxColumn request = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn car = new DataGridViewTextBoxColumn();
        DataGridViewButtonColumn details = new DataGridViewButtonColumn();
        DataGridViewButtonColumn accept = new DataGridViewButtonColumn();
        DataGridViewButtonColumn refuse = new DataGridViewButtonColumn();
        //
        //Members
        DataGridView grid = new DataGridView();
        DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn email_grid = new DataGridViewTextBoxColumn();
        DataGridViewButtonColumn edit = new DataGridViewButtonColumn();
        DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
        DataGridViewButtonColumn cars_grid = new DataGridViewButtonColumn();
        ComboBox search = new ComboBox();
        //
        //
        //
        PictureBox back1 = new PictureBox();
        PictureBox back2 = new PictureBox();
        //
        string user_name;
        //
        string position = "home";
        string username;
        public adminpage(string user)
        {
            InitializeComponent();
            username = user;
            user_name = username;
        }
        private void ain_Load(object sender, EventArgs e)
        {
            //Timer
            timer.Interval = 1000;
            timer.Tick += new EventHandler(this.timer_Tick);
            timer.Start();
            //
            //bottom
            shape.Location = new Point(0, 740);
            shape.Size = new Size(2000, 100);
            shape.BackColor = Color.FromArgb(0, 28, 37);
            //
            //
            //carspage
            cars.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(Cars_CellContentClick);
            update.Click += new System.EventHandler(update_Click);
            //
            //add car
            add.Click += new EventHandler(add_Click);
            //
            //profile
            image.Click += new EventHandler(image_Click);
            clear.Click += new EventHandler(clear_Click);
            savepro.Click += new System.EventHandler(this.savepro_Click);
            //
            //
            //notificationOfadmin
            notificationadmin.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(notificationadmin_CellContentClick);
            //
            //
            //Members
            grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(grid_CellContentClick);
            back1.Click += new EventHandler(members_Click);
            back2.Click += new EventHandler(members_Click);
            //
            top();
            left();
        }
        private void image_Click(object sender, EventArgs e)
        {
            string path = "";
            OpenFileDialog newimage = new OpenFileDialog();
            newimage.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand();
            if (newimage.ShowDialog() == DialogResult.OK)
            {
                path = newimage.FileName.ToString();
                image.ImageLocation = path;
                byte[] imageupload = null;
                FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(stream);
                imageupload = brs.ReadBytes((int)stream.Length);
                command = new SqlCommand(@"update Car_Owner set Image=@image where UserName='" + user_name + "';", con);
                command.Parameters.Add(new SqlParameter("@image", imageupload));
                command.ExecuteNonQuery();
            }
            con.Close();
            picturedisplay(profilePicture);
        }
        private void clear_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand(@"update Car_Owner set Image=null where UserName='" + user_name + "';", con);
            command.ExecuteNonQuery();
            con.Close();
            Image myimage = CarInsuranceSystem.Properties.Resources._default;
            image.Image = myimage;
            picturedisplay(profilePicture);
        }
        private void timer_Tick(object sender, EventArgs e)
        {

            long hour = 0, min = 0;
            time++;
            long seconds = time;
            hour = seconds / (60 * 60);
            seconds = (seconds % (60 * 60));
            min = seconds / 60;
            seconds = (seconds % 60);

            string x;
            clock.Text = hour.ToString() + ":";
            if (hour < 10)
                clock.Text = "0" + hour.ToString() + ":";

            x = min.ToString() + ":";
            if (min < 10)
                x = "0" + min.ToString() + ":";
            clock.Text += x;



            x = seconds.ToString();
            if (seconds < 10)
                x = "0" + seconds.ToString();
            clock.Text += x;
        }
        private void picturedisplay(PictureBox x)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand("Select  Image from Car_Owner where UserName='" + user_name + "';", con);
            try
            {
                byte[] MyImg = (byte[])command.ExecuteScalar();
                if (MyImg.ToString() != null)
                {
                    MemoryStream ms = new MemoryStream(MyImg);
                    Image img = Image.FromStream(ms);
                    x.Image = img;
                }
            }
            catch (Exception)
            {
                x.Image = CarInsuranceSystem.Properties.Resources._default;
            }
            con.Close();
        }
        private void appear_Click(object sender, EventArgs e)
        {
            if (appear.Left == 0)
            {
                menu.Controls.Remove(mark);
                profilePicture.Visible = true;
                while (appear.Left < 210 || menu.Width < 259)
                {
                    if (appear.Left < 211)
                        appear.Left += 8;
                    if (menu.Width < 260)
                        menu.Width += 8;
                }
                if (position == "profile")
                {
                    profile.Left = 20;
                    profilelabel.Left = 66;
                }
                else if (position == "your cars")
                {
                    viewcars.Left = 20;
                    viewcarslabel.Left = 66;
                }
                else if (position == "Members")
                {
                    members.Left = 20;
                    memberslabel.Left = 66;
                }
                else if (position == "Add Car")
                {
                    addcar.Left = 20;
                    addcarlabel.Left = 66;
                }
                else if (position == "Notification")
                {
                    notifi.Left = 20;
                    notification.Left = 66;
                }
                else if (position == "Members")
                {
                    members.Left = 20;
                    memberslabel.Left = 66;
                }
            }
            else
            {
                menu.Controls.Add(mark);

                while (appear.Left > 0 || menu.Width > 46)
                {
                    if (appear.Left != 0)
                        appear.Left -= 4;
                    if (menu.Width > 46)
                        menu.Width -= 4;

                }
                if (position == "profile")
                    profile.Left = 10;

                else if (position == "your cars")
                    viewcars.Left = 6;
                else if (position == "Members")
                    members.Left = 8;
                else if (position == "AddCar")
                    addcar.Left = 8;
                else if (position == "Notification")
                    notifi.Left = 8;
                else if (position == "Members")
                    members.Left = 8;


                profilePicture.Visible = false;
            }
        }
        private void viewcars_Click(object sender, EventArgs e)
        {
            clearpage();
            if (user_name == username)
            {
                mark.Location = new Point(0, 312);
                position = "your cars";
                title.Text = "Your Cars";
                if (appear.Left == 0)
                {
                    viewcars.Left = 6;
                    menu.Controls.Add(mark);
                }
                else
                {
                    viewcars.Left = 20;
                    viewcarslabel.Left = 66;
                }
            }
            cars.Columns.Clear();
            carspage();
            this.Controls.Add(shape);
            retrivedata();
            user_name = username;

        }
        public void retrivedata()
        {
                cars.Rows.Clear();
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand();
            if (username == user_name)
                command = new SqlCommand(@"Select  * from Car;", con);
            else
                command = new SqlCommand(@"Select  * from dbo.view_user_cars('" + user_name.ToString().Trim() + "');", con);
            

            SqlDataReader reader = command.ExecuteReader();
            int x = 0;
            while (reader.Read())
            {
                cars.Rows.Add(reader["PlateNum"].ToString(), reader["ModelNum"].ToString(), reader["ManufactureYear"].ToString(),
                    reader["Color"].ToString(), reader["MonthlyPaid"].ToString());
                if (reader["Preventinsurance"].ToString().Trim() == "1")
                    cars.Rows[x].Cells[5].Value = "Allow";
                else
                    cars.Rows[x].Cells[5].Value = "Prevent";


                x++;

            }
            reader.Close();
            con.Close();


        }
        private void Cars_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.cars.CurrentCell = this.cars.Rows[e.RowIndex].Cells[0];


            if (e.ColumnIndex == 5)
            {
                
                    if (cars.Rows[e.RowIndex].Cells[5].Value.ToString() == "Prevent")
                    {
                        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                        con.Open();
                        SqlCommand command = new SqlCommand(@"update  Car set Preventinsurance='1' where PlateNum='" + this.cars.CurrentCell.Value.ToString().Trim() +
                            "';", con);
                        command.ExecuteNonQuery();
                        this.cars.Rows[e.RowIndex].Cells[5].Value = "Allow";
                        con.Close();

                    }
                    else
                    {
                        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                        con.Open();

                        SqlCommand command = new SqlCommand(@"update  Car set Preventinsurance='0' where PlateNum='" + this.cars.CurrentCell.Value.ToString().Trim() +
                            "';", con);
                        command.ExecuteNonQuery();
                        this.cars.Rows[e.RowIndex].Cells[5].Value = "Prevent";

                        con.Close();

                    }
                
               
            }
            else if (e.ColumnIndex == 6)
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                con.Open();
                SqlCommand command = new SqlCommand("Delete_Car", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@platenum", this.cars.CurrentCell.Value.ToString().Trim()));

                command.ExecuteNonQuery();
                con.Close();

                retrivedata();

            }
        }
        private void update_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            con.Open();
            for (int i = 0; i < cars.RowCount; i++)
            {

                SqlCommand command = new SqlCommand(@"update  Car set ModelNum='" + cars.Rows[i].Cells[1].Value.ToString().Trim() +
                     "' , ManufactureYear='" + cars.Rows[i].Cells[2].Value.ToString().Trim() + "' , Color='" +
                     cars.Rows[i].Cells[3].Value.ToString().Trim() + "' , MonthlyPaid='" +
                     cars.Rows[i].Cells[4].Value.ToString().Trim() + "' where PlateNum='" +
                     cars.Rows[i].Cells[0].Value.ToString().Trim() + "';", con);
                command.ExecuteNonQuery();
            }
            con.Close();
            retrivedata();

        }
        private void power_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.OKCancel,
                      MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Hide();
                login f = new login();
                f.ShowDialog();
                this.Close();

            }
        }
        private void addcar_Click(object sender, EventArgs e)
        {
            clearpage();
            mark.Location = new Point(1, 395);
            position = "AddCar";
            title.Text = "Add Car....  ";
            if (appear.Left == 0)
            {
                addcar.Left = 8;
                menu.Controls.Add(mark);
            }
            else
            {
                addcar.Left = 20;
                addcarlabel.Left = 66;
            }
            this.Controls.Add(shape);
            add_Car();
        }
        private void add_Click(object sender, EventArgs e)
        {
            bool saveData = true;

            if (manfactureyear.SelectedIndex < 0)
            {
                manfactureyear.Focus(); saveData = false;
                manfactureyear.BackColor = Color.AliceBlue; error.Text = "Please, Enter the Manfacture year.";
                error.Visible = true;
            }

            if (platenumber.Text.Trim() == "" || (platenumber.Text.Trim() == "Plate number" && platenumber.ForeColor == Color.DarkGray))
            {
                platenumber.Focus(); saveData = false;
                platenumber.BackColor = Color.AliceBlue;
                error.Text = "Please, Enter the Plate number."; error.Visible = true;
            }


            if (color.Text.Trim() == "" || (color.Text.Trim() == "Color" && color.ForeColor == Color.DarkGray))
            {
                color.Focus(); saveData = false;
                color.BackColor = Color.AliceBlue;
                error.Text = "Please, Enter Color."; error.Visible = true;
            }


            if (model.Text.Trim() == "" || (model.Text.Trim() == "Model" && model.ForeColor == Color.DarkGray))
            {
                model.Focus(); model.BackColor = Color.AliceBlue;
                error.Text = "Please, Enter Model.";
                error.Visible = true;
                saveData = false;
            }
            if (username_addcar.SelectedIndex < 0)
            {
                username_addcar.Focus(); username_addcar.BackColor = Color.AliceBlue;
                error.Text = "Please, Enter a Username.";
                error.Visible = true;
                saveData = false;
            }


            if (saveData)
            {
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                connection.Open();
                SqlCommand command = new SqlCommand("Add_Car", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@User_Name", username_addcar.SelectedItem.ToString().Trim()));
                command.Parameters.Add(new SqlParameter("@Model_Number", model.Text.Trim()));
                command.Parameters.Add(new SqlParameter("@Manufacture_year", manfactureyear.Text.Trim()));
                command.Parameters.Add(new SqlParameter("@Color", color.Text.Trim()));
                command.Parameters.Add(new SqlParameter("@Plate_Number", platenumber.Text.Trim()));
                command.ExecuteNonQuery();
                connection.Close();
                reset();
                this.Refresh();
                MessageBox.Show("Car added  successfully.");
            }
        }
        private void profile_Click(object sender, EventArgs e)
        {
            clearpage();
            if (user_name == username)
            {
                mark.Location = new Point(1, 577);
                position = "profile";
                title.Text = "Profile";
                if (appear.Left == 0)
                {
                    profile.Left = 10;
                    menu.Controls.Add(mark);
                }
                else
                {
                    profile.Left = 20;
                    profilelabel.Left = 66;
                }
            }
            this.Controls.Add(shape);
            profilepage();
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand("Select  * from Car_Owner where UserName='" + user_name + "';", con);
            SqlDataReader reader = command.ExecuteReader();
            string typeofin = "";
            string ge = "";
            while (reader.Read())
            {
                firstname.Text = reader["FirstName"].ToString().Trim();
                lastname.Text = reader["LastName"].ToString().Trim();
                email.Text = reader["Email"].ToString().Trim();
                password.Text = reader["Password"].ToString().Trim();
                phonenum1.Text = reader["PhoneNum1"].ToString().Trim();
                phonenum2.Text = reader["PhoneNum2"].ToString().Trim();
                ge = reader["Gender"].ToString().Trim();
                typeofin = reader["TypeOfInsurance"].ToString().Trim();
                if (reader["Image"].ToString() != "")
                {
                    byte[] MyImg = (byte[])reader["Image"];
                    MemoryStream ms = new MemoryStream(MyImg);
                    Image img = Image.FromStream(ms); //error 
                    image.Image = img;
                }
                user_name = username;
            }

            reader.Close();
            con.Close();
            if (typeofin == "Employee")
                type.SelectedIndex = 0;
            else
                type.SelectedIndex = 1;

            if (ge == "Female")
                gender.SelectedIndex = 0;
            else
                gender.SelectedIndex = 1;
        }
        private void savepro_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand(@"update Car_Owner set FirstName='" + firstname.Text.ToString().Trim() + "' ,LastName='" + lastname.Text.ToString().Trim() +
                "' ,Email='" + email.Text.ToString().Trim() + "' ,PhoneNum1='" + phonenum1.Text.ToString().Trim() + "' ,PhoneNum2='" + phonenum2.Text.ToString().Trim() +
                "' ,TypeOfInsurance='" + type.SelectedItem.ToString().Trim() + "' ,Gender='" + gender.SelectedItem.ToString().Trim() + "' , Password='" + password.Text + "' where UserName='" + user_name + "';", con);
            command.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Done Successfuly.");
        }
        private void notifi_Click(object sender, EventArgs e)
        {
            clearpage();
            mark.Location = new Point(1, 446);
            position = "Notification";
            title.Text = "Notification";
            if (appear.Left == 0)
            {
                notifi.Left = 8;
                menu.Controls.Add(mark);
            }
            else
            {
                notifi.Left = 20;
                notification.Left = 66;
            }
            this.Controls.Add(shape);
            notificationadmin.Columns.Clear();
            notificationOfadmin();
           retrivedmin_notifiData();
        }
        private void notificationadmin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
                specialData(this.notificationadmin.Rows[e.RowIndex].Cells[1].Value.ToString());

            
            else if (e.ColumnIndex == 3)
            {
                if (notificationadmin.Rows[e.RowIndex].Cells[0].Value.ToString().Trim() == "Insurance")
                {
                    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                    con.Open();
                    SqlCommand command = new SqlCommand(@"update Car SET Request='RA'  where PlateNum='" + notificationadmin.Rows[e.RowIndex].Cells[1].Value.ToString().Trim() + "';", con);
                    command.ExecuteNonQuery();
                    con.Close();

                    retrivedmin_notifiData();


                }
            }
            else if (e.ColumnIndex == 4)
            {
                if (notificationadmin.Rows[e.RowIndex].Cells[0].Value.ToString().Trim() == "Insurance")
                {
                    SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                    con.Open();
                    SqlCommand command = new SqlCommand(@"update Car SET Request = 'RF'  where PlateNum='" + notificationadmin.Rows[e.RowIndex].Cells[1].Value.ToString().Trim() + "'; ", con);
                    command.ExecuteNonQuery();
                    con.Close();

                    retrivedmin_notifiData();

                }


            }
        }
        private void specialData(string platenum)
        {
            clearpage();
            Label  label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            Label label4 = new Label();
            Label label5 = new Label();
            Label label6 = new Label();
            Label label7 = new Label();
            Label carPlatenum = new Label();
            Label type = new Label();
            Label email = new Label();
            Label name = new Label();
            Label model = new Label();
            Label manfactureyear = new Label();
            Label color = new Label();
            PictureBox back = new PictureBox();
            GroupBox data = new GroupBox();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = Color.Black;
            label1.Location = new Point(24, 30);
            label1.Size = new Size(45, 16);
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic,GraphicsUnit.Point, ((byte)(0)));
            label2.ForeColor = Color.Black;
            label2.Location = new Point(24, 63);
            label2.Size = new Size(42, 16);
            label2.Text = "Email";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.ForeColor = Color.Black;
            label3.Location = new Point(24, 127);
            label3.Size = new Size(108, 16);
            label3.Text = "CarPlateNumber";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.White;
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.ForeColor = System.Drawing.Color.Black;
            label4.Location = new System.Drawing.Point(368, 30);
            label4.Size = new System.Drawing.Size(40, 16);
            label4.Text = "Color";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.Color.White;
            label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.ForeColor = System.Drawing.Color.Black;
            label5.Location = new System.Drawing.Point(24, 94);
            label5.Size = new System.Drawing.Size(117, 16);
            label5.Text = "Type Of Insurance";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.AliceBlue;
            label6.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            label6.ForeColor = Color.Black;
            label6.Location = new Point(368, 99);
            label6.Size = new Size(97, 16);
            label6.Text = "Model Number";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.White;
            label7.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            label7.ForeColor = Color.Black;
            label7.Location = new Point(368, 66);
            label7.Size = new Size(106, 16);
            label7.Text = "Manfacture Year";
            // 
            // carPlatenum
            // 
            carPlatenum.AutoSize = true;
            carPlatenum.BackColor = Color.Black;
            carPlatenum.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            carPlatenum.ForeColor = Color.White;
            carPlatenum.Location = new Point(191, 127);
            carPlatenum.Size = new Size(45, 16);
            carPlatenum.Text = "Name";
            // 
            // type
            // 
            type.AutoSize = true;
            type.BackColor = Color.Black;
            type.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            type.ForeColor = Color.White;
            type.Location = new Point(191, 94);
            type.Size = new Size(45, 16);
            type.Text = "Name";
            // 
            // email
            // 
            email.AutoSize = true;
            email.BackColor = Color.Black;
            email.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            email.ForeColor = Color.White;
            email.Location = new Point(191, 63);
            email.Size = new Size(45, 16);
            email.Text = "Name";
            // 
            // name
            // 
            name.AutoSize = true;
            name.BackColor = Color.Black;
            name.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            name.ForeColor = Color.White;
            name.Location = new Point(191, 30);
            name.Size = new Size(45, 16);
            name.Text = "Name";
            // 
            // model
            // 
            model.AutoSize = true;
            model.BackColor = Color.Black;
            model.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            model.ForeColor = Color.White;
            model.Location = new Point(551, 99);
            model.Size = new Size(45, 16);
            model.Text = "Name";
            // 
            // manfactureyear
            // 
            manfactureyear.AutoSize = true;
            manfactureyear.BackColor = Color.Black;
            manfactureyear.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            manfactureyear.ForeColor = Color.White;
            manfactureyear.Location = new Point(551, 66);
            manfactureyear.Size = new Size(45, 16);
            manfactureyear.Text = "Name";
            // 
            // color
            // 
            color.AutoSize = true;
            color.BackColor = Color.Black;
            color.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            color.ForeColor = Color.White;
            color.Location = new Point(551, 30);
            color.Size = new Size(45, 16);
            color.Text = "Name";
            // 
            // button4
            // 
            back.Image = CarInsuranceSystem.Properties.Resources.back3;
            back.BorderStyle = BorderStyle.None;
            back.SizeMode = PictureBoxSizeMode.StretchImage;
            back.Location = new Point(200, 350);
            back.Size = new Size(50, 50);
            back.Click += new EventHandler(notifi_Click);
            // 
            // specificcarDetail
            // 
            //
            //
            //
            data.Location = new Point(400, 250);
            data.Size = new Size(700, 250);
            data.TabStop = false;
            data.Controls.Add(color);
            data.Controls.Add(model);
            data.Controls.Add(manfactureyear);
            data.Controls.Add(name);
            data.Controls.Add(email);
            data.Controls.Add(type);
            data.Controls.Add(carPlatenum);
            data.Controls.Add(label7);
            data.Controls.Add(label6);
            data.Controls.Add(label5);
            data.Controls.Add(label4);
            data.Controls.Add(label3);
            data.Controls.Add(label2);
            data.Controls.Add(label1);
            this.Controls.Add(back);
            this.Controls.Add(data);
            this.Controls.Add(shape);
            //
            //data load
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand(@"SELECT ModelNum,ManufactureYear,Color,Owner_ID
              FROM Car WHERE PlateNum='" + platenum.ToString().Trim() + "';", con);
            SqlDataReader reader = command.ExecuteReader();
            int id = -1;
            while (reader.Read())
            {

                carPlatenum.Text = platenum;
                color.Text = reader["Color"].ToString();
                manfactureyear.Text = reader["ManufactureYear"].ToString();
                model.Text = reader["ModelNum"].ToString();
                id = (int)reader["Owner_ID"];


            }
            reader.Close();
            command = new SqlCommand(@"SELECT FirstName,LastName,Email,TypeOfInsurance
              FROM Car_Owner WHERE User_ID=" + id.ToString() + ";", con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {

                name.Text = reader["FirstName"] + " " + reader["LastName"];
                email.Text = reader["Email"].ToString().Trim();
                type.Text = reader["TypeOfInsurance"].ToString();

            }

            reader.Close();
            con.Close();
                //

        }
        public void retrivedmin_notifiData()
        {
            notificationadmin.Rows.Clear();
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand(@"SELECT PlateNum FROM Car WHERE Request='R';", con);

            SqlDataReader reader = command.ExecuteReader();
            int x = 0;
            while (reader.Read())
            {
                notificationadmin.Rows.Add("Insurance", reader["PlateNum"].ToString(), "Details", "Accept", "Refuse");
                x++;
            }


            reader.Close();
            command = new SqlCommand(@"SELECT PlateNum FROM Car INNER JOIN CarAccidents ON Car.Car_ID= CarAccidents.Car_ID  ", con);

            reader = command.ExecuteReader();
            while (reader.Read())
            {

                notificationadmin.Rows.Add("Accident", reader["PlateNum"].ToString());
                notificationadmin.Rows[x].Cells[3].Value = "Call Soon";
                notificationadmin.Rows[x].Cells[4].Value = "";

                x++;
            }

            reader.Close();
            con.Close();


        }
        private void members_Click(object sender, EventArgs e)
        {
            clearpage();
            mark.Location = new Point(1, 358);
            position = "Members";
            title.Text = "Members";
            if (appear.Left == 0)
            {
                members.Left = 8;
                menu.Controls.Add(mark);
            }
            else
            {
                members.Left = 20;
                memberslabel.Left = 66;
            }
            this.Controls.Add(shape);
            grid.Columns.Clear();
            memberpage();
            members_data();
        }
        private void members_data()
        {
            grid.Rows.Clear();
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand("Select  * from dbo.view_users()", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                grid.Rows.Add(reader["FirstName"].ToString() + " " + reader["LastName"].ToString(), reader["Email"].ToString());
            }
            reader.Close();
            con.Close();
        }
        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                this.grid.CurrentCell = this.grid.Rows[e.RowIndex].Cells[1];
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                con.Open();
                SqlCommand command = new SqlCommand(@"Select UserName from  Car_Owner where Email='"+ this.grid.CurrentCell.Value.ToString()+
                    "';", con);         
                user_name = (String)command.ExecuteScalar();
                con.Close();
                back1.Image = CarInsuranceSystem.Properties.Resources.back3;
                back1.BorderStyle = BorderStyle.None;
                back1.SizeMode = PictureBoxSizeMode.StretchImage;
                back1.Location = new Point(200, 350);
                back1.Size = new Size(50, 50);
                profile_Click(sender,e);
                this.Controls.Add(back1);
                user_name = username;

            }
            else if (e.ColumnIndex == 3)
            {
                this.grid.CurrentCell = this.grid.Rows[e.RowIndex].Cells[1];
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                con.Open();
                SqlCommand command = new SqlCommand("Delete_User", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@email", this.grid.CurrentCell.Value.ToString()));
                try
                {
                    command.ExecuteNonQuery();
                }
                catch(Exception  ){
                    MessageBox.Show("ok");
                }
                con.Close();
                members_data();
            }
            else if (e.ColumnIndex == 4)
            {
                this.grid.CurrentCell = this.grid.Rows[e.RowIndex].Cells[1];
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                con.Open();
                SqlCommand command = new SqlCommand(@"Select UserName from  Car_Owner where Email='" + this.grid.CurrentCell.Value.ToString() +
                    "';", con);
                user_name = (String)command.ExecuteScalar();
                con.Close();
                back2.Image = CarInsuranceSystem.Properties.Resources.back3;
                back2.BorderStyle = BorderStyle.None;
                back2.SizeMode = PictureBoxSizeMode.StretchImage;
                back2.Location = new Point(700, 80);
                back2.Size = new Size(50, 50);
                viewcars_Click(sender,e);
                this.Controls.Add(back2);
                user_name = username;
            }
        }
        private void search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (search.SelectedIndex == 0)
            {
                grid.Rows.Clear();
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                con.Open();

                SqlCommand command = new SqlCommand(@"select * from search('Employee');", con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    grid.Rows.Add(reader["FirstName"].ToString() + " " + reader["LastName"].ToString(), reader["Email"].ToString());

                reader.Close();
                con.Close();
            }
            else if (search.SelectedIndex == 1)
            {
                grid.Rows.Clear();
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                con.Open();
                SqlCommand command = new SqlCommand(@"select * from search('Student');", con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    grid.Rows.Add(reader["FirstName"].ToString() + " " + reader["LastName"].ToString(), reader["Email"].ToString());

                reader.Close();
                con.Close();
            }
            else if (search.SelectedIndex == 2)
                members_data();
        }
        private void memberpage()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            PictureBox pictureBox1 = new PictureBox();
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            name,
            email_grid,
            edit,
            delete,
            cars_grid});
            grid.Location = new System.Drawing.Point(120, 150);
            grid.ReadOnly = true;
            grid.Size = new System.Drawing.Size(1200, 550);
            // 
            // name
            // 
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style.BackColor =Color.Black;
            style.ForeColor = Color.White;
            style.SelectionBackColor = Color.MidnightBlue;
            style.SelectionForeColor = Color.White;
            name.DefaultCellStyle = style;
            name.FillWeight = 203.0457F;
            name.HeaderText = "Name";
            name.ReadOnly = true;
            // 
            // email
            // 
            email_grid.DefaultCellStyle = style;
            email_grid.HeaderText = "Email";
            email_grid.ReadOnly = true;
            // 
            // edit
            // 
            edit.DefaultCellStyle = style;
            edit.FillWeight = 38.45745F;
            edit.FlatStyle = FlatStyle.Popup;
            edit.HeaderText = "Edit";
            edit.ReadOnly = true;
            edit.Text = "Edit Data";
            edit.UseColumnTextForButtonValue = true;
            // 
            // delete
            // 
            delete.DefaultCellStyle = style;
            delete.FillWeight = 56.49335F;
            delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            delete.HeaderText = "Delete";
            delete.ReadOnly = true;
            delete.Text = "Delete User";
            delete.UseColumnTextForButtonValue = true;
            // 
            // cars
            // 
            cars_grid.DefaultCellStyle = style;
            cars_grid.FillWeight = 102.0036F;
            cars_grid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            cars_grid.HeaderText = "Cars";
            cars_grid.ReadOnly = true;
            cars_grid.Text = "View Cars";
            cars_grid.UseColumnTextForButtonValue = true;
            // 
            // search
            // 
            search.FlatStyle = FlatStyle.Popup;
            search.FormattingEnabled = true;
            search.Items.Clear();
            search.Items.AddRange(new object[] {
            "Employee",
            "Student",
            "All"});
            search.Location = new Point(644, 116);
            search.Size = new Size(157, 21);
            search.Text = "Search...";
            search.SelectedIndexChanged += new System.EventHandler(search_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ControlLightLight;
            pictureBox1.Image = CarInsuranceSystem.Properties.Resources.search;
            pictureBox1.Location = new Point(599, 112);
            pictureBox1.Size = new Size(39, 25);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabStop = false;
            // 
            // members
            // 
            this.Controls.Add(pictureBox1);
            this.Controls.Add(search);
            this.Controls.Add(grid);
        }
        private void notificationOfadmin()
        {
          
            // request
            // 
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style.BackColor = Color.Black;
            style.ForeColor = Color.White;
            style.SelectionBackColor = Color.MidnightBlue;
            request.DefaultCellStyle = style;
            request.FillWeight = 203.0457F;
            request.HeaderText = "Request";
            request.ReadOnly = true;
            // 
            // car
            // 
            car.DefaultCellStyle = style;
            car.HeaderText = "Car PlateNumber";
            car.ReadOnly = true;
            // 
            // details
            // 
            details.HeaderText = "Details";
            details.ReadOnly = true;
            details.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            details.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            details.Text = "Details";
            details.UseColumnTextForButtonValue = true;
            // 
            // accept
            // 
            accept.DefaultCellStyle = style;
            accept.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            accept.HeaderText = "Accept";
            accept.ReadOnly = true;
            accept.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            accept.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            accept.Text = "Accept";
            accept.ToolTipText = "Accept";
            // 
            // refuse
            // 
            refuse.FillWeight = 56.49335F;
            refuse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            refuse.HeaderText = "Refuse";
            refuse.ReadOnly = true;
            refuse.Text = "Refuse";
            // 
            // grid
            // 
            notificationadmin.AllowUserToAddRows = false;
            notificationadmin.BorderStyle = BorderStyle.None;
            notificationadmin.AllowUserToDeleteRows = false;
            notificationadmin.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            notificationadmin.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            notificationadmin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            notificationadmin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            request,
            car,
            details,
            accept,
            refuse});
            notificationadmin.Location = new Point(120, 150);
            notificationadmin.ReadOnly = true;
            notificationadmin.Size = new Size(1200, 550);
            // 
            // notificatioOfUser
            //           
            this.Controls.Add(notificationadmin);

        }
        private void carspage()
        {
            DataGridViewTextBoxColumn platenum = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn modnum = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn manfacture = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn color = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn monthlypaid = new DataGridViewTextBoxColumn();
            DataGridViewButtonColumn preventinsurance = new DataGridViewButtonColumn();
            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
            // platenum
            // 
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style.BackColor = Color.Black;
            style.ForeColor = Color.White;
            style.SelectionBackColor = Color.White;
            style.SelectionForeColor = Color.Black;
            platenum.DefaultCellStyle = style;
            platenum.FillWeight = 203.0457F;
            platenum.HeaderText = "PlateNumber";
            platenum.ReadOnly = true;
            // 
            // modnum
            //           
            modnum.DefaultCellStyle = style;
            modnum.HeaderText = "Model Number";
            // 
            // manfacture
            // 
            manfacture.DefaultCellStyle = style;
            manfacture.HeaderText = "Manfacture Year";
            // 
            // color
            // 
            color.DefaultCellStyle = style;
            color.HeaderText = "Color";
            // 
            // monthlypaid
            // 
            monthlypaid.DefaultCellStyle = style;
            monthlypaid.HeaderText = "MonthlyPaid";
            // 
            // preventinsurance
            // 
            preventinsurance.DefaultCellStyle = style;
            preventinsurance.FillWeight = 102.0036F;
            preventinsurance.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            preventinsurance.HeaderText = "Insurance";
            preventinsurance.Text = "Prevent";
            // 
            // delete
            // 
            delete.DefaultCellStyle = style;
            delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            delete.HeaderText = "Delete";
            delete.Name = "delete";
            delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            delete.Text = "Delete";
            delete.UseColumnTextForButtonValue = true;
            // 
            cars.AllowUserToAddRows = false;
            cars.AllowUserToDeleteRows = false;
            cars.AllowUserToOrderColumns = true;
            cars.BorderStyle = BorderStyle.None;
            cars.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            cars.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            cars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            cars.Columns.AddRange(new DataGridViewColumn[] {
             platenum,
             modnum,
             manfacture,
             color,
             monthlypaid,
             preventinsurance,
             delete});
            cars.Location = new System.Drawing.Point(120, 150);
            cars.Size = new System.Drawing.Size(1200, 500);
            //
            //update
            // button1
            // 
            update.BackColor = System.Drawing.Color.Black;
            update.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            update.Font = new Font("Arial Narrow", 20F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            update.ForeColor = Color.White;
            update.Location = new Point(1150, 650);
            update.Size = new Size(150, 60);
            update.Text = "Update";
            update.UseVisualStyleBackColor = false;
            // 
            //
            this.Controls.Add(cars);
            this.Controls.Add(update);

        }
        private void add_Car()
        {
            GroupBox addCar = new GroupBox();
            //   
            // username
            // 
            username_addcar.ForeColor = System.Drawing.Color.DarkGray;
            username_addcar.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Italic);
            username_addcar.ForeColor = System.Drawing.Color.DarkGray;
            username_addcar.FormattingEnabled = true;
            username_addcar.Location = new System.Drawing.Point(300, 80);
            username_addcar.Size = new System.Drawing.Size(300, 30);
            username_addcar.Text = "Username....";
            username_addcar.Items.Clear();
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand("SELECT UserName FROM Car_Owner where Admin='0';", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                username_addcar.Items.Add(reader["UserName"]);
            reader.Close();
            con.Close();
            //        
            // model
            // 
            model.BorderStyle = System.Windows.Forms.BorderStyle.None;
            model.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Italic);
            model.ForeColor = System.Drawing.Color.DarkGray;
            model.BackColor = SystemColors.Info;
            model.Location = new System.Drawing.Point(100, 160);
            model.Multiline = true;
            model.Size = new Size(300, 30);
            model.TabIndex = 63;
            model.Text = "Model";
            textOfbox(model);
            //
            // color
            // 
            color.BorderStyle = BorderStyle.None;
            color.Font = new Font("Lucida Bright", 12F, System.Drawing.FontStyle.Italic);
            color.ForeColor = Color.DarkGray;
            color.BackColor = SystemColors.Info;
            color.Location = new Point(500, 160);
            color.Multiline = true;
            color.Size = new Size(300, 30);
            color.Text = "Color";
            textOfbox(color);
            //  
            // platenumber
            // 
            platenumber.BorderStyle = BorderStyle.None;
            platenumber.Font = new Font("Lucida Bright", 12F, System.Drawing.FontStyle.Italic);
            platenumber.ForeColor = Color.DarkGray;
            platenumber.BackColor = SystemColors.Info;
            platenumber.Location = new Point(100, 250);
            platenumber.Multiline = true;
            platenumber.Size = new Size(300, 30);
            platenumber.Text = "Plate number";
            textOfbox(platenumber);
            // 
            // manfactureyear
            // 
            manfactureyear.Font = new Font("Lucida Bright", 12F, System.Drawing.FontStyle.Italic);
            manfactureyear.ForeColor = Color.DarkGray;
            manfactureyear.FormattingEnabled = true;
            manfactureyear.Items.AddRange(new object[] {
            "2018",
            "2017",
            "2016",
            "2015",
            "2014",
            "2013",
            "2012",
            "2011",
            "2010",
            "2009",
            "2008",
            "2007",
            "2006",
            "2005",
            "2004",
            "2003",
            "2002",
            "2001",
            "2000",
            "1999",
            "1998",
            "1997",
            "1996",
            "1995",
            "1994",
            "1993",
            "1992",
            "1991",
            "1990",
            "1989",
            "1988",
            "1987",
            "1986",
            "1985",
            "1984",
            "1983",
            "1982",
            "1981",
            "1980",
            "1979",
            "1978",
            "1977",
            "1976",
            "1975",
            "1974",
            "1973",
            "1972",
            "1971",
            "1970",
            "1969",
            "1968",
            "1967",
            "1966",
            "1965",
            "1964",
            "1963",
            "1962",
            "1961",
            "1960"});
            manfactureyear.Location = new System.Drawing.Point(500, 250);
            manfactureyear.Size = new System.Drawing.Size(300, 30);
            manfactureyear.Text = "Manfacture Year";
            // 
            // error
            // 
            error.AutoSize = true;
            error.Font = new System.Drawing.Font("Lucida Handwriting", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            error.ForeColor = System.Drawing.Color.Red;
            error.Location = new System.Drawing.Point(400, 400);
            error.Size = new System.Drawing.Size(300, 100);
            error.Text = "Fill all the information cells !!";
            error.Visible = false;
            // 
            // add
            // 
            add.BackColor = System.Drawing.Color.Brown;
            add.ForeColor = System.Drawing.Color.White;
            add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            add.Font = new Font("Arial Narrow", 20F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            add.Location = new System.Drawing.Point(600, 450);
            add.Size = new System.Drawing.Size(250, 75);
            add.Text = "Add Car";
            add.UseVisualStyleBackColor = false;
            // 
            // addCar
            // 
            addCar.Controls.Add(add);
            addCar.Controls.Add(error);
            addCar.Controls.Add(platenumber);
            addCar.Controls.Add(color);
            addCar.Controls.Add(model);
            addCar.Controls.Add(manfactureyear);
            addCar.Controls.Add(username_addcar);
            addCar.Location = new Point(300, 100);
            addCar.Size = new Size(1000, 600);
            addCar.TabStop = false;
            this.Controls.Add(addCar);
            // 

        }
        private void left()
        {
            //
            //menu
            menu.Location = new Point(0, 20);
            menu.Size = new Size(46, 900);
            menu.TabIndex = 88;
            menu.BackColor = SystemColors.MenuText;
            //
            //appear
            //
            appear.BackColor = SystemColors.ButtonHighlight;
           appear.Image = CarInsuranceSystem.Properties.Resources.appear;
            appear.Location = new Point(0, 40);
            appear.Size = new Size(43, 32);
            appear.SizeMode = PictureBoxSizeMode.StretchImage;
            appear.TabIndex = 84;
            appear.TabStop = false;
            appear.Click += new EventHandler(this.appear_Click);
            //
            // profilePicture
            // 
            profilePicture.BackColor = SystemColors.ButtonHighlight;
            profilePicture.Location = new Point(20, 100);
            profilePicture.Size = new Size(220, 200);
            profilePicture.SizeMode = PictureBoxSizeMode.StretchImage;
            profilePicture.TabIndex = 74;
            profilePicture.TabStop = false;
            picturedisplay(profilePicture);
            profilePicture.Visible = false;
            // 
            // viewcars
            // 
            viewcars.BackColor = SystemColors.MenuText;
            viewcars.Image = CarInsuranceSystem.Properties.Resources.yourcars;
            viewcars.Location = new Point(0, 312);
            viewcars.Size = new Size(45, 40);
            viewcars.SizeMode = PictureBoxSizeMode.StretchImage;
            viewcars.TabIndex = 83;
            viewcars.TabStop = false;
            viewcars.Click += new EventHandler(viewcars_Click);
            //
            //viewcarslabel
            // 
            viewcarslabel.AutoSize = true;
            viewcarslabel.Font = new Font("Ink Free", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            viewcarslabel.ForeColor = SystemColors.Window;
            viewcarslabel.Location = new Point(51, 312);
            viewcarslabel.Size = new Size(124, 34);
            viewcarslabel.TabIndex = 76;
            viewcarslabel.Text = "Cars";
            viewcarslabel.Click += new EventHandler(viewcars_Click);
            // 
            // members
            // 
            members.BackColor = SystemColors.MenuText;
            members.Image = CarInsuranceSystem.Properties.Resources.members;
            members.Location = new Point(7, 358);
            members.Size = new Size(30, 30);
            members.SizeMode = PictureBoxSizeMode.StretchImage;
            members.TabIndex = 81;
            members.TabStop = false;
            members.Click += new EventHandler(members_Click);
            // 
            // memberslabel
            // 
            memberslabel.AutoSize = true;
            memberslabel.Font = new Font("Ink Free", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            memberslabel.ForeColor = SystemColors.Window;
            memberslabel.Location = new Point(51, 358);
            memberslabel.Size = new Size(109, 34);
            memberslabel.TabIndex = 79;
            memberslabel.Text = "Members";
            memberslabel.Click += new EventHandler(members_Click);
            // 

            // addcar
            // 
            addcar.Image = CarInsuranceSystem.Properties.Resources.addcar;
            addcar.Location = new Point(3, 395);
            addcar.Size = new Size(40, 40);
            addcar.SizeMode = PictureBoxSizeMode.StretchImage;
            addcar.TabIndex = 82;
            addcar.TabStop = false;
            addcar.Click += new EventHandler(addcar_Click);
            // 
            // addcarlabel
            // 
            addcarlabel.AutoSize = true;
            addcarlabel.Font = new Font("Ink Free", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            addcarlabel.ForeColor = SystemColors.Window;
            addcarlabel.Location = new Point(51, 400);
            addcarlabel.Size = new Size(106, 34);
            addcarlabel.TabIndex = 80;
            addcarlabel.Text = "Add Car";
            addcarlabel.Click += new EventHandler(addcar_Click);
            // 
            // notifi
            //
            notifi.BackColor = SystemColors.ButtonHighlight;
            notifi.Image = CarInsuranceSystem.Properties.Resources.notification;
            notifi.Location = new Point(3, 446);
            notifi.Size = new Size(38, 34);
            notifi.SizeMode = PictureBoxSizeMode.StretchImage;
            notifi.TabIndex = 75;
            notifi.TabStop = false;
            notifi.Click += new System.EventHandler(notifi_Click);
            //
            // notification
            // 
            notification.AutoSize = true;
            notification.Font = new Font("Ink Free", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            notification.ForeColor = SystemColors.Window;
            notification.Location = new Point(51, 446);
            notification.Size = new Size(161, 34);
            notification.TabIndex = 78;
            notification.Text = "Notifications";
            notification.Click += new System.EventHandler(notifi_Click);
            // 
            // profile
            // 
            profile.BackColor = Color.Black;
            profile.Image = CarInsuranceSystem.Properties.Resources.profiledata;
            profile.Location = new Point(5, 577);
            profile.Size = new Size(38, 34);
            profile.SizeMode = PictureBoxSizeMode.StretchImage;
            profile.TabIndex = 85;
            profile.TabStop = false;
            profile.Click += new System.EventHandler(this.profile_Click);
            // 
            // profile label
            // 
            profilelabel.Font = new Font("Ink Free", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            profilelabel.ForeColor = SystemColors.Window;
            profilelabel.Location = new Point(51, 587);
            profilelabel.Size = new Size(130, 25);
            profilelabel.TabIndex = 76;
            profilelabel.Text = username;
            //profilelabel.Text = "Profile";
            /*SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand(@"Select FirstName, LastName from Car_Owner where UserName='" + username + "';", con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            profilelabel.Text = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
            reader.Close();
            con.Close();*/
            profilelabel.Click += new System.EventHandler(this.profile_Click);

            // 
            // question
            // 
            question.BackColor = SystemColors.ButtonHighlight;
            question.BorderStyle = BorderStyle.None;
            question.Image = CarInsuranceSystem.Properties.Resources.question;
            question.Location = new Point(5, 617);
            question.Size = new Size(38, 34);
            question.SizeMode = PictureBoxSizeMode.StretchImage;
            question.TabIndex = 86;
            question.TabStop = false;
            // 
            // question label
            // 
            questionlabel.Font = new Font("Ink Free", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            questionlabel.ForeColor = SystemColors.Window;
            questionlabel.Location = new Point(51, 627);
            questionlabel.Size = new Size(124, 34);
            questionlabel.TabIndex = 76;
            questionlabel.Text = "Help";
            // 
            // settings
            // 
            settings.BackColor = SystemColors.ButtonHighlight;
            settings.BorderStyle = BorderStyle.None;
            settings.Image = CarInsuranceSystem.Properties.Resources.settings;
            settings.Location = new Point(5, 657);
            settings.Size = new Size(38, 34);
            settings.SizeMode = PictureBoxSizeMode.StretchImage;
            settings.TabIndex = 87;
            settings.TabStop = false;
            //     
            // setting label
            // 
            settingslabel.Font = new Font("Ink Free", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            settingslabel.ForeColor = SystemColors.Window;
            settingslabel.Location = new Point(51, 667);
            settingslabel.Size = new Size(124, 34);
            settingslabel.TabIndex = 76;
            settingslabel.Text = "Settings";
            //      

            menu.Controls.Add(settings);
            menu.Controls.Add(question);
            menu.Controls.Add(profile);
            menu.Controls.Add(appear);
            menu.Controls.Add(profilePicture);
            menu.Controls.Add(viewcars);
            menu.Controls.Add(addcar);
            menu.Controls.Add(members);
            menu.Controls.Add(addcarlabel);
            menu.Controls.Add(notifi);
            menu.Controls.Add(memberslabel);
            menu.Controls.Add(notification);
            menu.Controls.Add(viewcarslabel);
            menu.Controls.Add(profilelabel);
            menu.Controls.Add(questionlabel);
            menu.Controls.Add(settingslabel);
            this.Controls.Add(menu);
            //
            //mark
            mark.Location = new Point(1, 577);
            mark.Size = new Size(5, 34);
            mark.BackColor = Color.Pink;
            //

        }
        private void profilepage()
        {
            // image
            // 
            image.BackColor = SystemColors.ButtonHighlight;
            image.BorderStyle = BorderStyle.FixedSingle;
            image.Location = new Point(450, 53);
            image.Size = new Size(400, 300);
            image.SizeMode = PictureBoxSizeMode.StretchImage;
            picturedisplay(image);
            //
            // clear
            // 
            clear.BackColor = SystemColors.ActiveCaptionText;
            clear.FlatStyle = FlatStyle.Popup;
            clear.ForeColor = SystemColors.ControlLightLight;
            clear.Location = new Point(625, 354);
            clear.Size = new Size(78, 23);
            clear.TabIndex = 21;
            clear.Text = "Claer";
            clear.UseVisualStyleBackColor = false;
            //  
            //
            // 
            // firstnamelabel
            // 
            Label first = new Label();
            first.ForeColor = Color.FromArgb(0, 42, 56);
            first.Font = new Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            first.Location = new Point(295, 400);
            first.Size = new Size(85, 26);
            first.Text = "FirstName";
            // 
            // firstname
            firstname.BackColor = SystemColors.Info;
            firstname.Multiline = true;
            firstname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            firstname.Font = new System.Drawing.Font("Lucida Bright", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            firstname.ForeColor = System.Drawing.Color.Gray;
            firstname.Location = new Point(380, 400);
            firstname.Size = new Size(214, 26);
            firstname.Text = "";
            // 
            // lastnamelabel
            // 
            Label last = new Label();
            last.ForeColor = Color.FromArgb(0, 42, 56);
            last.Font = new Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            last.Location = new Point(295, 450);
            last.Size = new Size(85, 26);
            last.Text = "LastName";
            // 
            // lastname
            // 
            lastname.BackColor = SystemColors.Info;
            lastname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lastname.Font = new System.Drawing.Font("Lucida Bright", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lastname.ForeColor = System.Drawing.Color.Gray;
            lastname.Location = new Point(380, 450);
            lastname.Multiline = true;
            lastname.Size = new Size(214, 26);
            lastname.Text = "";
            // 
            // emaillabel
            // 
            Label emaillabel = new Label();
            emaillabel.ForeColor = Color.FromArgb(0, 42, 56);
            emaillabel.Font = new Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            emaillabel.Location = new Point(295, 500);
            emaillabel.Size = new Size(85, 26);
            emaillabel.Text = "Email";
            // 
            // email
            // 
            email.BackColor = SystemColors.Info;
            email.BorderStyle = System.Windows.Forms.BorderStyle.None;
            email.Font = new System.Drawing.Font("Lucida Bright", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            email.ForeColor = System.Drawing.Color.Gray;
            email.Location = new Point(380, 500);
            email.Multiline = true;
            email.Size = new Size(214, 26);
            email.Text = "";
            // 
            // passlabel
            // 
            Label passlabel = new Label();
            passlabel.ForeColor = Color.FromArgb(0, 42, 56);
            passlabel.Font = new Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            passlabel.Location = new Point(295, 550);
            passlabel.Size = new Size(85, 26);
            passlabel.Text = "Password";
            // 
            // password
            // 
            password.BackColor = SystemColors.Info;
            password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            password.Font = new System.Drawing.Font("Lucida Bright", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            password.ForeColor = System.Drawing.Color.Gray;
            password.Location = new Point(380, 550);
            password.Multiline = true;
            password.Size = new Size(214, 26);
            // 
            // phone1label
            // 
            Label phone1label = new Label();
            phone1label.ForeColor = Color.FromArgb(0, 42, 56);
            phone1label.Font = new Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            phone1label.Location = new Point(660, 400);
            phone1label.Size = new Size(100, 26);
            phone1label.Text = "Phone Number";
            // 
            // phonenum1
            // 
            phonenum1.BackColor = SystemColors.Info;
            phonenum1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            phonenum1.Font = new System.Drawing.Font("Lucida Bright", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            phonenum1.ForeColor = System.Drawing.Color.Gray;
            phonenum1.Location = new Point(780, 400);
            phonenum1.Multiline = true;
            phonenum1.Size = new Size(214, 26);
            phonenum1.Text = "";
            // 
            // phone2label
            // 
            Label phone2label = new Label();
            phone2label.ForeColor = Color.FromArgb(0, 42, 56);
            phone2label.Font = new Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            phone2label.Location = new Point(660, 450);
            phone2label.Size = new Size(100, 26);
            phone2label.Text = "Another Phone";
            // 
            // phonenum2
            // 
            phonenum2.BackColor = SystemColors.Info;
            phonenum2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            phonenum2.Font = new System.Drawing.Font("Lucida Bright", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            phonenum2.ForeColor = System.Drawing.Color.Gray;
            phonenum2.Location = new Point(780, 450);
            phonenum2.Multiline = true;
            phonenum2.Size = new Size(214, 26);
            phonenum2.Text = "";
            // 
            // typelabel
            // 
            Label typelabel = new Label();
            typelabel.ForeColor = Color.FromArgb(0, 42, 56);
            typelabel.Font = new Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            typelabel.Location = new Point(660, 500);
            typelabel.Size = new Size(120, 26);
            typelabel.Text = "Type of insurance";
            // 
            // type
            // 
            type.BackColor = SystemColors.Info;
            type.FlatStyle = FlatStyle.Popup;
            type.ForeColor = Color.Gray;
            type.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            type.FormattingEnabled = true;
            type.Items.Clear();
            type.Items.AddRange(new object[] {
            "Employee",
            "Student"});
            type.Location = new Point(780, 500);
            type.Size = new Size(214, 26);
            // 
            // genderlabel
            // 
            Label genderlabel = new Label();
            genderlabel.ForeColor = Color.FromArgb(0, 42, 56);
            genderlabel.Font = new Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            genderlabel.Location = new Point(660, 550);
            genderlabel.Size = new Size(85, 26);
            genderlabel.Text = "Gender";
            // 
            // gender
            // 
            gender.ForeColor = Color.Gray;
            gender.BackColor = SystemColors.Info;
            gender.FlatStyle = FlatStyle.Popup;
            gender.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            gender.FormattingEnabled = true;
            gender.Items.Clear();
            gender.Items.AddRange(new object[] {
            "Female",
            "Male"});
            gender.Location = new Point(780, 550);
            gender.Size = new Size(214, 26);
            // 
            //save
            savepro.BackColor = Color.Black;
            savepro.FlatStyle = FlatStyle.Popup;
            savepro.ForeColor = Color.White;
            savepro.Location = new Point(1000, 600);
            savepro.Size = new Size(200, 75);
            savepro.Font = new Font("Arial Narrow", 20F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            savepro.Text = "Save";
            savepro.UseVisualStyleBackColor = false;
            //
            //


            this.Controls.Add(image);
            this.Controls.Add(first);
            this.Controls.Add(firstname);
            this.Controls.Add(last);
            this.Controls.Add(lastname);
            this.Controls.Add(emaillabel);
            this.Controls.Add(email);
            this.Controls.Add(passlabel);
            this.Controls.Add(password);
            this.Controls.Add(phone1label);
            this.Controls.Add(phonenum1);
            this.Controls.Add(phone2label);
            this.Controls.Add(phonenum2);
            this.Controls.Add(typelabel);
            this.Controls.Add(type);
            this.Controls.Add(genderlabel);
            this.Controls.Add(gender);
            this.Controls.Add(savepro);
            this.Controls.Add(clear);

        }
        private void top()
        {
            // Title
            // 
            title.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            title.BackColor = SystemColors.Desktop;
            title.FlatStyle = FlatStyle.Popup;
            title.Font = new Font("Microsoft Sans Serif", 27.75F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            title.ForeColor = Color.White;
            title.Location = new Point(600, 15);
            title.MaximumSize = new Size(1082, 50);
            title.Size = new Size(249, 39);
            title.TabIndex = 89;
            title.Text = "Home";
            // 
            // clock
            // 
            clock.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            clock.AutoSize = true;
            clock.Font = new Font("Microsoft Sans Serif", 26F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            clock.ForeColor = Color.White;
            clock.Location = new Point(1100, 16);
            clock.Text = "00:00:00";
            clock.TabIndex = 86;
            // 
            // power
            // 
            power.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            power.BackColor = SystemColors.ButtonHighlight;
            power.Image = CarInsuranceSystem.Properties.Resources.power;
            power.Location = new Point(1300, 20);
            power.Size = new Size(57, 32);
            power.SizeMode = PictureBoxSizeMode.StretchImage;
            power.TabIndex = 85;
            power.TabStop = false;
            power.Click += new EventHandler(power_Click);
            // 

            // topbar
            // 
            topbar.BackColor = System.Drawing.SystemColors.MenuText;
            topbar.ForeColor = Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            topbar.Location = new Point(-15, -14);
            topbar.Size = new Size(1700, 60);
            topbar.TabIndex = 87;
            topbar.Controls.Add(clock);
            topbar.Controls.Add(power);
            topbar.Controls.Add(title);
            this.Controls.Add(topbar);
            // 

        }
        private void clearpage()
        {
            this.Controls.Clear();
            viewcars.Location = new Point(0, 312);
            viewcarslabel.Location = new Point(51, 312);
            members.Location = new Point(7, 358);
            memberslabel.Location = new Point(51, 358);
            addcar.Location = new Point(3, 395);
            addcarlabel.Location = new Point(51, 400);
            notifi.Location = new Point(3, 446);
            notification.Location = new Point(51, 446);
            profile.Location = new Point(5, 577);
            profilelabel.Location = new Point(51, 587);
            question.Location = new Point(5, 617);
            questionlabel.Location = new Point(51, 627);
            settings.Location = new Point(5, 657);
            settingslabel.Location = new Point(51, 667);
            this.Controls.Add(topbar);
            this.Controls.Add(menu);
            this.Controls.Remove(mark);
        }
        public void textOfbox(TextBox tb)
        {
            string x = tb.Text;
            tb.KeyDown += (sernder, KeyDown) => Text_KeyDown(tb, KeyDown, x);
            tb.KeyUp += (sernder, KeyUp) => Text_KeyUp(tb, KeyUp, x);
        }
        private void Text_KeyDown(object sender, EventArgs e, String x)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == x && tb.ForeColor == Color.DarkGray)
            {
                tb.Text = ""; tb.ForeColor = Color.Gray;
                tb.BackColor = Color.FromArgb(255, 254, 234);
            }
        }
        private void Text_KeyUp(object sender, EventArgs e, String x)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
                tb.Text = x; tb.ForeColor = Color.DarkGray;


        }
        public void reset()
        {
            model.Text = "Model"; model.ForeColor = Color.DarkGray;
            color.Text = "Color"; color.ForeColor = Color.DarkGray;
            platenumber.Text = "Plate number"; platenumber.ForeColor = Color.DarkGray;
            manfactureyear.SelectedIndex = -1; username_addcar.SelectedIndex = -1;
            manfactureyear.Text = "Manfacture Year";
            error.Text = "Fill all the information cells !!";
            error.Visible = false;
            username_addcar.Text = "Username....";
            username_addcar.BackColor= SystemColors.Window;
        }
    }
}
