using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CulminatingGameIshraq
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        public static string PlayerName;
        private void Menu_Load(object sender, EventArgs e)
        {
            this.lblUsername.Text = PlayerName;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Game ShowGame = new Game();
            this.Hide();
            ShowGame.Show();
        }

        private void btnLeaderboard_Click(object sender, EventArgs e)
        {
            Leaderboard ShowLeaderboard = new Leaderboard();
            this.Hide();
            ShowLeaderboard.Show();
        }

        private void btnStoryLine_Click(object sender, EventArgs e)
        {
            Storyline ShowStoryline = new Storyline();
            this.Hide();
            ShowStoryline.Show();
        }

        private void btnHowToPlay_Click(object sender, EventArgs e)
        {
            HowToPlay ShowHowToPlay = new HowToPlay();
            this.Hide();
            ShowHowToPlay.Show();
        }

        private void btnCredentials_Click(object sender, EventArgs e)
        {
            Credentials ShowCredentials = new Credentials();
            this.Hide();
            ShowCredentials.Show();
        }
    }
}
