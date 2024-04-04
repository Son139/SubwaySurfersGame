using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesRandom : MonoBehaviour
{
    public GameObject[] difficulties;

    public bool isDefault;

    private void Start()
    {
        if (!isDefault && difficulties.Length > 0)
        {
            int randomValue = Random.Range(0, difficulties.Length);
            difficulties[randomValue].SetActive(true);
        }
    }

    private void ResetIsDefault()
    {
        isDefault = false;
    }

}
