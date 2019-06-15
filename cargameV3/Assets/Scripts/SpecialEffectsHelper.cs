using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsHelper : MonoBehaviour
{
    public static SpecialEffectsHelper Instance;

    public ParticleSystem smokeEffect;
    public ParticleSystem fireEffect;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple Instances of SpecialEffectsHelper!");

        }
        Instance = this;
    }
    public void Explosion(Vector3 position)
    {
        instantiate(smokeEffect, position);

        instantiate(fireEffect, position);

    }
    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSysytem = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;

        Destroy(newParticleSysytem.gameObject, newParticleSysytem.main.startLifetimeMultiplier);

        return newParticleSysytem;
    }

}
