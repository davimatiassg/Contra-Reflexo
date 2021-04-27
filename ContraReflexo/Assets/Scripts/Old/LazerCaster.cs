using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerCaster: MonoBehaviour
{
	public GameObject EParent;
	public Vector2 point;
	[SerializeField] LineRenderer lrndr;
	public List<Vector2> vertices = new List<Vector2>();
	public Color rayCor = Color.white;

	void LateUpdate()
	{
		
		drawRay(lrndr, rayCor, vertices, this.gameObject);
		if(EParent)
		{
			if(EParent != this.gameObject)
			{
				if(!EParent.GetComponent<LightReceiver>().hitPoints.ContainsValue(this.gameObject))
				{
					Destroy(this.gameObject);
				}
			}
		}
		else
		{
			Destroy(this.gameObject);
		}
		
		
	}

	public void castLazer(Vector2 pos, Vector2 dir, GameObject emiter)
	{
		LazerCaster em = emiter.GetComponent<LazerCaster>();
		
		RaycastHit2D _hited = Physics2D.Raycast(pos, dir);

		if(_hited)
		{

			Debug.DrawLine(pos, _hited.point, Color.red, 0.0f, false);

			em.addReflectionPoint(_hited.point, emiter);


			if(_hited.collider.gameObject.tag == "reflect")
			{
				ReflectRay(_hited, dir);
			}

			else if(_hited.collider.gameObject.tag == "refract")
			{
				ReflectRay(_hited, dir);
				
			}
		}
	}

	public void drawRay(LineRenderer lr, Color co, List<Vector2> points, GameObject emiter)
	{

		lr.startColor = co;
		lr.endColor = co;
		var pts = new Vector3[points.Count];
		foreach(Vector2 v in points)
		{
			pts[points.IndexOf(v)] = v;
		}
		//emiter.GetComponent<LazerCaster>().lrndr.positionCount = pts.Length;
		lr.positionCount = pts.Length;
		lr.SetPositions(pts);

		clearReflectionPoints(emiter);
	}

	public void addReflectionPoint(Vector2 point, GameObject emiter)
	{

		LazerCaster em = emiter.GetComponent<LazerCaster>();
		em.vertices.Add(point);
	}

	public void clearReflectionPoints(GameObject emiter)
	{
		emiter.GetComponent<LazerCaster>().vertices.Clear();
	}



	public LightReceiver ReflectRay(RaycastHit2D _hited, Vector2 dir)
	{
		castLazer(_hited.point - dir/15, Vector2.Reflect(dir, _hited.normal), this.gameObject);
		LightReceiver hitObj = _hited.collider.gameObject.GetComponent<LightReceiver>();
		hitObj.Iluminate(this.gameObject, dir, _hited);
		return hitObj;
	}
}
