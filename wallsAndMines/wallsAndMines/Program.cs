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
            int[,] innerWall = new int[60, 25];
            Random wallRand = new Random();
            int counter = 0;
            //int wallHitCounter = 0;

            //duvara carpma kosulu icin deneme
            //int[,] wallLocation = null ;
            //wallLocation[0, i] =  { wallX, wallY};




            int score = 0;
            double time = 0;


            //Console.SetCursorPosition(3, 3);

            int cursorx = 35, cursory = 20;   // position of cursor 

            ConsoleKeyInfo cki;               // required for readkey 

            int ax = 13, ay = 5;    // position of A 

            int adirX = 1;            //Xaxis direction of A:   1:rigth   -1:left

            int adirY = 1;            //Yaxis direction of A:   1:Down   -1:Up



            // --- Static screen parts 

            Console.SetCursorPosition(3, 3);

            Console.WriteLine("⬔⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬓⬔");


            for (int i = 0; i < 22; i++)
            {

                Console.SetCursorPosition(3, 3 + i + 1);


                Console.WriteLine("▋                                                   ▊");

            }

            Console.SetCursorPosition(3, 25);

            Console.WriteLine("⬕⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬒⬕");


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
                    RemovingWalls(innerWall);
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

                Console.WriteLine(" ");
                /*
                if (adirX == 1)
                {
                    if (ax == cursorx && (innerWall[ax, ay - 1] != 1 || innerWall[ax, ay + 1] != 1))
                    {
                        if (ay > cursory)
                        {
                            adirY = -1;
                            ay += adirY;
                        }
                        else
                        {
                            adirY = 1;
                            ay += adirY;
                        }
                    }
                    
                        
                    if (innerWall[ax + adirX, ay] != 1)
                    {
                        if (ax > cursorx)
                        {
                            adirX = -1;
                            ax = ax + adirX;
                        }
                        else
                        {

                            adirX = 1;
                            ax = ax + adirX;
                        }
                        //ay += adirY;
                        //adirX = -1;
                    }
                    else if (innerWall[ax + adirX, ay] == 1)
                    {
                        if (ay < cursory &&(innerWall[ax, ay - 1] != 1 || innerWall[ax, ay + 1] != 1))
                        {
                            adirY = 1;
                            ay += adirY;
                        }
                        else if (ay > cursory && (innerWall[ax, ay - 1] != 1 || innerWall[ax, ay + 1] != 1))
                        {
                            adirY = -1;
                            ay += adirY;
                        }
                         
                        
                    }
                    else if (innerWall[ax + 1, ay] == 1 && innerWall[ax - 1, ay] == 1)
                    {
                        
                        ay += adirY;

                    }

                }

                if (adirX == -1)
                {
                    if (ax == cursorx && (innerWall[ax, ay - 1] != 1 || innerWall[ax, ay + 1] != 1))
                    {
                        if (ay > cursory && (innerWall[ax, ay - 1] != 1 || innerWall[ax, ay + 1] != 1))
                        {
                            adirY = -1;
                            ay += adirY;
                        }
                        else
                        {
                            adirY = 1;
                            ay += adirY;
                        }
                    }
                    else if (innerWall[ax + adirX, ay] != 1)
                    {

                        if (ax > cursorx)
                        {
                            adirX = -1;
                            ax = ax + adirX;
                        }
                        else
                        {

                            adirX = 1;
                            ax = ax + adirX;
                        }
                        //ay += adirY;

                    }
                    else if (innerWall[ax + adirX, ay] == 1)
                    {
                        //wallHitCounter++;
                        if (ay < cursory && (innerWall[ax, ay - 1] != 1 || innerWall[ax, ay + 1] != 1))
                        {
                            adirY = 1;
                            ay += adirY;
                        }
                        else if (ay > cursory && (innerWall[ax, ay - 1] != 1 || innerWall[ax, ay + 1] != 1))
                        {
                            adirY = -1;
                            ay += adirY;
                        }
                    }
                    else if (innerWall[ax + 1, ay] == 1 && innerWall[ax - 1, ay] == 1)
                    {
                        if (ay < cursory && (innerWall[ax, ay - 1] != 1 || innerWall[ax, ay + 1] != 1))
                        {
                            adirY = 1;
                            ay += adirY;
                        }
                        else
                        {
                            adirY = -1;
                            ay += adirY;
                        }

                    }
                }
                */

          
                Console.SetCursorPosition(ax, ay);    // refresh A (current position) 

                Console.WriteLine("ᗣ");
                                


                Console.SetCursorPosition(cursorx, cursory);    // refresh X (current position) 

                Console.WriteLine("ᗧ");



                Thread.Sleep(100);     // sleep 50 ms 


                Console.SetCursorPosition(70, 3);
                Console.Write("Time :" + Convert.ToInt16(time));
                time += 0.14;

                if (cursorx == ax && cursory == ay)
                {
                    Console.SetCursorPosition(cursorx, cursory);
                    Console.Write("X");
                    Console.SetCursorPosition(50, 30);
                    Console.WriteLine("GAMEOVER ,but continue anyways");
                    //break;
                    score++;
                }
                Console.SetCursorPosition(70, 5);
                Console.Write("Score :" + Convert.ToInt16(score));
            }


            Console.ReadLine();

        }

        static void RemovingWalls(int[,] innerWall)
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
                                    Console.SetCursorPosition(wall, wally + i);
                                    Console.Write("■");
                                    innerWall[wall, wally + i] = 1;

                                }
                                break;

                            case 2:
                                for (int i = 0; i <= 3; i++)
                                {
                                    Console.SetCursorPosition(wall + 3, wally + i);
                                    Console.Write("■");
                                    innerWall[wall + 3, wally + i] = 1;
                                }
                                break;
                            case 3:
                                for (int i = 0; i <= 3; i++)
                                {
                                    Console.SetCursorPosition(wall + i, wally);
                                    Console.Write("■");
                                    innerWall[wall + i, wally] = 1;
                                }
                                break;
                            case 4:
                                for (int i = 0; i <= 3; i++)
                                {
                                    Console.SetCursorPosition(wall + i, wally + 3);
                                    Console.Write("■");
                                    innerWall[wall + i, wally + 3] = 1;
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
                                    Console.SetCursorPosition(wall, wally + i);
                                    Console.Write("■");
                                    innerWall[wall, wally + i] = 1;

                                }
                                break;

                            case 2:
                                for (int i = 0; i <= 3; i++)
                                {
                                    Console.SetCursorPosition(wall + 3, wally + i);
                                    Console.Write("■");
                                    innerWall[wall + 3, wally + i] = 1;
                                }
                                break;
                            case 3:
                                for (int i = 0; i <= 3; i++)
                                {
                                    Console.SetCursorPosition(wall + i, wally);
                                    Console.Write("■");
                                    innerWall[wall + i, wally] = 1;
                                }
                                break;
                            case 4:
                                for (int i = 0; i <= 3; i++)
                                {
                                    Console.SetCursorPosition(wall + i, wally + 3);
                                    Console.Write("■");
                                    innerWall[wall + i, wally + 3] = 1;
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