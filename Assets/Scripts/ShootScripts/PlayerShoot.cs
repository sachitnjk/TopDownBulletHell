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

	private PlayerInput _input;
	private InputAction _shootInput;
	private InputAction _SwitchFireMode;


	private enum FireMode
	{
		SingleShot,
		BurstFire,
		AutoFire
	}

	private void Start()
	{
		_input = GetComponent<PlayerInput>();
		_shootInput = _input.actions["Shoot"];
		_SwitchFireMode = _input.actions["SwitchFireMode"];

		canShoot = true;
		currentFireMode = FireMode.SingleShot;
	}

	private void Update()
	{
		if(_shootInput.IsPressed() && canShoot)
		{
			Shoot();
		}
		if(_SwitchFireMode.triggered)
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
			case FireMode.SingleShot:
				ShootBullet();
				yield return new WaitForSeconds(2f);
				break;
			case FireMode.BurstFire:
				for(int i = 0; i < 3; i++)
				{
					ShootBullet();
					yield return new WaitForSecondsRealtime(0.1f);
				}
				yield return new WaitForSeconds(0.8f - 0.3f);  //3 seconds minus the total time spent waiting between shots (0.1 * 3 = 0.3)
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
		GameObject bulletObject = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
		Rigidbody bulletRb = bulletObject.GetComponent<Rigidbody>();
		bulletRb.velocity = shootPoint.forward * bulletSpeed;
	}

	private void ChangeFireMode()
	{
		switch (currentFireMode)
		{
			case FireMode.SingleShot:
				currentFireMode = FireMode.BurstFire;
				break;
			case FireMode.BurstFire:
				currentFireMode = FireMode.AutoFire;
				break;
			case FireMode.AutoFire:
				currentFireMode = FireMode.SingleShot;
				break;
			default:
				break;

		}
	}
}
