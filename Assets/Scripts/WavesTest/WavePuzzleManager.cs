using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePuzzleManager : MonoBehaviour
{
    [SerializeField] Wave wave;
    bool isComplete;
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
    public bool IsComplete()
    {
        return isComplete;
    }
}
