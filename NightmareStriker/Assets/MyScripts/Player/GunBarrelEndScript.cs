using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunBarrelEndScript : MonoBehaviour
{
	//粒子系统
	ParticleSystem ps;
	//线性渲染组件
	LineRenderer lineRenderer;
	//声音组件
	AudioSource aus;


	public int score;

    private static GunBarrelEndScript instance = null;

    private GunBarrelEndScript()
    {

    }


    public static GunBarrelEndScript Instance
    {
        get
        {
            if(instance == null )
            {
                instance = new GunBarrelEndScript();
            }
            return instance;
        }
    }



    void Start ()
	{
		//初始化
		ps = this.GetComponentInChildren<ParticleSystem> ();
		lineRenderer = GetComponent<LineRenderer> ();
		aus = GetComponent<AudioSource> ();
	}


	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			aus.enabled = true;
			aus.Play ();
			lineRenderer.enabled = true;
			lineRenderer.positionCount = 2;
			lineRenderer.SetPosition (0, transform.position);
			Ray ray = new Ray (transform.position, transform.forward);
			//射线碰撞信息
			RaycastHit hitInfo;
			//如果发生碰撞
			if (Physics.Raycast (ray, out hitInfo)) {
				//射击目标点
				Vector3 target = hitInfo.point;
				lineRenderer.SetPosition (1, target);
				//通过tag值判断游戏物体打中怪物
				if (hitInfo.collider.tag == "ZomBear") {
					hitInfo.collider.GetComponent<ZombunnyMoveScript> ().ZDamage (30f);
					if (hitInfo.collider.GetComponent<ZombunnyMoveScript> ().Z_HP <= 0) {
						score += 20;
					}
				}
				if (hitInfo.collider.tag == "Zombunny") {
					hitInfo.collider.GetComponent<ZombunnyMoveScript> ().ZDamage (30f);
					if (hitInfo.collider.GetComponent<ZombunnyMoveScript> ().Z_HP <= 0) {
						score += 30;
					}
				}
				if (hitInfo.collider.tag == "Hellephant") {
					hitInfo.collider.GetComponent<ZombunnyMoveScript> ().ZDamage (30f);
					if (hitInfo.collider.GetComponent<ZombunnyMoveScript> ().Z_HP <= 0) {
						score += 50;
					}
				}

			} else {
				//如果发生不碰撞
				lineRenderer.SetPosition (1, transform.forward * 100f);
			}
			StartCoroutine (LR ());
			//粒子系统
			Play ();
		}
	}
	//弹道的时间为0.05f
	IEnumerator LR ()
	{
		yield return new WaitForSeconds (0.05f);
		lineRenderer.enabled = false;
	}
	//播放声音组件
	public void Play ()
	{
		ps.Play ();
	}

}
