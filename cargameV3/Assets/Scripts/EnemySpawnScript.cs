using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    // a bool to check if a spawn is possible
    private bool hasSpawn;
    //a varaible to switch the movement on and off 
    private MoveScript moveScript;
    // a varibale to turn off the weapon but in our case there isnt a weapon
    private WeaponScript[] weapons;
    //a variable to turn off the collider while not in range on a collision
    //might have to change this later as our enemies will not be able to be hit 
    //by bullets while offscreen which may be problematic
    private Collider2D coliderComponent;
    //grabs the renderer component to turn off while not necessary
    private SpriteRenderer rendererComponent;
    //
    private AudioSource soundEffect;

    //--------------------------------|

    //Upon creation create holders of the above variables
    private void Awake()
    {
        weapons = GetComponentsInChildren<WeaponScript>();
        moveScript = GetComponent<MoveScript>();
        coliderComponent = GetComponent<Collider2D>();
        rendererComponent = GetComponent<SpriteRenderer>();
        soundEffect = GetComponent<AudioSource>();


    }


    private void Start()
    {
        //set all the component options to desired 
        hasSpawn = false;

        coliderComponent.enabled = false;

        moveScript.enabled = true;

        soundEffect.enabled = false;

        foreach( WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }
    }
    // Update is called once per frame
    void Update () 
    {
        if (hasSpawn == false)
        {
            if (rendererComponent.IsVisibleFrom(Camera.main))
            {
                Spawn();

            }
            
            
        }
        else
        {
            foreach( WeaponScript weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    weapon.Attack(true);
                }
            }
            //causes objects that pass the player to be destroyed once out of camera view

            if (rendererComponent.IsVisibleFrom(Camera.main) == false)
            {
                coliderComponent.enabled = false;

                moveScript.enabled = true;

                soundEffect.enabled = false;
            }

        }
    }
    //is called to notify if creating an enemy is possible
    private void Spawn()
    {
        //sets all component options on
        hasSpawn = true;

        coliderComponent.enabled = true;

        moveScript.enabled = true;

        soundEffect.enabled = true;

        foreach(WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }
    }
}
