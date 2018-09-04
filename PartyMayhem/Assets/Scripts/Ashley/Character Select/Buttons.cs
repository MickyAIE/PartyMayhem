using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private CharSelectManager manager;
    private Button startButton;

    private void Awake()
    {
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
        SceneManager.LoadScene("MissileMadness");
    }
}