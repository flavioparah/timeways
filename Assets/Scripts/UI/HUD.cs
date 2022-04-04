using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD Instance;


    [SerializeField] float speed;
    [SerializeField] GameObject lockedText;

    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI objectiveText;
    [SerializeField] Image imageBg;

    Color initialColor;
    [SerializeField] float speedLimit;
    bool upperSpeedLimit;

    [SerializeField] float intervalBlinkText;
    float time;
    bool textYellow;
    bool arrived;
    bool turnedAlarmOn;
    bool turnedAlarmOff;
    private void Awake()
    {
        Instance = this;
        speedText.text = "Slow Down";

    }

    // Start is called before the first frame update
    void Start()
    {
        //  ShowObjective();
        arrived = false;
        initialColor = imageBg.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (arrived) return;
        if (upperSpeedLimit)
        {
            objectiveText.enabled = false;
            speedText.enabled = true;
            imageBg.color = Color.red;

            time += Time.deltaTime;

            if (time >= intervalBlinkText)
            {
                time = 0;
                textYellow = !textYellow;
                if (textYellow)
                {
                    speedText.color = Color.yellow;
                }
                else
                {
                    speedText.color = Color.black;
                }
            }

        }
        else
        {

            imageBg.color = initialColor;
            objectiveText.enabled = true;
            speedText.enabled = false;
        }
    }

    public void ShowRadar()
    {

    }
    public void ShowLockedText()
    {
        imageBg.color = initialColor;
        objectiveText.enabled = false;
        speedText.enabled = false;
        lockedText.SetActive(true);
        arrived = true;

    }
    public void ShowObjective()
    {
        // StartCoroutine(ShowingObjective());
    }

    public void HideObjective()
    {

    }

    public void HideRadar()
    {

    }

    //IEnumerator ShowingObjective()
    //{
    //    RectTransform rect = objectives.GetComponent<RectTransform>();

    //    float startX = -rect.sizeDelta.x * 1.2f;
    //    float startY = Screen.height - rect.sizeDelta.y - Screen.height * .02f;

    //    float finalX = 0;// rect.sizeDelta.x / 2 + rect.sizeDelta.x * .02f; ;
    //    float finalY = startY;

    //    Vector2 start = new Vector2(startX, startY);
    //    Vector2 end = new Vector2(finalX, finalY);

    //    float factor = 0;

    //    while (factor < 1)
    //    {
    //        factor += speed * Time.deltaTime;
    //        rect.position = Vector2.Lerp(start, end, factor);


    //        yield return null;

    //    }
    //    finalX = -30;

    //    start = end;
    //    end.x = finalX;

    //    factor = 0;
    //    float speed2 = speed * 2.5f; 
    //    while (factor < 1)
    //    {
    //        factor += speed2 * Time.deltaTime;
    //        rect.position = Vector2.Lerp(start, end, factor);


    //        yield return null;

    //    }

    //}

    public void UpdateSpeed(float speed)
    {
        //  speedText.text = "Speed: " + speed.ToString("F2");

        upperSpeedLimit = speed >= speedLimit;

        if (upperSpeedLimit)
        {
            if (!turnedAlarmOn)
            {
                SoundManager.Instance.Play(AudioTypes.SFX_Alarme);
                turnedAlarmOn = true;
                turnedAlarmOff = false;
            }

            speedText.gameObject.SetActive(true);
        }
        else
        {
            if (!turnedAlarmOff)
            {
                SoundManager.Instance.Stop(AudioTypes.SFX_Alarme);
                turnedAlarmOn = false;
                turnedAlarmOff = true;
            }
            speedText.gameObject.SetActive(false);
        }

    }

}
