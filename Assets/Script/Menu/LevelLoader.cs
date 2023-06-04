using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject g1;
    public GameObject g2;
    public GameObject g3;
    public GameObject g4;
    public GameObject g5;
    public string LevelName;
    public Animator transition;
    public float transporttime;
    // Update is called once per frame
    private void Start()
    {
        Invoke("st", transporttime*0.5f);
    }
    void st()
    {
        g1.SetActive(true);
        g2.SetActive(true);
        g3.SetActive(true);
        g4.SetActive(true);
        g5.SetActive(true);
    }
    void ov()
    {
        g1.SetActive(false);
        g2.SetActive(false);
        g3.SetActive(false);
        g4.SetActive(false);
        g5.SetActive(false);
    }
    public void Loadlevel()
    {
        StartCoroutine(Loading(LevelName));
    }
    IEnumerator Loading(string LevelName)
    {
        transition.SetTrigger("Start");
        ov();   
        yield return new WaitForSeconds(transporttime*0.9f);
        SceneManager.LoadScene(LevelName);
    }
}
