using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PWeaponScript : MonoBehaviour
{
    //a variable that will take take the tranform component information of bullet
    //public Transform shotPrefab;
    public Transform[] shotPrefabs;
    public Transform bombShot;
    public Transform bombSpot;
    public int prefabChoice;
    public Sprite shotSprite;
    public bool shotLoaded;
    //a float that will determine how often the bullets will be created when able to
    public float shootingRate = 0.25f;
    //a variable that will tell when a bullet can be created
    private float shootCooldown;
    public int shotType;
    public Transform shotTransform;
    // Use this for initialization
    void Start()
    {
        prefabChoice = (int)Random.Range(0, (shotPrefabs.Length));
        shotSprite = shotPrefabs[prefabChoice].GetComponent<SpriteRenderer>().sprite;
        shotLoaded = true;
        shootCooldown = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        //loads a shot for nextBullet sprite
        if (!shotLoaded)
        {
            prefabChoice = (int)Random.Range(0, (shotPrefabs.Length));
            shotSprite = shotPrefabs[prefabChoice].GetComponent<SpriteRenderer>().sprite;
            shotLoaded = true;
            
        }
        //reduces cooldown of shot
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;

        }
    }
    public void BombsAway(bool isBomb)
    {
        shotTransform = Instantiate(bombShot) as Transform;
        Vector3 carSpot = new Vector3(bombSpot.position.x, transform.position.y -2.5f, transform.position.z);
        //then assign its position
        shotTransform.position = carSpot;
        //ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();

        /*if (shot != null)
        {
            shot.isEnemyShot = isEnemy;
        }*/
        MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
        if (move != null)
        {
            //will have to be adjusted for the pulpi to shoot towards the player
            move.direction = this.transform.up;
        }

    }
    public void Attack(bool isEnemy)
    {
        //if attack is possible first set cooldown back to the rate 

        if (CanAttack)
        {
            Debug.Log("InCanAtack");
            shootCooldown = shootingRate;
            if(shotType == 0)
            {
                shotTransform = Instantiate(shotPrefabs[0]) as Transform;
            }
            if (shotType == 1)
            {
                shotTransform = Instantiate(shotPrefabs[1]) as Transform;
            }
            if (shotType == 2)
            {
                shotTransform = Instantiate(shotPrefabs[2]) as Transform;
            }
            if (shotType == 3)
            {
                shotTransform = Instantiate(shotPrefabs[3]) as Transform;
            }
            //then create a new shot
            //shotTransform = Instantiate(shotPrefabs[prefabChoice]) as Transform;
            //then assign its position
            shotTransform.position = transform.position;

            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();

            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                //will have to be adjusted for the pulpi to shoot towards the player
                move.direction = this.transform.up;
            }

        }

    }
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}

