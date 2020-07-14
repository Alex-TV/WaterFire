
using System.Collections.Generic;
using Engine.Models.Interfaces;
using EngineCore.GameLoop.Entitys.Models;
using GameLoop.Entitys;

namespace EngineCore.GameLoop.Entitys
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
