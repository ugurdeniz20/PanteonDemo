using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : Build
{
    float timer;
    public bool isRun;

    public new void Start()
    {
        base.Start();
    }
    public new void Update()
    {
        base.Update();
        if (!isRun)
            return;
    }

    public override void BuildCreated()
    {
        isRun = true;
    }
}
