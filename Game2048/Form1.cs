using System;
using System.Drawing;
using System.Windows.Forms;

/*
 * Did my best on this C# Task... I did however miss out on a few things due to time
 * The first being that I was not able to implement the highest score as it would stop the game from running and couldn't quite resolve it in time
 * The second being that I ran out of time to implement the reset button
 * 
 * I do apologise... it took me quite some time but was able to produce a minimum viable product in the end
 */ 

namespace Game2048
{
    public partial class Form1 : Form
    {
        public int[,] map = new int[4, 4];
        public Label[,] labels = new Label[4, 4];
        public PictureBox[,] tiles = new PictureBox[4, 4];
        private int score = 0;

        //initialises the game
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(_keyboardEvent);
            map[0, 0] = 1;
            map[0, 1] = 1;
            createMap();
            createTiles();
            generateNewTile();
        }

        //maps the grey board/tiles of the game
        private void createMap()
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Location = new Point(44 + 56 * j, 130 + 56 * i);
                    pic.Size = new Size(50, 50);
                    pic.BackColor = Color.Gray;
                    this.Controls.Add(pic);
                }
            }
        }

        //generates a new random tile in the game with the properties of generating a new tile
        private void generateNewTile()
        {
            Random rnd = new Random();
            int a = rnd.Next(0, 4);
            int b = rnd.Next(0, 4);

            while (tiles[a, b] != null)
            {
                a = rnd.Next(0, 4);
                b = rnd.Next(0, 4);
            }
            map[a, b] = 1;
            tiles[a, b] = new PictureBox();
            labels[a, b] = new Label();
            labels[a, b].Text = "2";
            labels[a, b].Size = new Size(50, 50);
            labels[a, b].TextAlign = ContentAlignment.MiddleCenter;
            labels[a, b].Font = new Font(new FontFamily("Microsoft Sans Serif"), 10);
            tiles[a, b].Controls.Add(labels[a, b]);
            tiles[a, b].Location = new Point(44 + b * 56, 130 + 56 * a);
            tiles[a, b].Size = new Size(50, 50);
            tiles[a, b].BackColor = Color.Yellow;
            this.Controls.Add(tiles[a, b]);
            tiles[a, b].BringToFront();
        }

        //creates the tiles, highligting the main properties
        private void createTiles()
        {
            tiles[0, 0] = new PictureBox();
            labels[0, 0] = new Label();
            labels[0, 0].Text = "2";
            labels[0, 0].Size = new Size(50, 50);
            labels[0, 0].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 0].Font = new Font(new FontFamily("Microsoft Sans Serif"), 10);
            tiles[0, 0].Controls.Add(labels[0, 0]);
            tiles[0, 0].Location = new Point(44, 130);
            tiles[0, 0].Size = new Size(50, 50);
            tiles[0, 0].BackColor = Color.Yellow;
            this.Controls.Add(tiles[0, 0]);
            tiles[0, 0].BringToFront();

            tiles[0, 1] = new PictureBox();
            labels[0, 1] = new Label();
            labels[0, 1].Text = "2";
            labels[0, 1].Size = new Size(50, 50);
            labels[0, 1].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 1].Font = new Font(new FontFamily("Microsoft Sans Serif"), 10);
            tiles[0, 1].Controls.Add(labels[0, 1]);
            tiles[0, 1].Location = new Point(100, 130);
            tiles[0, 1].Size = new Size(50, 50);
            tiles[0, 1].BackColor = Color.Yellow;
            this.Controls.Add(tiles[0, 1]);
            tiles[0, 1].BringToFront();
        }

        //different color for each milestone score reached
        private void changeColor(int sum, int i, int m)
        {
            if (sum % 2048 == 0) tiles[i, m].BackColor = Color.Gold;
            else if (sum % 1024 == 0) tiles[i, m].BackColor = Color.DarkOrange;
            else if (sum % 512 == 0) tiles[i, m].BackColor = Color.Red;
            else if (sum % 256 == 0) tiles[i, m].BackColor = Color.DarkViolet;
            else if (sum % 128 == 0) tiles[i, m].BackColor = Color.Blue;
            else if (sum % 64 == 0) tiles[i, m].BackColor = Color.Brown;
            else if (sum % 32 == 0) tiles[i, m].BackColor = Color.Coral;
            else if (sum % 16 == 0) tiles[i, m].BackColor = Color.Cyan;
            else if (sum % 8 == 0) tiles[i, m].BackColor = Color.Maroon;
            else tiles[i, m].BackColor = Color.Green;
        }

        //dictates the movement in the game
        private void _keyboardEvent(object sender, KeyEventArgs e)
        {
            bool tileMoved = false;

            switch (e.KeyCode.ToString())
            {
                //moving right
                case "Right":
                    for(int k = 0; k < 4; k++)
                    {
                        for(int l = 2; l >= 0; l--)
                        {
                            if(map[k, l] == 1)
                            {
                                for (int i = l+1; i < 4; i++)
                                {
                                    if (map[k, i] == 0)
                                    {
                                        tileMoved = true;
                                        map[k, i - 1] = 0;
                                        map[k, i] = 1;                
                                        tiles[k, i] = tiles[k, i - 1];
                                        tiles[k, i - 1] = null;
                                        labels[k, i] = labels[k, i - 1];
                                        labels[k, i - 1] = null;
                                        tiles[k, i].Location = new Point(tiles[k, i].Location.X + 56, tiles[k, i].Location.Y);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[k, i].Text);
                                        int b = int.Parse(labels[k, i-1].Text);

                                        if(a == b)
                                        {
                                            tileMoved = true;
                                            labels[k, i].Text = (a + b).ToString();
                                            score += (a + b);
                                            changeColor(a + b, k, i);
                                            label1.Text = "Score: " + score;
                                            map[k, i - 1] = 0;
                                            this.Controls.Remove(tiles[k, i - 1]);
                                            this.Controls.Remove(labels[k, i - 1]);
                                            tiles[k, i - 1] = null;
                                            labels[k, i - 1] = null;
                                        }
                                    }
                                    
                                }
                            }          
                        }
                    }
                    break;

                //moving left   
                case "Left":
                    for (int k = 0; k < 4; k++)
                    {
                        for (int l = 1; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int i = l-1; i >= 0; i--)
                                {
                                    if (map[k, i] == 0)
                                    {
                                        map[k, i + 1] = 0;
                                        map[k, i] = 1;
                                        tiles[k, i] = tiles[k, i + 1];
                                        tiles[k, i + 1] = null;
                                        labels[k, i] = labels[k, i + 1];
                                        labels[k, i + 1] = null;
                                        tiles[k, i].Location = new Point(tiles[k, i].Location.X - 56, tiles[k, i].Location.Y);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[k, i].Text);
                                        int b = int.Parse(labels[k, i + 1].Text);

                                        if(a == b)
                                        {
                                            tileMoved = true;
                                            labels[k, i].Text = (a + b).ToString();
                                            score += (a + b);
                                            changeColor(a + b, k, i);
                                            label1.Text = "Score: " + score;
                                            map[k, i + 1] = 0;
                                            this.Controls.Remove(tiles[k, i + 1]);
                                            this.Controls.Remove(labels[k, i + 1]);
                                            tiles[k, i + 1] = null;
                                            labels[k, i + 1] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                
                //moving down
                case "Down":
                    for (int k = 2; k >= 0; k--)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int i = k + 1; i < 4; i++)
                                {
                                    if (map[i, l] == 0)
                                    {
                                        tileMoved = true;
                                        map[i-1, l] = 0;
                                        map[i, l] = 1;
                                        tiles[i, l] = tiles[i-1, l];
                                        tiles[i-1, l] = null;
                                        labels[i, l] = labels[i - 1, l];
                                        labels[i - 1, l] = null;
                                        tiles[i, l].Location = new Point(tiles[i, l].Location.X, tiles[i, l].Location.Y+56);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[i, l].Text);
                                        int b = int.Parse(labels[i - 1, l].Text);

                                        if (a == b)
                                        {
                                            tileMoved = true;
                                            labels[i, l].Text = (a + b).ToString();
                                            score += (a + b);
                                            changeColor(a + b, i, l);
                                            label1.Text = "Score: " + score;
                                            map[i - 1, l] = 0;
                                            this.Controls.Remove(tiles[i - 1, l]);
                                            this.Controls.Remove(labels[i - 1, l]);
                                            tiles[i - 1, l] = null;
                                            labels[i - 1, l] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                
                //moving up    
                case "Up":
                    for (int k = 1; k < 4; k++)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int i = k - 1; i >= 0; i--)
                                {
                                    if (map[i, l] == 0)
                                    {
                                        tileMoved = true;
                                        map[i + 1, l] = 0;
                                        map[i, l] = 1;
                                        tiles[i, l] = tiles[i + 1, l];
                                        tiles[i + 1, l] = null;
                                        labels[i, l] = labels[i + 1, l];
                                        labels[i + 1, l] = null;
                                        tiles[i, l].Location = new Point(tiles[i, l].Location.X, tiles[i, l].Location.Y - 56);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[i, l].Text);
                                        int b = int.Parse(labels[i + 1, l].Text);

                                        if (a == b)
                                        {
                                            tileMoved = true;
                                            labels[i, l].Text = (a + b).ToString();
                                            score += (a + b);
                                            changeColor(a + b, i, l);
                                            label1.Text = "Score: " + score;
                                            map[i + 1, l] = 0;
                                            this.Controls.Remove(tiles[i + 1, l]);
                                            this.Controls.Remove(labels[i + 1, l]);
                                            tiles[i + 1, l] = null;
                                            labels[i + 1, l] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            //when the player moves a tile, a new random tile is generated
            if (tileMoved)
            {
                generateNewTile();
            }
        }       

        //ignore... couldn't quite remove it
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //ignore... couldn't quite remove it
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}