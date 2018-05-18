using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

	GameObject bullet;
	//子弹预制体
	public float bulletSpeed;
	//子弹发射速度
	Transform bulletPos;
	//子弹发射位置

	private Transform turret;
	//炮台位置

	float shootRate = 0.5f;
	//发射频率
	float elapsedTime;
	//间隔时间

	private void Awake ()
	{
		bullet = Resources.Load ("Prefabs/shell") as GameObject;              //获取子弹预制体
		turret = GameObject.Find ("TankTurret1").GetComponent<Transform> ();   //获取炮台
		bulletPos = turret.GetChild (0).transform;                            //获取发射子弹位置
	}

	// Update is called once per frame
	void Update ()
	{
		elapsedTime += Time.deltaTime;

		if (Input.GetMouseButtonDown (0) && elapsedTime >= shootRate) {
			//清空计时器
			elapsedTime = 0;
			//实例化子弹
			GameObject go = Instantiate (bullet, bulletPos.position, bulletPos.rotation) as GameObject;
			//子彈移動
			go.GetComponent<Rigidbody> ().velocity = go.transform.forward * bulletSpeed;          
		}
	}
}
