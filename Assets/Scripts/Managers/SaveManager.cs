using UnityEngine;

public static class SaveManager
{
    const string levelCooker = "levelCooker";
    const string levelProgrammer = "levelProgrammer";
    const string levelOwner = "levelOwner";
    const string sfxVolume = "sfx";
    const string musicVolume = "music";
    const string mouseState = "mouseState";
    const string fullscreenState = "fullscreenState";
    const string selectedLanguage = "selectedLanguage";



    public static void SaveLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("Level", levelIndex);
        PlayerPrefs.Save();
    }
    public static int LoadLevel()
    {
        return PlayerPrefs.GetInt("Level", 2);
    }



    public static void SaveSFXVolume(float volumeLevel)
    {
        PlayerPrefs.SetFloat(sfxVolume, volumeLevel);
        PlayerPrefs.Save();
    }
    public static float LoadSFXVolume()
    {
        var currentVolume = PlayerPrefs.GetFloat(sfxVolume, 1);
        return currentVolume;
    }


    public static void SaveMusicVolume(float volumeLevel)
    {
        PlayerPrefs.SetFloat(musicVolume, volumeLevel);
        PlayerPrefs.Save();
    }
    public static float LoadMusicVolume()
    {
        var currentVolume = PlayerPrefs.GetFloat(musicVolume, 1);
        return currentVolume;
    }



    public static void SaveMouseState(int state)
    {
        PlayerPrefs.SetInt(mouseState, state);
        PlayerPrefs.Save();
    }
    public static int LoadMouseState()
    {
        var currentMouseState = PlayerPrefs.GetInt(mouseState);
        return currentMouseState;
    }



    public static void SaveSelectedLanguage(string language)
    {
        PlayerPrefs.SetString(selectedLanguage, language);
        PlayerPrefs.Save();
    }
    public static string LoadSelectedLanguage()
    {
        var currentLanguage = PlayerPrefs.GetString(selectedLanguage, "english");
        return currentLanguage;
    }



    public static void SaveFullscreenState(int state)
    {
        PlayerPrefs.SetInt(fullscreenState, state);
        PlayerPrefs.Save();
    }
    public static int LoadFullscreenState()
    {
        var screenState = PlayerPrefs.GetInt(fullscreenState);
        return screenState;
    }



    public static void ResetSavings()
    {
        PlayerPrefs.DeleteAll();
    }
}
