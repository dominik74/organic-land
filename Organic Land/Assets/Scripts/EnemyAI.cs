using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public float movementSpeed;
	public float damage;

	public float idleTimeMin;
	public float idleTimeMax = 0.1f;

	public int movementRange;
	public float minStopDistance = 0.2f;

	public float distanceOfInterest;

	private Transform player;
	private Coroutine currentState;

	private string currentStateName;

	void Start()
	{
		player = PlayerManager.playerUnit.transform;
		StartCoroutine("StateController");
	}

	void ChangeState(IEnumerator nextState, string name)
	{
		if(name != currentStateName)
		{
			if (currentState != null)
				StopCoroutine(currentState);

			currentState = StartCoroutine(nextState);
			currentStateName = name;
		}
	}

	IEnumerator StateController()
	{
		while (true)
		{
			if ((player.position - transform.position).sqrMagnitude <= distanceOfInterest * distanceOfInterest)
			{
				ChangeState(MoveTowardsPlayer(), "movetowards");
			}
			else
			{
				ChangeState(Movement(), "movement");
			}
			yield return null;
		}
	}

	IEnumerator Movement()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(idleTimeMin, idleTimeMax));
			yield return StartCoroutine("MoveTo", GetRandomNearbyPoint());
		}
	}

	IEnumerator MoveTo(Vector3 targetPos)
	{
		bool isMoving = true;
		while (isMoving)
		{
			Vector3 dir = (targetPos - transform.position).normalized;
			dir.y = 0;

			transform.position += dir * movementSpeed * Time.fixedDeltaTime;

			if ((targetPos - transform.position).sqrMagnitude <= minStopDistance * minStopDistance)
				isMoving = false;
			yield return null;
		}
	}

	IEnumerator MoveTowardsPlayer()
	{
		PlayerStats playerStats = player.gameObject.GetComponent<PlayerStats>();
		while (true)
		{
			Vector3 dir = (player.position - transform.position).normalized;
			dir.y = 0;

			transform.position += dir * movementSpeed * Time.fixedDeltaTime;

			if ((player.position - transform.position).sqrMagnitude <= minStopDistance * minStopDistance)
				playerStats.TakeDamage(damage);
			yield return null;
		}
	}

	Vector3 GetRandomNearbyPoint()
	{
		Vector3 pos = transform.position + (Vector3)Random.insideUnitCircle * movementRange;
		pos.y = 1;
		return pos;
	}

}
