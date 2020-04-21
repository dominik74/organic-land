﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteGenerator : MonoBehaviour {

	public int halfTileAmountX;
	public int halfTileAmountZ;
	public int tileSize = 10;
	public int clampDistance;
	public GameObject player;

	public GameObject tileTemplate;

	private Vector3 startPos;
	private Hashtable tiles = new Hashtable();

	class Tile
	{
		public GameObject theTile;
		public float creationTime;

		public Tile(GameObject t, float ct)
		{
			theTile = t;
			creationTime = ct;
		}
	}

	void Start()
	{
		startPos = new Vector3(0, 0, 0);
		float updateTime = Time.realtimeSinceStartup;

		for (int x = -halfTileAmountX; x < halfTileAmountX; x++)
		{
			for (int z = -halfTileAmountZ; z < halfTileAmountZ; z++)
			{
				Vector3 pos = new Vector3((x * tileSize + startPos.x), 0, (z * tileSize + startPos.z));

				if ((player.transform.position - pos).sqrMagnitude > clampDistance * clampDistance)
					continue;

				GameObject t = Instantiate(tileTemplate);
				string tileName = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();

				t.transform.position = pos;
				t.name = tileName;
				Tile tile = new Tile(t, updateTime);
				tiles.Add(tileName, tile);
			}
		}

		StartCoroutine("UpdateTerrain");
	}

	IEnumerator UpdateTerrain()
	{
		while (true)
		{
			int xDeltaMove = (int)(player.transform.position.x - startPos.x);
			int zDeltaMove = (int)(player.transform.position.z - startPos.z);

			if (Mathf.Abs(xDeltaMove) >= tileSize || Mathf.Abs(zDeltaMove) >= tileSize)
			{
				float updateTime = Time.realtimeSinceStartup;

				int playerX = (int)(Mathf.Floor(player.transform.position.x / tileSize) * tileSize);
				int playerZ = (int)(Mathf.Floor(player.transform.position.z / tileSize) * tileSize);

				for (int x = -halfTileAmountX; x < halfTileAmountX; x++)
				{
					for (int z = -halfTileAmountZ; z < halfTileAmountZ; z++)
					{
						Vector3 pos = new Vector3((x * tileSize + playerX), 0, (z * tileSize + playerZ));


						if ((player.transform.position - pos).sqrMagnitude > clampDistance * clampDistance)
							continue;

						string tileName = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();

						if (!tiles.ContainsKey(tileName))
						{
							GameObject t = Instantiate(tileTemplate);
							t.name = tileName;
							t.transform.position = pos;

							Tile tile = new Tile(t, updateTime);
							tiles.Add(tileName, tile);
							yield return null;
						}
						else
						{
							(tiles[tileName] as Tile).creationTime = updateTime;
						}
					}
				}

				Hashtable newTiles = new Hashtable();
				foreach (Tile tls in tiles.Values)
				{
					if (tls.creationTime != updateTime)
					{
						Destroy(tls.theTile);
					}
					else
					{
						newTiles.Add(tls.theTile.name, tls);
					}
				}

				tiles = newTiles;

				startPos = player.transform.position;
			}
			
			yield return null;
		}
	}

}
