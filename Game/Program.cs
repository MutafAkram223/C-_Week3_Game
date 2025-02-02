using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

using System;
using System.Threading;

namespace Game
{
    class Program
    {
        static void Main()
        {
            Random rand = new Random();
            Console.Clear();
            PrintMaze();

            Player player = new Player(20, 21);
            Enemy[] enemies = new Enemy[3];
            for (int i = 0; i < 3; i++)
            {
                enemies[i] = new Enemy();
                enemies[i].Spawn(rand);  
            }

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.LeftArrow)
                    {
                        player.MoveLeft();
                    }
                    if (key == ConsoleKey.RightArrow)
                    {
                        player.MoveRight();
                    }
                    if (key == ConsoleKey.Spacebar)
                    {
                        player.GenerateBullet();
                    }
                }

                player.MoveBullets();
                player.CheckBulletCollisionWithEnemies(enemies);
                player.CheckCollisionWithEnemies(enemies);
                player.PrintScore();
                foreach (var enemy in enemies)
                {
                    enemy.Move();
                    enemy.CheckBulletCollision(player);
                }

                if (player.Score >= 300) 
                {
                    Console.Clear();
                    Console.WriteLine("Congratulations! You score is 300. You won the Game");
                    break;
                }

                Thread.Sleep(100);
            }
        }

        static void PrintMaze()
        {
            string[] maze = {
                "######################################################",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "#                                                    #",
                "######################################################"
            };
            foreach (var line in maze)
            {
                Console.WriteLine(line);
            }
        }
    }
}

