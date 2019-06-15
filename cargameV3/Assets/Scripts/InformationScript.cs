using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationScript : MonoBehaviour
{
    public bool isTimer = false;
    public bool isScore = false;
    public bool isLives = false;
    public bool isBomb = false;
    public bool isHighScore = false;
    public bool isNextBullet = false;
    [SerializeField]Text textComponent;
    //method to grab differnt info to pass to UI tabs
    public void GetInfo(int lives)
    {

        if (this.isLives)
        {
            textComponent.text = (" = X " + lives);
        }
    }
    public void GetTimer(string time)
    {
        if(this.isTimer)
        {
            textComponent.text = time;
        }
    }
    public void GetBombs(int bombs)
    {
        textComponent.text = (" = X " + bombs);
    }

    
    // Update is called once per frame
    void Update () 
    {
        
        PlayerScript player = FindObjectOfType<PlayerScript>();
        if (player != null)
        {
            PWeaponScript bullet = player.gameObject.GetComponent<PWeaponScript>();


            if (isLives)
            {
                var hp = player.playerLives;
                GetInfo(hp);
            }
            else if (bullet != null && isNextBullet)
            {
                this.GetComponent<Image>().sprite = bullet.shotSprite;
            }
            else if (isTimer)
            {

                GetTimer(player.timer);
            }
            else if (isBomb)
            {
                GetBombs(player.bombsLeft);
            }
        }

    }
}
