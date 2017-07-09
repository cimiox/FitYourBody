using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMuscles : MonoBehaviour 
{

	public void OnValueChangedToggle(GameObject muscle)
	{
		if (this.GetComponent<Toggle>().isOn)
			muscle.SetActive(true);
		else
			muscle.SetActive(false);
	}
}
