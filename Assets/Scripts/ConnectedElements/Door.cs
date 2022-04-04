using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Door : MonoBehaviour
{
    [SerializeField] float lightDelay;
    [SerializeField] float changeLightSpeed;
    [SerializeField] bool closed;
    [SerializeField] Color closedColor;
    [SerializeField] Color openedColor;
    [SerializeField] Color disableColor;
    [SerializeField] GameObject doorCollider;
    List<Light2D> lights = new List<Light2D>();


    [SerializeField] float maxBright;
    [SerializeField] float minBright;
    // Start is called before the first frame update
    void Start()
    {
        GetLights();
        doorCollider.SetActive(closed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetLights()
    {
        Transform lights = this.transform.Find("Lights");
        foreach (Transform t in lights.transform)
        {
            Light2D l = t.GetComponent<Light2D>();
            this.lights.Add(l);
        }
    }

    public bool IsClosed()
    {
        return closed;
    }
    public void ToggleDoor()
    {
        closed = !closed;
        StartCoroutine(TogglingDoor(closed));
    }

    IEnumerator TogglingDoor(bool closing)
    {
        int total = lights.Count;
        int index = 0;

        Color startColor = closing ? openedColor : closedColor;
        Color targetColor = closing ? closedColor : openedColor;

        Light2D[] lightsAux = new Light2D[total];
        if (!closing)
        {
            for (int i = total - 1; i >= 0; i--)
            {
                lightsAux[index] = lights[i];
                index++;
            }
        }
        else
        {
            lights.CopyTo(lightsAux);

        }

        index = 0;
        while (index < total)
        {
            Light2D l = lightsAux[index];

            //float factor = 0;
            //while(factor < 1)
            //{
            //    factor += changeLightSpeed * Time.deltaTime;

            //    l.color = Color.Lerp(startColor, targetColor, factor);

            //    if(factor >= 1)
            //    {
            //        l.color = targetColor;
            //    }

            //    yield return null;
            //}
            l.color = targetColor;
            yield return new WaitForSeconds(lightDelay);
            index++;
        }

        doorCollider.SetActive(closed);
    }

    public void Blink()
    {
        StartCoroutine(Blinking());
    }

    IEnumerator Blinking()
    {
        for (int i = 0; i < 3; i++)
        {

            float speed = changeLightSpeed;
            float factor = minBright;

            while (factor < maxBright)
            {
                factor += speed * Time.deltaTime;
                foreach (Light2D l in lights)
                {
                    l.intensity = factor;
                }

                yield return null;
            }

            while (factor > minBright)
            {
                factor -= speed * Time.deltaTime;
                foreach (Light2D l in lights)
                {
                    l.intensity = factor;
                }
                yield return null;
            }


        }
        foreach (Light2D l in lights)
        {
            l.intensity = minBright;
        }
    }
}
