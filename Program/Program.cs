using Chess;

namespace Program;
public class Program
{
    public static void Main()
    {
        Game game = new();
        int[] position = [1, 1];
        int[] enemypos = [0, 6];
        Figure Pawn = new("P", position);
        Figure Enemy = new("T", enemypos);

        game.SetFigure(Pawn, "      b     ,        8   ");
        game.SetFigure(Enemy, "a, 7");

        Console.WriteLine(game);
        for(int count = 0; count < 3; count ++){
            Console.WriteLine($"Where do you want to go with your {Pawn.Name}");

            game.Move(Pawn, Console.ReadLine());

            Console.WriteLine(game);
            Console.WriteLine($"x: {Pawn.Position[1]}, y:{Pawn.Position[0]}");
            Console.WriteLine($"Enemy is Alive: {Enemy.Alive}");
        }
    }
}
