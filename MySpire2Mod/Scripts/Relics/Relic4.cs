using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using System;
using System.Linq;
using Test.Scripts;
namespace MySpire2Mod.Scripts.Relics;

using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;

[Pool(typeof(SharedRelicPool))]
public class TestRelic4 : CustomRelicModel
{
    // 稀有度
    private const int EXTRA_ENERGY = 1;
    private bool _usedThisTurn = false;
    public override RelicRarity Rarity => RelicRarity.Ancient;

    // 遗物的数值。替换本地化中的{Cards}。
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];
    public override string PackedIconPath => "res://MySpire2Mod/images/Relics/relic1s.png";
    protected override string PackedIconOutlinePath => "res://MySpire2Mod/images/Relics/relic1s.png";
    protected override string BigIconPath => "res://MySpire2Mod/images/Relics/relic1b.png";


    // 每回合重置标记
    public override Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side == base.Owner.Creature.Side)
        {
            _usedThisTurn = false;
        }
        return Task.CompletedTask;
    }

    // 打出卡牌后触发
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        // 只处理玩家自己的卡牌
        if (cardPlay.Card.Owner != base.Owner) return;

        // 每回合只触发一次
        if (_usedThisTurn) return;

        _usedThisTurn = true;

        // 播放遗物闪光特效
        base.Flash();
        // 增加 1 点能量
        await PlayerCmd.GainEnergy(EXTRA_ENERGY, base.Owner);

        // 消耗这张卡牌
        await CardCmd.Exhaust(context, cardPlay.Card, false, false);


    }
}
