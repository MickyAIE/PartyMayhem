using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject prefab;
    public Transform startPos;
    public Customisation custom;
    public int number;

    private void Awake()
    {
    }

    private void Start()
    {
    }

    private void Update()
    {

    }

    public void Spawn()
    {
        Instantiate(prefab, startPos);
    }
}