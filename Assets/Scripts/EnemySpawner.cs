using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	public float spawnTime = 2f;
	public GameObject enemyType;
	float timeUntilNextSpawn;
	List<GameObject> availableEnemies;

	// Use this for initialization
	void Start () {
		availableEnemies = new List<GameObject> ();
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
			if (!enemy.activeInHierarchy) {
				availableEnemies.Add (enemy);
			}
		}
		timeUntilNextSpawn = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeUntilNextSpawn <= 0) {
			SpawnEnemy ();
			timeUntilNextSpawn = spawnTime;
		}
		timeUntilNextSpawn -= Time.deltaTime;
	}

	void SpawnEnemy() {
		if (availableEnemies.Count > 0) {
			Enemy enemyToSpawn = availableEnemies[0].GetComponent<Enemy> ();
			enemyToSpawn.transform.position = transform.position;
			enemyToSpawn.Reset ();
			availableEnemies.RemoveAt (0);
		} else {
			Instantiate(enemyType, transform.position, Quaternion.identity);
		}
	}
}
