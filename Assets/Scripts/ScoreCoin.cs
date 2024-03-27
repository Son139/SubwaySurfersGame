using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCoin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalCoinsText;
    [SerializeField] TextMeshProUGUI currentScoreText;

    [SerializeField] Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalCoinsText.text = PlayerPrefs.GetInt("totalCoins",0).ToString("0");
        currentScoreText.text = Player.transform.position.z.ToString("0000000");

        if(Player.transform.position.z > PlayerPrefs.GetFloat("highScore", 0f)
        {
            PlayerPrefs.SetFloat("highScore", Player.transform.position.z); 
        }
    }
}
