using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ZooWorld.Core.Configs
{
    [CreateAssetMenu(menuName = "ZooWorld/AnimalCatalog")]
    public class AnimalCatalog : ScriptableObject
    {
        [SerializeField] private List<AnimalDefinition> _animalDefinitions = new();
    
        private Dictionary<string, AnimalDefinition> _definitionDictionary;
    
        public void Initialize()
        {
            _definitionDictionary = _animalDefinitions.ToDictionary(x => x.Id, x => x);
        }
    
        public AnimalDefinition GetDefinition(string id)
        {
            if (_definitionDictionary == null) 
                Initialize();
        
            if (_definitionDictionary.TryGetValue(id, out var definition))
            {
                return definition;
            }
        
            throw new Exception($"Definition for animal id '{id}' not found!");
        }

        public string GetRandomAnimalIdByWeight()
        {
            if (_animalDefinitions.Count == 0)
                throw new Exception("Animal registry is empty");

            float totalWeight = 0f;
            foreach (var entry in _animalDefinitions)
            {
                totalWeight += Mathf.Max(0.0001f, entry.SpawnWeight);
            }

            float roll = UnityEngine.Random.value * totalWeight;
            foreach (var entry in _animalDefinitions)
            {
                roll -= Mathf.Max(0.0001f, entry.SpawnWeight);
                if (roll <= 0f)
                    return entry.Id;
            }

            return _animalDefinitions[^1].Id;
        }
    }
}
