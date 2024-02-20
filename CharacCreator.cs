﻿// Copyright (C) 2024 Rémy Cases
// See LICENSE file for extended copyright information.
// This file is part of the repository CharacCreator from .

using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;
using UndertaleModTool;
using System.Linq;

namespace CharacCreator;
public class CharacCreator : Mod
{
    public override string Author => "zizani";
    public override string Name => "Character Creator";
    public override string Description => "mod_description";
    public override string Version => "0.1.0.0";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        // add some dialog lines
        LocalizationDialog localizationDialog = new(
            new LocalizationSentence(
                id: "_mod_cc_greetings",
                sentence: "Hey, rookie! Need something?"),
            new LocalizationSentence(
                id: "_mod_cc_greeting_free_skill",
                sentence: "Who are you?"),
            new LocalizationSentence(
                id: "_mod_cc_greeting_teach_combat",
                sentence: "Can you teach me about combat?"),
            new LocalizationSentence(
                id: "_mod_cc_greeting_end",
                sentence: "No thanks!"),
            new LocalizationSentence(
                id: "_mod_cc_training",
                sentence: "Sure! I'm quite experienced in the field of Weaponry and Utility skills, to say the least. I also have some knowledge of Sorcery magic. Since I owe Verren a favor, I will teach you a few skills. However, I won't teach you all of them. You can choose two skills from either Weaponry or Sorcery, and two from Utility. Which ones would you like to learn?"),
            new LocalizationSentence(
                id: "_mod_cc_training_weaponry",
                sentence: "Weaponry."),
            new LocalizationSentence(
                id: "_mod_cc_training_sorcery",
                sentence: "Sorcery."),
            new LocalizationSentence(
                id: "_mod_cc_training_utility",
                sentence: "Utility."),
            new LocalizationSentence(
                id: "_mod_cc_training_end",
                sentence: "Maybe next time!"),
            new LocalizationSentence(
                id: "_mod_cc_training_weaponry_choice",
                sentence: "Straight into action, eh?"),
            new LocalizationSentence(
                id: "_mod_cc_training_weaponry_sword",
                sentence: "Learn about Swords"),
            new LocalizationSentence(
                id: "_mod_cc_training_weaponry_axe",
                sentence: "Learn about Axes"),
            new LocalizationSentence(
                id: "_mod_cc_training_weaponry_mace",
                sentence: "Learn about Maces"),
            new LocalizationSentence(
                id: "_mod_cc_training_weaponry_dagger",
                sentence: "Learn about Daggers"),
            new LocalizationSentence(
                id: "_mod_cc_training_weaponry_spear",
                sentence: "Learn about Spears"),
            new LocalizationSentence(
                id: "_mod_cc_training_weaponry_staff",
                sentence: "Learn about Staves"),
            new LocalizationSentence(
                id: "_mod_cc_training_weaponry_thsword",
                sentence: "Learn about Two-Handed Swords"),
            new LocalizationSentence(
                id: "_mod_cc_training_weaponry_thaxe",
                sentence: "Learn about Two-Handed Axes"),
            new LocalizationSentence(
                id: "_mod_cc_training_weaponry_thmace",
                sentence: "Learn about Two-Handed Maces"),
            new LocalizationSentence(
                id: "_mod_cc_training_weaponry_shield",
                sentence: "Learn about Shields"),
            new LocalizationSentence(
                id: "_mod_cc_training_weaponry_bow",
                sentence: "Learn about Bows"),
            new LocalizationSentence(
                id: "_mod_cc_training_sorcery_choice",
                sentence: "Magic, huh?"),
            new LocalizationSentence(
                id: "_mod_cc_training_sorcery_pyro",
                sentence: "Learn about Pyromancy"),
            new LocalizationSentence(
                id: "_mod_cc_training_sorcery_geo",
                sentence: "Learn about Geomancy"),
            new LocalizationSentence(
                id: "_mod_cc_training_sorcery_eletro",
                sentence: "Learn about Electromancy"),
            new LocalizationSentence(
                id: "_mod_cc_training_utility_choice",
                sentence: "Cunning, aren't we?"),
            new LocalizationSentence(
                id: "_mod_cc_training_utility_athletics",
                sentence: "Learn about Athletics"),
            new LocalizationSentence(
                id: "_mod_cc_training_utility_warfare",
                sentence: "Learn about Warfare"),
            new LocalizationSentence(
                id: "_mod_cc_training_utility_dualwelding",
                sentence: "Learn about Dual Wielding"),
            new LocalizationSentence(
                id: "_mod_cc_training_utility_armored",
                sentence: "Learn about Armored Combat"),
            new LocalizationSentence(
                id: "_mod_cc_training_utility_mm",
                sentence: "Learn about Magic Mastery"),
            new LocalizationSentence(
                id: "_mod_cc_greeting_free_skill_lore",
                sentence: "Me? Just a retired mercenary with decades of experience under my belt. Due to an injury, I can no longer work as one. Now spend my days teaching greenhorns how to survive in the wilderness and triumph on the battlefield. I can teach you how to survive in the wilderness as well, but not for free of course!"),
            new LocalizationSentence(
                id: "_mod_cc_greeting_free_skill_accept",
                sentence: "Teach me how to survive in the wilderness. [100 crowns]"),
            new LocalizationSentence(
                id: "_mod_cc_greeting_free_skill_end",
                sentence: "Maybe next time!"),
            new LocalizationSentence(
                id: "_mod_cc_greeting_free_skill_tutorial",
                sentence: "First of all, you will need a knife or any sharp weapon to harvest the pelt and meat. And if you're lucky, you'll also get a hunting trophy. Just remember to cook the meat at a campfire to avoid food poisoning. You could also make a bedroll out of the pelts, some rope, and straw."),
            new LocalizationSentence(
                id: "_mod_cc_greeting_free_skill_thank",
                sentence: "Thank you!")
        );
        localizationDialog.InjectTable(); 

