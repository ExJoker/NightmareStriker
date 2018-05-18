using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextScript : MonoBehaviour
{

	Text scorePlayer;
	Text playerHP;
	float hp;
	Button regame;
    int score;

	void Start ()
	{
		scorePlayer = GameObject.Find ("Score").GetComponent<Text> ();
		playerHP = GameObject.Find ("HP").GetComponent<Text> ();
		regame = GameObject.Find ("Button").GetComponent<Button> ();
		regame.gameObject.SetActive (false);


    }

	void Update ()
	{
        scorePlayer.text = "score:" +  GameObject.Find("Player").GetComponentInChildren<GunBarrelEndScript>().score; ;
		hp = GameObject.Find ("Player").GetComponent<PlayerMove01Script> ().HP;
		if (hp <= 0) {
			hp = 0;

		}
		playerHP.text = "HP:" + hp;
		if (hp == 0) {
            PlayerPrefs.SetInt("分数",score);
			//Time.timeScale = 0;
			regame.gameObject.SetActive (true);
		}
	}


}
