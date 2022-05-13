using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Transform[] targets;

	//	todo: variable zoom
	public float smoothTime;

	private Vector3 smoothDampV;
	private float zoffset;

	private void Start()
	{
		zoffset = transform.position.z;
		targets = GameManager.instance.players.GetComponents<Transform>();
	}
	
	private void LateUpdate()
	{
		if (GameManager.instance.players.Length == 0)
			return ;
		
		private Vector3 targetCenter = targets[0].position;
		(Transform t in targets)
			targetCenter = Vector3.Lerp(targetCenter, t.position, .5f);
		targetCenter = Vector3.SmoothDamp(transform.position, targetCenter, ref smoothDampV, smoothTime);
		transform.position = new Vector3(targetCenter.x, targetCenter.y, zoffset);
	}
}
