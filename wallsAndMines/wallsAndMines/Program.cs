using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading;



namespace CursorMovement

{

    class Program

    {

        static void Main(string[] args)
        {
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

            ConsoleColor currentForeground = Console.ForegroundColor;

            int[,] innerWall = new int[60, 26];
            Random wallRand = new Random();
            int counter = 0;
            //int wallHitCounter = 0;

            //duvara carpma kosulu icin deneme
            //int[,] wallLocation = null ;
            //wallLocation[0, i] =  { wallX, wallY};




            int score = 0;
            double time = 0;


            //Console.SetCursorPosition(3, 3);

            int cursorx = 26, cursory = 24;   // position of cursor 

            ConsoleKeyInfo cki;               // required for readkey 

            int ax = 15, ay = 4;    // position of A 

            int adirX = 1;            //Xaxis direction of A:   1:rigth   -1:left

            int adirY = 1;            //Yaxis direction of A:   1:Down   -1:Up



            // --- Static screen parts 

            Console.SetCursorPosition(3, 3);

            Console.ForegroundColor = colors[9];
            Console.WriteLine("⬔⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬔", colors[9]);


            for (int i = 0; i < 22; i++)
            {

                Console.SetCursorPosition(3, 3 + i + 1);

                
                Console.WriteLine("▋                                                   ▊");

            }

            Console.SetCursorPosition(3, 25);
            
            Console.WriteLine("⬕⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬕");

            Console.ResetColor();

            // --- Main game loop

            while (true)
            {
                //removes and creates the wall in every 15 seconds
                if (counter == 0)
                { 
                    //resetting the indexes of the wall
                    for (int i = 0; i < innerWall.GetLength(0); i++)
                    {
                        for (int j = 0; j < innerWall.GetLength(1); j++)
                        {
                            innerWall[i, j] = 0;
                        }
                    }
                    CreatingWalls(innerWall);
                }
                counter++;
                if (counter % 7 == 0)
                {
                    ShiftingWalls(innerWall);
                }


                while (Console.KeyAvailable)
                {       // true: there is a key in keyboard buffer 

                    cki = Console.ReadKey(true);       // true: do not write character  



                    if (cki.Key == ConsoleKey.RightArrow && cursorx < 54 && innerWall[cursorx + 1, cursory] != 1)
                    {   // key and boundary control 

                        Console.SetCursorPosition(cursorx, cursory);           // delete X (old position) 

                        Console.WriteLine(" ");

                        cursorx++;

                    }

                    if (cki.Key == ConsoleKey.LeftArrow && cursorx > 4 && innerWall[cursorx - 1, cursory] != 1)
                    {

                        Console.SetCursorPosition(cursorx, cursory);

                        Console.WriteLine(" ");

                        cursorx--;

                    }

                    if (cki.Key == ConsoleKey.UpArrow && cursory > 4 && innerWall[cursorx, cursory - 1] != 1)
                    {

                        Console.SetCursorPosition(cursorx, cursory);

                        Console.WriteLine(" ");

                        cursory--;

                    }
                    //sets the limit of X player
                    if (cki.Key == ConsoleKey.DownArrow && cursory < 24 && innerWall[cursorx, cursory + 1] != 1)
                    {

                        Console.SetCursorPosition(cursorx, cursory);

                        Console.WriteLine(" ");

                        cursory++;

                    }

                    if (cki.KeyChar >= 97 && cki.KeyChar <= 102)
                    {       // keys: a-f  

                        Console.SetCursorPosition(50, 5);

                        Console.WriteLine("Pressed Key: " + cki.KeyChar);

                    }

                    if (cki.Key == ConsoleKey.Escape) break;

                }

                
                if (adirX == 1 && ax >= 54 ) adirX = -1;    // change direction at boundaries 

                if (adirX == -1 && ax <= 4) adirX = 1;


                Console.SetCursorPosition(ax, ay);    // delete old A 

                Console.Write(" ");
                //depending on the location of X and player, chooses the direction of X
                if (ax == cursorx)
                    adirX = 0;
                else if (ax > cursorx)
                    adirX = -1;
                else
                    adirX = 1;

                if (ay == cursory)
                    adirY = 0;
                else if (ay > cursory)
                    adirY = -1;
                else
                    adirY = 1;

                if (ax == cursorx && (innerWall[ax, ay + adirY] != 1))
                {
                    if (adirY == -1)
                    {
                        if (innerWall[ax, ay - 1] != 1)
                        {
                            ay += adirY;
                        }
                    }
                    else if (adirY == 1)
                    {
                        if (innerWall[ax, ay + 1] != 1)
                        {
                            ay += adirY;
                        }
                    }
                }

                //incase of X intervines a corner, moves to the nearest exit
                else if (ax == cursorx && (innerWall[ax, ay + adirY] == 1) && (innerWall[ax + 1, ay] != 1 || innerWall[ax +adirX,ay] == 1))
                {

                    for (int i = 0; i < 4; i++)
                    {
                        adirX = 1;
                        if (innerWall[ax + i, ay] == 1)
                        {
                            adirX = -1;
                            break;
                        }
                    }

                    if (innerWall[ax, ay + adirY] == 1)
                    {

                        if (innerWall[ax + adirX, ay] != 1)
                        {
                            ax = ax + adirX;
                        }

                        else if (innerWall[ax + adirX, ay] == 1 && innerWall[ax, ay + adirY] != 1)
                        {
                            ay += adirY;
                        }
                    }
                    if (innerWall[ax, ay + adirY] == 1)
                    {
                        if (innerWall[ax + adirX, ay] != 1)
                        {
                            ax = ax + adirX;
                        }

                        else if (innerWall[ax + adirX, ay] == 1 && innerWall[ax, ay + adirY] != 1)
                        {
                            ay += adirY;
                        }
                    }
                    if (innerWall[ax, ay + adirY] == 1)
                    {
                        if (innerWall[ax + adirX, ay] != 1)
                        {
                            ax = ax + adirX;
                        }

                        else if (innerWall[ax + adirX, ay] == 1 && innerWall[ax, ay + adirY] != 1)
                        {
                            ay += adirY;
                        }
                    }
                    if (innerWall[ax, ay + adirY] == 1)
                    {
                        if (innerWall[ax + adirX, ay] != 1)
                        {
                            ax = ax + adirX;
                        }

                        else if (innerWall[ax + adirX, ay] == 1 && innerWall[ax, ay + adirY] != 1)
                        {
                            ay += adirY;
                        }
                    }
                    ay += adirY;
                }
                else if (innerWall[ax + adirX, ay] != 1)
                {
                    ax = ax + adirX;
                }

                else if (innerWall[ax + adirX, ay] == 1 && innerWall[ax, ay + adirY] != 1)
                {
                    ay += adirY;
                }

                

                Console.SetCursorPosition(80, 20);
                Console.Write("wall index-1:" + innerWall[ax, ay - 1]);
                Console.SetCursorPosition(80, 21);
                Console.Write("wall index+1:" + innerWall[ax, ay + 1]);


                Console.SetCursorPosition(ax, ay);    // refresh A (current position) 

                


                Console.ForegroundColor = colors[11];
                Console.WriteLine("ᗣ", colors[11]);

                
                Console.ResetColor();
                //Console.WriteLine("\nOriginal colors restored...");
                //Console.ReadLine();




                Console.SetCursorPosition(cursorx, cursory);    // refresh X (current position) 

               


                Console.ForegroundColor = colors[14];
                Console.WriteLine("ᗧ", colors[14]);

                Console.ResetColor();


                Thread.Sleep(100);     // sleep 50 ms 


                Console.SetCursorPosition(70, 3);
                Console.Write("Time :" + Convert.ToInt16(time));
                time += 0.14;

                if (cursorx == ax && cursory == ay)
                {
                    Console.SetCursorPosition(cursorx, cursory);
                    Console.Write("X");
                    Console.SetCursorPosition(50, 30);
                    Console.ForegroundColor = colors[4];
                    Console.WriteLine("GAMEOVER ,but continue anyways", colors[4]);
                    Console.ResetColor();
                    Console.Beep();
                    //break;
                    score++;
                }
                Console.SetCursorPosition(70, 5);
                Console.Write("Score :" + Convert.ToInt16(score));
            }


            Console.ReadLine();

        }

