using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatLenBehavior : LightFont, LightInteract
{	
	[SerializeField] private GameObject LightPrefab;

	private Dictionary<int, LightInfo> thisinfo = new Dictionary<int, LightInfo>();

	private Dictionary<int, GameObject> refracted = new Dictionary<int, GameObject>();

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
		
		
		if(!refracted.ContainsKey(info.id))
		{	
			thisinfo.Add(info.id, info);
			GameObject refBeam = Instantiate(LightPrefab);
			refracted.Add(info.id, refBeam);
			
			SetColor(thiscolor);
			refBeam.GetComponent<LightBeam>().ActivateRay(Physics2D.ClosestPoint(htp.point -htp.normal*120, htp.collider) + info.angle/15, info.angle, thiscolor, htp.point);
			
		}
		else
		{	
			refracted[info.id].GetComponent<LightBeam>().ActivateRay(Physics2D.ClosestPoint(htp.point -htp.normal*120, htp.collider) + info.angle/15, info.angle, thiscolor, htp.point);
		}

	}

	public void LightOff(int ind)
	{
		if(!refracted.ContainsKey(ind-1))
		{
			thisinfo.Remove(ind);
			GameObject refBeam;
			refracted.TryGetValue( ind, out refBeam);
			refracted.Remove( ind);
			Destroy(refBeam);
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
