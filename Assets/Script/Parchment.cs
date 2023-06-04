using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parchment : MonoBehaviour
{
    PlayerHealthController hc;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    int num;
    private void Awake()
    {       
        hc = GetComponent<PlayerHealthController>();
    }
    private void Update()
    {
        ParchmentControl();
        ParchmentUse();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "parchment")
        {
            Destroy(other.gameObject);  
            num++;
            if (num > 4)
            {
                num = 4;
                hc.health += 20;
            }
                
        }
    }
    void ParchmentControl()
    {
        switch(num)
        {
            case 0:
                p1.SetActive(false);
                p2.SetActive(false);
                p3.SetActive(false);
                p4.SetActive(false);
                break;
            case 1:
                p1.SetActive(true);
                p2.SetActive(false);
                p3.SetActive(false);
                p4.SetActive(false);
                break;
            case 2:
                p1.SetActive(true);
                p2.SetActive(true);
                p3.SetActive(false);
                p4.SetActive(false);
                break;
            case 3:
                p1.SetActive(true);
                p2.SetActive(true);
                p3.SetActive(true);
                p4.SetActive(false);
                break;
            case 4:
                p1.SetActive(true);
                p2.SetActive(true);
                p3.SetActive(true);
                p4.SetActive(true);
                break;
        }
    }
    void ParchmentUse()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            num--;
            hc.health += 20;
        }
    }

}
