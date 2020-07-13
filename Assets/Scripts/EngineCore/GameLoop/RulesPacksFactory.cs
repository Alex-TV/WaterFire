
using Engine.Rules;
using EngineCore.GameLoop.Rules;

namespace GameLoop
{
    /// <summary>Фабрика пакетов правил </summary>
    public sealed class RulesPacksFactory
    {
        public RulesPack StartLevelPack { get; } = new RulesPack(
            new  SeparateRulesPack(new LoadLevelRule()),
            new InitLevelRule()
            );

        /// <summary> Пользователь сдвинул игровой элемент </summary>
        public RulesPack UserMoveElementsPack { get; } = new RulesPack();

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
