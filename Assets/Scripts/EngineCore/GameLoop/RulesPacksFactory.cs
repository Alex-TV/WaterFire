
using Engine.Rules;
using EngineCore.GameLoop.Rules;

namespace GameLoop
{
    /// <summary>Фабрика пакетов правил </summary>
    public sealed class RulesPacksFactory
    {
        /// <summary>Загрузка уровня инициализация игрового поля </summary>
        public RulesPack StartLevelPack { get; } = new RulesPack(
            new SeparateRulesPack(new LoadLevelRule()),
            new SeparateRulesPack(new InitLevelRule()),
            new SeparateRulesPack(new CreateVisualElementsRule()),
            new UpdateLayerVisualElementsRule()
            );

        /// <summary> Пользователь сдвинул игровой элемент </summary>
        public RulesPack UserMoveElementsPack { get; } = new RulesPack(
            new SeparateRulesPack(new SwipeRule()),
            new SeparateRulesPack(new MoveElementRule())
            );

        /// <summary> Игровой цикл, проверка на действие игры</summary>
        public RulesPack LoopPack { get; } = new RulesPack(
            new SeparateRulesPack(new DropRule()),
            new SeparateRulesPack(new MoveElementRule()),
            new SeparateRulesPack(new FillMathRule()),
            new LevelEndRule()
            );
    }
}
