using System.Collections;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float shootingRange = 5f;      // Range at which enemy starts shooting
    public float projectileSpeed = 5f;    // Speed of the projectile
    public GameObject projectilePrefab;   // Prefab of the projectile
    public float shootInterval = 1f;      // Interval between each shot
    public float bulletOffset = 0.5f;     // Offset distance from enemy to spawn bullets

    private Transform player;              // Reference to the player's transform
    private bool canShoot = false;         // Flag to control shooting
    private SpriteRenderer spriteRenderer; // Reference to the enemy's SpriteRenderer component

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ScanForPlayer());
    }

    IEnumerator ScanForPlayer()
    {
        while (true)
        {
            // Check if player is within shooting range
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= shootingRange)
            {
                if (!canShoot)
                {
                    canShoot = true;
                    StartCoroutine(ShootRoutine());
                }
            }
            else
            {
                canShoot = false;
            }

            yield return new WaitForSeconds(0.5f); // Check every 0.5 seconds
        }
    }

    IEnumerator ShootRoutine()
    {
        while (canShoot)
        {
            // Calculate direction towards player
            Vector2 direction = (player.position - transform.position).normalized;

            // Determine if enemy should face left or right based on direction
            if (direction.x >= 0f)
            {
                spriteRenderer.flipX = false; // Face right
            }
            else
            {
                spriteRenderer.flipX = true; // Face left
            }

            // Calculate offset position to spawn bullets
            Vector2 spawnPosition = (Vector2)transform.position + direction * bulletOffset;

            // Shoot projectile from the calculated position
            ShootProjectile(spawnPosition, direction);

            yield return new WaitForSeconds(shootInterval);
        }
    }

    void ShootProjectile(Vector2 spawnPosition, Vector2 direction)
    {
        // Instantiate projectile at spawnPosition
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        // Get Rigidbody2D component of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Set velocity of the projectile to move in the calculated direction
        rb.velocity = direction * projectileSpeed;
    }

}
