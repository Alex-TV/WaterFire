
using System.Collections.Generic;
using Engine.Models.Interfaces;
using EngineCore.GameLoop.Entities.Models;
using GameLoop.Entities;

namespace EngineCore.GameLoop.Entities
{
    public class MoveElementEntity  : IGameStateEntity
    {
        public ElementGridEntity GridEntity { get; }
        public List<MoveElementCoordsModel> MoveElementCoords { get; }

        public MoveElementEntity(ElementGridEntity gridEntity, List<MoveElementCoordsModel> moveElementCoords)
        {
            GridEntity = gridEntity;
            MoveElementCoords = moveElementCoords;
        }
    }
}
