using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class NPC : MonoBehaviour
{
    [SerializeField] PathCreator path;
    [SerializeField] float distance;
    [SerializeField] float speed;
    [SerializeField] CharacterUI characterUI;


    private void Start()
    {

        transform.rotation = Quaternion.LookRotation(Vector3.forward, path.path.GetNormalAtDistance(distance));
        transform.position = path.path.GetPointAtDistance(distance);
    }


    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, path.path.GetNormalAtDistance(distance));
        transform.position = path.path.GetPointAtDistance(distance);

        CheckCameraPosition();
    }

    public void Interact()
    {
        characterUI.Talk();
    }

    public void CancelInteraction()
    {
        characterUI.StopTalking();
    }

    void CheckCameraPosition()
    {
        float width = Screen.width;
        float height = Screen.height;

        Vector2 screenMax = Camera.main.ScreenToWorldPoint(new Vector2(width, height));
        Vector2 screenMin = Camera.main.ScreenToWorldPoint(Vector2.zero);

       float widthMax = screenMax.x;
       float heightMax = screenMax.y;

        float widthMin = screenMin.x;
        float heightMin = screenMin.y;

        float x = this.transform.position.x;
        float y = this.transform.position.y;

      

        if (x < widthMin || x > widthMax || y < heightMin || y > heightMax) CancelInteraction(); 
    }
}
