using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapLogic : MonoBehaviour
{
    private const int MaxStep = 30;

  //  [SerializeField] private GameObject _prefabMapTile; 
    [SerializeField] private TilePool _tilePool; 
    [SerializeField] private Game _game;
    [SerializeField] private ObstacleView _obstacleView;
    [SerializeField] private ObstacleLogic _obstaleLogic;
    [SerializeField] private RewardCounter _rewarder;

    private Point[,] _map;
    private TileHelper[,] _tiles;

    private Queue<TileHelper> _carStartPoints = new Queue<TileHelper>();
    private List<TileHelper> _carFinishPoints = new List<TileHelper>();

    private List<TileHelper> _emptyCellObstacle = new List<TileHelper>();
    private List<TileHelper> _filledCellObstacle = new List<TileHelper>();
    private List<TileHelper> _filledSpesialCellObstacle = new List<TileHelper>();

    private List<TileHelper> _carSpesialStartPoints = new List<TileHelper>();
    private List<TileHelper> _carSpesialFinishPoints = new List<TileHelper>();

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

    public void Init(int width, int height, int roadOffVerticalValue)
    {
        RoadOffVerticalValue = roadOffVerticalValue;

        _width = width;
        _height = height;

        _map = new Point[_height, _width];
        _tiles = new TileHelper[_height, _width];

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

        foreach (var tile in _tiles)
        {
            _map[tile.cordX, tile.cordY].ChangeObstacle(false);
            _tilePool.ReturnItem(tile);
        }
    }

    public void Clean()
    {
        _carFinishPoints = new List<TileHelper>();
        _carSpesialFinishPoints = new List<TileHelper>();
        _filledCellObstacle = new List<TileHelper>();
        _filledSpesialCellObstacle = new List<TileHelper>();
        _emptyCellObstacle = new List<TileHelper>();
        _tilePool.Clean();

        //foreach (TileHelper item in _tiles)
        //{
        //    item.RemoveWalls();
        //    item.spriteRenderer.sprite = _obstacleView.GetSpriteEmpty();
        //    item.spriteRenderer.gameObject.SetActive(false);
        //    item.gameObject.SetActive(false);
        //}
        _tiles = null;
    }

    public int GetMaxPlaceCount()
    {
        return GetMaxFinishPlaceCount() + GetMaxSpesialFinishPlaceCount();
    }

    public int GetMaxFinishPlaceCount()
    {
        int result = 0;

        foreach (TileHelper tile in _carFinishPoints)
            if (tile.gameObject.activeSelf)
                result++;

        return result;
    }

    public int GetMaxSpesialFinishPlaceCount()
    {
        int result = 0;

        foreach (TileHelper tile in _carSpesialFinishPoints)
            if (tile.gameObject.activeSelf)
                result++;

        return result;
    }


    public void SetStart(TileHelper tile)
    {
        _start_X = tile.cordX;
        _start_Y = tile.cordY;
    }

    public void SetEnd(TileHelper tile)
    {
        _end_X = tile.cordX;
        _end_Y = tile.cordY;
        SetStartAndEnd(_start_X, _start_Y, _end_X, _end_Y);
        FindPath();
    }

    public bool CheckObstacle(int x, int y)
    {
        return _map[x, y].IsObstacle;
    }

    public void PrepareReward()
    {
        _rewarder.SetStartPositions(_carFinishPoints);
        _rewarder.SetStartSpesialPositions(_carSpesialFinishPoints);
        _rewarder.WriteReward();
    }

    public TileHelper GetFreePlaceOnQenue(int x)
    {
        int countClosed = 0;

        for (int i = 0; i <= RoadOffVerticalValue; i++)
        {
            if (_map[x, i].IsObstacle)
            {
                countClosed += 1;
            }
        }

        if (CheckObstacle(x, Math.Abs(RoadOffVerticalValue - countClosed - 1)))
        {
            return _tiles[x, 0];
        }

        return _tiles[x, Math.Abs(RoadOffVerticalValue - countClosed - 1)];
    }

    public List<TileHelper> GetPath()
    {
        List<TileHelper> path = new List<TileHelper>();
        Point temp = _end.Parent;

        if (_end.Parent == null)
        {
            return null;
        }

        while (temp != _start)
        {
            if (path.Count > MaxStep)
            {
                return null;
            }

            if (temp == null)
            {
                return null;
            }

            path.Add(_tiles[temp.X, temp.Y]);
            temp = temp.Parent;
        }
        path.Add(GetTile(_start.X, _start.Y));

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
        TileHelper tile = _carSpesialFinishPoints[UnityEngine.Random.Range(0, _carSpesialFinishPoints.Count)];
        _carSpesialFinishPoints.Remove(tile);

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
        _carStartPoints.Enqueue(_tiles[x, y]);
    }

    public void AddCarFinishPoint(int x, int y)
    {
        if (_carFinishPoints.Contains(_tiles[x, y]))
            return;

        _carFinishPoints.Add(_tiles[x, y]);

        if (_map[x, y].IsObstacle)
            return;

        _tiles[x, y].spriteRenderer.gameObject.SetActive(true);
        _tiles[x, y].spriteRenderer.sprite = _obstacleView.GetSpriteOpen(_tiles[x, y].transform);
        //    _carFinishPoints.Add(_tiles[x, y]);
    }

    public void AddSpesialCarStartPoint(int x, int y)
    {
        _carSpesialStartPoints.Add(_tiles[x, y]);
    }

    public void AddSpesialCarFinishPoint(int x, int y)
    {
        if (_carSpesialFinishPoints.Contains(_tiles[x, y]))
            return;

        _carSpesialFinishPoints.Add(_tiles[x, y]);

        if (_map[x, y].IsObstacle)
            return;

        _tiles[x, y].gameObject.SetActive(true);
        _tiles[x, y].spriteRenderer.gameObject.SetActive(true);
        _tiles[x, y].spriteRenderer.sprite = _obstacleView.GetSpriteOpenSpesial(_tiles[x, y].transform);
        // _carSpesialFinishPoints.Add(_tiles[x, y]);
    }

    public void AddVoid(int x, int y)
    {
        _map[x, y].ChangeObstacle(true);
        _tiles[x, y].spriteRenderer.sprite = _obstacleView.GetSpriteEmpty();
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
                _tiles[i, j] = _tilePool.GetItem().GetComponent<TileHelper>();
                _tiles[i, j].transform.SetParent(transform, false);
                _tiles[i, j].transform.position = new Vector3(i + 1, 0, j - 7);
                _tiles[i, j].cordX = i;
                _tiles[i, j].cordY = j;
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
            List<Point> surroundPoints = GetSurroundPoint(point.X, point.Y);

            foreach (Point p in workedOutPoints)
                if (surroundPoints.Contains(p))
                    surroundPoints.Remove(p);

            foreach (Point p in surroundPoints)
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
        List<Point> points = new List<Point>();

        if (x > 0 && !_map[x - 1, y].IsObstacle && !_map[x - 1, y].Walls[1] && !_map[x, y].Walls[0])
        {
            points.Add(_map[x - 1, y]);
        }
        if (y > 0 && !_map[x, y - 1].IsObstacle && !_map[x, y - 1].Walls[2] && !_map[x, y].Walls[3])
        {
            points.Add(_map[x, y - 1]);
        }
        if (x < _height - 1 && !_map[x + 1, y].IsObstacle && !_map[x + 1, y].Walls[0] && !_map[x, y].Walls[1])
        {
            points.Add(_map[x + 1, y]);
        }
        if (y < _width - 1 && !_map[x, y + 1].IsObstacle && !_map[x, y + 1].Walls[3] && !_map[x, y].Walls[2])
        {
            points.Add(_map[x, y + 1]);
        }
        return points;
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

    public void SetVaribleObstaclePlaces(int horizontal, int vertical, int length)
    {
        for (int i = 0; i < length; i++)
        {
            if (_map[i, horizontal].IsObstacle == false)
            {
                TileHelper tile = GetTile(i, horizontal);
                _emptyCellObstacle.Add(tile);
            }
        }

        for (int i = RoadOffVerticalValue + 3; i < _width - 2; i++)
        {
            if (_map[vertical, i].IsObstacle == false)
            {
                TileHelper tile = GetTile(vertical, i);
                _emptyCellObstacle.Add(tile);
            }
        }
    }

    public void CreateRandomObstacle()
    {
        TileHelper tile = _emptyCellObstacle[UnityEngine.Random.Range(0, _emptyCellObstacle.Count)];
        CreatingObstacle(tile.cordX, tile.cordY);
    }

    public void CreatingObstacle(int x, int y)
    {
        if (_map[x, y].IsObstacle)
            return;

        TileHelper creatingTile = GetTile(x, y);
        creatingTile.gameObject.SetActive(true);
        creatingTile.spriteRenderer.gameObject.SetActive(true);

        if (_carSpesialFinishPoints.Contains(creatingTile) == false)
        {
            _filledCellObstacle.Add(creatingTile);
            creatingTile.spriteRenderer.sprite = _obstacleView.GetSpriteClose(creatingTile.transform);
        }
        else
        {
            _filledSpesialCellObstacle.Add(creatingTile);
            creatingTile.spriteRenderer.sprite = _obstacleView.GetSpriteCloseSpesial(creatingTile.transform);
        }

        AddObstacle(creatingTile.cordX, creatingTile.cordY);
        _obstaleLogic.RememberObstacle(creatingTile);
    }

    public void DeleteObstacle(int x, int y)
    {
        TileHelper tile = GetTile(x, y);
        tile.gameObject.SetActive(true);
        tile.spriteRenderer.gameObject.SetActive(true);
        RemoveObstacle(tile.cordX, tile.cordY);

        if (_filledCellObstacle.Contains(tile))
        {
            _filledCellObstacle.Remove(tile);
            tile.spriteRenderer.sprite = _obstacleView.GetSpriteOpen(tile.transform);
            _rewarder.ChangeRewardCell(tile);
        }

        if (_filledSpesialCellObstacle.Contains(tile))
        {
            _filledSpesialCellObstacle.Remove(tile);
            tile.spriteRenderer.sprite = _obstacleView.GetSpriteOpenSpesial(tile.transform);
            _rewarder.ChangeRewardSpesialCell(tile);
        }
    }
}
