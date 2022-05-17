using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RobotDeposit : MonoBehaviour
{
	private GameManager gm;

	public LayoutGroup asmGFXparent;
	public Image arrowGFX;

	public LayerMask blastMask;
	public float blastRadius;
	public float blastForce;
	public Vector2 blastDirection;
	
	private static Vector3 arrowOffset;
	private Image[] asmGFX;
	
	[HideInInspector]
	public Stack<RobotPart.RobotPartType> assembly = new Stack<RobotPart.RobotPartType>();

	private void Start()
	{
		gm = GameManager.instance;
		asmGFX = asmGFXparent.GetComponentsInChildren<Image>();
		arrowOffset = arrowGFX.transform.localPosition;
		Debug.Log(arrowOffset);
		
		Initialize();
		GUI_Refresh(assembly);
	}

	private void Update()
	{
		arrowGFX.transform.localPosition += Vector3.right * Mathf.Sin(Time.time * 10f) * .02f;
	}

	public void Initialize()
	{
		Blast();
		assembly.Clear();
		int count = Random.Range(1, asmGFX.Length + 1);
		while (count-- > 0)
		{
			RobotPart.RobotPartType rptype = RobotPart.RandomType();
			assembly.Push(rptype);
		}
	}
	
	public void Evaluate(RobotPart part)
	{
		RobotPart.RobotPartType expectedPart = assembly.Pop();
		SpawnManager.RecallToPool(part);
		if (part.partType == expectedPart)
		{
			if (assembly.Count == 0)
			{
				gm.UpdateScore(1);
				Initialize();
			} 
		}
		else
		{
			gm.UpdateLives(-1);
			Initialize();
		}
		GUI_Refresh(assembly);
	}

	private void Blast()
	{
		Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, blastRadius, blastMask);
		foreach (Collider2D c in results)
		{
			Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
			if (rb != null)
				rb.AddForce(blastDirection * blastForce * rb.gravityScale *
							(blastRadius / (Vector2.Distance(rb.position, transform.position) + .1f))   );
		}
	}

	private void GUI_Refresh(Stack<RobotPart.RobotPartType> input)
	{
		Stack<RobotPart.RobotPartType> stack = new Stack<RobotPart.RobotPartType>(input);
		int i = 0;
		while (stack.Count > 0)
			asmGFX[i++].sprite = gm.GetImage(stack.Pop());
		int index = Mathf.Max(0, i - 1);
		arrowGFX.transform.SetParent(asmGFX[index].transform);
		arrowGFX.transform.localPosition = arrowOffset;
		while (i < asmGFX.Length)
			asmGFX[i++].sprite = gm.GetImage(null);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, blastRadius);
		Gizmos.DrawLine(transform.position, (Vector2)transform.position + blastDirection);
	}
}

// TODO: In-game key for quitting application
// counter for lives
