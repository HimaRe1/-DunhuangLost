using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backTOmenu : MonoBehaviour
{
    public void back()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
