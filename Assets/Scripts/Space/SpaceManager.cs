using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    public static SpaceManager Instance;
    [SerializeField] GameObject canvasTutorial;
    [SerializeField] List<Transform> startPoints = new List<Transform>();
    [SerializeField] List<GameObject> parts = new List<GameObject>();
    [SerializeField] GameObject target;
    GameObject part;
    int partIndex;

    float time;

    private void Awake()
    {
        Instance = this;
        partIndex = 0;
        part = parts[partIndex];
        

    }
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time <= 1) return;
        Vector2 targetPos = target.transform.position;
        Vector2 screenPos = Camera.main.WorldToScreenPoint(targetPos);

        if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height) Debug.Log("GAME OVER");
    }

    public void OpenTutorial()
    {
        canvasTutorial.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseTutorial()
    {
        canvasTutorial.SetActive(false);
        Time.timeScale = 1;
    }


    public void NextPart()
    {
        time = 0;
        //  part.SetActive(false);
        partIndex = 3;
        part = parts[partIndex];
        part.SetActive(true);
       // return null;
    }
}
