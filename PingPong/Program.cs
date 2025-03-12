public enum Vector
{
    Left = 1,
    Right = 2,

    TopLeft = 11,
    BottomLeft = 12,

    TopRight = 21,
    BottomRight = 22
}

public class Buffer(Program program)
{
    public int RocketFirstX { get; set; } = program.RocketFirstX;
    public int RocketFirstY { get; set; } = program.RocketFirstY;
    public int RocketSecondX { get; set; } = program.RocketSecondX;
    public int RocketSecondY { get; set; } = program.RocketSecondY;
    public int BallX { get; set; } = program.BallX;
    public int BallY { get; set; } = program.BallY;
}

public class Program
{
    public int RocketFirstX { get; set; } = 1;
    public int RocketFirstY { get; set; } = 1;
    public int RocketSecondX { get; set; } = 0;
    public int RocketSecondY { get; set; } = 0;
    public int FirstScore { get; set; } = 0;
    public int SecondScore { get; set; } = 0;
    public int BallX { get; set; } = 0;
    public int BallY { get; set; } = 0;
    public Buffer Buffer { get; set; }

    public Vector Vector { get; set; } = Vector.TopLeft;

    const int FieldX = 100;
    const int FieldY = 25;
    const int FinalScore = 5;

    public char[,] ConsoleBuffer { get; set; } = new char[FieldX, FieldY];

    public static void Main()
    {
        var program = new Program()
        {
            BallX = FieldX / 2,
            BallY = FieldY / 2,
            RocketSecondX = FieldX - 2,
            RocketSecondY = 1
        };
        program.Buffer = new Buffer(program);
        program.FirstDraw();
        Task.Run(program.Draw);
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);

