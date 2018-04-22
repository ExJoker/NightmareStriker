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

	void Update ()
	{
		//得到摄像机要移动到的目标位置
		targetPos = followPlayer.position + Vector3.up * 4f - Vector3.forward * 9;
		//设置摄像机的位置
		transform.position = Vector3.Lerp (transform.position, targetPos, Time.deltaTime * amooth);
		//摄像机看向游戏物体
		transform.LookAt (followPlayer);
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("当前的游戏时间为：" + Time.time);
            Debug.Log("当前时间的缩放比例:" + Time.timeScale);
        }

        
    }
    //重新开始游戏 ，切换场景，将时间的缩放比例调回1
    public void ReGameButtonOnClick()
    {
        Time.timeScale = 1f;
        Debug.Log("当前时间的缩放比例: " + Time.timeScale);
        SceneManager.LoadScene(0);
    }

}
