using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] StationControllerUI uiController;
    [SerializeField] int number;
    TextMeshProUGUI textClosed;
    TextMeshProUGUI textOpened;
    TextMeshProUGUI text;
    float sizeFont;
    bool isClosed;
    private void Start()
    {
        isClosed = true;
        
        textOpened = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textClosed = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        text = textClosed;
        textOpened.enabled = false;
        sizeFont = textClosed.fontSize;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontSize = sizeFont * 1.2f;
        uiController.SelectDoor(number);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontSize = sizeFont;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        uiController.ToggleDoor();
        isClosed = !isClosed;
        ChangeText();
    }

    void ChangeText()
    {
        text = isClosed ? textClosed : textOpened;
        textClosed.enabled = isClosed;
        textOpened.enabled = !isClosed;
    }
}
