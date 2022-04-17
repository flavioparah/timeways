using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntennaConnection : Connection
{
    [SerializeField] WavePanel wavePanel;
    [SerializeField] CanvasGroup waveImage;
    [SerializeField] Slider periodSlide;
    [SerializeField] Slider amplitudeSlide;

    Coroutine coroutine;
    public override void Hide()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(Hiding());
    }

    public override void PerformAction()
    {
        //
    }

    public override void Show(Panel panel = null)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(Showing());

    }

    public void Slider()
    {
        //wavePanel.ChangeValue(new Vector2(ampl))
    }

    public void EndPuzzle()
    {
        Debug.Log("endingPuzzle;");
        padScreen.CloseScreen();
    }

    IEnumerator Showing()
    {
        float factor = 0;


        while (factor < 1)
        {
            factor += 2 * Time.deltaTime;


            waveImage.alpha = factor;

            if (factor >= 1)
            {
                waveImage.alpha = 1;
            }
            yield return null;
        }
    }

    IEnumerator Hiding()
    {
        float factor = 0;


        while (factor < 1)
        {
            factor += 2 * Time.deltaTime;


            waveImage.alpha = 1 - factor;

            if (factor >= 1)
            {
                waveImage.alpha = 0;
            }
            yield return null;
        }
    }
}
