using UnityEngine;
using System.Collections;

public class Player : Unit {
	private float _x;
	private float _y;
//	public float speed = 4f;
//	float health = 3f;
//	float damage = 1f;
//	private Rigidbody2D playerRigidBody;
	Gun gun;
	float damageCooldown = 1.5f;
	float timeLastHit;
//	GameObject damageFlash;
//	bool isDead;

	protected override void Start () {
		base.Start ();
//		playerRigidBody = gameObject.GetComponent<Rigidbody2D> ();
		_x = transform.position.x;
		_y = transform.position.y;
		gun = gameObject.GetComponentInChildren<Gun> ();
		timeLastHit = 0;
		isDead = false;
	}

	protected override void Update() {
		base.Update ();
//		if (health <= 0) {
//			Die ();
//		}
		if (Input.GetMouseButtonDown (0) && gun.remainingCooldown <= 0) {
			Fire ();
		}
//		Rotate ();
//		Move ();
		timeLastHit += Time.deltaTime;
	}


	/* Player movement */
	protected override void Move() {
		_x = Input.GetAxisRaw ("Horizontal");
		_y = Input.GetAxisRaw ("Vertical");
		Vector2 movementDir = new Vector2(_x, _y);
		movementDir.Normalize ();
		rigidBody.velocity = movementDir * speed;
	}

	/* Player rotation */
	protected override void Rotate() {
		Vector3 pos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2 (dir.x, dir.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.back);
	}

//	public void TakeDamage(float dmg) {
//		health -= dmg;
//		StartCoroutine (Flash ());
//	}
//
//	IEnumerator Flash () {
//		damageFlash.SetActive (true);
//		yield return new WaitForSeconds(0.1f);
//		damageFlash.SetActive (false);
//	}

	void Fire() {
		RaycastHit2D hit = gun.FireLaser ();
		Unit target = hit.collider.gameObject.GetComponent<Unit> ();
		if (target != null) {
			target.TakeDamage(damage);
			Vector2 hitPoint = hit.point;
			Vector2 force = (hitPoint - (Vector2) transform.position).normalized * 2000f;
			target.KnockBack (force, hitPoint);
		}
	}

//	void Die() {
//		gameObject.SetActive (false);
//		isDead = true;
//	}

	public bool IsDead() {
		return isDead;
	}

	bool CanTakeDamage() {
		return timeLastHit >= damageCooldown;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy" && CanTakeDamage()) {
			TakeDamage(1f);
			timeLastHit = 0f;
		}
	}
}
