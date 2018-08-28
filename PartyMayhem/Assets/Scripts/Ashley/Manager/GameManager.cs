using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    private void Update()
    {
        
    }

    private void KeepGameManager() //Keeps the GameManager across scenes.
    {
        if (gameManager == null)
            gameManager = this;
        else if (gameManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}