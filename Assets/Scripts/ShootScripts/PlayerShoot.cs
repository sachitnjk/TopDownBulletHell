using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
	[SerializeField] private Transform shootPoint;
	[SerializeField] private GameObject bulletPrefab;

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
		Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
	}
}
