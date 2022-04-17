using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePuzzleManager : MonoBehaviour
{
    [SerializeField] Wave wave;
    bool isComplete;
    WavePanel panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPuzzleComplete()
    {
        isComplete = true;
    }

    public void CompletePuzzle()
    {
        isComplete = true;
        panel.SetPuzzleComplete(this);
    }
    public bool IsComplete()
    {
        return isComplete;
    }

    public void SetWavePanel(WavePanel wavePanel)
    {
        panel = wavePanel;
    }

}
