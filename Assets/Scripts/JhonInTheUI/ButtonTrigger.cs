using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] GameObject btn;
    SpriteRenderer sr;
    Color color; 

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        color = sr.color;
    }

    void SetAlpha()
    {
        Color c = color;
        c.a += .33f;
        sr.color = c;
        color = sr.color;

        if(color.a >= 1)
        {
            btn.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("player");
            SetAlpha();
        }
    }
}
