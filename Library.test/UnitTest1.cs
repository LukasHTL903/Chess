using Chess;
using Xunit;
namespace Chess.test;

public class FigureTests
{
    [Fact]
    public void Figure_IsAlive_ByDefault()
    {
        var fig = new Figure(Figure.FigureType.Rook, new int[] { 0, 0 }, true);

        Assert.True(fig.Alive);
    }

    [Fact]
    public void Figure_CanBeKilled()
    {
        var fig = new Figure(Figure.FigureType.Rook, new int[] { 0, 0 }, true);

        fig.Alive = false;

        Assert.False(fig.Alive);
    }

    [Fact]
    public void Figure_CannotBeRevived()
    {
        var fig = new Figure(Figure.FigureType.Rook, new int[] { 0, 0 }, true);

        Assert.Throws<ArgumentException>(() => fig.Alive = true);
    }

    [Fact]
    public void Position_Set_Valid()
    {
        var fig = new Figure(Figure.FigureType.Rook, new int[] { 0, 0 }, true);

        fig.Position = new int[] { 3, 4 };

        Assert.Equal(3, fig.Position[0]);
        Assert.Equal(4, fig.Position[1]);
    }

    [Fact]
    public void Figure_isWhite()
    {
        var fig = new Figure(Figure.FigureType.Rook, new int[] { 0, 0 }, true);

        Assert.True(fig.IsWhite);
    }
    [Fact]
    public void Position_Set_Invalid_Throws()
    {
        var fig = new Figure(Figure.FigureType.Rook, new int[] { 0, 0 }, true);

        Assert.Throws<ArgumentException>(() => fig.Position = new int[] { 8, 0 });
    }
}

public class GameSetFigureTests
{
    [Fact]
    public void SetFigure_PlacesFigureCorrectly()
    {
        var game = new Game();
        var fig = new Figure(Figure.FigureType.Rook, new int[] { 0, 0 }, true);

        game.SetFigure(fig, "a1");

        Assert.Equal(0, fig.Position[0]);
        Assert.Equal(0, fig.Position[1]);
    }
}

public class RookTests
{
    [Fact]
    public void Rook_CanMove_Straight()
    {
        var game = new Game();
        var rook = new Figure(Figure.FigureType.Rook, new int[] { 0, 0 }, true);

        game.SetFigure(rook, "a1");

        var result = game.Move(rook, "a5");

        Assert.True(result);
        Assert.Equal(4, rook.Position[1]);
    }

    [Fact]
    public void Rook_CannotMove_Diagonal()
    {
        var game = new Game();
        var rook = new Figure(Figure.FigureType.Rook, new int[] { 0, 0 }, true);

        game.SetFigure(rook, "a1");

        var result = game.Move(rook, "c3");

        Assert.False(result);
    }

    [Fact]
    public void Rook_Blocked_ByPiece()
    {
        var game = new Game();
        var rook = new Figure(Figure.FigureType.Rook, new int[] { 0, 0 }, true);
        var blocker = new Figure(Figure.FigureType.Pawn, new int[] { 0, 0 }, true);

        game.SetFigure(rook, "a1");
        game.SetFigure(blocker, "a3");

        var result = game.Move(rook, "a5");

        Assert.False(result);
    }
}

public class BishopTests
{
    [Fact]
    public void Bishop_CanMove_Diagonal()
    {
        var game = new Game();
        var bishop = new Figure(Figure.FigureType.Bishop, new int[] { 0, 0 }, true);

        game.SetFigure(bishop, "c1");

        var result = game.Move(bishop, "h6");

        Assert.True(result);
    }

    [Fact]
    public void Bishop_CannotMove_Straight()
    {
        var game = new Game();
        var bishop = new Figure(Figure.FigureType.Bishop, new int[] { 0, 0 }, true);

        game.SetFigure(bishop, "c1");

        var result = game.Move(bishop, "c5");

        Assert.False(result);
    }
}


public class KnightTests
{
    [Fact]
    public void Knight_CanMove_LShape()
    {
        var game = new Game();
        var knight = new Figure(Figure.FigureType.Knight, new int[] { 0, 0 }, true);

        game.SetFigure(knight, "b1");

        var result = game.Move(knight, "c3");

        Assert.True(result);
    }

    [Fact]
    public void Knight_CannotMove_Invalid()
    {
        var game = new Game();
        var knight = new Figure(Figure.FigureType.Knight, new int[] { 0, 0 }, true);

        game.SetFigure(knight, "b1");

        var result = game.Move(knight, "b3");

        Assert.False(result);
    }
}


public class KingTests
{
    [Fact]
    public void King_CanMove_OneStep()
    {
        var game = new Game();
        var king = new Figure(Figure.FigureType.King, new int[] { 0, 0 }, true);

        game.SetFigure(king, "e4");

        var result = game.Move(king, "e5");

        Assert.True(result);
    }

    [Fact]
    public void King_CannotMove_TwoSteps()
    {
        var game = new Game();
        var king = new Figure(Figure.FigureType.King, new int[] { 0, 0 }, true);

        game.SetFigure(king, "e4");

        var result = game.Move(king, "e6");

        Assert.False(result);
    }
}


public class PawnTests
{
    [Fact]
    public void Pawn_CanMove_Forward()
    {
        var game = new Game();
        var pawn = new Figure(Figure.FigureType.Pawn, new int[] { 0, 0 }, false);

        game.SetFigure(pawn, "a2");

        var result = game.Move(pawn, "a3");

        Assert.True(result);
    }

    [Fact]
    public void Pawn_CanMove_TwoSteps_FirstMove()
    {
        var game = new Game();
        var pawn = new Figure(Figure.FigureType.Pawn, new int[] { 0, 0 }, false);

        game.SetFigure(pawn, "a2");

        var result = game.Move(pawn, "a4");

        Assert.True(result);
    }

    [Fact]
    public void Pawn_CannotMove_Backwards()
    {
        var game = new Game();
        var pawn = new Figure(Figure.FigureType.Pawn, new int[] { 0, 0 }, false);

        game.SetFigure(pawn, "a2");

        var result = game.Move(pawn, "a1");

        Assert.False(result);
    }
}


public class CaptureTests
{
    [Fact]
    public void Piece_CanCapture_Enemy()
    {
        var game = new Game();

        var rook = new Figure(Figure.FigureType.Rook, new int[] { 0, 0 }, true);
        var enemy = new Figure(Figure.FigureType.Pawn, new int[] { 0, 0 }, false);

        game.SetFigure(rook, "a1");
        game.SetFigure(enemy, "a5");

        var result = game.Move(rook, "a5");

        Assert.True(result);
        Assert.False(enemy.Alive);
    }

    [Fact]
    public void Piece_CantCapture_Teammate()
    {
        var game = new Game();

        var rook = new Figure(Figure.FigureType.Rook, new int[] { 0, 0 }, true);
        var enemy = new Figure(Figure.FigureType.Pawn, new int[] { 0, 0 }, true);

        game.SetFigure(rook, "a1");
        game.SetFigure(enemy, "a5");

        var result = game.Move(rook, "a5");

        Assert.False(result);
        Assert.True(enemy.Alive);
    }
}
