using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowMaterialScript : MonoBehaviour
{
    public Material material;
    
    [Min(0)]
    public float startGlowTime;

    [Min(0)]
    public float stopGlowTime;

    public Color glowColor;

    private float currentTime = 0f;
    private float currentInterpolationFactor = 0f;

    private enum GlowState
    {
        idle, // can be used for fully lit, or fully off
        initializing,
        shuttingOff,
    }

    private GlowState glowState = GlowState.idle;

    private void Update()
    {
        if (glowState == GlowState.initializing)
        {
            currentTime += Time.deltaTime;

            currentInterpolationFactor = currentTime / startGlowTime;

            material.SetColor("_EmissionColor", Color.Lerp(Color.black, glowColor, currentInterpolationFactor));

            if(currentInterpolationFactor >= 1f)
            {
                glowState = GlowState.idle;
                currentInterpolationFactor = 1f;
            }
        }
        else if (glowState == GlowState.shuttingOff)
        {
            currentTime -= Time.deltaTime;

            currentInterpolationFactor = currentTime / stopGlowTime;

            material.SetColor("_EmissionColor", Color.Lerp(Color.black, glowColor, currentInterpolationFactor));

            if (currentInterpolationFactor <= 0f)
            {
                glowState = GlowState.idle;
                currentInterpolationFactor = 0f;
            }
        }
    }

    private void OnMouseOver()
    {
        //prepare to light it up!
        glowState = GlowState.initializing;

        currentTime = startGlowTime * currentInterpolationFactor;
    }

    private void OnMouseExit()
    {
        //prepare to shut it down!
        glowState = GlowState.shuttingOff;

        currentTime = stopGlowTime * currentInterpolationFactor;
    }
}
