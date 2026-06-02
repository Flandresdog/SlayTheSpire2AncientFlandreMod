using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using MySpire2Mod.Scripts.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Rooms;
namespace MySpire2Mod.Scripts.Relics;
using MegaCrit.Sts2.Core.Models;  // ModelDb
using MegaCrit.Sts2.Core.Entities.Cards;
[Pool(typeof(SharedRelicPool))]
public class AddPowerCardRelic : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    public override string PackedIconPath => "res://MySpire2Mod/images/Relics/Relic2b.png";
    protected override string PackedIconOutlinePath => "res://MySpire2Mod/images/Relics/Relic2b.png";
    protected override string BigIconPath => "res://MySpire2Mod/images/Relics/Relic2b.png";



    public override async Task AfterObtained()
    {
        var player = base.Owner;
        if (player == null) return;
        var cardModel = player.RunState.CreateCard<TestCard2>(player);
        await CardPileCmd.Add(cardModel, PileType.Deck);

        base.Flash();
    }
}