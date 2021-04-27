using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInfo
{
    // Start is called before the first frame update
	public Color color;
	public LineRenderer origin;
	public RaycastHit2D hitPoint;
	public Vector2 angle;
	public int id;

	public LightInfo(Color c, LineRenderer l, RaycastHit2D r, Vector2 a, int i)
	{
		color = c;
		origin = l;
		hitPoint = r;
		angle = a;
		id = i;
	}

	public LightInfo()
	{
		color = Color.white;
		origin = null;

		angle = Vector2.zero;
		id = -1;
	}

    // Update is called once per frame
    public override string ToString()
    {
        return "Color: " + color + "Origin: " + origin;
    }
    public string ShowAngle()
    {
    	return angle.ToString();
    }
}

