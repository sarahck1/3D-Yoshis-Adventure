using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class playerHealth : MonoBehaviour
{
    public int health = 3;
    public Text healthText;

    void Start()
    {
        UpdateHealthDisplay();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        UpdateHealthDisplay();

        if (health <= 0)
        {
            Object.FindFirstObjectByType<GameManager>().GameOver();
            
        }
    }

    void UpdateHealthDisplay()
    {
        healthText.text = "Health: " + health;
    }
}
