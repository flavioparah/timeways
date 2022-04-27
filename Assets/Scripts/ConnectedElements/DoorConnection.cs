using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DoorConnection : Connection
{
    [SerializeField] float speed;
    [SerializeField] float toggleButtonSpeed;
    [SerializeField] Image btnEffectOpen;
    [SerializeField] Image btnEffectClosed;

    [SerializeField] Color secondColor;
    [SerializeField] Color firstColor;



    [SerializeField] Vector3 initialScale;


    [SerializeField] GameObject button;

    [SerializeField] TextAnimation buttonText;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI textDenied;

    [SerializeField] TextMeshProUGUI textOpenDoor;
    [SerializeField] TextMeshProUGUI textCloseDoor;
    [SerializeField] TextMeshProUGUI connectedText;
    [SerializeField] TextMeshProUGUI connectionDeniedText;

    Color textInitialColor;
    Animator anim;
    bool doorToggled;

    DoorPanel doorPanel;
    PadElement doorButton;

    bool isOpenButton;
    bool isInteractable;
    int protocol;

    public override void Show(Panel panel)
    {
        screenClosed = false;
        this.doorPanel = (DoorPanel)panel;
        protocol = GameManager.Instance.GetProtocol();
        ShowButton();

    }
    public void ToggleDoorButton()
    {

        isOpenButton = !isOpenButton;
        doorToggled = true;
        text.GetComponent<TextAnimation>().StopTyping();
        EraseTexts();
        StartCoroutine(TogglingDoorButton());
    }


    public override void Hide()
    {
        if (screenClosed) return;
        HideButton();
        padScreen.EnableConnectionTab();
    }
    public override void PerformAction()
    {
        if (!doorToggled) return;
        doorPanel.ToggleDoor();
        doorToggled = false;
    }

    public void ShowButton()
    {
        isOpenButton = doorPanel.IsDoorClosed();
        int doorProtocol = doorPanel.GetDoorProtocol();
        SetText(doorProtocol);
        if (isOpenButton) TypeOpenDoor();
        else TypeCloseDoor();

        if (protocol >= doorProtocol)
        {
            isInteractable = true;
            button.GetComponent<Image>().color = firstColor;
            button.GetComponent<Button>().interactable = true;
        }
        else
        {
            isInteractable = false;
            button.GetComponent<Image>().color = secondColor;
            button.GetComponent<Button>().interactable = false;
            padScreen.SetCloseButtonToNavigate();
            return;
        }

        this.btnEffectClosed.fillAmount = isOpenButton ? 1 : 0;
        this.btnEffectOpen.fillAmount = isOpenButton ? 0 : 1;
        StartCoroutine(ShowingButton(protocol >= doorProtocol));
    }

    public void HideButton()
    {
        EraseTexts();
        if (isInteractable)
            StartCoroutine(HidingButton(false));
    }

    #region Texts Methods
    public void SetText(int doorProtocol)
    {

        string door = "";

        if (protocol >= doorProtocol)
        {
            door = connectedText.text + doorPanel.GetDoorName().ToUpper();
            text.GetComponent<TextAnimation>().Type(door);

        }

        else
        {
            door = connectionDeniedText.text;
            textDenied.GetComponent<TextAnimation>().Type(door);
        }


    }

    public void EraseTexts()
    {
        textDenied.text = "";
        text.text = "";
        buttonText.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void TypeOpenDoor()
    {
        buttonText.Type(textOpenDoor.text);
    }

    public void TypeCloseDoor()
    {
        buttonText.Type(textCloseDoor.text);
    }

    #endregion


    IEnumerator ShowingButton(Image btn)
    {
        float factor = 0;

        while (factor < 1)
        {
            factor += speed * Time.deltaTime;
            btn.fillAmount = factor;

            if (factor >= 1)
            {
                btn.fillAmount = 1;
            }
            yield return null;
        }
    }

    IEnumerator HidingButton(Image btn)
    {
        float factor = 1;

        while (factor > 0)
        {
            factor -= speed * Time.deltaTime;
            btn.fillAmount = factor;

            if (factor <= 0)
            {
                btn.fillAmount = 0;
                button.SetActive(false);
            }
            yield return null;
        }


    }




    IEnumerator TogglingDoorButton()
    {
        //  this.GetComponent<Animator>().enabled = false;
        float factor = 0;


        while (factor < 1)
        {
            factor += toggleButtonSpeed * Time.deltaTime;

            if (!isOpenButton)
            {

                btnEffectOpen.fillClockwise = true;
                btnEffectClosed.fillClockwise = false;
                btnEffectClosed.fillAmount = 1 - factor;
                btnEffectOpen.fillAmount = factor;

                if (factor >= 1)
                {
                    btnEffectClosed.fillAmount = 0;
                    btnEffectOpen.fillAmount = 1;
                }
            }
            else
            {
                btnEffectOpen.fillClockwise = false;
                btnEffectClosed.fillClockwise = true;
                btnEffectClosed.fillAmount = factor;
                btnEffectOpen.fillAmount = 1 - factor;

                if (factor >= 1)
                {
                    btnEffectClosed.fillAmount = 1;
                    btnEffectOpen.fillAmount = 0;
                }

            }
            yield return null;
        }

        StartCoroutine(HidingButton(true));

    }

    IEnumerator HidingButton(bool closePad)
    {
        button.GetComponent<Animator>().enabled = false;
        //  this.GetComponent<Animator>().enabled = false;
        float factor = 0;

        Vector3 startScale = initialScale;
        Vector3 finalScale = Vector3.zero;

        while (factor <= 1)
        {
            factor += speed * Time.deltaTime;

            button.transform.localScale = Vector3.Lerp(startScale, finalScale, factor);
            if (factor >= 1)
            {
                button.transform.localScale = Vector3.zero;

            }

            yield return null;
        }

        screenClosed = true;
        if (closePad)
            padScreen.CloseScreen();

    }

    IEnumerator ShowingButton(bool interactable)
    {
        float factor = 0;

        Vector3 startScale = Vector3.zero;
        Vector3 finalScale = initialScale;

        while (factor <= 1)
        {
            factor += speed * Time.deltaTime;

            button.transform.localScale = Vector3.Lerp(startScale, finalScale, factor);
            if (factor >= 1)
            {
                button.transform.localScale = finalScale;

            }

            yield return null;
        }
        button.GetComponent<Animator>().enabled = true;

        EventSystem.current.SetSelectedGameObject(button);



    }


}
