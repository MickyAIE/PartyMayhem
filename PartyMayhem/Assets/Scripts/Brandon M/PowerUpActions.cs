using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpActions : MonoBehaviour {

    [SerializeField]
    private PlayerMovement playerController;

    public void HighSpeedStartAction()
    {
        playerController.speed *= 2;
    }

    public void HighSpeedEndAction()
    {
        playerController.speed = 1000;
    }
}
