using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Panel : MonoBehaviour
{
   protected Enums.Connection connection;
   protected new Light2D light;
    protected bool connected;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        light = this.transform.GetChild(0).GetComponent<Light2D>();
    }

    public Enums.Connection GetConnection()
    {
        return connection;
    }


        protected void OnTriggerExit2D(Collider2D collision)
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


    public bool IsPlayerConnected()
    {
        return connected;
    }

  
}
