using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPlacer : MonoBehaviour
{	
	[SerializeField] private Transform trs;
	private SpriteRenderer spr;
    private Vector2 startTouch;
    [SerializeField] public bool isSwiping;
    [SerializeField] public bool isSelected;

    public GameObject Plotable;

    public GameObject Ploted;

    private Vector3 pos;

   	public Camera cam;

    private Vector3 actualEuler = Vector3.zero;

    void Start()
    {
        trs = this.gameObject.GetComponent<Transform>();
        cam = Camera.main;
        pos = trs.position;
        spr = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    	
        if(Input.touchCount > 0)
        {	
        	if(isSwiping && isSelected)
	    	{	
	    		
	    		trs.eulerAngles = Vector3.forward*(Vector2.SignedAngle(cam.ScreenToWorldPoint(startTouch) - pos, cam.ScreenToWorldPoint(Input.GetTouch(0).position) - pos)) + actualEuler;
	    	}
        	if(Input.GetTouch(0).phase == TouchPhase.Began)
        	{
        		startTouch = Input.GetTouch(0).position;
        		if((cam.ScreenToWorldPoint(Input.GetTouch(0).position) - trs.position).magnitude <= 11)
        		{
        			isSwiping = true;
        			actualEuler = trs.eulerAngles;

        		}
        		
        	}
        	else if(Input.GetTouch(0).phase == TouchPhase.Ended)
        	{
        		isSwiping = false;
                
        	}
        }
        if(isSelected && !isSwiping)
        {
        	spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, (0.85f + Mathf.Sin(Time.time*10)*0.15f));
        }
        else
        {   
            if(isSwiping)
            {
                GameEvents.ScreamEvent("UpdateLight");
            }
            if(Ploted)
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0.85f);
            }
            else
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1);
            }
        	
        }
    }

    public void Unselect()
    {
    	GameEvents.ScreamEvent("CloseTrayMenu");
    	isSelected = false;
    	TouchDetector.Selected = null;
    }

	public void Select()
    {	
    	GameEvents.ScreamEvent("OpenTrayMenu");
    	isSelected = true;
    	TouchDetector.Selected = this.gameObject;
    }

    public void RemoveObj()
    {	
    	Destroy(Ploted);
    	Ploted = null;

    	ObjMenuManager.AddToList(Plotable);
    	Plotable = null;
    }

    public void SetObj(GameObject p)
    {
		if(Plotable)
    	{
    		RemoveObj();
    	}
    	Plotable = p;
    	Ploted = Instantiate(p, trs);
        Ploted.GetComponent<LightInteract>().SetColor(new Color(spr.color.r, spr.color.g, spr.color.b, 1));
        GameEvents.ScreamEvent("UpdateLight");
    	

    }

}
