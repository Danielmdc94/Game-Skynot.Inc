using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
	public float radius;
	public LayerMask itemMask;

	public SpriteRenderer itemGfx;
	public Rigidbody2D itemPrefab;

	private RobotPart equippedPart;

	private void Awake()
	{
		itemGfx.gameObject.SetActive(false);
	}
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (equippedPart == null)
				PickUp();
			else Drop();
		}
	}

		private void PickUp()
		{
			Collider2D col = Physics2D.OverlapCircle(transform.position, radius, itemMask);
			if (col)
				equippedPart = col.GetComponent<RobotPart>();
			if (equippedPart == null)
				return ;
			Debug.Log("pickup object");
			Sprite img = col.GetComponent<SpriteRenderer>().sprite;
			Destroy(equippedPart.gameObject);
			itemGfx.gameObject.SetActive(true);
			itemGfx.sprite = img;
		}

		private void Drop()
		{
			itemGfx.gameObject.SetActive(false);
			Rigidbody2D rb = Instantiate<Rigidbody2D>(itemPrefab, transform.position, Quaternion.identity);
			rb.velocity = Vector2.zero;
			equippedPart = null;
		}

		// DEBUG
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.white;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
}
