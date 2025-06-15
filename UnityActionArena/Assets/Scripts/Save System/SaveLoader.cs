using UnityEngine;

namespace ATG.Save
{
    public abstract class SaveLoader<TService, TData> : ISaveLoader
    {
        private readonly ISerializableRepository _serializableRepository;
        protected readonly TService _dataService;
        
        protected abstract string DATA_KEY { get; }

        protected SaveLoader(ISerializableRepository serializableRepository, TService dataService)
        {
            _serializableRepository = serializableRepository;
            _dataService = dataService;
        }
        
        public void Save()
        {
            TData data = ConvertToData();
            
            _serializableRepository.SetData(DATA_KEY, data);
            
            Debug.Log($"{typeof(TService)} serialized");
        }

        public void Load()
        {
            if(_serializableRepository.TryGetData(DATA_KEY, out TData serializableSet) == true)
            {
                SetupData(serializableSet);
            }
            else
            {
                Debug.LogWarning($"data for {typeof(TService)} not found");
                SetupInitialData();
            }
        }
        
        protected abstract TData ConvertToData();
        protected abstract void SetupData(TData resourcesSet);

        protected abstract void SetupInitialData();
    }
}