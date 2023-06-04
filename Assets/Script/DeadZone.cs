using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    Collider2D player;
    public float cd;
    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player = collision;
            StartCoroutine(GameOver());
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
    IEnumerator GameOver()
    {
        player.gameObject.GetComponent<PlayerHealthController>().Hurt(damage);
        yield return new WaitForSeconds(cd);
        player.gameObject.GetComponent<PlayerHealthController>().Hurt(damage);
        yield return null;
    }
}
