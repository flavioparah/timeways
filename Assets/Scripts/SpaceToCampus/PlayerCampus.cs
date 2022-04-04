using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCampus : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float angularSpeed;
    [SerializeField] GameObject mask;
    [SerializeField] float force;
    [SerializeField] Transform hook;
    [SerializeField] Transform jetPack;
    InputReader inputReader;
    Rigidbody2D rb;
    Vector2 direction;

    bool movingRight;
    bool movingLeft;
    bool movingUp;
    bool movingDown;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();


    }

    private void OnEnable()
    {
        inputReader = this.GetComponent<InputReader>();
        inputReader.EnableSpace();
        inputReader.spaceMoveEvent += Move;
        inputReader.cancelSpaceMoveEvent += CancelMove;
    }
    private void OnDisable()
    {
        inputReader.spaceMoveEvent -= Move;
        inputReader.cancelSpaceMoveEvent -= CancelMove;
    }

    private void Update()
    {
        if (direction != Vector2.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, direction), angularSpeed * Time.deltaTime);
            // transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }

        if(hook != null)
        {
            hook.transform.position = jetPack.position;
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

    }
    private void FixedUpdate()
    {
        // rb.velocity = direction * speed;
    }
    private void Move(Vector2 move)
    {
        direction = move;

        movingRight = move.x > 0;
        movingLeft = move.x < 0;
        movingUp = move.y > 0;
        movingDown = move.y < 0;
    }

    private void CancelMove()
    {
        movingRight = false;
        movingLeft = false;
        movingUp = false;
        movingDown = false;
        direction = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (direction != Vector2.zero && collision.tag == "Mask")
            collision.transform.GetChild(0).gameObject.SetActive(true);

    }
    bool CheckMask()
    {
        LayerMask mask = LayerMask.GetMask("Mask");
        Collider2D[] cols = Physics2D.OverlapCircleAll((Vector2)transform.position, .4f, mask);
        return cols.Length > 2;
    }
    void InstantiateMask()
    {
        Instantiate(mask, transform.position, Quaternion.identity);
    }


}
