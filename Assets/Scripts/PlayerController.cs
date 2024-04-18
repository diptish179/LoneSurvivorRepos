using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Player SFX  
    [SerializeField] private AudioClip JumpSound;

    //Player Attributes
    [SerializeField] public float moveSpeed = 4f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float jumpForce = 5f;
    public float playerSize = 3f;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isJumping;
    private string currentAnimation;
    private bool playIdleAfterJump; 
    public float projectileSpeed = 10f;
    private ObjectPooler objectPooler;

    //heal attributes
    public double currentHP;
    public double maxHP = 6;
    private float healAmount;
    public bool isInvincible;
    [SerializeField] SpriteRenderer spriteRenderer;
    Material material;

    //energy attributes
    public double currentPower; // New variable to track power
    public double maxPower = 100;
    public bool isOutOfPower;

    // Animation States
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_WALK = "Player_Walk";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_JUMP = "Player_Jump";

    

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        currentPower = maxPower;
        rb = GetComponent < Rigidbody2D>();
        animator = GetComponent < Animator>();
        objectPooler = FindObjectOfType<ObjectPooler>();
        playIdleAfterJump = false;
        material = spriteRenderer.material;

    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        // Check if left shift is held down to enable running
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

       transform.position += new Vector3(inputX, 0, 0) * currentSpeed * Time.deltaTime;

        if (inputX != 0)
        {
            // Flip the character sprite based on the direction.
            transform.localScale = new Vector3(Mathf.Sign(inputX) * playerSize, playerSize, playerSize);

            if (!isJumping)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    ChangeAnimationState(PLAYER_RUN);
                }
                else
                {
                    ChangeAnimationState(PLAYER_WALK);
                }
            }
        }

        // Check for jump input first and exit early if not grounded or already jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
        {

            
            // Apply the jump force if the player is grounded
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true; // Mark as jumping
            ChangeAnimationState(PLAYER_JUMP);
            AudioManager.InstanceAM.PlaySound(JumpSound); 
            // Reset the flag to play idle after one jump
            playIdleAfterJump = true;
        }

        if (inputX == 0 && isGrounded)
        {
            ChangeAnimationState(PLAYER_IDLE);
        }

        // Check for mouse click to launch the circular sprite
        if (Input.GetMouseButtonDown(0))
        {
            LaunchProjectile();
        }
    }

    private void FixedUpdate()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
    }

    // Check if the player is no longer pressing the jump button to allow another jump
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            if (playIdleAfterJump)
            {
                ChangeAnimationState(PLAYER_IDLE);
                playIdleAfterJump = false;
            }
        }
    }

    public bool OnDamage()
    {
        if (!isInvincible)
        {
            isInvincible = true;
            StartCoroutine(InvincibilityCoroutine());
            if (currentHP-- <= 0)
            {
                //TitleManager.saveData.deathCount++;
                Destroy(gameObject);
                SceneManager.LoadScene("MainMenu");

                AudioManager.InstanceAM.StopSound();

                // Check if the player gameobject is still active in the scene
                if (GameObject.Find("Player") == null)
                {
                    SceneManager.LoadScene("MainMenu");
                    
                    AudioManager.InstanceAM.StopSound();


                }
            }
            return true;
        }
        return false;
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.white;
        isInvincible = false;
    }

    void LaunchProjectile()
    {
        GameObject projectile = objectPooler.GetPooledProjectile();

        if (projectile != null)
        {
            // Set the position with an offset from the player
            Vector3 offset = new Vector3(0f, 0f, 0f); // Adjust the offset as needed
            projectile.transform.position = transform.position + offset;

            // Calculate the direction towards the mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - transform.position).normalized;

            // Activate the projectile and apply force
            projectile.SetActive(true);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        }
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    internal void PlayerHeal()
    {
        StartCoroutine(HealCouroutine());
        if (currentHP == maxHP)
        {
            // The player is already at full health, no further action needed
        }
        else if (currentHP <= 0)
        {
            currentHP = 0.75; // If player is on 0 hp, update it to 75%
        }
        else
        {
            // Calculate the amount to heal the player by 25%
            healAmount = (float)(0.25f * maxHP);

            // Ensure the heal amount won't exceed the amount needed to reach full health
            if (currentHP + healAmount > maxHP)
            {
                healAmount = (float)(maxHP - currentHP);
            }
            // Apply the heal
            currentHP += healAmount;
        }
    }


    IEnumerator HealCouroutine()
    {
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.white;

    }
}

