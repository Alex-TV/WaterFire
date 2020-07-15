
using System;
using System.Collections.Generic;
using Engine.Models;
using Engine.Models.Interfaces;

namespace GameLoop.Model
{
    public class GameStateModel : IGameStateModel
    {
        private readonly Dictionary<Type, IGameStateEntity> _entitiesContainer = new Dictionary<Type, IGameStateEntity>();

        public T TryGetEntity<T>() where T : class, IGameStateEntity  
        {

            if (typeof(T) == GameStateEntity.Empty.GetType())
            {
                return GameStateEntity.Empty as T;
            }

            if (_entitiesContainer.TryGetValue(typeof(T) , out var result))
            {
                _entitiesContainer.Remove(typeof(T));
                return result as T; 
            }
            return null;
        }

        public bool AddEntity(IGameStateEntity entity) 
        {
            if (entity == GameStateEntity.Empty)
            {
                return true;
            }
            _entitiesContainer[entity.GetType()] = entity;
            return true;
        }
    }
}
