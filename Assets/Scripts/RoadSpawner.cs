using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RoadSpawner : MonoBehaviour
{
    protected GameObject roadPrefab;
    protected GameObject roadSpawnPosition;
    protected float distance = 0;
    protected GameObject roadCurrent;

    private void Awake()
    {
        roadPrefab = GameObject.Find("RoadPrefab");
        roadSpawnPosition = GameObject.Find("RoadSpawnPosition");
        roadPrefab.SetActive(false);

        roadCurrent = roadPrefab;
        Spawn(roadCurrent.transform.position);
    }

    private void FixedUpdate()
    {
        UpdateRoad();
    }

    protected virtual void UpdateRoad()
    {
        distance = Vector3.Distance(PlayerController.instance.transform.position, roadCurrent.transform.position);
        if (distance > 116) Spawn();
    }

    protected virtual void Spawn()
    {
        Vector3 pos = roadSpawnPosition.transform.position;
        pos.x = 0;
        pos.y = 0;
        Spawn(pos);
    }

    protected virtual void Spawn(Vector3 position)
    {
        roadCurrent = Instantiate(roadPrefab, position, roadPrefab.transform.rotation);
        roadCurrent.transform.parent = transform;
        roadCurrent.SetActive(true);
    }


}