        static void ShiftingWalls(int[,] innerWall)
        {
            int[] Xblock =  {5,10,15,20,25,30,35,40,45,50};
            int[] Yblock =  {5,10,15,20};

            Random singBlock = new Random();
            int temp = singBlock.Next(0, 10);
            int chosenBlockX = Xblock[temp];
            temp = singBlock.Next(0, 4);
            int chosenBlockY = Yblock[temp];

            for (int i = chosenBlockY; i <= chosenBlockY+3; i++)
            {
                for (int j = chosenBlockX; j <= chosenBlockX+3; j++)
                {
                    innerWall[j, i] = 0;
                    Console.SetCursorPosition(j, i);
                    Console.Write(" ");
                }
            }

            Random rand = new Random();
            for (int wally = chosenBlockY; wally < chosenBlockY+1; wally++ )
            {

                for (int wall = chosenBlockX; wall < chosenBlockX+1; wall++)
                {
                    //Console.SetCursorPosition(wall, wally);

                    int noOfWalls = rand.Next(1, 4);
                    for (int j = 0; j < noOfWalls; j++)
                    {
                        int randWall = rand.Next(1, 5);
                        switch (randWall)
                        {
                            case 1:
                                for (int i = 0; i <= 3; i++)
                                {
                                    if (i == 0 || i == 3)
                                    {
                                        Console.SetCursorPosition(wall, wally + i);
                                        Console.Write("+");
                                        innerWall[wall, wally + i] = 1;
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(wall, wally + i);
                                        Console.Write("┃");
                                        innerWall[wall, wally + i] = 1;
                                    }
                                }
                                break;

                            case 2:
                                for (int i = 0; i <= 3; i++)
                                {
                                    if (i == 0 || i == 3)
                                    {
                                        Console.SetCursorPosition(wall+3, wally + i);
                                        Console.Write("+");
                                        innerWall[wall+3, wally + i] = 1;
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(wall+3, wally + i);
                                        Console.Write("┃");
                                        innerWall[wall+3, wally + i] = 1;
                                    }

                                }
                                break;
                            case 3:
                                for (int i = 0; i <= 3; i++)
                                {
                                    if (i == 0 || i == 3)
                                    {
                                        Console.SetCursorPosition(wall + i, wally);
                                        Console.Write("+");
                                        innerWall[wall + i, wally ] = 1;
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(wall + i, wally );
                                        Console.Write("━");
                                        innerWall[wall + i, wally ] = 1;
                                    }
                                }
                                break;
                            case 4:
                                for (int i = 0; i <= 3; i++)
                                {
                                    if (i == 0 || i == 3)
                                    {
                                        Console.SetCursorPosition(wall + i, wally + 3);
                                        Console.Write("+");
                                        innerWall[wall + i, wally + 3] = 1;
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(wall + i, wally + 3);
                                        Console.Write("━");
                                        innerWall[wall + i, wally + 3] = 1;
                                    }
                                    
                                }
                                break;

                        }
                    }
                    //Console.Write("▣");
                }
            }

        }

