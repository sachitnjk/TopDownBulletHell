using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyOnHit : MonoBehaviour
{
	[SerializeField] private string projectileTag;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == projectileTag)
		{
			other.gameObject.SetActive(false);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if(other.tag == projectileTag)
		{
			other.gameObject.SetActive(false);
		}
	}
}
