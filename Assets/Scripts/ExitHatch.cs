using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitHatch : Section
{
    [SerializeField] Door doorLeft;
    [SerializeField] Door doorRight;
    [SerializeField] Animator hatchAnim;

    bool doorLeftClosed;
    bool doorRightClosed;
    // Start is called before the first frame update
    void Start()
    {
        doorLeft.doorClosed += SetDoorLeftClosed;
        doorRight.doorClosed += SetDoorRightClosed;

        doorLeftClosed = doorLeft.IsClosed();
        doorRightClosed = doorRight.IsClosed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SetDoorLeftClosed(bool closed)
    {
        doorLeftClosed = closed;
        if (closed && doorRightClosed) LightRed();
        else LightWhite();
    }

    void SetDoorRightClosed(bool closed)
    {
        doorRightClosed = closed;
        if (closed && doorLeftClosed) LightRed();
        else LightWhite();
    }


    void LightRed()
    {
        this.light.color = Color.red;
        hatchAnim.SetBool("Ready", true);
        hatchAnim.GetComponent<Collider2D>().enabled = true;
    }

    void LightWhite()
    {
        this.light.color = Color.white;
        hatchAnim.SetBool("Ready", false);
        hatchAnim.GetComponent<Collider2D>().enabled = false;
    }

    public void OpenHatch()
    {
        hatchAnim.SetTrigger("Open");
    }
}
