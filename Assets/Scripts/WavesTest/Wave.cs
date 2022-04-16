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

    float amplitudeTime;
    float periodTime;
    float amplitudeInterval;
    float periodInterval;

    bool periodBlocked;
    bool amplitudeBlocked;
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
        amplitudeInterval = 0f;
        periodInterval = .05f;
        periodBlocked = false;
        amplitudeBlocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * speed;
        amplitudeTime += Time.deltaTime;
        periodTime += Time.deltaTime;
        if (periodTime >= periodInterval)
        {

            periodBlocked = false;
        }

        if (amplitudeTime >= amplitudeInterval)
        {

            amplitudeBlocked = false;
        }
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
        if (periodBlocked) return;
        if (isMainWave) return;
        period += (increasing ? incSpeed : -incSpeed);
        period = Mathf.Clamp(period, -5, 5);
        periodBlocked = true;
        periodTime = 0;
    }

    public void SetAmplitude(bool increasing)
    {
        if (amplitudeBlocked) return;
        if (isMainWave) return;
        amplitude += (increasing ? incSpeed : -incSpeed);
        amplitude = Mathf.Clamp(amplitude, -10, 10);
        amplitudeBlocked = true;
        amplitudeTime = 0;
    }

    public void CheckWaves()
    {
        if (isMainWave) return;
        float p = mainWave.GetPeriod();
        float a = mainWave.GetAmplitude();
        float s = mainWave.GetSpeed();
        float factor = .05f;
        if (period > p - factor && period < p + factor)
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
