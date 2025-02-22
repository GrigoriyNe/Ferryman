using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class MapLogic : MonoBehaviour
{
    private const int MaxStep = 50;

    [SerializeField] private GameObject _prefabMapTile;
    [SerializeField] private ObstacleView _obstacleView;

    private Point[,] _map;
    private TileHelper[,] _tiles;

    private Queue<TileHelper> _carStartPoints = new Queue<TileHelper>();
    private List<TileHelper> _carFinishPoints = new List<TileHelper>();

    private List<TileHelper> _emptyCellObstacle = new List<TileHelper>();
    private Queue<TileHelper> _filledCellObstacle = new Queue<TileHelper>();

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

    public int RoadOffVerticalValue { get; private set; }

    public void Init(int width, int height)
    {
        _width = width;
        _height = height;

        _map = new Point[_height, _width];
        _tiles = new TileHelper[_height, _width];

        RoadOffVerticalValue = 8;

        if (_width == 17)
        {
            RoadOffVerticalValue = 9;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }

        if (_width > 17)
        {
            RoadOffVerticalValue = _width - 9;
            transform.position = new Vector3(transform.position.x, transform.position.y, -7);
        }

        Activate();
        InitLogicMap();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    //    _emptyCellObstacle = null;
   //     _filledCellObstacle = new Queue<TileHelper>();
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
        return _map[x, y].IsObstacle;
    }

    public TileHelper GetFreePlaceOnQenue(int x)
    {
        int countClosed = 0;

        for (int i = 0; i < RoadOffVerticalValue; i++)
        {
            if (_map[x, i].IsObstacle)
            {
                countClosed += 1;
            }
        }

        return _tiles[x, Math.Abs(RoadOffVerticalValue - countClosed - 2)];
    }

    public List<TileHelper> GetPath()
    {
        List<TileHelper> path = new List<TileHelper>();
        Point temp = _end.Parent;

        if (_end.Parent == null)
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
            temp = temp.Parent;
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
        _map[x, y].ChangeObstacle(true);
    }

    public void RemoveObstacle(int x, int y)
    {
        _map[x, y].ChangeObstacle(false);
    }

    public TileHelper GetTile(int x, int y)
    {
        return _tiles[x, y];
    }

    public void AddCarStartPoint(int x, int y)
    {
        _map[x, y].ChangeObstacle(true);
        _carStartPoints.Enqueue(_tiles[x, y]);
    }

    public void AddCarFinishPoint(int x, int y)
    {
        if (_map[x, y].IsObstacle)
            return;

        _tiles[x, y].sprite.gameObject.SetActive(true);
        _carFinishPoints.Add(_tiles[x, y]);
    }

    public void AddSpesialCarStartPoint(int x, int y)
    {
        _map[x, y].ChangeObstacle(true);
        _carSpesialStartPoints.Add(_tiles[x, y]);
    }

    public void AddSpesialCarFinishPoint(int x, int y)
    {
        if (_map[x, y].IsObstacle)
            return;

        _carSpesialFinishPoints.Enqueue(_tiles[x, y]);
    }

    public void AddVoid(int x, int y)
    {
        _map[x, y].ChangeObstacle(true);
        _tiles[x, y].gameObject.SetActive(false);
    }

    public void AddWall(int x, int y, int walls_id)
    {
        _map[x, y].AddWalls(walls_id);
        _tiles[x, y].gameObject.GetComponent<TileHelper>().SetWalls(walls_id);
    }

    private void InitLogicMap()
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
            Point point = GetMinPathCout(freePoints);

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
                    int newG = 1 + point.CountTileToFinishVertical;

                    if (newG < p.CountTileToFinishVertical)
                    {
                        p.SetValues(point, newG);
                    }
                }
                else
                {
                    p.SetParent(point);
                    GetPathCout(p);
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

        if (x > 0 && !_map[x - 1, y].IsObstacle && !_map[x - 1, y].Walls[1] && !_map[x, y].Walls[0])
        {
            PointList.Add(_map[x - 1, y]);
        }
        if (y > 0 && !_map[x, y - 1].IsObstacle && !_map[x, y - 1].Walls[2] && !_map[x, y].Walls[3])
        {
            PointList.Add(_map[x, y - 1]);
        }
        if (x < _height - 1 && !_map[x + 1, y].IsObstacle && !_map[x + 1, y].Walls[0] && !_map[x, y].Walls[1])
        {
            PointList.Add(_map[x + 1, y]);
        }
        if (y < _width - 1 && !_map[x, y + 1].IsObstacle && !_map[x, y + 1].Walls[3] && !_map[x, y].Walls[2])
        {
            PointList.Add(_map[x, y + 1]);
        }
        return PointList;
    }


    private void GetPathCout(Point point)
    {
        int G = 0;
        int H = Mathf.Abs(_end.X - point.X) + Mathf.Abs(_end.Y - point.Y);

        if (point.Parent != null)
        {
            G = 1 + point.Parent.CountTileToFinishVertical;
        }

        int F = H + G;
        point.Change(H, G, F);
    }


    private Point GetMinPathCout(List<Point> list)
    {
        int min = int.MaxValue;
        Point point = null;

        foreach (Point p in list)
        {
            if (p.CountTileToFinish < min)
            {
                min = p.CountTileToFinish;
                point = p;
            }
        }
        return point;
    }

    public void SetObstaclePlaces(int horizontal, int vertical, int length)
    {
        for (int i = 0; i < length; i++)
        {
            TileHelper tile = GetTile( i, horizontal);
            _emptyCellObstacle.Add(tile);
        }

        for (int i = RoadOffVerticalValue + 3; i < _width; i++)
        {
            TileHelper tile = GetTile(vertical, i);

            _emptyCellObstacle.Add(tile);    
        }

     //   AddedCreatedObstacle();
    }

    public void CreateObstacle()
    {
        TileHelper tile = _emptyCellObstacle[UnityEngine.Random.Range(0, _emptyCellObstacle.Count)];
        CreatingObstacle(tile);
    }

    //private void AddedCreatedObstacle()
    //{
    //    if (_filledCellObstacle.Count>0)
    //    {
    //        foreach (TileHelper tile in _filledCellObstacle)
    //        {
    //            CreatingObstacle(tile); 
    //        }
    //    }
    //}

    private void CreatingObstacle(TileHelper tile)
    {
        tile.sprite.sprite = _obstacleView.GetSprite();
        _carFinishPoints.Remove(tile);
        _carStartPoints.Dequeue();

        AddObstacle(tile.cord_x, tile.cord_y);
        tile.sprite.gameObject.SetActive(true);
        _filledCellObstacle.Enqueue(tile);
        _emptyCellObstacle.Remove(tile);
    }

    public void DeleteObstacle()
    {
        TileHelper tile = _filledCellObstacle.Dequeue();
        RemoveObstacle(tile.cord_x, tile.cord_y);
        _emptyCellObstacle.Add(tile);
    }
}
