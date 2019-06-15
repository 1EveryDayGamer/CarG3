using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class GameOverScript : MonoBehaviour 
{
    //an array of pressable buttons
    private Button[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();

        //remove them from being usable
        HideButtons();

    }
    public void HideButtons()
    {
        foreach(var b in buttons)
        {
            b.gameObject.SetActive(false);
        }
    }
    public void ShowButtons()
    {
        foreach(var b in buttons)
        {
            b.gameObject.SetActive(true);
        }
    }
    public void ExitToMenu()
    {
       SceneManager.LoadScene("Menu");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Stage1");
    }
}
