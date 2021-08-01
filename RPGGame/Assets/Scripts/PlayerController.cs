using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public GameObject characterPlayer;
    [SerializeField]
    private GameObject antiodePrefab;

    private CharacterController cc;
    internal Animator animator;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private GameObject cam;

    [SerializeField] internal float atkDamage;
    [SerializeField] internal float playerHPMax = 100f;
    internal float currentHealth;

    [SerializeField] internal Text deadTimerText;
    [SerializeField] internal float timeToDead = 10f;

    [SerializeField] private GameManager gm;
    internal bool suddenDead = false;


    private float xRotation = 0f;
    private float yRotation = 0f;

    [SerializeField] private Transform[] spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        cc = characterPlayer.GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = characterPlayer.GetComponent<Animator>();
        
        //hp mechanic
        currentHealth = playerHPMax;
        gm.SetMaxHealth(playerHPMax);

    }

    // Update is called once per frame
    void Update()
    {

        if (isDead() == false)
        {
            
            CharacterMovement();
            CharacterRotation();
            CharacterAttacking();
            deadTimerText.gameObject.SetActive(false);

        }
        else if (timeToDead > 0 && isDead() == true && suddenDead == false)
        {

            deadTimerText.gameObject.SetActive(true);
            Instantiate(antiodePrefab, spawnPoint[Random.Range(0, spawnPoint.Length)].position, Quaternion.identity);
            
            suddenDead = true;

        }
        else if (timeToDead > 0 && isDead() == true && suddenDead == true)
        {

            CharacterMovement();
            CharacterRotation();
            CharacterAttacking();

            timeToDead -= Time.deltaTime;
            deadTimerText.text = "<color=red>Dead Timer = </color>" + timeToDead.ToString("0.0");

        }
        else if(timeToDead <= 0 && isDead() == true && suddenDead == true)
        {

            PlayerDead();
            deadTimerText.gameObject.SetActive(false);

        }

        gm.SetUIHpBar(currentHealth);
        Debug.Log("<color=green>Player HP = </color>" + currentHealth);

    }

    public bool isDead()
    {
        if (currentHealth <= 0)
        {

            return true;

        }

        return false;
        
    }

    public void PlayerDead()
    {
        
        animator.SetBool("Die", true);
        //UI Game Over etc

    }

    private void CharacterMovement()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 movementVector = transform.right * x + transform.forward * y;

        cc.Move(movementVector * Time.deltaTime * movementSpeed);

        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);

        if (x==0 && y==0)
        {
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Run", true);
        }
        

    }

    void CharacterRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        //yRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, -20f, 20f);
        //yRotation = Mathf.Clamp(yRotation, -20f, 20f);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        cc.transform.Rotate(Vector3.up * mouseX);

    }


    void CharacterAttacking()
    {
        
        if (Input.GetMouseButtonDown(0) == true)
        {

            animator.SetBool("Attack", true);
            animator.SetInteger("AtkRnd", Random.Range(0, 2));
            Debug.Log("Comencing Attack !!");

        }
        /// animator.SetBool("Attack", false);

    }

    

    public void AttackParaSetter()
    {
        animator.SetBool("Attack", false);
    }

    public void PlayerHit()
    {

        animator.SetBool("Hit", true);

    }

    public void HitParaSetter()
    {
        animator.SetBool("Hit", false);
    }

}
