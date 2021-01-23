using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using System.Collections;

namespace Pong
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console setup
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Title = "Pong";
            
            // New thread allowing to use keyboard features
            Thread t = new Thread(ThreadProc);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            void ThreadProc()
            {
                string[,] position = new string[39, 19];
                
                string buildingBlockHorizontal = "═";
                string buildingBlockVertical = "║";

                for (int j = 0; j < position.GetLength(1); j++)
                {

                    for (int i = 0; i < position.GetLength(0); i++)
                    {

                        position[i, j] = " ";
                    }
                }

                for (int i = 0; i < position.GetLength(0); i++)
                {
                    position[i, 0] = buildingBlockHorizontal;
                }

                for (int i = 0; i < position.GetLength(0); i++)
                {
                    position[i, position.GetLength(1) - 1] = buildingBlockHorizontal;
                }

                for (int i = 0; i < position.GetLength(1); i++)
                {
                    position[0, i] = buildingBlockVertical;
                }

                for (int i = 0; i < position.GetLength(1); i++)
                {
                    position[position.GetLength(0) - 1, i] = buildingBlockVertical;
                }

                position[0, 0] = "╔";
                position[position.GetLength(0) - 1, 0] = "╗";
                position[position.GetLength(0) - 1, position.GetLength(1) - 1] = "╝";
                position[0, position.GetLength(1) - 1] = "╚";

                
                //Draws over 2D field "position"
                void Draw()
                {
                    
                    Console.SetCursorPosition(0, 0);
                    for (int j = 0; j < position.GetLength(1); j++)
                    {
                        for (int i = 0; i < position.GetLength(0); i++)
                        {
                            Console.Write(position[i, j]);
                        }
                        Console.WriteLine();
                    }
                    Clear();
                    
                }

                //Clears the play area excluding borders
                void Clear()
                {
                    for (int j = 0; j < position.GetLength(1) - 2; j++)
                    {

                        for (int i = 0; i < position.GetLength(0) - 2; i++)
                        {

                            position[i + 1, j + 1] = " ";
                        }
                    }

                }

                // Creates player arrays and start position for players
                int TopPlayerStart = 8;
                ArrayList racketListPlayerTwo = new ArrayList();
                ArrayList racketListPlayerOne = new ArrayList();


                
                // Renders player rackets
                void racketRander()
                {
                    foreach (RacketCell item in racketListPlayerTwo)
                    {
                        position[item.positionWidth, item.positionHeight] = "▓";
                    }
                    foreach (RacketCell item in racketListPlayerOne)
                    {
                        position[item.positionWidth, item.positionHeight] = "▓";
                    }
                }

               
                // updates positions of players while checking walls above and beneath
                void racketUpdatePlayer(int direction, ArrayList racketList)
                {
                    bool wallCheckCeiling = false;
                    bool wallCheckFloor = false;
                    foreach (RacketCell item in racketList)
                    {
                        if (item.positionHeight - 1 == 0)
                        {
                            wallCheckCeiling = true;
                        }
                        if (item.positionHeight + 1 == 18)
                        {
                            wallCheckFloor = true;
                        }
                        
                    }
                    
                    
                        switch (direction)
                        {
                            case 1:
                            if (!wallCheckCeiling )
                            {
                                foreach (RacketCell i in racketList)
                                {
                                    i.positionHeight--;
                                }
                                
                            }
                            break;
                            case -1:
                            if (!wallCheckFloor)
                            {
                                foreach (RacketCell i in racketList)
                                {
                                    i.positionHeight++;
                                }
                                
                            }
                            break;
                            default:
                                break;
                        }
                    
                    


                }

                //Creates ball object
                Ball ball = new Ball(18, 9, 2);
                Thread.Sleep(300);


                void GameStart()
                {
                    
                    //Fills game area with stuff
                    for (int i = TopPlayerStart; i < TopPlayerStart + 5; i++)
                    {

                        RacketCell cell = new RacketCell(3, position.GetLength(1) - i);
                        racketListPlayerTwo.Add(cell);
                    }
                    for (int i = TopPlayerStart; i < TopPlayerStart + 5; i++)
                    {

                        RacketCell cell = new RacketCell(position.GetLength(0) - 4, position.GetLength(1) - i);
                        racketListPlayerOne.Add(cell);
                    }
                    Draw();
                }

                //Main game loop. Takes care of Player input, updating positions
                void GameLoop()
                {
                    
                    int[] directions = { -3, -2, -1, 1, 2, 3 };
                    int randomDirection = new Random().Next(directions.Length);
                    int currentDirection = directions[randomDirection];
                    while (true)
                    {
                        
                        try
                        {
                            //player input
                            if (Keyboard.IsKeyDown(Key.Up))
                            {
                                racketUpdatePlayer(1, racketListPlayerOne);
                            }
                            else if (Keyboard.IsKeyDown(Key.Down))
                            {
                                racketUpdatePlayer(-1, racketListPlayerOne);
                            }
                            else
                            {
                                racketUpdatePlayer(0, racketListPlayerOne);
                            }

                            if (Keyboard.IsKeyDown(Key.W))
                            {
                                racketUpdatePlayer(1, racketListPlayerTwo);
                            }
                            else if (Keyboard.IsKeyDown(Key.S))
                            {
                                racketUpdatePlayer(-1, racketListPlayerTwo);
                            }
                            else
                            { 
                                racketUpdatePlayer(0, racketListPlayerTwo);
                            }
                            racketRander();
                            if (!directions.Contains(ball.CollisionCheck(currentDirection, racketListPlayerOne, racketListPlayerTwo)))
                            {
                                currentDirection =-1;
                            }
                            else
                            {
                                currentDirection = ball.CollisionCheck(currentDirection, racketListPlayerOne, racketListPlayerTwo);
                            }
                            
                            
                            //checks current directions and update ball position
                            switch (currentDirection)
                            {
                                case 1:
                                    ball.positionWidth++;
                                    ball.positionHeight--;
                                    break;
                                case 2:
                                    ball.positionWidth++;
                                    break;
                                case 3:
                                    ball.positionWidth++;
                                    ball.positionHeight++;
                                    break;
                                case -1:
                                    ball.positionWidth--;
                                    ball.positionHeight--;
                                    break;
                                case -2:
                                    ball.positionWidth--;
                                    break;
                                case -3:
                                    ball.positionWidth--;
                                    ball.positionHeight++;
                                    break;
                                default:
                                    break;
                            }
                            //renders ball
                            position[ball.positionWidth, ball.positionHeight] = "*";
                            //regulation of ball speed according to the direction
                            if (currentDirection == 2 | currentDirection == -2)
                            {
                                Thread.Sleep(10);
                            }
                            else
                            {
                                Thread.Sleep(35);
                            }
                            
                            
                            Draw();

                          
                        }
                        //game end
                        catch (IndexOutOfRangeException)
                        {
                            if (ball.positionWidth > position.GetLength(0) / 2)
                            {
                                Console.Clear();
                                Console.WriteLine("Player one wins!");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Player two wins!");
                            }
                            
                            break;
                        }
                        
                    }
                }

                GameStart();
                GameLoop();
                

                
            }
        }
    }
}
