using System.Net.Http.Headers;

namespace Chess;

public class Chess
{
    private Figure?[,] field = new Figure?[8,8];
    private Figure Turm = new("♖");
    private Figure Springer = new("");
    private Figure Laufer = new("");
    private Figure Dame = new("");
    private Figure Konig = new("");
    private Figure Bauer = new("");


    public bool Move(Figure F, string destination, int[] position){
        int[] xy = TransformString(destination);
        int x = xy[0];
        int y = xy[1];

        if(F.Name == "♖"){
            if((x == position[0] && y != position[1]) || (y == position[1] && x != position[0])){
                if(field != null){
                    if(field[x, y] == null){
                        field[position[0], position[1]] = null;
                        field[x, y] = F;
                        return true;
                    } else {
                        field[position[0], position[1]] = null;
                        field[x, y].Alive = false;
                        field[x, y] = F;
                        return true;
                    }
                }
            }
        }
        
        return false;
    }

    private static int[] TransformString(string _destination){
        string destination = _destination.Trim();
        int[] position = new int[2];

        if(destination[0] == 'a' || destination[0] == 'A'){
            position[0] = 0;
        } else if(destination[0] == 'b' || destination[0] == 'B'){
            position[0] = 1;
        } else if(destination[0] == 'c' || destination[0] == 'C'){
            position[0] = 2;
        } else if(destination[0] == 'd' || destination[0] == 'D'){
            position[0] = 3;
        } else if(destination[0] == 'e' || destination[0] == 'E'){
            position[0] = 4;
        } else if(destination[0] == 'f' || destination[0] == 'F'){
            position[0] = 5;
        } else if(destination[0] == 'g' || destination[0] == 'G'){
            position[0] = 6;
        } else if(destination[0] == 'h' || destination[0] == 'H'){
            position[0] = 7;
        } else {
            throw new ArgumentException("Only letters from a-h");
        }

        if(!int.TryParse(destination[1].ToString(), out position[1])){
            throw new ArgumentException("Second input has to be a number");
        }

        return position;
    }
    
}
public class Figure
{
    private string _name;
    private bool _firstmove;
    private bool _alive;

    public Figure(string n){
        _name = n;
        _firstmove = false;
    }

    public bool Firstmove {
        get{return _firstmove;}
        set{
            if(value == false){
                throw new ArgumentException("You can't set the firstmove to false");
            } else {
                _firstmove = value;
            }
        }
    }

    public string Name {
        get{return _name;}
    }

    public bool Alive {
        get{return _alive;}
        set{
            if(value == true){
                throw new ArgumentException("You can't make a figure alive again.");
            } else {
                _alive = value;
            }
        }
    }
}

