using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{

    public static GridController m;
    public SpriteRenderer ground;
    public int rowSize;
    public int columnSize;
    public int pixelSize;
    public GameObject targetPoint;
    public float width, height;
    public byte[,] gridArray;
    private Vector2 newCenterPosition;
    public Vector2 currentPosition;

    // Use this for initialization
    void Start()
    {
        m = this;
        gridArray = new byte[columnSize, rowSize];
        height = Camera.main.orthographicSize * 2;
        width = Camera.main.aspect * height;

        newCenterPosition = new Vector2(-columnSize * pixelSize / 2, rowSize * pixelSize / 2);
        ground.transform.position = newCenterPosition;
        ground.size = new Vector2(columnSize * pixelSize / 100f, rowSize * pixelSize / 100f);
      
    }

//  Coordinate calculation from position
    public Cordinat GetCordinatFromWorldPosition(Vector2 position)
    {
        Vector2 relativePosition = (newCenterPosition - position) * -1f;
        relativePosition = new Vector2(relativePosition.x, relativePosition.y * -1f);

        if (relativePosition.x < 0 || relativePosition.y < 0
            || relativePosition.x > (columnSize * pixelSize)
            || relativePosition.y > (rowSize * pixelSize))
        {
            
            return null;
        }

        Vector2 snapPosition = new Vector2(relativePosition.x - (pixelSize / 2f), relativePosition.y - (pixelSize / 2f));
        Cordinat cord = new Cordinat((int)(snapPosition.x / pixelSize), (int)(snapPosition.y / pixelSize));

        if (snapPosition.x % pixelSize > (pixelSize / 2f)) cord.x++;
        if (snapPosition.y % pixelSize > (pixelSize / 2f)) cord.y++;

        return cord;
    }

//Position calculation from Coordinate
    public Vector2 GetGridPositionFromCordinat(Cordinat cord)
    {
        Vector2 finalPosition = new Vector2((pixelSize * cord.x) + (pixelSize / 2f), (pixelSize * cord.y) + (pixelSize / 2f));
        finalPosition = new Vector2(finalPosition.x, finalPosition.y * -1f);
        finalPosition *= -1f;
        finalPosition = newCenterPosition - finalPosition;

        return finalPosition;
    }


    public void ShowTargetIcon(Vector2 position)
    {
        targetPoint.SetActive(true);
        targetPoint.transform.localPosition = position;
    }


    public void CloseTargetIcon()
    {
        targetPoint.SetActive(false);
    }


    public bool GetGridIsEmpty(Cordinat c)
    {
        return gridArray[c.x, c.y] == 0 ? true : false;
    }
}

public class Cordinat
{
    public int x;
    public int y;
    //ctor : A
    public Cordinat(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public List<Cordinat> GetNeighbors()
    {
        List<Cordinat> neighbors = new List<Cordinat>();
        Cordinat current = new Cordinat(x - 1, y - 1);
        for (int i = 0; i < 8; i++)
        {
            if (current.x - 1 >= 0 && current.y - 1 >= 0 && current.x < GridController.m.columnSize && current.y < GridController.m.rowSize)
            {
                if (GridController.m.GetGridIsEmpty(current))
                    neighbors.Add(current);
            }

            current = new Cordinat(current.x, current.y);

            if (i < 2)
                current.x++;
            else if (i < 4)
                current.y++;
            else if (i < 6)
                current.x--;
            else
                current.y--;
        }
        return neighbors;
    }

    public bool Isquals(Cordinat target)
    {
        if (target.x == x && target.y == y)
            return true;
        else
            return false;
    }
}
