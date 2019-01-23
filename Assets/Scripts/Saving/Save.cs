using System;
using System.IO;
using Utils;

namespace Saving
{
    /// <summary>
    /// This class holds all save info in one place
    /// </summary>
    public class Save
    {
        private readonly string _file;
        /// <summary>
        /// Save id
        /// </summary>
        public readonly Guid Id;
        /// <summary>
        /// Save metadata
        /// </summary>
        public readonly SaveMetadata SaveMetadata;
        /// <summary>
        /// Save data
        /// </summary>
        public readonly SaveData SaveData;
        /// <summary>
        /// If this is <code>true</code> - save is invalid and can't be used
        /// </summary>
        public readonly bool Invalid;

        internal Save(string folder, string name)
        {
            Id = Guid.NewGuid();
            _file = folder + "/" + Id;
            File.Create(_file + ".id").Close();
            Invalid = false;
            SaveMetadata = new SaveMetadata(_file, DateTime.Now, name);
            SaveData = new SaveData(_file);
        }

        internal Save(string folder, Guid id)
        {
            Id = id;
            _file = folder + "/" + Id;
            if (!File.Exists(_file + ".id"))
            {
                Invalid = true;
                return;
            }
            try
            {
                SaveMetadata = new SaveMetadata(_file);
                SaveData = new SaveData(_file);
                Invalid = false;
            }
            catch (Exception e)
            {
                ErrorWindow.ShowException(e);
                Invalid = true;
            }
        }
        
        /// <summary>
        /// Save data to file sometime in 1 second period
        /// </summary>
        public void SaveToFile()
        {
            Debouncer.Instance.Debounce(this, SaveToFileForce, 1000);
        }

        /// <summary>
        /// Save data to file immediately(blocking)
        /// </summary>
        /// <exception cref="IOException"></exception>
        public void SaveToFileForce()
        {
            SaveMetadata.SaveForce();
            SaveData.SaveForce();
        }
        
        internal void Remove()
        {
            SaveMetadata.Remove();
            SaveData.Remove();
            File.Delete(_file + ".id");
        }
    }
}