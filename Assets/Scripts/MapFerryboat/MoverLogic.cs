using System;
using System.Collections.Generic;
using UnityEngine;

public class MoverLogic : MonoBehaviour
{
    private const int Width = 15;
    private const int Height = 15;

    [SerializeField] private GameObject _prefab;

    private Point[,] _map = new Point[Height, Width];
    private TileHelper[,] _tiles = new TileHelper[Height, Width];

    private Queue<TileHelper> _carStartPoints = new Queue<TileHelper>();
    private Queue<TileHelper> _carFinishPoints = new Queue<TileHelper>();

    private Point _start;
    private Point _end;

    private int _start_X;
    private int _start_Y;
    private int _end_X;
    private int _end_Y;

    public int CountFinishPlace => _carFinishPoints.Count;
    public int CountStartPlace => _carStartPoints.Count;

    void Start()
    {
        InitMap();
        AddItemOnMap();
    }

    public void SetStart(TileHelper tile)
    {
        _start_X = tile.cord_x;
        _start_Y = tile.cord_y;
    }

    public void SetEnd(TileHelper tile)
    {
        _end_X = tile.cord_x;
        _end_Y = tile.cord_y;
        SetStartAndEnd(_start_X, _start_Y, _end_X, _end_Y);
        FindPath();
    }

    public bool CheckObstacle(int x, int y)
    {
        _tiles[x, y].sprite.color = Color.magenta;
        return _map[x, y].isObstacle;
    }

    public List<TileHelper> GetPath()
    {
        List<TileHelper> path = new List<TileHelper>();
        Point temp = _end.parent;

        if (_end.parent == null)
        {
            Debug.Log("No way");
            return null;
        }

        while (temp != _start)
        {
            if (path.Count > 100)
            {
                Debug.Log("No way");
                return null;
            }

            path.Add(_tiles[temp.X, temp.Y]);
            temp = temp.parent;
        }

        return path;
    }

    public TileHelper GetStartCarPosition()
    {

        TileHelper tile = _carStartPoints.Dequeue();
        TileHelper cashtile = tile;
        _carStartPoints.Enqueue(tile);

        return cashtile;
    }

    public TileHelper GetFinihCarPosition()
    {
        return _carFinishPoints.Dequeue();
    }

    public void AddObstacle(int x, int y)
    {
        _map[x, y].isObstacle = true;
        _tiles[x, y].sprite.color = Color.black;
    }

    public void RemoveObstacle(int x, int y)
    {
        _map[x, y].isObstacle = false;
        _tiles[x, y].sprite.color = Color.white;
    }

    public TileHelper GetTile(int x, int y)
    {
        return _tiles[x, y];
    }

