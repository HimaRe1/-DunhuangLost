using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pause : MonoBehaviour
{
    public GameObject menu;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MenuControl();
        }
    }
    public void MenuControl()
    {
        if(menu.activeSelf == false)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
        }
            
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1;
        }
              
    }
}
