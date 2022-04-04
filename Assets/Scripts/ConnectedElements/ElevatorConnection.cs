using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorConnection : Connection
{
    ElevatorPanel elevatorPanel;
   

    public override void Show(Panel panel)
    {
        this.elevatorPanel = (ElevatorPanel)panel;
    }

    public override void Hide()
    {


    }

    public override void PerformAction()
    {
        
    }
}
