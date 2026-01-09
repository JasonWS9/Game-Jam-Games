using System;
using UnityEngine;

public class ColorSwap : MonoBehaviour
{

    [Header("Background Colors")]
    [SerializeField] private Color black;
    [SerializeField] private Color darkBlue;
    [SerializeField] private Color red;

    [Header("Object Colors")]
    [SerializeField] private Color white;
    [SerializeField] private Color lightBlue;
    [SerializeField] private Color green;

    [Header("Layer Names")]
    [SerializeField] string objectLayer = "Foreground";
    [SerializeField] string backgroundLayer = "Background";

    // MaterialPropertyBlock allows you to override shader properties for specific renderers
    private MaterialPropertyBlock block;
    void Awake()
    {
        block = new MaterialPropertyBlock();
    }

    void Start()
    {
        SetBackGroundColor(black);
        SetObjectColor(white);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SetObjectColor(lightBlue);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetObjectColor(white);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SetBackGroundColor(darkBlue);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            SetBackGroundColor(black);
        }
    }


    public void SetBackGroundColor(Color backgroundColor)
    {
        ApplyColorToLayer(backgroundLayer, backgroundColor);
    }

    public void SetObjectColor(Color objectColor)
    {
        ApplyColorToLayer(objectLayer, objectColor);
    }

    private void ApplyColorToLayer(String layerName, Color color)
    {
        int layer = LayerMask.NameToLayer(layerName);
        SpriteRenderer[] renderers = FindObjectsByType<SpriteRenderer>(FindObjectsSortMode.None);

        foreach (Renderer r in renderers)
        {
            // If this object is not on the target layer,
            // ignore it and move on to the next one
            if (r.gameObject.layer != layer)
                continue;

            // Read the current MaterialPropertyBlock so we don't overwrite
            r.GetPropertyBlock(block);
            // Override the "_ReplaceColor" property in the shader
            block.SetColor("_ReplaceColor", color);
            // Apply the modified property block back to the renderer
            r.SetPropertyBlock(block);
        }
    }


    private void SetColor(Color color)
    {
        foreach (SpriteRenderer render in GetComponentsInChildren<SpriteRenderer>())
        {
            render.material.SetColor("texture",white);
        }
    }
}
