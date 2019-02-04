using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{

    // Use this for initialization

    public ShipStation shipstationPrefab;
    public PowerPlant powerplantPrefab;


    public static BuildController a;
    void Awake()
    {
        a = this;
    }
    void Start()
    {
    }
    public void CreateBaraca()
    {
        Vector2 createPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ShipStation shipstation = Instantiate(shipstationPrefab, createPos, Quaternion.identity);
        shipstation.gameObject.transform.parent = gameObject.transform;
        InfoController.m.scrollBar.SetActive(true);
    }

    public void CreateFactory()
    {
        Vector2 createPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PowerPlant powerplant = Instantiate(powerplantPrefab, createPos, Quaternion.identity);
        powerplant.gameObject.transform.parent = gameObject.transform;
       InfoController.m.scrollBar.SetActive(true);
    }


}
