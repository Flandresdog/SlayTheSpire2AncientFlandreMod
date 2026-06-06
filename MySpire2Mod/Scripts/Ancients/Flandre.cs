using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using MySpire2Mod.Scripts.Relics;

namespace MySpire2Mod.Scripts;

public class MyAncient : CustomAncientModel
{
    // 选项按钮颜色
    public override Color ButtonColor => new(0.12f, 0.2f, 0.8f, 0.5f);
    // 对话框颜色
    public override Color DialogueColor => new(0.12f, 0.2f, 0.8f);

    // 出现条件：只在第二幕出现
    public override bool IsValidForAct(ActModel act) => act.ActNumber() == 2;

    // 自定义场景路径（Godot 场景文件）
    public override string? CustomScenePath => "res://MySpire2Mod/scenes/AnFlandre.tscn";

    // 地图图标
    public override string? CustomMapIconPath => "res://MySpire2Mod/svgs/flandrehicon.svg";
    public override string? CustomMapIconOutlinePath => "res://MySpire2Mod/svgs/flandrehicon.svg";

    // 历史记录图标
    public override string? CustomRunHistoryIconPath => "res://MySpire2Mod/svgs/flandrehicon.svg";
    public override string? CustomRunHistoryIconOutlinePath => "res://MySpire2Mod/svgs/flandrehicon.svg";

    // 奖励选项池（给什么遗物）
    protected override OptionPools MakeOptionPools => new OptionPools(
        MakePool(
            AncientOption<TestRelic>(),   // 删基础卡
            AncientOption<TestRelic4>()     // 船锚
        ),
        MakePool(
            AncientOption<AddPowerCardRelic>(),   // 拿先古卡
            AncientOption<ArcaneScroll>()  // 奥术卷轴
        ),
        MakePool(
            AncientOption<TestRelic3>(weight: 2),  // 曲奇饼（权重更高）
            AncientOption<WingCharm>()              // 翼符
        )
    );
}