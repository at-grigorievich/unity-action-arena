using System.Collections.Generic;

namespace ATG.Save
{
    public class SaveLoadersService: ISaveService
    {
        private readonly IEnumerable<ISaveLoader> _saveLoaders;
        private readonly ISerializableRepository _serializableRepository;

        public SaveLoadersService(ISerializableRepository repository, IEnumerable<ISaveLoader> saveLoaders)
        {
            _saveLoaders = saveLoaders;
            _serializableRepository = repository;
        }
        
        public void Save()
        {
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.Save();
            }
            
            _serializableRepository.SerializeState();
        }

        public void Load()
        {
            _serializableRepository.DeserializeState();
            
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.Load();
            }
        }
    }
}