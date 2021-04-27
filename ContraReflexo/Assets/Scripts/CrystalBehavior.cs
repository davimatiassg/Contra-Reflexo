using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBehavior : LightFont, LightInteract
{	
	public bool iluminated;

	[SerializeField] private CrystalGlow glow;

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

		if(info.color == thiscolor && !iluminated)
		{
			CrystalManager.addCrystal(1);
			glow.Activate(thiscolor);
			iluminated = true;
		}
		
	}

	public void LightOff(int ind)
	{		
		CrystalManager.addCrystal(-1);
		iluminated = false;
		glow.Deactivate();
	}

	public void PassToRendering()
	{	
		SetColorHard(thiscolor);
	}

	public void SetColor(Color col)
	{
		this.gameObject.GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, col.b ,  1.66f  - (col.r + col.g + col.b)/3) *3/4;
		thiscolor = col;
	}
	public void SetColorHard(Color col)
	{
		this.gameObject.GetComponent<SpriteRenderer>().color = col;
		thiscolor = col;
	}
}
