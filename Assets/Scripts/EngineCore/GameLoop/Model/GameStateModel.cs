
using System;
using System.Collections.Generic;
using Engine.Models;
using GameLoop.Entitys;

namespace GameLoop.Model
{
    public class GameStateModel : IGameStateModel
    {
        private Dictionary<Type, IGameStateEntity> _entitiesContainer = new Dictionary<Type, IGameStateEntity>();

        public T TryGetEntity<T>() where T : class, IGameStateEntity  
        {
            IGameStateEntity result = null;
            if (_entitiesContainer.TryGetValue(typeof(T) , out result))
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
