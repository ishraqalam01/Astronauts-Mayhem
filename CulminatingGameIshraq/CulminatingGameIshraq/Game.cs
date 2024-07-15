//Name: Ishraq Alam
//Date: June 17, 2024
//Title: AstronautMayhem
//Purpose: Culminating game incorporating all aspects of the course.
using CulminatingGameIshraq.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace CulminatingGameIshraq
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }
        

        //Variable Declaration 

        public static int intCurrentScoreTracker = 0; //Tracks the score in-game
        public static int intXValue = 0; //The x-coordinate spawn point of falling objects
        public static int intBottomBoundary = 0; //The bottom boundary to respawn when interacting
        public static int intHealthBarCounter = 0; //To gradually decrease the health bar 
        public static int intUFOHealthBarCounter = 0; //To gradually decrease the UFO health bar
        public static int intNumberOfHits = 0; //For collecting data
        public static int intObjectsThatFellCounter = 0; //To increase difficulty
        public static int intDirection = 1; //Direction = 1 --> left, Direction = 2 ---> right 
        public static int intNumberOfGames = 0; //To calculate how many games played in one sitting
        public static string strUsername = ""; //To display username 



        Random RandomClass = new Random(); //Random generation

        public static string[] strCollectedItems; //Seeing collected items 

        public static string strCurrentItem = ""; //Current item viewing

        public static string strOutputArray = ""; //Seeing all collected items 

        //Final velocity used in math calculation
        public static double dblObject1FinalVelocity = 1;
        public static double dblObject2FinalVelocity = 2;
        public static double dblObject3FinalVelocity = 1;
        public static double dblObject4FinalVelocity = 2;


        //Time used in math calculation
        public static double dblObject1Time = 30;
        public static double dblObject2Time = 40;
        public static double dblObject3Time = 35;
        public static double dblObject4Time = 43;


        //Acceleration used in math calculation
        public static double dblObject1Acceleration = 0;
        public static double dblObject2Acceleration = 0;
        public static double dblObject3Acceleration = 0;
        public static double dblObject4Acceleration = 0;

        //Current velocity used for falling object motion 
        public static double dblObject1CurrentVelocity = 0;
        public static double dblObject2CurrentVelocity = 0;
        public static double dblObject3CurrentVelocity = 0;
        public static double dblObject4CurrentVelocity = 0;

        //SIZE IS 2 - Only 2 picture boxes spawn when the form loads, but when the difficulty increases, then resize the array and add 1 more of each type. - Picture box arrays for falling objects 
        public static PictureBox[] satellite = new PictureBox[3];
        public static PictureBox[] rocket = new PictureBox[3];
        public static PictureBox[] asteroid = new PictureBox[3];
        public static PictureBox[] star = new PictureBox[3];

        /* 
         * MATH PORTION 
         * Use acceleration = (vf - vi / delta t). 
         * Each object, has a fixed time, but the vf is random. The time remains constant for each individual item, but they are different between the objects. The acceleration represents the 
         * interval of increase between each velocity at specific time intervals, or locations on the window. This means for ex. if the formula above makes acceleration = 2, then at 0s, V = 5, then at 1s, V = 5 + 2 = 7, at 2s, V = 7 + 2 = 9, etc. 
         */

        private void Game_KeyDown(object sender, KeyEventArgs e) //Astronaut movement left and right 
        {
            if (e.KeyData == Keys.D) //If the user clicks "D", the paddle will move to the right by 10
            {
                this.pcbAstronaut.Left += 3;

            }

            else if (e.KeyData == Keys.A) //If the user clicks "A", the paddle will move to the left by 10
            {
                this.pcbAstronaut.Left -= 3;
            }
        }

        public double AccelerationCalculation(double dblFinalVelocity, double dblInitialVelocity, double dblDeltaTime) //Function for math calc
        {
            return (dblFinalVelocity - dblInitialVelocity) / dblDeltaTime; // a = (vf - vi) / t
        }

        public double VerticalValSpeed(double dblAcceleration, double dblCurrentVelocity) //Function for math calc 
        {
            return (dblAcceleration + dblCurrentVelocity); // a + v for constantly changing velocity based on acceleration 
        }



        //When the game first loads 
        private void Game_Load(object sender, EventArgs e)
        {
            strCollectedItems = new string[1]; //Reset Collected items array
            this.lblUsername.Text = strUsername; //Display username
            tmrGame.Enabled = true; //Starts game movement
            intBottomBoundary = this.pcbAstronaut.Top + this.pcbAstronaut.Height; //Bottom limits
            intCurrentScoreTracker = 0; //Score reset

            /*
             * SPAWNING FALLING OBJECTS 
             * - used fixed / random location mix, and set properties for all 4 different types of objects
             */
            for (int i = 0; i < 2; i++)
            {
                
                satellite[i] = new PictureBox();
                satellite[i].Location = new Point(150 + 10 * (i * 25), 0);
                satellite[i].Size = new Size(45, 45);
                satellite[i].Name = "FallingSatellite" + i;
                satellite[i].Tag = i;
                satellite[i].Image = Resource1.Satellite;
                satellite[i].BackColor = Color.Green;
                Controls.Add(satellite[i]);
                satellite[i].BringToFront();
            }

            for (int j = 0; j < 2; j++)
            {
                rocket[j] = new PictureBox();
                rocket[j].Location = new Point(450 + 10 * (j * 25), 0);
                rocket[j].Size = new Size(45, 45);
                rocket[j].Name = "FallingRocket" + j;
                rocket[j].Tag = j;
                rocket[j].Image = Properties.Resources.Rocket;
                rocket[j].BackColor = Color.Cyan;
                Controls.Add(rocket[j]);
                rocket[j].BringToFront();
            }

            for (int k = 0; k < 2; k++)
            {
                asteroid[k] = new PictureBox();
                asteroid[k].Location = new Point(230 + k * 430, 0);
                asteroid[k].Size = new Size(45, 45);
                asteroid[k].Name = "FallingAsteroid" + k;
                asteroid[k].Tag = k;
                asteroid[k].Image = Properties.Resources.Asteroid;
                asteroid[k].BackColor = Color.Red;
                Controls.Add(asteroid[k]);
                asteroid[k].BringToFront();
            }

            for (int l = 0; l < 2; l++)
            {
                star[l] = new PictureBox();
                star[l].Location = new Point(RandomClass.Next(170, this.Width), 0);
                star[l].Size = new Size(45, 45);
                star[l].Name = "FallingStar" + l;
                star[l].Tag = l;
                star[l].Image = Properties.Resources.Star;
                star[l].BackColor = Color.Yellow;
                Controls.Add(star[l]);
                star[l].BringToFront();
            }

            /*
             * SPAWNING the inivisible falling objects that can only be seen when the difficulty increases.
             */

            satellite[2] = new PictureBox();
            satellite[2].Location = new Point(150 + 10 * (2 * 25), 0);
            satellite[2].Size = new Size(45, 45);
            satellite[2].Name = "FallingSatellite" + 2;
            satellite[2].Tag = 2;
            satellite[2].BackColor = Color.Transparent;
            Controls.Add(satellite[2]);

            rocket[2] = new PictureBox();
            rocket[2].Location = new Point(450 + 10 * (2 * 25), 0);
            rocket[2].Size = new Size(45, 45);
            rocket[2].Name = "FallingRocket" + 2;
            rocket[2].Tag = 2;
            rocket[2].BackColor = Color.Transparent;
            Controls.Add(rocket[2]);

            asteroid[2] = new PictureBox();
            asteroid[2].Location = new Point(230 + 2 * 430, 0);
            asteroid[2].Size = new Size(45, 45);
            asteroid[2].Name = "FallingAsteroid" + 2;
            asteroid[2].Tag = 2;
            asteroid[2].BackColor = Color.Transparent;
            Controls.Add(asteroid[2]);

            star[2] = new PictureBox();
            star[2].Location = new Point(RandomClass.Next(170, this.Width), 0);
            star[2].Size = new Size(45, 45);
            star[2].Name = "FallingStar" + 2;
            star[2].Tag = 2;
            star[2].BackColor = Color.Transparent;
            Controls.Add(star[2]);

        }

        private void pcbAstronaut_Click(object sender, EventArgs e)
        {

        }

        //TO FIX THE FREEZING - get rid of the loops for the satellite[i] stuff inside intersection, replace with satellite[0], satellite[1], satellite[2], etc. Get rid of background image of form

        //Timer
        private void tmrGame_Tick(object sender, EventArgs e)
        {
            /*
             * FALLING OBJECT MOVEMENT CALCULATIONS
             */
            dblObject1Acceleration = AccelerationCalculation(dblObject1FinalVelocity, 0, dblObject1Time);
            dblObject1CurrentVelocity = VerticalValSpeed(dblObject1Acceleration, dblObject1CurrentVelocity);

            dblObject2Acceleration = AccelerationCalculation(dblObject2FinalVelocity, 0, dblObject2Time);
            dblObject2CurrentVelocity = VerticalValSpeed(dblObject2Acceleration, dblObject2CurrentVelocity);

            dblObject3Acceleration = AccelerationCalculation(dblObject3FinalVelocity, 0, dblObject3Time);
            dblObject3CurrentVelocity = VerticalValSpeed(dblObject3Acceleration, dblObject3CurrentVelocity);

            dblObject4Acceleration = AccelerationCalculation(dblObject4FinalVelocity, 0, dblObject4Time);
            dblObject4CurrentVelocity = VerticalValSpeed(dblObject4Acceleration, dblObject4CurrentVelocity);


            /*
             * OBJECT MOVEMENT - increases as timer goes on by moving at current velocity based on math calculation 
             */

            this.pcbFallingObject1.Top += (int)dblObject1CurrentVelocity;
            this.pcbFallingObject2.Top += (int)dblObject2CurrentVelocity;
            this.pcbFallingObject3.Top += (int)dblObject3CurrentVelocity;
            this.pcbFallingObject4.Top += (int)dblObject4CurrentVelocity;

            //Object movement for rest of falling objects in array 
            for (int i = 0; i < satellite.Length; i++)
            {
                satellite[i].Top += (int)dblObject1CurrentVelocity;
            }

            for (int j = 0; j < rocket.Length; j++)
            {
                rocket[j].Top += (int)dblObject2CurrentVelocity;
            }

            for (int k = 0; k < asteroid.Length; k++)
            {
                asteroid[k].Top += (int)dblObject3CurrentVelocity;
            }

            for (int l = 0; l < star.Length; l++)
            {
                star[l].Top += (int)dblObject4CurrentVelocity;
            }

            //Intersects with astronaut that user controls, or the bottom boundary 

            //If the SATELLITE intersects with ASTRONAUT
            if (this.pcbAstronaut.Bounds.IntersectsWith(this.pcbFallingObject1.Bounds) || this.pcbAstronaut.Bounds.IntersectsWith(satellite[0].Bounds) || this.pcbAstronaut.Bounds.IntersectsWith(satellite[1].Bounds) || this.pcbAstronaut.Bounds.IntersectsWith(satellite[2].Bounds))
            {
                int intRandomLocation = RandomClass.Next(179, this.Width);
                intXValue = intRandomLocation;


                //INCORPORATE ARRAY VALUES --> instead of just falling object1

                //If Astronaut catches satellite, will reset and add 10 points
                if (this.pcbAstronaut.Bounds.IntersectsWith(this.pcbFallingObject1.Bounds))
                {
                    intCurrentScoreTracker = intCurrentScoreTracker + 10;
                    this.pcbFallingObject1.Top = 0;
                    this.pcbFallingObject1.Left = intXValue;
                    SatelliteHit();

                    //Depletes UFO health if intersected
                    intUFOHealthBarCounter++;

                    //A piece of the UFO health bar disappears, to give the illusion the health decreased 
                    if (intUFOHealthBarCounter == 1)
                    {
                        this.pcbUFOHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 2)
                    {
                        this.pcbUFOHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 3)
                    {
                        this.pcbUFOHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 4)
                    {
                        this.pcbUFOHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 5)
                    {
                        this.pcbUFOHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 6)
                    {
                        this.pcbUFOHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 7)
                    {
                        this.pcbUFOHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 8)
                    {
                        this.pcbUFOHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 9)
                    {
                        this.pcbUFOHealthBar9.BackColor = Color.Transparent;
                    }

                }

                /*
                 * Astronaut intersects with array value satellites, same as above as well as rest in this if statement
                 */
                else if (this.pcbAstronaut.Bounds.IntersectsWith(satellite[0].Bounds))
                {
                    intCurrentScoreTracker = intCurrentScoreTracker + 10;

                    satellite[0].Top = 0;
                    satellite[0].Left = intXValue;
                    SatelliteHit();

                    intUFOHealthBarCounter++;

                    if (intUFOHealthBarCounter == 1)
                    {
                        this.pcbUFOHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 2)
                    {
                        this.pcbUFOHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 3)
                    {
                        this.pcbUFOHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 4)
                    {
                        this.pcbUFOHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 5)
                    {
                        this.pcbUFOHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 6)
                    {
                        this.pcbUFOHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 7)
                    {
                        this.pcbUFOHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 8)
                    {
                        this.pcbUFOHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 9)
                    {
                        this.pcbUFOHealthBar9.BackColor = Color.Transparent;
                    }
                }

                else if (this.pcbAstronaut.Bounds.IntersectsWith(satellite[1].Bounds))
                {
                    intCurrentScoreTracker = intCurrentScoreTracker + 10;

                    satellite[1].Top = 0;
                    satellite[1].Left = intXValue;
                    SatelliteHit();

                    intUFOHealthBarCounter++;

                    if (intUFOHealthBarCounter == 1)
                    {
                        this.pcbUFOHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 2)
                    {
                        this.pcbUFOHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 3)
                    {
                        this.pcbUFOHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 4)
                    {
                        this.pcbUFOHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 5)
                    {
                        this.pcbUFOHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 6)
                    {
                        this.pcbUFOHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 7)
                    {
                        this.pcbUFOHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 8)
                    {
                        this.pcbUFOHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 9)
                    {
                        this.pcbUFOHealthBar9.BackColor = Color.Transparent;
                    }
                }

                else if (this.pcbAstronaut.Bounds.IntersectsWith(satellite[2].Bounds) && intObjectsThatFellCounter > 48) //If satellite is hit, but the difficulty increased as well
                {
                    intCurrentScoreTracker = intCurrentScoreTracker + 10;
                    satellite[2].Top = 0;
                    satellite[2].Left = intXValue;
                    SatelliteHit();

                    intUFOHealthBarCounter++;

                    if (intUFOHealthBarCounter == 1)
                    {
                        this.pcbUFOHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 2)
                    {
                        this.pcbUFOHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 3)
                    {
                        this.pcbUFOHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 4)
                    {
                        this.pcbUFOHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 5)
                    {
                        this.pcbUFOHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 6)
                    {
                        this.pcbUFOHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 7)
                    {
                        this.pcbUFOHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 8)
                    {
                        this.pcbUFOHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 9)
                    {
                        this.pcbUFOHealthBar9.BackColor = Color.Transparent;
                    }
                }

                else if (this.pcbAstronaut.Bounds.IntersectsWith(satellite[2].Bounds) && intObjectsThatFellCounter < 48) //If astronaut intersects with difficulty objects before difficulty increased
                {
                    satellite[2].Top = 0;
                    satellite[2].Left = intXValue; //resets location
                }


                intNumberOfHits++; //Hits counter increases to help with collections 

                dblObject1FinalVelocity = RandomClass.Next(1, 3); //Velocity becomes 1 or 2
                dblObject1CurrentVelocity = 0; //Current velocity resets when it goes back to top

                if (intObjectsThatFellCounter > 48) //If difficulty is raised, velocity is raised 
                {
                    dblObject1FinalVelocity = RandomClass.Next(3, 6);
                }

                intObjectsThatFellCounter++; 

            }

            //If the ROCKET intersects with the ASTRONAUT
            else if (this.pcbAstronaut.Bounds.IntersectsWith(this.pcbFallingObject2.Bounds) || this.pcbAstronaut.Bounds.IntersectsWith(rocket[0].Bounds) || this.pcbAstronaut.Bounds.IntersectsWith(rocket[1].Bounds) || this.pcbAstronaut.Bounds.IntersectsWith(rocket[2].Bounds))
            {
                int intRandomLocation = RandomClass.Next(179, this.Width); //Random spawn location for x-coordinate
                intXValue = intRandomLocation;


                //If astronaut catches rocket, 20 points are earned 
                if (this.pcbAstronaut.Bounds.IntersectsWith(this.pcbFallingObject2.Bounds))
                {
                    intCurrentScoreTracker = intCurrentScoreTracker + 20;
                    this.pcbFallingObject2.Top = 0;
                    this.pcbFallingObject2.Left = intXValue; //Random x location
                    RocketHit();

                    //UFO health bar goes down 
                    intUFOHealthBarCounter++;

                    //The UFO health bar has illusion by depleting rectangles that make it
                    if (intUFOHealthBarCounter == 1)
                    {
                        this.pcbUFOHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 2)
                    {
                        this.pcbUFOHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 3)
                    {
                        this.pcbUFOHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 4)
                    {
                        this.pcbUFOHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 5)
                    {
                        this.pcbUFOHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 6)
                    {
                        this.pcbUFOHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 7)
                    {
                        this.pcbUFOHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 8)
                    {
                        this.pcbUFOHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 9)
                    {
                        this.pcbUFOHealthBar9.BackColor = Color.Transparent;
                    }

                }


                //For array values of the rocket - same properties as above 
                else if (this.pcbAstronaut.Bounds.IntersectsWith(rocket[0].Bounds))
                {
                    intCurrentScoreTracker = intCurrentScoreTracker + 20;
                    rocket[0].Top = 0;
                    rocket[0].Left = intXValue;
                    RocketHit();

                    intUFOHealthBarCounter++;

                    if (intUFOHealthBarCounter == 1)
                    {
                        this.pcbUFOHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 2)
                    {
                        this.pcbUFOHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 3)
                    {
                        this.pcbUFOHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 4)
                    {
                        this.pcbUFOHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 5)
                    {
                        this.pcbUFOHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 6)
                    {
                        this.pcbUFOHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 7)
                    {
                        this.pcbUFOHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 8)
                    {
                        this.pcbUFOHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 9)
                    {
                        this.pcbUFOHealthBar9.BackColor = Color.Transparent;
                    }
                }

                else if (this.pcbAstronaut.Bounds.IntersectsWith(rocket[1].Bounds))
                {
                    intCurrentScoreTracker = intCurrentScoreTracker + 20;
                    rocket[1].Top = 0;
                    rocket[1].Left = intXValue;
                    RocketHit();

                    intUFOHealthBarCounter++;

                    if (intUFOHealthBarCounter == 1)
                    {
                        this.pcbUFOHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 2)
                    {
                        this.pcbUFOHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 3)
                    {
                        this.pcbUFOHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 4)
                    {
                        this.pcbUFOHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 5)
                    {
                        this.pcbUFOHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 6)
                    {
                        this.pcbUFOHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 7)
                    {
                        this.pcbUFOHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 8)
                    {
                        this.pcbUFOHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 9)
                    {
                        this.pcbUFOHealthBar9.BackColor = Color.Transparent;
                    }
                }

                //If the difficulty increased and touches the difficult falling object 
                else if (this.pcbAstronaut.Bounds.IntersectsWith(rocket[2].Bounds) && intObjectsThatFellCounter > 48)
                {
                    intCurrentScoreTracker = intCurrentScoreTracker + 20;
                    rocket[2].Top = 0;
                    rocket[2].Left = intXValue;
                    RocketHit();

                    intUFOHealthBarCounter++;

                    if (intUFOHealthBarCounter == 1)
                    {
                        this.pcbUFOHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 2)
                    {
                        this.pcbUFOHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 3)
                    {
                        this.pcbUFOHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 4)
                    {
                        this.pcbUFOHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 5)
                    {
                        this.pcbUFOHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 6)
                    {
                        this.pcbUFOHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 7)
                    {
                        this.pcbUFOHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 8)
                    {
                        this.pcbUFOHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intUFOHealthBarCounter == 9)
                    {
                        this.pcbUFOHealthBar9.BackColor = Color.Transparent;
                    }
                }

                //If the difficulty didn't increase, the rocket remains invisible and just resets and can't be interacted with. 
                else if (this.pcbAstronaut.Bounds.IntersectsWith(rocket[2].Bounds) && intObjectsThatFellCounter < 48)
                {
                    rocket[2].Top = 0;
                    rocket[2].Left = intXValue;
                }
                
                //Final velocity is randomly 1 or 2
                dblObject2FinalVelocity = RandomClass.Next(1, 3);
                dblObject2CurrentVelocity = 0; //Resets velocity at the top  

                if (intObjectsThatFellCounter > 48) //If difficulty increased, velocity between 3 and 5
                {
                    dblObject2FinalVelocity = RandomClass.Next(3, 6);
                }

                intObjectsThatFellCounter++;
            }

            //If the ASTEROID intersects with the astronaut
            else if (this.pcbAstronaut.Bounds.IntersectsWith(this.pcbFallingObject3.Bounds) || this.pcbAstronaut.Bounds.IntersectsWith(asteroid[0].Bounds) || this.pcbAstronaut.Bounds.IntersectsWith(asteroid[1].Bounds) || this.pcbAstronaut.Bounds.IntersectsWith(asteroid[2].Bounds))
            {
                int intRandomLocation = RandomClass.Next(179, this.Width); //Randomly spawns at an x-coordinate 
                intXValue = intRandomLocation;


                //If the asteroid intersects with astronaut, 15 points are LOST / DEPLETED
                if (this.pcbAstronaut.Bounds.IntersectsWith(this.pcbFallingObject3.Bounds))
                {
                    intCurrentScoreTracker = intCurrentScoreTracker - 15; // -15 points
                    this.pcbFallingObject3.Top = 0;
                    this.pcbFallingObject3.Left = intXValue; //Random respawn
                    AsteroidHit();

                    //Player's health bar goes down
                    intHealthBarCounter++;

                    //Illusion it goes down by making a piece disappear
                    if (intHealthBarCounter == 1)
                    {
                        this.pcbUserHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 2)
                    {
                        this.pcbUserHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 3)
                    {
                        this.pcbUserHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 4)
                    {
                        this.pcbUserHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 5)
                    {
                        this.pcbUserHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 6)
                    {
                        this.pcbUserHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 7)
                    {
                        this.pcbUserHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 8)
                    {
                        this.pcbUserHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 9)
                    {
                        this.pcbUserHealthBar9.BackColor = Color.Transparent;
                    }
                }


                //Same as above, but for the array values of asteroid
                else if (this.pcbAstronaut.Bounds.IntersectsWith(asteroid[0].Bounds))
                {
                    intCurrentScoreTracker = intCurrentScoreTracker - 15; //Depletes points
                    asteroid[0].Top = 0;
                    asteroid[0].Left = intXValue; //Random respawn
                    AsteroidHit(); //Collection information

                    intHealthBarCounter++;

                    //Depleting player health bar 
                    if (intHealthBarCounter == 1)
                    {
                        this.pcbUserHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 2)
                    {
                        this.pcbUserHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 3)
                    {
                        this.pcbUserHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 4)
                    {
                        this.pcbUserHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 5)
                    {
                        this.pcbUserHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 6)
                    {
                        this.pcbUserHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 7)
                    {
                        this.pcbUserHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 8)
                    {
                        this.pcbUserHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 9)
                    {
                        this.pcbUserHealthBar9.BackColor = Color.Transparent;
                    }
                }

                
                else if (this.pcbAstronaut.Bounds.IntersectsWith(asteroid[1].Bounds))
                {
                    intCurrentScoreTracker = intCurrentScoreTracker - 15;
                    asteroid[1].Top = 0;
                    asteroid[1].Left = intXValue;
                    AsteroidHit();

                    intHealthBarCounter++;

                    if (intHealthBarCounter == 1)
                    {
                        this.pcbUserHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 2)
                    {
                        this.pcbUserHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 3)
                    {
                        this.pcbUserHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 4)
                    {
                        this.pcbUserHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 5)
                    {
                        this.pcbUserHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 6)
                    {
                        this.pcbUserHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 7)
                    {
                        this.pcbUserHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 8)
                    {
                        this.pcbUserHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 9)
                    {
                        this.pcbUserHealthBar9.BackColor = Color.Transparent;
                    }
                }

                //If difficult pieces interacted with after difficulty raised 
                else if (this.pcbAstronaut.Bounds.IntersectsWith(asteroid[2].Bounds) && intObjectsThatFellCounter > 48)
                {
                    intCurrentScoreTracker = intCurrentScoreTracker - 15; // 15 points lost 
                    asteroid[2].Top = 0;
                    asteroid[2].Left = intXValue; //random respawn
                    AsteroidHit();

                    intHealthBarCounter++;

                    if (intHealthBarCounter == 1)
                    {
                        this.pcbUserHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 2)
                    {
                        this.pcbUserHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 3)
                    {
                        this.pcbUserHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 4)
                    {
                        this.pcbUserHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 5)
                    {
                        this.pcbUserHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 6)
                    {
                        this.pcbUserHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 7)
                    {
                        this.pcbUserHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 8)
                    {
                        this.pcbUserHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 9)
                    {
                        this.pcbUserHealthBar9.BackColor = Color.Transparent;
                    }
                }

                else if (this.pcbAstronaut.Bounds.IntersectsWith(asteroid[2].Bounds) && intObjectsThatFellCounter < 48) //if difficulty wasn't raised yet, then respawns without interacting
                {
                    asteroid[2].Top = 0;
                    asteroid[2].Left = intXValue;
                }

                dblObject3FinalVelocity = RandomClass.Next(1, 3); //Random velocity of 1 or 2
                dblObject3CurrentVelocity = 0;

                if (intObjectsThatFellCounter > 48) //If difficulty raised, velocity of 3 to 5
                {
                    dblObject3FinalVelocity = RandomClass.Next(3, 6);
                }



                intObjectsThatFellCounter++;
            }

            //If the STARS interact with ASTRONAUT - MAKE THIS A DEPLETING OBJECT THAT WILL TAKE AWAY USER'S HEALTH
            else if (this.pcbAstronaut.Bounds.IntersectsWith(this.pcbFallingObject4.Bounds) || this.pcbAstronaut.Bounds.IntersectsWith(star[0].Bounds) || this.pcbAstronaut.Bounds.IntersectsWith(star[1].Bounds) || this.pcbAstronaut.Bounds.IntersectsWith(star[2].Bounds))
            {
                //Randomly respawns in a random x-coordinate
                int intRandomLocation = RandomClass.Next(179, this.Width);
                intXValue = intRandomLocation;

                //If star intersects with astronaut, then 10 points depleted 
                if (this.pcbAstronaut.Bounds.IntersectsWith(this.pcbFallingObject4.Bounds))
                {
                    intCurrentScoreTracker = intCurrentScoreTracker - 10; //-10 points
                    this.pcbFallingObject4.Top = 0;
                    this.pcbFallingObject4.Left = intXValue; //Random respawn
                    StarHit(); //Collection data

                    intHealthBarCounter++;

                    //player's health bar decreases if hit
                    if (intHealthBarCounter == 1)
                    {
                        this.pcbUserHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 2)
                    {
                        this.pcbUserHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 3)
                    {
                        this.pcbUserHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 4)
                    {
                        this.pcbUserHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 5)
                    {
                        this.pcbUserHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 6)
                    {
                        this.pcbUserHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 7)
                    {
                        this.pcbUserHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 8)
                    {
                        this.pcbUserHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 9)
                    {
                        this.pcbUserHealthBar9.BackColor = Color.Transparent;
                    }
                }

                //Same as above, but for the array values of the star object
                else if (this.pcbAstronaut.Bounds.IntersectsWith(star[0].Bounds)) 
                {
                    intCurrentScoreTracker = intCurrentScoreTracker - 10; //-10 points
                    star[0].Top = 0;
                    star[0].Left = intXValue; //Randomly respawns at an x-coordinate
                    StarHit();

                    intHealthBarCounter++;

                    //The user's health bar goes down if encountered
                    if (intHealthBarCounter == 1)
                    {
                        this.pcbUserHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 2)
                    {
                        this.pcbUserHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 3)
                    {
                        this.pcbUserHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 4)
                    {
                        this.pcbUserHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 5)
                    {
                        this.pcbUserHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 6)
                    {
                        this.pcbUserHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 7)
                    {
                        this.pcbUserHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 8)
                    {
                        this.pcbUserHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 9)
                    {
                        this.pcbUserHealthBar9.BackColor = Color.Transparent;
                    }
                }

                
                else if (this.pcbAstronaut.Bounds.IntersectsWith(star[1].Bounds))
                {
                    intCurrentScoreTracker = intCurrentScoreTracker - 10;
                    star[1].Top = 0;
                    star[1].Left = intXValue;
                    StarHit();

                    intHealthBarCounter++;

                    if (intHealthBarCounter == 1)
                    {
                        this.pcbUserHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 2)
                    {
                        this.pcbUserHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 3)
                    {
                        this.pcbUserHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 4)
                    {
                        this.pcbUserHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 5)
                    {
                        this.pcbUserHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 6)
                    {
                        this.pcbUserHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 7)
                    {
                        this.pcbUserHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 8)
                    {
                        this.pcbUserHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 9)
                    {
                        this.pcbUserHealthBar9.BackColor = Color.Transparent;
                    }
                }

                //If difficulty is increased and user interacts with difficult objects
                else if (this.pcbAstronaut.Bounds.IntersectsWith(star[2].Bounds) && intObjectsThatFellCounter > 48)
                {
                    intCurrentScoreTracker = intCurrentScoreTracker - 10;
                    star[2].Top = 0;
                    star[2].Left = intXValue;
                    StarHit();

                    intHealthBarCounter++;

                    if (intHealthBarCounter == 1)
                    {
                        this.pcbUserHealthBar1.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 2)
                    {
                        this.pcbUserHealthBar2.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 3)
                    {
                        this.pcbUserHealthBar3.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 4)
                    {
                        this.pcbUserHealthBar4.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 5)
                    {
                        this.pcbUserHealthBar5.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 6)
                    {
                        this.pcbUserHealthBar6.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 7)
                    {
                        this.pcbUserHealthBar7.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 8)
                    {
                        this.pcbUserHealthBar8.BackColor = Color.Transparent;
                    }

                    else if (intHealthBarCounter == 9)
                    {
                        this.pcbUserHealthBar9.BackColor = Color.Transparent;
                    }
                }

                //If difficulty didn't increase yet, and user encounters one of the objects, nothing will happen - just reset location
                else if (this.pcbAstronaut.Bounds.IntersectsWith(star[2].Bounds) && intObjectsThatFellCounter < 48)
                {
                    star[2].Top = 0;
                    star[2].Left = intXValue;
                }

               
                
                //Random final velocity between 1 and 2
                dblObject4FinalVelocity = RandomClass.Next(1, 3);
                dblObject4CurrentVelocity = 0;

                if (intObjectsThatFellCounter > 48) //If difficulty raised, final velocity between 3 and 5 
                {
                    dblObject4FinalVelocity = RandomClass.Next(3, 6);
                }

                intObjectsThatFellCounter++;
            }

            //if the SATELLITES interact with the VOID
            else if ((this.pcbFallingObject1.Top + this.pcbFallingObject1.Height) > intBottomBoundary || (satellite[0].Top + satellite[0].Height) > intBottomBoundary || (satellite[1].Top + satellite[1].Height) > intBottomBoundary || (satellite[2].Top + satellite[2].Height) > intBottomBoundary)
            {
                int intRandomLocation = RandomClass.Next(179, this.Width);
                intXValue = intRandomLocation; //Randomly assigns x-coordinate for respawning 

                if ((this.pcbFallingObject1.Top + this.pcbFallingObject1.Height) > intBottomBoundary) //Resets location 
                {
                    this.pcbFallingObject1.Top = 0;
                    this.pcbFallingObject1.Left = intXValue;

                }

                else if ((satellite[0].Top + satellite[0].Height) > intBottomBoundary) //Resets location
                {
                    satellite[0].Top = 0;
                    satellite[0].Left = intXValue;
                }

                else if ((satellite[1].Top + satellite[1].Height) > intBottomBoundary) //Resets location
                {
                    satellite[1].Top = 0;
                    satellite[1].Left = intXValue;
                }

                else if ((satellite[2].Top + satellite[2].Height) > intBottomBoundary) //Resets location
                {
                    satellite[2].Top = 0;
                    satellite[1].Left = intXValue;
                }

                dblObject1FinalVelocity = RandomClass.Next(1, 3); //Final velocity 1 or 2
                dblObject1CurrentVelocity = 0;

                if (intObjectsThatFellCounter > 48)
                {
                    dblObject1FinalVelocity = RandomClass.Next(3, 6); //If increased difficulty, final velocity 3-5
                }

                intObjectsThatFellCounter++;
            }

            //If the ROCKETS interact with the VOID
            else if ((this.pcbFallingObject2.Top + this.pcbFallingObject2.Height) > intBottomBoundary || (rocket[0].Top + rocket[0].Height) > intBottomBoundary || (rocket[1].Top + rocket[1].Height) > intBottomBoundary || (rocket[2].Top + rocket[2].Height) > intBottomBoundary)
            {
                int intRandomLocation = RandomClass.Next(179, this.Width);
                intXValue = intRandomLocation; //Random x-coordinate location 

                if ((this.pcbFallingObject2.Top + this.pcbFallingObject2.Height) > intBottomBoundary) //Resets location
                {
                    this.pcbFallingObject2.Top = 0;
                    this.pcbFallingObject2.Left = intXValue;
                }

                else if ((rocket[0].Top + rocket[0].Height) > intBottomBoundary) //Resets location
                {
                    rocket[0].Top = 0;
                    rocket[0].Left = intXValue;
                }

                else if ((rocket[1].Top + rocket[1].Height) > intBottomBoundary) //Resets location
                {
                    rocket[1].Top = 0;
                    rocket[1].Left = intXValue;
                }

                else if ((rocket[2].Top + rocket[2].Height) > intBottomBoundary) //Resets location
                {
                    rocket[2].Top = 0;
                    rocket[2].Left = intXValue;
                }

                dblObject2FinalVelocity = RandomClass.Next(1, 3); //Randomly makes velocity 1 or 2
                dblObject2CurrentVelocity = 0;

                if (intObjectsThatFellCounter > 48)  //Randomly makes velocity 3-5 if difficulty raised 
                {
                    dblObject2FinalVelocity = RandomClass.Next(3, 6);
                }

                intObjectsThatFellCounter++;
            }

            //If the ASTEROIDS interact with the VOID
            else if ((this.pcbFallingObject3.Top + this.pcbFallingObject3.Height) > intBottomBoundary || (asteroid[0].Top + asteroid[0].Height) > intBottomBoundary || (asteroid[1].Top + asteroid[1].Height) > intBottomBoundary || (asteroid[2].Top + asteroid[2].Height) > intBottomBoundary)
            {
                int intRandomLocation = RandomClass.Next(179, this.Width);
                intXValue = intRandomLocation; //Random x-coordinate 

                if ((this.pcbFallingObject3.Top + this.pcbFallingObject3.Height) > intBottomBoundary) //Random respawn
                {
                    this.pcbFallingObject3.Top = 0;
                    this.pcbFallingObject3.Left = intXValue;
                }

                else if ((asteroid[0].Top + asteroid[0].Height) > intBottomBoundary) //Random respawn
                {
                    asteroid[0].Top = 0;
                    asteroid[0].Left = intXValue;
                }

                else if ((asteroid[1].Top + asteroid[1].Height) > intBottomBoundary) //Random respawn 
                {
                    asteroid[1].Top = 0;
                    asteroid[1].Left = intXValue;
                }

                else if ((asteroid[2].Top + asteroid[2].Height) > intBottomBoundary) //Random respawn 
                {
                    asteroid[2].Top = 0;
                    asteroid[2].Left = intXValue;
                }

                dblObject3FinalVelocity = RandomClass.Next(1, 3); //Velocity becomes 1 or 2
                dblObject3CurrentVelocity = 0;

                if (intObjectsThatFellCounter > 48)
                {
                    dblObject3FinalVelocity = RandomClass.Next(3, 6); //Velocity becomes 2-5 with increased difficulty
                }

                intObjectsThatFellCounter++;
            }

            //If the STARS interact with the VOID
            else if ((this.pcbFallingObject4.Top + this.pcbFallingObject4.Height) > intBottomBoundary || (star[0].Top + star[0].Height) > intBottomBoundary || (star[1].Top + star[1].Height) > intBottomBoundary || (star[2].Top + star[2].Height) > intBottomBoundary)
            {
                int intRandomLocation = RandomClass.Next(179, this.Width);
                intXValue = intRandomLocation; //Random location x-coordinate

                if ((this.pcbFallingObject4.Top + this.pcbFallingObject4.Height) > intBottomBoundary) //Random respawn
                {
                    this.pcbFallingObject4.Top = 0;
                    this.pcbFallingObject4.Left = intXValue;
                }

                else if ((star[0].Top + star[0].Height) > intBottomBoundary) //Random respawn 
                {
                    star[0].Top = 0;
                    star[0].Left = intXValue;
                }

                else if ((star[1].Top + star[1].Height) > intBottomBoundary) //Random respawn
                {
                    star[1].Top = 0;
                    star[1].Left = intXValue;
                }

                else if ((star[2].Top + star[2].Height) > intBottomBoundary) //Random respawn 
                {
                    star[2].Top = 0;
                    star[2].Left = intXValue; 
                }

                dblObject4FinalVelocity = RandomClass.Next(1, 3); //Velocity becomes 1 or 2
                dblObject4CurrentVelocity = 0;

                if (intObjectsThatFellCounter > 48) //Velocity becomes 3-5 if difficulty raised
                {
                    dblObject4FinalVelocity = RandomClass.Next(3, 6);
                }
                intObjectsThatFellCounter++;
            }

            this.lblInGameScoreValue.Text = intCurrentScoreTracker.ToString(); //Changes the score value to the most current score 

            if (DifficultyIncrease(intObjectsThatFellCounter) == 1) //Sends number of times objects fell into the function to check if it is 48. If it is 48, the difficulty increases
            {
                AddOneOfEachObject(); //Makes invisible objects visible now. 
            }



            //UFO MOVEMENT - LEFT AND RIGHT 
            if (this.pcbUFO.Left < this.pcbBackground.Left) //If UFO hits left boundary
            {
                intDirection = 2; //It will go in direction 2 (right)
            }

            else if (this.pcbUFO.Left + this.pcbUFO.Width > this.Width) //If UFO hits the right boundary
            {
                intDirection = 1; //It will go in direction 1 (left)
            }

            if (intDirection == 1) //Direction 1 indicates 3 units left per timer tick
            {
                this.pcbUFO.Left -= 3;
            }

            else if (intDirection == 2) //Direction 2 indicates 3 units right  per timer tick 
            {
                this.pcbUFO.Left += 3;
            }


            if (intHealthBarCounter == 9) //Game ends (UFO wins)
            {
                this.tmrGame.Enabled = false; //Timer disabled to stop movement
                this.KeyPreview = false; //Can't control astronaut anymore
                Reset(); //Resets and sends information to leaderboard

                MessageBox.Show("You've lost! You may try again."); //Final message




            }

            else if (intUFOHealthBarCounter == 9) //Game ends (Player wins)
            {
                this.tmrGame.Enabled = false; //Timer disabled to stop movement
                this.KeyPreview = false; //Can't control astronaut anymore
                Reset(); //Resets and sends information to leaderboard

                MessageBox.Show("Congrats, you've won! You successfully beat the UFO!"); //Congratulatory message
                
            }
            
        }

        //Function for tracking ALL items collected
        public static string[] CollectedItemsTracker(string strCurrentItem) 
        {
            string[] strTemp = new string[strCollectedItems.Length + 1]; //Adds 1 to existing array to add new item 

            for (int i = 0; i < strCollectedItems.Length; i++) //Stores items collected in temporary array 
            {
                strTemp[i] = strCollectedItems[i];
            }

            strTemp[strTemp.Length - 1] = strCurrentItem; //Adds new item

            strCollectedItems = new string[strTemp.Length]; //Remakes old array with 1 more

            for (int i = 0; i < strCollectedItems.Length; i++) //Copies temporary + added item into old array
            { 
                strCollectedItems[i] = strTemp[i];
            }

            return strCollectedItems; //Returns renvisioned array 
        }

        private void btnViewItems_Click(object sender, EventArgs e) //User can view all the items they've collected so far 
        {
            for (int i = 0; i < strCollectedItems.Length -1; i++) //Displays all items in message box 
            {
                strOutputArray = strCollectedItems[i] + "\n" + strOutputArray; //Concatenates string together
            }

            MessageBox.Show(strOutputArray); //Outputs
        }

        private void btnReturn_Click(object sender, EventArgs e) //Pressing the return button will hide the game form, and then take the player to the main menu form
        { 
            MainMenu ShowMainMenu = new MainMenu();
            this.Hide();
            ShowMainMenu.Show();
        }



        public static int DifficultyIncrease(int intObjectsThatFellCounter) //Difficulty increase function for raising difficulty level. If objects fall 48 times, the difficulty will rise.
        {
            if (intObjectsThatFellCounter == 48)
            {
                return 1;
            }

            return 0;
        }


        public static void AddOneOfEachObject() //Changes the visibility (color + bring to front) properties of the hidden objects when difficulty is increased. 
        {
            satellite[2].BackColor = Color.Green;
            satellite[2].BringToFront();

            rocket[2].BackColor = Color.Cyan;
            rocket[2].BringToFront();


            asteroid[2].BackColor = Color.Red;
            asteroid[2].BringToFront();


            star[2].BackColor = Color.Yellow;
            star[2].Image = Resources.Star;
            star[2].BringToFront();

        }


        public static void SatelliteHit() //Used for collecting info if satellite was hit
        {
            strCurrentItem = "Satellite";

            if (intNumberOfHits == 0)
            {
                strCollectedItems[0] = "Satellite";
            }

            else
            {
                CollectedItemsTracker(strCurrentItem);
            }
            intNumberOfHits++;
        }

        public static void RocketHit() //Used for collecting info if rocket was hit 
        {
            strCurrentItem = "Rocket"; //sets current item collected as rocket and then stores in array 
            if (intNumberOfHits == 0)
            {
                strCollectedItems[0] = "Rocket";
            }

            else
            {
                CollectedItemsTracker(strCurrentItem);
            }

            intNumberOfHits++;
        }

        public static void AsteroidHit() //Used for collecting info if asteroid was hit 
        {
            strCurrentItem = "Asteroid"; //sets current item as asteroid then puts it into array 
            if (intNumberOfHits == 0)
            {
                strCollectedItems[0] = "Asteroid";
            }

            else
            {
                CollectedItemsTracker(strCurrentItem);
            }

            intNumberOfHits++;
        }

        public static void StarHit() //Used for collecting info if star was hit 
        {
            strCurrentItem = "Star"; //sets current item as star then puts into array 
            if (intNumberOfHits == 0)
            {
                strCollectedItems[0] = "Star";
            }

            else
            {
                CollectedItemsTracker(strCurrentItem);
            }

            intNumberOfHits++; 
        }

       public static void Reset() //Resets the game
        {
            
            intNumberOfGames++; //Number of games counter to see how many games played by user
            Leaderboard.intCurrentHighScore = intCurrentScoreTracker; //sends latest score to leaderboard as high score
            Leaderboard.blnGameDone = true; //Sets as true to trigger rewrite feature in leaderboard
            Leaderboard.intArraySize = intNumberOfGames; 
            Leaderboard.strUsername = strUsername; //Username carried to leaderboard

        }

    }
}



