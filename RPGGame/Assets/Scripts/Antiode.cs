using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antiode : MonoBehaviour
{
    public PlayerController pc;
    [SerializeField] private int healAmount;

    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && this.tag == "Antiode")
        {

            pc.currentHealth += healAmount;
            pc.suddenDead = false;
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);

        }

    }

}
