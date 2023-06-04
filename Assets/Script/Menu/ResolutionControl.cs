using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionControl : MonoBehaviour
{
    public ScreenSet screenset;
    public TMP_Dropdown dropdown;

    private void Start()
    {
        dropdown.onValueChanged.AddListener((int value) =>changeResolution(value));
    }
    public void changeResolution(int index)
    {
        bool isFullScreen = screenset.isFullScreen;
        switch (index)
        {
            case 0:
                if (isFullScreen)
                    Screen.SetResolution(1920, 1080, true);
                else
                    Screen.SetResolution(1920, 1080, false);
                break;
            case 1:
                if (isFullScreen)
                    Screen.SetResolution(1080, 720, true);
                else
                    Screen.SetResolution(1080, 720, false);
                break;
            case 2:
                if (isFullScreen)
                    Screen.SetResolution(800, 600, true);
                else
                    Screen.SetResolution(800, 600, false);
                break;
        }
    }
}
