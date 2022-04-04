using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;

public class Menu : MonoBehaviour
{
    public static Menu Instance;
    [SerializeField] GameObject menu;
    public bool menuOpened;

    [SerializeField] Slider musicVolume;
    [SerializeField] Slider sfxVolume;
    [SerializeField] Toggle fullScreen;
    [SerializeField] TMP_Dropdown language;

    private void Awake()
    {
        Instance = this;
        // menu.SetActive(false);

    }
   // private void Start()
    //{
       

        
    //}

    IEnumerator Start()
    {
        SetMusicVolume(SaveManager.LoadMusicVolume());
        SetSFXVolume(SaveManager.LoadSFXVolume());
        SetFullScreenToggle();
        yield return LocalizationSettings.InitializationOperation;
        SetLanguage();
    }
    public void OpenMenu()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
        menuOpened = true;
    }

    public void CloseMenu()
    {

        Time.timeScale = 1;
        menu.SetActive(false);
        // player.ClosePad();
        menuOpened = false;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume.value = volume;
    }

    public void ChangeMusicVolume()
    {
        float volume = musicVolume.value;
        SoundManager.Instance.ChangeBGMVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume.value = volume;
    }

    public void ChangeSFXVolume()
    {
        float volume = sfxVolume.value;
        SoundManager.Instance.ChangeSFXVolume(volume);
    }

    public void ToggleFullScreen()
    {
        GameManager.Instance.ToggleFullScreen(fullScreen.isOn);
    }

    public void SetFullScreenToggle()
    {
        fullScreen.isOn = GameManager.isFullScreen;
    }

    public void ChangeLanguage()
    {

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[language.value];
        SaveManager.SaveSelectedLanguage(language.value == 0 ? "english" : "portugues");
    }

    public void SetLanguage()
    {
        string l = SaveManager.LoadSelectedLanguage();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[l == "english" ? 0 : 1];
        language.value = l == "english" ? 0 : 1;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
