using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showMember : MonoBehaviour
{
    public GameObject m;
    public void show()
    {
        if (m.activeSelf == false)
            m.SetActive(true);
        else
            m.SetActive(false);
    }
}
