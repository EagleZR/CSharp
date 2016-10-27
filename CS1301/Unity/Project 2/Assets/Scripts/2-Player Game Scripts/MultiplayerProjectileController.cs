﻿using UnityEngine;
using System.Collections;

public class MultiplayerProjectileController : MonoBehaviour {

	public GameObject firingSource;

	public float killTime = 4.0f;

	private float age = 0.0f;


	/* void FixedUpdate () {
		print (Vector3.Distance (gameObject.transform.position, firingSource.transform.position) + " , " + Time.time);
		if (Vector3.Distance (gameObject.transform.position, firingSource.transform.position) > 0.1f) {
			gameObject.GetComponent<Collider> ().enabled = true;
			print ("Collisions on");
		}
	}*/

	void Update () {
		age += 1.0f * Time.deltaTime;
		if (Vector3.Distance (gameObject.transform.position, firingSource.transform.position) > 100.0f || age > killTime) {
			Object.Destroy (gameObject);
		}
	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject != firingSource) {
			if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") {
				other.gameObject.GetComponent <MultiplayerTankController> ().Kill ();

				if (other.gameObject.CompareTag ("Player") && other.gameObject != firingSource && firingSource.name.Contains ("Player")) {
					firingSource.GetComponent<MultiplayerPlayerController> ().DeclareWinner ();
				}

				Object.Destroy (gameObject);
			}
		}
	}
}
