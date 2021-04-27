using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CrystalManager : MonoBehaviour
{	
	public static int startcrystals = 0;

	public static int crystalqtt = 0;

	public static bool zooming = false;

	[SerializeField] private float speed;

	public static bool stoped;

	public static void addCrystal(int c)
	{
		
		crystalqtt += c;
		if(crystalqtt < 0)
		{
			crystalqtt = 0;
		}
		else if(crystalqtt > startcrystals)
		{
			crystalqtt = startcrystals;
		}

	}

	void Start()
	{	
		speed = 0;
		CrystalManager.startcrystals = Object.FindObjectsOfType<CrystalBehavior>().Length;
		zooming = false;
		Camera.main.gameObject.transform.position = -Vector3.right*48 -Vector3.forward*10;
	}
	void FixedUpdate()
	{	
		if(speed < 0)
		{
			speed = 0;
		}
		else if(speed + Time.fixedDeltaTime > 85)
		{
			speed = 85;
		}
		else
		{	
			speed += 1;
		}

		if(CrystalManager.zooming)
		{	


			if(Camera.main.gameObject.transform.position.x < 30f)
			{
				Camera.main.gameObject.transform.position = Vector3.right*Mathf.MoveTowards(Camera.main.gameObject.transform.position.x, 50, Time.fixedDeltaTime*(86-speed)) -Vector3.forward*10;
			}
			else
			{	
				speed = 0;
				CrystalManager.crystalqtt = 0;		
				if(SceneManager.GetActiveScene().buildIndex > SceneManager.sceneCountInBuildSettings-2)
				{
					SceneManager.LoadScene("TitleScreen");
				}
				else
				{
					SceneManager.LoadScene("Fase" + (SceneManager.GetActiveScene().buildIndex + 1));
				}

			}
		}
		else if(Input.touchCount == 0 || Input.GetTouch(0).phase == TouchPhase.Ended)
		{	
			speed = 0;
			if(Mathf.Abs(CrystalManager.crystalqtt) == CrystalManager.startcrystals)
			{
				GameEvents.ScreamEvent("LevelUp");
				CrystalManager.zooming = true;
			}

			if(Camera.main.gameObject.transform.position.x < 0)
			{	
				Camera.main.gameObject.transform.position = Vector3.right*Mathf.MoveTowards(Camera.main.gameObject.transform.position.x, 0, Time.fixedDeltaTime*(86-speed)) -Vector3.forward*10;
			}
		}
	}
}
