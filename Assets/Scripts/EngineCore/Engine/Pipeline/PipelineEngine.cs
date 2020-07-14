

using Module.Input.Facade;
using Module.Levels.Facade;
using Module.VisualElementsModule.Facade;

namespace Engine.Pipeline
{
    public sealed class PipelineEngine : AbstractPipelineEngine
    {
        public ILevelFacade LevelFacade { get; }
        public IVisualElementsFacade VisualElementsFacade { get; }
        public IInputFacade InputFacade { get; }

        public PipelineEngine(ILevelFacade levelFacade, 
                              IVisualElementsFacade visualElementsFacade, 
                              IInputFacade inputFacade)
        {
            LevelFacade = levelFacade;
            VisualElementsFacade = visualElementsFacade;
            InputFacade = inputFacade;
        }
    }
}