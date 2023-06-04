using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
public enum EnemyStates { GUARD, PATROL, CHASE, DEAD};

public class Movement_Enemy01 : MonoBehaviour
{
    [Header("组件")]
    Rigidbody2D rb;
    Animator anim;

    [Header("AI")]
    float faceOri;
    public float face;
    public float patrolRadius;
    public float lookRadius;
    public float attackRadius;
    public bool isGuard;
    public bool ishurt;
    public bool isDead;
    public float lookAtTime;
    public float stopDistance;
    [SerializeField]
    float remainLookAtTime;
    Vector3 Guardpoint;
    Vector3 Waypoint;
    //[HideInInspector]
    public EnemyStates enemyState;
    GameObject attackTarget;
    

    [Header("属性")]
    public float speed = 8f;
    [SerializeField]
    float speednow;
    public int damage;
    public bool isHurt;


    [Header("动画")]
    public float CDAttack;
    [SerializeField]
    float CDAttackNow;
    public float attackdelay;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Guardpoint = transform.position;
        faceOri = transform.localScale.x;
        
        CDAttackNow = CDAttack;
    }
    private void Start()
    {
        face = faceOri;
        if(isGuard)
        {
            enemyState = EnemyStates.GUARD;
        }
        else
        {
            enemyState = EnemyStates.PATROL;
            GetNewWayPoint();
        }
    }
    private void Update()
    {
        SwitchStates();
        hurtCheck();
        CDAttackNow -= Time.deltaTime;
        anim.SetFloat("velocity", Mathf.Abs(rb.velocity.x));
        if (enemyState == EnemyStates.DEAD)
            transform.localScale = faceOri * Vector3.one;
    }
    public void GetNewWayPoint()
    {
        remainLookAtTime = lookAtTime;
        float randomX = Random.Range(-patrolRadius,patrolRadius);
        Vector3 randompoint = new Vector3(Guardpoint.x + randomX, Guardpoint.y,Guardpoint.z);
        Waypoint = randompoint;
        Debug.Log(Waypoint);
    }
    void Move(Vector3 destination)
    {
        speednow = speed * 0.5f;
        float offset = destination.x - transform.position.x;
        transform.localScale = new Vector3(face, transform.localScale.y, transform.localScale.z);
        if(offset < 0)
        {
            face = -1*faceOri;
            
        }
        else
        {
            face = 1 * faceOri;
        }
        if(Mathf.Abs(destination.x - transform.position.x) > stopDistance)
            rb.velocity = new Vector2(face * speednow, rb.velocity.y);  
        else
            rb.velocity = Vector2.zero;
    }
    bool FoundPlayer()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, lookRadius);
        foreach (var collider in colliders)
        {
            if (collider.tag == "Player")
            {
                attackTarget = collider.gameObject;
                return true;
            }
            attackTarget = null;
        }
        return false;
    }
    void SwitchStates()
    {
        if(FoundPlayer())
        {
            enemyState = EnemyStates.CHASE;
        }
        switch(enemyState)
        {
            case EnemyStates.GUARD:
                rb.velocity = Vector2.zero;
                if (isDead)
                    enemyState = EnemyStates.DEAD;
                break;
            case EnemyStates.PATROL:
                if (isDead)
                    enemyState = EnemyStates.DEAD;
                speednow = speed * 0.5f;
                if (Mathf.Abs(transform.position.x - Waypoint.x) <= stopDistance)
                {
                    if(remainLookAtTime > 0)
                    {
                        remainLookAtTime -= Time.deltaTime;
                        rb.velocity = Vector2.zero;
                    }
                       
                    else
                        GetNewWayPoint();
                }
                else
                {
                    Move(Waypoint);
                }
                break;
            case EnemyStates.CHASE:
                if (isDead)
                    enemyState = EnemyStates.DEAD;
                speednow = speed;
                if(!FoundPlayer())
                {
                    if (remainLookAtTime > 0)
                    {
                        rb.velocity = Vector2.zero;
                        remainLookAtTime -= Time.deltaTime;
                    }
                    else if (isGuard)
                    {
                        //Move(Guardpoint);
                        enemyState = EnemyStates.GUARD;
                    }
                    else
                        enemyState = EnemyStates.PATROL;
                }
                else
                {
                    Move(attackTarget.transform.position);
                
                    if(Mathf.Abs(transform.position.x - attackTarget.transform.position.x) < attackRadius)
                    {
                        speednow = 0;
                        if (CDAttackNow <= 0)
                        {
                            StartCoroutine(Attack(attackTarget));
                            CDAttackNow = CDAttack;
                        }
                    }
                }
                break;
            case EnemyStates.DEAD:
                break;
        }
    }
    void hurtCheck()
    {
        if (ishurt)
        {
            anim.SetTrigger("hurt");
            speednow = speed * 0.5f;
            //设置受伤动画
            CDAttackNow = CDAttack*0.5f;
            ishurt = false;
        }
    }
    IEnumerator Attack(GameObject player)
    {
        int r = Random.Range(0,10);
        if (r < 6)
            anim.SetTrigger("attack1");
        else
            anim.SetTrigger("attack2");
        yield return new WaitForSeconds(attackdelay);
        if (transform.localScale.x > 0 ? player.transform.position.x - transform.position.x < attackRadius : transform.position.x - player.transform.position.x < attackRadius)
        {
            player.GetComponent<Movement_Player>().ishurt = true;
            player.GetComponent<PlayerHealthController>().Hurt(damage);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
