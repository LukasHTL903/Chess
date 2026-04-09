using Chess;

namespace Program;
public class Program{
    public static void Main(){
        Game game = new();
        int[] position = new int[2];
        position[0] = 2;
        position[1] = 2;
        Figure Turm = new("T", position);

        game.SetFigure(Turm, "      c     ,        5   ");

        Console.WriteLine(game);
    }
}
