using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class ClockDisplay : MonoBehaviour
{
    public Text countdown; //UI Text Object
    public Text timeScaleDisplay; //UI Text Object

    void Start()
    {
        ChangeTimeScale(1); //Just making sure that the timeScale is right
    }

    void Update()
    {
        float hours = Mathf.Floor(Time.time / (60 * 60));
        float minutes = Mathf.Floor((Time.time % (60*60)) / (60));
        float seconds = Mathf.RoundToInt(Time.time % (60));
        countdown.text = ("" + hours+ "h " + minutes + "m " + seconds + "s"); //Showing the Score on the Canvas

    }

    //Update the timescale and the display
    public void ChangeTimeScale(float timeScaleValue)
    {
        Time.timeScale = timeScaleValue;
        timeScaleDisplay.text = timeScaleValue.ToString();
    }

    //Change the timescale following the end of editing
    public void InputFieldTime(InputField input)
    {
       
        ChangeTimeScale(float.Parse(input.text));

    }
}
