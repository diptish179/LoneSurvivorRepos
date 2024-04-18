using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet collides with the player
        if (other.CompareTag("Player"))
        {
            // Get the player component
            PlayerController player = other.GetComponent<PlayerController>();

            // Call the OnDamage function in the player script
            player.OnDamage();

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
