using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    public string name;
    public Sprite img;
    public int width;
    public int height;
    public bool isForStay = true;
    public List<Cordinat> areaCordinats = new List<Cordinat>();
    public Cordinat centerCordinat;
    private SpriteRenderer spriteRenderer;
    public bool follow = false;
    private Type childType;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        TakeControl();
        Debug.Log("Build");
    }

    public void Update()
    {
        if (follow)
        {
            SnapPosition(Input.mousePosition);
            InfoController.m.scrollBar.SetActive(false);
        }
    }
    public void SetType(Type type)
    {
        childType = type;
    }

    public void SnapPosition(Vector2 mousePosition)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        centerCordinat = GridController.m.GetCordinatFromWorldPosition(mousePosition);
        if (centerCordinat != null)
        {
            Vector2 finalPosition = GridController.m.GetGridPositionFromCordinat(centerCordinat);
            CheckArea(centerCordinat, GridController.m.gridArray, finalPosition, GridController.m.pixelSize);
            SetPosition(finalPosition, GridController.m.pixelSize);

        }
        else
        {
            transform.position = mousePosition;
            isForStay = false;
        }
    }
    public void TakeControl()
    {
        follow = true;
        spriteRenderer.sortingOrder = 10;
    }

    public void LeaveControl()
    {
        isForStay = false;
        follow = false;
        spriteRenderer.sortingOrder = 1;
        InfoController.m.scrollBar.SetActive(true);
        InfoController.m.infoPanel.SetActive(true);
        InfoController.m.SetInfo(name, img);
        areaCordinats.ForEach(cord => GridController.m.gridArray[cord.x, cord.y] = 1);
        BuildCreated();
    }

    public void OnMouseDown()
    {
        if (!follow)
        {
            TakeControl();
        }

        if (isForStay)
        {
            LeaveControl();
        }
    }
    public void CheckArea(Cordinat pivotIndex, byte[,] areaMap, Vector2 position, float pixelSize)
    {

        if (centerCordinat.x <= pivotIndex.x || centerCordinat.y <= pivotIndex.y)
        {
            int minX, maxX;
            int minY, maxY;

            CalcMinMaxLimits(width, pivotIndex.x, out minX, out maxX);
            CalcMinMaxLimits(height, pivotIndex.y, out minY, out maxY);

            isForStay = true;
            List<Cordinat> buildArea = GetTargetAreaPoints(minX, maxX, minY, maxY);
            areaCordinats.ForEach(cord => areaMap[cord.x, cord.y] = 0);

            foreach (Cordinat cord in buildArea)
            {
                if (areaMap[cord.x, cord.y] == 1)
                {
                    isForStay = false;
                    break;
                }
        
            }

            if (isForStay)
            {
                areaCordinats = buildArea;
                spriteRenderer.color = Color.white;
            }
            else
            {
                spriteRenderer.color = Color.red;
            }

           
        }
    }

    public List<Cordinat> GetTargetAreaPoints(int minX, int maxX, int minY, int maxY)
    {
        List<Cordinat> cordList = new List<Cordinat>();
        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                if (i >= 0 && j >= 0)
                    cordList.Add(new Cordinat(i, j));
            }
        }
        return cordList;
    }
    public void CalcMinMaxLimits(int sizeValue, int positionValue, out int min, out int max)
    {
        int factor = (int)(sizeValue / 2);
        min = positionValue - (factor - (sizeValue % 2 == 0 ? 1 : 0));
        max = positionValue + factor;
    }

    public void SetPosition(Vector2 position, float pixelSize)
    {
        position = new Vector2(width % 2 == 0 ? position.x + pixelSize / 2f : position.x,
                                height % 2 == 0 ? position.y - pixelSize / 2f : position.y);
        transform.position = position;
    }

    public virtual void BuildCreated() { }
}
