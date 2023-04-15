using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
	public float lifetime = 2f;

	private void Start()
	{
		Invoke("DestroyBulletAfterLifetime", lifetime);
	}
	private void DestroyBulletAfterLifetime()
	{
		Destroy(gameObject);
	}
}
