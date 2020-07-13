

using Module.Levels.Facade;
using Module.VisualElementsModule.Facade;

namespace Engine.Pipeline
{
    public sealed class PipelineEngine : AbstractPipelineEngine
    {
        public ILevelFacade LevelFacade { get; }
        public IVisualElementsFacade VisualElementsFacade { get; }

        public PipelineEngine(ILevelFacade levelFacade, IVisualElementsFacade visualElementsFacade)
        {
            LevelFacade = levelFacade;
            VisualElementsFacade = visualElementsFacade;
        }
    }
}