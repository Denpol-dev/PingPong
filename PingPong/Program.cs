public enum Vector
{
    Left = 1,
    Right = 2,

    TopLeft = 11,
    BottomLeft = 12,

    TopRight = 21,
    BottomRight = 22
}

public class Program
{
    public int ballX { get; set; }
    public int ballY { get; set; }
    public int rocketFirstX { get; set; } = 1;
    public int rocketFirstY { get; set; } = 1;
    public int rocketSecondX { get; set; } = 0;
    public int rocketSecondY { get; set; } = 0;
    public int firstScore { get; set; } = 0;
    public int secondScore { get; set; } = 0;

    public Vector vector { get; set; } = Vector.TopLeft;

    const int fieldX = 80;
    const int fieldY = 25;
    const int finalScore = 5;

    public static void Main()
    {
        var program = new Program
        {
            ballX = fieldX / 2,
            ballY = fieldY / 2,
            rocketSecondX = fieldX - 2,
            rocketSecondY = 1
        };
        Task.Run(program.Draw);
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);

            program.CalculationRockerPosition(key.Key.ToString());
        }
        while (key.Key != ConsoleKey.Escape); // по нажатию на Escape завершаем цикл
    }

    public void CalculateBallPosition()
    {
        //Коллизия левой ракетки
        if (ballX == 2)
        {
            if (ballX == rocketFirstX + 1 && ballY == rocketFirstY)
            {
                vector = Vector.TopRight;
            }
            else if (ballX == rocketFirstX + 1 && ballY == rocketFirstY + 1)
            {
                vector = Vector.Right;
            }
            else if (ballX == rocketFirstX + 1 && ballY == rocketFirstY + 2)
            {
                vector = Vector.BottomRight;
            }
        }
        //Коллизия левой стены
        else if (ballX == 1)
        {
            firstScore++;
            if (vector == Vector.TopLeft)
            {
                vector = Vector.BottomRight;
            }
            else if (vector == Vector.Left)
            {
                vector = Vector.Right;
            }
            else if (vector == Vector.BottomLeft)
            {
                vector = Vector.TopRight;
            }
        }

        //Коллизия правой ракетки
        else if (ballX == fieldX - 3)
        {
            if (ballX == rocketSecondX - 1 && ballY == rocketSecondY)
            {
                vector = Vector.TopLeft;
            }
            else if (ballX == rocketSecondX - 1 && ballY == rocketSecondY + 1)
            {
                vector = Vector.Left;
            }
            else if (ballX == rocketSecondX - 1 && ballY == rocketSecondY + 2)
            {
                vector = Vector.BottomLeft;
            }
        }
        //Коллизия правой стены
        else if (ballX == fieldX - 2)
        {
            secondScore++;
            if (vector == Vector.TopRight)
            {
                vector = Vector.BottomLeft;
            }
            else if (vector == Vector.Right)
            {
                vector = Vector.Left;
            }
            else if (vector == Vector.BottomRight)
            {
                vector = Vector.TopLeft;
            }
        }

        //Коллизия верхней стены
        else if (ballY == 1)
        {
            if (vector == Vector.TopRight)
            {
                vector = Vector.BottomRight;
            }
            else if (vector == Vector.TopLeft)
            {
                vector = Vector.BottomLeft;
            }
        }
        //Коллизия нижней стены
        else if (ballY == fieldY - 2)
        {
            if (vector == Vector.BottomRight)
            {
                vector = Vector.TopRight;
            }
            else if (vector == Vector.BottomLeft)
            {
                vector = Vector.TopLeft;
            }
        }

        switch (vector)
        {
            case Vector.Left:
                ballX--;
                break;
            case Vector.Right:
                ballX++;
                break;
            case Vector.TopLeft:
                ballX--;
                ballY--;
                break;
            case Vector.BottomLeft:
                ballX--;
                ballY++;
                break;
            case Vector.TopRight:
                ballX++;
                ballY--;
                break;
            case Vector.BottomRight:
                ballX++;
                ballY++;
                break;
            default:
                break;
        }
    }

    public void CalculationRockerPosition(string key)
    {
        switch (key)
        {
            case "W":
                if (rocketFirstY > 1 && rocketFirstY <= fieldY - 4)
                    rocketFirstY--;
                break;
            case "S":
                if (rocketFirstY >= 1 && rocketFirstY < fieldY - 4)
                    rocketFirstY++;
                break;
            case "O":
                if (rocketSecondY > 1 && rocketSecondY <= fieldY - 4)
                    rocketSecondY--;
                break;
            case "L":
                if (rocketSecondY >= 1 && rocketSecondY < fieldY - 4)
                    rocketSecondY++;
                break;
            default:
                break;
        }
    }

    public void Draw()
    {
        while (true)
        {
            CalculateBallPosition();
            Console.Clear();
            Console.WriteLine($@"Счет: {firstScore} : {secondScore}");
            Console.WriteLine("Управление левой ракеткой: W - вверх, S - вниз");
            Console.WriteLine("Управление правой ракеткой: O - вверх, L - вниз");
            Console.WriteLine("Чтобы выйти нажмите Esc");

            for (int y = 0; y < fieldY; y++)
            {
                for (int x = 0; x < fieldX; x++)
                {
                    //Местоположение левой ракетки
                    if (rocketFirstX == x && rocketFirstY == y)
                    {
                        Console.Write("I");
                    }
                    else if (rocketFirstX == x && rocketFirstY + 1 == y)
                    {
                        Console.Write("I");
                    }
                    else if (rocketFirstX == x && rocketFirstY + 2 == y)
                    {
                        Console.Write("I");
                    }
                    //Местоположение правой ракетки
                    else if (rocketSecondX == x && rocketSecondY == y)
                    {
                        Console.Write("I");
                    }
                    else if (rocketSecondX == x && rocketSecondY + 1 == y)
                    {
                        Console.Write("I");
                    }
                    else if (rocketSecondX == x && rocketSecondY + 2 == y)
                    {
                        Console.Write("I");
                    }
                    //Заполенение шара
                    else if (ballX == x && ballY == y)
                    {
                        Console.Write("O");
                    }
                    //Заполнение поля
                    else if (y == 0)
                    {
                        if (x == fieldX - 1)
                        {
                            Console.WriteLine("\\");
                        }
                        else if (x == 0)
                        {
                            Console.Write("/");
                        }
                        else
                        {
                            Console.Write("=");
                        }
                    }
                    else if (y == fieldY - 1)
                    {
                        if (x == fieldX - 1)
                        {
                            Console.WriteLine("/");
                        }
                        else if (x == 0)
                        {
                            Console.Write("\\");
                        }
                        else
                        {
                            Console.Write("=");
                        }
                    }
                    else if (x == 0)
                    {
                        Console.Write("|");
                    }
                    else if (x == fieldX - 1)
                    {
                        Console.WriteLine("|");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
            }
            if (firstScore >= finalScore)
            {
                Console.WriteLine("Победил правый игрок!");
                break;
            }
            else if (secondScore >= finalScore)
            {
                Console.WriteLine("Победил левый игрок!");
                break;
            }
            Thread.Sleep(100);
        }
    }
}