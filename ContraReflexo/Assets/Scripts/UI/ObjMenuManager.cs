using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMenuManager : MonoBehaviour
{
	public static List<GameObject> Deployables = new List<GameObject>();

	public List<GameObject> StartDeployables = new List<GameObject>();

	public static ObjMenuManager instance;

	public GameObject ButtonPrefab;
	[SerializeField] static public GameObject PlaceMenu;
	[SerializeField] static public Transform PlaceLocal;

	void Awake()
	{
		if(instance == null)
		{
			instance = this.gameObject.GetComponent<ObjMenuManager>();
			ObjMenuManager.Deployables = instance.StartDeployables;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	void Start()
	{	
		ObjMenuManager.PlaceMenu = GameObject.Find("PlaceMenu");
		ObjMenuManager.PlaceLocal = GameObject.Find("PlaceLocal").transform;
		foreach(GameObject d in ObjMenuManager.Deployables)
		{
			ObjMenuManager.AddToList(d, false);
		}
	}

	public static void TogglePlaceMenu()
	{
		PlaceMenu.SetActive(!PlaceMenu.activeSelf);
	}

	public static void AddToList(GameObject m, bool add = true)
	{
		if(add)
		{
			Deployables.Add(m);
		}
		
		SelectorButtons Obj = Instantiate(instance.ButtonPrefab, ObjMenuManager.PlaceLocal).GetComponent<SelectorButtons>();
		Obj.SetObject(m);
	}

	public static void RemoveFromList(GameObject m)
	{
		Deployables.Remove(m);
	}


}
