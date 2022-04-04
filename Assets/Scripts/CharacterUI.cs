using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] Transform headPoint;
    [SerializeField] Transform arrow;
    NPCTexts lines;
    int lineIndex = 0;

    public void Talk()
    {
        lines.StartLine();

    }

    public void StopTalking()
    {
        lines.StopLine();
    }

    // Start is called before the first frame update
    void Start()
    {

        lines = this.GetComponent<NPCTexts>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 bottomPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 0));
        Vector2 position = (bottomPos - (Vector2)headPoint.position).normalized * Vector2.Distance((Vector2)headPoint.position, bottomPos) /2 ;
        this.transform.position =(Vector2)headPoint.position +  position;
        this.transform.rotation = Quaternion.LookRotation(Vector3.forward);

        arrow.rotation = Quaternion.LookRotation(Vector3.forward, headPoint.position - this.transform.position);
        arrow.position = this.transform.position + (headPoint.position - this.transform.position) / 2;
    }
}
