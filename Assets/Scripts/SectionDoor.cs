using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionDoor : MonoBehaviour
{
    Transform previousSection;
    [SerializeField] Transform nextSection;
    Transform nextPos;
    Transform previousPos;
    // Start is called before the first frame update
    void Start()
    {
        previousSection = this.transform.parent;
        previousPos = this.transform.GetChild(0);
        nextPos = this.transform.GetChild(1);
    }

    public (Transform,Transform) GetNextSection(Transform actualSection)
    {
        if(previousSection == actualSection)
        {
            return (nextSection, nextPos);
        }
        else
        {
            return (previousSection, previousPos);
        }
    }
}
