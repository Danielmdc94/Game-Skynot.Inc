using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
	public float radius;
	public LayerMask itemMask;
	public Vector2 tossForce;
	public Vector2 jumpTossMinMax;
	public Vector2 dropOffset;

	public SpriteRenderer itemGfx;

	private RobotPart equippedPart;
	private Player player;
	private Rigidbody2D rb;
	private Rigidbody2D itemRB;

	// small amount of cooldown time between repeated picking up of objects
	// to prevent jarring canceling of throws
	private const float grabCooldownTime = .5f;
	private float grabCooldown = 0f;

	private const float velocityThreshold = .2f;

	private void Awake()
	{
		player = GetComponent<Player>();
		rb = GetComponent<Rigidbody2D>();
		itemGfx.gameObject.SetActive(false);
	}
	
	private void Update()
	{
		grabCooldown -= Time.deltaTime;
		if (player.queInteract)
		{
			if (equippedPart == null)
				player.queInteract = !PickUp();
			else {
				Drop();
				player.queInteract = false;
			}
		}
	}

		private bool PickUp()
		{
			if (grabCooldown > 0f)
				return false;
			grabCooldown = grabCooldownTime;
			Collider2D col = Physics2D.OverlapCircle(transform.position, radius, itemMask);
			if (col)
				equippedPart = col.GetComponent<RobotPart>();
			if (equippedPart == null)
				return false;
			itemRB = equippedPart.GetComponent<Rigidbody2D>();
			SpriteRenderer rend = col.GetComponent<SpriteRenderer>();
			equippedPart.gameObject.SetActive(false);
			itemGfx.gameObject.SetActive(true);
			itemGfx.sprite = rend.sprite;
			itemGfx.color = rend.color;
			return true;
		}

		private void Drop()
		{
			itemGfx.gameObject.SetActive(false);
			itemRB.gameObject.SetActive(true);
			itemRB.position = transform.position + (Vector3)dropOffset;
			itemRB.rotation = 0;

			float vy = 0f;
			if (rb.velocity.y > velocityThreshold)
				vy = jumpTossMinMax.y;
			else if (rb.velocity.y < -velocityThreshold || Mathf.Abs(rb.velocity.x) < velocityThreshold)
				vy = jumpTossMinMax.x;
			else vy = tossForce.y;
			itemRB.velocity = new Vector2(tossForce.x * Mathf.Clamp(rb.velocity.x, -1f, 1f), vy);
			equippedPart = null;
		}

		// DEBUG
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.white;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
}
