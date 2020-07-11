
using Engine.Models.Interfaces;
using Engine.Pipeline;

namespace Engine.Rules.Interfaces
{
    /// <summary>
    ///     Проверяет состояние модели и создает необхадимые стейджи.
    /// </summary>
    public interface IRule
    {
        /// <summary>
        ///     создание нужных стейджей
        /// </summary>
        /// <param name="state">модель</param>
        /// <param name="engine">движок</param>
        void CreatePipeline(IGameStateModel model, AbstractPipelineEngine engine);
    }
}