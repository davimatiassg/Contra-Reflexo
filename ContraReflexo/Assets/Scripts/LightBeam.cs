using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : LightFont
{
    // Start is called before the first frame update
	public LightInfo Light = new LightInfo();


    // Update is called once per frame
    public void ActivateRay(Vector2 pos, Vector2 dir, Color c, Vector2 o_pos)
    {	
    	Light.origin = this.gameObject.GetComponent<LineRenderer>();
        castLazer(pos, dir, Light.origin, c, 1);
        Light.origin.SetPosition(0, o_pos);
        Light.origin.SetPosition(1, pos);

    	Light.origin.startColor = c;
    	Light.origin.endColor = c;
    }
}
