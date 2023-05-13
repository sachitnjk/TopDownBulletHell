using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
	[Header("Projectile Settings")]
	[SerializeField] private int numberOfProjectiles;
	[SerializeField] private float projectileSpeed;

	[Header("Projectile direction and position related")]
	private Vector3 startPoint;
	private const float radius = 1f;

	private void Update()
	{
		startPoint = transform.position;
		SpawnProjectile(numberOfProjectiles);
	}
	private void SpawnProjectile(int _numberOfProjectiles)
	{

		float angleStep = 360f / _numberOfProjectiles;
		float angle = 0f;

		for(int i = 0; i <= _numberOfProjectiles - 1; i++) 
		{
			//Direction calculation
			float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
			float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

			Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
			Vector3 projectileMoveDirection = (projectileVector - startPoint).normalized * projectileSpeed;

			//projectile setting activen and applying velocity

			GameObject projectileObject = EnemyObjectPooler.instance.GetPooledObject();
			if (projectileObject == null)
			{
				return;
			}
			projectileObject.transform.position = startPoint;
			projectileObject.transform.rotation = Quaternion.identity;
			projectileObject.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.y);

			projectileObject.SetActive(true);
			angle += angleStep;

		}
	}
}
