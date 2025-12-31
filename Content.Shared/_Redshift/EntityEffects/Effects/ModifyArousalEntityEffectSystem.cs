using Content.Shared._Redshift.Sex;
using Content.Shared._Redshift.Sex.Components;
using Content.Shared.EntityEffects;
using Robust.Shared.Prototypes;

// should probably be moved, was adapted from pre-entityeffect-refactor code
namespace Content.Shared._Redshift.EntityEffects.Effects;

public sealed partial class ModifyArousalEntityEffectSystem : EntityEffectSystem<ArousalComponent, ModifyArousal>
{
    [Dependency] private readonly SharedArousalSystem _arousal = default!;

    protected override void Effect(Entity<ArousalComponent> ent, ref EntityEffectEvent<ModifyArousal> args)
    {
        if (args.Effect.MaxThreshold != null && args.Effect.Amount > 0 && ent.Comp.CurrentArousal + args.Effect.Amount >= args.Effect.MaxThreshold)
            return;

        _arousal.ModifyArousal(ent, args.Effect.Amount);
    }
}

public sealed partial class ModifyArousal : EntityEffectBase<ModifyArousal>
{
    [DataField] public float Amount = 10;

    [DataField] public float? MaxThreshold = null;

    public override string? EntityEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => Loc.GetString("reagent-effect-guidebook-modify-arousal", ("chance", Probability), ("amount",  Amount), ("max", MaxThreshold == null? -1: MaxThreshold.Value), ("positive", Amount>0));
}
