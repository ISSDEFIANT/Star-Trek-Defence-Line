using System;
using System.Collections.Generic;

namespace Saving
{
    public class SaveData
    {
        public readonly Dictionary<Guid, ISaveDataEntity> Entities = new Dictionary<Guid, ISaveDataEntity>();

        public void Save()
        {
            
        }

        public void SaveForce()
        {
            
        }

        public void Load()
        {
            
        }
    }
}