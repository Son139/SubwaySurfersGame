using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class RoadSpawner : MonoBehaviour
{
    protected GameObject roadPrefab;
    protected GameObject roadSpawnPosition;
    protected float distance = 0;
    protected GameObject roadCurrent;
    protected int instancesCreated = 0;
    protected bool isFirstInstance = false;

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

        // Lấy hai child đầu tiên của roadCurrent
        int childCount = Mathf.Min(2, roadCurrent.transform.childCount);
        Debug.Log("số con là: "+childCount);  
        for (int i = 0; i < childCount; i++)
        {
            Transform child = roadCurrent.transform.GetChild(i);

            // Lấy GameObject Difficulties từ child
            GameObject difficulties = child.Find("Difficulties")?.gameObject;
            if (difficulties != null)
            {
                // Lấy component ObstaclesRandom từ GameObject Difficulties
                ObstaclesRandom obstaclesScript = difficulties.GetComponent<ObstaclesRandom>();
                if (obstaclesScript != null && !isFirstInstance)
                {
                    obstaclesScript.isDefault = true;
                }
            }
        }
        isFirstInstance = true;
    }


}

