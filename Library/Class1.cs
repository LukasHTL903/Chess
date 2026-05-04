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

    private bool SetFigure(Figure F, int x, int y, int xCurrent, int yCurrent)
    {
        if (field[y, x] == null)                           //if no Enemy on new spot
        {
            field[yCurrent, xCurrent] = null;
            field[y, x] = F;

            //set new Position
            F.Position[1] = y;
            F.Position[0] = x;
            return true;
        }
        else                                                //if Enemy on new spot
        {
            if(field[y, x].IsWhite != field[yCurrent, xCurrent].IsWhite){
                field[yCurrent, xCurrent] = null;
                field[y, x].Alive = false;
                field[y, x] = F;

                //set new Position
                F.Position[1] = y;
                F.Position[0] = x;
                return true;
            }
        }
        
        return false;
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

        if (F.Name == "R" || F.Name == "r")
        {  
            if (RookMove(x, y, xCurrent, yCurrent))
            {
                if(SetFigure(F, x, y, xCurrent, yCurrent)){
                    return true;
                } else {
                    return false;
                }
            }
        }
        else if (F.Name == "K" || F.Name == "k")
        {
            if (KingMove(x, y, xCurrent, yCurrent))
            {
                if(SetFigure(F, x, y, xCurrent, yCurrent)){
                    return true;
                } else {
                    return false;
                }
            }
        }
        else if (F.Name == "P" || F.Name == "p") 
        {
            if(PawnMove(F, x, y, xCurrent, yCurrent)){
                if(SetFigure(F, x, y, xCurrent, yCurrent)){
                    return true;
                } else {
                    return false;
                }
            }
            
        } else if(F.Name == "Kn" || F.Name == "kn")
        {
            if(KnightMove(x, y, xCurrent, yCurrent))
            {
                if(SetFigure(F, x, y, xCurrent, yCurrent)){
                    return true;
                } else {
                    return false;
                }
                
            }

        } else if (F.Name == "B" || F.Name == "b")
        {
            if(BishopMove(x, y, xCurrent, yCurrent)){
                if(SetFigure(F, x, y, xCurrent, yCurrent)){
                    return true;
                } else {
                    return false;
                }
            }

        } else if(F.Name == "Q" || F.Name == "q")
        {
            if(BishopMove(x, y, xCurrent, yCurrent) || RookMove(x, y, xCurrent, yCurrent)){
                if(SetFigure(F, x, y, xCurrent, yCurrent)){
                    return true;
                } else {
                    return false;
                }
            }
        }

        return false;
    }

    private bool PawnMove(Figure F, int x, int y, int xCurrent, int yCurrent)
    {
        bool inRange = false;
        
        if(F.Name == "p")
        {
            if(yCurrent + 1 == y && xCurrent == x){
                inRange = true;
            }

            if(field[y, x] != null && yCurrent + 1 == y && Math.Abs(xCurrent - x) == 1){
                inRange = true; 
            }

            if(yCurrent + 2 == y && xCurrent == x && yCurrent == 1 && field[y - 1, x] == null)
            {
                inRange = true;
            }
        }

        if(F.Name == "P")
        {
            if(yCurrent - 1 == y && xCurrent == x){
                inRange = true;
            }

            if(field[y, x] != null && yCurrent - 1 == y && Math.Abs(xCurrent - x) == 1){
                inRange = true; 
            }

            if(yCurrent - 2 == y && xCurrent == x && yCurrent == 6 && field[y + 1, x] == null)
            {
                inRange = true;
            }
        }

        return inRange;
    }

    private bool KingMove(int x, int y, int xCurrent, int yCurrent){
        bool inRange = false;

        int xdistance = Math.Abs(x - xCurrent);
        int ydistance = Math.Abs(y - yCurrent);

        if(xdistance <= 1 && ydistance <= 1 && (xdistance + ydistance) > 0)
        {
            inRange = true;
        }
        
        return inRange;
    }

    private bool KnightMove(int x, int y, int xCurrent, int yCurrent)
    {
    bool inRange = false;

        int xdistance = Math.Abs(x - xCurrent);
        int ydistance = Math.Abs(y - yCurrent);

        if((xdistance == 2 && ydistance == 1) || (ydistance == 2 && xdistance == 1))
        {
            inRange = true;
        }

        return inRange;
    }

    private bool BishopMove(int x, int y, int xCurrent, int yCurrent)
    {
    bool inRange = false;

        if (x > xCurrent && y > yCurrent){
            for (int count = 1; count < 8; count++)
            {
                // downright Range?
                if (xCurrent + count > 7 || yCurrent + count > 7)
                {
                    break;
                }
                if (xCurrent + count == x && yCurrent + count == y)
                {
                    inRange = true;
                    break;
                }
                if (field[yCurrent + count, xCurrent + count] != null)
                {
                    break;
                }
            }
        }

        if (x > xCurrent && y < yCurrent)
        {
            for (int count = 1; count < 8; count++)
            {
                // upright in Range?
                if (yCurrent - count < 0 || xCurrent + count > 7)
                {
                    break;
                }
                if (yCurrent - count == y && xCurrent + count == x)
                {
                    inRange = true;
                    break;
                }
                if (field[yCurrent - count, xCurrent + count] != null)
                {
                    break;
                }
            }
        }
        
        if (x < xCurrent && y > yCurrent)
        {
            for (int count = 1; count < 8; count++)
            {
                // downleft in Range?
                if (yCurrent + count > 7 || xCurrent - count < 0)
                {
                    break;
                }
                if (yCurrent + count == y && xCurrent - count == x)
                {
                    inRange = true;
                    break;
                }
                if (field[yCurrent + count, xCurrent - count] != null)
                {
                    break;
                }
            }
        }

        if (x < xCurrent && y < yCurrent)
        {
            for (int count = 1; count < 8; count++)
            {
                // upleft in Range?
                if (yCurrent - count < 0 || xCurrent - count < 0)
                {
                    break;
                }
                if (yCurrent - count == y && xCurrent - count == x)
                {
                    inRange = true;
                    break;
                }
                if (field[yCurrent - count, xCurrent - count] != null)
                {
                    break;
                }
            }
        }

        return inRange;
    }

    private bool RookMove(int x, int y, int xCurrent, int yCurrent)
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

        return inRange;
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
    private bool _alive;
    private int[] _position;
    private bool _isWhite;

    public Figure(string n, int[] position, bool isWhite)
    {
        _name = n;
        _alive = true;
        _position = position;
        _isWhite = isWhite;
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

    public bool IsWhite
    {
        get {return _isWhite;}
    }

    public override string ToString()
    {
        return Name;
    }
}

