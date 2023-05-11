using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
	[SerializeField] private Transform shootPoint;
	[SerializeField] private GameObject bulletPrefab;

	[SerializeField] private float fireRate;
	[SerializeField] private float bulletSpeed;
	[SerializeField] private FireMode currentFireMode;

	private bool canShoot;

	private PlayerInputControls _input;


	private enum FireMode
	{
		BurstFire,
		AutoFire
	}

	private void Start()
	{
		_input = GetComponent<PlayerInputControls>();

		canShoot = true;
		currentFireMode = FireMode.BurstFire;
	}

	private void Update()
	{
		if(_input._shoot.IsPressed() && canShoot)
		{
			Shoot();
		}
		if(_input._switchFireMode.WasPressedThisFrame())
		{
			ChangeFireMode();
		}
	}

	private void Shoot()
	{
		StartCoroutine(ShootCoroutine());
	}

	private IEnumerator ShootCoroutine()
	{
		canShoot = false;

		switch(currentFireMode)
		{
			case FireMode.BurstFire:
				for(int i = 0; i < 3; i++)
				{
					ShootBullet();
					yield return new WaitForSecondsRealtime(0.1f);
				}
				yield return new WaitForSeconds(0.8f - 0.3f);
				break;
			case FireMode.AutoFire:
				ShootBullet();
				yield return new WaitForSeconds(0.1f);
				break;
			default:
				break;
		}

		canShoot = true;
	}

	private void ShootBullet()
	{
		//GameObject bulletObject = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
		GameObject bulletObject = ObjectPooler.instance.GetPooledObject();
		if(bulletObject == null ) 
		{
			return;
		}
		bulletObject.transform.position = shootPoint.position;
		bulletObject.transform.rotation = Quaternion.identity;
		Rigidbody bulletRb = bulletObject.GetComponent<Rigidbody>();
		bulletRb.velocity = shootPoint.forward * bulletSpeed;
		bulletObject.SetActive(true);
	}

	private void ChangeFireMode()
	{
		switch (currentFireMode)
		{
			case FireMode.BurstFire:
				currentFireMode = FireMode.AutoFire;
				break;
			case FireMode.AutoFire:
				currentFireMode = FireMode.BurstFire;
				break;
		}
	}
}
