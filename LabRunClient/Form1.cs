using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabRunClient
{
    public partial class Form1 : Form
    {
        HubConnection connection;

        //game logic
        bool goUp, goDown, goLeft, goRight, isGameOver;
        int score, score2, playerSpeed;

        public Form1()
        {
            //Setup of a form 
            InitializeComponent();

            ResetGame();

            //connection to SignalR server hub
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:44398/gamehub")
                .Build();

                //getting answer from server
                connection.On<string, string>("ReceiveMessage", (string userName, string message) =>
                {
                    //TODO: remove after testing
                    txtCoins.Text = userName + " " + message;
                });

            //trying connect to server 
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
                //connection.StartAsync().Wait();

                //sending to server
                await connection.InvokeCoreAsync("SendMessage", args: new[] { "User1", "Ready" });
            };
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                ResetGame();
            }


        }

        private void MainGameTimer(object sender, EventArgs e)
        {
            txtScorePlayer1.Text = "Score: " + score;
            txtScorePlayer2.Text = "Score: " + score2;

            if (goLeft)
            {
                player1.Left -= playerSpeed;
                player1.Image = Properties.Resources.left;
            }
            if (goRight == true)
            {
                player1.Left += playerSpeed;
                player1.Image = Properties.Resources.right;
            }
            if (goDown == true)
            {
                player1.Top += playerSpeed;
                player1.Image = Properties.Resources.down;

            }
            if (goUp == true)
            {
                player1.Top -= playerSpeed;
                player1.Image = Properties.Resources.Up;
            }

            if (player1.Left < -10)
            {
                player1.Left = 750;
            }
            if (player1.Left > 750)
            {
                player1.Left = -10;
            }

            if (player1.Top < -10)
            {
                player1.Top = 450;
            }
            if (player1.Top > 450)
            {
                player1.Top = 0;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "coin" && x.Visible == true)
                    {
                        if (player1.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1;
                            x.Visible = false;
                        }
                        if (player2.Bounds.IntersectsWith(x.Bounds))
                        {
                            score2 += 1;
                            x.Visible = false;
                        }
                    }

                    if ((string)x.Tag == "wall")
                    {
                        if (player1.Bounds.IntersectsWith(x.Bounds))
                        {
                            GameOver("You Lose!");
                        }


                        /*if (pinkGhost1.Bounds.IntersectsWith(x.Bounds))
                        {
                            pinkGhost1.Left = pinkGhost1.Left * (-1);
                        }*/
                    }


                    if ((string)x.Tag == "ghost")
                    {
                        if (player1.Bounds.IntersectsWith(x.Bounds))
                        {
                            GameOver("You Lose!");
                        }

                    }
                }
            }

        }
        private void ResetGame()
        {
            txtScorePlayer1.Text = "Player 1 score: 0";
            txtScorePlayer2.Text = "Player 2 score: 0";
            txtCoins.Text = "20 coins left";

            score = 0;
            score2 = 0;

            //pinkGhostSpeed = 5;
            isGameOver = false;

            playerSpeed = 8;
            player1.Left = 50;
            player1.Top = 400;
            
            player2.Left = 650;
            player2.Top = 400;
        
            
            /*pinkGhost1.Left = 287;
            pinkGhost2.Left = 414;

            pinkGhost1.Top = 162;
            pinkGhost2.Top = 162;*/

            gameTimer.Start();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = true;
                    if(x.Tag != null && x.Tag.ToString() == "ghost")
                    {
                        x.Visible = false;
                    }
                }
            }

        }
        private void GameOver(string message)
        {
            isGameOver = true;

            gameTimer.Stop();

            txtScorePlayer1.Text = "Score: " + score + Environment.NewLine + message;
            txtScorePlayer2.Text = "Score: " + score + Environment.NewLine + message;

        }
    }
}
