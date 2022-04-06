using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] Transform radarStation;
    [SerializeField] Transform player;
    [SerializeField] Transform station;
    [SerializeField] float speedLimit;

    [SerializeField] Transform farPointReal;
    [SerializeField] Transform farPointRadar;
    [SerializeField] Transform radarCenter;
  //  [SerializeField] ArrivalCam cam;
    float maxDistanceReal;
    float maxDistanceRadar;
    float radarScale;
   
    bool locked;

   
    private void Start()
    {
        SetDistances();
      

       
    }

    void SetDistances()
    {
        maxDistanceReal = Vector2.Distance(farPointReal.position, station.position);
        maxDistanceRadar = Vector2.Distance(farPointRadar.position, radarCenter.position);
        radarScale = (maxDistanceRadar) / maxDistanceReal;
        
    }
  
    private void Update()
    {
        if (locked) return;
        Vector3 directionReal = player.position - station.position;
        Vector3 directionRadar = GetRadarDirection(directionReal);

        radarStation.position =  directionRadar;

        if (!player.GetComponent<SpaceShip>().IsGameOver() && Vector2.Distance(radarStation.position, radarCenter.position) <= .005f && GetSpeed() < speedLimit)
        {
            locked = true;
            player.GetComponent<SpaceShip>().SetArrived();
            HUD.Instance.ShowLockedText();
            SoundManager.Instance.Play(AudioTypes.SFX_Encaixe);

            this.gameObject.SetActive(false);
        }
        // distanceX = Mathf.Abs(distanceX);
        //  SetRadarPosition(GetPercentage(), GetPercentageY());

    }


    Vector3 GetRadarDirection(Vector3 direction)
    {
        Vector3 radarDirection = direction;
        radarDirection = radarCenter.position + direction.normalized * (direction.magnitude * radarScale * -1);
        return radarDirection ;
    }
    //float GetPercentage()
    //{
    //    float p = (100 - (100 * Mathf.Abs(distanceX)) / Mathf.Abs(maxDistanceX));
    //    if (p > 98)
    //    {
    //        p = 100;
    //    }
    //    return p;
    //}

    //float GetPercentageY()
    //{
    //    float p = (100 - (100 * Mathf.Abs(distanceY)) / Mathf.Abs(maxDistanceY));
    //    if (p > 95)
    //    {
    //        p = 100;
    //    }
    //    return p;
    //}

    void SetRadarPosition(float percentage, float percentageY)
    {
        //int x = (int)(percentage / factor);
        //x -= 7;

        //int y = (int)(percentageY / factor);
        //y -= 7;

        //x = Mathf.Clamp(x, -7, 7);
        //y = Mathf.Clamp(y, -7, 7);
        //// Debug.Log(x);
        //Vector2 pos = radarStation.localPosition;
        //pos.x = distanceX <= 0 ? -x : x;
        //pos.y = distanceY <= 0 ? -y : y;

        //radarStation.localPosition = pos;

        //if (pos == Vector2.zero && GetSpeed() < speedLimit)
        //{
        //    locked = true;
        //    player.GetComponent<SpaceShip>().SetArrived();
        //    HUD.Instance.ShowLockedText();
        //}
    }

    float GetSpeed()
    {
        float speed = player.GetComponent<Rigidbody2D>().velocity.magnitude;
        return speed;
    }

}
