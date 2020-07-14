
using System.Collections.Generic;
using Engine.Models.Interfaces;
using EngineCore.GameLoop.Entities.Models;
using GameLoop.Entities;

namespace EngineCore.GameLoop.Entities
{
    public class CreateVisualElementsEntity : IGameStateEntity
    {
        public List<CreateVisualElementModel> Elements { get; }
        public ElementGridEntity GridEntity { get; }

        public CreateVisualElementsEntity(List<CreateVisualElementModel> elements, ElementGridEntity gridEntity)
        {
            Elements = elements;
            GridEntity = gridEntity;
        }
    }
}
