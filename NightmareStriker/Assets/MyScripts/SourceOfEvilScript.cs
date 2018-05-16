using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SourceOfEvilScript : MonoBehaviour
{
	//获取怪物预设体
	public Transform[] zombunnyPrafab;

	//定时器
	float timer = 0;

	void Update ()
	{
		timer += Time.deltaTime;

		if (timer > 5f) {
          Transform go =  Instantiate (zombunnyPrafab[0], 
				transform.position, Quaternion.identity) ;
            go.SetParent(transform);
			timer = 0;
        }

        
		
	}
}
