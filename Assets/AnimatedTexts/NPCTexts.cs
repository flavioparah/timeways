using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCTexts : MonoBehaviour
{
    [SerializeField] float interval;
    [SerializeField] List<string> texts = new List<string>();
    [SerializeField] TextMeshProUGUI line;
    [SerializeField] Image background;
    [SerializeField] Image arrow;

    [SerializeField] float visibleTime;
    int index = 0;
    bool talking;
    string textToTalk;
    // Start is called before the first frame update
    void Start()
    {
        HideBackground();
    }

    public void StartLine()
    {
        background.enabled = true;
        arrow.enabled = true;
        StartCoroutine(Typing());
    }
    public void StopLine()
    {
        background.enabled = false;
        arrow.enabled = false;
        StopCoroutine(Typing());
        line.text = "";
    }

    public void Talk(string text)
    {
        if (talking) StopCoroutine("Talking");
        background.enabled = true;
        arrow.enabled = true;
        textToTalk = text;
        StartCoroutine("Talking");
    }

    void HideBackground()
    {
        background.enabled = false;
        arrow.enabled = false;
        line.text = "";
        talking = false;
    }
    IEnumerator Typing()
    {
        char[] text = texts[index].ToCharArray();
        line.text = "";
        int charIndex = 0;

        while (charIndex < text.Length)
        {
            line.text += text[charIndex];
            yield return new WaitForSeconds(interval);

            charIndex++;

        }
    }

    IEnumerator Talking()
    {
        string text = textToTalk; 
        talking = true;
        line.text = "";
        int charIndex = 0;

        while (charIndex < text.Length)
        {
            line.text += text[charIndex];
            yield return new WaitForSeconds(interval);

            charIndex++;

        }

        yield return new WaitForSeconds(visibleTime);

        line.text = "";
        HideBackground();
    }
}
