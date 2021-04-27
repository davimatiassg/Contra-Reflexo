using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReceiver : MonoBehaviour
{   
	public int hitCount;
	public GameObject Lg;
    public bool changeColor = false;
    public bool haveLight;
    public Color thisColor;
    public Dictionary<int, GameObject> hitPoints = new Dictionary<int, GameObject>();

    public List<Vector2> dirs = new List<Vector2>();
    public List<RaycastHit2D> hits = new List<RaycastHit2D>();

    // Update is called once per frame
    void Awake()
    {
    	if(changeColor)
    	{
    		this.gameObject.GetComponent<SpriteRenderer>().color = thisColor;
    	}
    }
    void LateUpdate()
    {
        if(haveLight)
        {   
            haveLight = false;
        }
        else
        {   
        	hitPoints.Clear();
        	hits.Clear();
        	dirs.Clear();

        	if(!changeColor)
        	{
        		thisColor = Color.white;
        	}
        }
        hitCount = 0;
    }

    void Update()
    {
    	if(this.gameObject.tag == "refract")
	    {	
	    	for(int i = -1; i > hitCount && i < hitPoints.Count; i++)
	    	{	

	    		GameObject l;
	    		hitPoints.TryGetValue(i, out l);
	    		hitPoints.Remove(i);
	    		Destroy(l);
	    	}
	    	for(int i = 0; i < hitCount; i++)
	    	{
	    		GameObject l;
	    		hitPoints.TryGetValue(i, out l);
	    		Refract(l.GetComponent<LazerCaster>(), dirs[i], hits[i]);
	    	}

	        
	    }	
    }

    public void Iluminate(GameObject emiter, Vector2 dir, RaycastHit2D _hited)
    {   
    	
        haveLight = true;
        if(this.gameObject.tag != "reflect")
        {
        	if(!hitPoints.ContainsKey(hitCount))
        	{	
        		GameObject l = Instantiate(Lg,this.gameObject.transform.position, this.gameObject.transform.rotation);
        		if(changeColor)
        		{
	        		l.GetComponent<LazerCaster>().rayCor = thisColor;     			
        		}

        		hitPoints.Add(hitCount, l);
        		hits.Add(_hited);
        		dirs.Add(dir);
        	}
        	else
        	{
        		hits[hitCount] = _hited;
        		dirs[hitCount] = dir;
        	}
        }
        hitCount ++;
	}

	public void Refract(LazerCaster l, Vector2 dir, RaycastHit2D _hited)
	{
		l.clearReflectionPoints(l.gameObject);
		l.addReflectionPoint(_hited.point, l.gameObject);
		l.addReflectionPoint(Physics2D.ClosestPoint(_hited.point -_hited.normal*10, _hited.collider), l.gameObject);
		l.castLazer(Physics2D.ClosestPoint(_hited.point -_hited.normal*10, _hited.collider), Vector2.Reflect(dir, Vector2.Perpendicular(_hited.normal)), l.gameObject);
		l.point = _hited.point;
		l.EParent = this.gameObject;
	}
}
