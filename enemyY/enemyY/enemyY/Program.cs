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
            char[,] innerWall_mbyın_tutan = new char[60, 26]; // +4 because of where they start
            int[,] innerWall = new int[60, 26];
            Random wallRand = new Random();
            int counter = 0;
            //int wallHitCounter = 0;

            //duvara carpma kosulu icin deneme
            //int[,] wallLocation = null ;
            //wallLocation[0, i] =  { wallX, wallY};
            bool mine_yerlestirme = true;
            int mine = 4; //0 lanacak 
            bool aliveY = true;


            int moveCounter = 0;
            int energy = 500;
            int score = 0;
            double time = 0;




            //Console.SetCursorPosition(3, 3);

            int cursorx = 26, cursory = 24;   // position of cursor 

            ConsoleKeyInfo cki;               // required for readkey 

            



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

                    if (cki.Key == ConsoleKey.Spacebar)
                    {
                        if (mine > 0)
                        {
                            if (cursorx != 54 && innerWall_mbyın_tutan[cursorx + 1, cursory] != '#')
                            { Console.SetCursorPosition(cursorx + 1, cursory); innerWall_mbyın_tutan[cursorx + 1, cursory] = '+'; }

                            else if (cursorx != 4 && innerWall_mbyın_tutan[cursorx - 1, cursory] != '#')
                            { Console.SetCursorPosition(cursorx - 1, cursory); innerWall_mbyın_tutan[cursorx - 1, cursory] = '+'; }

                            else if (cursory != 24 && innerWall_mbyın_tutan[cursorx, cursory + 1] != '#')
                            { Console.SetCursorPosition(cursorx, cursory + 1); innerWall_mbyın_tutan[cursorx, cursory + 1] = '+'; }

                            else if (cursory != 4 && innerWall_mbyın_tutan[cursorx, cursory - 1] != '#')
                            { Console.SetCursorPosition(cursorx, cursory - 1); innerWall_mbyın_tutan[cursorx, cursory - 1] = '+'; }

                            else { mine_yerlestirme = false; }
                            if (mine_yerlestirme)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.WriteLine("+");
                                Console.ResetColor();

                                mine--;

                            }
                            mine_yerlestirme = true;
                        }

                        else
                        {
                            Console.SetCursorPosition(70, 10);
                            Console.ForegroundColor = colors[4];
                            Console.WriteLine("You don't have any mine!", colors[4]); // yerini byarla!  mine: da çıksın
                            Console.ResetColor();
                        }


                    }



                    if (energy > 0)
                    {
                        if (cki.Key == ConsoleKey.RightArrow && cursorx < 54 && innerWall[cursorx + 1, cursory] != 1)
                        {   // key and boundary control 

                            Console.SetCursorPosition(cursorx, cursory);           // delete X (old position) 

                            Console.WriteLine(" ");

                            cursorx++;

                            if (energy > 0)
                                energy--;

                        }

                        if (cki.Key == ConsoleKey.LeftArrow && cursorx > 4 && innerWall[cursorx - 1, cursory] != 1)
                        {

                            Console.SetCursorPosition(cursorx, cursory);

                            Console.WriteLine(" ");

                            cursorx--;

                            if (energy > 0)
                                energy--;


                        }

                        if (cki.Key == ConsoleKey.UpArrow && cursory > 4 && innerWall[cursorx, cursory - 1] != 1)
                        {

                            Console.SetCursorPosition(cursorx, cursory);

                            Console.WriteLine(" ");

                            cursory--;

                            if (energy > 0)
                                energy--;


                        }
                        //sets the limit of X plbyer
                        if (cki.Key == ConsoleKey.DownArrow && cursory < 24 && innerWall[cursorx, cursory + 1] != 1)
                        {

                            Console.SetCursorPosition(cursorx, cursory);

                            Console.WriteLine(" ");

                            cursory++;

                            if (energy > 0)
                                energy--;


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

                        }

                        if (cki.Key == ConsoleKey.LeftArrow && cursorx > 4 && innerWall[cursorx - 1, cursory] != 1)
                        {

                            Console.SetCursorPosition(cursorx, cursory);

                            Console.WriteLine(" ");

                            cursorx--;

                            if (energy > 0)
                                energy--;


                        }

                        if (cki.Key == ConsoleKey.UpArrow && cursory > 4 && innerWall[cursorx, cursory - 1] != 1)
                        {

                            Console.SetCursorPosition(cursorx, cursory);

                            Console.WriteLine(" ");

                            cursory--;

                            if (energy > 0)
                                energy--;


                        }
                        //sets the limit of X plbyer
                        if (cki.Key == ConsoleKey.DownArrow && cursory < 24 && innerWall[cursorx, cursory + 1] != 1)
                        {

                            Console.SetCursorPosition(cursorx, cursory);

                            Console.WriteLine(" ");

                            cursory++;

                            if (energy > 0)
                                energy--;


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
                }

                int bx = 15, by = 4;    // position of A

                int bdirX = 1;            //Xbxis direction of A:   1:rigth   -1:left

                int bdirY = 1;            //Ybxis direction of A:   1:Down   -1:Up

                if (bdirX == 1 && bx >= 54) bdirX = -1;    // change direction at boundaries 

                if (bdirY == -1 && by <= 4) bdirY = 1;


                Console.SetCursorPosition(bx, by);    // delete old A 

                if (aliveY == true)
                {

                    if (cursory == by && cursorx == bx)
                    {
                        Console.SetCursorPosition(cursorx, cursory);
                        Console.Write("X");
                        Console.SetCursorPosition(50, 30);
                        Console.ForegroundColor = colors[4];
                        Console.Clear();
                        string s = "                      ⢀⣀⣠⣴⣶⣶⣶⣶⣾⣿⣿⣶⣶⣶⣤⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣶⣿⣿⡿⠿⠛⠛⠋⠉⠉⠉⠉⠉⠉⠛⠛⠻⢿⣿⣿⣷⣦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⣿⣿⠿⠛⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠙⠻⣿⣿⣶⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⡿⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠻⣿⣿⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣾⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣿⣿⣦⡀⠀⢀⣠⣤⣶⡄⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⡶⠶⠛⠛⠋⠉⠉⠛⠛⠳⠶⢤⣄⡀⠀⠀⠀⠀⠀⠀⠀⣀⣨⣿⣿⣿⡿⠿⠛⠛⢿⣿⡀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⢠⣾⣿⠟⠀⠀⠀⠀⠀⠀⠀⠀⣀⣴⠞⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⢶⣄⣠⣤⣶⣶⡿⠿⠟⠛⠉⠁⢀⣀⠀⠀⠘⣿⣇⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⢠⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⣠⡾⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣴⣶⣾⣿⠿⠟⠋⠉⠀⢀⣀⣴⡴⣾⣿⣿⣿⣷⡄⠀⢹⣿⡄⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⢀⣾⣿⠇⠀⠀⠀⠀⠀⠀⠀⣴⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣤⣶⣾⣿⠿⠛⠋⠉⠀⠀⠀⢠⣶⣾⣿⣿⠿⠟⠃⢹⣿⡄⠈⣿⣧⠀⠀⣿⣧⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⣼⣿⡏⠀⠀⠀⠀⠀⠀⠀⣼⠋⠀⠀⠀⢀⣀⣤⣤⣶⣾⡿⠿⠛⠛⠉⠁⢀⣤⣤⣄⠸⣿⣆⠀⠸⣿⡇⢿⣧⠀⢀⣠⠈⣿⣷⣶⣿⡏⠀⠀⠸⣿⣇⠀⠀\n⠀⠀⠀⠀⠀⢠⣿⣿⠁⠀⠀⠀⠀⠀⠀⣸⣏⣠⣤⣶⣿⡿⠿⠟⠛⠉⠀⠀⠀⠀⠀⠀⠀⣾⣿⠻⣿⣧⠹⣿⣆⠀⣿⡇⠸⣿⣿⣿⠿⠇⠸⣿⡏⢻⣿⣆⠀⠀⢿⣿⡀⠀\n⠀⠀⠀⠀⠀⢸⣿⡟⠀⠀⣀⣠⣴⣶⣿⣿⠿⠟⠋⠉⠀⢀⣠⣤⣶⣾⣧⠀⠀⠀⠀⠀⠀⣿⣿⠀⠈⣿⣧⠘⣿⣆⣿⣿⠀⢻⣿⡄⠀⠀⣀⢻⣿⡀⠙⣿⣷⠄⠘⣿⣧⠀\n⠀⠀⠀⢀⣀⣾⣿⣷⣾⡿⠿⠛⠋⠉⠀⠀⣤⣤⠀⢸⣿⡌⣿⣿⠋⠉⠁⠀⠀⠀⠀⠀⠀⠸⣿⣇⠀⢸⣿⡇⠘⣿⣾⣿⠀⠈⣿⣷⣾⣿⡿⠮⠛⠃⠀⢀⣀⣠⣤⣾⣿⠄\n⣴⣶⣾⡿⠿⠛⠛⠉⠁⠀⠀⠀⣶⣿⣆⠀⢹⣿⣧⣸⣿⣧⠸⣿⣦⣤⣶⣆⠀⠀⠀⠀⠀⠀⢻⣿⣄⢀⣿⡇⠀⠘⣿⣿⡆⠀⠘⠛⠉⠁⣀⣠⣤⣶⣾⣿⠿⠟⠛⠉⠁⠀\n⢹⣿⣇⠀⠀⠀⣴⣿⣿⣷⡄⠀⢹⡿⣿⣆⠈⣿⣿⣿⣿⢿⣆⢻⣿⠟⠋⠉⠀⠀⠀⠀⠀⠀⠀⠻⣿⣿⡿⠃⠀⠀⠀⠀⣀⣠⣴⣶⣿⣿⠿⠟⠋⢩⣿⣿⠀⠀⠀⠀⠀⠀\n⠀⢿⣿⡀⠀⠀⣿⣟⠈⠛⠋⠀⢸⣿⠈⣿⣆⠸⣿⣻⣿⡾⣿⡌⣿⣇⣀⣤⣴⡆⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣤⣶⣾⣿⠿⣿⠛⠉⠁⠀⠀⠀⠀⢸⣿⡟⠀⠀⠀⠀⠀⠀\n⠀⠸⣿⣧⠀⠀⢻⣿⠀⣶⣾⣧⠘⣿⣷⣾⣿⣆⢿⣧⠉⠁⢻⣧⢹⣿⡿⠿⠛⠃⠀⢀⣀⣤⣴⣶⡾⡿⠟⠋⠛⠉⠁⠀⣰⠏⠀⠀⠀⠀⠀⠀⠀⣿⣿⠇⠀⠀⠀⠀⠀⠀\n⠀⠀⢹⣿⡄⠀⠈⣿⣧⠙⠙⣿⡆⣿⣟⠉⠘⣿⣾⣿⡆⠀⠀⠛⠁⢀⣀⣠⣤⣶⣿⡿⠿⠛⠋⠁⠀⠀⠀⠀⠀⠀⠀⣴⠟⠀⠀⠀⠀⠀⠀⠀⣸⣿⡏⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⣿⣷⠀⠀⠘⣿⣷⣴⣿⡏⣿⡿⠀⠀⠈⠉⢀⣀⣠⣴⣶⣿⡿⠿⠛⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⡾⠋⠀⠀⠀⠀⠀⠀⠀⣰⣿⡟⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠸⣿⡆⠀⠀⠈⠛⠛⠋⠀⢀⣀⣤⣴⣶⣿⡿⠿⣿⣋⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⡾⠋⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⢻⣿⡄⢀⣀⣤⣴⣶⣿⠿⠿⠛⠋⠉⠀⠀⠀⠈⠙⠳⢦⣤⣀⣀⠀⠀⠀⠀⠀⢀⣀⣠⣤⠶⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⡿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠈⣿⣿⡿⠿⠛⠙⢿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠉⠙⠛⠛⠛⠛⠉⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠿⣿⣷⣦⣄⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣾⣿⡿⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠛⠿⢿⣿⣷⣦⣄⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣤⣶⣿⣿⡿⠟⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠻⠿⣿⣿⣿⣿⣷⣶⣶⣶⣾⣿⣿⣿⣿⡿⠿⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠀⠉⠉⠉⠉⠉⠉⠉⠉⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀";


                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (s.Length / 2)) + "}", s));
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

                    if (by == cursory && (innerWall[bx+bdirX, by] != 1))
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
                    else if (by == cursory && (innerWall[bx + bdirX, by] == 1) && (innerWall[bx , by + 1] != 1 || innerWall[bx, by+bdirY] == 1))
                    {
                        if (bdirY == 1)
                            bdirY = -1;
                        else
                            bdirY = 1;

                        if (innerWall[bx + bdirX, by] == 1)
                        {

                            if (innerWall[bx , by+bdirY] != 1)
                            {
                                by = by + bdirY;
                            }

                            else if (innerWall[bx , by+bdirY] == 1 && innerWall[bx+bdirX, by] != 1)
                            {
                                bx += bdirX;
                            }
                        }
                        if (innerWall[bx+bdirX, by ] == 1)
                        {
                            if (innerWall[bx, by+bdirY] != 1)
                            {
                                by = by + bdirY;
                            }

                            else if (innerWall[bx, by+bdirY] == 1 && innerWall[bx+bdirX, by ] != 1)
                            {
                                bx += bdirX;
                            }
                        }
                        if (innerWall[bx+bdirX, by] == 1)
                        {
                            if (innerWall[bx, by +bdirY] != 1)
                            {
                                by = by + bdirY;
                            }

                            else if (innerWall[bx , by +bdirY] == 1 && innerWall[bx+bdirX, by ] != 1)
                            {
                                bx += bdirX;
                            }
                        }
                        if (innerWall[bx+bdirX, by ] == 1)
                        {
                            if (innerWall[bx , by +bdirY] != 1)
                            {
                                by = by + bdirY;
                            }

                            else if (innerWall[bx , by+bdirY] == 1 && innerWall[bx+bdirX,by] != 1)
                            {
                                bx += bdirX;
                            }
                        }
                        bx += bdirX;
                    }
                    else if (innerWall[bx , by+bdirY] != 1)
                    {
                        by = by + bdirY;
                    }

                    else if (innerWall[bx , by+bdirY] == 1 && innerWall[bx+bdirX, by] != 1)
                    {
                        bx += bdirX;
                    }

                    if (time % 5 == 0)
                    {
                        bx = wallRand.Next(3, 56);
                        by = wallRand.Next(4, 22);
                    }

                    Console.Write(" ");
                    Console.SetCursorPosition(bx, by);    // refresh A (current position) 
                    Console.ForegroundColor = colors[11];
                    Console.WriteLine("ᗣ", colors[11]);
                    Console.ResetColor();
                }
                else Console.WriteLine("");


                if (innerWall_mbyın_tutan[bx, by] == '+' && aliveY)
                {
                    innerWall[bx, by] = '\0';
                    Console.SetCursorPosition(bx, by);
                    Console.Write(" ");
                    aliveY = false;
                    score += 300;
                }

                Console.SetCursorPosition(80, 20);
                Console.Write("wall index-1:" + innerWall[bx-1, by ]);
                Console.SetCursorPosition(80, 21);
                Console.Write("wall index+1:" + innerWall[bx+1, by ]);


                //Console.WriteLine("\nOriginal colors restored...");
                //Console.ReadLine();




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

    }

}



