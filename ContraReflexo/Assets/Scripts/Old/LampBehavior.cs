using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(LazerCaster))]
public class LampBehavior : MonoBehaviour
{	
	[SerializeField] private LazerCaster lazerCaster;
	[SerializeField] private Transform castPoint;
	public Color rayColor = Color.white;
	private Transform trs;
	private SpriteRenderer spr;
	public GameObject particles;

	void Start()
	{	

		trs = this.gameObject.GetComponent<Transform>();
		spr = this.gameObject.GetComponent<SpriteRenderer>();
		spr.color = rayColor;
		lazerCaster = this.gameObject.GetComponent<LazerCaster>();
		if(!castPoint)
		{
			castPoint = trs;
		}
		lazerCaster.EParent = this.gameObject;
		lazerCaster.rayCor = rayColor;
		ParticleSystem.MainModule newMain = particles.GetComponent<ParticleSystem>().main;
      	newMain.startColor = rayColor;

	}

	void Update()
	{
		lazerCaster.clearReflectionPoints(this.gameObject);
		lazerCaster.addReflectionPoint(castPoint.position, this.gameObject);
		lazerCaster.castLazer(castPoint.position, trs.right, this.gameObject);
	}
}
