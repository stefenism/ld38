using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedExplosive : MonoBehaviour {

	private float timer = 0;
	public float fuseLength = 3;

	public GameObject explosionObject;
	public GameObject explosion;

	void Awake()
	{

	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= fuseLength)
		{
			SpawnExplosion();
		}
	}

	void SpawnExplosion()
	{
		//Instantiate(explosionObject, transform.position, Quaternion.identity);
		timer = 0;

		explosion = ObjectPooler.SharedInstance.GetPooledObject(explosionObject.name);

		if(explosion != null)
		{
			explosion.transform.position = transform.position;
			explosion.transform.rotation = transform.rotation;
			explosion.SetActive(true);
			ParticleSystem explosionParticle = explosion.GetComponent<ParticleSystem>();
			explosion.GetComponent<Destructible2D.D2dExplosion>().enabled = true;
			ObjectPooler.SharedInstance.DisableObject(explosion,1f);
			explosionParticle.Clear();
			explosionParticle.time = 0;
			explosionParticle.Play();
		}

		gameObject.SetActive(false);
		//ObjectPooler.SharedInstance.DisableObject(explosionObject);
		//Destroy(explosionObject);
	}
}
