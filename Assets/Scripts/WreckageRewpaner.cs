using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckageRewpaner : MonoBehaviour
{
    [SerializeField] GameObject wreckagePrefab;
    [SerializeField] float interval;
    float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= interval)
        {
            time = 0;
            GameObject w = Instantiate(wreckagePrefab, this.transform.position, Quaternion.identity); ;
            Destroy(w, 20);
        }
    }

  
}
