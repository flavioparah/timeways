using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Inspector
    [SerializeField] float nextLevelWaitTime;
    [SerializeField] float gameOverWaitTime;
    [SerializeField] List<Sprite> shipSprites = new List<Sprite>();
    #endregion

    #region Members
    public static GameManager Instance;

    public static bool Game_Over = false;

    public static int shipState;

    public static bool isMainMenu;

    bool waiting;
    int scene;
    public static bool isFullScreen;

    #endregion

    #region Actions
    public static Action endLevel;
    public static Action toggledFullScreen;
    #endregion
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            SetFullScreen();

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        ScreenTransition.OnComplete += StopWaiting;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        ScreenTransition.OnComplete -= StopWaiting;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.scene = scene.buildIndex;

        isMainMenu = scene.name == "MainMenu";

        if (isMainMenu)
        {
            SoundManager.Instance.Play(AudioTypes.BGM_MainTheme);
            SoundManager.Instance.Stop(AudioTypes.SFX_Ambience);
            SoundManager.Instance.Stop(AudioTypes.BGM_Theme);
        }

        if(scene.name == "StationArrival")
        {
            SoundManager.Instance.Stop(AudioTypes.BGM_MainTheme);
            SoundManager.Instance.Play(AudioTypes.SFX_Ambience);
            SoundManager.Instance.Play(AudioTypes.BGM_Theme);
        }
        //SoundManager.Instance.MuteBGM(true);
        if (scene.name == "Station")
        {
            LoadShipState();
            SoundManager.Instance.Play(AudioTypes.SFX_Ambience);
            SoundManager.Instance.Stop(AudioTypes.BGM_MainTheme);
            SoundManager.Instance.Play(AudioTypes.BGM_Theme);
        }
    }
    private void Start()
    {
        // scene = 1;
    }
    public void EndLevel()
    {
        Game_Over = false;
        endLevel?.Invoke();

        StartCoroutine(WaitNextLevel(nextLevelWaitTime));
    }

    public void NextLevel()
    {
        scene++;

        SceneManager.LoadScene(scene);
    }

    public void SetShipState(int number)
    {
        number--;
        shipState = number;
    }

    public void SaveShipState()
    {
        PlayerPrefs.SetInt("shipState", shipState);
    }

    void LoadShipState()
    {
        shipState = PlayerPrefs.GetInt("shipState", 3);
        GameObject.Find("SpaceShip").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetSpaceShipSprite();
    }

    Sprite GetSpaceShipSprite()
    {
        return shipSprites[shipState];

    }
    public void GameOver()
    {
        Game_Over = true;
        ScreenTransition.Instance.ToggleLevelText(true);
        StartCoroutine(WaitGameOverScreen(gameOverWaitTime));
    }

    public void Retry()
    {
        SceneManager.LoadScene(scene);
    }

    void StopWaiting()
    {
        waiting = false;
    }
    IEnumerator WaitNextLevel(float waitTime)
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);

        ScreenTransition.Instance.FadeOut();

        yield return new WaitUntil(() => !waiting);
        yield return new WaitForSeconds(1);

        NextLevel();
    }

    public void StartGame()
    {
        StartCoroutine(WaitStartGame());

    }

    public void ContinueGame()
    {
        int n = SaveManager.LoadLevel();
        scene = n--;
        StartCoroutine(WaitNextLevel(0));
    }
    IEnumerator WaitStartGame()
    {
        ScreenTransition.Instance.FadeOut();

        yield return new WaitUntil(() => !waiting);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }

    IEnumerator WaitToMainMenu()
    {
        ScreenTransition.Instance.FadeOut();

        yield return new WaitUntil(() => !waiting);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator WaitGameOverScreen(float waitTime)
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);

        ScreenTransition.Instance.FadeOut();

        yield return new WaitUntil(() => !waiting);
        yield return new WaitForSeconds(3);


        Retry();
    }

    public void SetFullScreen()
    {
        isFullScreen = SaveManager.LoadFullscreenState() == 1;
        Screen.fullScreen = isFullScreen;
        if (isFullScreen) Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        else Screen.fullScreenMode = FullScreenMode.Windowed;
    }
    public void ToggleFullScreen(bool isFull)
    {
        Debug.Log("full screen" + isFull);
        isFullScreen = isFull;
        Screen.fullScreen = isFullScreen;
        if (isFullScreen) Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        else Screen.fullScreenMode = FullScreenMode.Windowed;
        SaveManager.SaveFullscreenState(isFullScreen ? 1 : 0);
        toggledFullScreen?.Invoke();
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(WaitToMainMenu());
    }
}
