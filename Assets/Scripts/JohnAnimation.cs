using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer rigged;
    [SerializeField] SpriteRenderer idle;
    [SerializeField] SpriteRenderer turn;
    [SerializeField] GameObject padLeft;
    [SerializeField] GameObject padRight;

    bool facingRight = true;
    Animator anim;
    Player player;

    float time;
    float factor;
    bool hiding;
    bool showing;
    Color color;

    SpriteRenderer johnSpriteRenderer;

    Enums.Interaction interaction;

    //ZoomOut
    [SerializeField] Transform center;
    [SerializeField] float minSize;
    [SerializeField] float zoomSpeed;
    [SerializeField] GameObject zoomOutButton;
    [SerializeField] GameObject zoomedCam;

    bool hasJetPack;
    // Start is called before the first frame update
    void Awake()
    {
        color = Color.white;
        anim = this.GetComponent<Animator>();
        player = this.transform.parent.GetComponent<Player>();
        johnSpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hiding)
        {
            factor -= Time.deltaTime;

            color.a = factor;
            idle.color = color;
            johnSpriteRenderer.color = color;
            rigged.color = color;
            if (factor <= 0)
            {
                color.a = 0;
                hiding = false;
                idle.color = color;
                johnSpriteRenderer.color = color;
                rigged.color = color;
            }
        }
        if (showing)
        {
            factor += Time.deltaTime;

            color.a = factor;
            idle.color = color;
            johnSpriteRenderer.color = color;
            rigged.color = color;
            if (factor >= 1)
            {
                color.a = 1;
                idle.color = color;
                johnSpriteRenderer.color = color;
                rigged.color = color;
                showing = false;
            }
        }
    }

    public bool TurnAround(bool lookRight)
    {
        if (facingRight == lookRight) return false;
        facingRight = lookRight;
        // turning = true;
        rigged.GetComponent<SpriteRenderer>().flipX = !facingRight;
        this.GetComponent<SpriteRenderer>().flipX = !facingRight;
        idle.GetComponent<SpriteRenderer>().flipX = !facingRight;
        return true;
        // anim.SetTrigger("Turn");
    }


    public void StartWalk()
    {
        // EnableRigged();
        anim.SetBool("Walk", true);
    }


    public void Fall()
    {
        anim.SetTrigger("Fall");
    }

    public void DisableAnimator()
    {
        anim.enabled = false;
    }

    public void StopWalk()
    {
        //EnableTurn();
        //  DisableSprites();
        anim.SetBool("Walk", false);
    }

    public void EndTimeline()
    {
        rigged.transform.localPosition = Vector3.zero;
        rigged.transform.localRotation = Quaternion.Euler(Vector3.zero);
        anim.SetTrigger("EndTimeline");
    }

    public void DisableSprites()
    {
        rigged.enabled = false;
        turn.enabled = false;
        idle.enabled = false;
    }

    public void EnableRigged()
    {
        DisableSprites();
        rigged.GetComponent<SpriteRenderer>().flipX = !facingRight;
        rigged.enabled = true;
    }

    public void EnableTurn()
    {
        DisableSprites();
        turn.GetComponent<SpriteRenderer>().flipX = !facingRight;
        turn.enabled = true;
    }

    public void EnableIdle()
    {
        DisableSprites();
        idle.GetComponent<SpriteRenderer>().flipX = !facingRight;
        idle.enabled = true;
    }
    public void ActivePad()
    {
        anim.SetTrigger("ActivePad");
        anim.SetBool("PadActive", true);
    }

    public void OpenPad()
    {
        player.OpenPad();
    }

    public void HidePlayer()
    {
        color.a = 1;
        factor = 1;

        hiding = true;
    }

    public void ShowPlayer()
    {
        color.a = 0;
        factor = 0;
        showing = true;
    }
    public void DeactivatePad()
    {
        anim.SetBool("PadActive", false);
    }

    public void Inception()
    {
        anim.SetTrigger("Inception");
    }

    public void StopInception()
    {
        anim.SetTrigger("Inception2");
    }

    public void CheckPadSide()
    {
        padLeft.SetActive(rigged.GetComponent<SpriteRenderer>().flipX);
        padRight.SetActive(!rigged.GetComponent<SpriteRenderer>().flipX);
    }
  
    public void DisablePad()
    {
        padLeft.SetActive(false);
        padRight.SetActive(false);
        player.StopPadInteraction();
    }

    public void StartInteract(Enums.Interaction interaction )
    {
        this.interaction = interaction;
        anim.SetBool("Interact", true);

    }

    public void StopInteract()
    {
        anim.SetBool("Interact", false);
    }

    public void PerformInteraction()
    {
        if(interaction == Enums.Interaction.zoomOutButton)
        {
            zoomOutButton.SetActive(false);
            StopInteract();
            ZoomOut();
                
        }

        if (interaction == Enums.Interaction.Ulises)
        {
            player.UlisesInteraction();
        }

        if(interaction == Enums.Interaction.ExitHatch)
        {
            player.OpenHatch();
        }
        }

    void ZoomOut()
    {
        StartCoroutine(ZoomingOut());
    }

    IEnumerator ZoomingOut()
    {
        float factor = 0;

        Vector3 startScale = player.transform.localScale;
        Vector3 finalScale = new Vector3(minSize, minSize, minSize);
        Vector3 startPos = player.transform.position;
        Vector3 finalPos = center.transform.position;
        bool zoomed = false;

        while(factor < 1)
        {
            factor += zoomSpeed * Time.deltaTime;
            player.transform.localScale = Vector3.Lerp(startScale, finalScale, factor);
            player.transform.position = Vector3.Lerp(startPos, finalPos, factor);

            if(factor >= .75f && !zoomed)
            {
                zoomedCam.SetActive(true);
                zoomed = true;
            }

            yield return null;
        }

        

        GameManager.Instance.EndLevel();
    }

  

   
}
