using System;

namespace Game
{
    public class Enemy
    {
        public int X;
        public int Y;

        public static int BulletCount = 0;
        public const int MaxBullets = 100;
        public int[] BulletX = new int[MaxBullets];
        public int[] BulletY = new int[MaxBullets];
        public bool[] IsBulletActive = new bool[MaxBullets];

        public Enemy()
        {
            Y = 2;
        }

        public void Spawn(Random rand)
        {
            X = rand.Next(0, 60 / 7) * 7 + 1;
            PrintEnemy();
        }

        public void PrintEnemy()
        {
            Console.SetCursorPosition(X, Y);
            Console.WriteLine(" ****  ");
            Console.SetCursorPosition(X, Y + 1);
            Console.WriteLine("  **   ");
            Console.SetCursorPosition(X, Y + 2);
            Console.WriteLine(" ****  ");
            Console.SetCursorPosition(X, Y + 3);
            Console.WriteLine("  **   ");
        }

        public void EraseEnemy()
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

        public void Move()
        {
            EraseEnemy();
            Y++;
            if (Y > 21)
            {
                Respawn();
            }
            PrintEnemy();
        }

        public void Respawn()
        {
            Y = 2;
            Random rand = new Random();
            X = rand.Next(0, 60 / 7) * 7 + 1;
        }

        public void CheckBulletCollision(Player player)
        {
            for (int i = 0; i < MaxBullets; i++)
            {
                if (IsBulletActive[i] && BulletX[i] >= player.X && BulletX[i] <= player.X + 7 && BulletY[i] >= player.Y && BulletY[i] <= player.Y + 4)
                {
                    IsBulletActive[i] = false;
                    EraseEnemy();
                    Respawn();
                }
            }
        }
    }
}

