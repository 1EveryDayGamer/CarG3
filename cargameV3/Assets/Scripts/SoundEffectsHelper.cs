using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsHelper : MonoBehaviour 
{
    public static SoundEffectsHelper Instance;

    public AudioClip explosionSound;

    public AudioClip playerShotSound;

    public AudioClip carCrashSound;

    public AudioClip truckSound;

    public AudioClip taxiSound;

    public AudioClip miniSound;

    public AudioClip policeSound;

    public AudioClip ambulanceSound;

    public AudioClip[] InputSounds;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple Instance of SoundEffectHelper!");
        }
        Instance = this;
    }
    public void MakeExplosionSound()
    {
        MakeSound(explosionSound);
    }
    public void MakePlayerShotSound()
    {
        MakeSound(playerShotSound);
    }
    public void MakeCarCrashSound()
    {
        MakeSound(carCrashSound);
    }
    public void MakeTruckSound()
    {
        MakeSound(truckSound);
    }
    public void MakeTaxiSound()
    {
        MakeSound(taxiSound);
    }
    public void MakeMiniSound()
    {
        MakeSound(miniSound);
    }
    public void MakePoliceSound()
    {
        MakeSound(policeSound);
    }
    public void MakeAmbulanceSound()
    {
        MakeSound(ambulanceSound);
    }
    public void MakeInputSound(int x)
    {

        MakeSound(InputSounds[x]);
    }

    private void MakeSound(AudioClip originalClip)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
   	 
}
