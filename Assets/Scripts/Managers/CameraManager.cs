using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] CinemachineVirtualCamera mainCam;
    [SerializeField] CinemachineVirtualCamera spaceCam;
    [SerializeField] CinemachineVirtualCamera panelCam;
    [SerializeField] CinemachineVirtualCamera antenaCam;

    bool isPlayerCam = true;
    CinemachineVirtualCamera actualCam;
    private void Awake()
    {
        //if(Instance == null)
        //{
        Instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else
        //{
        //    Destroy(this.gameObject);
        //}
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeCamera()
    {
        isPlayerCam = !isPlayerCam;
        playerCam.gameObject.SetActive(isPlayerCam);
        mainCam.gameObject.SetActive(!isPlayerCam);
    }

    public void StopCamera()
    {
        spaceCam.Follow = null;
        // spaceCam.GetCinemachineComponent<>
    }

    public void ChangeCamera(bool isPanelCam, bool entering)
    {
        if (actualCam != null)
            actualCam.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(!entering);
        panelCam.gameObject.SetActive(isPanelCam && entering);
        antenaCam.gameObject.SetActive(!isPanelCam && entering);
    }

    public void ChangeCamera(CinemachineVirtualCamera camera)
    {
        if (actualCam != null)
            actualCam.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(false);
        panelCam.gameObject.SetActive(false);
        antenaCam.gameObject.SetActive(false);
        actualCam = camera;
        camera.gameObject.SetActive(true);
    }
}
