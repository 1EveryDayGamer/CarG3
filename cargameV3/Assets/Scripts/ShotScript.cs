using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour 
{
    //the amount of dmg the bullet will do once it collides with an object
    public int dmg = 1;
    //a variable allowing the 2 types of shot to be detected on collison
    public bool isEnemyShot = false;
    public bool isTaxiShot = false;
    public bool isTruckShot = false;
    public bool isCopShot = false;
    public bool isMiniShot = false;
    public bool isBombShot = false;

    // Use this for initialization
    void Start () 
    {

        //upon creation gives the object a max duration of 20 secs without making a collison
        Destroy(gameObject, 32);

	}
    private void Update()
    {


    }


}
