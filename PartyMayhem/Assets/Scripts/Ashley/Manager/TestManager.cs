using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public PlayerProfile[] profiles;
    public Transform[] startPositions;

    private void Awake()
    {
        profiles = GetComponents<PlayerProfile>();
    }

    private void Start()
    {
    }

    private void Update()
    {

    }

    public void Spawn()
    {
        foreach (PlayerProfile profile in profiles)
        {
            int i = 1;
            Instantiate(profiles[i].Character(), startPositions[i]);
        }
    }
}