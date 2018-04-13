using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Attributes/Health System")]
public class HealthSystemAttribute : MonoBehaviour
{
    public int health = 3;

    private UIScript ui;
    private int maxHealth;
    public SpriteRenderer PlayerSprite;

    // Will be set to 0 or 1 depending on how the GameObject is tagged
    // it's -1 if the object is not a player
    private int playerNumber;



    private void Start()
    {
        NormalColor = PlayerSprite.color;
        // Find the UI in the scene and store a reference for later use
        ui = GameObject.FindObjectOfType<UIScript>();

        // Set the player number based on the GameObject tag
        switch (gameObject.tag)
        {
            case "Player":
                playerNumber = 0;
                break;
            case "Player2":
                playerNumber = 1;
                break;
            default:
                playerNumber = -1;
                break;
        }

        // Notify the UI so it will show the right initial amount
        if (ui != null
            && playerNumber != -1)
        {
            ui.SetHealth(health, playerNumber);
        }

        maxHealth = health; //note down the maximum health to avoid going over it when the player gets healed
    }

    public int SaveCount = 0;
    int MaxSaveAmount = 4;

    public void SaveOneInjureSlime()
    {
        SaveCount += 1;
    }

    public bool IsMaxSave()
    {
        if (SaveCount < MaxSaveAmount)
            return false;
        else
            return true;
    }

    // changes the energy from the player
    // also notifies the UI (if present)
    public void ModifyHealth(int amount)
    {
        //Invincible time
        if (isInvincible())
            return;

        //avoid going over the maximum health by forcin
        if (health + amount > maxHealth)
        {
            amount = maxHealth - health;
        }

        health += amount;

        // Notify the UI so it will change the number in the corner
        if (ui != null
            && playerNumber != -1)
        {
            ui.ChangeHealth(amount, playerNumber);
        }

        //DEAD
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        //ChangeSpriteColor
        if (amount < 0)
            DamageSpriteShining();
    }

    public Color HitColor = Color.red;
    Color NormalColor = Color.white;
    float HitTimer = 0;
    float ShiningInteral = .2f;
    bool isHitColor = false;
    int ColorChangeCount = 9;
    int ColorChangeAmount = 9;

    private void Update()
    {
        if (isInvincible())
        {
            HitTimer += Time.deltaTime;
            if (HitTimer > ShiningInteral)
            {
                HitTimer = 0;
                isHitColor = !isHitColor;
                ColorChangeCount++;
            }
        }
        if (isHitColor)
        {
            PlayerSprite.color = Color.Lerp(PlayerSprite.color, HitColor, Time.deltaTime * 2);
        }
        else
        {
            PlayerSprite.color = Color.Lerp(PlayerSprite.color, NormalColor, Time.deltaTime * 2);
        }
    }

    public void DamageSpriteShining()
    {
        isHitColor = true;
        ColorChangeCount = 0;
    }

    bool isInvincible()
    {
        if (ColorChangeCount < ColorChangeAmount)
            return true;
        else
            return false;
    }
}
