using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class InceptionCamera : MonoBehaviour
{
    public static InceptionCamera Instance;
    [SerializeField] CinemachineVirtualCamera cam1;
    [SerializeField] CinemachineVirtualCamera cam2;
    [SerializeField] CinemachineVirtualCamera cam3;
    [SerializeField] CinemachineVirtualCamera cam4;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCamera1()
    {
        cam1.Priority = 1;
        cam2.Priority = 0;
        cam3.Priority = 0;
        cam4.Priority = 0;
    }
   

    public void SetCamera2()
    {
        cam2.Priority = 1;
        cam1.Priority = 0;
        cam3.Priority = 0;
        cam4.Priority = 0;
    }

    public void SetCamera3()
    {
        cam3.Priority = 1;
        cam2.Priority = 0;
        cam1.Priority = 0;
        cam4.Priority = 0;
    }
    public void SetCamera4()
    {
        cam4.Priority = 1;
        cam2.Priority = 0;
        cam3.Priority = 0;
        cam1.Priority = 0;
    }
}
