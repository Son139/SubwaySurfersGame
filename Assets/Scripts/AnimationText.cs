using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationText : MonoBehaviour
{
    public TextMeshProUGUI tapToStartText;
    public float animationDuration = 0.5f;
    public Vector3 endScale = new Vector3(1.2f, 1.2f, 1f);
    public Color endColor = Color.red;

    void Start()
    {
        if (tapToStartText != null)
        {
            tapToStartText.transform.DOScale(endScale, animationDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutQuad);

            // Thay đổi màu của văn bản "Tap to Start"
            //tapToStartText.DOColor(endColor, animationDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutQuad);
        }
    }

    void OnDestroy()
    {
        DOTween.Kill(tapToStartText.transform); 
    }
}
