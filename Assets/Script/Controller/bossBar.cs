using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossBar : MonoBehaviour
{
    public Transform player;
    public GameObject bar;
    public Image healthbar;
    float percent;
    float healthnow;
    float health;
    HealthController hc;
    Movement_Enemy01 me;
    public BGM bb;
    private void Awake()
    {
        me = GetComponent<Movement_Enemy01>();
        hc = GetComponent<HealthController>();
    }
    private void Start()
    {
        health = hc.health;
        bb = GameObject.FindGameObjectWithTag("bgm").GetComponent<BGM>();
    }
    private void Update()
    {
        barControl();
        updateHealth();
    }
    void barControl()
    {
        if(bb != null)
        {
            if (Vector2.Distance(player.position, transform.position) <= me.lookRadius)
            {
                bar.SetActive(true);
                bb.setBossAudio();
            }
            
            else
            {
                bar.SetActive(false);
                //bb.setOriAudio();
            }
        }
        
            
    }
    void updateHealth()
    {
        healthnow = hc.health;
        percent = healthnow / health;
        healthbar.fillAmount = percent;
    }
}
