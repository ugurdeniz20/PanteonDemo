using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController m;
    public Ship selectedShip;


    void Awake()
    {
        m = this;
    }


//To increase score by coroutine
    void Start(){
        StartCoroutine(UpdateEnergyValue());
    }

    IEnumerator UpdateEnergyValue(){
        while(true){
            yield return new WaitForSeconds(1);
           
        }
    }
}
