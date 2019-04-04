
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
public class MovementController : MonoBehaviour
{
	public Camera cam;
	NavMeshAgent agent;
	public float lookRadius = 10f;
	GameObject FocusTarget;
	Transform TargetPoint;
	Transform point;
	UnitStats CurrentFocus;
	UnitStats TargetFocus;
	public event System.Action OnAttack;
	bool isCurrentlyAttacking = false;
	void Update()
	{
		if (FocusTarget != null && TargetPoint != null)
		{

			float distance = Vector3.Distance(FocusTarget.transform.position, TargetPoint.position);

			if (distance <= agent.stoppingDistance)
			{
				setAttackSpeed();
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.tag == "Unit")
				{
					FocusTarget = hit.transform.gameObject;
					Debug.Log("Hit a unit, setting focus...");
					agent = FocusTarget.GetComponent<NavMeshAgent>();
				}

			}
		}
		if (Input.GetMouseButtonDown(1) && FocusTarget != null)
		{
			Ray ray1 = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit1;

			if (Physics.Raycast(ray1, out hit1))
			{
				point = hit1.transform;
				if (hit1.collider.tag == "team2")
				{
					Debug.Log("Attacking");
					TargetPoint = hit1.transform;
					CurrentFocus = FocusTarget.gameObject.GetComponent<UnitStats>();
					TargetFocus = TargetPoint.gameObject.GetComponent<UnitStats>();
					agent.SetDestination(TargetPoint.position);
				}
				else
				{
					TargetPoint = null;
					if (IsInvoking("attack"))
					{
						CancelInvoke("attack");
					}
					agent.SetDestination(hit1.point);
					faceTargetRay();
				}
			}
		}
	}

	public void setAttackSpeed()
	{
		if (TargetPoint.gameObject != null && !IsInvoking("attack"))
		{
			InvokeRepeating("attack", 0.0f, FocusTarget.GetComponent<UnitStats>().attackspeed);
		}

	}

	void attack()
	{
		if (OnAttack != null) OnAttack();
		if (TargetPoint != null) StartCoroutine(DoDamage(CurrentFocus, TargetFocus, CurrentFocus.attackDelay));

	}

	IEnumerator DoDamage(UnitStats Dealing, UnitStats Receiver, float delay)
	{
		yield return new WaitForSeconds(delay);
		Receiver.takeDamage(Dealing.damage.getValue());
	}


	void faceTargetRay()
	{
		Vector3 direction = (point.position - FocusTarget.transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 180, direction.z));
		FocusTarget.transform.rotation = Quaternion.Slerp(FocusTarget.transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void OnDrawGizmosSelected()
	{
		if (FocusTarget != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(FocusTarget.transform.position, agent.stoppingDistance);
		}
	}
}