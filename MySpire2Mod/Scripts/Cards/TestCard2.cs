using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;
using MySpire2Mod.Scripts.Power;
using System.Threading.Tasks;

namespace MySpire2Mod.Scripts.Cards;

// 注册卡牌。如果要写自定义池看添加人物的开头
[Pool(typeof(ColorlessCardPool))]
public class TestCard2 : CustomCardModel
{
    // 基础耗能
    private const int energyCost = 3;
    // 卡牌类型
    private const CardType type = CardType.Power;
    // 卡牌稀有度
    private const CardRarity rarity = CardRarity.Ancient;
    // 目标类型（AnyEnemy表示任意敌人）
    private const TargetType targetType = TargetType.AnyEnemy;
    // 是否在卡牌图鉴中显示
    private const bool shouldShowInCardLibrary = true;

    // 卡牌的基础属性（例如这里是12点伤害）
    //protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(99, ValueProp.Move)];
    public override string PortraitPath => $"res://MySpire2Mod/images/cards/Card2.png";
    public TestCard2() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    // 打出时的效果逻辑
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var player = base.Owner;

        // 失去当前生命值的 1/4
        int lossAmount = 4;
        player.Creature.LoseHpInternal(lossAmount, 0);

        // 施加能力，Amount = 1 表示每回合只触发 1 次
        object value = await PowerCmd.Apply<QuadrupleFirstCardPower>(new Creature[] { player.Creature }, 1, player.Creature, null);


    }
    public override IEnumerable<CardKeyword> CanonicalKeywords
    {
        get
        {
            return new List<CardKeyword> { CardKeyword.Ethereal };
        }
    }
    // 升级后的效果逻辑
    protected override void OnUpgrade()
    {
        base.RemoveKeyword(CardKeyword.Ethereal);
    }
}