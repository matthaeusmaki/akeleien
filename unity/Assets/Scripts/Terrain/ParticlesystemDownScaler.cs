using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticlesystemDownScaler : MonoBehaviour {


    //private List<float> initialSizes = new List<float>();


    /// <summary> Gibt an ob die Skalierung auch auf die Kinder übertragen werden soll </summary>
    public bool scaleChildren = true;

    /// <summary> Gibt an ob der StartSize-Parameter skaliert werden soll </summary>
    public bool scacleStartSize = false;
    /// <summary> Gibt an ob der StartSpeed-Parameter skaliert werden soll </summary>
    public bool scaleStartSpeed = false;

    /// <summary> Skalierungsfaktor für den Parameter StartSize </summary>
    public float factorStartSize = -2.0f;
    /// <summary> Skalierungsfaktor für den Parameter StartSpeed </summary>
    public float factorStartSpeed = -2.0f;

    void Awake()
    {
        ParticleSystem[] particles = gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
        {
            updateScale(particle);
        }
    }

    public void updateScale(ParticleSystem particle)
    {
        if (scacleStartSize)
        {
            particle.startSize *= factorStartSize;
        }

        if (scaleStartSpeed)
        {
            particle.startSpeed *= factorStartSpeed;
        }

        if (scaleChildren)
        {
            /*
            ParticleSystem[] particles = particle.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem p in particles)
            {
                updateScale(p);
            }
             */
        }
    }

    /*
    void Awake()
    {
        // Save off all the initial scale values at start.
        ParticleSystem[] particles = gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
        {
            particle.
            initialSizes.Add(particle.startSize);
            ParticleSystemRenderer renderer = particle.GetComponent<ParticleSystemRenderer>();
            if (renderer)
            {
                initialSizes.Add(renderer.lengthScale);
                initialSizes.Add(renderer.velocityScale);
            }
        }
    }

    public void UpdateScale()
    {
        // Scale all the particle components based on parent.
        int arrayIndex = 0;
        ParticleSystem[] particles = gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
        {
            particle.startSize = initialSizes[arrayIndex++] * gameObject.transform.localScale.magnitude;
            ParticleSystemRenderer renderer = particle.GetComponent<ParticleSystemRenderer>();
            if (renderer)
            {
                renderer.lengthScale = initialSizes[arrayIndex++] *
                    gameObject.transform.localScale.magnitude;
                renderer.velocityScale = initialSizes[arrayIndex++] *
                    gameObject.transform.localScale.magnitude;
            }
        }
    }
     */
}
