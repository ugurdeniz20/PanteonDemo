using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InfoController : MonoBehaviour
{

    public static InfoController m;
    public GameObject scrollBar;
    public GameObject infoPanel;
    public Text name;
    public Image image;
    public Button createshipButton;

    void Start()
    {
        m = this;
        infoPanel.SetActive(false);
    }

    public void SetShipStationInformation(ShipStation ShipStation)
    {
        createshipButton.gameObject.SetActive(true);
        createshipButton.onClick.RemoveAllListeners();
        createshipButton.onClick.AddListener(ShipStation.CreateSoldier);
        
    }

    public void SetInfo(string name, Sprite image)
    {
        this.name.text = name;
        this.image.sprite = image;
        createshipButton.gameObject.SetActive(false);
    }

}
