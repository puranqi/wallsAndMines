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
            
            Random wallRand = new Random();
            int counter = 210;

            //duvara carpma kosulu icin deneme
            //int[,] wallLocation = null ;
            //wallLocation[0, i] =  { wallX, wallY};



          
            int score = 0;
            double time = 0;


            //Console.SetCursorPosition(3, 3);

            int cursorx = 13, cursory = 13;   // position of cursor 

            ConsoleKeyInfo cki;               // required for readkey 

            int ax = 13, ay = 20;    // position of A 

            int adir = 1;            // direction of A:   1:rigth   -1:left 



            // --- Static screen parts 

            Console.SetCursorPosition(3, 3);

            Console.WriteLine("#####################################################");


            for (int i = 0; i < 22; i++)
            {

                Console.SetCursorPosition(3, 3 + i + 1);


                Console.WriteLine("#                                                   #");

            }

            Console.SetCursorPosition(3, 25);

            Console.WriteLine("#####################################################");


            // --- Main game loop



            //Creating inner walls

            /*
            for(int wallXaxis = 5; wallXaxis <= 50; wallXaxis += 5)
            {

                for( int wallYaxis = 5; wallYaxis < 21 ;wallYaxis+=1 )
                {
                    
                    for (int i = 0; i < wall.GetLength(0); i++)
                    {
                        Console.SetCursorPosition(wallXaxis, wallYaxis);
                        for (int j = 0; j < wall.GetLength(1); j++)
                        {
                            Console.WriteLine(wall[i, j]);
                            wallYaxis++;
                        }
                        
                    }
                    wallY -= wall.GetLength(1);

                }
            }
            */




            

            while (true)
            {
                if(counter % 210 ==0)                
                {
                    RemovingWalls();
                    CreatingWalls();
                }
                counter++;


                if (Console.KeyAvailable)
                {       // true: there is a key in keyboard buffer 

                    cki = Console.ReadKey(true);       // true: do not write character  



                    if (cki.Key == ConsoleKey.RightArrow && cursorx < 54)
                    {   // key and boundary control 

                        Console.SetCursorPosition(cursorx, cursory);           // delete X (old position) 

                        Console.WriteLine(" ");

                        cursorx++;

                    }

                    if (cki.Key == ConsoleKey.LeftArrow && cursorx > 4)
                    {

                        Console.SetCursorPosition(cursorx, cursory);

                        Console.WriteLine(" ");

                        cursorx--;

                    }

                    if (cki.Key == ConsoleKey.UpArrow && cursory > 4)
                    {

                        Console.SetCursorPosition(cursorx, cursory);

                        Console.WriteLine(" ");

                        cursory--;

                    }
                    //sets the limit of X player
                    if (cki.Key == ConsoleKey.DownArrow && cursory < 24)
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


                if (adir == 1 && ax >= 54) adir = -1;    // change direction at boundaries 

                if (adir == -1 && ax <= 4) adir = 1;



                Console.SetCursorPosition(ax, ay);    // delete old A 

                Console.WriteLine(" ");

                ax = ax + adir;

                Console.SetCursorPosition(ax, ay);    // refresh A (current position) 

                Console.WriteLine("A");



                Console.SetCursorPosition(cursorx, cursory);    // refresh X (current position) 

                Console.WriteLine("X");



                Thread.Sleep(50);     // sleep 50 ms 


                Console.SetCursorPosition(70, 3);
                Console.Write("Time :" + Convert.ToInt16(time));
                time += 0.07;

                if (cursorx == ax && cursory == ay)
                    score++;

                Console.SetCursorPosition(70, 5);
                Console.Write("Score :" + Convert.ToInt16(score));
            }


            Console.ReadLine();

        }

        static void RemovingWalls()
        {
            for (int i = 4; i < 55; i++)
            {
                for (int j = 4; j < 25; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(" ");
                }
            }

        }

        static void CreatingWalls()
        {
            Random rand = new Random();
            for (int wally = 5; wally < 21; wally += 5)
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
                                    Console.Write("#");
                                }
                                break;

                            case 2:
                                for (int i = 0; i <= 3; i++)
                                {
                                    Console.SetCursorPosition(wall + 3, wally + i);
                                    Console.Write("#");
                                }
                                break;
                            case 3:
                                for (int i = 0; i <= 3; i++)
                                {
                                    Console.SetCursorPosition(wall + i, wally);
                                    Console.Write("#");
                                }
                                break;
                            case 4:
                                for (int i = 0; i <= 3; i++)
                                {
                                    Console.SetCursorPosition(wall + i, wally + 3);
                                    Console.Write("#");
                                }
                                break;
                        }
                    }
                    //Console.Write("#");
                }
            }
        }
    }

}