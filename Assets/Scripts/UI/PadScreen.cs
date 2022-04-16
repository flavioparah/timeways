using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PadScreen : MonoBehaviour
{

    public bool screenOpened;

    #region Inspector
    #region UIElements
    [SerializeField] Image mapBG;
    [SerializeField] Image ulisesBG;
    [SerializeField] Image suitBG;
    [SerializeField] Image closeBtn;
    [SerializeField] RectTransform mapTab;
    [SerializeField] RectTransform ulisesTab;
    [SerializeField] RectTransform suitTab;
    [SerializeField] RectTransform connectionTab;
    #endregion

    [SerializeField] DoorConnection doorConnection;
    [SerializeField] ElevatorConnection elevatorConnection;
    [SerializeField] UlisesConnection ulisesConnection;
    [SerializeField] AntennaConnection antennaConnection;

    [SerializeField] float speedBg;
    [SerializeField] float speedBtn;
    [SerializeField] float speedTabs;

    [SerializeField] Pad pad;
    [SerializeField] bool firstTime;
    #endregion

    #region Members
    Button buttonToNavigate;

    Image actualBg;
    Image nextBG;

    Enums.Screen actualScreen;
    Enums.Screen nextScreen;

    bool animating;
    Animator anim;

    Connection connection;
    Connection container;

    #endregion

    void Start()
    {
        anim = this.GetComponent<Animator>();
        if (!firstTime) pad.GrantPlayerAccess();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OpenScreen(Panel panel)
    {
        screenOpened = true;


        if (firstTime)
        {
            StartCoroutine(Loading());
            EventSystem.current.SetSelectedGameObject(closeBtn.gameObject);
            actualScreen = Enums.Screen.ulises;
            return;
        }
        else if (panel == null)
        {
            actualBg = ulisesBG;
            actualScreen = Enums.Screen.ulises;
            container = ulisesConnection;
            ulisesConnection.Show();
        }
        else if (panel.GetConnection() == Enums.Connection.door)
        {
            actualBg = doorConnection.GetBG();
            doorConnection.Show(panel);
            connection = doorConnection;

        }
        else if (panel.GetConnection() == Enums.Connection.elevator)
        {
            actualBg = elevatorConnection.GetBG();
            elevatorConnection.Show(panel);
            connection = elevatorConnection;
        }

        else if (panel.GetConnection() == Enums.Connection.antenna)
        {
            actualBg = antennaConnection.GetBG();
            antennaConnection.Show(panel);
            connection = antennaConnection;
        }

        connectionTab.GetComponent<Button>().interactable = false;
        StartCoroutine(turningScreenOn(true));
    }

    public void CloseScreen()
    {
        screenOpened = false;
        StartCoroutine(turningScreenOn(false));
    }

    public void HidePad()
    {
        pad.ClosePad();
    }



    public void OpenTab(int tabNumber)
    {
        if (animating) return;

        Enums.Screen screen = (Enums.Screen)tabNumber;
        string tab = screen.ToString();

        if (tab == "map")
        {
            nextScreen = Enums.Screen.map;
            StartCoroutine(ChangeTabs(mapBG));
        }
        else if (tab == "ulises")
        {
            nextScreen = Enums.Screen.ulises;
            StartCoroutine(ChangeTabs(ulisesBG));
        }
        else if (tab == "suit")
        {
            nextScreen = Enums.Screen.suit;
            StartCoroutine(ChangeTabs(suitBG));
        }
        else if (tab == "connection")
        {
            nextScreen = Enums.Screen.connection;
            StartCoroutine(ChangeTabs(connection.GetBG())); ;

            connectionTab.GetComponent<Button>().interactable = false;
        }
    }

    IEnumerator turningScreenOn(bool turnOn)
    {
        float factor = 0;
        bool startButtonAnimation = false;
        bool startTabsAnimation = false;

        if (turnOn)
        {
            actualBg.fillAmount = 0;
            while (factor < 1)
            {
                factor += speedBg * Time.deltaTime;
                actualBg.fillAmount = factor;

                if (!startButtonAnimation && factor > .7f)
                {
                    StartCoroutine(ToggleButton(true));
                    startButtonAnimation = true;
                }
                if (!startTabsAnimation && factor >= .82f)
                {
                    StartCoroutine(ToggleTabs(true));
                    startTabsAnimation = true;
                }

                if (factor >= 1)
                {
                    actualBg.fillAmount = 1;
                }

                yield return null;
            }
        }
        else
        {
            if (connection != null)
                connection.Hide();
            if (container != null)
                container.Hide();
            factor = 1;
            while (factor > 0)
            {
                factor -= speedBg * Time.deltaTime;
                actualBg.fillAmount = factor;

                if (!startButtonAnimation && factor > .7f)
                {
                    StartCoroutine(ToggleButton(false));
                    startButtonAnimation = true;
                }
                if (!startTabsAnimation && factor >= .82f)
                {
                    StartCoroutine(ToggleTabs(false));
                    startTabsAnimation = true;
                }

                if (factor >= 1)
                {
                    actualBg.fillAmount = 0;
                }

                yield return null;
            }
            HidePad();
        }


    }

    IEnumerator ToggleButton(bool turnOn)
    {
        float factor = 0;
        if (turnOn)
        {
            while (factor < 1)
            {
                factor += speedBtn * Time.deltaTime;
                closeBtn.fillAmount = factor;

                if (factor >= 1)
                {
                    closeBtn.fillAmount = 1;
                }

                yield return null;
            }
        }
        else
        {
            factor = 1;
            while (factor > 0)
            {
                factor -= speedBtn * Time.deltaTime;
                closeBtn.fillAmount = factor;

                if (factor >= 1)
                {
                    closeBtn.fillAmount = 0;
                }

                yield return null;
            }
        }





    }

    IEnumerator ToggleTabs(bool turnOn)
    {
        mapTab.GetComponent<Animator>().enabled = false;
        ulisesTab.GetComponent<Animator>().enabled = false;
        suitTab.GetComponent<Animator>().enabled = false;
        connectionTab.GetComponent<Animator>().enabled = false;

        float factor = 0;
        Vector3 start = turnOn ? Vector3.zero : new Vector3(.3f, .25f, .3f);
        Vector3 final = turnOn ? new Vector3(.3f, .25f, .3f) : Vector3.zero;


        while (factor < 1)
        {
            factor += speedTabs * Time.deltaTime;

            mapTab.localScale = Vector3.Lerp(start, final, factor);
            ulisesTab.localScale = Vector3.Lerp(start, final, factor);
            suitTab.localScale = Vector3.Lerp(start, final, factor);
            connectionTab.localScale = Vector3.Lerp(start, final, factor);


            if (factor >= 1)
            {
                mapTab.localScale = final;
                ulisesTab.localScale = final;
                suitTab.localScale = final;
                connectionTab.localScale = final;

                mapTab.GetComponent<Animator>().enabled = turnOn;
                ulisesTab.GetComponent<Animator>().enabled = turnOn;
                suitTab.GetComponent<Animator>().enabled = turnOn;
                connectionTab.GetComponent<Animator>().enabled = turnOn;
            }

            yield return null;
        }


    }

    IEnumerator ToggleBackground(bool turnOn, Image background)
    {
        float factor = 0;

        if (turnOn)
        {
            if (actualScreen == Enums.Screen.connection) connection.Show();
            else container.Show();
            background.fillAmount = 0;

            while (factor < 1)
            {
                factor += speedBg * Time.deltaTime;
                background.fillAmount = factor;

                if (factor >= 1)
                {
                    background.fillAmount = 1;
                }

                yield return null;
            }
            actualBg = background;
            animating = false;
        }

        else
        {
            if (actualScreen == Enums.Screen.connection) connection.Hide();
            else container.Hide();
            factor = 1;

            while (factor > 0)
            {
                factor -= speedBg * Time.deltaTime;
                background.fillAmount = factor;

                if (factor <= 0)
                {
                    background.fillAmount = 0;
                }

                yield return null;
            }

            actualScreen = nextScreen;
        }
    }
    IEnumerator ChangeTabs(Image newTab)
    {
        animating = true;
        if (connection != null) connection.Hide();
        StartCoroutine(ToggleBackground(false, actualBg));

        yield return new WaitForSeconds(.5f);

        StartCoroutine(ToggleBackground(true, newTab));
    }

    public Button GetButtonToNavigate()
    {
        return buttonToNavigate;
    }

    public void SetButtonToNavigate(Button button)
    {
        buttonToNavigate = button;
    }

    IEnumerator Loading()
    {
        ulisesConnection.OpenLoadingScreen();

        yield return new WaitForSeconds(4.5f);

        firstTime = false;
        OpenScreen(null);
    }
    public void EnableConnectionTab()
    {
        connectionTab.GetComponent<Button>().interactable = true;
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            SetSelectedButton();
    }
    public void SetSelectedButton()
    {
        if (screenOpened)
            EventSystem.current.SetSelectedGameObject(mapTab.gameObject);
    }
}
