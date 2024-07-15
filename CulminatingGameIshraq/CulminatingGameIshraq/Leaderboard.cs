//Name: Ishraq Alam
//Date: June 17, 2024
//Purpose: This form shows the highest scores ever played 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CulminatingGameIshraq
{
    public partial class Leaderboard : Form
    {
        public Leaderboard()
        {
            InitializeComponent();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            MainMenu ShowMainMenu = new MainMenu();
            this.Hide();
            ShowMainMenu.Show();
        }

        //Variable Declaration
        public static int[] intTempScore; //Storing score temporarily
        public static bool blnGameDone = false; //To know whether to add anything to leaderboard, or just view leaderboard
        public static int intArraySize = 0; 
        public static int[] intHighScores; //High scores array for adding new values 
        public static string[] strNamesArray; //Names array for adding new values 
        public static string[] strTempNames; //Storing names temporarily 
        public static int intCurrentHighScore; //High score after a game ends 
        public static string strUsername = ""; //Username, or current name to go with high score
        public static int intTempHighScore = 0; //Used in bubble sorting 
        public static string strTempName = ""; //Used in bubble sorting 
        public static int intDisplayArraySize = 0; //Used when displaying file
        public static string[] strDisplayNameArray; //Used when displaying file 
        public static int[] intDisplayScoreArray; //Used when displaying file 
        public static int intCurrentLeaderboardSize = 0; //Used to find the size of the array 

        //Copy all information from leaderboard into an array, then add the value from most recent game into that array, sort, then rewrite the leaderboard file --> then output the leaderboard onto labels
        private void Leaderboard_Load(object sender, EventArgs e)
        {
            intDisplayArraySize = 0; //Resets counter used for calculating array size in existing leaderboard

            if (blnGameDone == true) //If game finished, and gets carried over from game form 
            {
                StreamReader se = File.OpenText("NameLeaderboard.txt"); //Opens the name leaderboard file from debug
                string strReadingInput = null; //Sets input reader

                while ((strReadingInput = se.ReadLine()) != null) //Calculates the number of items in the file, and creates an array size
                {
                    intCurrentLeaderboardSize++;
                }

                se.Close();

                //Array sizes get set based on existing items in the leaderboard
                strNamesArray = new string[intCurrentLeaderboardSize];
                intHighScores = new int[intCurrentLeaderboardSize];

                

                se = File.OpenText("NameLeaderboard.txt"); //Opens the name leaderboard again 

                for (int i = 0; i < strNamesArray.Length; i++) //Copies info into name array
                {
                    strReadingInput = se.ReadLine();
                    strNamesArray[i] = strReadingInput;
                }

                se.Close();

                StreamReader qe = File.OpenText("ScoreLeaderboard.txt"); //Opens score leaderboard
                for (int i = 0; i < intHighScores.Length; i++)
                {
                    strReadingInput = qe.ReadLine(); //Copies information into score array 
                    intHighScores[i] = Int32.Parse(strReadingInput);
                }
                qe.Close();

               

                intTempScore = new int[intHighScores.Length + 1]; //Has one more size than old array

                for (int i = 0; i < intHighScores.Length; i++) //Copy info into temp array
                {
                    intTempScore[i] = intHighScores[i];
                }

                intTempScore[intTempScore.Length - 1] = intCurrentHighScore; //Add new score

                intHighScores = new int[intTempScore.Length];

                for (int i = 0; i < intHighScores.Length; i++) //Copies information back into original array
                {
                    intHighScores[i] = intTempScore[i];
                }

                strTempNames = new string[strNamesArray.Length + 1]; //Has one more size than old array

                for (int i = 0; i < strNamesArray.Length; i++) //Copies info into temp array 
                {
                    strTempNames[i] = strNamesArray[i];
                }

                strTempNames[strTempNames.Length - 1] = strUsername; //Add new name

                strNamesArray = new string[strTempNames.Length];

                for (int i = 0; i < strNamesArray.Length; i++) //Copies info back into original array 
                {
                    strNamesArray[i] = strTempNames[i];
                }

               


                // Bubble sort using highest - lowest score sorting

                for (int i = 0; i < intHighScores.Length; i++) //Bubble sort both arrays so it is organized in highest to lowest scores - name follows score shifting
                {
                    for (int j = 0; j < intHighScores.Length - 1; j++)
                    {
                        if (intHighScores[j] < intHighScores[j + 1])
                        {
                            intTempHighScore = intHighScores[j];
                            intHighScores[j] = intHighScores[j + 1];
                            intHighScores[j + 1] = intTempHighScore;

                            strTempName = strNamesArray[j];
                            strNamesArray[j] = strNamesArray[j + 1];
                            strNamesArray[j + 1] = strTempName;
                        }
                    }
                }

                using (StreamWriter Tex = new StreamWriter("NameLeaderboard.txt", false)) //Overrwrites existing file with new array values
                {
                    for (int i = 0; i < strNamesArray.Length; i++)
                    {
                        Tex.WriteLine(strNamesArray[i]);
                    }

                }

                using (StreamWriter Dex = new StreamWriter("ScoreLeaderboard.txt", false)) //Overrwrites existing file with new array values 
                {
                    for (int i = 0; i < intHighScores.Length; i++)
                    {
                        Dex.WriteLine(intHighScores[i]);
                    }
                }

                blnGameDone = false; //Changes back to false - will become true if player completes another game 

            }



            //Opening leaderboard and outputting into form

            StreamReader re = File.OpenText("NameLeaderboard.txt"); //Opens name leaderboard 
            string strInput; //Input used to read lines 

            while ((strInput = re.ReadLine()) != null) //Finds the size of the array 
            {
                intDisplayArraySize++; 
            }


            re.Close();

            strDisplayNameArray = new string[intDisplayArraySize]; //Display name array gets created
            intDisplayScoreArray = new int[intDisplayArraySize]; //Display score array gets created

            re = File.OpenText("NameLeaderboard.txt"); //Opens name leaderboard file

            for (int i = 0; i < strDisplayNameArray.Length; i++) //Copies information into display name array from file
            {
                strInput = re.ReadLine();
                strDisplayNameArray[i] = strInput;
            }

            re.Close();

            StreamReader de = File.OpenText("ScoreLeaderboard.txt"); //Opens score leaderboard file

            for (int i = 0; i < intDisplayScoreArray.Length; i++) //Copies information into display score array from file 
            {
                strInput = de.ReadLine();
                intDisplayScoreArray[i] = Int32.Parse(strInput);
            }

            de.Close();

            if (intDisplayArraySize == 1) //1 item in leaderboard
            {
                this.lblTopName1.Text = strDisplayNameArray[0];
                this.lblTopScore1.Text = intDisplayScoreArray[0].ToString();
            }

            else if (intDisplayArraySize == 2) //2 items in leaderboard
            {
                this.lblTopName1.Text = strDisplayNameArray[0];
                this.lblTopScore1.Text = intDisplayScoreArray[0].ToString();

                this.lblTopName2.Text = strDisplayNameArray[1];
                this.lblTopScore2.Text = intDisplayScoreArray[1].ToString();
            }

            else if (intDisplayArraySize == 3) //3 items in leaderboard
            {
                this.lblTopName1.Text = strDisplayNameArray[0];
                this.lblTopScore1.Text = intDisplayScoreArray[0].ToString();

                this.lblTopName2.Text = strDisplayNameArray[1];
                this.lblTopScore2.Text = intDisplayScoreArray[1].ToString();

                this.lblTopName3.Text = strDisplayNameArray[2];
                this.lblTopScore3.Text = intDisplayScoreArray[2].ToString();
            }

            else if (intDisplayArraySize == 4) //4 items in leaderboard
            {
                this.lblTopName1.Text = strDisplayNameArray[0];
                this.lblTopScore1.Text = intDisplayScoreArray[0].ToString();

                this.lblTopName2.Text = strDisplayNameArray[1];
                this.lblTopScore2.Text = intDisplayScoreArray[1].ToString();

                this.lblTopName3.Text = strDisplayNameArray[2];
                this.lblTopScore3.Text = intDisplayScoreArray[2].ToString();

                this.lblTopName4.Text = strDisplayNameArray[3];
                this.lblTopScore4.Text = intDisplayScoreArray[3].ToString();
            }

            else if (intDisplayArraySize >= 5)//5 items in leaderboard 
            {
                this.lblTopName1.Text = strDisplayNameArray[0];
                this.lblTopScore1.Text = intDisplayScoreArray[0].ToString();

                this.lblTopName2.Text = strDisplayNameArray[1];
                this.lblTopScore2.Text = intDisplayScoreArray[1].ToString();

                this.lblTopName3.Text = strDisplayNameArray[2];
                this.lblTopScore3.Text = intDisplayScoreArray[2].ToString();

                this.lblTopName4.Text = strDisplayNameArray[3];
                this.lblTopScore4.Text = intDisplayScoreArray[3].ToString();

                this.lblTopName5.Text = strDisplayNameArray[4];
                this.lblTopScore5.Text = intDisplayScoreArray[4].ToString();
            }
            

        }


    }
}
