﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestButtons : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Game Test");
    }
}