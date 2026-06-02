using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using System.Linq;

namespace MySpire2Mod.Scripts.Power;

public class QuadrupleFirstCardPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    //public override string? CustomPackedIconPath => "res://MySpire2Mod/images/powers/Power1.png";
    //public override string? CustomBigIconPath => "res://MySpire2Mod/images/powers/Power1.png";

    // 核心：修改卡牌播放次数
    public override int ModifyCardPlayCount(CardModel card, Creature target, int playCount)
    {
        if (card.Owner.Creature != base.Owner) return playCount;


        int triggeredCount = CombatManager.Instance.History.CardPlaysStarted.Count(
            e => e.Actor == base.Owner && e.CardPlay.IsFirstInSeries && e.HappenedThisTurn(base.CombatState));

        if (triggeredCount > 0) return playCount;

        return playCount + 3;
    }

    public override Task AfterModifyingCardPlayCount(CardModel card)
    {
        base.Flash();
        return Task.CompletedTask;
    }
}