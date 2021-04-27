using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightFont : MonoBehaviour
{
	public Color thiscolor;

	public Collider2D lastcol;
	//public LineRenderer ln;

	public void castLazer(Vector2 pos, Vector2 dir, LineRenderer l, Color c, int i)
	{

		RaycastHit2D _hited = Physics2D.Raycast(pos, dir);

		if(_hited)
		{	
			l.positionCount = i+2;
			l.SetPosition(i+1, _hited.point);
			l.SetPosition(i, pos);
			if(l.positionCount < i+2)
			{
				l.positionCount = i+1;
			}

			if(!lastcol)
			{
				lastcol = _hited.collider;
			}
			
			Debug.DrawLine(pos, _hited.point, Color.red, 0.0f, false);

			
			if(lastcol != _hited.collider)
			{	
				if(lastcol.gameObject.tag == "react")

				{
					lastcol.gameObject.GetComponent<LightInteract>().LightOff(i+1);
				}
				lastcol = _hited.collider;
				

				
			}
			if(_hited.collider.gameObject.tag == "react")
			{	
				_hited.collider.gameObject.GetComponent<LightInteract>().Iluminate(new LightInfo(c, l, _hited, dir, i + 1));
			}
			l.Simplify(0.01f);
			
		}
	}
}
