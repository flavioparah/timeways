using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SolarPanel : MonoBehaviour
{
    [SerializeField] Transform panels;
    [SerializeField] Transform panels2;
    List<Animator> anims = new List<Animator>();
    bool opened;

    [SerializeField] PlayerSpace player;
    [SerializeField] CinemachineVirtualCamera solarPanelCam;

    [SerializeField] List<Transform> points = new List<Transform>();

    List<bool> puzzlesComplete = new List<bool>();
    [SerializeField] List<PuzzleManager> puzzles = new List<PuzzleManager>();

    [SerializeField] GameObject mask;
    [SerializeField] GameObject mask2;
    [SerializeField] GameObject closedPanels;
    [SerializeField] GameObject closedPanels2;

    [SerializeField] bool DELETE_SAVES;
    // Start is called before the first frame update
    void Start()
    {
        if (DELETE_SAVES) PlayerPrefs.DeleteAll();
        foreach (Transform t in panels)
        {
            Animator anim = t.GetComponent<Animator>();
            anims.Add(anim);
        }
        foreach (Transform t in panels2)
        {
            Animator anim = t.GetComponent<Animator>();
            anims.Add(anim);
        }
        Load();
    }

    void Load()
    {
        int completes = 0;
        for (int i = 0; i < 6; i++)
        {
            bool complete = PlayerPrefs.GetInt("solarPuzzle" + i, 0) == 1;
            puzzlesComplete.Add(complete);
            if (complete)
            {
                completes++;
                puzzles[i].SetVictory();
            }

        }
        if (completes == 6)
        {
            SetSolarPanelOpen();
        }
    }

    public void SetPuzzleComplete(PuzzleManager puzzle)
    {

        player.Show();
        int index = puzzles.IndexOf(puzzle);
        puzzlesComplete[index] = true;
        PlayerPrefs.SetInt("solarPuzzle" + index, 1);

        foreach (bool b in puzzlesComplete)
        {
            if (!b) return;
        }

        Access();
    }
    public void Access()
    {
        if (opened) return;
        this.GetComponent<Collider2D>().enabled = false;
        CameraManager.Instance.ChangeCamera(solarPanelCam);
        StartCoroutine(OpeningPanels(anims));
    }

    void SetSolarPanelOpen()
    {
        mask.SetActive(false);
        mask2.SetActive(false);
        closedPanels.SetActive(false);
        closedPanels2.SetActive(false);

        this.GetComponent<Collider2D>().enabled = false;

    }
    // Update is called once per frame
    void Update()
    {

    }

    public Transform GetPoint(int index)
    {
        return points[index];
    }
    public Transform PointOnLeft(Transform actualPoint)
    {
        int pointIndex = points.IndexOf(actualPoint);
        if (pointIndex % 2 == 0)
        {
            return actualPoint;
        }


        pointIndex--;
        return points[pointIndex];
    }
    public void OpenPuzzle(Transform point)
    {
        point.GetComponentInChildren<PuzzleManager>().TurnPuzzleOn();
    }

    public bool PuzzleComplete(Transform point)
    {
        int index = points.IndexOf(point);
        return puzzlesComplete[index];
    }

    public Transform PointOnRight(Transform actualPoint)
    {
        Debug.Log("alou");
        int pointIndex = points.IndexOf(actualPoint);
        if (pointIndex % 2 != 0)
        {
            return actualPoint;
        }
        pointIndex++;
        return points[pointIndex];
    }

    public Transform PointOnDown(Transform actualPoint)
    {
        int pointIndex = points.IndexOf(actualPoint);
        if (pointIndex == points.Count - 1 || pointIndex == points.Count - 2)
        {
            return actualPoint;
        }
        pointIndex += 2;
        return points[pointIndex];
    }
    public Transform PointOnUp(Transform actualPoint)
    {
        int pointIndex = points.IndexOf(actualPoint);
        if (pointIndex == 0 || pointIndex == 1)
        {
            return actualPoint;
        }
        pointIndex -= 2;
        return points[pointIndex];
    }


    public void OpenPuzzle(int index)
    {
        Debug.Log("Abrindo puzzle");
    }
    IEnumerator OpeningPanels(List<Animator> anims)
    {
        player.SetWaitingState(true);
        foreach (Animator a in anims)
        {
            a.SetTrigger("Open");
            yield return new WaitForSeconds(Random.Range(.3f, .5f));
        }

        player.SetWaitingState(false);
        CameraManager.Instance.ChangeCamera(false, false);
    }
}
