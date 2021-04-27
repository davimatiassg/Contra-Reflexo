using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMenu : MonoBehaviour
{
    public float positionx;
    public Camera cam;
    public RectTransform trs;

    private int state = 1; //up = 1; down = -1;

    private bool isok = false;

    private float startpos;

    void Start()
    {
		trs = this.gameObject.GetComponent<RectTransform>();
        cam = Camera.main;

        startpos = trs.anchoredPosition.x;
        trs.anchoredPosition += Vector2.right*trs.rect.width;
        isok = true;
    }

    // Update is called once per frame
    void Update()
    {
     	if(!isok)
     	{
     		if(Mathf.Abs(positionx - trs.anchoredPosition.x) >= 10f)
     		{
     			trs.anchoredPosition = Vector2.right * Mathf.MoveTowards(trs.anchoredPosition.x, positionx, Time.deltaTime*1500);
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

    	positionx = (state+1)*trs.rect.width/2 + startpos;

    }
}
