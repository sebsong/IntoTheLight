using UnityEngine;
using System.Collections;

public abstract class Unit : MonoBehaviour {
	public float startHealth;
	protected float health;
	protected bool isDead;
	public float damage;
	public float speed;
	protected Rigidbody2D rigidBody;
	protected GameObject damageFlash;


	// Use this for initialization
	protected virtual void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		damageFlash = transform.Find ("DamageFlash").gameObject;
		health = startHealth;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (health <= 0) {
			Die ();
		}

		Rotate ();
		Move ();
	}

	protected abstract void Rotate();

	protected abstract void Move();

	public void TakeDamage(float dmg) {
		health -= dmg;
		StartCoroutine (Flash ());
	}

	IEnumerator Flash () {
		damageFlash.SetActive (true);
		yield return new WaitForSeconds(0.1f);
		damageFlash.SetActive (false);
	}

	public void KnockBack(Vector2 force, Vector2 hitPoint) {
		rigidBody.AddForceAtPosition (force, hitPoint);
	}

	void Die() {
		gameObject.SetActive (false);
		isDead = true;
	}
}
