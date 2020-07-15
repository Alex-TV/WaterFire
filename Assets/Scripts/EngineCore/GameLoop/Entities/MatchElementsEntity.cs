
using System.Collections.Generic;
using Engine.Models.Interfaces;
using GameLoop.Entities;
using Scripts.Controllers.Helpers;

namespace EngineCore.GameLoop.Entities
{
    public class MatchElementsEntity : IGameStateEntity
    {
        public ElementGridEntity GridEntity { get; }
        public List<FieldCoords> MatchElementsCoords { get; }

        public MatchElementsEntity(ElementGridEntity gridEntity, List<FieldCoords> matchElementsCoords)
        {
            GridEntity = gridEntity;
            MatchElementsCoords = matchElementsCoords;

        }
    }
}
