using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance;
    [SerializeField] List<string> texts = new List<string>();
    [SerializeField] AnimatedText animatedTextPrefab;

    AnimatedText currentAnimatedText;
    int index = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FirstText();
       
    }

    public void SetNextText()
    {

        
        index++;
        if (index >= texts.Count)
        {
            Debug.Log("EndLevel");
            return;
        }
        Destroy(currentAnimatedText.gameObject);
        currentAnimatedText = Instantiate(animatedTextPrefab);
        currentAnimatedText.transform.SetParent(this.transform);
        string t = texts[index];
        currentAnimatedText.SetText(t);
        currentAnimatedText.StartTyping();
    }

    public void FirstText()
    {
        currentAnimatedText = Instantiate(animatedTextPrefab);
        currentAnimatedText.transform.SetParent(this.transform);
        string t = texts[index];
        currentAnimatedText.SetText(t);
        currentAnimatedText.StartTyping();
    }

    //IEnumerator Test()
    //{
    //    while(true)
    //    {
    //        yield return new WaitForSeconds(6f);
    //        SetNextText();
    //    }
       
    //}

}
