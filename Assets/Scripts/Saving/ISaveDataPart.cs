using System;

namespace Saving
{
    public interface ISaveDataPart<out TSerialType>
    {
        TSerialType Serialize();
    }

    public interface ISaveDataPartFactory<out TPart, in TSerialType> where TPart : ISaveDataPart<TSerialType>
    {
        TPart Deserialize(TSerialType type, double version);

        TPart Create();
        
        Guid GetId();

        double GetVersion();

        Type GetSerialType();
    }
}