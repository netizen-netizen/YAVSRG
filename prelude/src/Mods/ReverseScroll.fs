namespace Prelude.Mods

open Prelude
open Prelude.Charts

/// Reverse Scroll Mod: Inverts scroll direction visually
/// Combines upscroll and downscroll effects in the same playfield
/// Pure visual modifier - chart data unchanged
module ReverseScroll =

    // This is a visual-only mod - chart data is not modified
    // The visual inversion doesn't affect hit detection or timing
    let apply (chart: ModdedChartInternal) : ModdedChartInternal * bool =
        // ReverseScroll is visual-only rendering mod, no chart changes
        chart, false
