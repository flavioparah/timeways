using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
   // [SerializeField] GameObject menu;
    public bool menuOpened;
    Animator anim;
    [SerializeField] GameObject musicVolume;

    [SerializeField] GameObject CloseBtn;
    [SerializeField] GameObject noBtn;
    [SerializeField] GameObject mainMenuBtn;
    private void Awake()
    {
        Instance = this;
      //  menu.SetActive(false);
    }

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }
    public void OpenMenu()
    {
        anim.SetTrigger("Open");
        Time.timeScale = 0;
       // menu.SetActive(true);
        menuOpened = true;
        EventSystem.current.SetSelectedGameObject(CloseBtn);
    }

    public void CloseMenu()
    {

        Time.timeScale = 1;
        //  menu.SetActive(false);
        anim.SetTrigger("Close");
       // player.ClosePad();
        menuOpened = false;
    }


    public void MainMenu()
    {
        CloseMenu();
        GameManager.Instance.ReturnToMainMenu();
    }

    public void OpenPopUp()
    {
        anim.SetTrigger("OpenBox");

        EventSystem.current.SetSelectedGameObject(noBtn);
    }

    public void ClosePopUp()
    {
        anim.SetTrigger("CloseBox");

        EventSystem.current.SetSelectedGameObject(mainMenuBtn);
    }

    public void ClickSound()
    {
        SoundManager.Instance.Play(AudioTypes.SFX_MenuClick);
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
