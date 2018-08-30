using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public int playerNumber = 1;

    public Image currentSprite;
    public Sprite[] doggos;
    public Sprite[] shibes;
    public int characterIdx;
    public int flavourIdx;

    public float buttonDelay = 0.3f;
    public float buttonTimer = 0;

    private void Update()
    {
        buttonTimer += Time.deltaTime;

        if (Input.GetAxis("P" + playerNumber + " Vertical") > 0 && buttonTimer > buttonDelay)
        {
            buttonTimer = 0;

            if (flavourIdx > 0)
                flavourIdx--;
            else
                flavourIdx = doggos.Length - 1;
        }

        if (Input.GetAxis("P" + playerNumber + " Vertical") < 0 && buttonTimer > buttonDelay)
        {
            buttonTimer = 0;

            if (flavourIdx < doggos.Length - 1)
                flavourIdx++;
            else
                flavourIdx = 0;
        }

        if ((Input.GetAxis("P" + playerNumber + " Horizontal") > 0 || Input.GetAxis("P" + playerNumber + " Horizontal") < 0) && buttonTimer > buttonDelay)
        {
            buttonTimer = 0;

            if (characterIdx == 0)
                characterIdx = 1;
            else
                characterIdx = 0;
        }

        if (characterIdx == 0)
            currentSprite.sprite = doggos[flavourIdx];
        else if (characterIdx == 1)
            currentSprite.sprite = shibes[flavourIdx];
    }
}