        // change current characters
        Msl.LoadGML("gml_Object_o_dataLoader_Other_10")
            .MatchBelow("scr_classCreate", 5)
            .ReplaceBy(ModFiles, "gml_Object_o_dataLoader_Other_10.gml")
            .Save();

        // add new globals
        Msl.LoadGML("gml_GlobalScript_scr_characterMapInit")
            .MatchFrom("global.timeLevel = 0")
            .InsertBelow(ModFiles, "gml_GlobalScript_scr_characterMapInit.gml")
            .Save();

        // utility functions
        Msl.AddFunction(ModFiles.GetCode("scr_npc_can_study_branch_trainer.gml"), "scr_npc_can_study_branch_trainer");
        Msl.AddFunction(ModFiles.GetCode("scr_npc_can_study_set_bed.gml"), "scr_npc_can_study_set_bed");
        Msl.AddFunction(ModFiles.GetCode("scr_npc_check_combat.gml"), "scr_npc_check_combat");
        Msl.AddFunction(ModFiles.GetCode("scr_npc_check_level.gml"), "scr_npc_check_level");
        Msl.AddFunction(ModFiles.GetCode("scr_npc_check_token.gml"), "scr_npc_check_token");
        Msl.AddFunction(ModFiles.GetCode("scr_npc_check_utility.gml"), "scr_npc_check_utility");
        Msl.AddFunction(ModFiles.GetCode("scr_skill_branch_study_dialogue_trainer.gml"), "scr_skill_branch_study_dialogue_trainer");
        Msl.AddFunction(ModFiles.GetCode("scr_unlock_set_bed.gml"), "scr_unlock_set_bed");

        // add trainer npc
        UndertaleGameObject npcTrainer = Msl.AddObject(
            name:"o_npc_trainer",
            spriteName:"s_npc_merc_inn_fight",
            parentName:"o_npc_baker",
            isVisible:true,
            isAwake:true,
            collisionShapeFlags:UndertaleModLib.Models.CollisionShapeFlags.Circle
        );
        
        npcTrainer.ApplyEvent(ModFiles,
            new MslEvent("npc_trainer_create_0.gml", UndertaleModLib.Models.EventType.Create, 0)
        );

        // get the tavern room
        UndertaleRoom room = Msl.GetRooms().First(t => t.Name.Content == "r_taverninside1floor");

        // add a target marker
        UndertaleRoom.Layer.LayerInstancesData targetInstances = room.Layers.First(t => t.LayerName.Content == "target").InstancesData;
        UndertaleModLib.Models.UndertaleRoom.GameObject gameObjectTargetTrainer = new()
        {
            InstanceID = DataLoader.data.GeneralInfo.LastObj++,
            X = 368,
            Y = 312,
            CreationCode = Msl.AddCode(ModFiles.GetCode("taverninside_101_create.gml"), "gml_RoomCC_r_taverninside1floor_101_Create"),
            ObjectDefinition = Msl.GetObject("o_NPC_target")
        };
        room.GameObjects.Add(gameObjectTargetTrainer);
        targetInstances.Instances.Add(gameObjectTargetTrainer);

        // save the target id and inject it in the creation code of the trainer
        uint tagertID = gameObjectTargetTrainer.InstanceID;
        string codeTrainer = string.Format(@ModFiles.GetCode("taverninside_100_create.gml"), tagertID);

        // create the trainer
        UndertaleRoom.Layer.LayerInstancesData npcInstances = room.Layers.First(t => t.LayerName.Content == "NPC").InstancesData;
        UndertaleModLib.Models.UndertaleRoom.GameObject gameObjectNpcTrainer = new()
        {
            InstanceID = DataLoader.data.GeneralInfo.LastObj++,
            X = 376,
            Y = 328,
            CreationCode = Msl.AddCode(codeTrainer, "gml_RoomCC_r_taverninside1floor_100_Create"),
            ObjectDefinition = npcTrainer
        };
        room.GameObjects.Add(gameObjectNpcTrainer);
        npcInstances.Instances.Add(gameObjectNpcTrainer);
    }
}