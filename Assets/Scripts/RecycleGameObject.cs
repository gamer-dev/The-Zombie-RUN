using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IRecycle
{
	void Restart();
	void Shutdown();
}



public class RecycleGameObject : MonoBehaviour {
	
	
	private List<IRecycle> recycleComponents;
	
	void Awake()
	{
		
		var components = GetComponents<MonoBehaviour> ();
		recycleComponents = new List<IRecycle> ();
		
		foreach(var component in components)
		{
			
			if(component is IRecycle)
			{
				recycleComponents.Add (component as IRecycle);
			}
			
			
		}
				
	}
	
	

	//To create a new obstacle
	public void Restart()
	{
		
		gameObject.SetActive(true); //Make it exist in Scene, Hierarchy; won't be visible and execute its scripts
		
		foreach(var component in recycleComponents)
		{
			component.Restart();
		}
		
	}
	
	//To shutdown an obstacle, not delete it actually
	public void Shutdown()
	{
		
		gameObject.SetActive(false);
		
		foreach(var component in recycleComponents)
		{
			component.Shutdown();
		}
		
	}
	

	
}
