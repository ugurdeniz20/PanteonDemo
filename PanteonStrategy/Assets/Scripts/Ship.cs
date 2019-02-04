using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public string name;
    public Sprite img;
    public Cordinat shipCord;
    private SpriteRenderer spriteRenderer;
    public int speed = 250 ;
    bool isRun;
    private int pathIndex;
    private Vector2[] path;


    public GameObject pathicon;
    List<GameObject> pathobje = new List<GameObject>();
    public void SetCordinat(Cordinat cord)
    {
        shipCord = cord;
        GridController.m.gridArray[shipCord.x, shipCord.y] = 1;
    }

    void Start()
    {
       
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && GameController.m.selectedShip == this)
        {
            path = PathFinding.SearchPath(transform.localPosition, Input.mousePosition);
            if (path == null)
            {
                Debug.LogError("Wrong Target Selection!!");
                GridController.m.CloseTargetIcon();
                isRun = false;
                Alert.m.Open("ALERT!","Department is currently full!.","Okay",null);
                return;
            }
            else
            {
                GridController.m.gridArray[shipCord.x, shipCord.y] = 0;
                isRun = true;
                pathIndex = 0;
            }
            DrawOnThePath();

        }

        if (isRun)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, path[pathIndex], speed * Time.deltaTime);
            if (Vector2.Distance(transform.localPosition, path[pathIndex]) < 0.1f)
            {
                Destroy(pathobje[pathIndex]);
                pathIndex++;
                if (pathIndex == path.Length)
                {
                    GridController.m.CloseTargetIcon();
                    SetCordinat(GridController.m.GetCordinatFromWorldPosition(transform.position));
                    isRun = false;
                }

            }

        }
    }
    
//Draws the target path
    private void DrawOnThePath()
    {
        pathobje.ForEach(x => Destroy(x.gameObject));
        pathobje.Clear();
        if (path != null)
        {
            for (int i = 0; i < path.Length; i++)
            {
                pathobje.Add(Instantiate(pathicon, path[i], Quaternion.identity));
            }
        }
       
    
    }

    void OnMouseDown()
    {
        GameController.m.selectedShip = this;
        InfoController.m.SetInfo("Name :" + name + "\nSpeed :" + speed , img);
    }
}
