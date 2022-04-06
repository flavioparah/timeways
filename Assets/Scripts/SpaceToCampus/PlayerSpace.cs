using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpace : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float angularSpeed;
    [SerializeField] float force;
    [SerializeField] Transform station;
    [SerializeField] Transform antenna;

    [SerializeField] float minSizePlayer;
    [SerializeField] float maxSizePlayer;
    [SerializeField] float minSizeBone;
    [SerializeField] float maxSizeBone;

    InputReader inputReader;
    Rigidbody2D rb;
    Vector2 direction;
    bool movingRight;
    bool movingLeft;
    bool movingUp;
    bool movingDown;

    float maxDistance;
    [SerializeField] bool mainMenu;

    [SerializeField] GameObject particles;
    SolarPanel solarPanel;
    private void Awake()
    {

    }
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        maxDistance = Vector2.Distance(station.position, antenna.position);
        inputReader = this.GetComponent<InputReader>();
        inputReader.EnableSpace();
        inputReader.spaceMoveEvent += Move;
        inputReader.cancelSpaceMoveEvent += CancelMove;
        inputReader.interactEvent += Interact;

    }

    private void OnEnable()
    {


    }
    private void OnDisable()
    {
        inputReader.spaceMoveEvent -= Move;
        inputReader.cancelSpaceMoveEvent -= CancelMove;

        inputReader.interactEvent -= Interact;
    }

    private void Update()
    {
        if (direction != Vector2.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, direction), angularSpeed * Time.deltaTime);
            // transform.Translate(direction * speed * Time.deltaTime, Space.World);
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
        particles.SetActive(true);
    }

    private void CancelMove()
    {
        movingRight = false;
        movingLeft = false;
        movingUp = false;
        movingDown = false;
        direction = Vector2.zero;
        particles.SetActive(false);
    }

    void Interact()
    {
        if(solarPanel != null)
        {
            solarPanel.Access();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "SolarPanel")
        {
            solarPanel = collision.GetComponent<SolarPanel>();
            CameraManager.Instance.ChangeCamera();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SolarPanel")
        {
            solarPanel = null;
            CameraManager.Instance.ChangeCamera();
        }
    }


}
