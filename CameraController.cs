using UnityEngine;

public class CameraController : MonoBehaviour
{
	//	todo: variable zoom
	public float smoothTime;

	private Vector3 smoothDampV;
	private float zoffset;

	private void Awake()
	{
		zoffset = transform.position.z;
	}
	
	private void LateUpdate()
	{
		if (!GameManager.instance || !GameManager.instance.players)
			return ;
		Transform[] targets = GameManager.instance.players;
		private Vector3 targetCenter = targets;
		
		(Transform t in targets)
			targetCenter = Vector3.Lerp(targetCenter, t.position, .5f);
		targetCenter = Vector3.SmoothDamp(transform.position, targetCenter, ref smoothDampV, smoothTime);
		transform.position = new Vector3(targetCenter.x, targetCenter.y, zoffset);
	}
}
