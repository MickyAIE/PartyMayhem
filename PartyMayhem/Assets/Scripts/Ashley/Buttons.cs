using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private GameManager manager;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void StartButton()
    {
        SceneManager.LoadScene(manager.minigameToLoad);
    }

    public void BackButton()
    {
        if (PlayerPrefs.GetInt("Mode") == 2 && manager.currentRound == manager.rounds)
            manager.returningToMenus = false;
        else
            manager.returningToMenus = true;

        SceneManager.LoadScene("Menus");
    }
}