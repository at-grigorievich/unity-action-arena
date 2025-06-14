using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace ATG.Save
{
    public class SerializableRepository: ISerializableRepository, IClearable
    {
        private const string STATE_PATH = "state-path";
        
        private Dictionary<string, string> _serializableState = new();
        
        public void SetData(string key, object data)
        {
            string serializedData = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            
            if (_serializableState.TryAdd(key, serializedData) == false)
            {
                _serializableState[key] = serializedData;
            }
        }

        public bool TryGetData<T>(string key, out T result)
        {
            if(_serializableState.TryGetValue(key, out string data) == true)
            {
                result = JsonConvert.DeserializeObject<T>(data);
                return true;
            }

            result = default;
            return false;
        }

        public void RemoveData(string key)
        {
            if(_serializableState.ContainsKey(key) == false) return;
            _serializableState.Remove(key);
        }

        public bool TryGetAllDataByTag<T>(string tag, out IEnumerable<KeyValuePair<string,T>> result)
        {
            List<KeyValuePair<string, T>> data = new();

            foreach (var e in _serializableState)
            {
                if(e.Key.Contains(tag) == true)
                    data.Add(new KeyValuePair<string, T>(e.Key, JsonConvert.DeserializeObject<T>(e.Value)));
            }

            result = data;
            
            return data.Count > 0;
        }
        

        public void SerializeState()
        {
            string stateJson = JsonConvert.SerializeObject(_serializableState);
            SecurePlayerPrefsAes.Save(STATE_PATH, stateJson);
        }

        public void DeserializeState()
        {
            if(_serializableState.Count != 0) return;
            
            if (SecurePlayerPrefsAes.TryRead(STATE_PATH,out string data) == true)
            {
                _serializableState = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            }
        }

        public void Clear()
        {
            if (PlayerPrefs.HasKey(STATE_PATH) == true)
            {
                PlayerPrefs.DeleteKey(STATE_PATH);
                _serializableState.Clear();
            }
        }
    }
}