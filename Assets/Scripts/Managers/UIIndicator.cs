using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIIndicator : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI meters;
    float borderSizeX;
    float borderSizeY;

    private void Start()
    {
        borderSizeX = this.GetComponent<RectTransform>().rect.width / 2;
        borderSizeY = this.GetComponent<RectTransform>().rect.height / 2;
    }
    // Update is called once per frame
    void Update()
    {
        
        Vector2 camPos = Camera.main.transform.position;
        Vector2 targetPos = target.transform.position;
        Vector2 screenPos = Camera.main.WorldToScreenPoint(targetPos);

        if (screenPos.x < 0) screenPos.x = borderSizeX;
        if (screenPos.x > Screen.width) screenPos.x = Screen.width - borderSizeX;
        if (screenPos.y < 0) screenPos.y = borderSizeY;
        if (screenPos.y > Screen.height) screenPos.y = Screen.height - borderSizeY;
        this.transform.position = screenPos;

        meters.text = Vector3.Distance(player.position, target.position).ToString("F2") + "m";
    }
}
