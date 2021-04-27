using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorBehavior : LightFont, LightInteract
{	



	private Dictionary<int, LightInfo> thisinfo = new Dictionary<int, LightInfo>();

    void OnEnable()
    {
        GameEvents.StartListening("UpdateLight", PassToRendering);

    }
	void OnDisable()
    {
        GameEvents.StopListening("UpdateLight", PassToRendering);
    }

	public void Iluminate(LightInfo info)
	{	

		RaycastHit2D htp = info.hitPoint;
		Vector2 ndir = Vector2.Reflect(info.angle, htp.normal);
		castLazer(htp.point + ndir/15, ndir, info.origin, info.color, info.id + 1);
		
		if(!thisinfo.ContainsKey(info.id))
		{
			thisinfo.Add(info.id , info);
		}
	}

	public void LightOff(int ind)
	{
		if(!thisinfo.ContainsKey(ind))
		{
			thisinfo.Remove(ind);				
		}	
	}

	public void PassToRendering()
	{	
	}
	public void SetColor(Color col)
	{
		this.gameObject.GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, col.b ,  1.66f  - (col.r + col.g + col.b)/3);
		thiscolor = col;
	}
}
