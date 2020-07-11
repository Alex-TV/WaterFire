

using Engine.Models;
using Engine.Rules;

namespace Engine.Pipeline
{
    /// <summary> Базовый класс ядра, для кора. </summary>
    public abstract class PipelineStage<T> : AbstractPipelineStage<T> where T : class, IGameStateEntity
    {
        protected PipelineStage(PipelineEngine engine, T entity, Rule<T> rule) : base(engine, entity, rule)
        {
        }
    }
}