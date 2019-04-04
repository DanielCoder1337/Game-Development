using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
	public Camera cam;
	public GameObject Unit;

	#region Singleton

	public static UnitManager instance;

	private void setNewFocus()
	{
		instance = this;
	}


	#endregion

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.tag == "Unit") {
					setNewFocus();
					Unit = hit.transform.gameObject;
				}
			}
		}
	}

}
