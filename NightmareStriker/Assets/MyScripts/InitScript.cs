using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 1f;
    }
	// Update is called once per frame
	void Update () {
        
    }


    public void BeginGame()
    {
        SceneManager.LoadScene(1);
    }
}
