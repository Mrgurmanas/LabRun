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

        bool goUp, goDown, goLeft, goRight, isGameOver;

        int score, playerSpeed, pinkGhostSpeed;

        public Form1()
        {
            InitializeComponent();

            ResetGame();
            //textBox1.Text = "asd";

            //connection = new HubConnectionBuilder()
            //    .WithUrl("http://localhost:44398/gamehub")//53353/44398/
            //    .Build();

            //connection.Closed += async (error) =>
            //{
            //    await Task.Delay(new Random().Next(0, 5) * 1000);
            //    await connection.StartAsync();
            //    //connection.StartAsync().Wait();
            //    await connection.InvokeCoreAsync("SendMessage", args: new[] { "User1", "Ready" });
            //    connection.On("ReceiveMessage", (string userName, string message) =>
            //    {
            //        Console.WriteLine(userName + " " + message);
            //        //textBox1.Text = userName + " " + message;
            //    });
            //};
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
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
            txtScore.Text = "Score: " + score;

            if (goLeft)
            {
                pacman.Left -= playerSpeed;
                pacman.Image = Properties.Resources.left;
            }
            if(goRight == true)
            {
                pacman.Left += playerSpeed;
                pacman.Image = Properties.Resources.right;
            }
            if(goDown == true)
            {
                pacman.Top += playerSpeed;
                pacman.Image = Properties.Resources.down;

            }
            if (goUp == true)
            {
                pacman.Top -= playerSpeed;
                pacman.Image = Properties.Resources.Up;
            }

            if(pacman.Left < -10)
            {
                pacman.Left = 750;
            }
            if (pacman.Left > 750)
            {
                pacman.Left = -10;
            }

            if (pacman.Top < -10)
            {
                pacman.Top = 450;
            }
            if (pacman.Top > 450)
            {
                pacman.Top = 0;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "coin" && x.Visible == true)
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1;
                            x.Visible = false;
                        }
                    }

                    if ((string)x.Tag == "wall")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            GameOver("You Lose!");
                        }


                        if (pinkGhost1.Bounds.IntersectsWith(x.Bounds))
                        {
                            pinkGhost1.Left = pinkGhost1.Left * (-1);
                        }
                    }


                    if ((string)x.Tag == "ghost")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            GameOver("You Lose!");
                        }

                    }
                }
            }

        }
        private void ResetGame()
        {
            txtScore.Text = "Score: 0";
            score = 0;
            pinkGhostSpeed = 5;
            isGameOver = false;

            playerSpeed = 8;
            pacman.Left = 43;
            pacman.Top = 49;

            pinkGhost1.Left = 287;
            pinkGhost2.Left = 414;

            pinkGhost1.Top = 162;
            pinkGhost2.Top = 162;

            gameTimer.Start();

            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    x.Visible = true;
                }
            }

        }
        private void GameOver(string message)
        {
            isGameOver = true;

            gameTimer.Stop();

            txtScore.Text = "Score: " + score + Environment.NewLine + message;

        }
    }
}
