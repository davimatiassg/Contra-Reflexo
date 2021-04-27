using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayMenu : MonoBehaviour
{
    public float positiony;
    public Camera cam;
    public RectTransform trs;

    private int state = 1; //up = -1; down = 1;

    private bool isok = true;

    private float startpos;
    

    void OnEnable()
    {
        GameEvents.StartListening("ToogleTrayMenu", ToogleTray);
        GameEvents.StartListening("OpenTrayMenu", OpenTray);
        GameEvents.StartListening("CloseTrayMenu", CloseTray);
    }
    void OnDisable()
    {
        GameEvents.StopListening("ToogleTrayMenu", ToogleTray);
        GameEvents.StopListening("OpenTrayMenu", OpenTray);
        GameEvents.StopListening("CloseTrayMenu", CloseTray);
    }

    void Start()
    {
		trs = this.gameObject.GetComponent<RectTransform>();
        cam = Camera.main;
        startpos = Screen.height - trs.rect.height*3/2;
        trs.anchoredPosition += Vector2.up*trs.rect.height;
        
    }

    // Update is called once per frame
    void Update()
    {
     	if(!isok)
     	{
     		if(Mathf.Abs(positiony - trs.anchoredPosition.y) >= 10f)
     		{    
                trs.anchoredPosition = Vector2.up * Mathf.MoveTowards(trs.anchoredPosition.y, positiony, Time.deltaTime*1500);
     		}
     		else
     		{
     			isok = true;
     		}
     	}   
    }


    public void ToogleTray()
    {   
    	isok = false;
    	state = -state;

    	positiony = state*trs.rect.height/2 + startpos;

    }
    public void OpenTray()
    {   
        if(state > 0)
        {
            ToogleTray();
        }
    }
    public void CloseTray()
    {   
        if(state < 0)
        {
            ToogleTray();
        }
    }
}
