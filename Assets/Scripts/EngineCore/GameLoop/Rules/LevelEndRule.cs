
using Engine.Pipeline;
using Engine.Rules;
using EngineCore.GameLoop.Helpers;
using GameLoop.Entities;
using View.Helpers;

namespace EngineCore.GameLoop.Rules
{
    public class LevelEndRule : Rule<ElementGridEntity>
    {
        public override bool CheckRule(ElementGridEntity entity, PipelineEngine engine)
        {
            if (entity == null)
            {
                return true;
            }

            foreach (var visualElementModel in entity.Grid)
            {
                if (visualElementModel.Name != GameElementType.Empty)
                {
                    return false;
                }
            }

            engine.UiComponentFacade.OnSendAction(ViewActionType.LevelWin, CustomObject.Empty);
            return true;
        }
    }
}
