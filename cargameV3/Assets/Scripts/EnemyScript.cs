using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour 
{
    private WeaponScript weapon;

    private void Awake()
    {
        weapon = GetComponent<WeaponScript>();

    }
   


	// Update is called once per frame
	void Update () 
    {
		
        if(weapon != null && weapon.CanAttack)
        {
            weapon.Attack(true); 
        }
    }
}
