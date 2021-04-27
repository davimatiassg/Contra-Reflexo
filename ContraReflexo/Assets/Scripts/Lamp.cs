using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : LightFont
{
    [SerializeField] private GameObject LightObj;

    [SerializeField] private LineRenderer Light;

    [SerializeField] private GameObject Particles;

    private ParticleSystem.ColorOverLifetimeModule col;


    void OnEnable()
    {
        GameEvents.StartListening("UpdateLight", ActivateRay);
    }
	void OnDisable()
    {
        GameEvents.StopListening("UpdateLight", ActivateRay);
    }

    void Start()
    {      
    	GameEvents.ScreamEvent("UpdateLight");
    }
    void ActivateRay()
    {   
        this.gameObject.GetComponent<SpriteRenderer>().color = thiscolor;


        var main = Particles.GetComponent<ParticleSystem>().main;
        main.startColor = new ParticleSystem.MinMaxGradient(thiscolor);
        castLazer(LightObj.transform.position, LightObj.transform.right, Light, thiscolor, 0);


        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(thiscolor, 1f)},
            new GradientAlphaKey[] { new GradientAlphaKey(1, 1.0f) }
        );
        Light.SetPosition(0, LightObj.transform.position);
        Light.colorGradient = gradient;
    }
}
