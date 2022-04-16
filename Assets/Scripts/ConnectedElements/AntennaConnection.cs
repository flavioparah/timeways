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
    public override void Hide()
    {
        waveImage.alpha = 0;
    }

    public override void PerformAction()
    {
       //
    }

    public override void Show(Panel panel = null)
    {
        waveImage.alpha = 1;
    }

    public void Slider()
    {
        //wavePanel.ChangeValue(new Vector2(ampl))
    }
   
}
