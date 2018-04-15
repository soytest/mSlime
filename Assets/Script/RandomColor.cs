using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomColor : MonoBehaviour {

    public float ChangeInteral = 1.5f;
    public float ChangeSpeed = 100;
    Color DesiseColor;
    float Timer;
    Text text;

	void Start () {
        text = GetComponent<Text>();
    }
	

	void Update () {
        Timer += Time.deltaTime;
        if(Timer > ChangeInteral)
        {
            Timer = 0;
            DesiseColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            text.color = DesiseColor;
        }
        
    }
}
