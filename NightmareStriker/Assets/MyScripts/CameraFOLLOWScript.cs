using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFOLLOWScript : MonoBehaviour
{
   
	//摄像机要跟随的对象
	public  Transform followPlayer;
	//摄像机的平滑系数
	float amooth = 5f;
	//摄像机应该在的目标点
	Vector3 targetPos;

    public Transform[] sources;

    void Update ()
	{
		//得到摄像机要移动到的目标位置
		targetPos = followPlayer.position + Vector3.up * 3.5f + Vector3.forward * 7f;
		//设置摄像机的位置
		transform.position = Vector3.Lerp (transform.position, targetPos, Time.deltaTime * amooth);
		//摄像机看向游戏物体
		transform.LookAt (followPlayer);
        if (followPlayer.GetComponent<PlayerMove01Script>().HP<= 0)
        {
            sources[0].gameObject.SetActive(false);
            sources[1].gameObject.SetActive(false);

        }

    }
  


 


//重新开始游戏 ，切换场景，将时间的缩放比例调回1
public void ReGameButtonOnClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public GameObject JPanel;
    public void Start001()
    {
        JPanel.SetActive(false);
    }
}
