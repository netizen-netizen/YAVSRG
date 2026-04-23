namespace Interlude.Features.Play

open Percyqaz.Flux.Graphics
open Prelude
open Prelude.Mods

/// Helper module for applying mod-based visual transformations during rendering
module ModRenderingHelpers =

    /// Check if a specific mod is active
    let has_mod (mod_id: string) (mods: ModState) : bool =
        mods.ContainsKey mod_id

    /// Get the state value for a mod
    let get_mod_state (mod_id: string) (mods: ModState) : int64 option =
        mods.TryFind mod_id

    /// Check if lane split is in vertical mode (state = 1)
    let is_lane_split_vertical (mods: ModState) : bool =
        match get_mod_state "lane_split" mods with
        | Some 1L -> true
        | _ -> false

    /// Check if lane split is in horizontal mode (state = 0)
    let is_lane_split_horizontal (mods: ModState) : bool =
        match get_mod_state "lane_split" mods with
        | Some 0L -> true
        | Some _ -> false
        | None -> false

    /// Apply lane-split transform to a rectangle for a given lane
    /// Inverts the vertical position for the right half of the keyboard
    let apply_lane_split_transform (lane: int) (total_lanes: int) (bottom: float32) (rect: Rect) : Rect =
        let is_right_half = lane >= total_lanes / 2
        if is_right_half then
            // Invert Y-axis for right half (downscroll effect)
            {
                rect with
                    Top = bottom - rect.Bottom
                    Bottom = bottom - rect.Top
            }
        else
            // Left half remains normal (upscroll)
            rect

    /// Apply hidden mod transform to opacity
    let apply_hidden_opacity (scroll_pos: float32) (mods: ModState) : float32 =
        match get_mod_state "hidden" mods with
        | Some 0L ->
            // Standard hidden: fade in as notes approach judgment line
            let fade_distance = 200.0f
            if scroll_pos > fade_distance then 0.0f
            elif scroll_pos < 0.0f then 1.0f
            else 1.0f - (scroll_pos / fade_distance)
        | Some 1L ->
            // Late hidden: appear even later
            let fade_distance = 100.0f
            if scroll_pos > fade_distance then 0.0f
            elif scroll_pos < 0.0f then 1.0f
            else 1.0f - (scroll_pos / fade_distance)
        | _ -> 1.0f

    /// Apply sudden mod transform to opacity
    let apply_sudden_opacity (scroll_pos: float32) (mods: ModState) : float32 =
        match get_mod_state "sudden" mods with
        | Some 0L ->
            // Standard sudden: notes appear near judgment line
            let appear_distance = 300.0f
            if scroll_pos > appear_distance then 0.0f
            elif scroll_pos < 0.0f then 1.0f
            else (scroll_pos / appear_distance)
        | Some 1L ->
            // Late sudden: appear even closer
            let appear_distance = 150.0f
            if scroll_pos > appear_distance then 0.0f
            elif scroll_pos < 0.0f then 1.0f
            else (scroll_pos / appear_distance)
        | _ -> 1.0f

    /// Combine hidden and sudden opacity effects
    let apply_opacity_mods (scroll_pos: float32) (mods: ModState) : float32 =
        let hidden_opacity = apply_hidden_opacity scroll_pos mods
        let sudden_opacity = apply_sudden_opacity scroll_pos mods
        hidden_opacity * sudden_opacity

    /// Get tint color modification for chaos mod
    let get_chaos_tint (mods: ModState) : Color =
        if has_mod "chaos" mods then
            // Add slight random tint variation
            let rand = System.Random(int32 (System.DateTime.Now.Ticks % 100L))
            let hue_shift = (rand.NextSingle() - 0.5f) * 0.1f
            Color.FromHSV(hue_shift, 0.9f, 1.0f)
        else
            Color.White
