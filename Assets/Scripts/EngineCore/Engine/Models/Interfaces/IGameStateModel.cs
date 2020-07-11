
namespace Engine.Models.Interfaces
{
    public interface IGameStateModel
    {
        T TryGetEntity<T>() where T : class, IGameStateEntity;
        bool AddEntity(IGameStateEntity entity);
    }
}
