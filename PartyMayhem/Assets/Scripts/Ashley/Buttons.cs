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
        SceneManager.LoadScene("Menus");
    }
}