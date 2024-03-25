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
        //ResetIsDefault();   
    }

    //private void Start()
    //{
    //    if (isDefault && difficulties.Length > 0)
    //    {
    //        difficulties[0].SetActive(true);
    //    }
    //    else if (!isDefault && difficulties.Length > 0)
    //    {
    //        int randomValue = Random.Range(1, difficulties.Length);
    //        difficulties[randomValue].SetActive(true);
    //    }
    //    ResetIsDefault();
    //}

    private void ResetIsDefault()
    {
        isDefault = false;
    }

    //public void SetIsDefaultRecursively(bool value)
    //{
    //    isDefault = value;
    //    foreach (Transform child in transform)
    //    {
    //        ObstaclesRandom childScript = child.GetComponent<ObstaclesRandom>();
    //        if (childScript != null)
    //        {
    //            childScript.SetIsDefaultRecursively(value);
    //        }
    //    }
    //}
}
