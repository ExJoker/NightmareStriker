using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

	private GameObject BulletExplosion;
	//粒子預製體

	void Start ()
	{
		BulletExplosion = Resources.Load ("Prefabs/ShellExplosion") as GameObject;
	}

	private void OnTriggerEnter (Collider cos)
	{
		//在碰撞點生成粒子效果(預製體)
		GameObject go = GameObject.Instantiate (BulletExplosion, this.transform.position, this.transform.rotation) as GameObject;
		//銷燬子彈
		GameObject.Destroy (this.gameObject);
		//銷燬粒子預製體
		GameObject.Destroy (go, 1.5f);
		//判斷碰撞者信息
		if (cos.tag == "Player" || cos.tag == "Enemy") {
			cos.SendMessage ("UnderFire");
		}


	}
}
