using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class UlisesConnection : Connection
{
    [SerializeField] GameObject loading;
    [SerializeField] TextMeshProUGUI objective;
    [SerializeField] GameObject container;

    Animator anim;
    public override void Start()
    {
        base.Start();
        anim = this.GetComponent<Animator>();
       // objective.fontStyle = FontStyles.Strikethrough;
    }
    public override void Hide()
    {
        anim.SetTrigger("Close");
    }

    public override void Show(Panel panel = null)
    {
        anim.SetTrigger("Open");
    }

    public override void PerformAction()
    {

    }

    public void OpenLoadingScreen()
    {
        loading.SetActive(true);
    }

    public void ObjectiveComplete()
    {
        objective.fontStyle = FontStyles.Strikethrough;
    }
}
