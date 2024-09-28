using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Update()
    {
        // Ensure health doesn't exceed the number of hearts
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        // Update heart display
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // Toggle heart visibility based on the number of hearts
            hearts[i].enabled = i < numOfHearts;
        }
    }
}
