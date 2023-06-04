using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class options : MonoBehaviour
{
    public GameObject optionBar;
    bool isShow;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        if(isShow)
        {
            optionBar.SetActive(false);
        }
        else
        {
            optionBar.SetActive(true);
        }
        isShow = !isShow;
    }
}
