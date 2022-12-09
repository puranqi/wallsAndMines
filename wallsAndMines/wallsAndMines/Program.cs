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

            double time = 0;


            //Console.SetCursorPosition(3, 3);

            int cursorx = 13, cursory = 13;   // position of cursor 

            ConsoleKeyInfo cki;               // required for readkey 

            int ax = 13, ay = 20;    // position of A 

            int adir = 1;            // direction of A:   1:rigth   -1:left 



            // --- Static screen parts 

            Console.SetCursorPosition(3, 3);

            Console.WriteLine("#####################################################");


            for (int i = 0; i < 20; i++)
            {

                Console.SetCursorPosition(3, 3 + i + 1);


                Console.WriteLine("#                                                   #");

            }

            Console.SetCursorPosition(3, 23);

            Console.WriteLine("#####################################################");


            // --- Main game loop 

            while (true)
            {

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

                    if (cki.Key == ConsoleKey.DownArrow && cursory < 22)
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
            }



            Console.ReadLine();

        }

    }

}