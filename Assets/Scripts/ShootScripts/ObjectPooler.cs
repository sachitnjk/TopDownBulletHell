using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public static ObjectPooler instance;

	public GameObject bulletPrefab;
	public List<GameObject> pooledObjects;
	public int countToPool = 20;

	private void Awake()
	{
		if(instance == null)
		{  
			instance = this;
		}
		pooledObjects = new List<GameObject>();
		for(int i = 0; i < countToPool; i++) 
		{
			GameObject instantiatedObject = Instantiate(bulletPrefab);
			instantiatedObject.SetActive(false);
			pooledObjects.Add(instantiatedObject);
		}
	}

	public GameObject GetPooledObject()
	{
		for(int i = 0; i < pooledObjects.Count; i++) 
		{
			if (!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}
		return null;
	}

}
