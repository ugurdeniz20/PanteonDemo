
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ShipStation : Build
{
    public Ship prefab;
    public int shipCost = 50;
    private List<Cordinat> ShipStationNeighbor = new List<Cordinat>();

    public new void Start()
    {
        base.Start();
        base.SetType(typeof(ShipStation));
    }

    public override void BuildCreated()
    {
        InfoController.m.SetShipStationInformation(this);
    }
    // Soldier creation division only by ShipStatioN
    public void CreateSoldier()
    {
       
        SearchEmptyNeighborsForSoldier();
        if (ShipStationNeighbor.Count == 0)
        {
            Alert.m.Open("ALERT!!", "In order to produce more troops, you must send troops to another location.!", "Okay", null);
            return;
        }

       
        int rnd = Random.Range(0, ShipStationNeighbor.Count);
        Ship ship = Instantiate(prefab, GridController.m.GetGridPositionFromCordinat(ShipStationNeighbor[rnd])
                            , Quaternion.identity, GridController.m.transform);
        ship.SetCordinat(ShipStationNeighbor[rnd]);

    }

    // To search for free space around of ShipStatioN
    public void SearchEmptyNeighborsForSoldier()
    {
        ShipStationNeighbor.Clear();
        int neighborCount = 2 * (width + height) + 4;
        Cordinat tempCenter = new Cordinat(centerCordinat.x, centerCordinat.y);
        for (int i = 0; i <= neighborCount; i++)
        {
            Cordinat temp = new Cordinat(tempCenter.x - 2, tempCenter.y - 2);
            if (temp.x >= 0 && temp.y >= 0)
            {
                if (GridController.m.GetGridIsEmpty(temp))
                {
                    ShipStationNeighbor.Add(temp);
                }
            }
            if (i < 4)
                tempCenter.x++;
            else if (i < 8)
                tempCenter.y++;
            else if (i < 12)
                tempCenter.x--;
            else
                tempCenter.y--;
        }

    }
}
