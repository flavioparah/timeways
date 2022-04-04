using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Ulises : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Player player;
    [SerializeField] Pad pad;
    [SerializeField] UlisesConnection ulisesPad;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] CinemachineVirtualCamera cam;

    [SerializeField] Image balloon;
    [SerializeField] TextAnimation text;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject wifiSymbol;

    [SerializeField] GameObject tutorial;
    [SerializeField] List<TextMeshProUGUI> lines;
    int lineIndex;
    bool connected;
    bool waitAction;

    // Start is called before the first frame update
    void Start()
    {
        pad.padClosed += ConnectionClosed;
        lineIndex = 0;
        tutorial.SetActive(false);
        continueButton.GetComponent<Button>().interactable = false;
    }

    private void OnDisable()
    {
        pad.padClosed -= ConnectionClosed;
    }

    void ConnectionClosed()
    {
        if (connected)
        {

            GetNextLine();
        }
    }
    public void GetNextLine()
    {

        lineIndex++;
        if (lineIndex == 3)
        {
            player.GrantPadAccess();
            waitAction = true;
        }

        if (lineIndex == 4)
        {
            waitAction = false;
        }

        if (lineIndex == 5)
        {
            wifiSymbol.SetActive(false);
        }

        if (lineIndex == 6)
        {
            StopTalk();
            player.CloseUlises();
        }
        if (lineIndex >= lines.Count) return;

        Talk(lines[lineIndex].text);

    }
    // Update is called once per frame
    void Update()
    {

    }

    void TurnOn()
    {
        tutorial.SetActive(true);
        StartCoroutine(Toggling(true));
    }

    void TurnOff()
    {
        StartCoroutine(Toggling(false));
    }

    public void Show()
    {
        tutorial.SetActive(false);
        cam.gameObject.SetActive(true);

        StartCoroutine(WaitCamera());
    }

    public void Talking(bool isTalking)
    {
        anim.SetBool("Talking", isTalking);
    }

    public void Hide()
    {
        anim.SetBool("Open", false);
        cam.gameObject.SetActive(false);
    }

    public void StartTalk()
    {
        StartCoroutine(ToggleBalloon(true));
    }

    void Talk(string text)
    {
        Talking(true);
        continueButton.GetComponent<Button>().interactable = false;
        this.text.Type(text);
        this.text.typeFinished += FinishSentence;

    }

    public void FinishSentence()
    {
        Talking(false);
        this.text.typeFinished -= FinishSentence;

        if (lineIndex == 4)
            wifiSymbol.SetActive(true);

        if (waitAction) return;

        continueButton.GetComponent<Button>().interactable = true;
        EventSystem.current.SetSelectedGameObject(continueButton);
    }
    public void StopTalk()
    {
        Talking(false);
        this.text.Erase();
        StartCoroutine(ToggleBalloon(false));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            connected = true;
            tutorial.SetActive(true);
            collision.GetComponent<Player>().ConnectUlises(this);
            TurnOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            tutorial.SetActive(false);
            collision.GetComponent<Player>().DisconnectUlises();
            TurnOff();
            connected = false;
        }
    }

    IEnumerator Toggling(bool isOn)
    {
        float factor = 0;

        if (isOn)
        {
            while (factor < 1)
            {
                factor += speed * Time.deltaTime;
                Color c = Color.white;
                c.a = factor;
                sprite.color = c;

                yield return null;
            }
        }
        else
        {
            factor = 1;
            while (factor > 0)
            {
                factor -= speed * Time.deltaTime;
                Color c = Color.white;
                c.a = factor;
                sprite.color = c;

                yield return null;
            }


            tutorial.SetActive(false);
        }

    }

    IEnumerator ToggleBalloon(bool turnOn)
    {
        float factor = 0;

        if (turnOn)
        {
            balloon.fillAmount = 0;

            while (factor < 1)
            {
                factor += speed * Time.deltaTime;
                balloon.fillAmount = factor;



                if (factor >= 1)
                {
                    balloon.fillAmount = 1;
                    // Talking(true);
                    string text = "";
                    if (lineIndex >= lines.Count)
                    {
                        Talk("...");
                    }
                    else
                    {
                        text = lines[lineIndex].text;
                        Talk(text);
                    }

                }

                yield return null;
            }
        }


        else
        {

            factor = 1;

            while (factor > 0)
            {
                factor -= speed * Time.deltaTime;
                balloon.fillAmount = factor;


                if (factor <= 0)
                {
                    balloon.fillAmount = 0;
                    Hide();
                }

                yield return null;
            }


        }
    }

    IEnumerator WaitCamera()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Open", true);
        yield return new WaitForSeconds(.25f);
        StartTalk();
    }

    //ÌEnumerator WaitCameraToClose()
    //{

    //}

}
