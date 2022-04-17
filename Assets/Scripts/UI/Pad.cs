using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pad : MonoBehaviour
{
    #region Inspector
    [SerializeField] GameObject padPanel;
    [SerializeField] PadScreen padScreen;
    [SerializeField] Player player;
    [SerializeField] bool isOutside;
    [SerializeField] PlayerSpace playerSpace;
    #endregion

    #region Members
    Enums.Screen screen;
    Animator anim;
    #endregion

    #region elementActions
    bool toggledDoor;
    bool toggleElevator;
    #endregion

    #region ConnectedElements
    Panel panel;

    #endregion

    #region Actions
    public event UnityAction padClosed;
    #endregion
    private void Start()
    {
        anim = this.GetComponent<Animator>();
        
    }
    public void OpenPad(Panel panel)
    {
        this.panel = panel;
        anim.SetBool("OpenPad", true);
    }

    public void ShowScreen()
    {
        padScreen.OpenScreen(panel);
    }

    public void ClosePad()
    {
        padClosed?.Invoke();
        anim.SetBool("OpenPad", false);
        if(isOutside)
        {
            playerSpace.ClosePad();
            return;
        }
        player.ClosePad();
    }


    public void ClosePadScreen()
    {
        padScreen.CloseScreen();
    }

    public void GrantPlayerAccess()
    {
        if (player != null)
            player.GrantPadAccess();
    }



}
