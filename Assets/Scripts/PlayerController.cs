using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private float _x;
	private float _y;
	public float speed = 4f;
	private Rigidbody2D playerRigidBody;

	void Start () {
		playerRigidBody = gameObject.GetComponent<Rigidbody2D> ();
		_x = transform.position.x;
		_y = transform.position.y;
	}
	
	void FixedUpdate () {
		/* Player movement */
		_x = Input.GetAxisRaw ("Horizontal");
		_y = Input.GetAxisRaw ("Vertical");
		Vector2 movementDir = new Vector2(_x, _y);
		movementDir.Normalize ();
		playerRigidBody.velocity = movementDir * speed;

		/* Player rotation */
		Vector3 pos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2 (dir.x, dir.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.back);

	}
}
