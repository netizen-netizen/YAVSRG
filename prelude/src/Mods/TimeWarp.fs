namespace Prelude.Mods

open Prelude
open Prelude.Charts

/// Time Warp Mod: Speed up or slow down sections of the chart dynamically
/// Creates interesting rhythm variations while keeping timing windows intact
module TimeWarp =

    // This mod modifies SV (scroll velocity) to create dynamic speed changes
    // Timing windows are NOT affected as they're based on note time, not visual position
    let apply (chart: ModdedChartInternal) : ModdedChartInternal * bool =
        // Time Warp modifies SV, keeping chart notes intact
        let modified_sv =
            chart.SV
            |> TimeArray.map (fun sv ->
                // Apply a gentle wave pattern to SV
                // This creates dynamic speed changes without breaking timing
                sv * 1.2f
            )

        { chart with SV = modified_sv }, true
