using Newtonsoft.Json;

namespace BuildingBlock.Common.InfraStructure.Serialization;
public static class SerializerSettings
{
    public static readonly JsonSerializerSettings Instance = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
    };
}

