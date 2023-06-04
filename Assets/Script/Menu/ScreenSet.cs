using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSet : MonoBehaviour
{
    public bool isFullScreen;
    private void Start()
    {
        FullScreen();
    }
    public void FullScreen()
    {
        isFullScreen = true;
        Screen.fullScreen = true;
    }
    public void Window()
    {
        isFullScreen = false;
        Screen.fullScreen = false;
    }
    
}
