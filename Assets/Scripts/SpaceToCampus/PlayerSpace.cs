using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpace : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float angularSpeed;
    [SerializeField] float force;
    [SerializeField] Transform hook;
    [SerializeField] Transform jetPack;
    [SerializeField] Transform station;
    [SerializeField] Transform antenna;

    [SerializeField] float minSizePlayer;
    [SerializeField] float maxSizePlayer;
    [SerializeField] float minSizeBone;
    [SerializeField] float maxSizeBone;

    InputReader inputReader;
    Rigidbody2D rb;
    Vector2 direction;
    List<Transform> bones = new List<Transform>();
    bool movingRight;
    bool movingLeft;
    bool movingUp;
    bool movingDown;

    float maxDistance;
    [SerializeField] bool mainMenu;


    SolarPanel solarPanel;
    private void Awake()
    {

    }
    private void Start()
    {
        SetBones();
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

        if (hook != null)
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

        if (!mainMenu)
            SetSize();
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

    void Interact()
    {
        if(solarPanel != null)
        {
            solarPanel.Access();
        }
    }
    void SetBones()
    {
        Transform rope = hook.parent;

        for (int i = 1; i <= 20; i++)
        {
            bones.Add(rope.GetChild(i));
        }

    }

    void SetSize()
    {
        float distance = Vector2.Distance(this.transform.position, station.position);

        float s = ((maxSizePlayer - minSizePlayer) * (maxDistance - distance) / maxDistance) + minSizePlayer;
        s = Mathf.Clamp(s, minSizePlayer, maxSizePlayer);
        Vector3 size = new Vector3(s, s, s);
        this.transform.localScale = size;

        // SetBonesSize();
    }

    void SetBonesSize()
    {
        foreach (Transform b in bones)
        {
            float distance = Vector2.Distance(b.position, station.position);
            float s = ((maxSizeBone - minSizeBone) * (maxDistance - distance) / maxDistance) + minSizeBone;
            s = Mathf.Clamp(s, minSizeBone, maxSizeBone);
            Vector3 size = new Vector3(s, s, s);
            b.transform.localScale = size;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "SolarPanel")
        {
            solarPanel = collision.GetComponent<SolarPanel>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SolarPanel")
        {
            solarPanel = null;
        }
    }


}
