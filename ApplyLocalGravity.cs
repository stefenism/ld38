using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyLocalGravity : MonoBehaviour {

	private PlayerGravity localGravity;
	private Vector2 gravityVector;
	public GameObject planet;
	// Use this for initialization
	void Awake () {

		planet = GameObject.FindGameObjectWithTag("Planet");
		localGravity = planet.GetComponent<PlayerGravity>();
	}

	// Update is called once per frame
	void Update () {
		//DetermineVectors();
		localGravity.Attract(transform);
	}

	void DetermineVectors()
	{
		gravityVector = localGravity.ForceDirection;
	}
}
