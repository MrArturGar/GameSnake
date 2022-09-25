using SnakeGame.Models;
using static System.Windows.Forms.AxHost;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int maxX, maxY;

        Snake Snake;
        Food Food;
        Stats Stats;
        bool Started = false;
        private void StartButton_Click(object sender, EventArgs e)
        {
            LoadGame();
        }

        private void LoadGame()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            updateTimer.Interval = Settings.UpdateIntervalMillisecond;
            Started = true;
            maxX = pictureBox1.Width / Settings.ObjectWidth; maxY = pictureBox1.Height / Settings.ObjectHeight;

            Snake = new Snake(Direction.Down)
            {
                CoordinateList = new List<Coordinate>()
                {
                    new Coordinate(maxX/2, maxY/2),
                    new Coordinate(maxX/2+1, maxY/2)
                }
            };
            GenerateFood();

            Stats = new Stats
            {
                startTime = DateTime.Now,
                startSankeSize = Snake.CoordinateList.Count,
                GameSize = new Coordinate(maxX, maxY),
            };

            updateTimer.Start();
        }

        private void GenerateFood()
        {
            Random rnd = new Random();
            Food = new Food(rnd.Next(maxX), rnd.Next(maxY));
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            Coordinate lastPlace = null;
            for (int i = 0; i < Snake.CoordinateList.Count; i++)
            {
                Coordinate bufferCoordinate = lastPlace;
                if (i == 0)
                {
                    lastPlace = Snake.CoordinateList[i].Clone();
                    switch (Snake.Direction)
                    {
                        case Direction.Up:
                            Snake.CoordinateList[i].Y--;
                            if (Snake.CoordinateList[i].Y < 0)
                                //Snake.CoordinateList[i].Y += maxY;
                                YouLose();
                            break;
                        case Direction.Down:
                            Snake.CoordinateList[i].Y++;
                            if (Snake.CoordinateList[i].Y > maxY)
                                //Snake.CoordinateList[i].Y -= maxY;
                                YouLose();
                            break;
                        case Direction.Right:
                            Snake.CoordinateList[i].X++;
                            if (Snake.CoordinateList[i].X > maxX)
                                //Snake.CoordinateList[i].X -= maxX;
                                YouLose();
                            break;
                        case Direction.Left:
                            Snake.CoordinateList[i].X--;
                            if (Snake.CoordinateList[i].X < 0)
                                //Snake.CoordinateList[i].X += maxX;
                                YouLose();
                            break;
                    }

                    if (Snake.Find(Snake.CoordinateList[i])>1)
                        YouLose();

                }
                else
                {
                    lastPlace = Snake.CoordinateList[i];

                    if (lastPlace != bufferCoordinate)
                        Snake.CoordinateList[i] = bufferCoordinate;
                }

                if (Snake.CoordinateList[i] == Food.Coordinate)
                {
                    EatFood();
                    GenerateFood();
                }


            }
            pictureBox1.Invalidate();
        }

        private void EatFood()
        {
            Snake.CoordinateList.Add(Food.Coordinate);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && Snake.Direction != Direction.Down)
            {
                Snake.Direction = Direction.Up;
            }
            else if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && Snake.Direction != Direction.Up)
            {
                Snake.Direction = Direction.Down;
            }
            else if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && Snake.Direction != Direction.Right)
            {
                Snake.Direction = Direction.Left;
            }
            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && Snake.Direction != Direction.Left)
            {
                Snake.Direction = Direction.Right;
            }
        }

        private void StopButton2_Click(object sender, EventArgs e)
        {
            if (Started)
            {
                updateTimer.Stop();
                Stats.endTime = DateTime.Now;
                Stats.endSankeSize = Snake.CoordinateList.Count;
            }
        }

        private void UpdatePictureBox(object sender, PaintEventArgs e)
        {
            if (!Started)
                return;

            Graphics canvas = e.Graphics;

            Brush snakeColour;

            for (int i = 0; i < Snake.CoordinateList.Count; i++)
            {
                if (i == 0)
                {
                    snakeColour = Settings.SnakeColor;
                }
                else
                {
                    snakeColour = Brushes.DarkGreen;
                }

                canvas.FillEllipse(snakeColour, new Rectangle
                    (
                    Snake.CoordinateList[i].X * Settings.ObjectWidth,
                    Snake.CoordinateList[i].Y * Settings.ObjectHeight,
                    Settings.ObjectHeight, Settings.ObjectHeight
                    ));
            }


            canvas.FillEllipse(Food.Color, new Rectangle
            (
            Food.Coordinate.X * Settings.ObjectWidth,
            Food.Coordinate.Y * Settings.ObjectHeight,
            Settings.ObjectHeight, Settings.ObjectHeight
            ));
        }

        private void statsButton3_Click(object sender, EventArgs e)
        {

        }

        private void YouLose()
        {
            StopButton2_Click(null, null);
            MessageBox.Show("You LOSE!");
        }
    }
}