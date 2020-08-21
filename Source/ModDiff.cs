﻿// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace ModDiff
{
    public class Settings : ModSettings
    {
       
        public bool ignoreSelf = false;
        public bool selfPreservation = true;
        public bool alternativePallete = false;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref ignoreSelf, "ignoreSelf", false);
            Scribe_Values.Look(ref selfPreservation, "selfPreservation", true);
            Scribe_Values.Look(ref alternativePallete, "alternativePallete", false);

            base.ExposeData();
        }
    }

    public class ModDiff : Mod
    {
        public static string packageIdOfMine = null;
        public static Settings settings = null;
        public static bool CassowaryPackaged = true;

        public ModDiff(ModContentPack content) : base(content)
        {
            packageIdOfMine = content.PackageId;
            settings = GetSettings<Settings>();

            Harmony harmony = new Harmony(packageIdOfMine);

            harmony.Patch(AccessTools.Method(typeof(ScribeMetaHeaderUtility), "TryCreateDialogsForVersionMismatchWarnings"),
                prefix: new HarmonyMethod(typeof(HarmonyPatches), "TryCreateDialogsForVersionMismatchWarnings_Prefix"));               

        }

        public override string SettingsCategory()
        {
            return "Mod Diff";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            var options = new Listing_Standard();
            options.maxOneColumn = true;

            options.Begin(inRect);
            options.CheckboxLabeled("Ignore self in modlist matching check", ref settings.ignoreSelf, "What could go wrong?");
            options.CheckboxLabeled("Keep self loaded after modlist change", ref settings.selfPreservation);
            options.CheckboxLabeled("Alternative palette", ref settings.alternativePallete);

            options.End();
        }


    }



}
