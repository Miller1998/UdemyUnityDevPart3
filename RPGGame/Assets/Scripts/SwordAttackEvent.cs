using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackEvent : MonoBehaviour
{

    public PlayerController pc;
    public EnemyController ec;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy" && this.tag == "PlayerWeapon")
        {

            float dmg = pc.atkDamage;
            Debug.Log("<color=white>Player damage to enemy = </color>" + dmg);
            other.GetComponent<EnemyController>().currentHealth -= dmg;
            other.GetComponent<EnemyController>().EnemyHit();

        }

        if (other.tag == "Player" && this.tag == "EnemyWeapon")
        {
            
            float dmg = ec.enemyDmg;
            Debug.Log("<color=cyan>Enemy damage to player = </color>" + dmg);
            other.GetComponent<PlayerController>().currentHealth -= dmg;
            other.GetComponent<PlayerController>().PlayerHit();

        }

    }

}
