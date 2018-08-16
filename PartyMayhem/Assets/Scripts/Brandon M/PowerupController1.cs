using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class PowerupController : MonoBehaviour {

    public GameObject powerupPrefab;

    public List<speedboostcode> powerups;

    public Dictionary<speedboostcode, float> activePowerups = new Dictionary<speedboostcode, float>();

    private List<speedboostcode> keys = new List<speedboostcode>();

	
	// Update is called once per frame
	void Update () {
        HandleActivePowerups();

    }

    public void HandleActivePowerups()
    {
        bool changed = false;

        if (activePowerups.Count > 0)
        {
            foreach(speedboostcode powerup in keys)
            {
                if (activePowerups[powerup] > 0)
                {
                    activePowerups[powerup] -= Time.deltaTime;
                }
                else
                {
                    changed = true;

                    activePowerups.Remove(powerup);

                    powerup.End();
                }
            }
        }

        if (changed)
        {
            keys = new List<speedboostcode>(activePowerups.Keys);
        }
    }

    public void ActivatePowerup(speedboostcode powerup)
    {
        if (!activePowerups.ContainsKey(powerup))
        {
            powerup.Start();
            activePowerups.Add(powerup, powerup.duration);
        }
        else
        {
            activePowerups[powerup] += powerup.duration;
        }

        keys = new List<speedboostcode>(activePowerups.Keys);
    }

    public GameObject SpawnPowerup(speedboostcode powerup, Vector2 position)
    {
        GameObject powerupGameObject = Instantiate(powerupPrefab);

        var powerupBehavior = powerupGameObject.GetComponent<PowerUpBehavior>();

        powerupBehavior.controller = this;

        powerupBehavior.SetPowerup(powerup);

        powerupGameObject.transform.position = position;

        return powerupGameObject;
    }
}*/
