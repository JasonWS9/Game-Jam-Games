using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Camera playerCamera;
    public LayerMask presentMask;
    public LayerMask ruinsMask;
    [HideInInspector] public LayerMask currentLayerMask;

    public bool isMaskOn { get; private set; }
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        EquipMask(false);
        currentLayerMask = presentMask;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EquipMask(!isMaskOn);
        }
    }

    void EquipMask(bool puttingMaskOn)
    {
        isMaskOn = puttingMaskOn;
        VolumeEffectManager.instance.SetMaskVignette(isMaskOn);

        // If the mask is on, sees the ruins world, otherwise sees the present
        LayerMask targetMask = puttingMaskOn ? ruinsMask : presentMask;

        // Update the camera's culling mask to only render objects in the target layer
        playerCamera.cullingMask = targetMask;
        currentLayerMask = targetMask;
    }
}
