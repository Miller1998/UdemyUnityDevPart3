using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //game object caller
    [SerializeField]
    private NavMeshSurface surface;

    //UI HP Bar
    public Slider hpBar;
    public Text text;
    public Gradient gradient;
    public Image fill;

    //list and Queue

    //Awake is called when the script is being loaded
    void Awake()
    {
                
        surface.BuildNavMesh();

    }

    // Start is called before the first frame update
    void Start()
    { 
            


    }

    // Update is called once per frame
    void Update()
    {

        

    }

    public void SetMaxHealth(float health)
    {

        hpBar.maxValue = health;
        hpBar.value  = health;

        fill.color = gradient.Evaluate(1f);

    }

    public void SetUIHpBar(float health)
    {

        hpBar.value = health;
        if (health <= 0)
        {
            text.text = "HP : 0";
        }
        else
        {
            text.text = "HP : " + health.ToString();
        }
        fill.color = gradient.Evaluate(hpBar.normalizedValue);

    }

}
