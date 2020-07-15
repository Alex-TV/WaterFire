using Controllers.Interfaces;
using Engine.Models.Interfaces;
using Engine.Pipeline;
using GameLoop.Helpers;
using System;
using System.Collections.Generic;
using Module.Input.Facade;
using Module.Input.Facade.Controllers;
using Module.Levels.Facade;
using Module.VisualElementsModule.Facade;
using Scripts.Controllers.Helpers;

namespace GameLoop
{
    /// <summary> тут происходить связь MCVB и движка игры </summary>
    public class MainEngineController : ICustomUpdatable
    {
        private readonly PipelineEngine _engine;
        private readonly IGameStateModel _model;
        private readonly IInputController _inputController;
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
                                (f, m, e) => f.UserMoveElementsPack.Applay(m,e,"Mouse Up")

                            }},
                            {EventTypeEnum.EventMouseDoubleClick,new List<Action<RulesPacksFactory, IGameStateModel, PipelineEngine>> {
                                    (f, m, e) => { }
                            }},
                            { EventTypeEnum.Loop,new List<Action<RulesPacksFactory, IGameStateModel, PipelineEngine>> {
                                (f, m, e) => f.DropPack.Applay(m,e,"Drop"),

                            }}, 

                    };


        public MainEngineController(IGameStateModel model, 
                                    ILevelFacade levelFacade, 
                                    IVisualElementsFacade visualElementsFacade,
                                    IInputController inputController,
                                    IInputFacade inputFacade)
        {
            _model = model;
            _inputController = inputController;
            _rulesPacksFactory = new RulesPacksFactory();
            _engine = new PipelineEngine(levelFacade, visualElementsFacade, inputFacade);
        }


        public void Init()
        {
            _inputController.MouseUp += HandleInputControllerMouseUp;
        }

        private void HandleInputControllerMouseUp(object sender, FieldCoords coords)
        {
            EngineRequest(EventTypeEnum.EventMouseUp);
        }

        private void HandleInputControllerMouseMove(object sender, FieldCoords coords)
        {
            EngineRequest(EventTypeEnum.EventMouseMove);
        }

        private void HandleInputControllerMouseDown(object sender, FieldCoords coords)
        {
            EngineRequest(EventTypeEnum.EventMouseDown);
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
            EngineRequest(EventTypeEnum.Loop);
        }
    }
}
