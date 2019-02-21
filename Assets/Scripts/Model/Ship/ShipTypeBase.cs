using System.Collections.Generic;
using Model.Ability;
using Model.Behavior;
using UnityEngine;

namespace Model.Ship
{
    //TODO cost
    [CreateAssetMenu(menuName = "Data/Ship Type", fileName = "New ShipType")]
    public sealed class ShipTypeBase : ScriptableObject
    {
        [SerializeField] private string type;
        /// <summary>
        /// Ship mass in tons(10^3 kg)
        /// </summary>
        [SerializeField] private ulong mass;
        [SerializeField] private ulong size;
        [SerializeField] private int crewSize;
        [SerializeField] private int maxSpawnSize;
        [SerializeField] private GameObject model;
        [SerializeField] private uint health;
        [SerializeField] private List<Behavior.Behavior> behaviors;
        [SerializeField] private List<ShipSubsystemBase> subsystems;

        public string Type
        {
            get { return type; }
        }

        public ulong Mass
        {
            get { return mass; }
        }

        public ulong Size
        {
            get { return size; }
        }

        public int CrewSize
        {
            get { return crewSize; }
        }

        public int MaxSpawnSize
        {
            get { return maxSpawnSize; }
        }

        public GameObject Model
        {
            get { return model; }
        }

        public uint Health
        {
            get { return health; }
        }

        public List<Behavior.Behavior> Behaviors
        {
            get { return behaviors; }
        }

        public List<ShipSubsystemBase> Subsystems
        {
            get { return subsystems; }
        }
    }
}