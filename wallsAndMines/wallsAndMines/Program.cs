﻿using System;

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
            int XdeathCounter = 0;
            int YdeathCounter = 0;
         
            int mine = 0; //0 lanacak 
            bool aliveX = true;
            bool aliveY = true;            

            //energy
            int moveCounter = 0;
            int energy = 500;
            int score = 0;
            double time = 0;
            
     

            int cursorx = 26, cursory = 24;   // position of cursor 

            ConsoleKeyInfo cki;               // required for readkey 

            string lastKey = "";

            int ax = 15, ay = 4;    // position of A

            int adirX = 1;            //Xaxis direction of A:   1:rigth   -1:left

            int adirY = 1;            //Yaxis direction of A:   1:Down   -1:Up
                
            int bx = wallRand.Next(4, 56);          // position of B
            int by = wallRand.Next(4, 22);          // position of B

            int bdirX = 1;            //Xbxis direction of A:   1:rigth   -1:left

            int bdirY = 1;            //Ybxis direction of A:   1:Down   -1:Up



            // --- outerWalls 

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
            Console.SetCursorPosition(70,25);
            Console.WriteLine("Press Any Key to Start");
            Console.ReadKey();
            Console.SetCursorPosition(70, 25);
            Console.WriteLine("                                   ");

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

                if (counter % 7 == 0)
                    AddingPoints(innerWall);



                if (counter == 0)
                {
                    Console.SetCursorPosition(70, 25);
                    Console.WriteLine("Press Any Key to Start");
                    Console.ReadKey();
                    Console.SetCursorPosition(70, 25);
                    Console.WriteLine("                                   ");
                }


                while (Console.KeyAvailable)
                {       // true: there is a key in keyboard buffer 

                    cki = Console.ReadKey(true);       // true: do not write character  

                    
                    if (energy > 0)
                    {
                        if (cki.Key == ConsoleKey.RightArrow && cursorx < 54 && innerWall[cursorx + 1, cursory] != 1)
                        {   // key and boundary control 

                            Console.SetCursorPosition(cursorx, cursory);           // delete X (old position) 

                            Console.WriteLine(" ");

                            cursorx++;

                            if (energy > 0)
                                energy--;

                            lastKey = "R";

                        }

                        if (cki.Key == ConsoleKey.LeftArrow && cursorx > 4 && innerWall[cursorx - 1, cursory] != 1)
                        {

                            Console.SetCursorPosition(cursorx, cursory);

                            Console.WriteLine(" ");

                            cursorx--;

                            if (energy > 0)
                                energy--;

                            lastKey = "L";

                        }

                        if (cki.Key == ConsoleKey.UpArrow && cursory > 4 && innerWall[cursorx, cursory - 1] != 1)
                        {

                            Console.SetCursorPosition(cursorx, cursory);

                            Console.WriteLine(" ");

                            cursory--;

                            if (energy > 0)
                                energy--;

                            lastKey = "U";

                        }
                        //sets the limit of X player
                        if (cki.Key == ConsoleKey.DownArrow && cursory < 24 && innerWall[cursorx, cursory + 1] != 1)
                        {

                            Console.SetCursorPosition(cursorx, cursory);

                            Console.WriteLine(" ");

                            cursory++;

                            if (energy > 0)
                                energy--;

                            lastKey = "D";

                        }

                        if (cki.KeyChar >= 97 && cki.KeyChar <= 102)
                        {       // keys: a-f  

                            Console.SetCursorPosition(50, 5);

                            Console.WriteLine("Pressed Key: " + cki.KeyChar);

                        }

                        if (cki.Key == ConsoleKey.Escape) break;

                        if (energy == 1)
                            moveCounter++;
                    }
                    if (energy == 0 && moveCounter % 2 == 0)
                    {
                        if (cki.Key == ConsoleKey.RightArrow && cursorx < 54 && innerWall[cursorx + 1, cursory] != 1)
                        {   // key and boundary control 

                            Console.SetCursorPosition(cursorx, cursory);           // delete X (old position) 

                            Console.WriteLine(" ");

                            cursorx++;

                            if (energy > 0)
                                energy--;

                            lastKey = "R";

                        }

                        if (cki.Key == ConsoleKey.LeftArrow && cursorx > 4 && innerWall[cursorx - 1, cursory] != 1)
                        {

                            Console.SetCursorPosition(cursorx, cursory);

                            Console.WriteLine(" ");

                            cursorx--;

                            if (energy > 0)
                                energy--;

                            lastKey = "L";

                        }

                        if (cki.Key == ConsoleKey.UpArrow && cursory > 4 && innerWall[cursorx, cursory - 1] != 1)
                        {

                            Console.SetCursorPosition(cursorx, cursory);

                            Console.WriteLine(" ");

                            cursory--;

                            if (energy > 0)
                                energy--;

                            lastKey = "U";

                        }
                        //sets the limit of X player
                        if (cki.Key == ConsoleKey.DownArrow && cursory < 24 && innerWall[cursorx, cursory + 1] != 1)
                        {

                            Console.SetCursorPosition(cursorx, cursory);

                            Console.WriteLine(" ");

                            cursory++;

                            if (energy > 0)
                                energy--;

                            lastKey = "D";
                        }

                        if (cki.KeyChar >= 97 && cki.KeyChar <= 102)
                        {       // keys: a-f  

                            Console.SetCursorPosition(50, 5);

                            Console.WriteLine("Pressed Key: " + cki.KeyChar);

                        }

                        if (cki.Key == ConsoleKey.Escape) break;

                    }
                    if ((cki.Key == ConsoleKey.RightArrow || cki.Key == ConsoleKey.LeftArrow || cki.Key == ConsoleKey.UpArrow || cki.Key == ConsoleKey.DownArrow) && energy == 0)
                        moveCounter++;

                    if (cki.Key == ConsoleKey.Spacebar)
                    {

                        if (mine > 0)
                        {
                                                      
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Black;
                                if (lastKey == "D" && cursory < 26 && innerWall[cursorx, cursory - 1] != 1)
                                {
                                    Console.SetCursorPosition(cursorx, cursory - 1);
                                    Console.WriteLine("+");
                                    innerWall[cursorx, cursory - 1] = 5;
                                    mine--;
                                }
                                else if (lastKey=="U" && cursory < 26 && innerWall[cursorx, cursory + 1] != 1)
                                {
                                    Console.SetCursorPosition(cursorx, cursory + 1);
                                    Console.WriteLine("+");
                                    innerWall[cursorx, cursory + 1] = 5;
                                    mine--;
                                }
                                else if (lastKey=="R" && cursorx < 60 && innerWall[cursorx-1, cursory] != 1)
                                {
                                    Console.SetCursorPosition(cursorx-1, cursory );
                                    Console.WriteLine("+");
                                    innerWall[cursorx-1, cursory ] = 5;
                                    mine--;
                                }
                                else if (lastKey == "L" && cursorx < 60 && innerWall[cursorx + 1, cursory] != 1)
                                {
                                    Console.SetCursorPosition(cursorx + 1, cursory);
                                    Console.WriteLine("+");
                                    innerWall[cursorx + 1, cursory] = 5;
                                    mine--;
                                }
                              
                                Console.ResetColor();
                                                                    
                        }

                        else
                        {
                            Console.SetCursorPosition(70, 10);
                            Console.ForegroundColor = colors[4];
                            Console.WriteLine("You don't have any mine!", colors[4]); // yerini ayarla!  mine: da çıksın
                            Console.ResetColor();
                        }


                    }
                }




                if (adirX == 1 && ax >= 54) adirX = -1;    // change direction at boundaries 

                if (adirX == -1 && ax <= 4) adirX = 1;


                Console.SetCursorPosition(ax, ay);    // delete old A 

                if (aliveX == true)
                {
                    //X winning against Player
                    if (cursorx == ax && cursory == ay)
                    {
                        Console.SetCursorPosition(cursorx, cursory);
                        Console.Write("X");
                        Console.SetCursorPosition(50, 30);
                        Console.ForegroundColor = colors[4];
                        Console.Clear();
                        string s = "                      ⢀⣀⣠⣴⣶⣶⣶⣶⣾⣿⣿⣶⣶⣶⣤⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣶⣿⣿⡿⠿⠛⠛⠋⠉⠉⠉⠉⠉⠉⠛⠛⠻⢿⣿⣿⣷⣦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⣿⣿⠿⠛⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠙⠻⣿⣿⣶⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⡿⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠻⣿⣿⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣿⣿⣦⡀⠀⢀⣠⣤⣶⡄⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⡶⠶⠛⠛⠋⠉⠉⠛⠛⠳⠶⢤⣄⡀⠀⠀⠀⠀⠀⠀⠀⣀⣨⣿⣿⣿⡿⠿⠛⠛⢿⣿⡀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⢠⣾⣿⠟⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⠞⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⢶⣄⣠⣤⣶⣶⡿⠿⠟⠛⠉⠁⢀⣀⠀⠀⠘⣿⣇⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⣠⡾⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣴⣶⣾⣿⠿⠟⠋⠉⠀⢀⣀⣴⡴⣾⣿⣿⣿⣷⡄⠀⢹⣿⡄⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⢀⣾⣿⠇⠀⠀⠀⠀⠀⠀⠀⣴⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣤⣶⣾⣿⠿⠛⠋⠉⠀⠀⠀⢠⣶⣾⣿⣿⠿⠟⠃⢹⣿⡄⠈⣿⣧⠀⠀⣿⣧⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⣼⣿⡏⠀⠀⠀⠀⠀⠀⠀⣼⠋⠀⠀⠀⢀⣀⣤⣤⣶⣾⡿⠿⠛⠛⠉⠁⢀⣤⣤⣄⠸⣿⣆⠀⠸⣿⡇⢿⣧⠀⢀⣠⠈⣿⣷⣶⣿⡏⠀⠀⠸⣿⣇⠀⠀\n⠀⠀⠀⠀⠀⢠⣿⣿⠁⠀⠀⠀⠀⠀⠀⣸⣏⣠⣤⣶⣿⡿⠿⠟⠛⠉⠀⠀⠀⠀⠀⠀⠀⣾⣿⠻⣿⣧⠹⣿⣆⠀⣿⡇⠸⣿⣿⣿⠿⠇⠸⣿⡏⢻⣿⣆⠀⠀⢿⣿⡀⠀\n⠀⠀⠀⠀⠀⢸⣿⡟⠀⠀⣀⣠⣴⣶⣿⣿⠿⠟⠋⠉⠀⢀⣠⣤⣶⣾⣧⠀⠀⠀⠀⠀⠀⣿⣿⠀⠈⣿⣧⠘⣿⣆⣿⣿⠀⢻⣿⡄⠀⠀⣀⢻⣿⡀⠙⣿⣷⠄⠘⣿⣧⠀\n⠀⠀⠀⢀⣀⣾⣿⣷⣾⡿⠿⠛⠋⠉⠀⠀⣤⣤⠀⢸⣿⡌⣿⣿⠋⠉⠁⠀⠀⠀⠀⠀⠀⠸⣿⣇⠀⢸⣿⡇⠘⣿⣾⣿⠀⠈⣿⣷⣾⣿⡿⠮⠛⠃⠀⢀⣀⣠⣤⣾⣿⠄\n⣴⣶⣾⡿⠿⠛⠛⠉⠁⠀⠀⠀⣶⣿⣆⠀⢹⣿⣧⣸⣿⣧⠸⣿⣦⣤⣶⣆⠀⠀⠀⠀⠀⠀⢻⣿⣄⢀⣿⡇⠀⠘⣿⣿⡆⠀⠘⠛⠉⠁⣀⣠⣤⣶⣾⣿⠿⠟⠛⠉⠁⠀\n⢹⣿⣇⠀⠀⠀⣴⣿⣿⣷⡄⠀⢹⡿⣿⣆⠈⣿⣿⣿⣿⢿⣆⢻⣿⠟⠋⠉⠀⠀⠀⠀⠀⠀⠀⠻⣿⣿⡿⠃⠀⠀⠀⠀⣀⣠⣴⣶⣿⣿⠿⠟⠋⢩⣿⣿⠀⠀⠀⠀⠀⠀\n⠀⢿⣿⡀⠀⠀⣿⣟⠈⠛⠋⠀⢸⣿⠈⣿⣆⠸⣿⣻⣿⡾⣿⡌⣿⣇⣀⣤⣴⡆⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣤⣶⣾⣿⠿⣿⠛⠉⠁⠀⠀⠀⠀⢸⣿⡟⠀⠀⠀⠀⠀⠀\n⠀⠸⣿⣧⠀⠀⢻⣿⠀⣶⣾⣧⠘⣿⣷⣾⣿⣆⢿⣧⠉⠁⢻⣧⢹⣿⡿⠿⠛⠃⠀⢀⣀⣤⣴⣶⡾⡿⠟⠋⠛⠉⠁⠀⣰⠏⠀⠀⠀⠀⠀⠀⠀⣿⣿⠇⠀⠀⠀⠀⠀⠀\n⠀⠀⢹⣿⡄⠀⠈⣿⣧⠙⠙⣿⡆⣿⣟⠉⠘⣿⣾⣿⡆⠀⠀⠛⠁⢀⣀⣠⣤⣶⣿⡿⠿⠛⠋⠁⠀⠀⠀⠀⠀⠀⠀⣴⠟⠀⠀⠀⠀⠀⠀⠀⣸⣿⡏⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⣿⣷⠀⠀⠘⣿⣷⣴⣿⡏⣿⡿⠀⠀⠈⠉⢀⣀⣠⣴⣶⣿⡿⠿⠛⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⡾⠋⠀⠀⠀⠀⠀⠀⠀⣰⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠸⣿⡆⠀⠀⠈⠛⠛⠋⠀⢀⣀⣤⣴⣶⣿⡿⠿⣿⣋⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⡾⠋⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⢻⣿⡄⢀⣀⣤⣴⣶⣿⠿⠿⠛⠋⠉⠀⠀⠀⠈⠙⠳⢦⣤⣀⣀⠀⠀⠀⠀⠀⢀⣀⣠⣤⠶⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠈⣿⣿⡿⠿⠛⠙⢿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠉⠙⠛⠛⠛⠛⠉⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠿⣿⣷⣦⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣾⣿⡿⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠿⢿⣿⣷⣦⣄⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣤⣶⣿⣿⡿⠟⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠻⠿⣿⣿⣿⣿⣷⣶⣶⣶⣾⣿⣿⣿⣿⡿⠿⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠀⠉⠉⠉⠉⠉⠉⠉⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀";


                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (s.Length / 2)) + "}", s));
                        Console.SetCursorPosition(24, 30);
                        Console.WriteLine("Your Score:" + score);
                        Console.ReadKey();
                        Console.ResetColor();
                        //  Console.Beep(5000, 1000);
                        break;
                      
                    }

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
                    else if (ax == cursorx && (innerWall[ax, ay + adirY] == 1) && (innerWall[ax + 1, ay] != 1 || innerWall[ax + adirX, ay] == 1))
                    {
                        if (adirX == 1)
                            adirX = -1;
                        else
                            adirX = 1;

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

                    Console.Write(" ");
                    Console.SetCursorPosition(ax, ay);    // refresh A (current position) 
                    Console.ForegroundColor = colors[11];
                    Console.WriteLine("ᗣ", colors[11]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("");
                    XdeathCounter++;

                    //X respawn time
                    Console.SetCursorPosition(70, 16);
                    Console.Write("                                         ");
                    Console.SetCursorPosition(70, 16);
                    Console.Write("X Respawns in:" + (10 - XdeathCounter / 10));


                }

                if (aliveX == false && XdeathCounter % 100 == 0)
                {
                    aliveX = true;
                    XdeathCounter = 0;

                    Console.SetCursorPosition(70, 16);
                    Console.Write("                             ");
                }
                    


                if (bdirX == 1 && bx >= 54) bdirX = -1;    // change direction at boundaries 

                if (bdirY == -1 && by <= 4) bdirY = 1;


                Console.SetCursorPosition(bx, by);    // delete old A 

                if (aliveY == true)
                {
                    //Player B winning against player
                    if ((cursory == by && cursorx == bx))
                    {
                        Console.SetCursorPosition(cursorx, cursory);
                        Console.Write("X");
                        Console.SetCursorPosition(50, 30);
                        Console.ForegroundColor = colors[4];
                        Console.Clear();
                        string s = "                      ⢀⣀⣠⣴⣶⣶⣶⣶⣾⣿⣿⣶⣶⣶⣤⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣶⣿⣿⡿⠿⠛⠛⠋⠉⠉⠉⠉⠉⠉⠛⠛⠻⢿⣿⣿⣷⣦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⣿⣿⠿⠛⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠙⠻⣿⣿⣶⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⡿⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠻⣿⣿⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣿⣿⣦⡀⠀⢀⣠⣤⣶⡄⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⡶⠶⠛⠛⠋⠉⠉⠛⠛⠳⠶⢤⣄⡀⠀⠀⠀⠀⠀⠀⠀⣀⣨⣿⣿⣿⡿⠿⠛⠛⢿⣿⡀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⢠⣾⣿⠟⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⠞⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⢶⣄⣠⣤⣶⣶⡿⠿⠟⠛⠉⠁⢀⣀⠀⠀⠘⣿⣇⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⣠⡾⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣴⣶⣾⣿⠿⠟⠋⠉⠀⢀⣀⣴⡴⣾⣿⣿⣿⣷⡄⠀⢹⣿⡄⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⢀⣾⣿⠇⠀⠀⠀⠀⠀⠀⠀⣴⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣤⣶⣾⣿⠿⠛⠋⠉⠀⠀⠀⢠⣶⣾⣿⣿⠿⠟⠃⢹⣿⡄⠈⣿⣧⠀⠀⣿⣧⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⣼⣿⡏⠀⠀⠀⠀⠀⠀⠀⣼⠋⠀⠀⠀⢀⣀⣤⣤⣶⣾⡿⠿⠛⠛⠉⠁⢀⣤⣤⣄⠸⣿⣆⠀⠸⣿⡇⢿⣧⠀⢀⣠⠈⣿⣷⣶⣿⡏⠀⠀⠸⣿⣇⠀⠀\n⠀⠀⠀⠀⠀⢠⣿⣿⠁⠀⠀⠀⠀⠀⠀⣸⣏⣠⣤⣶⣿⡿⠿⠟⠛⠉⠀⠀⠀⠀⠀⠀⠀⣾⣿⠻⣿⣧⠹⣿⣆⠀⣿⡇⠸⣿⣿⣿⠿⠇⠸⣿⡏⢻⣿⣆⠀⠀⢿⣿⡀⠀\n⠀⠀⠀⠀⠀⢸⣿⡟⠀⠀⣀⣠⣴⣶⣿⣿⠿⠟⠋⠉⠀⢀⣠⣤⣶⣾⣧⠀⠀⠀⠀⠀⠀⣿⣿⠀⠈⣿⣧⠘⣿⣆⣿⣿⠀⢻⣿⡄⠀⠀⣀⢻⣿⡀⠙⣿⣷⠄⠘⣿⣧⠀\n⠀⠀⠀⢀⣀⣾⣿⣷⣾⡿⠿⠛⠋⠉⠀⠀⣤⣤⠀⢸⣿⡌⣿⣿⠋⠉⠁⠀⠀⠀⠀⠀⠀⠸⣿⣇⠀⢸⣿⡇⠘⣿⣾⣿⠀⠈⣿⣷⣾⣿⡿⠮⠛⠃⠀⢀⣀⣠⣤⣾⣿⠄\n⣴⣶⣾⡿⠿⠛⠛⠉⠁⠀⠀⠀⣶⣿⣆⠀⢹⣿⣧⣸⣿⣧⠸⣿⣦⣤⣶⣆⠀⠀⠀⠀⠀⠀⢻⣿⣄⢀⣿⡇⠀⠘⣿⣿⡆⠀⠘⠛⠉⠁⣀⣠⣤⣶⣾⣿⠿⠟⠛⠉⠁⠀\n⢹⣿⣇⠀⠀⠀⣴⣿⣿⣷⡄⠀⢹⡿⣿⣆⠈⣿⣿⣿⣿⢿⣆⢻⣿⠟⠋⠉⠀⠀⠀⠀⠀⠀⠀⠻⣿⣿⡿⠃⠀⠀⠀⠀⣀⣠⣴⣶⣿⣿⠿⠟⠋⢩⣿⣿⠀⠀⠀⠀⠀⠀\n⠀⢿⣿⡀⠀⠀⣿⣟⠈⠛⠋⠀⢸⣿⠈⣿⣆⠸⣿⣻⣿⡾⣿⡌⣿⣇⣀⣤⣴⡆⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣤⣶⣾⣿⠿⣿⠛⠉⠁⠀⠀⠀⠀⢸⣿⡟⠀⠀⠀⠀⠀⠀\n⠀⠸⣿⣧⠀⠀⢻⣿⠀⣶⣾⣧⠘⣿⣷⣾⣿⣆⢿⣧⠉⠁⢻⣧⢹⣿⡿⠿⠛⠃⠀⢀⣀⣤⣴⣶⡾⡿⠟⠋⠛⠉⠁⠀⣰⠏⠀⠀⠀⠀⠀⠀⠀⣿⣿⠇⠀⠀⠀⠀⠀⠀\n⠀⠀⢹⣿⡄⠀⠈⣿⣧⠙⠙⣿⡆⣿⣟⠉⠘⣿⣾⣿⡆⠀⠀⠛⠁⢀⣀⣠⣤⣶⣿⡿⠿⠛⠋⠁⠀⠀⠀⠀⠀⠀⠀⣴⠟⠀⠀⠀⠀⠀⠀⠀⣸⣿⡏⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⣿⣷⠀⠀⠘⣿⣷⣴⣿⡏⣿⡿⠀⠀⠈⠉⢀⣀⣠⣴⣶⣿⡿⠿⠛⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⡾⠋⠀⠀⠀⠀⠀⠀⠀⣰⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠸⣿⡆⠀⠀⠈⠛⠛⠋⠀⢀⣀⣤⣴⣶⣿⡿⠿⣿⣋⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⡾⠋⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⢻⣿⡄⢀⣀⣤⣴⣶⣿⠿⠿⠛⠋⠉⠀⠀⠀⠈⠙⠳⢦⣤⣀⣀⠀⠀⠀⠀⠀⢀⣀⣠⣤⠶⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠈⣿⣿⡿⠿⠛⠙⢿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠉⠙⠛⠛⠛⠛⠉⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠿⣿⣷⣦⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣾⣿⡿⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠿⢿⣿⣷⣦⣄⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣤⣶⣿⣿⡿⠟⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠻⠿⣿⣿⣿⣿⣷⣶⣶⣶⣾⣿⣿⣿⣿⡿⠿⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠀⠉⠉⠉⠉⠉⠉⠉⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀";


                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (s.Length / 2)) + "}", s));
                        Console.SetCursorPosition(24, 30);
                        Console.WriteLine("Your Score:" + score);
                        Console.ReadKey();
                        Console.ResetColor();
                        //  Console.Beep(5000, 1000);
                        break;
                        //score++;
                    }

                    //depending on the location of X and plbxer, chooses the direction of X
                    if (by == cursory)
                        bdirY = 0;
                    else if (by > cursory)
                        bdirY = -1;
                    else
                        bdirY = 1;

                    if (bx == cursorx)
                        bdirX = 0;
                    else if (bx > cursorx)
                        bdirX = -1;
                    else
                        bdirX = 1;

                    if (by == cursory && (innerWall[bx + bdirX, by] != 1))
                    {
                        if (bdirX == -1)
                        {
                            if (innerWall[bx + bdirX, by] != 1)
                            {
                                bx += bdirX;
                            }
                        }
                        else if (bdirX == 1)
                        {
                            if (innerWall[bx + bdirX, by] != 1)
                            {
                                bx += bdirX;
                            }
                        }
                    }

                    //incase of X intervines a corner, moves to the nearest exit
                    else if (by == cursory && (innerWall[bx + bdirX, by] == 1) && (innerWall[bx, by + 1] != 1 || innerWall[bx, by + bdirY] == 1))
                    {
                        if (bdirY == 1)
                            bdirY = -1;
                        else
                            bdirY = 1;

                        if (innerWall[bx + bdirX, by] == 1)
                        {

                            if (innerWall[bx, by + bdirY] != 1)
                            {
                                by = by + bdirY;
                            }

                            else if (innerWall[bx, by + bdirY] == 1 && innerWall[bx + bdirX, by] != 1)
                            {
                                bx += bdirX;
                            }
                        }
                        if (innerWall[bx + bdirX, by] == 1)
                        {
                            if (innerWall[bx, by + bdirY] != 1)
                            {
                                by = by + bdirY;
                            }

                            else if (innerWall[bx, by + bdirY] == 1 && innerWall[bx + bdirX, by] != 1)
                            {
                                bx += bdirX;
                            }
                        }
                        if (innerWall[bx + bdirX, by] == 1)
                        {
                            if (innerWall[bx, by + bdirY] != 1)
                            {
                                by = by + bdirY;
                            }

                            else if (innerWall[bx, by + bdirY] == 1 && innerWall[bx + bdirX, by] != 1)
                            {
                                bx += bdirX;
                            }
                        }
                        if (innerWall[bx + bdirX, by] == 1)
                        {
                            if (innerWall[bx, by + bdirY] != 1)
                            {
                                by = by + bdirY;
                            }

                            else if (innerWall[bx, by + bdirY] == 1 && innerWall[bx + bdirX, by] != 1)
                            {
                                bx += bdirX;
                            }
                        }
                        bx += bdirX;
                    }
                    else if (innerWall[bx, by + bdirY] != 1)
                    {
                        by = by + bdirY;
                    }

                    else if (innerWall[bx, by + bdirY] == 1 && innerWall[bx + bdirX, by] != 1)
                    {
                        bx += bdirX;
                    }

                    

                    Console.Write(" ");
                    Console.SetCursorPosition(bx, by);    // refresh A (current position) 
                    Console.ForegroundColor = colors[4];
                    Console.WriteLine("ᗣ", colors[4]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("");
                    YdeathCounter++;

                    //Y respawn time
                    Console.SetCursorPosition(70, 17);
                    Console.Write("                                         " );
                    Console.SetCursorPosition(70, 17);
                    Console.Write("Y Respawns in:" +(10  - YdeathCounter / 10));
                    
                }

                if (aliveY == false && YdeathCounter % 100 == 0)
                {
                    aliveY = true;
                    YdeathCounter = 0;
                    Console.SetCursorPosition(70, 17);
                    Console.Write("                         ");
                }

                //character X (cyan) death
                if (innerWall[bx, by] == 5 && aliveY)
                {
                    innerWall[bx, by] = 0;
                    Console.SetCursorPosition(bx, by);
                    Console.Write(" ");
                    aliveY = false;
                    score += 300;
                }

                //character Y (red) death
                if (innerWall[ax, ay] == 5 && aliveX)
                {
                    innerWall[ax, ay] =0;
                    Console.SetCursorPosition(ax, ay);
                    Console.Write(" ");
                    aliveX = false;
                    score += 300;
                }

                //playerDeath on mine
                if (innerWall[cursorx, cursory] == 5)
                {
                    innerWall[cursorx, cursory] = 0;
                    Console.SetCursorPosition(cursorx, cursory);
                    Console.Write("X");
                    Console.SetCursorPosition(50, 30);
                    Console.ForegroundColor = colors[4];
                    Console.Clear();
                    string s = "                      ⢀⣀⣠⣴⣶⣶⣶⣶⣾⣿⣿⣶⣶⣶⣤⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣶⣿⣿⡿⠿⠛⠛⠋⠉⠉⠉⠉⠉⠉⠛⠛⠻⢿⣿⣿⣷⣦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⣿⣿⠿⠛⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠙⠻⣿⣿⣶⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⡿⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠻⣿⣿⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣿⣿⣦⡀⠀⢀⣠⣤⣶⡄⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⡶⠶⠛⠛⠋⠉⠉⠛⠛⠳⠶⢤⣄⡀⠀⠀⠀⠀⠀⠀⠀⣀⣨⣿⣿⣿⡿⠿⠛⠛⢿⣿⡀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⢠⣾⣿⠟⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⠞⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⢶⣄⣠⣤⣶⣶⡿⠿⠟⠛⠉⠁⢀⣀⠀⠀⠘⣿⣇⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⣠⡾⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣴⣶⣾⣿⠿⠟⠋⠉⠀⢀⣀⣴⡴⣾⣿⣿⣿⣷⡄⠀⢹⣿⡄⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⢀⣾⣿⠇⠀⠀⠀⠀⠀⠀⠀⣴⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣤⣶⣾⣿⠿⠛⠋⠉⠀⠀⠀⢠⣶⣾⣿⣿⠿⠟⠃⢹⣿⡄⠈⣿⣧⠀⠀⣿⣧⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⣼⣿⡏⠀⠀⠀⠀⠀⠀⠀⣼⠋⠀⠀⠀⢀⣀⣤⣤⣶⣾⡿⠿⠛⠛⠉⠁⢀⣤⣤⣄⠸⣿⣆⠀⠸⣿⡇⢿⣧⠀⢀⣠⠈⣿⣷⣶⣿⡏⠀⠀⠸⣿⣇⠀⠀\n⠀⠀⠀⠀⠀⢠⣿⣿⠁⠀⠀⠀⠀⠀⠀⣸⣏⣠⣤⣶⣿⡿⠿⠟⠛⠉⠀⠀⠀⠀⠀⠀⠀⣾⣿⠻⣿⣧⠹⣿⣆⠀⣿⡇⠸⣿⣿⣿⠿⠇⠸⣿⡏⢻⣿⣆⠀⠀⢿⣿⡀⠀\n⠀⠀⠀⠀⠀⢸⣿⡟⠀⠀⣀⣠⣴⣶⣿⣿⠿⠟⠋⠉⠀⢀⣠⣤⣶⣾⣧⠀⠀⠀⠀⠀⠀⣿⣿⠀⠈⣿⣧⠘⣿⣆⣿⣿⠀⢻⣿⡄⠀⠀⣀⢻⣿⡀⠙⣿⣷⠄⠘⣿⣧⠀\n⠀⠀⠀⢀⣀⣾⣿⣷⣾⡿⠿⠛⠋⠉⠀⠀⣤⣤⠀⢸⣿⡌⣿⣿⠋⠉⠁⠀⠀⠀⠀⠀⠀⠸⣿⣇⠀⢸⣿⡇⠘⣿⣾⣿⠀⠈⣿⣷⣾⣿⡿⠮⠛⠃⠀⢀⣀⣠⣤⣾⣿⠄\n⣴⣶⣾⡿⠿⠛⠛⠉⠁⠀⠀⠀⣶⣿⣆⠀⢹⣿⣧⣸⣿⣧⠸⣿⣦⣤⣶⣆⠀⠀⠀⠀⠀⠀⢻⣿⣄⢀⣿⡇⠀⠘⣿⣿⡆⠀⠘⠛⠉⠁⣀⣠⣤⣶⣾⣿⠿⠟⠛⠉⠁⠀\n⢹⣿⣇⠀⠀⠀⣴⣿⣿⣷⡄⠀⢹⡿⣿⣆⠈⣿⣿⣿⣿⢿⣆⢻⣿⠟⠋⠉⠀⠀⠀⠀⠀⠀⠀⠻⣿⣿⡿⠃⠀⠀⠀⠀⣀⣠⣴⣶⣿⣿⠿⠟⠋⢩⣿⣿⠀⠀⠀⠀⠀⠀\n⠀⢿⣿⡀⠀⠀⣿⣟⠈⠛⠋⠀⢸⣿⠈⣿⣆⠸⣿⣻⣿⡾⣿⡌⣿⣇⣀⣤⣴⡆⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣤⣶⣾⣿⠿⣿⠛⠉⠁⠀⠀⠀⠀⢸⣿⡟⠀⠀⠀⠀⠀⠀\n⠀⠸⣿⣧⠀⠀⢻⣿⠀⣶⣾⣧⠘⣿⣷⣾⣿⣆⢿⣧⠉⠁⢻⣧⢹⣿⡿⠿⠛⠃⠀⢀⣀⣤⣴⣶⡾⡿⠟⠋⠛⠉⠁⠀⣰⠏⠀⠀⠀⠀⠀⠀⠀⣿⣿⠇⠀⠀⠀⠀⠀⠀\n⠀⠀⢹⣿⡄⠀⠈⣿⣧⠙⠙⣿⡆⣿⣟⠉⠘⣿⣾⣿⡆⠀⠀⠛⠁⢀⣀⣠⣤⣶⣿⡿⠿⠛⠋⠁⠀⠀⠀⠀⠀⠀⠀⣴⠟⠀⠀⠀⠀⠀⠀⠀⣸⣿⡏⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⣿⣷⠀⠀⠘⣿⣷⣴⣿⡏⣿⡿⠀⠀⠈⠉⢀⣀⣠⣴⣶⣿⡿⠿⠛⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⡾⠋⠀⠀⠀⠀⠀⠀⠀⣰⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠸⣿⡆⠀⠀⠈⠛⠛⠋⠀⢀⣀⣤⣴⣶⣿⡿⠿⣿⣋⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⡾⠋⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⢻⣿⡄⢀⣀⣤⣴⣶⣿⠿⠿⠛⠋⠉⠀⠀⠀⠈⠙⠳⢦⣤⣀⣀⠀⠀⠀⠀⠀⢀⣀⣠⣤⠶⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠈⣿⣿⡿⠿⠛⠙⢿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠉⠙⠛⠛⠛⠛⠉⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠿⣿⣷⣦⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣾⣿⡿⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠿⢿⣿⣷⣦⣄⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣤⣶⣿⣿⡿⠟⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠻⠿⣿⣿⣿⣿⣷⣶⣶⣶⣾⣿⣿⣿⣿⡿⠿⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠀⠉⠉⠉⠉⠉⠉⠉⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀";


                    Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (s.Length / 2)) + "}", s));
                    Console.SetCursorPosition(24, 30);
                    Console.WriteLine("Your Score:" + score);
                    Console.ReadKey();
                    Console.ResetColor();
                    //  Console.Beep(5000, 1000);
                    break;

                }


                Console.SetCursorPosition(cursorx, cursory);    // refresh X (current position) 
                Console.ForegroundColor = colors[14];
                Console.WriteLine("ᗧ", colors[14]);

                Console.ResetColor();


                Thread.Sleep(100);     // sleep 50 ms

                if (innerWall[cursorx, cursory] == 2)
                {
                    score += 10;
                    innerWall[cursorx, cursory] = 0;
                }
                else if (innerWall[cursorx, cursory] == 3)
                {
                    score += 30;
                    energy += 50;
                    innerWall[cursorx, cursory] = 0;
                }
                else if (innerWall[cursorx, cursory] == 4)
                {
                    score += 90;
                    energy += 200;
                    mine++;
                    innerWall[cursorx, cursory] = 0;
                }

                //TIme label
                Console.SetCursorPosition(70, 3);
                Console.Write("Time :" + Convert.ToInt16(time));
                time += 0.14;

                //score label
                Console.SetCursorPosition(70, 5);
                Console.Write("Score :" + Convert.ToInt16(score));

                //energy label
                Console.SetCursorPosition(70, 7);
                Console.Write("Energy :          ");
                Console.SetCursorPosition(70, 7);
                Console.Write("Energy :" + energy);

                //mine label
                Console.SetCursorPosition(70, 9);
                Console.Write("                                   ");
                Console.SetCursorPosition(70, 9);
                Console.Write("Mines :" + Convert.ToInt16(mine));

             
            }


            Console.ReadLine();

        }

        static void ShiftingWalls(int[,] innerWall)
        {
            int[] Xblock = { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50 };
            int[] Yblock = { 5, 10, 15, 20 };

            Random singBlock = new Random();
            int temp = singBlock.Next(0, 10);
            int chosenBlockX = Xblock[temp];
            temp = singBlock.Next(0, 4);
            int chosenBlockY = Yblock[temp];

            for (int i = chosenBlockY; i <= chosenBlockY + 3; i++)
            {
                for (int j = chosenBlockX; j <= chosenBlockX + 3; j++)
                {
                    innerWall[j, i] = 0;
                    Console.SetCursorPosition(j, i);
                    Console.Write(" ");
                }
            }

            Random rand = new Random();
            for (int wally = chosenBlockY; wally < chosenBlockY + 1; wally++)
            {

                for (int wall = chosenBlockX; wall < chosenBlockX + 1; wall++)
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

            //spawning points
            Random rnd = new Random();
            int rndX;
            int rndY;

            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < 20; i++)
            {
                rndX = rnd.Next(4, 55);
                rndY = rnd.Next(4, 25);


                int chosenPoint = rnd.Next(1, 11);
                //possibility of point 1
                if (innerWall[rndX, rndY] != 1 && chosenPoint <= 6)
                {
                    innerWall[rndX, rndY] = 2;
                    Console.SetCursorPosition(rndX, rndY);
                    Console.Write("1");
                }
                //possibility of point 2
                else if (innerWall[rndX, rndY] != 1 && chosenPoint <= 9)
                {
                    innerWall[rndX, rndY] = 3;
                    Console.SetCursorPosition(rndX, rndY);
                    Console.Write("2");
                }
                //possibility of point 3
                else if (innerWall[rndX, rndY] != 1 && chosenPoint == 10)
                {
                    innerWall[rndX, rndY] = 4;
                    Console.SetCursorPosition(rndX, rndY);
                    Console.Write("3");
                }
                else
                    i--;
            }

        }

        static void AddingPoints(int[,] innerWall)
        {
            //spawning points
            Random rnd = new Random();
            int rndX;
            int rndY;

            Console.ForegroundColor = ConsoleColor.Green;

            
                rndX = rnd.Next(4, 55);
                rndY = rnd.Next(4, 25);


                int chosenPoint = rnd.Next(1, 11);
                //possibility of point 1
                if (innerWall[rndX, rndY] != 1 && chosenPoint <= 6)
                {
                    innerWall[rndX, rndY] = 2;
                    Console.SetCursorPosition(rndX, rndY);
                    Console.Write("1");
                }
                //possibility of point 2
                else if (innerWall[rndX, rndY] != 1 && chosenPoint <= 9)
                {
                    innerWall[rndX, rndY] = 3;
                    Console.SetCursorPosition(rndX, rndY);
                    Console.Write("2");
                }
                //possibility of point 3
                else if (innerWall[rndX, rndY] != 1 && chosenPoint == 10)
                {
                    innerWall[rndX, rndY] = 4;
                    Console.SetCursorPosition(rndX, rndY);
                    Console.Write("3");
                }
                
                    
            

        }
            
    }

}



