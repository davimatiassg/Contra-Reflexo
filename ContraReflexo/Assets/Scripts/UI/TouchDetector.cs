using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{	
	public Camera cam;
	public GameObject[] placers;
	public static GameObject Selected;
    // Start is called before the first frame update
    void Start()
    {	
    	placers = GameObject.FindGameObjectsWithTag("placepoint");
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {	
        	if(Input.GetTouch(0).phase == TouchPhase.Began)
        	{
	        	foreach(GameObject g in placers)
	        	{	
	        		ObjPlacer gCode = g.GetComponent<ObjPlacer>();
	        		if(!gCode.isSelected)
	        		{	
	        			if(Vector2.Distance(cam.ScreenToWorldPoint(Input.GetTouch(0).position), g.transform.position) <= g.transform.localScale.x)
	        			{
	        				if(!TouchDetector.Selected)
	        				{	
	        					gCode.Select();
		        				break;
	        				}
	        				else if(TouchDetector.Selected != g)
	        				{	
	        					TouchDetector.Selected.GetComponent<ObjPlacer>().Unselect();
	        					gCode.Select();
		        				break;
	        				}

	        			}
	        		}
	        	}
	        }
        }
    }

    public static void SelectNone()
    {
    	if(Selected)
    	{
    		Selected.GetComponent<ObjPlacer>().Unselect();
    	}
    }
}
