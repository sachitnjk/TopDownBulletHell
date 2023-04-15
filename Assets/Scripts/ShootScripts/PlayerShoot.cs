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

		GameObject bulletObject = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
		Rigidbody bulletRb = bulletObject.GetComponent<Rigidbody>();
		bulletRb.velocity = shootPoint.forward * bulletSpeed;

		yield return new WaitForSeconds(1f/fireRate);

		canShoot = true;
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
