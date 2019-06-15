using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	public void StartGame()
    {

        // Start Game with Stage 1
        SceneManager.LoadScene("Stage1");
	}
	

}
