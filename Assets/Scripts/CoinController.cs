using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.itemCollected);
            PlayerPrefs.SetInt("totalCoins", PlayerPrefs.GetInt("totalCoins", 0) + 1);
            Destroy(gameObject);
        }
    }
}
