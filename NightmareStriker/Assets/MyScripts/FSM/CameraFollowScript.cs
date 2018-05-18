using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour
{
	//单例脚本
	public static CameraFollowScript Instance;
	Transform enemyPoint;

	void Awake ()
	{
		Instance = this;
		enemyPoint = GameObject.Find ("EnemyPoint").transform;
	}
	//目标点的属性
	public Transform TargetPoint {
		get;
		set;
	}

	/// <summary>
	/// 平滑系数，摄像机跟随的速度
	/// </summary>
	public float speed = 3f;

	void FixedUpdate ()
	{
		if (TargetPoint == null) {
			TargetPoint = enemyPoint;
			return;
		}
		//位置
		transform.position = Vector3.Lerp (transform.position,
			TargetPoint.position, Time.deltaTime * speed);
		//角度
		transform.rotation = Quaternion.Lerp 
			(transform.rotation, TargetPoint.rotation, Time.deltaTime * speed);
	}
}