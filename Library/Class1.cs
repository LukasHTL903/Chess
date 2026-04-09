using System.Net.Http.Headers;

namespace Chess;

public class Chess
{
    private Figure[,]? field = new Figure[8,8];
    private Figure Turm = new("♖");
    public bool Move(Figure F, string destination){
        if(F.name == "♖"){
            
        }
        
        return false;
    }
    
}
public class Figure
{
    private string _name;
    private bool _firstmove;
    
    public Figure(string n){
        _name = n;
        _firstmove = false;
    }

    public bool firstmove {
        get{return _firstmove;}
        set{
            if(value == false){
                throw new ArgumentException("You can't set the firstmove to false");
            } else {
                _firstmove = value;
            }
        }
    }

    public string name {
        get{return _name;}
    }
}

