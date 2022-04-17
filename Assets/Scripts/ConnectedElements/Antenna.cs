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

    public WavePuzzleManager GetCurrentPuzzle()
    {
        return currentPuzzle;
    }
    public Transform GetAntennaPoint()
    {
        CameraManager.Instance.ChangeCamera(antennaCam);


        for(int i = 1; i< puzzlesPoints.Count; i++)
        {
            currentPuzzle = puzzlesPoints[i];
            if (!PuzzleComplete(currentPuzzle))
            {
                i = puzzlesPoints.Count;
            }
                
        }
       
        
       
        SetLightColor(Color.yellow);
        return antennaPoint;
    }

    public bool GetNextPoint(WavePuzzleManager puzzle)
    {
        
        int index = puzzlesPoints.IndexOf(puzzle);
        if (index == 0) return false;
        index++;

        if(index >= puzzlesPoints.Count)
        {
            SetLightColor(Color.red);
            return false;
        }
        WavePuzzleManager nextPuzzle = puzzlesPoints[index];
        if (PuzzleComplete(nextPuzzle))
        {
            return GetNextPoint(nextPuzzle);
        }
        SetLightColor(Color.red);
        currentPuzzle = puzzlesPoints[index];
        SetLightColor(Color.yellow);
        return true;
    }

    public bool GetPreviousPoint(WavePuzzleManager puzzle)
    {
        int index = puzzlesPoints.IndexOf(puzzle);
        index--;

        if (index < 1 )
        {
            SetLightColor(Color.red);
            return false;
        }

        WavePuzzleManager previousPuzzle = puzzlesPoints[index];
        if (PuzzleComplete(previousPuzzle))
        {            
            return GetPreviousPoint(previousPuzzle);
        }

        SetLightColor(Color.red);
        currentPuzzle = puzzlesPoints[index];
        SetLightColor(Color.yellow);
        return true;
    }

    void SetLightColor(Color color)
    {
        if (color != Color.green && PuzzleComplete()) return;
        currentPuzzle.transform.Find("Light").GetComponent<Light2D>().color = color;
    }

    public void OpenPuzzle(WavePanel panel)
    {
        wavePanel = panel;
        panel.SetWavePuzzle(currentPuzzle);
    }

    public bool PuzzleComplete(WavePuzzleManager puzzle = null)
    {
        if (puzzle == null) puzzle = currentPuzzle;
        return puzzle.IsComplete();
    }

    public void SetOpen()
    {
        if(anim == null) anim = this.GetComponent<Animator>();
        anim.SetTrigger("Opened");
        baseCollider.enabled = false;
        antennaCollider.enabled = false;
        foreach(WavePuzzleManager puzzle in puzzlesPoints)
        {
            currentPuzzle = puzzle;
            SetLightColor(Color.green);

        }
    }

    public void BaseComplete()
    {
        baseCollider.enabled = false;
    }

    public void NotBaseComplete()
    {

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

        player.ObjectiveCompleted(true);
        SetOpen();
        CameraManager.Instance.ChangeCamera(false, false);
    }

    public void SetPuzzleComplete(WavePuzzleManager puzzle)
    {
        currentPuzzle = puzzle;
        SetLightColor(Color.green);

        int index = puzzlesPoints.IndexOf(puzzle);
        if(index == 0)
        {
            player.LeaveWavePanel();
        }
        SetNewCurrentPuzzle();

        player.LeavePuzzle();
    }

    public void SetNewCurrentPuzzle()
    {
        int index = puzzlesPoints.IndexOf(currentPuzzle);
        bool puzzleFound = false;
        for (int i = 1; i < puzzlesPoints.Count; i++)
        {
            currentPuzzle = puzzlesPoints[i];
            if (!PuzzleComplete(currentPuzzle))
            {
                SetLightColor(Color.yellow);
                i = puzzlesPoints.Count;
                puzzleFound = true;
            }

        }

        if (!puzzleFound) player.LeaveWavePanel();
        else player.SetWavePuzzle(currentPuzzle);
    }
}
