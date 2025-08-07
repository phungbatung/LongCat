using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CaptureEffect : MonoBehaviour
{
    public RenderTexture captureTarget;
    public RawImage captureImage;
    public Image frameImage;
    public Image pictureFrameImage;

    public void PlayCaptureAnimation(Action callback)
    {
        Capture();
        gameObject.SetActive(true);
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        frameImage.transform.localScale = new Vector2(1.5f, 1.5f);
        frameImage.gameObject.SetActive(true);
        pictureFrameImage.gameObject.SetActive(false);
        Sequence squence = DOTween.Sequence();
        squence.Append(frameImage.transform.DOScale(1, 0.1f)).SetEase(Ease.OutQuad).AppendInterval(0.3f)
        .AppendCallback(()=>
        {
            frameImage.gameObject.SetActive(false);
            pictureFrameImage.gameObject.SetActive(true);
        })
        .Append(transform.DOScale(0.5f, 1f)).SetEase(Ease.InOutCirc)
        .Join(transform.DORotate(new Vector3(0,0,15f), 1f)).SetEase(Ease.InOutCirc)
        .OnComplete(() => callback?.Invoke());
    }
    public void Capture()
    {
        Debug.Log("log1");
        RenderTexture.active = captureTarget;

        int w = Mathf.Min(captureTarget.width, captureTarget.height);
        int h = w;

        int offsetX = (captureTarget.width - w) / 2;
        int offsetY = (captureTarget.height - h) / 2;

        Texture2D tex = new Texture2D(w, h, TextureFormat.RGBA32, false, true);

        tex.ReadPixels(new Rect(offsetX, offsetY, w, h), 0, 0);
        tex.Apply();

        captureImage.texture = tex;
        RenderTexture.active = null;
    }
}