    private void InitMap()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                _tiles[i, j] = Instantiate(_prefab, new Vector3(i, 0, j), Quaternion.identity).GetComponent<TileHelper>();
                _tiles[i, j].cord_x = i;
                _tiles[i, j].cord_y = j;
                _map[i, j] = new Point(i, j);
            }
        }
    }

    private void AddItemOnMap()
    {
        //     AddObstacle(2, 4);

        for (int i = 0; i < 3; i++)
            AddVoid(6, i);

        for (int i = 0; i < Height; i++)
            AddVoid(0, i);

        for (int i = 1; i < 9; i++)
            AddVoid(4, i);

        for (int i = 4; i <= 6; i++)
            AddVoid(6, i);

        for (int i = 7; i < Height; i++)
            for (int j = 0; j < Height; j++)
                AddVoid(i, j);

        AddWalls(1, 9, 2);
        AddWalls(4, 9, 2);

        for (int i = 1; i <= 4; i++)
            for (int j = 11; j < Height; j++)
                AddCarFinishPoint(i, j);

        for (int i = 1; i <= 3; i++)
            for (int j = 0; j < _carFinishPoints.Count / 3; j++)
                AddCarStartPoint(i, j);
    }

    private void AddCarStartPoint(int x, int y)
    {
        _map[x, y].isObstacle = true;
        _tiles[x, y].sprite.color = Color.blue;
        _carStartPoints.Enqueue(_tiles[x, y]);
    }

    private void AddCarFinishPoint(int x, int y)
    {
        _carFinishPoints.Enqueue(_tiles[x, y]);
        _tiles[x, y].sprite.color = Color.yellow;
    }

    private void AddVoid(int x, int y)//Add void
    {
        _map[x, y].isObstacle = true;
        _tiles[x, y].sprite.gameObject.SetActive(false);
    }

    private void AddWalls(int x, int y, int walls_id) //Add Walls
    {
        _map[x, y].walls[walls_id] = true;
        _tiles[x, y].gameObject.GetComponent<TileHelper>().SetWalls(walls_id);
    }

    private void SetStartAndEnd(int startX, int startY, int endX, int endY)//Set start and end points
    {
        _start = _map[startX, startY];
        _tiles[startX, startY].sprite.color = Color.green;
        _end = _map[endX, endY];
        _tiles[endX, endY].sprite.color = Color.red;
    }

    private void FindPath()
    {
        List<Point> openList = new List<Point>();
        List<Point> closeList = new List<Point>();
        openList.Add(_start);

        while (openList.Count > 0)//Continue as long as there are elements in the open list
        {
            Point point = GetMinFOfList(openList);//Select the point with the smallest F value in the open set
            openList.Remove(point);
            closeList.Add(point);
            List<Point> SurroundPoints = GetSurroundPoint(point.X, point.Y);

            foreach (Point p in closeList)//Delete the points that are already closing the list in the surrounding points
            {
                if (SurroundPoints.Contains(p))
                {
                    SurroundPoints.Remove(p);
                }
            }

            foreach (Point p in SurroundPoints)//Traverse the surrounding points
            {
                if (openList.Contains(p))//The surrounding points are already in the open list
                {
                    //Recalculate G, if it is smaller than the original G, change the father of this point
                    int newG = 1 + point.G;
                    if (newG < p.G)
                    {
                        p.SetParent(point, newG);
                    }
                }
                else
                {
                    //Set father and F and join the open list
                    p.parent = point;
                    GetF(p);
                    openList.Add(p);
                }
            }
            if (openList.Contains(_end))//As long as the end appears, it ends
            {
                break;
            }
        }
    }


    private List<Point> GetSurroundPoint(int x, int y)//Get a point around a point
    {
        List<Point> PointList = new List<Point>();

        if (x > 0 && !_map[x - 1, y].isObstacle && !_map[x - 1, y].walls[1] && !_map[x, y].walls[0])
        {
            PointList.Add(_map[x - 1, y]);
            //  tiles[x - 1, y].sprite.color = Color.yellow;
        }
        if (y > 0 && !_map[x, y - 1].isObstacle && !_map[x, y - 1].walls[2] && !_map[x, y].walls[3])
        {
            PointList.Add(_map[x, y - 1]);
            //  tiles[x, y - 1].sprite.color = Color.yellow;
        }
        if (x < Height - 1 && !_map[x + 1, y].isObstacle && !_map[x + 1, y].walls[0] && !_map[x, y].walls[1])
        {
            PointList.Add(_map[x + 1, y]);
            // tiles[x + 1, y].sprite.color = Color.yellow;
        }
        if (y < Width - 1 && !_map[x, y + 1].isObstacle && !_map[x, y + 1].walls[3] && !_map[x, y].walls[2])
        {
            PointList.Add(_map[x, y + 1]);
            //tiles[x, y + 1].sprite.color = Color.yellow;
        }
        return PointList;
    }


    private void GetF(Point point)//Calculate the F value of a certain point
    {
        int G = 0;
        int H = Mathf.Abs(_end.X - point.X) + Mathf.Abs(_end.Y - point.Y);

        if (point.parent != null)
        {
            G = 1 + point.parent.G;
        }

        int F = H + G;
        point.H = H;
        point.G = G;
        point.F = F;
    }


    private Point GetMinFOfList(List<Point> list)//Get the point with the smallest F value in a set
    {
        int min = int.MaxValue;
        Point point = null;
        foreach (Point p in list)
        {
            if (p.F < min)
            {
                min = p.F;
                point = p;
            }
        }
        return point;
    }


}
