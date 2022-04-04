using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] int points;
    [SerializeField] float amplitude;
    [SerializeField] float period;
    [SerializeField] float pointInterval;
    [SerializeField] float speed;
    [SerializeField] float k;

    float time;
    Vector3[] positions;
    LineRenderer wave;

    // Start is called before the first frame update
    void Start()
    {
        wave = this.GetComponent<LineRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * speed;
        //if(time >= amplitude)
        //{
        //    time = 0;
        //}
        CreatePoints();
        Senoid();

    }


    void Senoid()
    {
        wave.positionCount = points;
        wave.SetPositions(positions);
    }

    void CreatePoints()
    {
        List<Vector3> positionsList = new List<Vector3>();
        float x = 0;
        Debug.Log(points);
        for(int i = 0; i < points; i++)
        {
            Vector3 p = new Vector3(x, GetY(x));
            positionsList.Add(p);
            x += pointInterval;
        }

        positions = positionsList.ToArray();
    }

    float GetY(float x)
    {
        float y = amplitude * Mathf.Sin(x * period - (k *time)) ;
        return y;
    }
}
