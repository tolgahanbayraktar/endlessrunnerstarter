using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject[] tilePrefabs;

	private Transform playerTransform;
	private float spawnZ = -10.0f;
	private float tileLength = 10.0f;
	private float saveZone = 15.0f;
	private int amnTilesOnScreen = 5;
	private int lastPrefabIndex = 0;

	private List<GameObject> activeTiles;

	void Start()
	{
		activeTiles = new List<GameObject> ();

		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		for (int i = 0; i < amnTilesOnScreen; i++) {

			if (i < 2)
				SpawnTile (0);
			else
				SpawnTile ();
		}
	}

	void Update()
	{
		if (playerTransform.position.z - saveZone > (spawnZ-tileLength*amnTilesOnScreen)) {
			SpawnTile ();
			DeleteTile ();
		}

	}

	void SpawnTile(int prefabIndex=-1)
	{
		GameObject go;

		if (prefabIndex == -1)
			go = Instantiate (tilePrefabs [RandomPrefabIndex ()]) as GameObject;
		else
			go = Instantiate (tilePrefabs [prefabIndex]) as GameObject;

		print (prefabIndex);
		go.transform.SetParent (transform);
		go.transform.position = Vector3.forward * spawnZ;
		spawnZ += tileLength;
		activeTiles.Add (go);


	}

	void DeleteTile()
	{
		Destroy (activeTiles [0]);
		activeTiles.RemoveAt (0);
	}

	int RandomPrefabIndex()
	{
		if (tilePrefabs.Length < 1)
			return 0;

		int randomIndex = lastPrefabIndex;
		while (randomIndex == lastPrefabIndex) {
			randomIndex = Random.Range (0, tilePrefabs.Length);
		}
		lastPrefabIndex = randomIndex;
		return randomIndex;
	}
}
