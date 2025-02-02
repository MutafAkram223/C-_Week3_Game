using System;

namespace Game
{
    public class Player
    {
        private const int MaxBullets = 100;
        private static int _score = 0;

        public int X;
        public int Y;

        private int[] BulletX = new int[MaxBullets];
        private int[] BulletY = new int[MaxBullets];
        private bool[] IsBulletActive = new bool[MaxBullets];
        private static int BulletCount = 0;

        public int Score
        {
            get { return _score; }
        }

        public Player(int x, int y)
        {
            X = x;
            Y = y;
            PrintPlayer();
        }

        public void PrintPlayer()
        {
            Console.SetCursorPosition(X, Y);
            Console.WriteLine("   || ");
            Console.SetCursorPosition(X, Y + 1);
            Console.WriteLine("  ||||");
            Console.SetCursorPosition(X, Y + 2);
            Console.WriteLine("   || ");
            Console.SetCursorPosition(X, Y + 3);
            Console.WriteLine("  |||| ");
        }

        public void MoveLeft()
        {
            if (X > 1)
            {
                ErasePlayer();
                X -= 7;
                PrintPlayer();
            }
        }

        public void MoveRight()
        {
            if (X < 60)
            {
                ErasePlayer();
                X += 7;
                PrintPlayer();
            }
        }

        public void ErasePlayer()
        {
            Console.SetCursorPosition(X, Y);
            Console.WriteLine("       ");
            Console.SetCursorPosition(X, Y + 1);
            Console.WriteLine("       ");
            Console.SetCursorPosition(X, Y + 2);
            Console.WriteLine("       ");
            Console.SetCursorPosition(X, Y + 3);
            Console.WriteLine("       ");
        }

        public void GenerateBullet()
        {
            BulletX[BulletCount] = X + 3;
            BulletY[BulletCount] = Y - 1;
            IsBulletActive[BulletCount] = true;
            BulletCount++;
            if (BulletCount >= MaxBullets)
                BulletCount = 0;
        }

        public void MoveBullets()
        {
            for (int i = 0; i < MaxBullets; i++)
            {
                if (IsBulletActive[i])
                {
                    EraseBullet(BulletX[i], BulletY[i]);
                    BulletY[i]--;
                    if (BulletY[i] <= 1)
                    {
                        IsBulletActive[i] = false;
                    }
                    else
                    {
                        PrintBullet(BulletX[i], BulletY[i]);
                    }
                }
            }
        }

        public void PrintBullet(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("*");
        }

        public void EraseBullet(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }

        public void CheckBulletCollisionWithEnemies(Enemy[] enemies)
        {
            for (int i = 0; i < MaxBullets; i++)
            {
                if (IsBulletActive[i])
                {
                    foreach (var enemy in enemies)
                    {
                        if (BulletX[i] >= enemy.X && BulletX[i] <= enemy.X + 7 && BulletY[i] >= enemy.Y && BulletY[i] <= enemy.Y + 4)
                        {
                            IsBulletActive[i] = false;
                            EraseBullet(BulletX[i], BulletY[i]);
                            enemy.Respawn();
                            AddScore();
                        }
                    }
                }
            }
        }

        public void CheckCollisionWithEnemies(Enemy[] enemies)
        {
            foreach (var enemy in enemies)
            {
                if (X >= enemy.X && X <= enemy.X + 7 && Y + 4 >= enemy.Y && Y <= enemy.Y + 4)
                {
                    enemy.Respawn();
                    GameOver();
                }
            }
        }

        public void PrintScore()
        {
            Console.SetCursorPosition(60, 1);
            Console.WriteLine("Score: " + _score);
        }

        private void AddScore()
        {
            _score += 10;
        }

        public void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game Over! Final Score: " + _score);
            Environment.Exit(0);
        }
    }
}
