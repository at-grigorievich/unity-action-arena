using System.Collections.Generic;

namespace ATG.Save
{
    public interface ISerializableRepository
    {
        void SetData(string key, object data);
        bool TryGetData<T>(string key, out T result);

        void RemoveData(string key);
        
        bool TryGetAllDataByTag<T>(string tag, out IEnumerable<KeyValuePair<string,T>> result);
        
        void SerializeState();
        void DeserializeState();
    }

    public interface IClearable
    {
        void Clear();
    }
}