using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Section : MonoBehaviour
{
    [SerializeField] Light2D light;
    private void Start()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("opa um player entrou aqui.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TurnLightOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TurnLightOff();
        }
    }

    void TurnLightOn()
    {
        StartCoroutine(TurningLightOn());
    }

    void TurnLightOff()
    {
        light.enabled = false;
    }

    IEnumerator TurningLightOn()
    {
        light.enabled = true;

        yield return new WaitForSeconds(.3f);

        light.enabled = false;

        yield return new WaitForSeconds(.2f);

        light.enabled = true;

        yield return new WaitForSeconds(.1f);

        light.enabled = false;

        yield return new WaitForSeconds(.1f);

        light.enabled = true;
    }

}
