using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hospital : MonoBehaviour {

    private UIScript userInterface;
    public AudioClip AddHp;
    public AudioClip AddHpFull;
    AudioSource audio;
    UIScript ui;

    // Start is called at the beginning
    private void Start()
    {
        // Find the UI in the scene and store a reference for later use
        userInterface = GameObject.FindObjectOfType<UIScript>();
        audio = GetComponent<AudioSource>();
        ui = GameObject.FindObjectOfType<UIScript>();
    }



    // This function gets called everytime this object collides with another
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        string playerTag = otherCollider.gameObject.tag;

        // is the other object a player?
        if (playerTag == "Player" || playerTag == "Player2")
        {
            if (userInterface != null)
            {
                // add one point
                int playerId = (playerTag == "Player") ? 0 : 1;


                HealthSystemAttribute player = otherCollider.GetComponent<HealthSystemAttribute>();
                //Could carry more slime
                if (player)
                {
                    userInterface.AddScore(playerId, player.SaveCount);
                    player.SaveCount = 0;
                    player.GetComponent<Move>().ChangeSpeedAndScale(0);
                    HealthSystemAttribute Hp = player.GetComponent<HealthSystemAttribute>();
                    if (Hp != null)
                    {
                        //Cure Audio Effect
                        if(Hp.health < Hp.MaxHealth)
                        {
                            Hp.health += 1;
                            if (Hp.health < Hp.MaxHealth)
                                audio.clip = AddHp;
                            else
                                audio.clip = AddHpFull;

                            audio.Play();
                            //Update amount of hp on ui
                            if (ui)
                                ui.SetHealth(Hp.health, Hp.GetplayerNumber());
                        }
                    }
                }
            }
        }
    }
}
