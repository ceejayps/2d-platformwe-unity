using UnityEngine;

public class health : MonoBehaviour
{
    public int maxHealth = 100; // the maximum health of the player
    public int currentHealth; // the current health of the player
    public bool invincible = false; // whether the player is invincible or not
    public float invincibilityDuration = 1f; // how long the invincibility lasts after being hit
    private float invincibilityTimer; // keeps track of how long the player has been invincible
    private bool isDead = false; // whether the player is dead or not

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // If the player is invincible, decrement the timer
        if (invincible && invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;

            // If the invincibility timer is over, stop being invincible
            if (invincibilityTimer <= 0)
            {
                invincible = false;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        // If the player is invincible or already dead, do nothing
        if (invincible || isDead)
        {
            return;
        }

        // Subtract the damage from the player's health
        currentHealth -= amount;

        // If the player's health is zero or less, they're dead
        if (currentHealth <= 0)
        {
            isDead = true;
            GetComponent<playerMovement>().enabled = false;
            // Do something to handle the player's death, such as playing an animation or restarting the level
        }
        else
        {
            // Start the invincibility timer
            invincible = true;
            invincibilityTimer = invincibilityDuration;
            // Do something to show that the player has been hit, such as playing a sound or displaying a visual effect
        }
    }

    public void Heal(int amount)
    {
        // Add the healing to the player's health, but don't let it exceed the maximum health
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}
