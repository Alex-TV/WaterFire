
using Engine.Models.Interfaces;
using GameLoop.Entities;
using Scripts.Controllers.Helpers;

namespace EngineCore.GameLoop.Entities
{
    public class MoveElementEntity  : IGameStateEntity
    {
        public ElementGridEntity GridEntity { get; }
        public FieldCoords StartMoveCoords { get; }
        public FieldCoords EndMoveCoords { get; }

        public MoveElementEntity(ElementGridEntity gridEntity, FieldCoords startMoveCoords, FieldCoords endMoveCoords)
        {
            GridEntity = gridEntity;
            StartMoveCoords = startMoveCoords;
            EndMoveCoords = endMoveCoords;
        }
    }
}
