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
    [SerializeField] bool isMainWave;
    [SerializeField] float incSpeed;

    Wave mainWave;

    float time;
    Vector3[] positions;
    LineRenderer wave;

    // Start is called before the first frame update
    void Start()
    {
        wave = this.GetComponent<LineRenderer>();
        if (!isMainWave)
        {
            mainWave = this.transform.parent.Find("MainWave").GetComponent<Wave>();
        }

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

        CheckWaves();
    }


    void Senoid()
    {
        wave.positionCount = points;
        wave.SetPositions(positions);
    }

    void CreatePoints()
    {
        List<Vector3> positionsList = new List<Vector3>();
        float x = 200;
        Debug.Log(points);
        for (int i = 0; i < points; i++)
        {
            Vector3 p = new Vector3(x, GetY(x));
            positionsList.Add(p);
            x += pointInterval;
        }

        positions = positionsList.ToArray();
    }

    float GetY(float x)
    {
        float y = amplitude * Mathf.Sin(x * period - (k * time));
        return y;
    }

    //-----------------------------------------------

    public float GetSpeed()
    {
        return speed;
    }

    public float GetPeriod()
    {
        return period;
    }

    public float GetAmplitude()
    {
        return amplitude;
    }

    public void SetSpeed(bool increasing)
    {
        if (isMainWave) return;
        speed += (increasing ? incSpeed : -incSpeed);
    }

    public void SetPeriod(bool increasing)
    {
        if (isMainWave) return;
        period += (increasing ? incSpeed : -incSpeed);
    }

    public void SetAmplitude(bool increasing)
    {
        if (isMainWave) return;
        amplitude += (increasing ? incSpeed : -incSpeed);
    }

    public void CheckWaves()
    {
        if (isMainWave) return;
        float p = mainWave.GetPeriod();
        float a = mainWave.GetAmplitude();
        float s = mainWave.GetSpeed();
        float factor = .05f;
        if (period > p - factor && period < p - factor)
        {
            if (amplitude > a - factor && amplitude < a + factor)
            {
                if (speed > s - factor && speed < s + factor)
                {
                    Debug.Log("you win!!!!!!");
                }
            }
        }
    }
}
