using System;
using UnityEngine;

// RPG is the main namespace
// for our game
namespace RPG.Example
{
    public class Robot : MonoBehaviour
    {
        private BatteryRegulations includedBattery;

        Robot()
        {
            includedBattery = new Battery(80f);
            includedBattery.checkHealth();
            Charger.chargeBattery(includedBattery);
            includedBattery.checkHealth();
            print(Charger.chargerInUse);
        }
    }

    public class Battery : BatteryRegulations
    {
        public Battery(float newHealth) : base(newHealth) { }

        public override void checkHealth()
        {
            Debug.Log(health);
        }
    }

    static class Charger
    {
        public static bool chargerInUse = false;

        public static void chargeBattery(BatteryRegulations batteryToCharge)
        {
            chargerInUse = true;
            batteryToCharge.health = 100f;
        }
    }

    public abstract class BatteryRegulations
    {
        public float health;

        public BatteryRegulations (float newHealth)
        {
            health = newHealth;
            Debug.Log("New battery created.");
        }

        public abstract void checkHealth();
    }

    
}
