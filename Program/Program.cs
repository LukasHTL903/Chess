using Chess;

namespace Program;
public class Program
{
    public static void Main()
    {
        Game game = new();
        int[] position = [1, 1];
        int[] enemypos = [0, 6];
        Figure Knight = new("Kn", position);
        Figure Enemy = new("T", enemypos);

        game.SetFigure(Knight, "      b     ,        2   ");
        game.SetFigure(Enemy, "a, 7");

        Console.WriteLine(game);
        for(int count = 0; count < 3; count ++){
            Console.WriteLine($"Where do you want to go with your {nameof(Knight)}");

            game.Move(Knight, Console.ReadLine());

            Console.WriteLine(game);
            Console.WriteLine($"x: {Knight.Position[1]}, y:{Knight.Position[0]}");
            Console.WriteLine($"Enemy is Alive: {Enemy.Alive}");
        }
    }
}
