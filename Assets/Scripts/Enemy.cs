using UnityEngine;
using System.Collections;

public class Enemy : Unit {
//	public bool isDead;
//	public float speed = 2;
//	public float damage = 1;
//	public float startHealth = 2;
//	float health;
//	GameObject damageFlash;
//	Rigidbody2D rigidBody;
	Transform playerTransform;
	Player player;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		player = playerTransform.GetComponent<Player> ();
//		rigidBody = gameObject.GetComponent<Rigidbody2D> ();
//		health = startHealth;
//		Transform t = transform.Find ("DamageFlash");
//		damageFlash = t.gameObject;
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (!player.IsDead ()) {
			base.Update ();
		}
		print (player);
	}

	protected override void Move() {
		rigidBody.velocity = transform.up * speed;
	}

	protected override void Rotate () {
		Vector3 dir = playerTransform.position - transform.position;
		float angle = Mathf.Atan2 (dir.x, dir.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.back);
	}

//	public void TakeDamage(float dmg) {
//		health -= dmg;
//		StartCoroutine(Flash ());
//	}

//	public void KnockBack(Vector2 force, Vector2 hitPoint) {
//		rigidBody.AddForceAtPosition (force, hitPoint);
//	}

//	IEnumerator Flash () {
//		damageFlash.SetActive (true);
//		yield return null;// new WaitForSeconds(0.1f);
//		damageFlash.SetActive (false);
//	}
//
//	void Die() {
//		isDead = true;
//		gameObject.SetActive (false);
//	}

	public void Reset() {
		health = startHealth;
		isDead = false;
		gameObject.SetActive (true);
	}
}
