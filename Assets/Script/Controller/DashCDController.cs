using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCDController : MonoBehaviour
{
    public Image cdBar;
    public Movement_Player player;
    float percent;
    float dashTime;
    float dashTimeNow;
    float dashCD;

    private void Start()
    {
        dashTime = player.dashTime;
        dashTimeNow = dashTime;
        dashCD = player.dashCD;
    }
    private void Update()
    {
        updateCD();
        cdBar.fillAmount = percent;
    }
    void updateCD()
    {
        if(player.isDash)
        {
            dashTimeNow -= Time.deltaTime;
            StartCoroutine(redash());
        }
            
        percent = dashTimeNow / dashTime;
        if(dashTimeNow <dashTime && !player.isDash)
        {
            dashTimeNow += Time.deltaTime;
        }
    }
    IEnumerator redash()
    {
        yield return new WaitForSeconds(dashCD);
        dashTimeNow = dashTime;
    }
}
