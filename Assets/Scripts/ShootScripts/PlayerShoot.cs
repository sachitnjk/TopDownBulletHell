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

	private bool canShoot;

	private PlayerInput _input;
	private InputAction shootInput;

	private void Start()
	{
		_input = GetComponent<PlayerInput>();
		shootInput = _input.actions["Shoot"];

		canShoot = true;
	}

	private void Update()
	{
		if(shootInput.IsPressed() && canShoot)
		{
			Shoot();
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
}
