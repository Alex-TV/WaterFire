
using Engine.Models;
using Engine.Pipeline;

namespace Engine.Rules.Interfaces
{
    /// <summary>     Проверяет состояние модели и создает необхадимые стейджи. </summary>
    public interface IParameterRule<T> : IRule where T : class, IGameStateEntity
    {
        /// <summary>
        ///     Проверка правила
        /// </summary>
        /// <param name="state">модель</param>
        /// <param name="engine">движок</param>
        /// <param name="parametrs">допалнительные параметры из вне</param>
        void CheckRule(T entity, PipelineEngine engine, object parametrs);

        /// <summary>
        ///     создание нужных стейджей
        /// </summary>
        /// <param name="state">модель</param>
        /// <param name="engine">движок</param>
        /// <param name="parametrs">допалнительные параметры из вне</param>
        void CreatePipeline(IGameStateModel model, PipelineEngine engine, object parametrs);
    }
}