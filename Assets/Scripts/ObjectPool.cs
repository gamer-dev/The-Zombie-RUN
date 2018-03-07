using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour {

	//This objectpool will always give back an instance that has already been created, or create a new one from the scratch and give it
	
	//Each object pool will be responsible for 1 type of GameObject
	
	public RecycleGameObject prefab;
	
	//Keep track of created instances, usin a last (dynamic)
	
	private List<RecycleGameObject> poolInstances = new List<RecycleGameObject>();
	
	//method to create an instance when we need it
	
	private RecycleGameObject CreateInstance (Vector3 pos)
	{
		var clone = GameObject.Instantiate(prefab);
		clone.transform.position = pos;
		//nest to objectpool
		clone.transform.parent = transform;
		
		poolInstances.Add(clone);
		
		return clone;
		
	}
	
	
	//method to return objects when requested
	
	public RecycleGameObject NextObject(Vector3 pos)
	{
		RecycleGameObject instance = null;
	
		foreach(var go in poolInstances)
		{
			if(go.gameObject.activeSelf != true)
			{
				instance = go;
				instance.transform.position = pos;
				
			}
			
			
		}
		
		if(instance == null)
			instance  = CreateInstance(pos);
		
		
		instance.Restart();
		
		return instance;
		
	}
	
	


}
