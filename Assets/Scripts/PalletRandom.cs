using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletRandom : MonoBehaviour
{
    public GameObject[] difficults;
    public bool isDefault;
    void Start()
    {
        if(!isDefault)
        {
            int randomValue = Random.Range(0, difficults.Length);
            difficults[randomValue].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
