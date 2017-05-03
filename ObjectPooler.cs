using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	public static ObjectPooler SharedInstance;

	public List<GameObject> pooledObjects;
	public GameObject[] objectsToPool;
	public int amountToPool;

	public GameObject bulletParent;

	void Awake()
	{
		SharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		pooledObjects = new List<GameObject>();
		for(int j = 0; j < objectsToPool.Length; j++)
		{
			for(int i = 0; i < amountToPool; i++)
			{
				GameObject obj = (GameObject)Instantiate(objectsToPool[j]);
				obj.SetActive(false);
				obj.transform.parent = bulletParent.gameObject.transform;
				pooledObjects.Add(obj);
			}
		}

	}

	// Update is called once per frame
	void Update () {

	}

	public GameObject GetPooledObject(string objectName)
	{
		//Debug.Log("object is gettin' got");
		//Debug.Log("object name: " + objectName);
		//1
		for(int i = 0; i < pooledObjects.Count; i++)
		{
			//2
			//Debug.Log("pooledobjectsname: " + pooledObjects[i].name);
			if(pooledObjects[i].name == (objectName + "(Clone)"))
			{
				if(!pooledObjects[i].activeInHierarchy)
				{
					return pooledObjects[i];
				}
			}
		}
		return null;
	}

	public void DisableObject(GameObject obj, float time = 0f)
	{
		StartCoroutine(DisableWait(obj, time));
	}

	private IEnumerator DisableWait(GameObject obj, float time)
	{
		yield return new WaitForSeconds(time);
		obj.SetActive(false);
	}
}
