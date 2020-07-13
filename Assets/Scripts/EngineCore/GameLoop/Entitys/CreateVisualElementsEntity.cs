
using System.Collections.Generic;
using Engine.Models.Interfaces;
using EngineCore.GameLoop.Entitys.Models;

namespace EngineCore.GameLoop.Entitys
{
    public class CreateVisualElementsEntity : IGameStateEntity
    {
        public List<CreateVisualElementModel> Elements { get; }

        public CreateVisualElementsEntity(List<CreateVisualElementModel> elements)
        {
            Elements = elements;
        }
    }
}
