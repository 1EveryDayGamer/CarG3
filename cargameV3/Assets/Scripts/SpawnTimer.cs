using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimer : MonoBehaviour 
{

    private WeaponScript weapon;
    public void Awake()
    {
        weapon = GetComponent<WeaponScript>();
        weapon.shootingRate = timerSpeed;

    }
    [SerializeField]
    private float timerSpeed = 2f;


    private float lastSpawn;
	
	// Update is called once per frame
	void Update () 
    {
        float counter = Random.Range(0.9f, 5.0f);
		if(Time.time - lastSpawn >= timerSpeed)
        {
            lastSpawn = Time.time;
            weapon.Attack(true);
            timerSpeed = counter;
            weapon.shootingRate = timerSpeed;

        }
    }
}