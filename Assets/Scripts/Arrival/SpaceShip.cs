using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] InputReader InputReader;
    [SerializeField] GameObject leftParticles;
    [SerializeField] GameObject rightParticles;
    [SerializeField] GameObject topParticles;
    [SerializeField] GameObject bottomParticles;
    [SerializeField] float force;
    [SerializeField] float speed;
    [SerializeField] GameObject maskEntrance;
    [SerializeField] GameObject lights;
    [SerializeField] GameObject headlights;
    [SerializeField] GameObject john;
    [SerializeField] ArrivalCam arrivalCam;

    [SerializeField] Sprite broken1;
    [SerializeField] Sprite broken2;
    [SerializeField] Sprite broken3;
    [SerializeField] GameObject explosion;
    Sprite sprite;
    SpriteRenderer spriteRenderer;
    int lives;

    Rigidbody2D rb;
    float move;
    bool movingRight;
    bool movingLeft;
    bool movingUp;
    bool movingDown;

    bool arrived;
    bool gameOver;

    private void OnEnable()
    {
        if (InputReader != null)
        {
            InputReader.moveEvent += Move;
            InputReader.moveEventCancel += MoveCancel;
            //InputReader.interactEvent += Interact;
            //InputReader.mousePositionEvent += MousePosition;
            //InputReader.mouseDownEvent += Click;
            //InputReader.mouseUpEvent += MouseUp;
            InputReader.pauseEvent += Pause;
            //InputReader.changeCameraEvent += ChangeCamera;


        }
    }

    private void OnDisable()
    {

        if (InputReader != null)
        {
            InputReader.moveEvent -= Move;
            InputReader.moveEventCancel -= MoveCancel;

            //InputReader.interactEvent -= Interact;
            //InputReader.mousePositionEvent -= MousePosition;
            //InputReader.mouseDownEvent -= Click;
            //InputReader.mouseUpEvent -= MouseUp;
            InputReader.pauseEvent -= Pause;
            //InputReader.changeCameraEvent -= ChangeCamera;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * 10);
        spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprite = spriteRenderer.sprite;
        lives = 4;
        explosion.SetActive(false);
        GameManager.Instance.SetShipState(lives);
    }

    // Update is called once per frame
    void Update()
    {

        if (PauseMenu.Instance.menuOpened) return;
        if (arrived || gameOver)
        {
            //HUD.Instance.UpdateSpeed();
            rb.velocity = Vector2.zero;
            return;
        }

        if (movingRight)
        {
            rb.AddForce(Vector2.right * force);
        }
        else if (movingLeft)
        {
            rb.AddForce(Vector2.left * force);
        }

        if (movingUp)
        {
            rb.AddForce(Vector2.up * force);
        }

        else if (movingDown)
        {
            rb.AddForce(Vector2.down * force);
        }

        float speed = this.GetComponent<Rigidbody2D>().velocity.magnitude;
        HUD.Instance.UpdateSpeed(speed);
    }

    private void Move(Vector2 move)
    {
       

        // this.move =  ? 1 : -1;
        movingRight = move.x > 0;
        movingLeft = move.x < 0;
        movingUp = move.y > 0;
        movingDown = move.y < 0;

        if (arrived) return;
        SoundManager.Instance.Play(AudioTypes.SFX_Propulsor);
        leftParticles.SetActive(movingRight);
        rightParticles.SetActive(movingLeft);
        topParticles.SetActive(movingUp);
        bottomParticles.SetActive(movingDown);
    }

    void MoveCancel()
    {
       
        movingRight = false;
        movingLeft = false;
        movingUp = false;
        movingDown = false;

        leftParticles.SetActive(movingRight);
        rightParticles.SetActive(movingLeft);
        topParticles.SetActive(movingUp);
        bottomParticles.SetActive(movingDown);

        SoundManager.Instance.Stop(AudioTypes.SFX_Propulsor);
    }

    private void Pause()
    {
        if (PauseMenu.Instance.menuOpened)
        {
            PauseMenu.Instance.CloseMenu();
            return;
        }
        PauseMenu.Instance.OpenMenu();
    }

    void CheckPosition()
    {
        if (true)
        {
            Debug.Log("ChangeScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "StartAnimation")
        {
            // maskEntrance.SetActive(true);
            //  lights.SetActive(true);
            headlights.SetActive(true);
        }

        if (collision.tag == "EndPart")
        {
            CheckPosition();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Limit")
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Station")
        {
            if (this.rb.velocity.magnitude > 1f)
            {
                arrivalCam.StartShake();
              
                BreakShip();
            }


        }
    }

    void BreakShip()
    {
        SoundManager.Instance.Play(AudioTypes.SFX_ShipHit);
        lives--;
        if (lives == 3)
        {
            spriteRenderer.sprite = broken1;

            headlights.transform.GetChild(0).gameObject.SetActive(false);
            
        }

        else if (lives == 2)
        {
            spriteRenderer.sprite = broken2;
            headlights.transform.GetChild(1).gameObject.SetActive(false);
        }

        else if (lives == 1)
        {
            spriteRenderer.sprite = broken3;
        }

        else if (lives == 0)
        {
            gameOver = true;
            spriteRenderer.sprite = null;
            explosion.SetActive(true);
            StartCoroutine(ExplosionFade());
            GameManager.Instance.GameOver();
        }

        GameManager.Instance.SetShipState(lives);

    }

    public bool IsGameOver()
    {
        return gameOver;
    }
    public void SetArrived()
    {
        arrived = true;
        john.SetActive(true);
        headlights.SetActive(false);
        arrivalCam.StopShake(true);
        GameManager.Instance.SaveShipState();
    }

    IEnumerator ExplosionFade()
    {

        yield return new WaitForSeconds(0.07f);
        SpriteRenderer sr = explosion.GetComponent<SpriteRenderer>();

        float factor = 1;
        float speed = 2;
        while(factor > 0)
        {
            factor -= speed * Time.deltaTime;
            Color c = sr.color;
            c.a = factor;
            sr.color = c;

            yield return null;
        }
    }
}



