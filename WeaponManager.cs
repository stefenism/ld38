using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

	public string fireWeaponButton;
	public string switchGunUpButton;
	public string switchGunDownButton;
	public GameObject[] toolInventory;
	private StructHolder currentWeapon;
	private Animator anim;
	private bool shooting;
	public bool fireAnim = false;
	private int equippedItem;
	public GameObject[] spawnPoints;

	public GameObject gun;
	private Destructible2D.D2dGun gunn;

    public CoinSound audio;
		public bool canControl = true;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if(canControl)
		{
			CheckShooting();
			CheckSwitching();
		}

		SetAnims();
	}

	void FixedUpdate()
	{
		if(shooting)
		{
			Shoot();
		}
	}

	void CheckSwitching()
	{


		float inventoryLength = 0;
		for(int i = 0; i < toolInventory.Length; i++)
		{
			if(toolInventory[i] != null)
			{
				inventoryLength += 1;
			}
		}


		if(Input.GetButtonDown(switchGunUpButton))
		{
			if(equippedItem + 1 < inventoryLength)
			{
				if(toolInventory[equippedItem + 1] != null)
				{
					equippedItem += 1;
				}
			}
			else
			{
				equippedItem = 0;
			}

		}

		if(Input.GetButtonDown(switchGunDownButton))
		{
			if(equippedItem -1 >= 0)
			{
				if(toolInventory[equippedItem - 1] != null)
				{
					equippedItem -= 1;
				}

			}
			else
			{
				equippedItem = (int)inventoryLength -1;
			}

			}
		gunn = gun.GetComponent<Destructible2D.D2dGun>();
		currentWeapon = toolInventory[equippedItem].GetComponent<StructHolder>();
		anim.runtimeAnimatorController = currentWeapon.weaponData.animationController;

	}

	void CheckShooting()
	{
		/*
		if(toolInventory.Length > 0)
		{
			currentWeapon = toolInventory[equippedItem].GetComponent<StructHolder>();
		}
		*/
		if(Input.GetButtonDown(fireWeaponButton))
		{
			shooting = true;
		}
		if(Input.GetButtonUp(fireWeaponButton))
		{
			shooting = false;
		}
	}

	void SetAnims()
	{
		fireAnim = (gunn.CanShoot && shooting);
		if(fireAnim)
		{
			anim.SetTrigger("Shooting");
		}
		else
		{
			anim.SetBool("Shooting", false);
		}
	}

	void Shoot()
	{

		Vector3 currentSpawnPoint = Vector3.zero;

		gunn.BulletPrefab = currentWeapon.weaponData.bulletPrefab;
		gunn.ShootDelay = currentWeapon.weaponData.fireRate;

		for(int i = 0; i < spawnPoints.Length; i++)
		{
			if(spawnPoints[i].gameObject.name == currentWeapon.weaponData.spawnPoint)
			{
				//Debug.Log("weaponname: " + currentWeapon.weaponData.weaponName);
				currentSpawnPoint = spawnPoints[i].gameObject.transform.position;

				//Debug.Log("currentSpawnPoint: " + currentSpawnPoint);
				//i += spawnPoints.Length;
			}
		}




		Vector3 direction = (currentSpawnPoint - transform.position).normalized;// - transform.position).normalized;

		float speed = .2f;

		gunn.Shoot(currentSpawnPoint, currentWeapon.weaponData.fireSpeed, currentWeapon.weaponData.lifeTime, currentWeapon.weaponData.weaponName, audio);

		/*
		set gun.BulletPrefab
		set gun.shootdelay
		set direction for gun
		set speed of bullet
		gun.Shoot(direction, speed);
		*/
	}
}
