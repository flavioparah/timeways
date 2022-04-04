using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DoorPanel : Panel
{
    Door door;
    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();
        door = this.transform.parent.GetComponent<Door>();
        light = this.transform.GetChild(0).GetComponent<Light2D>();
        connection = Enums.Connection.door;
    }

    public void ToggleDoor()
    {
        door.ToggleDoor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            connected = true;
            collision.GetComponent<Player>().ConnectPad(this);
            TurnLightOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().DisconnectPad();
            TurnLightOff();
            connected = false;
        }
    }

    private void TurnLightOn()
    {
        light.enabled = true;
    }

    private void TurnLightOff()
    {
        light.enabled = false;
    }

    public bool IsDoorClosed()
    {
        return door.IsClosed();
    }

    public bool IsPlayerConnected()
    {
        return connected;
    }

    public string GetDoorName()
    {
        return door.gameObject.name;
    }
}
