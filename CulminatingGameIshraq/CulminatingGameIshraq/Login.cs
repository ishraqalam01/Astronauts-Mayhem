using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CulminatingGameIshraq
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public static string strUsername = "Unknown";
        private void btnContinueToMenu_Click(object sender, EventArgs e)
        {
           
                //Tried implementing a system where user must enter a name and can't just proceed without not doing so. 
                
                if (this.txtLoginBox.Text == "" || this.txtLoginBox.Text == " " || this.txtLoginBox.Text == "  " || this.txtLoginBox.Text == "    ")
                {
                    MessageBox.Show("Please enter a valid name!");
                    
                }
                

                else
                {
                    strUsername = this.txtLoginBox.Text;
                    MainMenu.PlayerName = strUsername;
                    Game.strUsername = strUsername;
                    MainMenu ShowMenu = new MainMenu();
                    this.Hide();
                    ShowMenu.Show();
                    

                }
               
            }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
    }