            program.CalculationRockerPosition(key.Key.ToString());
            program.DrawRocket();
        }
        while (key.Key != ConsoleKey.Escape); // по нажатию на Escape завершаем цикл
    }

    public void CalculateBallPosition()
    {
        Buffer = new Buffer(this);
        //Коллизия левой ракетки
        if (BallX == 2)
        {
            if (BallX == RocketFirstX + 1 && BallY == RocketFirstY)
            {
                Vector = Vector.TopRight;
            }
            else if (BallX == RocketFirstX + 1 && BallY == RocketFirstY + 1)
            {
                Vector = Vector.Right;
            }
            else if (BallX == RocketFirstX + 1 && BallY == RocketFirstY + 2)
            {
                Vector = Vector.BottomRight;
            }
        }
        //Коллизия левой стены
        else if (BallX <= 1)
        {
            FirstScore++;
            DrawScore();
            if (Vector == Vector.TopLeft)
            {
                Vector = Vector.TopRight;
            }
            else if (Vector == Vector.Left)
            {
                Vector = Vector.Right;
            }
            else if (Vector == Vector.BottomLeft)
            {
                Vector = Vector.BottomRight;
            }
        }

        //Коллизия правой ракетки
        else if (BallX == FieldX - 3)
        {
            if (BallX == RocketSecondX - 1 && BallY == RocketSecondY)
            {
                Vector = Vector.TopLeft;
            }
            else if (BallX == RocketSecondX - 1 && BallY == RocketSecondY + 1)
            {
                Vector = Vector.Left;
            }
            else if (BallX == RocketSecondX - 1 && BallY == RocketSecondY + 2)
            {
                Vector = Vector.BottomLeft;
            }
        }
        //Коллизия правой стены
        else if (BallX >= FieldX - 2)
        {
            SecondScore++;
            DrawScore();
            if (Vector == Vector.TopRight)
            {
                Vector = Vector.TopLeft;
            }
            else if (Vector == Vector.Right)
            {
                Vector = Vector.Left;
            }
            else if (Vector == Vector.BottomRight)
            {
                Vector = Vector.BottomLeft;
            }
        }

        //Коллизия верхней стены
        else if (BallY <= 1)
        {
            if (Vector == Vector.TopRight)
            {
                Vector = Vector.BottomRight;
            }
            else if (Vector == Vector.TopLeft)
            {
                Vector = Vector.BottomLeft;
            }
        }
        //Коллизия нижней стены
        else if (BallY >= FieldY - 2)
        {
            if (Vector == Vector.BottomRight)
            {
                Vector = Vector.TopRight;
            }
            else if (Vector == Vector.BottomLeft)
            {
                Vector = Vector.TopLeft;
            }
        }

        switch (Vector)
        {
            case Vector.Left:
                BallX--;
                break;
            case Vector.Right:
                BallX++;
                break;
            case Vector.TopLeft:
                BallX--;
                BallY--;
                break;
            case Vector.BottomLeft:
                BallX--;
                BallY++;
                break;
            case Vector.TopRight:
                BallX++;
                BallY--;
                break;
            case Vector.BottomRight:
                BallX++;
                BallY++;
                break;
            default:
                break;
        }
    }

    public void CalculationRockerPosition(string key)
    {
        Buffer = new Buffer(this);
        switch (key)
        {
            case "W":
                if (RocketFirstY > 1 && RocketFirstY <= FieldY - 4)
                    RocketFirstY--;
                break;
            case "S":
                if (RocketFirstY >= 1 && RocketFirstY < FieldY - 4)
                    RocketFirstY++;
                break;
            case "O":
                if (RocketSecondY > 1 && RocketSecondY <= FieldY - 4)
                    RocketSecondY--;
                break;
            case "L":
                if (RocketSecondY >= 1 && RocketSecondY < FieldY - 4)
                    RocketSecondY++;
                break;
            default:
                break;
        }
    }

    public void FirstDraw()
    {
        Console.Clear();

        for (int y = 0; y < FieldY; y++)
        {
            for (int x = 0; x < FieldX; x++)
            {
                //Местоположение левой ракетки
                if (RocketFirstX == x && RocketFirstY == y)
                {
                    Console.Write("I");
                }
                else if (RocketFirstX == x && RocketFirstY + 1 == y)
                {
                    Console.Write("I");
                }
                else if (RocketFirstX == x && RocketFirstY + 2 == y)
                {
                    Console.Write("I");
                }
                //Местоположение правой ракетки
                else if (RocketSecondX == x && RocketSecondY == y)
                {
                    Console.Write("I");
                }
                else if (RocketSecondX == x && RocketSecondY + 1 == y)
                {
                    Console.Write("I");
                }
                else if (RocketSecondX == x && RocketSecondY + 2 == y)
                {
                    Console.Write("I");
                }
                //Заполенение шара
                else if (BallX == x && BallY == y)
                {
                    Console.Write("O");
                }
                //Заполнение поля
                else if (y == 0)
                {
                    if (x == FieldX - 1)
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
                else if (y == FieldY - 1)
                {
                    if (x == FieldX - 1)
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
                else if (x == FieldX - 1)
                {
                    Console.WriteLine("|");
                }
                else
                {
                    Console.Write(" ");
                }
            }
        }
        Console.WriteLine($@"Счет: {FirstScore} : {SecondScore}");
        Console.WriteLine("Управление левой ракеткой: W - вверх, S - вниз");
        Console.WriteLine("Управление правой ракеткой: O - вверх, L - вниз");
        Console.WriteLine("Чтобы выйти нажмите Esc");
        Buffer = new Buffer(this);
    }

    public void Draw()
    {
        while (true)
        {
            CalculateBallPosition();
            DrawBall();

            if (FirstScore >= FinalScore)
            {
                Console.WriteLine(" Победил правый игрок!");
                break;
            }
            else if (SecondScore >= FinalScore)
            {
                Console.WriteLine(" Победил левый игрок!");
                break;
            }
            Thread.Sleep(100);
        }
    }

    public void DrawScore()
    {
        Console.SetCursorPosition(6, FieldY);
        Console.Write(FirstScore);
        Console.SetCursorPosition(10, FieldY);
        Console.Write(SecondScore);
    }

    public void DrawBall()
    {
        Console.SetCursorPosition(Buffer.BallX, Buffer.BallY);
        Console.Write(" ");
        Console.SetCursorPosition(BallX, BallY);
        Console.Write("O");
    }

    public void DrawRocket()
    {
        Console.SetCursorPosition(Buffer.RocketFirstX, Buffer.RocketFirstY);
        Console.Write(" ");
        Console.SetCursorPosition(Buffer.RocketFirstX, Buffer.RocketFirstY + 1);
        Console.Write(" ");
        Console.SetCursorPosition(Buffer.RocketFirstX, Buffer.RocketFirstY + 2);
        Console.Write(" ");
        Console.SetCursorPosition(RocketFirstX, RocketFirstY);
        Console.Write("I");
        Console.SetCursorPosition(RocketFirstX, RocketFirstY + 1);
        Console.Write("I");
        Console.SetCursorPosition(RocketFirstX, RocketFirstY + 2);
        Console.Write("I");

        Console.SetCursorPosition(Buffer.RocketSecondX, Buffer.RocketSecondY);
        Console.Write(" ");
        Console.SetCursorPosition(Buffer.RocketSecondX, Buffer.RocketSecondY + 1);
        Console.Write(" ");
        Console.SetCursorPosition(Buffer.RocketSecondX, Buffer.RocketSecondY + 2);
        Console.Write(" ");
        Console.SetCursorPosition(RocketSecondX, RocketSecondY);
        Console.Write("I");
        Console.SetCursorPosition(RocketSecondX, RocketSecondY + 1);
        Console.Write("I");
        Console.SetCursorPosition(RocketSecondX, RocketSecondY + 2);
        Console.Write("I");
    }
}