        static void CreatingWalls(int[,] innerWall)
        {

            Random rand = new Random();
            for (int wally = 5; wally <= 20; wally += 5)
            {

                for (int wall = 5; wall <= 50; wall += 5)
                {
                    //Console.SetCursorPosition(wall, wally);

                    int noOfWalls = rand.Next(1, 4);
                    for (int j = 0; j < noOfWalls; j++)
                    {
                        int randWall = rand.Next(1, 5);
                        switch (randWall)
                        {
                            case 1:
                                for (int i = 0; i <= 3; i++)
                                {
                                    if (i == 0 || i == 3)
                                    {
                                        Console.SetCursorPosition(wall, wally + i);
                                        Console.Write("+");
                                        innerWall[wall, wally + i] = 1;
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(wall, wally + i);
                                        Console.Write("┃");
                                        innerWall[wall, wally + i] = 1;
                                    }
                                }
                                break;

                            case 2:
                                for (int i = 0; i <= 3; i++)
                                {
                                    if (i == 0 || i == 3)
                                    {
                                        Console.SetCursorPosition(wall + 3, wally + i);
                                        Console.Write("+");
                                        innerWall[wall + 3, wally + i] = 1;
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(wall + 3, wally + i);
                                        Console.Write("┃");
                                        innerWall[wall + 3, wally + i] = 1;
                                    }

                                }
                                break;
                            case 3:
                                for (int i = 0; i <= 3; i++)
                                {
                                    if (i == 0 || i == 3)
                                    {
                                        Console.SetCursorPosition(wall + i, wally);
                                        Console.Write("+");
                                        innerWall[wall + i, wally] = 1;
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(wall + i, wally);
                                        Console.Write("━");
                                        innerWall[wall + i, wally] = 1;
                                    }
                                }
                                break;
                            case 4:
                                for (int i = 0; i <= 3; i++)
                                {
                                    if (i == 0 || i == 3)
                                    {
                                        Console.SetCursorPosition(wall + i, wally + 3);
                                        Console.Write("+");
                                        innerWall[wall + i, wally + 3] = 1;
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(wall + i, wally + 3);
                                        Console.Write("━");
                                        innerWall[wall + i, wally + 3] = 1;
                                    }

                                }
                                break;

                        }
                    }
                    //Console.Write("▣");
                }
            }
        }
    }

}