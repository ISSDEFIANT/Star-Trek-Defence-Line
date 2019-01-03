using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Saving
{
    /// <summary>
    /// This class holds and manages all metadata for Save
    /// </summary>
    public class SaveMetadata
    {
        private readonly string _file;

        /// <summary>
        /// DateTime of save creation
        /// </summary>
        public readonly DateTime CreationDateTime;

        /// <summary>
        /// DateTime of last save update. Must be set manually
        /// </summary>
        public DateTime UpdateDateTime;

        /// <summary>
        /// Name of the save
        /// </summary>
        public string Name;

        /// <summary>
        /// Constructor for class creation while loading existing save. All data will be loaded from file in this constructor
        /// </summary>
        /// <param name="file">save file path</param>
        /// <exception cref="SaveFormatException">if field is missing in the file/can't parse the file</exception>
        /// <exception cref="IOException">if file doesn't exists/can't read the file</exception>
        internal SaveMetadata(string file)
        {
            _file = file + ".meta";
            using (var reader = new StreamReader(file, Encoding.UTF8))
            {
                var contents = reader.ReadToEnd();
                var representation = JsonUtility.FromJson(contents, typeof(JsonRepresentation)) as JsonRepresentation;
                if (representation == null)
                    throw new SaveFormatException("Can't parse meta file");
                CreationDateTime = representation.creationDateTime; //TODO check nullability/parseability. Tests?
                UpdateDateTime = representation.updateDateTime;
                Name = representation.name;
            }
        }

        /// <summary>
        /// Constructor for class creation for new Save instance
        /// </summary>
        /// <param name="file">save file path</param>
        /// <param name="creationDateTime"></param>
        /// <param name="name"></param>
        /// <exception cref="IOException"></exception>
        internal SaveMetadata(string file, DateTime creationDateTime, string name)
        {
            _file = file + ".meta";
            File.Create(_file).Close();
            CreationDateTime = creationDateTime;
            UpdateDateTime = creationDateTime;
            Name = name;
            Save();
        }

        /// <summary>
        /// Save meta to file sometime in 1 second period
        /// </summary>
        public void Save()
        {
            Debouncer.Instance.Debounce(this, SaveForce, 1000);
        }

        /// <summary>
        /// Save meta to file immediately(blocking)
        /// </summary>
        /// <exception cref="IOException"></exception>
        public void SaveForce()
        {
            var representation = new JsonRepresentation
            {
                name = Name,
                creationDateTime = CreationDateTime,
                updateDateTime = UpdateDateTime
            };
            var json = JsonUtility.ToJson(representation, Debug.isDebugBuild);
            using (var streamWriter = new StreamWriter(_file, false, Encoding.UTF8))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
            }
        }

        internal void Remove()
        {
            File.Delete(_file);
        }

        [Serializable]
        private class JsonRepresentation
        {
            [FormerlySerializedAs("creation")] public DateTime creationDateTime;
            [FormerlySerializedAs("update")] public DateTime updateDateTime;
            public string name;
        }
    }
}