using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAttackEffect : MonoBehaviour
{
	public void attackAnimation(Transform form, GameObject effect)
	{
		Instantiate(effect, form.position, form.rotation);
	}
}
