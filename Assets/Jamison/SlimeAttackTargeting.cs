using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackTargeting : MonoBehaviour
{
	//This script is used in the kerfuffle of kongregations ad, specifically the enemies use this to move.

	//This makes the slimes move and attack. They prioritize specific 


	//Moving
	[Header("Movement Variables")] public Vector2 CurrentTarget;
	public float speed;
	public Vector2 CurrentPosition;

	//Destroying
	[Header("Destruction")] public string tagToDestroy;
	public GameObject destroyMe;

	//Other
	[Header("Other")] public GameObject TManager;
	public TargetingManager targetManager;

	//Animator
	[Header("Animator")] public Animator slimeanim;

	// Start is called before the first frame update
	void Start()
	{
		TManager = GameObject.Find("Targeting Manager");
		targetManager = TManager.GetComponent<TargetingManager>();
	}

	// Update is called once per frame
	void Update()
	{
		//Move To Building
		transform.Translate((destroyMe.transform.position - gameObject.transform.position) * speed * Time.deltaTime);



	}







	//Destroys Objects by Tag
	public void DestroyByTag(string tagToDestroy)
	{
		GameObject[] thingsToDestroy = GameObject.FindGameObjectsWithTag(tagToDestroy);


		GameObject closestObject = thingsToDestroy[0];
		float distanceToClosestObject = (closestObject.transform.position - transform.position).magnitude;

		foreach (GameObject destroyMe in thingsToDestroy)
		{
			//Finds Closest Building
			float distanceToDestroyMe = (destroyMe.transform.position - transform.position).magnitude;
			if (distanceToDestroyMe < distanceToClosestObject)
			{
				distanceToClosestObject = distanceToDestroyMe;
				closestObject = destroyMe;
			}


			//Priority Targeting (Elixir Storage)
			// if(Are)


			//Move To Building
			




		}

		//Finds Each Building
		void FindTargets()
		{
			if (targetManager.Cannons >= 1)
			{
				FindObject("Cannon");
			}

			else if (targetManager.GStorages >= 1 && targetManager.Cannons <= 0)
			{
				DestroyByTag("GoldStorage");
			}

			else if (targetManager.EStorages >= 1 && targetManager.GStorages <= 0)
			{
				DestroyByTag("ElixirStorage");
			}
		}

		
	}
}