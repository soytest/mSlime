﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[AddComponentMenu("Playground/Attributes/Collectable")]
[RequireComponent(typeof(Collider2D))]
public class CollectableAttribute : MonoBehaviour
{
	private UIScript userInterface;
    public AudioClip SaveAucioClip;
    public AudioClip SaveFullClip;
    AudioSource audio;
    bool hasBeenSave = false;

	// Start is called at the beginning
	private void Start()
	{
        audio = GetComponent<AudioSource>();
        // Find the UI in the scene and store a reference for later use
        userInterface = GameObject.FindObjectOfType<UIScript>();
	}



	// This function gets called everytime this object collides with another
	private void OnTriggerEnter2D(Collider2D otherCollider)
	{
        if (hasBeenSave)
            return;        

        string playerTag = otherCollider.gameObject.tag;

		// is the other object a player?
		if(playerTag == "Player" || playerTag == "Player2")
		{
			if(userInterface != null)
			{
				// add one point
				int playerId = (playerTag == "Player") ? 0 : 1;
				

                HealthSystemAttribute player = otherCollider.GetComponent<HealthSystemAttribute>();
                //Could carry more slime
                if (player && !player.IsMaxSave())
                {
                    hasBeenSave = true;
                    player.SaveOneInjureSlime();
                    player.GetComponent<Move>().ChangeSpeedAndScale(player.SaveCount);

                    //Play Audio
                    if (audio)
                    {
                        if (player.SaveCount >= 3)
                            audio.clip = SaveFullClip;
                        else
                            audio.clip = SaveAucioClip;

                        audio.Play();
                    }
                    GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);

                    Destroy(gameObject, 3);
                }
            }			
		}
	}
}
