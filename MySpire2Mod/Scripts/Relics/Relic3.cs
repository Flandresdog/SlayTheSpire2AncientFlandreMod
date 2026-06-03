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
[Pool(typeof(SharedRelicPool))]
public class TestRelic3 : CustomRelicModel
{
    // 稀有度
    public override RelicRarity Rarity => RelicRarity.Ancient;

    // 遗物的数值。替换本地化中的{Cards}。
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];
    public override string PackedIconPath => "res://MySpire2Mod/images/Relics/relic1s.png";
    protected override string PackedIconOutlinePath => "res://MySpire2Mod/images/Relics/relic1s.png";
    protected override string BigIconPath => "res://MySpire2Mod/images/Relics/relic1b.png";

    public override async Task AfterObtained()
    {
        var player = base.Owner;
        if (player == null) return;

        // 获取牌组中所有攻击牌
        var attackCards = player.Deck.Cards.Where(card => card.Type == CardType.Attack).ToList();

        if (attackCards.Count == 0)
        {
            Log.Info("没有攻击牌，无法附加附魔");
            return;
        }

        //选择 4 张（如果不足 4 张，就全选）
        int count = Math.Min(4, attackCards.Count);
        var selectedCards = attackCards.Take(count).ToList();

        // 获取附魔模型
        var enchantment = ModelDb.Enchantment<TestEnchantment>();

        // 给选中的卡牌附加附魔
        foreach (var card in selectedCards)
        {
            CardCmd.Enchant<TestEnchantment>(card, 1);
        }

        base.Flash();
        await Task.CompletedTask;

    }

    // 初始遗物的升级可以写这里
    // public override RelicModel? GetUpgradeReplacement() => ModelDb.Relic<Circlet>().ToMutable();
}