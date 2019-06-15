using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    //variable to hold the speed of the enemy or object
    public Vector2 speed = new Vector2(10, 10);

    //public variable to hold the direction the enemy should move
    public Vector2 direction = new Vector2(-1, 0);

    //variable to control the direction of the enemies movement
    private Vector2 movement;

    //variable to hold the collision info of the object 
    private Rigidbody2D rigidbodyComponent;

    /*------------------------------------------*/

    // Update is called once per frame
    void Update()
    {

        //takes the coord values from above and uses them to direct the player sprite
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
    }
    //method that updates at a consistent time vs Update which happens based on hardware
    void FixedUpdate()
    {
        //checks to see if the component has data inside
        if (rigidbodyComponent == null)

        rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.velocity = movement;


    }

}
