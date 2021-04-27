using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorButtons : MonoBehaviour
{
    [SerializeField] private GameObject selectedpref;
    [SerializeField] private RawImage img;

    public void SetObject(GameObject g)
    {
    	selectedpref = g;
    	//Debug.Log((Texture)g.GetComponent<SpriteRenderer>().sprite.texture);
    	img.texture = (Texture) g.GetComponent<SpriteRenderer>().sprite.texture;
    }

    public void PlaceObj()
    {
    	if(TouchDetector.Selected)
    	{	

			TouchDetector.Selected.GetComponent<ObjPlacer>().SetObj(selectedpref);
	    	ObjMenuManager.RemoveFromList(selectedpref);
            GameEvents.ScreamEvent("CloseTrayMenu");
	    	Destroy(this.gameObject);
    	}

    }
}
