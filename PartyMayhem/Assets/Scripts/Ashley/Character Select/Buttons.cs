using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private GameManager gameManager;
    private CharSelectManager manager;
    private Button startButton;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        manager = GameObject.FindGameObjectWithTag("MinigameManager").GetComponent<CharSelectManager>();
        startButton = GetComponent<Button>();
    }

    private void Update()
    {
        if (manager.CanStartGame())
            startButton.interactable = true;
        else
            startButton.interactable = false;
    }

    public void StartButton()
    {
        SceneManager.LoadScene(gameManager.minigameToLoad);
    }
}