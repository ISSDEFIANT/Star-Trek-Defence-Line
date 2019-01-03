using System;
using System.IO;
using Utils;

namespace Saving
{
    public class Save
    {
        private readonly string _file;
        public readonly Guid Id;
        public readonly SaveMetadata SaveMetadata;
        public readonly SaveData SaveData;
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
                //TODO handle
                Invalid = true;
            }
        }

        public void SaveToFile()
        {
            Debouncer.Instance.Debounce(this, SaveToFileForce, 1000);
        }

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