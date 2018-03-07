using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A single point in our game to delete, create gameObjects in the game (implement object pooling to Recycle)

public class GameObjectUtil {

	//This will be accessible by all classes, anywhere, anytime
	
	//we'll make static methods for that
	
	private static Dictionary<RecycleGameObject,ObjectPool> pools = new Dictionary<RecycleGameObject, ObjectPool>();
	
	
	private static ObjectPool GetObjectPool(RecycleGameObject reference)
	{
		
		ObjectPool pool = null;
		
		if(pools.ContainsKey (reference))
		{
			pool = pools [reference];
		}
		else 
		{
			var poolContainer = new GameObject(reference.gameObject.name +"ObjectPool");
			pool = poolContainer.AddComponent<ObjectPool>();
			pool.prefab = reference;
			pools.Add(reference, pool);
			
		}
		
		return pool;
		
	}
	
	public static GameObject Instantiate(GameObject prefab, Vector3 pos) //the gameObject we will instantiate, pos for where to exactly instantiate it
	{
		
		GameObject instance = null; //container variable to store the references of the instance that we create
		
		var recycledScript = prefab.GetComponent<RecycleGameObject>();
		
		if(recycledScript != null)
		{
			 var pool = GetObjectPool(recycledScript);
			 instance = pool.NextObject(pos).gameObject;
		}
		else
		{
		instance = GameObject.Instantiate(prefab); //we can't use Monobehaviour's instantiate, use static
		instance.transform.position = pos;
		}
		return instance;
		
	}
	
	public static void Destroy(GameObject gameObject)
	{
		
		var recycleGameObject = gameObject.GetComponent<RecycleGameObject>();
		
		if(recycleGameObject != null)
		{
			recycleGameObject.Shutdown();
		}
		
		else
		{
			GameObject.Destroy(gameObject);
		}
		
	}
	
	
}
