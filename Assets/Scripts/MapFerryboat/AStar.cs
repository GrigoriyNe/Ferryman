using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public const int width = 5;
    public const int height = 5;

    public Point[,] map = new Point[height, width];
    public TileHelper[,] tiles = new TileHelper[height, width];

    public GameObject prefab;  
    public Point start;
    public Point end;

    int Start_X;
    int Start_Y;
    int End_X;
    int End_Y;

    void Start()
    {
        InitMap();
        /*AddObstacle(2, 4);
        AddObstacle(2, 3);
        AddObstacle(2, 2);
        AddObstacle(2, 0);
        AddObstacle(6, 4);
        AddObstacle(8, 4);
        */

          AddWalls(2, 7, 2);
          AddWalls(3, 7, 2);
          AddWalls(4, 7, 2);
          AddWalls(5, 7, 2);
          AddWalls(7, 7, 2);

          AddWalls(2, 7, 0);
          AddWalls(2, 6, 0);
          AddWalls(2, 5, 0);
          AddWalls(2, 4, 0);
          AddWalls(2, 2, 0);

          AddWalls(2, 2, 3);
          AddWalls(3, 2, 3);
          AddWalls(4, 2, 3);
          AddWalls(5, 2, 3);
          AddWalls(6, 2, 3);
          AddWalls(7, 2, 3);

          AddWalls(7, 2, 1);
          AddWalls(7, 3, 1);
          AddWalls(7, 4, 1);
          AddWalls(7, 5, 1);
          AddWalls(7, 6, 1);
          AddWalls(7, 7, 1);


          AddWalls(2, 4, 3);
          AddWalls(3, 4, 3);
          AddWalls(3, 4, 1);

          AddWalls(2, 2, 2);
        
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (end != null)
                {
                    ClearPath();
                }
                if (hit.collider.gameObject.tag == "Tile")
                {
                    tiles[Start_X, Start_Y].sprite.color = Color.white;
                    tiles[End_X, End_Y].sprite.color = Color.white;
                    Start_X = hit.collider.gameObject.GetComponent<TileHelper>().cord_x;
                    Start_Y = hit.collider.gameObject.GetComponent<TileHelper>().cord_y;
                }

            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Tile")
                {
                    if (end != null)
                    {
                        ClearPath();
                    }
                    tiles[End_X, End_Y].sprite.color = Color.white;
                    End_X = hit.collider.gameObject.GetComponent<TileHelper>().cord_x;
                   End_Y = hit.collider.gameObject.GetComponent<TileHelper>().cord_y;
                    SetStartAndEnd(Start_X, Start_Y, End_X, End_Y);
                    FindPath();
                    ShowPath();
                };
            }

        }
    }
    public void InitMap()//Initialize the map
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tiles[i, j] = Instantiate(prefab, new Vector3(i, 0, j), Quaternion.identity).GetComponent<TileHelper>();
                tiles[i, j].cord_x = i;
                tiles[i, j].cord_y = j;
                map[i, j] = new Point(i, j);
            }
        }
    }

    public void AddObstacle(int x, int y)//Add barriers
    {
        map[x, y].isObstacle = true;
        tiles[x, y].sprite.color = Color.black;
    }

    public void AddWalls(int x, int y,int walls_id) //Add Walls
    {
        map[x, y].walls[walls_id] = true;
        tiles[x, y].gameObject.GetComponent<TileHelper>().SetWalls(walls_id);
    }

    public void SetStartAndEnd(int startX, int startY, int endX, int endY)//Set start and end points
    {
        start = map[startX, startY];
        tiles[startX, startY].sprite.color = Color.green;
        end = map[endX, endY];
        tiles[endX, endY].sprite.color = Color.red;
    }

    public void ShowPath()//Show path
    {
        Point temp = end.parent;
        while (temp != start)
        {
            tiles[temp.X, temp.Y].sprite.color = Color.gray;
            temp = temp.parent;
        }
    }
    public void ClearPath() //Clear path
    {
        Point temp = end.parent;
        while (temp != start)
        {
            tiles[temp.X, temp.Y].sprite.color = Color.white;
            temp = temp.parent;
        }
    }

    public void FindPath()
    {
        List<Point> openList = new List<Point>();
        List<Point> closeList = new List<Point>();
        openList.Add(start);
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
            if (openList.Contains(end))//As long as the end appears, it ends
            {
                break;
            }
        }
    }


    public List<Point> GetSurroundPoint(int x, int y)//Get a point around a point
    {
        List<Point> PointList = new List<Point>();
        
        if (x > 0 && !map[x - 1, y].isObstacle && !map[x - 1, y].walls[1] && !map[x, y].walls[0])
        {
            PointList.Add(map[x - 1, y]);
          //  tiles[x - 1, y].sprite.color = Color.yellow;
        }
        if (y > 0 && !map[x, y - 1].isObstacle && !map[x, y-1].walls[2] && !map[x, y].walls[3])
        {
            PointList.Add(map[x, y - 1]);
          //  tiles[x, y - 1].sprite.color = Color.yellow;
        }
        if (x < height - 1 && !map[x + 1, y].isObstacle && !map[x+1,y].walls[0] && !map[x, y].walls[1])
        {
            PointList.Add(map[x + 1, y]);
           // tiles[x + 1, y].sprite.color = Color.yellow;
        }
        if (y < width - 1 && !map[x, y + 1].isObstacle && !map[x, y+1].walls[3] && !map[x, y].walls[2])
        {
            PointList.Add(map[x, y + 1]);
            //tiles[x, y + 1].sprite.color = Color.yellow;
        }
        return PointList;
    }


    public void GetF(Point point)//Calculate the F value of a certain point
    {
        int G = 0;
        int H = Mathf.Abs(end.X - point.X) + Mathf.Abs(end.Y - point.Y);
        if (point.parent != null)
        {
            G = 1 + point.parent.G;
        }
        int F = H + G;
        point.H = H;
        point.G = G;
        point.F = F;
    }


    public Point GetMinFOfList(List<Point> list)//Get the point with the smallest F value in a set
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
