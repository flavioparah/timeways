using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PadElement : MonoBehaviour, ISelectHandler
{
     PadScreen padScreen;

    private void Start()
    {
        padScreen = this.gameObject.GetComponentInParent<PadScreen>();
    }
    public void OnSelect(BaseEventData eventData)
    {
        Navigation nav = this.GetComponent<Button>().navigation;
        if (padScreen.GetButtonToNavigate() == null) return;
        nav.selectOnLeft = padScreen.GetButtonToNavigate();
        this.GetComponent<Button>().navigation = nav;

    }
}
