using UnityEngine;

public class UIParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Canvas parentCanvas;

    void Start()
    {
        SetupUIParticles();
    }

    public void SetupUIParticles()
    {
        var renderer = particles.GetComponent<ParticleSystemRenderer>();
        renderer.sortingLayerName = "UI"; 
        renderer.sortingOrder = 1000;
        Material uiMaterial = Resources.Load<Material>("UI/Default");
        if (uiMaterial != null)
        {
            renderer.material = uiMaterial;
        }
    }

    public void PlayEffect()
    {
        if (particles != null)
            particles.Play();
    }

    public void StopEffect()
    {
        if (particles != null)
            particles.Stop();
    }
}