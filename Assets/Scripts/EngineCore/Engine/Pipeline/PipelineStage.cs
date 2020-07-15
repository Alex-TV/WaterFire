
using Engine.Models.Interfaces;
using Engine.Rules.Interfaces;

namespace Engine.Pipeline
{
    /// <summary> Базовый класс ядра, для кора. </summary>
    public abstract class PipelineStage<T> : AbstractPipelineStage<T> where T : class, IGameStateEntity
    {
        protected PipelineStage(PipelineEngine engine, T entity, IRule rule) : base(engine, entity, rule)
        {
        }
    }
}