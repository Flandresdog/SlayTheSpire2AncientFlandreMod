using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using System.Linq;
namespace MySpire2Mod.Scripts.Relics;
[Pool(typeof(SharedRelicPool))]
public class TestRelic : CustomRelicModel
{
    // 稀有度
    public override RelicRarity Rarity => RelicRarity.Ancient;

    // 遗物的数值。替换本地化中的{Cards}。
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];
    public override string PackedIconPath => "res://MySpire2Mod/images/Relics/relic1s.png";
    protected override string PackedIconOutlinePath => "res://MySpire2Mod/images/Relics/relic1s.png";
    protected override string BigIconPath => "res://MySpire2Mod/images/Relics/relic1b.png";

    //111111
    public override async Task AfterObtained()
    {
        var player = base.Owner;
        if (player == null) return;

        // 先收集要删除的卡
        var cardsToRemove = player.Deck.Cards
            .Where(card => card.Rarity == CardRarity.Basic)
            .ToList();  // 注意 ToList() 会创建一个副本

        // 再遍历副本删除
        foreach (var card in cardsToRemove)
        {
            player.Deck.RemoveInternal(card);
        }

        base.Flash();
        await Task.CompletedTask;

    }

    // 初始遗物的升级可以写这里
    // public override RelicModel? GetUpgradeReplacement() => ModelDb.Relic<Circlet>().ToMutable();
}