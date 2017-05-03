using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour {

	public float newMass = 0;
	public float massToCash = 5;
	public Vector2 rockSpawnPos;

	public GameObject item;

	private int rocksToSpawn;
	private GameObject clone;
	public int money = 0;
	public UIHandler uiHandler;
	public ShopUI shopUI;





	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if(newMass >= massToCash)
		{
			InitiateRockSpawning();
		}
      uiHandler.money = money;
      shopUI.money = money;
    }

	void InitiateRockSpawning()
	{
		//SpawnRocks(rockSpawnPos);
		uiHandler.currentMass += newMass;
		newMass -= massToCash;
		money += 100;

	}

	void SpawnRocks(Vector2 position)
	{
		clone = Instantiate(item, position, Quaternion.identity) as GameObject;

		clone.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,10f);

		Destroy(clone, 3f);

	}
}
