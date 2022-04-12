using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PuzzleTile : MonoBehaviour
{

    public Color color;
    public bool isBlocked;
    public bool isOn;
    public bool isGreen;

    new Light2D light;
    private void Awake()
    {
        light = this.transform.GetChild(0).GetComponent<Light2D>();
        TurnOffLight();

    }

    public void TurnOffLight()
    {
        light.intensity = 0;
        isOn = false;
        isBlocked = false;
    }

    public void TurnOnYellow()
    {
        light.intensity = 2.5f;
        light.color = Color.yellow;
        isOn = true;
        isGreen = false;
        isBlocked = true;
    }

    public void TurnOnGreen()
    {
        light.intensity = 2.5f;
        light.color = Color.green;
        isGreen = true;
        isOn = true;
        isBlocked = true;
    }

    public void Block()
    {
        light.intensity = 2.5f;
        light.color = Color.red;
        isGreen = false;
        isOn = true;
        isBlocked = true;
    }
}
