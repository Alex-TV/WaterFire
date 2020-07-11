
namespace Engine.Models
{
    public interface IGameStateModel
    {
        T TryGetEntity<T>() where T : class, IGameStateEntity;
        bool AddEntity(IGameStateEntity entity);
    }
}
