using System;
using System.Collections.Generic;
using UnityEngine;

public class MapLogic : MonoBehaviour
{

    private const int MaxStep = 50;

    [SerializeField] private GameObject _prefabMapTile;

    private Point[,] _map;
    private TileHelper[,] _tiles;

    private Queue<TileHelper> _carStartPoints = new Queue<TileHelper>();
    private List<TileHelper> _carFinishPoints = new List<TileHelper>();

    private List<TileHelper> _carSpesialStartPoints = new List<TileHelper>();
    private Queue<TileHelper> _carSpesialFinishPoints = new Queue<TileHelper>();

    private Point _start;
    private Point _end;

    private int _width;
    private int _height;

    private int _start_X;
    private int _start_Y;
    private int _end_X;
    private int _end_Y;

    public int CountFinishPlace => _carFinishPoints.Count;
    public int CountStartPlace => _carStartPoints.Count;
    public int CountStartSpesialPlace => _carSpesialStartPoints.Count;
    public int CountFinishSpesialPlace => _carSpesialFinishPoints.Count;

    public void Init(int width, int height)
    {
        _width = width;
        _height = height;

        _map = new Point[_height, _width];
        _tiles = new TileHelper[_height, _width];

        Activate();
        InitMap();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
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
            if (path.Count > MaxStep)
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
        TileHelper tile = _carFinishPoints[UnityEngine.Random.Range(0, _carFinishPoints.Count)];
        _carFinishPoints.Remove(tile);

        return tile;
    }

    public TileHelper GetStartSpesialCarPosition()
    {
        TileHelper tile = _carSpesialStartPoints[UnityEngine.Random.Range(0, _carSpesialStartPoints.Count)];

        return tile;
    }

    public TileHelper GetSpesialFinihCarPosition()
    {
        TileHelper tile = _carSpesialFinishPoints.Dequeue();

        return tile;
    }

    public void AddObstacle(int x, int y)
    {
        _map[x, y].isObstacle = true;
    }

    public void RemoveObstacle(int x, int y)
    {
        _map[x, y].isObstacle = false;
    }

    public TileHelper GetTile(int x, int y)
    {
        return _tiles[x, y];
    }

    private void InitMap()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                _tiles[i, j] = Instantiate(_prefabMapTile, new Vector3(i, 0, j), Quaternion.identity).GetComponent<TileHelper>();
                _tiles[i, j].transform.SetParent(transform, false);
                _tiles[i, j].cord_x = i;
                _tiles[i, j].cord_y = j;
                _map[i, j] = new Point(i, j);
            }
        }
    }

    //public void AddItemOnMap(Dictionary<string, List<int>> itemOnMap)
    //{

    //    if (itemOnMap.TryGetValue("Void", out List<int> list))
    //    {
    //        AddVoid(list[0], list[1]);
    //    }

    //    if (itemOnMap.TryGetValue("Wall", out List<int> listWall))
    //    {
    //        AddWalls(listWall[0], listWall[1], listWall[2]);
    //    }

    //    if (itemOnMap.TryGetValue("Finish", out List<int> listFinish))
    //    {
    //        AddCarFinishPoint(listWall[0], listWall[1]);
    //    }

    //    if (itemOnMap.TryGetValue("Start", out List<int> listStart))
    //    {
    //        AddCarStartPoint(listWall[0], listWall[1]);
    //    }

    //    if (itemOnMap.TryGetValue("SpesialFinish", out List<int> listSpesialFinish))
    //    {
    //        AddSpesialCarFinishPoint(listSpesialFinish[0], listSpesialFinish[1]);
    //    }

    //    if (itemOnMap.TryGetValue("SpesialStart", out List<int> listSpesialStart))
    //    {
    //        AddSpesialCarStartPoint(listSpesialStart[0], listSpesialStart[1]);
    //    }
    //}

    public void AddCarStartPoint(int x, int y)
    {
        _map[x, y].isObstacle = true;
        _carStartPoints.Enqueue(_tiles[x, y]);
    }

    public void AddCarFinishPoint(int x, int y)
    {
        if (_map[x, y].isObstacle)
            return;

        _tiles[x, y].sprite.gameObject.SetActive(true);
        _carFinishPoints.Add(_tiles[x, y]);
    }

    public void AddSpesialCarStartPoint(int x, int y)
    {
        _map[x, y].isObstacle = true;
        _carSpesialStartPoints.Add(_tiles[x, y]);
    }

    public void AddSpesialCarFinishPoint(int x, int y)
    {
        if (_map[x, y].isObstacle)
            return;

        _tiles[x, y].sprite.color = Color.red;
        _carSpesialFinishPoints.Enqueue(_tiles[x, y]);
    }

    public void AddVoid(int x, int y)
    {
        _map[x, y].isObstacle = true;
        _tiles[x, y].gameObject.SetActive(false);
    }

    public void AddWalls(int x, int y, int walls_id)
    {
        _map[x, y].walls[walls_id] = true;
        _tiles[x, y].gameObject.GetComponent<TileHelper>().SetWalls(walls_id);
    }

    private void SetStartAndEnd(int startX, int startY, int endX, int endY)
    {
        _start = _map[startX, startY];
        _end = _map[endX, endY];
    }

    private void FindPath()
    {
        List<Point> freePoints = new List<Point>();
        List<Point> workedOutPoints = new List<Point>();
        freePoints.Add(_start);

        while (freePoints.Count > 0)
        {
            Point point = GetMinF(freePoints);

            freePoints.Remove(point);
            workedOutPoints.Add(point);
            List<Point> SurroundPoints = GetSurroundPoint(point.X, point.Y);

            foreach (Point p in workedOutPoints)
                if (SurroundPoints.Contains(p))
                    SurroundPoints.Remove(p);

            foreach (Point p in SurroundPoints)
            {
                if (freePoints.Contains(p))
                {
                    int newG = 1 + point.G;

                    if (newG < p.G)
                    {
                        p.SetParent(point, newG);
                    }
                }
                else
                {
                    p.parent = point;
                    GetF(p);
                    freePoints.Add(p);
                }
            }

            if (freePoints.Contains(_end))
            {
                break;
            }
        }
    }

    private List<Point> GetSurroundPoint(int x, int y)
    {
        List<Point> PointList = new List<Point>();

        if (x > 0 && !_map[x - 1, y].isObstacle && !_map[x - 1, y].walls[1] && !_map[x, y].walls[0])
        {
            PointList.Add(_map[x - 1, y]);
        }
        if (y > 0 && !_map[x, y - 1].isObstacle && !_map[x, y - 1].walls[2] && !_map[x, y].walls[3])
        {
            PointList.Add(_map[x, y - 1]);
        }
        if (x < _height - 1 && !_map[x + 1, y].isObstacle && !_map[x + 1, y].walls[0] && !_map[x, y].walls[1])
        {
            PointList.Add(_map[x + 1, y]);
        }
        if (y < _width - 1 && !_map[x, y + 1].isObstacle && !_map[x, y + 1].walls[3] && !_map[x, y].walls[2])
        {
            PointList.Add(_map[x, y + 1]);
        }
        return PointList;
    }


    private void GetF(Point point)
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


    private Point GetMinF(List<Point> list)
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
