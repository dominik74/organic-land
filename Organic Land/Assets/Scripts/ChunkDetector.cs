using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkDetector : MonoBehaviour {

	public float detectionInterval = 0.25f;

	void Start()
	{
		StartCoroutine("GetChunkInfo");
	}

	IEnumerator GetChunkInfo()
	{
		while (true)
		{
			yield return new WaitForSeconds(detectionInterval);
			RaycastHit hit;
			if(Physics.Raycast(transform.position, Vector3.down, out hit))
			{
				if(hit.transform.gameObject.CompareTag("Ground"))
				{
					Chunk chunk = hit.transform.gameObject.GetComponent<Chunk>();

					DebugScreen.currentBiomeName = chunk.currentBiomeName;
				}
			}
		}
	}

}
