using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
	public float radius;
	public LayerMask itemMask;

	public SpriteRenderer itemGfx;
	public Rigidbody2D itemRB;

	private RobotPart equippedPart;
	private Rigidbody2D rb;

	// small amount of cooldown time between repeated picking up of objects
	// to prevent jarring canceling of throws
	private const float grabCooldownTime = .3f;
	private float grabCooldown = 0f;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
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
			itemRB = equippedPart.GetComponent<Rigidbody2D>();
			Sprite img = col.GetComponent<SpriteRenderer>().sprite;
			equippedPart.gameObject.SetActive(false);
			itemGfx.gameObject.SetActive(true);
			itemGfx.sprite = img;
		}

		private void Drop()
		{
			itemGfx.gameObject.SetActive(false);
			itemRB.gameObject.SetActive(true);
			itemRB.position = transform.position;
			itemRB.rotation = 0;
			itemRB.velocity = rb.velocity * Vector2.one * 2f;
			equippedPart = null;
		}

		// DEBUG
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.white;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
}
