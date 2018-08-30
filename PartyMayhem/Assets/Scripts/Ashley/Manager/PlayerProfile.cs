using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public GameManager gameManager;

    public int playerNumber;
    public GameObject player;

    public Animator animator;
    public SpriteRenderer sprite;

    private void Awake()
    {
    }

    public GameObject UpdateCharacterChoice()
    {
        animator = player.GetComponent<Animator>();
        sprite = player.GetComponent<SpriteRenderer>();

        //animator.runtimeAnimatorController = Resources.Load("main/colors/controllercolors/ControllerRED") as RuntimeAnimatorController;
        //sprite.sprite = aaaaa;

        return player;
    }
}