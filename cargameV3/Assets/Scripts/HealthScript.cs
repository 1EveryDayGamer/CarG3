using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour 
{
    //a variable that will denote the amount of dmg the object can take
    public int hp = 1;
    //a variable to check if this object is an enemy or a player
    public bool isEnemy = true;
    //variables to have a check for what type of enemy object
    public bool isCop = false;
    //variables to have a check for what type of enemy object
    public bool isTruck = false;
    //variables to have a check for what type of enemy object
    public bool isTaxi = false;
    //variables to have a check for what type of enemy object
    public bool isMini = false;

    //a variable that will pass the hp to the UI
    public int playerLives;

    //a method that will check if its an enemy and then do dmg to it if so
    public void Damage(int damageCount)
    {
        Debug.Log("Im here1" );
        //deducts the integer passed in as damagecount from the health of the object
        hp -= damageCount;


        //if the object has 0 or less health it will be destroyed 
        if (hp <= 0)
        {
            SpecialEffectsHelper.Instance.Explosion(transform.position);
            Destroy(gameObject);
        }
    }
    public void DestroyShot()
    {
        this.hp -= 1;
        SoundEffectsHelper.Instance.MakeExplosionSound();
        SpecialEffectsHelper.Instance.Explosion(transform.position);
        Destroy(gameObject);

    }
    //method that handles the collision event
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //checks to see if this is a collision is with a shot
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        HealthScript shotHealth = otherCollider.gameObject.GetComponent<HealthScript>();
        HealthScript health = this.gameObject.GetComponent<HealthScript>();
        MoveScript move = otherCollider.gameObject.GetComponent<MoveScript>();
        PlayerScript player = this.gameObject.GetComponent<PlayerScript>();
        //if there is a shot script on the collider then it is a bullet 
        if (shot != null)
        {
            //Debug.Log("ShotFound");

            //if this is a bullet the isEnemyShot will be false and if so and its hitting a
            //a Player isEnemy will be false as the player will have a health script as well
            //if its an enemy shot and its hitting a player or bullet the check will pass
            //Debug.Log("" + shot.isEnemyShot + isEnemy);
            if (shot.isEnemyShot != isEnemy)
            {


                //Damage(shot.dmg);
                //checks to see if player object was created 

                if (player != null)
                {
                    //checks to see if the object ran into a cop car and if so is destroyed
                    if(shotHealth.isCop)
                    {
                        health.hp = 0;
                    }
                    else
                    {
                        Debug.Log("there is a player");
                        health.hp -= 1;
                    }

                    if (health.hp <= 0)
                    {
                        SpecialEffectsHelper.Instance.Explosion(transform.position);
                        Destroy(gameObject);
                    }
                    //checks to see if barrier exists to them limit movement

                    //adjusts player sprite according to hp
                    else if (health.hp == 2)
                    {
                        player.gameObject.GetComponent<SpriteRenderer>().sprite = player.Sprites[0];
                    }
                    else if (health.hp == 1)
                    {
                        player.gameObject.GetComponent<SpriteRenderer>().sprite = player.Sprites[1];
                    }

                    SoundEffectsHelper.Instance.MakeExplosionSound();
                    SpecialEffectsHelper.Instance.Explosion(transform.position);
                    Destroy(shot.gameObject);
                    playerLives = hp;

                }
                if (shot.isEnemyShot == false)
                {
                    if (shot.isBombShot)
                    {
                        health.hp = 0;
                        DestroyShot();
                    }
                    //Debug.Log("Inshot bullet = " + shot);
                   //Debug.Log("" + isMini + shot.isMiniShot);
                    //if a cop is shot with a cop bullet

                    else if (isCop && shot.isCopShot)
                    {
                        DestroyShot();
                    }
                    //if a mini is shot with a mini bullet
                    else if (isMini && shot.isMiniShot)
                    {
                        DestroyShot();
                    }
                    //if a taxi is shot with a taxi bullet
                    else if (isTaxi && shot.isTaxiShot)
                    {
                        DestroyShot();
                    }
                    //if a truck is shot with a truck bullet
                    else if (isTruck && shot.isTruckShot)
                    {
                        DestroyShot(); 
                    }
                    //if the bullet doesnt match to car it hit 
                    else
                    {
                        //variable to find the player object to access the hp of it
                        var playerHp = FindObjectOfType<PlayerScript>();
                        //check to see if a cop was shot by anything but a cop bullet
                        if (isCop != shot.isCopShot)
                        {
                            //if so player instantly dies 
                            //playerHp.playerLives = 0 ;
                            playerHp.gameObject.GetComponent<HealthScript>().hp = 0;
                            SoundEffectsHelper.Instance.MakeExplosionSound();
                            SpecialEffectsHelper.Instance.Explosion(transform.position);
                            Destroy(playerHp.gameObject);
                            playerLives = hp;

                        }
                        else
                        {
                            //player will lose 1 hp
                            //playerHp.playerLives -= 1;
                            playerHp.gameObject.GetComponent<HealthScript>().hp -= 1;
                            playerLives = hp;

                            if (playerHp.gameObject.GetComponent<HealthScript>().hp <= 0)
                            {
                                SoundEffectsHelper.Instance.MakeExplosionSound();
                                SpecialEffectsHelper.Instance.Explosion(playerHp.gameObject.GetComponent<Transform>().position);
                                Destroy(playerHp.gameObject);
                            }
                            playerLives = hp;
                            SoundEffectsHelper.Instance.MakeExplosionSound();
                            SpecialEffectsHelper.Instance.Explosion(transform.position);
                            Destroy(gameObject);
                        }
                    }

                }
                if (!shot.isBombShot)
                {
                    Destroy(shot.gameObject);
                }


                if (shot.isEnemyShot)
                {
                    //once the collision happens and the dmg is done the bullet is destroyed
                   // Destroy(shot.gameObject);

                    if (shotHealth.hp <= 0)
                    {
                        SpecialEffectsHelper.Instance.Explosion(transform.position);
                        SoundEffectsHelper.Instance.MakeExplosionSound();
                        Destroy(shot.gameObject);

                    }

                }


            }
            else if (shot.isEnemyShot && isEnemy)
            {
                //if an enemy car collides with another the second cars speed is adjusted down
                //Debug.Log("ddd");
                move.speed = this.GetComponent<MoveScript>().speed;
            }

        }


        var info = FindObjectOfType<InformationScript>();
        info.GetInfo(playerLives);
    }

}
