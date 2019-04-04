
using UnityEngine;

public class UnitStats : MonoBehaviour
{
	public int currentHeath { get; private set; }

	public Stat heath;
	public Stat damage;
	public Stat energy;
	public Stat armor;

	private void Awake()
	{
		currentHeath = heath.getValue();
	}


	//private void Update()
	//{
	//	//test
	//	if (Input.GetKeyDown("t"))
	//	{
	//		takeDamage(10);
				
	//	}
	//}

	public void takeDamage(int damage)
	{
		damage -= armor.getValue();
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		currentHeath -= damage;
		Debug.Log(transform.name + " Takes: "+ damage +"damage");

		if (currentHeath <= 0)
		{
			Die();
		}
	}

	public virtual void Die()
	{
		//Animation
		Destroy(gameObject);
	}

}
