namespace Prelude.Mods

open Prelude
open Prelude.Charts

/// Chaos Mod: Random visual distortions and effects
/// Makes the chart harder to read but doesn't fundamentally change timing
/// Pure entertainment mod
module Chaos =

    // This mod adds random visual noise but doesn't modify core chart data
    // Timing and judgement remain fair despite the visual chaos
    let apply (chart: ModdedChartInternal) : ModdedChartInternal * bool =
        // Chaos is visual-only rendering mod, no chart changes
        chart, false
