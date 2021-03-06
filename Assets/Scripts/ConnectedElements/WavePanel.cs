using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePanel : Panel
{
    [SerializeField] List<WavePuzzleManager> puzzleManager = new List<WavePuzzleManager>();
    [SerializeField] Wave wave;
    [SerializeField] Wave MainWave;

    [SerializeField] List<GameObject> puzzles = new List<GameObject>();
    List<bool> puzzlesComplete = new List<bool>();

    [SerializeField] bool DELETE_SAVES;
    WavePuzzleManager currentPuzzleManager;

    [SerializeField] Antenna antenna;
    [SerializeField] CinemachineVirtualCamera antennaAnimationCam;
    bool opened;
    [SerializeField] AntennaConnection antennaConnection;
    // Start is called before the first frame update
    protected override void Start()
    {
        // base.Start();
        if (DELETE_SAVES) PlayerPrefs.DeleteAll();
        connection = Enums.Connection.antenna;
        Load();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SetWavePuzzle(WavePuzzleManager puzzle)
    {
        puzzles.ForEach(p => p.SetActive(false));
        this.currentPuzzleManager = puzzle;
        int index = puzzleManager.IndexOf(puzzle);
        puzzles[index].SetActive(true);
        wave = puzzles[index].transform.Find("Wave").GetComponent<Wave>();
        wave.SetManager(puzzle);
        puzzle.SetWavePanel(this);
    }

    public void ChangeValue(Vector2 value)
    {
        if (value.x > 0)
        {
            wave.SetPeriod(true);
        }
        else if(value.x < 0)
        {
            wave.SetPeriod(false);
        }

        if (value.y > 0)
        {
            wave.SetAmplitude(true);
        }
        else if (value.y < 0)
        {
            wave.SetAmplitude(false);
        }
    }


    void Load()
    {
        int completes = 0;
        {
            bool complete = PlayerPrefs.GetInt("AntennaPuzzle0", 0) == 1;
            puzzlesComplete.Add(complete);
            if (complete)
            {
                completes++;
                puzzleManager[0].SetPuzzleComplete();
                antenna.SetPuzzleComplete(puzzleManager[0]);
            }

            if (complete) SetPuzzleBaseComplete();
        }
        int notBaseCompletes = 0;
        for (int i = 1; i < 6; i++)
        {
            bool complete = PlayerPrefs.GetInt("AntennaPuzzle" + i, 0) == 1;
            puzzlesComplete.Add(complete);
            if (complete)
            {
                completes++;
                puzzleManager[i].SetPuzzleComplete();
                antenna.SetPuzzleComplete(puzzleManager[i]);
                notBaseCompletes++;
            }

            
        }

        if(notBaseCompletes == 5)
        {
            SetAntennaPuzzlesComplete();
        }
        if (completes == 6)
        {
            SetAntennaOpen();
        }
    }

    public void SetPuzzleComplete(WavePuzzleManager puzzle)
    {

       
        int index = puzzleManager.IndexOf(puzzle);
        puzzlesComplete[index] = true;
        PlayerPrefs.SetInt("AntennaPuzzle" + index, 1);
        antennaConnection.EndPuzzle();
        antenna.SetPuzzleComplete(puzzle);
        foreach (bool b in puzzlesComplete)
        {
            if (!b) return;
        }

        Access();
    }
    public void Access()
    {
        if (opened) return;
        
        CameraManager.Instance.ChangeCamera(antennaAnimationCam);
        antenna.Open();
        GameManager.Instance.SaveAntennaPuzzle();
    }

    void SetAntennaOpen()
    {
        opened = true;
        antenna.SetOpen();

    }

    void SetPuzzleBaseComplete()
    {
        antenna.BaseComplete();
    }

    void SetAntennaPuzzlesComplete()
    {
        antenna.NotBaseComplete();
    }
}
