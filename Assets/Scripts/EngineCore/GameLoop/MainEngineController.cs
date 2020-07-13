using Controllers.Interfaces;
using Engine.Models.Interfaces;
using Engine.Pipeline;
using GameLoop.Helpers;
using System;
using System.Collections.Generic;

namespace GameLoop
{
    /// <summary> тут происходить связь MCVB и движка игры </summary>
    public class MainEngineController : ICustomUpdatable
    {
        private readonly PipelineEngine _engine;
        private readonly IGameStateModel _model;
        private readonly RulesPacksFactory _rulesPacksFactory;

        private readonly Dictionary<EventTypeEnum, List<Action<RulesPacksFactory, IGameStateModel, PipelineEngine>>> _rulePacks =
                    new Dictionary<EventTypeEnum, List<Action<RulesPacksFactory, IGameStateModel, PipelineEngine>>> {

                            {EventTypeEnum.LevelStart, new List<Action<RulesPacksFactory, IGameStateModel, PipelineEngine>> {
                                (f, m, e) => f.StartLevelPack.Applay(m, e, "Start Level Pack")
                            }},
                            {EventTypeEnum.EventMouseMove,new List<Action<RulesPacksFactory, IGameStateModel, PipelineEngine>>
                            {
                                (f, m, e) => { f.UserMoveElementsPack.Applay(m,e,"Mouse Move"); }
                            }},
                            {EventTypeEnum.EventMouseDown,new List<Action<RulesPacksFactory, IGameStateModel, PipelineEngine>> {
                                    (f, m, e) => { }
                            }},
                            {EventTypeEnum.EventMouseUp,new List<Action<RulesPacksFactory, IGameStateModel, PipelineEngine>> {
                                (f, m, e) => f.FillMathElementsPack.Applay(m,e,"Mouse Up")
                            }},
                            {EventTypeEnum.EventMouseDoubleClick,new List<Action<RulesPacksFactory, IGameStateModel, PipelineEngine>> {
                                    (f, m, e) => { }
                            }}
                    };


        public MainEngineController(IGameStateModel model)
        {
            _model = model;
            _rulesPacksFactory = new RulesPacksFactory();
            _engine = new PipelineEngine();
        }

        public void EngineRequest(EventTypeEnum eventTypeEnum)
        {
            if (_rulePacks.TryGetValue(eventTypeEnum, out var action))
            {
                action.ForEach(a => a.Invoke(_rulesPacksFactory, _model, _engine));
            }
            else
            {
                throw new Exception("Not rule pack for event: " + eventTypeEnum);
            }
        }

        public void CustomUpdate()
        {
        }
    }
}
