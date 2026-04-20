using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;

namespace Chess;

public class Game
{
    private Figure?[,] field = new Figure[8, 8];

    public void SetFigure(Figure F, string destination)
    {
        int x = TransformString(destination)[1];
        int y = TransformString(destination)[0];

        field[y, x] = F;

        F.Position[0] = x;
        F.Position[1] = y;
    }

    private void SetFigure(Figure F, int x, int y, int xCurrent, int yCurrent)
    {
        if (field[y, x] == null)                            //if no Enemy on new spot
        {
            field[yCurrent, xCurrent] = null;
            field[y, x] = F;

            //set new Position
            F.Position[1] = y;
            F.Position[0] = x;
        }
        else                                                //if Enemy on new spot
        {
            field[yCurrent, xCurrent] = null;
            field[y, x].Alive = false;
            field[y, x] = F;

            //set new Position
            F.Position[1] = y;
            F.Position[0] = x;
        }
    }
    public bool Move(Figure F, string destination)
    {
        if (field == null)
        {
            return false;
        }

        int x = TransformString(destination)[1];
        int y = TransformString(destination)[0];

        int xCurrent = F.Position[0];
        int yCurrent = F.Position[1];

        if (F.Name == "T" || F.Name == "t")
        {
            bool inRange = false;

            if (x > xCurrent && y == yCurrent)
            {
                for (int count = 1; count < 8; count++)
                {
                    // right in Range?
                    if (xCurrent + count > 7)
                    {
                        break;
                    }
                    if (xCurrent + count == x)
                    {
                        inRange = true;
                        break;
                    }
                    if (field[yCurrent, xCurrent + count] != null)
                    {
                        break;
                    }
                }
            }


            if (y > yCurrent && x == xCurrent)
            {
                for (int count = 1; count < 8; count++)
                {
                    // down in Range?
                    if (yCurrent + count > 7)
                    {
                        break;
                    }
                    if (yCurrent + count == y)
                    {
                        inRange = true;
                        break;
                    }
                    if (field[yCurrent + count, xCurrent] != null)
                    {
                        break;
                    }
                }
            }

            if (y < yCurrent && x == xCurrent)
            {
                for (int count = 1; count < 8; count++)
                {
                    // up in Range?
                    if (yCurrent - count < 0)
                    {
                        break;
                    }
                    if (yCurrent - count == y)
                    {
                        inRange = true;
                        break;
                    }
                    if (field[yCurrent - count, xCurrent] != null)
                    {
                        break;
                    }
                }
            }

            if (x < xCurrent && y == yCurrent)
            {
                for (int count = 1; count < 8; count++)
                {
                    // left in Range?
                    if (xCurrent - count < 0)
                    {
                        break;
                    }
                    if (xCurrent - count == x)
                    {
                        inRange = true;
                        break;
                    }
                    if (field[yCurrent, xCurrent - count] != null)
                    {
                        break;
                    }
                }
            }

            if (inRange)
            {
                SetFigure(F, x, y, xCurrent, yCurrent);
                return true;
            }
        }
        else if (F.Name == "K" || F.Name == "k")
        {
            bool inRange = false;

            int xdistance = Math.Abs(x - xCurrent);
            int ydistance = Math.Abs(y - yCurrent);

            if(xdistance <= 1 && yCurrent <= 1 && xdistance + ydistance > 0)
            {
                inRange = true;
            }

            if (inRange)
            {
                SetFigure(F, x, y, xCurrent, yCurrent);
                return true;
            }
        }

        return false;
    }

    private static int[] TransformString(string _destination)
    {
        string destination = _destination.Trim().Replace(" ", "").Replace(",", "");
        int[] position = new int[2];

        if (destination[0] == 'a' || destination[0] == 'A')
        {
            position[1] = 0;
        }
        else if (destination[0] == 'b' || destination[0] == 'B')
        {
            position[1] = 1;
        }
        else if (destination[0] == 'c' || destination[0] == 'C')
        {
            position[1] = 2;
        }
        else if (destination[0] == 'd' || destination[0] == 'D')
        {
            position[1] = 3;
        }
        else if (destination[0] == 'e' || destination[0] == 'E')
        {
            position[1] = 4;
        }
        else if (destination[0] == 'f' || destination[0] == 'F')
        {
            position[1] = 5;
        }
        else if (destination[0] == 'g' || destination[0] == 'G')
        {
            position[1] = 6;
        }
        else if (destination[0] == 'h' || destination[0] == 'H')
        {
            position[1] = 7;
        }
        else
        {
            throw new ArgumentException("Only letters from 'a'-'h' or 'A'-'H'");
        }

        if (!int.TryParse(destination[1].ToString(), out position[0]))
        {
            throw new ArgumentException("Second input has to be a number");
        }

        position[0] -= 1;
        if (!(position[0] >= 0 && position[0] <= 7))
        {
            throw new ArgumentException("Second input has to be between 1-8");
        }

        return position;
    }


    public override string ToString()
    {
        string output = $"Chess Field: {Environment.NewLine}";
        bool cycle = false;

        output += $"  a   b   c   d   e   f   g   h{Environment.NewLine}";

        for (int count = 0; count < 8; count++)
        {
            output += $"+---+---+---+---+---+---+---+---+{Environment.NewLine}";
            cycle = !cycle;

            for (int i = 0; i < 8; i++)
            {
                if (i == 0)
                {
                    output += "| ";
                }

                if (field[count, i] != null)
                {
                    output += $"{field[count, i]} | ";
                }
                else
                {
                    if (cycle == false)
                    {
                        if (i % 2 == 0)
                        {
                            output += $"# | ";
                        }
                        else
                        {
                            output += $"  | ";
                        }
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {
                            output += $"  | ";
                        }
                        else
                        {
                            output += $"# | ";
                        }
                    }
                }
            }
            output += $"  {count + 1}{Environment.NewLine}";
        }
        output += $"+---+---+---+---+---+---+---+---+{Environment.NewLine}";
        return output;
    }
}

public class Figure
{
    private string _name;
    private bool _firstmove;
    private bool _alive;
    private int[] _position;

    public Figure(string n, int[] position)
    {
        _name = n;
        _firstmove = false;
        _alive = true;
        _position = position;
    }

    public bool Firstmove
    {
        get { return _firstmove; }
        set
        {
            if (value == false)
            {
                throw new ArgumentException("You can't set the firstmove to false");
            }
            else
            {
                _firstmove = value;
            }
        }
    }

    public string Name
    {
        get { return _name; }
    }

    public bool Alive
    {
        get { return _alive; }
        set
        {
            if (value == true)
            {
                throw new ArgumentException("You can't make a figure alive again.");
            }
            else
            {
                _alive = value;
            }
        }
    }

    public int[] Position
    {
        get { return _position; }
        set
        {
            if (value.Length == 2 && value[0] >= 0 && value[0] <= 7 && value[1] >= 0 && value[1] <= 7)
            {
                _position = value;
            }
            else
            {
                throw new ArgumentException($"{nameof(value)} has to be two ints long and every value has to be between 0-7");
            }
        }
    }

    public override string ToString()
    {
        return Name;
    }
}

