using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public CanvasGroup clockUI;

    public bool ShowUI { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        ShowUI = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (ShowUI == true)
            {
                Hide();
                ShowUI = false;
            }
            else
            {
                Show();
                ShowUI = true;
            }
        }
    }

    void Hide() {
        clockUI.alpha = 0f; //this makes everything transparent
        clockUI.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    void Show() {
        clockUI.alpha = 1f;
        clockUI.blocksRaycasts = true;
    }
}
