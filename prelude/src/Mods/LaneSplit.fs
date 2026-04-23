namespace Prelude.Mods

open Prelude
open Prelude.Charts

/// Lane Split Mod: Divides the playfield into left and right halves with different scroll directions
/// Left half: Upscroll (normal)
/// Right half: Downscroll (inverted)
/// This mod is VISUAL ONLY - no chart data is modified, ensuring timing fairness
module LaneSplit =

    // This mod doesn't modify the chart at all - it's purely a visual transformation
    // The chart data remains exactly the same so judgement system is unaffected
    let apply (chart: ModdedChartInternal) : ModdedChartInternal * bool =
        // Lane Split is visual-only rendering mod, no chart changes
        chart, false
