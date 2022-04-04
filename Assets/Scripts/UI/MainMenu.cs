using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject optionsButton;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject volumeMusic;
    [SerializeField] GameObject answerNoBox;
    [SerializeField] GameObject closeOptionBtn;
    Animator anim;
    int level;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        level = SaveManager.LoadLevel();
        Navigation nav;
        if (level > 2)
        {
            continueButton.SetActive(true);

            EventSystem.current.SetSelectedGameObject(continueButton);
            nav = startButton.GetComponent<Button>().navigation;

            nav.selectOnUp = continueButton.GetComponent<Button>();
            startButton.GetComponent<Button>().navigation = nav;
            return;
        }
        continueButton.SetActive(false);
        nav = startButton.GetComponent<Button>().navigation;

        nav.selectOnUp = null;
        startButton.GetComponent<Button>().navigation = nav;
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame(bool sure = false)
    {
        if (level > 2 && !sure)
        {
            EventSystem.current.SetSelectedGameObject(answerNoBox);
            anim.SetTrigger("OpenBox");
            return;
        }
        GameManager.Instance.StartGame();
    }

    public void ContinueGame()
    {
        GameManager.Instance.ContinueGame();
    }

    public void Options()
    {
        EventSystem.current.SetSelectedGameObject(closeOptionBtn);
        anim.SetTrigger("Options");
    }

    public void CloseOptions()
    {
        if (level > 2)
            EventSystem.current.SetSelectedGameObject(continueButton);
        else
            EventSystem.current.SetSelectedGameObject(startButton);
        anim.SetTrigger("CloseOptions");
    }

    public void NotSure()
    {
        EventSystem.current.SetSelectedGameObject(continueButton);
        anim.SetTrigger("CloseBox");
    }
}
