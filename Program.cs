using System;

namespace Dz6
{
    class Game
    {
        public static void Show (int[,] matrix)
        {
            string tab = "~~~"; matrix.GetLength(1);
            for (int i = 0; i < (matrix.GetLength(1)) * 8; i++)
            {
                tab += "~";
            }
            Console.Write("\t" + tab + "\n");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write("\t");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    
                    if (matrix[i, j] > 0)
                    {
                        Console.Write("| ");
                        ShowPlayer(matrix[i,j]);
                        Console.Write("\t ");
                    }
                    else
                    {
                        if (i == matrix.GetLength(0) - 1 && j == matrix.GetLength(1) - 1)
                        {
                            Console.Write("| ");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("♠♠♠♠♠♠");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.Write("| \t ");
                        }
                    }
                }
                Console.Write(" |");
                Console.Write("\n\t" + tab + "\n");
            }
        }
        public static int[,] Init(int[,] matrix, out int x, out int y) 
        {
            Random r = new Random();
            bool initChech = true;
            do
            {
                x = r.Next(matrix.GetLength(0));
                y = r.Next(matrix.GetLength(1));
                if (matrix[x, y] == 0 && !( x == matrix.GetLength(0)-1 && y == matrix.GetLength(1) - 1) && !(x == 0 && y == 0))
                {
                    matrix[x, y] = 2;
                    initChech = false;
                }
            }
            while (initChech);
            return matrix;
        }
        public static void ShowPlayer(int i) 
        {
            switch (i) 
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(Player.Symbol); 
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(Enemy.Symbol);
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                default:
                    Console.Write(" ");
                    return;
            }
        }
        public static bool PlayerWin(int[,] matrix) 
        {
            if (matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1] == Player.Value)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
    class Enemy 
    {
        public static int Value { get { return value; } }
        private static int value = 2;
        public int x = 0;
        public int y = 0;
        public static string Symbol { get { return symbol; } }
        private static string symbol = "☺";
        public Enemy( int x, int y) 
        {
            this.x = x;
            this.y = y;
        }
        public int[,] Move(int[,] matrix)
        {
            int way = 0;
            bool moveCheck = true;
            Random r = new Random();
            if (this.CanMove(matrix))
            {
                do
                {
                    way = r.Next(4);
                    switch (way)
                    {
                        case 0:
                            if (x > 0)
                            {
                                if (matrix[x - 1, y] == 0)
                                {
                                    matrix[x - 1, y] = 2;
                                    matrix[x, y] = 0;
                                    x--;
                                    moveCheck = false;
                                }
                            }
                            break;
                        case 1:
                            if (x < matrix.GetLength(0) - 1 && !( x+1 == matrix.GetLength(0) - 1 && y == matrix.GetLength(1)-1))
                            {
                                if (matrix[x + 1, y] == 0 )
                                {
                                    matrix[x + 1, y] = 2;
                                    matrix[x, y] = 0;
                                    x++;
                                    moveCheck = false;
                                }
                            }
                            break;
                        case 2:
                            if (y > 0)
                            {
                                if (matrix[x, y - 1] == 0)
                                {
                                    matrix[x, y - 1] = 2;
                                    matrix[x, y] = 0;
                                    y--;
                                    moveCheck = false;
                                }
                            }
                            break;
                        case 3:
                            if (y < matrix.GetLength(1) - 1 && !(x == matrix.GetLength(0) - 1 && y + 1 == matrix.GetLength(1) - 1))
                            {
                                if (matrix[x, y + 1] == 0)
                                {
                                    matrix[x, y + 1] = 2;
                                    matrix[x, y] = 0;
                                    y++;
                                    moveCheck = false;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                while (moveCheck);
            }
            return matrix;
        }
        public bool CanMove(int[,] matrix)
        {
            bool canMove = false;
            if (x - 1 >= 0)
            {
                if (matrix[x - 1, y] == 0)
                {
                    canMove = true;
                }
            }
            if (x + 1 <= matrix.GetLength(0) - 1 && !(x + 1 == matrix.GetLength(0) - 1 && y == matrix.GetLength(1) - 1))
            {
                if (matrix[x + 1, y] == 0)
                {
                    canMove = true;
                }
            }
            if (y - 1 >= 0)
            {
                if (matrix[x, y - 1] == 0)
                {
                    canMove = true;
                }
            }
            if (y + 1 <= matrix.GetLength(1) - 1 && !(x == matrix.GetLength(0) - 1 && y + 1  == matrix.GetLength(1) - 1))
            {
                if (matrix[x, y + 1] == 0)
                {
                    canMove = true;
                }
            }
            return canMove;
        }
    }
    class Player 
    {
        public static int Value { get { return value; } }
        private static int value = 1;
        public int x = 0;
        public int y = 0;
        public static string Symbol { get { return symbol; } }
        private static string symbol = "☻";
        public int[,] Init(int[,] matrix)
        {
            matrix[0, 0] = 1;
            return matrix;
        }

        public bool CanMove(int[,] matrix)
        {
            bool canMove = false;
            if (x - 1 >= 0)
            {
                if (matrix[x - 1, y] == 0)
                {
                    canMove = true;
                }
            }
            if (x + 1 <= matrix.GetLength(0) - 1)
            {
                if (matrix[x + 1, y] == 0)
                {
                    canMove = true;
                }
            }
            if (y - 1 >= 0)
            {
                if (matrix[x, y - 1] == 0)
                {
                    canMove = true;
                }
            }
            if (y + 1 <= matrix.GetLength(1) - 1)
            {
                if (matrix[x, y + 1] == 0)
                {
                    canMove = true;
                }
            }
            return canMove;
        }
        public int[,] Move(int[,] matrix, out ConsoleKeyInfo key)
        {
            key = new ConsoleKeyInfo('a',ConsoleKey.A,false,false,false);
            int way = 0;
            bool moveCheck = true;
            if (this.CanMove(matrix))
            {
                do
                {
                    bool moved = false;
                    do
                    {
                        key = Console.ReadKey();
                        switch (key.Key)
                        {
                            case ConsoleKey.UpArrow:
                                way = 0;
                                moved = true;
                                break;
                            case ConsoleKey.DownArrow:
                                way = 1;
                                moved = true;
                                break;
                            case ConsoleKey.LeftArrow:
                                way = 2;
                                moved = true;
                                break;
                            case ConsoleKey.RightArrow:
                                way = 3;
                                moved = true;
                                break;
                            case ConsoleKey.Escape:
                                return matrix;
                            default:
                                continue;
                        }
                    } while (!moved);

                    switch (way)
                    {
                        case 0:
                            if (x > 0)
                            {
                                if (matrix[x - 1, y] == 0)
                                {
                                    matrix[x - 1, y] = 1;
                                    matrix[x, y] = 0;
                                    x--;
                                    moveCheck = false;
                                }
                            }
                            break;
                        case 1:
                            if (x < matrix.GetLength(0) - 1)
                            {
                                if (matrix[x + 1, y] == 0)
                                {
                                    matrix[x + 1, y] = 1;
                                    matrix[x, y] = 0;
                                    x++;
                                    moveCheck = false;
                                }
                            }
                            break;
                        case 2:
                            if (y > 0)
                            {
                                if (matrix[x, y - 1] == 0)
                                {
                                    matrix[x, y - 1] = 1;
                                    matrix[x, y] = 0;
                                    y--;
                                    moveCheck = false;
                                }
                            }
                            break;
                        case 3:
                            if (y < matrix.GetLength(1) - 1)
                            {
                                if (matrix[x, y + 1] == 0)
                                {
                                    matrix[x, y + 1] = 1;
                                    matrix[x, y] = 0;
                                    y++;
                                    moveCheck = false;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                while (moveCheck);
            }
            return matrix;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int lvl = 0;
            int n = 10;
            int k = 7;
            bool checkMove;
            bool restart = true;
            ConsoleKeyInfo enterKey = new ConsoleKeyInfo('a',ConsoleKey.A,false,false,false);
            do
            {
                checkMove = true;
                if (!restart)
                {
                    lvl = 1;
                    k = 10;
                    restart = true;
                }
                else
                {
                    lvl++;
                    k = k + 3;
                }

                Enemy[] enemys = new Enemy[k];
                Player player = new Player();
                int[,] array = new int[n, n];
                
                array = player.Init(array);

                for (int i = 0; i < k; i++)
                {
                    array = Game.Init(array, out int x, out int y);
                    enemys[i] = new Enemy(x, y);
                }

                do
                {
                    Console.Clear();
                    Console.WriteLine("\t Your goal is too go to the end of the field ;");
                    Console.WriteLine("\t Your level is " + lvl+" ; Number of enemys :"+ k);
                    Console.WriteLine("\t Controls: Arrows, Esc - Exit ;\n" );
                    Game.Show(array);

                    if (Game.PlayerWin(array))
                    {
                        checkMove = false;
                    }
                    if (!player.CanMove(array))
                    {
                        restart = false;
                    }

                    array = player.Move(array, out enterKey);
                    for (int i = 0; i < k; i++)
                    {
                        array = enemys[i].Move(array);
                    }
                    if (enterKey.Key == ConsoleKey.Escape)
                    {
                        checkMove = false;
                        restart = false;
                    }

                }
                while (checkMove && restart);

                if (restart == false) 
                {
                    Console.Clear();
                    Game.Show(array);
                    Console.WriteLine("\t You are out of ways !");
                    Console.WriteLine("\t Your lvl is "+ lvl );
                    Console.WriteLine("\t Press Enter to continue ");
                    Console.ReadLine();
                }
            } while (enterKey.Key != ConsoleKey.Escape);
        }
    }
}
