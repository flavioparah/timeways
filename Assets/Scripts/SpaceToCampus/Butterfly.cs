using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    [SerializeField] float speed;
    bool moving;
    Vector2 nextPosition;
    int index;
    List<Vector2> points = new List<Vector2>();

    private void Start()
    {
        foreach(Transform t in transform)
        {
            points.Add(t.position);
        }
        index = 0;
        nextPosition = points[index];
        moving = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!moving) return;

        transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

    }

    void GetNextPoint()
    {
        index++;
        if (index >= points.Count) return;
        nextPosition = points[index];
        moving = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetNextPoint();
        }
    }
  
}
