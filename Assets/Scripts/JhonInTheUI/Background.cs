using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform bg1;
    [SerializeField] Transform bg2;
    bool falling;
    // Start is called before the first frame update
    void Start()
    {
        falling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(falling)
        {
            bg1.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
            bg2.Translate(Vector2.up * speed * Time.deltaTime, Space.World);

            if(bg1.position.y > 8)
            {
                Vector3 pos = bg2.position;
                pos.y -= 8;
                bg1.position = pos;
            }
            if (bg2.position.y > 8)
            {
                Vector3 pos = bg1.position;
                pos.y -= 8;
                bg2.position = pos;
            }
        }
    }
}
