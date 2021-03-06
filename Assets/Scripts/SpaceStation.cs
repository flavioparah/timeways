using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStation : MonoBehaviour
{
    [SerializeField] float angularSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, angularSpeed * Time.deltaTime), Space.World);
    }

    public void SetAngularSpeed(float s)
    {
        angularSpeed = s;
    }
}
