using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ScreenTransition : MonoBehaviour
{
    public static ScreenTransition Instance;

    #region Inspector
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed = 5f;
    // [SerializeField] private List<Color> colors = new List<Color>();
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TextMeshProUGUI dayTextNumber;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] List<TextMeshProUGUI> levelTexts = new List<TextMeshProUGUI>();
    #endregion

    #region Members
    private CanvasGroup canvasGroup;
    private bool fading = false;
    private float fadeTarget = 0f;
    private float elapsedTime = 0;
    private float canvasAlpha = 1f;
    bool onGame;
    #endregion

    #region Actions
    public static Action OnComplete;
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            canvasGroup = transform.GetChild(0).GetComponent<CanvasGroup>();
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            // GameManager.onClick += FadeIn;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        // SetFadeColor(scene.buildIndex);
        //if (scene.buildIndex != 0)
        //{
        //     ToggleLevelText(true);
        //}

        FadeIn();

    }

    private void Update()
    {
        if (fading)
        {
            elapsedTime += Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = Mathf.Lerp(canvasAlpha, fadeTarget, elapsedTime);
            if (elapsedTime >= 1f)
            {

                canvasGroup.alpha = fadeTarget;
                fading = false;
                elapsedTime = 0;
                if (!onGame)
                    OnComplete?.Invoke();

                if (fadeTarget == 0)
                {
                    ToggleLevelText(false);
                }
            }
        }
    }

    public void SetFadeColor(int index)
    {
        if (fadeImage != null)
        {
            index = Mathf.Clamp(index, 0, 1);
            //fadeImage.color = colors[index];
        }
    }


    public void FadeOut(bool onGame = false)
    {
        this.onGame = onGame;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 0f;
        canvasAlpha = canvasGroup.alpha;
        fadeTarget = 1f;
        fading = true;
    }


    public void FadeIn()
    {
        ToggleLevelText(false);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 1f;
        canvasAlpha = canvasGroup.alpha;
        fadeTarget = 0;
        fading = true;
    }

    public void SetLevelText(int levelNumber)
    {
        int day = levelNumber;
        switch (day)
        {
            case 2:
                day = 1;
                levelNumber = 0;
                break;

            case 3:
                day = 26;
                break;
        
        }

        dayTextNumber.text =  " "+ day.ToString();
        levelText.gameObject.SetActive(false);
        levelText = levelTexts[levelNumber];
        levelText.gameObject.SetActive(true);
    }

    public void ToggleLevelText(bool isOn)
    {

        levelText.gameObject.SetActive(isOn);
        dayText.gameObject.SetActive(isOn);
        dayTextNumber.gameObject.SetActive(isOn);
    }

    public bool isFading()
    {
        return fading;
    }
}