using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utils;

namespace Saving
{
    /// <summary>
    /// This class manages all saves
    /// </summary>
    public class SaveManager : Singleton<SaveManager>
    {
        /// <summary>
        /// Read-only collection with all saves
        /// </summary>
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

        /// <summary>
        /// Create new save
        /// </summary>
        /// <param name="saveName">save human-readable name</param>
        /// <returns>new save instance</returns>
        public Save NewSave(string saveName)
        {
            var save = new Save(_savesFolder, saveName);
            Saves.Add(save);
            return save;
        }

        /// <summary>
        /// Remove save from manager and delete all files and folders linked to it
        /// </summary>
        /// <param name="save">the save</param>
        /// <exception cref="IOException"></exception>
        public void RemoveSave(Save save)
        {
            Saves.Remove(save);
            save.Remove();
        }
    }
}