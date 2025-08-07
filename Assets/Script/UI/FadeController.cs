using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void FadeIn(Action callback)
    {
        image.color = new Color(0, 0, 0, 1);
        image.DOFade(0f, 0.5f).OnComplete(()=> callback?.Invoke());
    }   
    
    public void FadeOut(Action callback)
    {
        image.color = new Color(0, 0, 0, 0);
        image.DOFade(1f, 0.5f).OnComplete(() => callback?.Invoke());
    }

    public void FadeInOut(Action callback)
    {
        image.color = new Color(0, 0, 0, 0);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(image.DOFade(1f, 0.5f)).AppendCallback(() => callback?.Invoke()).AppendInterval(0.3f).Append(image.DOFade(0f, 0.5f));
    }
}
