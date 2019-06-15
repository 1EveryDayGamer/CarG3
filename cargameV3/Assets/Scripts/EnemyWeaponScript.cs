using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponScript : MonoBehaviour
{
    //a variable that will take take the tranform component information of bullet
    public Transform shotPrefab;

    //a float that will determine how often the bullets will be created when able to
    public float shootingRate = 3f;

    //a variable that will tell when a bullet can be created
    private float shootCooldown;

    // Use this for initialization
    void Start()
    {

        shootCooldown = 0f;

    }

    // Update is called once per frame
    void Update()
    {

        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;

        }

    }
    public void Attack(bool isEnemy)
    {
        //if attack is possible first set cooldown back to the rate 
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            //then create a new shot
            var shotTransform = Instantiate(shotPrefab) as Transform;
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
