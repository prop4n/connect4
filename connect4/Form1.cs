using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace connect4
{
    public partial class Form1 : Form
    {

        private BoardGame jeu;
        int color = 0;


        public Form1()
        {
            InitializeComponent();
            jeu = new BoardGame(8, 8);
            jeu.ShowGrid(picBox);

            picBox.MouseClick += new MouseEventHandler(pictureBox_MouseClick);
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            int colToPlace = e.Location.X / jeu.CellSize;

            char playerColor = (color % 2 == 0) ? 'y' : 'r';
            bool placed = jeu.PlacePion(colToPlace, playerColor);
            jeu.ShowGrid(picBox);
            if (placed)
            {
                color++;
            } else
            {
                MessageBox.Show("Le pion ne peut être placé ! ");
            }
        }

        class BoardGame
        {
            private char[,] board = new char[100, 100];
            private int row, col;
            private readonly SolidBrush yellowTeam = new SolidBrush(Color.Yellow);
            private readonly SolidBrush redTeam = new SolidBrush(Color.Red);
            private readonly int cellSize = 60;
            private readonly Pen pen = new Pen(Color.Black, 1);

            public int CellSize
            {
                get { return cellSize; }
            }

            public BoardGame(int nbRow, int nbCols)
            {
                this.row = nbRow;
                this.col = nbCols;
            }

            public void ShowGrid(PictureBox pictureBox)
            {
                Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
                Graphics graphics = Graphics.FromImage(bitmap);

                pictureBox.Height = cellSize * row;
                pictureBox.Width = cellSize * col;

                for (int x = 0; x < col; x++)
                {
                    graphics.DrawLine(pen, x * cellSize, 0, x * cellSize, row * cellSize);
                    for (int y = 0; y < row; y++)
                    {
                        graphics.DrawLine(pen, 0, y * cellSize, col * cellSize, y * cellSize);
                        int xPos = (x) * cellSize + cellSize / 2;
                        int yPos = (y) * cellSize + cellSize / 2;
                        int radius = cellSize / 2;

                        graphics.DrawEllipse(pen, xPos - radius, yPos - radius, cellSize, cellSize);

                        if (board[y, x] == 'r')
                        {
                            graphics.FillEllipse(redTeam, xPos - radius, yPos - radius, cellSize, cellSize);
                        }
                        else if (board[y, x] == 'y')
                        {
                            graphics.FillEllipse(yellowTeam, xPos - radius, yPos - radius, cellSize, cellSize);
                        }
                    }
                }

                pictureBox.Image = bitmap;
            }

            public bool PlacePion(int colToPlace, char color)
            {
                bool success = false;
                for (int y = 0; y < row; y++)
                {
                    if (board[y, colToPlace] == 'r' || board[y, colToPlace] == 'y')
                    {
                        if (y == 0)
                        {
                            if (board[0, colToPlace] == 'r' || board[0, colToPlace] == 'y')
                            {
                                success = false;
                                break;
                            }
                                
                            this.board[0, colToPlace] = color;
                            success = true;
                            break;
                        } else
                        {
                            this.board[y - 1, colToPlace] = color;
                            success = true;
                            break;
                        }
                    }
                    else if (y == row - 1)
                    {
                        this.board[row - 1, colToPlace] = color;
                        success = true;
                        break;
                    }
                }

                return success;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}