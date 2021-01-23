using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Pong
{

    class Ball
    {
        public int positionWidth;
        public int positionHeight;
        public int direction;
        

        public Ball(int width, int height, int baseDirection)
        {
            this.positionWidth = width;
            this.positionHeight = height;
            this.direction = baseDirection;
           
        }

        int randomDirection;

        public int CollisionCheck(int currentDirection, ArrayList listPlayerOne, ArrayList listPlayerTwo)
        {

            randomDirection = new Random().Next(1, 4);

                //checks current direction and define direction of the bounce    
                switch (currentDirection)
                {
                    case 1:
                    case 2:
                    case 3:
                        foreach (RacketCell item in listPlayerOne)
                        {
                            if (positionWidth + 1 == item.positionWidth & positionHeight == item.positionHeight)
                            {
                                return randomDirection * -1;
                            }

                        }
                    if (positionHeight - 1 == 0 & currentDirection != 2)
                        {
                            return currentDirection * 3;
                        }
                        else if( positionHeight + 1 == 18 & currentDirection != 2)
                        {
                            return currentDirection / 3;
                        }
                    break;
                       
                        
                     
                        


                    case -1:
                    case -2:
                    case -3:
                        foreach (RacketCell item in listPlayerTwo)
                        {
                            if (positionWidth - 1 == item.positionWidth & positionHeight == item.positionHeight)
                            {
                                return randomDirection;
                            }

                        }
                        if (positionHeight - 1 == 0 & currentDirection != 2)
                        {
                            return currentDirection * 3;
                        }
                        else if (positionHeight + 1 == 18 & currentDirection != 2)
                    {
                            return currentDirection / 3;
                        }
                    break;
                }
            
                return currentDirection;
            
        }



        }

    }
    

