namespace Prelude.Mods

open Prelude
open Prelude.Charts

/// Hidden Mod: Notes fade in/out based on scroll position
/// Variants allow controlling when notes become visible
module Hidden =

    // This mod is also visual-only - no chart modification
    // The chart data stays the same, timing is unchanged
    let apply (chart: ModdedChartInternal) : ModdedChartInternal * bool =
        // Hidden is visual-only rendering mod, no chart changes
        chart, false
