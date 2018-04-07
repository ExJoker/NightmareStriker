using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		
	}


    public void BeginGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
