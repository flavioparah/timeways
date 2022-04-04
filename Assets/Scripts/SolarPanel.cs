using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanel : MonoBehaviour
{
    [SerializeField] Transform panels;
    List<Animator> anims = new List<Animator>();
    bool opened;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in panels)
        {
            Animator anim = t.GetComponent<Animator>();
            anims.Add(anim);
        }
    }


    public void Access()
    {
        if (opened) return;
        StartCoroutine(OpeningPanels(anims));
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OpeningPanels(List<Animator> anims)
    {
        foreach(Animator a in anims)
        {
            a.SetTrigger("Open");
            yield return new WaitForSeconds(Random.Range(.3f, .5f));
        }
    }
}
