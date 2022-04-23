using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutscene : MonoBehaviour
{

    public void EndLevel()
    {
        GameManager.Instance.EndCutscene();
    }
}
