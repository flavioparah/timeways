using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kite : MonoBehaviour
{
    [SerializeField] InputReader InputReader;

    [SerializeField] float speed;
    [SerializeField] float maxSize;
    [SerializeField] float minSize;
    [SerializeField] Transform child;
    [SerializeField] AreaEffector2D wind;

    Collider2D col;
    Vector2 direction;
    LineRenderer lr;
    Vector2 orientation;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(1, 1);
        col = this.GetComponent<Collider2D>();
        lr = this.GetComponent<LineRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(direction * speed * Time.deltaTime, Space.World);
        rb.velocity = direction * speed;
        Vector3[] positions = { child.position, this.transform.position };
        lr.SetPositions(positions);
        SetSize();

        transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(Vector3.forward, orientation), 10 * Time.deltaTime); ;
    }

    private void OnEnable()
    {
        if (InputReader != null)
        {
            InputReader.kiteMoveEvent += Move;
            InputReader.kiteCancelMoveEvent += MoveCancel;
            InputReader.pauseEvent += Pause;
        }
    }

    private void OnDisable()
    {

        if (InputReader != null)
        {
            InputReader.kiteMoveEvent -= Move;
            InputReader.kiteCancelMoveEvent -= MoveCancel;
            InputReader.pauseEvent -= Pause;
        }
    }
    private void Move(Vector2 move)
    {
        this.col.enabled = true;
        direction = move;
        orientation = move;

        if (move.y > 0)
        {
            if (move == Vector2.up)
            {
                wind.forceAngle = -90;
            }
            else if (move == Vector2.up + Vector2.right)
            {
                wind.forceAngle = -135;
            }
            else if (move == Vector2.up + Vector2.left)
            {
                wind.forceAngle = -45;
            }
            return;
        }
        wind.forceAngle = Vector2.Angle(Vector2.right, -orientation);





    }
    void MoveCancel()
    {
        this.col.enabled = false;
        direction = Vector2.zero;
        orientation = Vector2.up;
        wind.forceAngle = -90;
    }

    private void Pause()
    {

    }

    void SetSize()
    {
        float screenX = Screen.width;
        float screenY = Screen.height;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(screenX, screenY));

        float maxY = worldPosition.y;

        float size = (maxSize - minSize) * (maxY - this.transform.position.y) / maxY;
        this.transform.localScale = new Vector3(size, size, size);
    }
}
