using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 网格脚本,用于将地面划分成多个网格,存放在二维数组中,方便计算
/// </summary>
public class Grid : MonoBehaviour
{
	
	//定义存储格子的集合
	public Node[,] grid;
	//定义整个地形的网格尺寸
	public Vector2 mapSize = new Vector2 (10f, 10f);
	//定义节点的半径
	public float nodeRadius = 0.2f;
	//节点的直径
	public float nodeDiamtete;
	//用于标识节点是在可行走层还是不可行走层
	public LayerMask walkLayer;
	//根据整个地形网格的宽高和节点半径来计算
	//每个方向上有多少个网格,即二维数组的长度和宽度
	public int gridCountX, gridCountY;
	//玩家所在位置
	public Transform player;
	//用来保存正确路径的列表
	public List<Node> path = new List<Node> ();


	void Start ()
	{
		//计算出直径
		nodeDiamtete = nodeRadius * 2;
		//计算出当前格子的数目,用来初始化二维数组
		//RoundToInt:将结果转成int类型,四舍五入
		gridCountX = Mathf.RoundToInt (mapSize.x / nodeDiamtete);
		gridCountY = Mathf.RoundToInt (mapSize.y / nodeDiamtete);
		//根据获得水平和垂直方向上的格子数组初始化数组
		grid = new Node[gridCountX, gridCountY];
//		Debug.Log ("gridCountX: " + gridCountX);
//		Debug.Log ("gridCountY: " + gridCountY);

		//创建格子
		CreateNode ();
	}

	void CreateNode ()
	{
		//startPos:获得是整个地形左下角个字的左下角坐标
		//而不是中心点坐标
		Vector3 startPos = transform.position -
		                   mapSize.x / 2 * Vector3.right -
		                   mapSize.y / 2 * Vector3.forward;
		//遍历二维数组
		for (int i = 0; i < gridCountX; i++) {
			for (int j = 0; j < gridCountY; j++) {
				//每个节点的实际位置应该是格子的中心点坐标
				//每个格子中心点 = 起始点+右边偏移量+前边偏移量
				Vector3 worldPos = 
					startPos +
					Vector3.right * (i * nodeDiamtete + nodeRadius) +
					Vector3.forward * (j * nodeDiamtete + nodeRadius);
				//从当前节点的位置开始去检测,以节点的半径为半径去检测该节点是否会碰撞到碰撞器
				bool canWalk = !Physics.CheckSphere (worldPos, nodeRadius, walkLayer);
				//实例化node
				grid [i, j] = new Node (canWalk, worldPos, i, j);
			}
		}
	}
	//画出网格的边缘,OnDrawGizmos在每帧被调用一次
	void OnDrawGizmos ()
	{
		//以平面的中心为所在位置画出立方体,第二个参数1表示大小
		Gizmos.DrawWireCube (
			transform.position, 
			new Vector3 (mapSize.x, 1, mapSize.y));
		//根据玩家的位置得到玩家所在格子的节点,这个就是寻路的起始点
		Node playerNode = GetNodeFromPosition (player.position);

		//遍历所有节点,如果他是可行走的,标记为白色,否则这标记为红色
		foreach (Node node in grid) {
			Gizmos.color = node._canWalk ? Color.white : Color.red;
			//在每个node所在的位置画一个立方体
			Gizmos.DrawCube (node._workdPos, Vector3.one * (nodeDiamtete - 0.1f));
		}
		if (playerNode == null) {
			//如果没有起始位置,直接返回
			return;
		}
		//画出路径
		if (path != null) {
			//遍历路径数组
			foreach (Node node in path) {
				//计算后的最优路径显示为黑色
				Gizmos.color = Color.black;
				//画出路径
				Gizmos.DrawCube (node._workdPos, Vector3.one * (nodeDiamtete - 0.1f));
			}
		}
		//画出player所在地方
		if (playerNode._canWalk) {
			Gizmos.color = Color.blue;
			Gizmos.DrawCube (playerNode._workdPos, Vector3.one * (nodeDiamtete - 0.1f));
		}
	}

	//根据世界坐标的位置,获取到二维数组中某一个对应节点(格子)
	public Node GetNodeFromPosition (Vector3 pos)
	{
		//算出pos的坐标在整个网络中的横纵方向的百分比
		float percentX = (pos.x + mapSize.x / 2) / mapSize.x;
		float percentY = (pos.z + mapSize.y / 2) / mapSize.y;

		//确保这两个比例在0-1之间的数
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);

		//通过比例得到所在格子的下标
		int x = Mathf.RoundToInt ((gridCountX - 1) * percentX);
		int y = Mathf.RoundToInt ((gridCountY - 1) * percentY);

		//根据下标找到对应的Node
		Node node = grid [x, y];
		Debug.Log ("x = " + node._gridX + "y = " + node._gridY);
		return node;

	}

	//获取指定节点周围的节点
	public List<Node> GetNeibourNode (Node node)
	{
		List<Node> points = new List<Node> ();
		#region 添加
//		points.Add (grid [node._gridX - 1, node._gridY + 1]);
//		points.Add (grid [node._gridX - 1, node._gridY]);
//		points.Add (grid [node._gridX - 1, node._gridY - 1]);
//		points.Add (grid [node._gridX, node._gridY + 1]);
//		points.Add (grid [node._gridX, node._gridY - 1]);
//		points.Add (grid [node._gridX + 1, node._gridY + 1]);
//		points.Add (grid [node._gridX + 1, node._gridY]);
//		points.Add (grid [node._gridX + 1, node._gridY - 1]);
		#endregion
		//下标从-1开始
		for (int i = -1; i <= 1; i++) {
			for (int j = -1; j <= 1; j++) {
				//将自己排除
				if (i == 0 && j == 0) {
					continue;
				}
				//将节点上横轴和纵轴上的值分别加i和j
				int tempX = node._gridX + i;
				int tempY = node._gridY + j;
				//检测是否越界
				if (tempX >= 0 && tempY < gridCountX
				    && tempY >= 0 && tempY < gridCountY) {
					//将节点加入List中
					points.Add (grid [tempX, tempY]);
				}
			}
		}

		return points;
		
	}
}
