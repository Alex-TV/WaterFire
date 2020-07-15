
using Engine.Rules;
using EngineCore.GameLoop.Rules;

namespace GameLoop
{
    /// <summary>Фабрика пакетов правил </summary>
    public sealed class RulesPacksFactory
    {
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


        public RulesPack DropPack { get; } = new RulesPack(
            new SeparateRulesPack(new DropRule()),
            new SeparateRulesPack(new MoveElementRule()),
            new FillMathRule()
            );

        /// <summary> Заполнение матчей элементов </summary>
        public RulesPack FillMathElementsPack { get; } = new RulesPack();
        /// <summary> Удаление элементов которые были в матче </summary>
        public RulesPack DestroyMathElementsPack { get; } = new RulesPack();

        /// <summary> Возврат действий пользователя, если не было матчей </summary>
        public RulesPack RevertMoveElementsPack { get; } = new RulesPack();

        /// <summary> Обновление состояния уровня </summary>
        public RulesPack UpdateLevelState { get; } = new RulesPack();
    }
}
