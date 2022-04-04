using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class Connection : MonoBehaviour
{
    [SerializeField] Image connectionBG;
    [SerializeField] Button buttonToNavigate;
    protected PadScreen padScreen;
    protected
    bool screenClosed;
    public virtual void Start()
    {
        padScreen = this.gameObject.GetComponentInParent<PadScreen>();
        SetButtonToNavigate();
    }

    private void OnEnable()
    {
        Player.padClosed += PerformAction;
    }
    private void OnDisable()
    {
        Player.padClosed -= PerformAction;
    }
    public abstract void Show(Panel panel = null);
    public abstract void Hide();

    public virtual void SetButtonToNavigate()
    {
        padScreen.SetButtonToNavigate(buttonToNavigate);
    }

    public virtual Image GetBG()
    {
        return connectionBG;
    }

    public abstract void PerformAction();
}
