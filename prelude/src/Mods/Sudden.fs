namespace Prelude.Mods

open Prelude
open Prelude.Charts

/// Sudden Mod: Notes appear suddenly near the judgment line
/// Creates a harder difficulty by giving less time to react
module Sudden =

    // Visual-only mod - chart data is not modified
    // Only affects when notes become visible to the player
    let apply (chart: ModdedChartInternal) : ModdedChartInternal * bool =
        // Sudden is visual-only rendering mod, no chart changes
        chart, false
