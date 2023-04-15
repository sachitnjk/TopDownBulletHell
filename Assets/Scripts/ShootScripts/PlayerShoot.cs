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

	private PlayerInput _input;
	private InputAction shootInput;

	private void Start()
	{
		_input = GetComponent<PlayerInput>();
		shootInput = _input.actions["Shoot"];
	}

	private void Update()
	{
		if(shootInput.IsPressed())
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		GameObject bulletObject = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
		Rigidbody bulletRb = bulletObject.GetComponent<Rigidbody>();
		bulletRb.velocity = shootPoint.forward * bulletSpeed;
	}
}
