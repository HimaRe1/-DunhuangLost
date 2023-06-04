using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    Animator anim; 
    float delay;
    public HealthController hc;
    float health;
    public GameObject Endbar;
    private void Start()
    {
        delay = hc.deadtime;
    }
    private void Update()
    {
        health = hc.health;
        EndCheck();
    }
    void EndCheck()
    {
        if(health<=0)
        {
            StartCoroutine(EndGame());
        }
        
    }
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(delay);
        Endbar.SetActive(true);
        yield return new WaitForSeconds(2);
        anim = GameObject.FindGameObjectWithTag("gameover").GetComponent<Animator>();
        anim.SetTrigger("Back");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
