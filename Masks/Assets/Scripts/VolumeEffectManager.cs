using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeEffectManager : MonoBehaviour
{
    public static VolumeEffectManager instance;

    private Vignette vignette;

    void Awake()
    {
        instance = this;

        Volume volume = GetComponent<Volume>();

        if (!volume.profile.TryGet(out vignette))
        {
            Debug.LogError("Vignette not found in Volume Profile");
            return;
        }
    }

    public void SetMaskVignette(bool maskOn)
    {
        if (vignette == null) return;

        vignette.active = maskOn;
    }
}