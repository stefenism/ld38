using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WeaponStruct : IComparable<WeaponStruct> {

	public string weaponName;
	public string spawnPoint;
	public Sprite itemImage;
	public GameObject bulletPrefab;
	public RuntimeAnimatorController animationController;
	public float fireRate;
	public float lifeTime;
	public int weaponTier;
	public int weaponType;
	public float fireSpeed;

	public WeaponStruct(string newWeaponName, string newSpawnPoint, Sprite newItemImage,
											GameObject newBulletPrefab, RuntimeAnimatorController newAnimationController,
											float newFireRate, float newLifeTime, int newWeaponTier, int newWeaponType, float newFireSpeed)
	{
		weaponName = newWeaponName;
		spawnPoint = newSpawnPoint;
		itemImage = newItemImage;
		bulletPrefab = newBulletPrefab;
		animationController = newAnimationController;
		fireRate = newFireRate;
		lifeTime = newLifeTime;
		weaponTier = newWeaponTier;
		weaponType = newWeaponType;
		fireSpeed = newFireSpeed;
	}
	// Use this for initialization
	public int CompareTo(WeaponStruct other)
	{
		if(other == null)
		{
			return 1;

		}
		return weaponType - other.weaponType;
	}
	}
