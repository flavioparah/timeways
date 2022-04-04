using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    Vector2 direction;
    Rigidbody2D rb;
    [SerializeField] float force;
    [SerializeField] float fadeSpeed;
    [SerializeField] float scaleSpeed;
    [SerializeField] float angularSpeed;
    [SerializeField] float fadeinSpeed;

    [SerializeField] bool isReal;
    

  //  Transform letter;

    bool fadeStarted;
    Vector3 size;
    Quaternion finalRot;
    bool done = false;
    private void OnEnable()
    {
        GameManager.endLevel += StartFade;
        
    }

    private void OnDisable()
    {
        GameManager.endLevel -= StartFade;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        finalRot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
        //letter = this.transform.GetChild(0);
       // size = letter.transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isReal) return;
        if (!fadeStarted) return;

        this.GetComponent<CanvasGroup>().alpha -= fadeSpeed * Time.deltaTime;

        if(this.GetComponent<CanvasGroup>().alpha <= 0 && !done)
        {
            done = true;
            this.transform.parent.GetComponent<AnimatedText>().IncreaseLettersDestroyed();
            Destroy(this.gameObject);
        }
      //  letter.transform.localScale = Vector3.Lerp(size, Vector3.zero, scaleSpeed * Time.deltaTime);
      //  transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(Vector3.forward, (transform.rotation * finalRot).eulerAngles), angularSpeed * Time.deltaTime);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = this.transform.position - collision.transform.position;
        rb.AddForce(direction * force);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // size = .sizeDelta;
        direction = this.transform.position - collision.transform.position;
        rb.AddForce(direction * force);
        StartFade();
    }

  public void StartFade()
    {
        fadeStarted = true;
    }

    public void FadeIn()
    {
        StartCoroutine(FadingIn());
    }

    IEnumerator FadingIn()
    {
        float alphaTarget = 1;
        float factor = 0;

        while(factor < alphaTarget)
        {
            factor += fadeinSpeed * Time.deltaTime;
            this.GetComponent<CanvasGroup>().alpha = factor;

            if(factor >= alphaTarget)
            {
                this.GetComponent<CanvasGroup>().alpha = 1;
            }

            yield return null;
        }

    }
}
