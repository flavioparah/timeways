using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Antenna : MonoBehaviour
{
    [SerializeField] List<WavePuzzleManager> puzzlesPoints = new List<WavePuzzleManager>();

    WavePuzzleManager currentPuzzle;
    [SerializeField] CinemachineVirtualCamera antennaCam;
    [SerializeField] Transform antennaPoint;
    [SerializeField] CinemachineVirtualCamera antennaBaseCam;
    [SerializeField] Transform antennaBasePoint;

    [SerializeField] Collider2D baseCollider;
    [SerializeField] Collider2D antennaCollider;
    WavePanel wavePanel;
    Animator anim;
    [SerializeField] PlayerSpace player;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetBasePoint()
    {
        CameraManager.Instance.ChangeCamera(antennaBaseCam);
        currentPuzzle = puzzlesPoints[0];
        SetLightColor(Color.yellow);
        return antennaBasePoint;
    }

    public Transform GetAntennaPoint()
    {
        CameraManager.Instance.ChangeCamera(antennaCam);
        currentPuzzle = puzzlesPoints[1];
        SetLightColor(Color.yellow);
        return antennaPoint;
    }

    public bool GetNextPoint()
    {
        int index = puzzlesPoints.IndexOf(currentPuzzle);
        if (index == 0) return false;
        index++;

        if(index >= puzzlesPoints.Count)
        {
            return false;
        }

        SetLightColor(Color.red);
        currentPuzzle = puzzlesPoints[index];
        SetLightColor(Color.yellow);
        return true;
    }

    public bool GetPreviousPoint()
    {
        int index = puzzlesPoints.IndexOf(currentPuzzle);
        
        index--;

        if (index < 1 )
        {
            return false;
        }

        SetLightColor(Color.red);
        currentPuzzle = puzzlesPoints[index];
        SetLightColor(Color.yellow);
        return true;
    }

    void SetLightColor(Color color)
    {
        currentPuzzle.transform.Find("Light").GetComponent<Light2D>().color = color;
    }

    public void OpenPuzzle(WavePanel panel)
    {
        wavePanel = panel;
        panel.SetWavePuzzle(currentPuzzle);
    }

    public bool PuzzleComplete()
    {
        return currentPuzzle.IsComplete();
    }

    public void SetOpen()
    {
        anim.SetTrigger("Opened");
        baseCollider.enabled = false;
        antennaCollider.enabled = false;
    }

    public void Open()
    {
        anim.SetTrigger("Open");
        StartCoroutine(WaitAnimation());
    }

    IEnumerator WaitAnimation()
    {
        player.SetWaitingState(true);
        yield return new WaitForSeconds(4f);
        player.SetWaitingState(false);
        SetOpen();
        CameraManager.Instance.ChangeCamera(false, false);
    }
}
