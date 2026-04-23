namespace Prelude.Mods

open Prelude

/// This module defines the list of mods that are available ingame, their display order and application order

module Mods =

    let AVAILABLE_MODS : Map<string, Mod> =
        Map.ofList [

            "mirror",
            { Mod.Default with
                Status = ModStatus.Ranked
                Exclusions = [ "shuffle"; "random"; "column_swap" ]
                Apply = fun _ -> Mirror.apply
                Shorthand = fun _ -> "MR"
            }

            "shuffle",
            { Mod.Default with
                Status = ModStatus.Unranked
                Type = RandomSeed
                Apply = fun s -> Randomise.shuffle (int32 s)
                Exclusions = [ "random"; "mirror"; "column_swap" ]
                Shorthand = fun _ -> "SHF"
            }

            "random",
            { Mod.Default with
                Status = ModStatus.Unranked
                Type = RandomSeed
                Apply = fun s -> Randomise.randomise (int32 s)
                Exclusions = [ "shuffle"; "mirror"; "column_swap" ]
                Shorthand = fun _ -> "RD"
            }

            "nosv",
            { Mod.Default with
                Status = ModStatus.Unranked
                Apply = fun _ -> NoSV.apply
                Shorthand = fun _ -> "NSV"
            }

            "noln",
            { Mod.Default with
                Status = ModStatus.Unranked
                Type = MultipleModes 4L
                Exclusions = []
                Apply =
                    fun state mc ->
                        match state with
                        | 0L -> NoLN.apply mc
                        | 1L -> NoLN.apply_shorter_than 1.0f<beat> mc
                        | 2L -> NoLN.apply_shorter_than 0.5f<beat>  mc
                        | 3L -> NoLN.apply_shorter_than 0.25f<beat> mc
                        | _ -> failwith "impossible"
                Shorthand = function 3L -> "LN-1" | 2L -> "LN-2" | 1L -> "LN-3" | _ -> "NLN"
            }

            "inverse",
            { Mod.Default with
                Status = ModStatus.Unranked
                Type = MultipleModes 3L
                Exclusions = []
                Apply = fun state mc ->
                    match state with
                    | 0L -> Inverse.apply 0.25f<beat> mc
                    | 1L -> Inverse.apply 0.125f<beat> mc
                    | 2L -> Inverse.apply 0.5f<beat> mc
                    | _ -> failwith "impossible"
                Shorthand = function 1L -> "INV+1" | 2L -> "INV-1" | _ -> "INV"
            }

            "more_notes",
            { Mod.Default with
                Status = ModStatus.Unstored
                Type = MultipleModes 2L
                Apply = fun s mc -> if s = 1L then MoreNotes.apply_chordjacks mc else MoreNotes.apply_minijacks mc
                Shorthand = function 1L -> "MNC" | _ -> "MNM"
            }

            "column_swap",
            { Mod.Default with
                Status = ModStatus.Offline
                Type = ColumnSwap
                Exclusions = [ "shuffle"; "random"; "mirror" ]
                Apply = fun s -> ColumnSwap.apply (ColumnSwap.unpack s)
                Shorthand = fun state -> sprintf "[%s]" (ColumnSwap.format (ColumnSwap.unpack state))
            }

            "lane_split",
            { Mod.Default with
                Status = ModStatus.Offline
                Type = MultipleModes 2L
                Exclusions = [ "mirror"; "shuffle"; "random" ]
                Apply = fun _ mc -> LaneSplit.apply mc
                Shorthand = function 1L -> "LS-V" | _ -> "LS-H"
            }

            "hidden",
            { Mod.Default with
                Status = ModStatus.Offline
                Type = MultipleModes 2L
                Apply = fun _ mc -> Hidden.apply mc
                Shorthand = function 1L -> "HID-L" | _ -> "HID"
            }

            "sudden",
            { Mod.Default with
                Status = ModStatus.Offline
                Type = MultipleModes 2L
                Apply = fun _ mc -> Sudden.apply mc
                Shorthand = function 1L -> "SUD-L" | _ -> "SUD"
            }

            "time_warp",
            { Mod.Default with
                Status = ModStatus.Offline
                Type = Stateless
                Apply = fun _ -> TimeWarp.apply
                Shorthand = fun _ -> "TW"
            }

            "reverse_scroll",
            { Mod.Default with
                Status = ModStatus.Offline
                Type = Stateless
                Exclusions = []
                Apply = fun _ -> ReverseScroll.apply
                Shorthand = fun _ -> "REV"
            }

            "chaos",
            { Mod.Default with
                Status = ModStatus.Offline
                Type = Stateless
                Apply = fun _ -> Chaos.apply
                Shorthand = fun _ -> "CHS"
            }
        ]

    let APPLICATION_PRIORITY_ORDER =
        [
            "noln"
            "more_notes"
            "column_swap"
            "mirror"
            "shuffle"
            "random"
            "inverse"
            "nosv"
            "lane_split"
            "hidden"
            "sudden"
            "time_warp"
            "reverse_scroll"
            "chaos"
        ]

    let MENU_DISPLAY_ORDER =
        [
            "mirror"
            "inverse"
            "random"
            "shuffle"
            "column_swap"
            "more_notes"
            "noln"
            "nosv"
            "lane_split"
            "hidden"
            "sudden"
            "time_warp"
            "reverse_scroll"
            "chaos"
        ]

    do
        assert(APPLICATION_PRIORITY_ORDER.Length = AVAILABLE_MODS.Count)
        assert(MENU_DISPLAY_ORDER.Length = AVAILABLE_MODS.Count)

    let name (id: string) (state: int64 option) : string =
        match state with
        | Some i when i > 0 && AVAILABLE_MODS.[id].Type.IsMultipleModes -> Localisation.localise (sprintf "mod.%s.%i" id i)
        | _ -> Localisation.localise (sprintf "mod.%s" id)

    let desc (id: string) (state: int64 option) : string =
        match state with
        | Some i when i > 0 && AVAILABLE_MODS.[id].Type.IsMultipleModes -> Localisation.localise (sprintf "mod.%s.%i.desc" id i)
        | _ -> Localisation.localise (sprintf "mod.%s.desc" id)