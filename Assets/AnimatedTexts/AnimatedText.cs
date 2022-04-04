using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedText : MonoBehaviour
{
    [SerializeField] string text;
    [SerializeField] int linesQuantity;
    [SerializeField] Canvas letterCanvasPrefab;
    [SerializeField] float interval;
    [SerializeField] float fontSize;
    [SerializeField] bool automatic;
    [SerializeField] List<Letter> letters = new List<Letter>();
    int characterCount;

    Vector2 firstPosition;
    Vector2 lastPosition;

    [SerializeField]
    float width;
    float height;

    int totalLettersInLine;

    int lettersDestroyed;
    // Start is called before the first frame update
    void Start()
    {

        if (!automatic)
            SetLetters();
        else
            StartTyping();



    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetPositions()
    {
        Bounds bounds = this.GetComponent<SpriteRenderer>().bounds;
        float x = bounds.center.x - bounds.extents.x;
        float y = bounds.center.y + bounds.extents.y;

        firstPosition = new Vector2(x, y);

        x = bounds.center.x + bounds.extents.x;
        y = bounds.center.y - bounds.extents.y;

        lastPosition = new Vector2(x, y);

    }

    void GetLetterSize()
    {
        int totalLetters = text.Length;
        characterCount = totalLetters;
        lettersDestroyed = 0;

        totalLettersInLine = totalLetters / linesQuantity;
        float sizeX = lastPosition.x - firstPosition.x;
        float sizeY = firstPosition.y - lastPosition.y;

        width = sizeX / totalLettersInLine;
        height = sizeY / linesQuantity;

    }

    public void StartTyping()
    {
        if(!automatic)
        {
            StartCoroutine(ShowingReadyLetters());
            return;
        }
        GetPositions();
        GetLetterSize();
        StartCoroutine(Typing());

    }

    public void SetText(string text)
    {
        this.text = text;
    }
    IEnumerator Typing()
    {
        char[] characters = text.ToCharArray();
        int index = 0;
        int indexAux = 0;
        int lastIndex = characters.Length - 1;
        Vector3 position = firstPosition;// this.transform.position;
        Vector3 lastPosition = position;
        float lastWidth = 1.09f;
        float lastHeight = height;
        float lastCanvasX = firstPosition.x;
        float lastCanvasY = firstPosition.y;
        bool typingWord = true;
        List<Canvas> aux = new List<Canvas>();
        while (index <= lastIndex)
        {
            Canvas letterCanvas = Instantiate(letterCanvasPrefab); //, position, Quaternion.identity);
            letterCanvas.transform.SetParent(this.transform);
            letterCanvas.transform.position = firstPosition;
            letterCanvas.worldCamera = Camera.main;
            TextMeshProUGUI letter = letterCanvas.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            // letter.transform.position = position;
            letter.text = characters[index].ToString();
            aux.Add(letterCanvas);

            // float width = LayoutUtility.GetMinWidth(letter.rectTransform);
            //letter.rectTransform.sizeDelta = new Vector2(width, 0);
            //float height = LayoutUtility.GetPreferredHeight(letter.rectTransform);
            letter.rectTransform.sizeDelta = new Vector2(width, height);
            letter.fontSize = fontSize;
            letterCanvas.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

            float newPositionX = lastCanvasX + lastWidth / 2 + width / 2;

            float newPositionY = lastCanvasY;


            if (indexAux > totalLettersInLine)
            {
                lastCanvasX = firstPosition.x;
                newPositionX = lastCanvasX + lastWidth / 2 + width / 2;
                newPositionY = lastCanvasY - lastHeight / 2 - height / 2;
                bool firstTime = true;
                foreach (Canvas c in aux)
                {
                    newPositionX = lastCanvasX + lastWidth / 2 + width / 2;
                    if (firstTime) c.GetComponent<RectTransform>().position = new Vector2(lastCanvasX, newPositionY);
                    c.GetComponent<RectTransform>().position = new Vector2(newPositionX, newPositionY);
                    firstTime = false;
                    
                }
                indexAux = 0;
            }


            if (characters[index] == ' ')
            {
                aux.Clear();
                // newPositionX += lastWidth;
            }

            letterCanvas.GetComponent<RectTransform>().position = index == 0 ? new Vector2(lastCanvasX, lastCanvasY) : new Vector2(newPositionX, newPositionY);

            // Debug.DrawLine(new Vector2(newPosition, 0), new Vector2(newPosition, 0) + Vector2.up, Color.blue, 50);

            lastCanvasX = letterCanvas.GetComponent<RectTransform>().position.x;
            lastCanvasY = letterCanvas.GetComponent<RectTransform>().position.y;
            lastWidth = width;
            lastHeight = height;
            // position.x = lastPosition.x + offset;
            // position.x += letter.GetComponent<Collider2D>().bounds.extents.x * 2;



            index++;
            indexAux++;
            yield return new WaitForSeconds(interval);

        }
    }

    public void IncreaseLettersDestroyed()
    {
        lettersDestroyed++;

        if (lettersDestroyed >= characterCount)
        {
            if (this.transform.parent != null)
                this.transform.parent.GetComponent<TextManager>().SetNextText();
            else
                Destroy(this.gameObject);
        }
    }

    void SetLetters()
    {
        foreach (Transform t in this.transform)
        {
            Letter l = t.GetComponent<Letter>();
            letters.Add(l);
        }

        characterCount = letters.Count;
        lettersDestroyed = 0;
        StartTyping();
    }

    IEnumerator ShowingReadyLetters()
    {
        int index = 0;
        int total = letters.Count;
        while (index < total)
        {
            letters[index].FadeIn();
            index++;
            yield return new WaitForSeconds(interval);
        }
    }
}
