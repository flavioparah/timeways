using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] string textToType;
    TextMeshProUGUI tm;
    [SerializeField] float interval;
    Coroutine lastCoroutine;
    public Action typeFinished;
    // Start is called before the first frame update
    void Start()
    {
        //tm = this.GetComponent<TextMeshProUGUI>();
        //if (textToType == "") textToType = tm.text;
        //tm.text = "";
        //StartCoroutine(Typing());
    }

    private void OnEnable()
    {
        //tm = this.GetComponent<TextMeshProUGUI>();
        //if (textToType == "") textToType = tm.text;
        //tm.text = "";
        //StartCoroutine(Typing());
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Type(string text)
    {
        tm = this.GetComponent<TextMeshProUGUI>();
        tm.text = "";
        textToType = text;
        lastCoroutine = StartCoroutine(Typing());
    }

    public void Erase()
    {
        if (lastCoroutine != null)
            StopCoroutine(lastCoroutine);
        tm = this.GetComponent<TextMeshProUGUI>();
        tm.text = "";
    }

    public void Type()
    {
        tm.text = "";
        StartCoroutine(Typing());
    }
    IEnumerator Typing()
    {
        string text = textToType;


        int charIndex = 0;

        while (charIndex < text.Length)
        {
            tm.text += text[charIndex];
            yield return new WaitForSeconds(interval);

            charIndex++;

        }

        typeFinished?.Invoke();

    }
}
