using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ElevatorPanel : Panel
{

    Elevator elevator;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        elevator = this.transform.parent.GetComponent<Elevator>();
        light = this.transform.GetChild(0).GetComponent<Light2D>();
        connection = Enums.Connection.elevator;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
