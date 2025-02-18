using System;

public class Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;

        Parent = null;
        IsObstacle = false;
        Walls = new bool[] { false, false, false, false };
    }

    public int X { get; private set; }
    public int Y { get; private set; }
    public int CountTileToFinish { get; private set; }
    public int CountTileToFinishVertical { get; private set; }
    public int CountTileToFinishHorizontal { get; private set; }
    public Point Parent { get; private set; }
    public bool IsObstacle { get; private set; }
    public bool[] Walls { get; private set; }

    public void SetValues(Point parent, int g)
    {
        SetParent(parent);
        CountTileToFinishVertical = g;
        CountTileToFinish = CountTileToFinishVertical + CountTileToFinishHorizontal;
    }

    public void SetParent(Point parent)
    {
        this.Parent = parent;
    }

    public void Change(int countTileToFinishHorizontal, int countTileToFinishVertical, int countTileToFinish)
    {
        CountTileToFinishHorizontal = countTileToFinishHorizontal;
        CountTileToFinishVertical = countTileToFinishVertical;
        CountTileToFinish = countTileToFinish;
    }

    public void ChangeObstacle(bool isActive)
    {
        IsObstacle = isActive;
    }

    public void AddWalls(int walls_id)
    {
        Walls[walls_id] = true;
    }
}
