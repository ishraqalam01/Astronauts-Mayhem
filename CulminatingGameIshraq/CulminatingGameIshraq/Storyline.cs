﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CulminatingGameIshraq
{
    public partial class Storyline : Form
    {
        public Storyline()
        {
            InitializeComponent();
        }

        public static string strInput = null;
        private void Storyline_Load(object sender, EventArgs e)
        {
           
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            MainMenu ShowMainMenu = new MainMenu();
            this.Hide();
            ShowMainMenu.Show();
        }

        private void lblStoryLineDescription_Click(object sender, EventArgs e)
        {

        }
    }
}
