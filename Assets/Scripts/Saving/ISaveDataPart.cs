using System;

namespace Saving
{
    /// <summary>
    /// SaveData part represents some grouped information to store in save
    /// </summary>
    /// <typeparam name="TSerialType">Serialization object type</typeparam>
    public interface ISaveDataPart<out TSerialType>
    {
        /// <summary>
        /// Serialize the object
        /// </summary>
        /// <returns>This object, serialized into serialization-ready object</returns>
        TSerialType Serialize();
    }

    /// <summary>
    /// Factory creates SaveData parts and manages global part information
    /// </summary>
    /// <typeparam name="TPart">SaveData part type</typeparam>
    /// <typeparam name="TSerialType">SaveData part serialization object type</typeparam>
    public interface ISaveDataPartFactory<out TPart, in TSerialType> where TPart : ISaveDataPart<TSerialType>
    {
        /// <summary>
        /// Deserialize part from deserialized serialization object
        /// </summary>
        /// <param name="type">deserialized serialization object</param>
        /// <param name="version">object's version</param>
        /// <returns></returns>
        TPart Deserialize(TSerialType type, double version);

        /// <summary>
        /// Create empty ready-for-use part
        /// </summary>
        /// <returns>new part</returns>
        TPart Create();
        
        /// <summary>
        /// Return global persistent unique id for this part type
        /// </summary>
        /// <returns>global persistent unique id</returns>
        Guid GetId();

        /// <summary>
        /// Return current part's serialization object version
        /// </summary>
        /// <returns>current part's serialization object version</returns>
        double GetVersion();

        /// <summary>
        /// Return serialization object type
        /// </summary>
        /// <returns>serialization object type</returns>
        Type GetSerialType();
    }
}