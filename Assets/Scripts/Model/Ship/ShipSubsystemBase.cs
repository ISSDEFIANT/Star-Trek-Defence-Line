using System.Collections.Generic;
using Model.Behavior;
using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "Data/Ship Subsystem", fileName = "New ShipSubsystem")]
    public class ShipSubsystemBase : ScriptableObject
    {
        [SerializeField] private new string name;
        [SerializeField] private int health;
        [SerializeField] private List<Behavior.Behavior> behaviors;

        public string Name
        {
            get { return name; }
        }

        public int Health
        {
            get { return health; }
        }

        public List<Behavior.Behavior> Behaviors
        {
            get { return behaviors; }
        }
    }
}