using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleButton : MonoBehaviour
{
    public float timeScaleValue;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    public void ChangeTimeScale()
    {
        Time.timeScale = timeScaleValue;
    }
}
