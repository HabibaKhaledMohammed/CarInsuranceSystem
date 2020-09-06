using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarInsuranceSystem
{
    public partial class chatForm : Form
    {
        string username;
        public chatForm()
        {
            InitializeComponent();
        }
        public chatForm(String user)
        {
            InitializeComponent();
            username = user;
        }
        //timer
        //
        Timer timer = new Timer();
        int mes_num = 0;
        private void chatForm_Load(object sender, EventArgs e)
        {
            //Timer
            timer.Interval = 1000;
            timer.Tick += new EventHandler(this.timer_Tick);
            timer.Start();
            //
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
            con.Open();
            SqlCommand command = new SqlCommand("Select  Image, UserName from Car_Owner where Admin='0' ;", con);
            SqlDataReader reader = command.ExecuteReader();
            int y = 25;
            while (reader.Read())
            {
                string name = reader["UserName"].ToString().Trim();
                if (name != username)
                {
                    friendsList.Items.Add(name);
                    PictureBox x = new PictureBox();
                    x.Size = new Size(30, 20);
                    x.Location = new Point(2, y);
                    x.SizeMode = PictureBoxSizeMode.StretchImage;
                    try
                    {
                        byte[] MyImg = (byte[])reader["Image"];
                        if (MyImg.ToString() != null)
                        {
                            MemoryStream ms = new MemoryStream(MyImg);
                            Image img = Image.FromStream(ms);
                            x.Image = img;
                            imageShow.Controls.Add(x);
                        }

                    }
                    catch (Exception)
                    {
                        x.Image = Properties.Resources._default;
                        imageShow.Controls.Add(x);
                    }
                    y += 20;
                }
            }

            reader.Close();
            con.Close();
            retrive();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (friendsList.SelectedIndex > 0)
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                con.Open();
                SqlCommand command = new SqlCommand("Select User_ID from car_Owner where UserName='" + username + "' ;", con);
                int ownid = (int)command.ExecuteScalar();
                command = new SqlCommand("Select User_ID from car_Owner where UserName='" + friendsList.SelectedItem + "' ;", con);
                int senderid = (int)command.ExecuteScalar();
                command = new SqlCommand("select count(*)  from  Chat where From_User_ID='" + ownid.ToString() + "'and To_User_ID='" + senderid + "' or From_User_ID='" + senderid.ToString() + "' and To_User_ID='" + ownid + "';", con);
                int num = (int)command.ExecuteScalar();
                con.Close();
                if (mes_num != num)
                {
                    mes_num = num;
                    retrive();
                }
            }
        }
        private void friendsList_SelectedIndexChanged(object sender, EventArgs e)
        {
              retrive();
              //MessageBox.Show("hi");
        }
        private void Send_button_Click(object sender, EventArgs e)
        {
            if (Message_text.Text.Trim() != "" && friendsList.SelectedIndex > 0)
            {
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                con.Open();
                SqlCommand command = new SqlCommand("Select User_ID from car_Owner where UserName='" + username + "' ;", con);
                int ownid = (int)command.ExecuteScalar();
                command = new SqlCommand("Select User_ID from car_Owner where UserName='" + friendsList.SelectedItem + "' ;", con);
                int senderid = (int)command.ExecuteScalar();
                command = new SqlCommand("insert into Chat Values(" + ownid.ToString() + "," + senderid + ",'" + Message_text.Text.Trim().Replace("'","''") + "','" +  DateTime.Now.ToString("dd'/'MM'/'yyyy HH:mm:ss") + "');", con);
                command.ExecuteNonQuery();
                con.Close();
            }
            retrive();
            Message_text.Clear();
        }
        void retrive()
        {
            if (friendsList.SelectedIndex>0) {
                string text = "<html><head><style type='text / css'> span{border-radius:25px; }</style></ head><body><table style='width:620px; '>";
                SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CarInsuranceSystem;Integrated Security=True");
                con.Open();
                SqlCommand command = new SqlCommand("Select User_ID from car_Owner where UserName='" + username + "' ;", con);
                int ownid = (int)command.ExecuteScalar();
                command = new SqlCommand("Select User_ID from car_Owner where UserName='" + friendsList.SelectedItem + "' ;", con);
                int senderid = (int)command.ExecuteScalar();
                command = new SqlCommand("select Message ,From_User_ID  from  Chat where From_User_ID='" + ownid.ToString() + "'and To_User_ID='" + senderid + "' or From_User_ID='" + senderid.ToString() + "' and To_User_ID='" + ownid + "';", con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    if (reader["From_User_ID"].ToString().Trim() == ownid.ToString().Trim())
                        text += "<tr><div style='width:300px; float:right; overflow: auto; margin:5px; text-align: right;  color:white;'> <span style=' background:blue; font-size: 22px;'>" + reader["Message"].ToString().Trim() + "</span></div></tr>";         
                    else
                        text += "<tr><div style='width:300px; float:left; margin:5px; color:white; '> <span style='font-size: 22px; background:crimson; '>" + reader["Message"].ToString().Trim() + "</span></div></tr> ";                    
                }
                text += "</table></body></html>";
                messageList.DocumentText =text;
                reader.Close();
                con.Close();          
            }

        }
        private void messageList_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            messageList.Document.Window.ScrollTo(0, messageList.Document.Body.ScrollRectangle.Height);
        }
    }
}
