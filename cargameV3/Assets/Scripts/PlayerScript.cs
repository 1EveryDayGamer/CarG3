using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //variable to hold the speed of the car
    public Vector2 speed = new Vector2(50, 50);
    //an array of the sprite images the car can become with damage
    public Sprite[] Sprites;
    //holds the set of different rows the player can move to
    public Transform[] RowPrefabs;
    //holds the row which the player is in for movement updates
    public int currentRow;
    //variable to pass to InformartionScript
    public int playerLives;
    //used to pass the introduction of player object to inforscript
    private float startTime;
    //string to pass to info funciton to display time alive
    public string timer;
    //variable to hold how many bombs a player has
    public int bombsLeft;
    // Use this for initialization void Start() 
    public void Start()
    {
        bombsLeft = 3;
        startTime = Time.time;
        currentRow = (int)(Random.Range(1, 4));
        transform.position = RowPrefabs[currentRow - 1].position;

    }
    // Update is called once per frame
    void Update()
    {
        SoundEffectsHelper sound = FindObjectOfType<SoundEffectsHelper>();
        bool inputX = Input.GetButtonUp("Horizontal");
        bool alsoInputX = Input.GetButtonUp("Vertical");
        if (inputX)
        {
            sound.MakeInputSound(0);
            if (currentRow == 1)
            {
                Debug.Log("cant move left");
            }
            else if (currentRow == 2)
            {
                transform.position = RowPrefabs[0].position;
                currentRow = 1;
            }
            else if (currentRow == 3)
            {
                transform.position = RowPrefabs[1].position;
                currentRow = 2;
            }
            else if (currentRow == 4)
            {
                transform.position = RowPrefabs[2].position;
                currentRow = 3;
            }
        }
        else if (alsoInputX)
        {
            sound.MakeInputSound(1);
            if (currentRow == 1)
            {
                transform.position = RowPrefabs[1].position;
                currentRow = 2;
            }
            else if (currentRow == 2)
            {
                transform.position = RowPrefabs[2].position;
                currentRow = 3;
            }
            else if (currentRow == 3)
            {
                transform.position = RowPrefabs[3].position;
                currentRow = 4;
            }
            else if (currentRow == 4)
            {
                Debug.Log("Cantmoveright");
            }
        }
        //variable used to get the health componenet of this object to give to lives
        //InformationScript info = FindObjectOfType<InformationScript>();
        HealthScript health = this.gameObject.GetComponent<HealthScript>();
        playerLives = health.hp;
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timer = (minutes + ":" + seconds);


        //connection to the shooting script--------------------------
        //a bool that will check if shooting is happening
        //bool shoot = Input.GetButtonDown("Fire1");
        bool miniShot = Input.GetButtonDown("Mini");
        bool taxiShot = Input.GetButtonDown("Taxi");
        bool truckShot = Input.GetButtonDown("Truck");
        bool copShot = Input.GetButtonDown("Cop");
        //shoot |= Input.GetButtonDown("Fire2");
        bool bomb = Input.GetButtonDown("Jump");
        int typeOfShot;
        if (miniShot || taxiShot || truckShot || copShot)
        {
            PWeaponScript weapon = GetComponent<PWeaponScript>();
            if(weapon != null)
            {
                if(miniShot)
                {
                    typeOfShot = 0;
                }
                else if(taxiShot)
                {
                    typeOfShot = 1;
                }
                else if(truckShot)
                {
                    typeOfShot = 2;
                }
                else
                {
                    typeOfShot = 3;
                }
                weapon.shotType = typeOfShot;
                weapon.Attack(false);
                weapon.shotLoaded = false;

                SoundEffectsHelper.Instance.MakePlayerShotSound();
                
            }
        }
        if (bomb)
        {
            if (bombsLeft > 0)
            {
                PWeaponScript weapon = GetComponent<PWeaponScript>();
                if (weapon != null)
                {
                    weapon.BombsAway(false);
                    SoundEffectsHelper.Instance.MakeAmbulanceSound();
                    if (bombsLeft >= 1)
                    {
                        bombsLeft -= 1;
                    }

                }
            }
        }

    }

    //updates at a specific rate

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("in player");

        bool damagePlayer = false;

        EnemySpawnScript enemy = collision.gameObject.GetComponent<EnemySpawnScript>();
        if (enemy != null)
        {
            HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
            if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);
            damagePlayer = true;
                    
        }
        if(damagePlayer)
        {
            Debug.Log("in player");
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null) playerHealth.Damage(1);

         
        }
    }
    private void OnDestroy()
    {
        //GameOver Call
        var gameOver = FindObjectOfType<GameOverScript>();
        gameOver.ShowButtons();
    }



            
              
        
        

}
