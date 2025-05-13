using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public int health = 3;
    public Text healthText;
    private float grapeInvincibilityTimer = 0f;
    public Text grapeInvincibilityText;

    public GameObject player;  // Reference to the Player object
    private Renderer rend;
    private Color originalColor;

    public bool isInvincible = false;
    public bool isGrapeInvincible = false;
    private bool invincibilityOnCooldown = false;
    public float invincibilityCooldownTime = 5f;

    void Start()
    {
        UpdateHealthDisplay();
        
        if (player != null)
        {
            rend = player.GetComponent<Renderer>(); 
           
            originalColor = rend.material.color;  
        }
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible || isGrapeInvincible) return;

        health -= amount;
        UpdateHealthDisplay();

        if (health <= 0)
        {
            Object.FindFirstObjectByType<GameManager>().GameOver();
        }
    }

    public void Heal(int amount)
    {
        health = Mathf.Min(3, health + amount);
        UpdateHealthDisplay();
    }

    public IEnumerator TemporaryInvincibility(float duration)
    {
        Debug.Log("i-frames");
        if (invincibilityOnCooldown) yield break;

        isInvincible = true;
        invincibilityOnCooldown = true;
    
        yield return new WaitForSeconds(duration);

        isInvincible = false;

        yield return new WaitForSeconds(invincibilityCooldownTime - duration);
        invincibilityOnCooldown = false;
    }

    public void GrapeInvincibility(float duration)
{
    Debug.Log("Grape Invincibility Started");

    isGrapeInvincible = true;
    grapeInvincibilityTimer = duration;

    if (rend != null)
        rend.material.color = Color.blue;
}



    void UpdateHealthDisplay()
    {
        healthText.text = "Health: " + health;
    }

    void Update()
{
    if (isGrapeInvincible)
    {
        grapeInvincibilityTimer -= Time.deltaTime;

        // update the countdown text
        if (grapeInvincibilityText != null)
            grapeInvincibilityText.text = "Invincible: " + Mathf.CeilToInt(grapeInvincibilityTimer) + "s";

        if (grapeInvincibilityTimer <= 0)
        {
            isGrapeInvincible = false;
            //Debug.Log("Grape Invincibility Ended");

            if (rend != null)
                rend.material.color = originalColor;

            if (grapeInvincibilityText != null)
                grapeInvincibilityText.text = "";  // Clear the text
        }
    }
}


}
