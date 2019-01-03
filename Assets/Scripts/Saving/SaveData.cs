using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.WSA;
using Utils;

namespace Saving
{
    /// <summary>
    /// Save data format:
    /// save.data: folder:
    ///     -> version.map  ; map of part versions (part guid : version)
    ///     -> part.list    ; list of parts
    ///     -> partGuid.data ; serialized part
    ///     -> ...
    /// </summary>
    public class SaveData
    {
        private readonly string _file;
        private readonly PartList _partList;
        private readonly VersionMap _versionMap;
        private readonly Dictionary<Guid, PartWrapper> _parts = new Dictionary<Guid, PartWrapper>();

        /// <summary>
        /// Creates save data from save locations
        /// </summary>
        /// <param name="file">save location</param>
        /// <exception cref="IOException"></exception>
        /// <exception cref="SaveFormatException"></exception>
        internal SaveData(string file)
        {
            _file = file + ".data";
            if (!Directory.Exists(_file))
                Directory.CreateDirectory(_file).Create();
            _partList = new PartList(_file);
            _versionMap = new VersionMap(_file);

            CheckDataConsistency();
        }

        private void CheckDataConsistency()
        {
            var toDelete = new List<Guid>();
            foreach (var id in _partList.Get())
                if (_versionMap.GetVersion(id) == null)
                    toDelete.Add(id);
            foreach (var id in _versionMap.GetIds())
                if (!_partList.Contains(id))
                    toDelete.Add(id);
            foreach (var guid in toDelete)
            {
                _versionMap.Remove(guid);
                _partList.Remove(guid);
            }

            _versionMap.Save();
            _partList.Save();
            //TODO clean unused parts .part files
        }

        /// <summary>
        /// Save meta to file sometime in 1 second period
        /// </summary>
        public void Save()
        {
            Debouncer.Instance.Debounce(this, SaveForce, 1000);
        }

        /// <summary>
        /// Save all structures to file immediately(blocking)
        /// </summary>
        /// <exception cref="IOException"></exception>
        public void SaveForce()
        {
            _versionMap.Save();
            _partList.Save();
            foreach (var part in _parts) part.Value.Save();
        }

        public TPart GetPart<TPart, TSerial>(ISaveDataPartFactory<TPart, TSerial> factory)
            where TPart : ISaveDataPart<TSerial>
        {
            if (_parts.ContainsKey(factory.GetId()))
                return _parts[factory.GetId()].Get<TPart>();
            if (_partList.Contains(factory.GetId()) && PartWrapper.Exists(_file, factory.GetId()))
            {
                var version = _versionMap.GetVersion(factory.GetId());
                if (version != null)
                {
                    var wrapper = PartWrapper.Load(_file, factory, (double) version);
                    if (!Equals(version, factory.GetVersion()))
                    {
                        wrapper.Save();
                        _versionMap.SetVersion(factory.GetId(), factory.GetVersion());
                    }

                    _parts.Add(factory.GetId(), wrapper);
                    return wrapper.Get<TPart>();
                }
            }

            var w = new PartWrapper((ISaveDataPart<object>) factory.Create(), _file, factory.GetId());
            w.Save();
            _partList.Add(factory.GetId());
            _versionMap.SetVersion(factory.GetId(), factory.GetVersion());
            _parts.Add(factory.GetId(), w);
            return w.Get<TPart>();
        }

        internal void Remove()
        {
            Directory.Delete(_file);
        }

        public void Remove<TPart, TSerial>(ISaveDataPartFactory<TPart, TSerial> factory)
            where TPart : ISaveDataPart<TSerial>
        {
            var id = factory.GetId();
            _partList.Remove(id);
            _versionMap.Remove(id);
            if (_parts.ContainsKey(id)) _parts.Remove(id);
            PartWrapper.Delete(_file, id);
        }

        public bool Contains<TPart, TSerial>(ISaveDataPartFactory<TPart, TSerial> factory)
            where TPart : ISaveDataPart<TSerial>
        {
            return _partList.Contains(factory.GetId()) && _versionMap.GetVersion(factory.GetId()) != null &&
                   PartWrapper.Exists(_file, factory.GetId());
        }

        private class PartWrapper
        {
            private readonly ISaveDataPart<object> _part;
            private readonly string _file;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="part"></param>
            /// <param name="file"></param>
            /// <param name="id"></param>
            /// <exception cref="IOException"></exception>
            public PartWrapper(ISaveDataPart<object> part, string file, Guid id)
            {
                _part = part;
                _file = file + "/" + id + ".part";
                File.Create(_file).Close();
            }

            private PartWrapper(ISaveDataPart<object> part, string f)
            {
                _part = part;
                _file = f;
            }

            public static PartWrapper Load<TPart, TSerial>(string file, ISaveDataPartFactory<TPart, TSerial> factory,
                double version) where TPart : ISaveDataPart<TSerial>
            {
                if (Exists(file, factory.GetId()))
                    throw new IOException("File not found");
                var f = file + "/" + factory.GetId() + ".part";
                using (var reader = new StreamReader(f, Encoding.UTF8))
                {
                    var content = reader.ReadToEnd();
                    var o = JsonUtility.FromJson(content, factory.GetSerialType());
                    var saveDataPart = factory.Deserialize((TSerial) o, version);
                    return new PartWrapper((ISaveDataPart<object>) saveDataPart, f);
                }
            }

