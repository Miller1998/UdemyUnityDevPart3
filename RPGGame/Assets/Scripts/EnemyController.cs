using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    private NavMeshAgent agent;
    internal Animator animator;
    public GameObject target;
    
    [SerializeField]
    private float radiusDetection;
    [SerializeField]
    internal float enemyHealthMax = 100f;
    internal float currentHealth;
    [SerializeField]
    internal float enemyDmg;
    private bool follow = false;

    //Awake is called when the script is being loaded
    void Awake()
    {

        

    }

    // Start is called before the first frame update
    void Start()
    {

        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();

        //hp mechanic
        currentHealth = enemyHealthMax;

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, this.transform.position);
        PlayerController pc = target.GetComponent<PlayerController>();

        if (isDead() == false)
        {
            
            PlayerDetector(distance);
            EnemyAttacking(distance, pc.isDead());

        }
        else
        {

            EnemyDead();

        }

        Debug.Log("<color=red>Enemy HP = </color>" + currentHealth);

    }

    private bool isDead()
    {
        if (currentHealth <= 0)
        {

            return true;

        }

        return false;

    }

    public void EnemyDead()
    {

        animator.SetBool("Die", true);
        //UI Game Over etc

    }

    private void PlayerDetector(float distance)
    {
        
        if (distance <= radiusDetection && distance > agent.stoppingDistance)
        {
            //enemy will start following player
            follow = true;
        }
        else
        {
            follow = false;
        }

        if (follow == true)
        {
            animator.SetBool("Run", true);
            CoreAIFunctionality(distance);
        }
        else
        {
            animator.SetBool("Run", false);
        }


    }

    private void EnemyAttacking(float distance, bool isPlayerDead)
    {

        if (distance <= agent.stoppingDistance)
        {
            animator.SetBool("AtkPlayer", true);
        }
        else if(isPlayerDead == true)
        {
            animator.SetBool("AtkPlayer", false);
        }
        else
        {
            animator.SetBool("AtkPlayer", false);
        }
    
    }

    private void CoreAIFunctionality(float dis)
    {

        agent.SetDestination(target.transform.position);
        if (dis <= agent.stoppingDistance)
        {
            follow = false;
        }

    }

    public void HitParaSetter()
    {
        animator.SetBool("Hit", false);
    }

    public void EnemyHit()
    {
        animator.SetBool("Hit", true);
    }

}
