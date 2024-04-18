using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    // References to components
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField]private AudioClip enemydeathscream;
    [SerializeField] GameObject energyCrystalPrefab;
    [SerializeField] GameObject goldCoinPrefab;
    [SerializeField] GameObject ultimateCoinPrefab;

    // Enemy properties
    [SerializeField] private float speed = 1f;
    [SerializeField] public bool isTrackingPlayer = true;
    [SerializeField] private float enemyHP = 10f;
    [SerializeField] public float enemyKillCount = 0f;
    [SerializeField] float energyCrystalVanishDelay = 15f;
    [SerializeField] float goldCoinVanishDelay = 15f;
    [SerializeField] float ultimateCoinVanishDelay = 15f;
    [SerializeField] float outOfBoundsDistance = 50f;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        GetComponents();
        PlayIdleAnimation();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
            FlipSprite();
            CheckForPlayer();
            CheckOutOfBounds();
        }
    }

    // Get references to components
    private void GetComponents()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Play the Idle animation
    private void PlayIdleAnimation()
    {
        animator.Play("Slime_Idle");
    }

    private void MoveTowardsPlayer()
    {
        Vector3 destination = player.transform.position;
        Vector3 source = transform.position;
        Vector3 direction = destination - source;
        direction.Normalize();

        if (!isTrackingPlayer)
        {
            // If not tracking player, move in the horizontal direction the enemy sprite is facing
            //float sign = Mathf.Sign(transform.localScale.x);
            if (direction.x > 0)
            { direction = new Vector3(-1f, 0, 0); }
            else
            { direction = new Vector3(1f, 0, 0); }
        }


        transform.position += direction * Time.deltaTime * speed;
    }






    // Flip the sprite based on the direction
    private void FlipSprite()
    {
        if (isTrackingPlayer)
        {
            if (transform.position.x < player.transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else if (transform.position.x > player.transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    // Check if the enemy has found the player and change the animator's state accordingly
    private void CheckForPlayer()
    {
        if (isTrackingPlayer)
        {
            animator.Play("Slime_Run");
        }
        else
        {
            animator.Play("Slime_Walk");
        }
    }

    [System.Obsolete]
    public void TakeDamage(float damage)
    {
        enemyHP -= damage;

        if (enemyHP <= 0)
        {
            Die();
        }
    }

    [System.Obsolete]
    private void Die()
    {
        // Destroy the enemy game object
        Destroy(gameObject);
        AudioManager.InstanceAM.PlaySoundEffect(enemydeathscream);
        
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null && !player.isInvincible)
        {
            if (player.OnDamage())
            {
                Die();
                
            }
        }
    }

    private void CheckOutOfBounds()
    {
        float playerXPosition = player.transform.position.x;
        float distanceFromPlayer = Mathf.Abs(transform.position.x - playerXPosition);
        if (distanceFromPlayer > outOfBoundsDistance)
        {
            Destroy(gameObject);
        }
    }



}
