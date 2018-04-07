using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove01Script : MonoBehaviour
{

	//移动速度
	public float playerMoveSpeed = 5f;
	//获取Animator组件(动画状态机)
	Animator playerAnim;
	int p_move, p_death;
	//获取SkinnedMeshRenderer组件,当玩家收到伤害,身体变红色
	SkinnedMeshRenderer playerSki;
	//获得声音组件
	AudioSource playerAus;
	//玩家受伤声音
	public AudioClip pHurtClip;
	public AudioClip pDeadClip;
	

	void Start ()
	{
		playerAus = GetComponent<AudioSource> ();
		//初始化
		playerAnim = GetComponent<Animator> ();
		p_move = Animator.StringToHash ("Move");
		p_death = Animator.StringToHash ("Death");
		playerSki = transform.Find ("Player").GetComponent<SkinnedMeshRenderer> ();
	}

	void Update ()
	{
		//游戏物体移动
		float hor = Input.GetAxis ("Horizontal");
		float ver = Input.GetAxis ("Vertical");
		transform.Translate (new Vector3 (hor, 0, ver) * playerMoveSpeed * Time.deltaTime);
		if (hor != 0 || ver != 0) {
			playerAnim.SetBool (p_move, true);
		} else {
			playerAnim.SetBool (p_move, false);
		}
		//游戏物体跟随鼠标旋转
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		//射线碰撞信息
		RaycastHit hitInfo;
		if (Physics.Raycast (ray, out hitInfo)) {
			Vector3 target = hitInfo.point;
			target.y = transform.position.y;
			transform.LookAt (target);
		}
		//玩家收到伤害后,身体由红色变白色
		playerSki.material.color = Color.Lerp (playerSki.material.color, Color.white, 0.1f);
	}

	/// <summary>
	/// 掉血
	/// </summary>
	//血量
	public  float HP = 20;

	public void ReductionBlood (float damage)
	{
		HP -= damage;
		if (HP <= 0) {
			playerAnim.SetBool (p_death, true);
			playerSki.material.color = Color.white;
			//播放玩家死亡声音
			playerAus.clip = pDeadClip;
			playerAus.Play ();
			//关闭脚本
			enabled = false;
			GetComponentInChildren<GunBarrelEndScript> ().enabled = false;

		} else {
			//收到伤害玩家变红
			playerSki.material.color = Color.red;
			//播放受伤声音
			playerAus.clip = pHurtClip;
			playerAus.Play ();
		}
	}

	//定时器
	float timer = 0;

	void OnTriggerStay (Collider other)
	{
		timer += Time.deltaTime;
		if (other.transform.tag == "Zombunny" && timer >= 1f) {
			if (HP > 0) {
				ReductionBlood (25f);
				timer = 0;
			}
		}
		if (other.transform.tag == "ZomBear" && timer >= 1f) {
			if (HP > 0) {
				ReductionBlood (20f);
				timer = 0;
			}
		}
		if (other.transform.tag == "Hellephant" && timer >= 1f) {
			if (HP > 0) {
				ReductionBlood (50f);
				timer = 0;
			}
		}

	}


		
}
