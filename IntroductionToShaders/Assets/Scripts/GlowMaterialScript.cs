using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowMaterialScript : MonoBehaviour
{
    public Material material;

    public Color glowColor = new Color(1f, 1f, 1f);

    private void OnMouseOver()
    {
        material.SetColor("_EmissionColor", glowColor);
    }

    private void OnMouseExit()
    {
        material.SetColor("_EmissionColor", Color.black);
    }
}
