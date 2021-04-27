using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LightInteract
{
	void Iluminate(LightInfo info);

	void LightOff(int ind);

	void PassToRendering();

	void SetColor(Color col);
}
