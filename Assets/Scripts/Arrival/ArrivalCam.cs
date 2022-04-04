using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class ArrivalCam : MonoBehaviour
{
    [SerializeField] Transform spaceShip;
    [SerializeField] Transform arrivalPoint;
    [SerializeField] float maxY;
    [SerializeField] float maxZoom;
    [SerializeField] float shakeIntensity;
    [SerializeField] float shakeTime;
    [SerializeField] Transform radar;

    float shakingTime;

    CinemachineVirtualCamera cam;
    float minY;
    float minZoom;
    float maxDistance;
    Vector2 radarPosition;
    Vector2 radarPositionScreen;
    bool shaking;
    bool endLevel;


    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<CinemachineVirtualCamera>();
        maxDistance = 4f;// Vector2.Distance(spaceShip.position, arrivalPoint.position);

        minZoom = cam.m_Lens.OrthographicSize;
        minY = cam.transform.position.y;
        UpdateRadarPosition();
    }
    private void OnEnable()
    {
        GameManager.toggledFullScreen += UpdateRadarPosition;
    }

    private void OnDisable()
    {
        GameManager.toggledFullScreen -= UpdateRadarPosition;
    }
    public void UpdateRadarPosition()
    {
        // radarPosition = radar.position;
        radarPositionScreen = GetRadarPositionScreen();
    }
    // Update is called once per frame
    void Update()
    {
        float percentage = GetProximityPercentage();
        SetZoom(percentage);
        

        if(shaking)
        {
            shakingTime += Time.deltaTime;
            if(shakingTime >= shakeTime)
            {
                
                StopShake();
            }
        }
    }

    private void FixedUpdate()
    {
        SetRadarPosition();
    }

    float GetProximityPercentage()
    {
        float distance = Vector2.Distance(spaceShip.position, arrivalPoint.position);
        if (distance > maxDistance)
        {
            return 0;
        }
        float p = 100 * (maxDistance - distance) / maxDistance;
        return p;
    }

    void SetZoom(float factor)
    {
        float y = ((100 * minY) - factor * (minY - maxY)) / 100;
        Vector3 pos = cam.transform.position;
        pos.y = y;
        cam.transform.position = pos;

        float zoom = ((100 * minZoom) - factor * (minZoom - maxZoom)) / 100;
        cam.m_Lens.OrthographicSize = zoom;

        float scale = ((0.5f * (zoom - maxZoom)) / (minZoom - maxZoom)) + .5f;
        radar.localScale = Vector3.one * scale;


    }

    void SetRadarPosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(radarPositionScreen);
        pos.z = 0;
        radar.position = pos;

      
    }

    Vector3 GetRadarPositionScreen()
    {
        Bounds bounds = radar.GetComponent<Collider2D>().bounds;

        Vector2 finalPointScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        radarPosition = finalPointScreen - (Vector2)bounds.extents;
        Debug.Log("RadarPosition = " + radarPosition);
        return Camera.main.WorldToScreenPoint(radarPosition);
    }
    public void StartShake()
    {
        if (endLevel) return;
        CinemachineBasicMultiChannelPerlin noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = shakeIntensity;
        shakingTime = 0;
        shaking = true;
    }

    public void StopShake(bool endLevel = false)
    {
        shaking = false;
        CinemachineBasicMultiChannelPerlin noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = 0;
        this.endLevel = endLevel;
    }
}
