using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGlow : MonoBehaviour
{	
	private Transform trs;
	private float size;
    void Start()
    {
    	trs = this.gameObject.GetComponent<Transform>();
    }
    public void Deactivate()
    {
        size = 0f;
    }

    // Update is called once per frame
    public void Activate()
    {
        size = 1f;
    }

    public void Activate(Color c)
    {	
    	this.gameObject.GetComponent<SpriteRenderer>().color = c;
         size = 1f;
    }

    void FixedUpdate()
    {
    	trs.localScale = Vector2.MoveTowards(trs.localScale, Vector2.one*size, Time.fixedDeltaTime*5);
    }
}
