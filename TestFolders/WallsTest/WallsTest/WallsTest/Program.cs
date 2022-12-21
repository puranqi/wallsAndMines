using System;

class Program
{
    static void Main(string[] args)
    {
        Random wallRand = new Random();

        string HorWall = "####";
        string VerWAll = "#\n#\n#\n#";

        //duvara carpma kosulu icin deneme
        //int[,] wallLocation = null ;
        //wallLocation[0, i] =  { wallX, wallY};


        int wallX = 5;
        int wallY = 5;


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


        // --- Inner wall loop
        CreatingWalls();
        

        Console.ReadKey();
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