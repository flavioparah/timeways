using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class UlisesConnection : Connection
{
    [SerializeField] GameObject loading;
    [SerializeField] TextMeshProUGUI objective1;
    [SerializeField] TextMeshProUGUI objective2;
    [SerializeField] GameObject container;

    Animator anim;

    bool objective1Completed;
    bool objective2Completed;
    [SerializeField] bool isOutside;
    public override void Start()
    {
        base.Start();
        anim = this.GetComponent<Animator>();
        SetObjectives();
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

    public void Objective1Complete()
    {
        objective1.fontStyle = FontStyles.Strikethrough;
        objective1Completed = true;
        CheckObjectives();
    }

    public void Objective2Complete()
    {
        objective2.fontStyle = FontStyles.Strikethrough;
        objective2Completed = true;
        CheckObjectives();
    }

    void CheckObjectives()
    {
        if (!isOutside) return;
        if(objective1Completed && objective2Completed)
        {
            padScreen.SetLevelComplete();
        }
    }
    public void SetObjectives()
    {
        objective1Completed = GameManager.Instance.IsAntenaPuzzleCompleted();
        objective2Completed = GameManager.Instance.IsSolarPuzzleCompleted();

        if (objective1Completed) Objective1Complete();
        if (objective2Completed) Objective2Complete();
    }
}