            public static bool Exists(string file, Guid id)
            {
                return File.Exists(file + "/" + id + ".part");
            }

            public static void Delete(string file, Guid id)
            {
                if (Exists(file, id))
                    File.Delete(file + "/" + id + ".part");
            }

            /// <summary>
            /// Saves data to file immediately
            /// </summary>
            /// <exception cref="IOException"></exception>
            public void Save()
            {
                using (var writer = new StreamWriter(_file, false, Encoding.UTF8))
                {
                    writer.Write(JsonUtility.ToJson(_part.Serialize()));
                    writer.Flush();
                }
            }

            public T Get<T>()
            {
                return (T) _part;
            }
        }

        private class PartList
        {
            private readonly string _file;
            private readonly List<Guid> _parts = new List<Guid>();

            /// <summary>
            /// Creates PartList from containing folder location
            /// </summary>
            /// <param name="file">Containing folder location</param>
            /// <exception cref="IOException"></exception>
            /// <exception cref="SaveFormatException"></exception>
            public PartList(string file)
            {
                _file = file + "/part.list";
                if (File.Exists(_file))
                    using (var reader = new StreamReader(_file, Encoding.UTF8))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                            _parts.Add(ParseGuid(line));
                    }
                else
                    File.Create(_file).Close();
            }

            private static Guid ParseGuid(string line)
            {
                Guid guid;
                try
                {
                    guid = new Guid(line);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e);
                    throw new SaveFormatException("part.list format error: guid parse failed with message '" +
                                                  e.Message +
                                                  "', line: '" + line + "'");
                }
                catch (OverflowException e)
                {
                    Console.WriteLine(e);
                    throw new SaveFormatException("part.list.map format error: guid parse failed with message '" +
                                                  e.Message +
                                                  "', line: '" + line + "'");
                }

                return guid;
            }

            /// <summary>
            /// Immediately saves part.list
            /// </summary>
            /// <exception cref="IOException"></exception>
            public void Save()
            {
                using (var writer = new StreamWriter(_file, false, Encoding.UTF8))
                {
                    foreach (var part in _parts)
                        writer.WriteLine(part);
                    writer.Flush();
                }
            }

            public List<Guid> Get()
            {
                return _parts;
            }

            public void Add(Guid guid)
            {
                _parts.Add(guid);
            }

            public bool Contains(Guid guid)
            {
                return _parts.Contains(guid);
            }

            public void Remove(Guid guid)
            {
                _parts.Remove(guid);
            }
        }

        private class VersionMap
        {
            private readonly string _file;
            private readonly Dictionary<Guid, double> _versions = new Dictionary<Guid, double>();

            /// <summary>
            /// Creates VersionMap from containing folder location
            /// </summary>
            /// <param name="file">Containing folder locations</param>
            /// <exception cref="SaveFormatException"></exception>
            /// <exception cref="IOException"></exception>
            public VersionMap(string file)
            {
                _file = file + "/version.map";
                if (File.Exists(_file))
                    using (var reader = new StreamReader(_file, Encoding.UTF8))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(':');
                            if (parts.Length != 2)
                                throw new SaveFormatException(
                                    "version.map format error: can't have more than 1 ':' separator, line: '" + line +
                                    "'");

                            _versions.Add(ParseGuid(parts[0], line), ParseDouble(parts[1], line));
                        }
                    }
                else
                    File.Create(_file).Close();
            }

            private static Guid ParseGuid(string g, string line)
            {
                Guid guid;
                try
                {
                    guid = new Guid(g);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e);
                    throw new SaveFormatException("version.map format error: guid parse failed with message '" +
                                                  e.Message +
                                                  "', line: '" + line + "'");
                }
                catch (OverflowException e)
                {
                    Console.WriteLine(e);
                    throw new SaveFormatException("version.map format error: guid parse failed with message '" +
                                                  e.Message +
                                                  "', line: '" + line + "'");
                }

                return guid;
            }

            private static double ParseDouble(string d, string line)
            {
                double ret;
                try
                {
                    ret = double.Parse(d);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e);
                    throw new SaveFormatException("version.map format error: double parse failed with message '" +
                                                  e.Message +
                                                  "', line: '" + line + "'");
                }
                catch (OverflowException e)
                {
                    Console.WriteLine(e);
                    throw new SaveFormatException("version.map format error: double parse failed with message '" +
                                                  e.Message +
                                                  "', line: '" + line + "'");
                }

                return ret;
            }

            /// <summary>
            /// Immediately saves version.map
            /// </summary>
            /// <exception cref="IOException"></exception>
            public void Save()
            {
                using (var writer = new StreamWriter(_file, false, Encoding.UTF8))
                {
                    foreach (var version in _versions) writer.WriteLine(version.Key + ":" + version.Value);
                    writer.Flush();
                }
            }

            public Dictionary<Guid, double>.KeyCollection GetIds()
            {
                return _versions.Keys;
            }

            public void Remove(Guid id)
            {
                _versions.Remove(id);
            }

            public void SetVersion(Guid guid, double version)
            {
                _versions[guid] = version;
            }

            public double? GetVersion(Guid guid)
            {
                return _versions[guid];
            }
        }
    }
}