using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utils;

namespace Saving
{
    public class SaveManager : Singleton<SaveManager>
    {
        public readonly List<Save> Saves = new List<Save>();
        private string _savesFolder;
        
        private void Awake()
        {
            var persistentDataPath = Application.persistentDataPath;
            var folder = persistentDataPath + "/Saves";
            if(!Directory.Exists(folder))
                Directory.CreateDirectory(folder).Create();
            _savesFolder = folder;
            
            foreach (var file in Directory.GetFiles(_savesFolder))
            {
                if (!file.EndsWith(".id")) continue;
                
                var id = new Guid(file.Substring(0, file.LastIndexOf(".id", StringComparison.Ordinal)));
                Saves.Add(new Save(_savesFolder, id));
            }
        }

        public Save NewSave(string saveName)
        {
            var save = new Save(_savesFolder, saveName);
            Saves.Add(save);
            return save;
        }

        public void RemoveSave(Save save)
        {
            Saves.Remove(save);
            save.Remove();
        }
    }
}