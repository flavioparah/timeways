using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wreckage : MonoBehaviour
{
    [SerializeField] Enums.WreckageType type;
    [SerializeField] float speedX;
    [SerializeField] float speedY;
    [SerializeField] float angularSpeed;
    int isClockwise;
    bool onHold;

    Rigidbody2D rb;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       // if (type == Enums.WreckageType.fast) speed = speed * 2;
        isClockwise = Random.Range(0, 2) == 1 ? 1 : -1;
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onHold) return;
        transform.Rotate(new Vector3(0, 0, angularSpeed * isClockwise * Time.deltaTime), Space.World);
        transform.Translate(new Vector2(-speedX, -speedY) * Time.deltaTime, Space.World);
    }

    public Enums.WreckageType GetWreckageType()
    {
        return type;
    }
    public void Get()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        onHold = true;
        if(anim != null)
        {
            anim.enabled = false;
        }
    }

    public void Throw(Vector2 direction, float force)
    {
       // this.GetComponent<BoxCollider2D>().enabled = true;
        onHold = false;
        rb.AddForce(direction * force);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Wreckage")
    //    {

    //    }
    //}
}
