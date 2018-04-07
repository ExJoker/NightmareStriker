using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceOfEvilScript : MonoBehaviour
{
	//获取怪物预设体
	public Transform[] zombunnyPrafab;

	//定时器
	float timer;

	void Start ()
	{
		
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if (timer > 5f) {
            Instantiate (zombunnyPrafab [Random.Range (0, zombunnyPrafab.Length)], 
				transform.position, Quaternion.identity);
			timer = 0;
           
        }



        
       
		
	}



}
