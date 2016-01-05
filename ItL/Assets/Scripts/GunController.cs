using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunController : MonoBehaviour {
	Rigidbody2D playerRb;
	SpriteRenderer sr;
//	LineRenderer lr;
	Vector3 offset;
	RaycastHit2D hit;
	float barrelLength;
	public float lightSpacing = .5f;
	public GameObject laserLight;
	public float laserForce;
	List<GameObject> availableLaserLights;
	float cooldown = 1f;
	float remainingCooldown;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playerRb = player.GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
//		lr = GetComponent<LineRenderer> ();
//		lr.SetVertexCount (2);
//		lr.SetWidth (.1f, .1f);
		barrelLength = sr.bounds.size.y;
		availableLaserLights = new List<GameObject> ();
		remainingCooldown = 0f;
	}

	// Update is called once per frame
	void Update () {
		Vector2 mousePos = (Vector2) Camera.main.ScreenToWorldPoint (Input.mousePosition);
		offset = transform.up * (barrelLength / 2);
		hit = Physics2D.Raycast (transform.position + offset, mousePos - (Vector2) transform.position);
		if (Input.GetMouseButtonDown (0) && remainingCooldown <= 0) {
			FireLaser(hit, offset);
		} else {
//			lr.enabled = false;
		}
		remainingCooldown -= Time.deltaTime;
	}

	public void FireLaser(RaycastHit2D hit, Vector3 offset) {
//		lr.SetPosition (0, transform.position + offset);
		if (hit.collider != null) {
//			lr.SetPosition (1, hit.point);
//			lr.enabled = true;
			StartCoroutine (IlluminateLaser(transform.position + offset, hit.point, lightSpacing));
			remainingCooldown = cooldown;
			playerRb.AddForce(-transform.up * laserForce);
		}
	}

	IEnumerator IlluminateLaser(Vector3 beg, Vector3 end, float spacing) {
		Ray ray = new Ray (beg, end - beg);
		float laserLength = Vector3.Distance (beg, end);
		for (float i = 0f; i < laserLength; i += spacing) {
			Vector3 lightPos = ray.GetPoint (i) + Vector3.back;
			if (availableLaserLights.Count <= 0) {
				Instantiate(laserLight, lightPos, Quaternion.identity);
			} else {
				GameObject lightObj = availableLaserLights[0];
				lightObj.transform.position = lightPos;
				lightObj.SetActive(true);
				availableLaserLights.RemoveAt(0);
			}
		}

		yield return new WaitForSeconds (.5f);

		availableLaserLights.Clear ();

		foreach (GameObject lightObj in GameObject.FindGameObjectsWithTag("LaserLight")) {
			lightObj.SetActive(false);
			availableLaserLights.Add(lightObj);
		}
	}
}
