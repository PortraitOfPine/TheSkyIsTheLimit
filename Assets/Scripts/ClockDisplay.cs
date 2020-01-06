using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class ClockDisplay : MonoBehaviour
{
    public Text countdown; //UI Text Object

    void Start()
    {
        Time.timeScale = 1; //Just making sure that the timeScale is right
    }

    void Update()
    {
        countdown.text = ("" + Time.time); //Showing the Score on the Canvas
    }
}
