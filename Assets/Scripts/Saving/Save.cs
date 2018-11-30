using System;

namespace Saving
{
    /// <summary>
    /// This class represents save metadata. All changes can be monitored by `PropertyChanged` event. The metadata file is
    /// `save-${save-uuid}.meta`
    /// </summary>
    [Serializable]
    public class Save
    {
        private readonly Guid _id;
        private readonly DateTime _creationDateTime;
        private string _name;
        private DateTime _modificationDateTime;
        [NonSerialized]
        private readonly SaveData _data;
        
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (PropertyChanged != null) PropertyChanged("Name", value);
            }
        }

        public Guid Id
        {
            get { return _id; }
        }

        public SaveData Data
        {
            get { return _data; }
        }

        public DateTime ModificationDateTime
        {
            get { return _modificationDateTime; }
            set
            {
                _modificationDateTime = value;
                if (PropertyChanged != null) PropertyChanged("ModificationDateTime", value);
            }
        }

        public DateTime CreationDateTime
        {
            get { return _creationDateTime; }
        }

        public event PropertyChangedDelegate PropertyChanged;

        public delegate void PropertyChangedDelegate(string name, object value);
    }
}