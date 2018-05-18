using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitScript : MonoBehaviour {
    public Transform Jpanel;
    float timer = 0;
    bool isStart = false;
        // Use this for initialization
	void Start () {
        Time.timeScale = 1f;
    }
	// Update is called once per frame
	void Update () {
        if (isStart)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                SceneManager.LoadScene(1);
                isStart = false;
            }
            
        }
    }
    public void BeginGame()
    {
        Jpanel.gameObject.SetActive(true);
        isStart = true;
    }
}
