using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMass : MonoBehaviour {

	private UIHandler uiHandler;

	void Start()
	{
		uiHandler = GameObject.FindGameObjectWithTag("HUD").GetComponent<UIHandler>();
		uiHandler.totalMass += this.gameObject.GetComponent<Rigidbody2D>().mass * 200;
	}

	// Update is called once per frame
	void Update () {

	}
}
