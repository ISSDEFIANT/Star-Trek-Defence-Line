namespace Model
{
    public interface IDataStore
    {
        T Set<T>(string key, object value);

        T Get<T>(string key);

        bool Contains(string key);

        T Remove<T>(string key);
    }
}