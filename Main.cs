using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Main.Scenes;
using Assets.Scripts.Models;
using Assets.Scripts.Models.GenericBehaviors;
using Assets.Scripts.Models.Powers;
using Assets.Scripts.Models.Profile;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Abilities;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using Assets.Scripts.Models.Towers.Filters;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Models.TowerSets;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Display;
using Assets.Scripts.Unity.Localization;
using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Unity.UI_New.InGame.StoreMenu;
using Assets.Scripts.Unity.UI_New.Upgrade;
using Assets.Scripts.Utils;
using Harmony;
using Il2CppSystem.Collections.Generic;
using MelonLoader;

using UnhollowerBaseLib;
using UnityEngine;
using BTD_Mod_Helper.Extensions;
using Assets.Scripts.Models.Towers.Weapons.Behaviors;
using Assets.Scripts.Models.Towers.Weapons;
using System.Net;
using Assets.Scripts.Unity.UI_New.Popups;
using TMPro;

namespace TowerTemplate
{

    class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            Console.WriteLine("tower template mod loaded. change this to your tower name.");
        }


        static string customTowerImageID;
        static string customTowerName = "customtower";
        static string customTowerDisplayName = "Custom Tower";
        static string customTowerDisplay = "cbac06a37a38a0746a4593de4a9b6296"; //check https://cdn.discordapp.com/attachments/505179324488613909/836926742404923412/Towers.7z for tower models. Requires 7zip or Gzip to open.

        //base tower cost

        static int baseTowerCost = 250;

        //upgrade names

        static string customTowerUpgrade1Top = "Upgrade1Top";
        static string customTowerUpgrade2Top = "Upgrade2Top";
        static string customTowerUpgrade3Top = "Upgrade3Top";
        static string customTowerUpgrade4Top = "Upgrade4Top";
        static string customTowerUpgrade5Top = "Upgrade5Top";

        static string customTowerUpgrade1Mid = "Upgrade1Mid";
        static string customTowerUpgrade2Mid = "Upgrade2Mid";
        static string customTowerUpgrade3Mid = "Upgrade3Mid";
        static string customTowerUpgrade4Mid = "Upgrade4Mid";
        static string customTowerUpgrade5Mid = "Upgrade5Mid";

        static string customTowerUpgrade1Bottom = "Upgrade1Bottom";
        static string customTowerUpgrade2Bottom = "Upgrade2Bottom";
        static string customTowerUpgrade3Bottom = "Upgrade3Bottom";
        static string customTowerUpgrade4Bottom = "Upgrade4Bottom";
        static string customTowerUpgrade5Bottom = "Upgrade5Bottom";

        //upgrade descriptions

        static string customTowerDescription1Top = "Insert description here.";
        static string customTowerDescription2Top = "Insert description here.";
        static string customTowerDescription3Top = "Insert description here.";
        static string customTowerDescription4Top = "Insert description here.";
        static string customTowerDescription5Top = "Insert description here.";

        static string customTowerDescription1Mid = "Insert description here.";
        static string customTowerDescription2Mid = "Insert description here.";
        static string customTowerDescription3Mid = "Insert description here.";
        static string customTowerDescription4Mid = "Insert description here.";
        static string customTowerDescription5Mid = "Insert description here.";

        static string customTowerDescription1Bottom = "Insert description here.";
        static string customTowerDescription2Bottom = "Insert description here.";
        static string customTowerDescription3Bottom = "Insert description here.";
        static string customTowerDescription4Bottom = "Insert description here.";
        static string customTowerDescription5Bottom = "Insert description here.";

        //upgrade costs

        static int customTowerCost1Top = 100;
        static int customTowerCost2Top = 100;
        static int customTowerCost3Top = 100;
        static int customTowerCost4Top = 100;
        static int customTowerCost5Top = 100;

        static int customTowerCost1Mid = 100;
        static int customTowerCost2Mid = 100;
        static int customTowerCost3Mid = 100;
        static int customTowerCost4Mid = 100;
        static int customTowerCost5Mid = 100;

        static int customTowerCost1Bottom = 100;
        static int customTowerCost2Bottom = 100;
        static int customTowerCost3Bottom = 100;
        static int customTowerCost4Bottom = 100;
        static int customTowerCost5Bottom = 100;

        //misc info

        static string customTowerZipLocation = @"Mods/TowerTemplate.zip"; //filepath to get the custom images.
        static string customTowerImages = @"Mods/TowerTemplate/"; //filepath to put the custom images. (i recommend just using the mod name)
        static string customTowerImageLocation = customTowerImages + "TowerTemplate.png"; //filepath of the main tower image. (you could use 000.png or just the tower name)
        static string customTowerTowerSet = "Primary"; //the type
        static int customTowerTowerIndex = 3; //no need to change this




        static string[] upgrades = new string[]
{
                customTowerUpgrade1Top,
                customTowerUpgrade2Top,
                customTowerUpgrade3Top,
                customTowerUpgrade4Top,
                customTowerUpgrade5Top,
                customTowerUpgrade1Mid,
                customTowerUpgrade2Mid,
                customTowerUpgrade3Mid,
                customTowerUpgrade4Mid,
                customTowerUpgrade5Mid,
                customTowerUpgrade1Bottom,
                customTowerUpgrade2Bottom,
                customTowerUpgrade3Bottom,
                customTowerUpgrade4Bottom,
                customTowerUpgrade5Bottom,
        };

        static string[] upgradeDescriptions = new string[]
        {
                customTowerDescription1Top,
                customTowerDescription2Top,
                customTowerDescription3Top,
                customTowerDescription4Top,
                customTowerDescription5Top,
                customTowerDescription1Mid,
                customTowerDescription2Mid,
                customTowerDescription3Mid,
                customTowerDescription4Mid,
                customTowerDescription5Mid,
                customTowerDescription1Bottom,
                customTowerDescription2Bottom,
                customTowerDescription3Bottom,
                customTowerDescription4Bottom,
                customTowerDescription5Bottom,
        };

        static int[] upgradeCosts = new int[]
        {
                customTowerCost1Top,
                customTowerCost2Top,
                customTowerCost3Top,
                customTowerCost4Top,
                customTowerCost5Top,
                customTowerCost1Mid,
                customTowerCost2Mid,
                customTowerCost3Mid,
                customTowerCost4Mid,
                customTowerCost5Mid,
                customTowerCost1Bottom,
                customTowerCost2Bottom,
                customTowerCost3Bottom,
                customTowerCost4Bottom,
                customTowerCost5Bottom,
        };


        
        [HarmonyPatch(typeof(TitleScreen), "Start")]
        public class Awake_Patch
        {

            [HarmonyPostfix]
            public static void Postfix()
            {
                //loads images to the mods folder.
                if (!Directory.Exists(customTowerImages)) 
                {
                    File.WriteAllBytes(customTowerZipLocation, Resource1.TowerTemplate);
                    System.IO.Compression.ZipFile.ExtractToDirectory(customTowerZipLocation, "Mods/");
                    File.Delete(customTowerZipLocation);
                }

                Game.instance.GetSpriteRegister().RegisterSpriteFromImage(customTowerImageLocation, default, out customTowerImageID); //loads the main tower image.

                string[] images = new string[] //image naming convention is tower tiers + "portrait" if the image is a tower portrait.
                {
                    "100.PNG",
                    "200.PNG",
                    "300.PNG",
                    "400.PNG",
                    "500.PNG",
                    "010.PNG",
                    "020.PNG",
                    "030.PNG",
                    "040.PNG",
                    "050.PNG",
                    "001.PNG",
                    "002.PNG",
                    "003.PNG",
                    "004.PNG",
                    "005.PNG",
                    "100portrait.PNG",
                    "200portrait.PNG",
                    "300portrait.PNG",
                    "400portrait.PNG",
                    "500portrait.PNG",
                    "010portrait.PNG",
                    "020portrait.PNG",
                    "030portrait.PNG",
                    "040portrait.PNG",
                    "050portrait.PNG",
                    "001portrait.PNG",
                    "002portrait.PNG",
                    "003portrait.PNG",
                    "004portrait.PNG",
                    "005portrait.PNG"
                };

                string a;
                foreach (string image in images)
                {
                    Game.instance.GetSpriteRegister().RegisterSpriteFromImage(customTowerImages + image, default, out a); //loads all images in the image folder.
                }

                Console.WriteLine("initializing " + customTowerName);
                
                if (!LocalizationManager.instance.textTable.ContainsKey(customTowerName))
                {
                    LocalizationManager.instance.textTable.Add(customTowerName, customTowerDisplayName);
                }


                for (int i = 0; i < upgrades.Length; i++)
                {
                    if (!LocalizationManager.instance.textTable.ContainsKey(upgrades[i] + " Description"))
                    {
                        LocalizationManager.instance.textTable.Add(upgrades[i] + " Description", upgradeDescriptions[i]);
                    }
                }
                
                Il2CppSystem.Collections.Generic.List<UpgradeModel> list = new Il2CppSystem.Collections.Generic.List<UpgradeModel>(); //tower upgrade info list

                for (int i = 0; i < 15; i++) 
                    list.Add(new UpgradeModel(
                        upgrades[i], //upgrade name
                        upgradeCosts[i], //upgrade cost
                        0, //xp cost to unlock upgrade (leave at 0 to auto-unlock)
                        new SpriteReference(customTowerImages + images[i]), //upgrade icon file path
                        Convert.ToInt32(Math.Floor(i/5.0)), //tower path
                        (i % 5) + 1, //tower tier
                        0,
                        "",
                        ""
                        ));

                Game.instance.model.upgrades = Game.instance.model.upgrades.Add(list);

                //add tower upgrades

                int[][] upgradeList = new int[][] //list of all possible tower crosspaths
                {
                    new int[]{0,0,0},
                    new int[]{0,0,1},
                    new int[]{0,0,2},
                    new int[]{0,0,3},
                    new int[]{0,0,4},
                    new int[]{0,0,5},
                    new int[]{0,1,0},
                    new int[]{0,1,1},
                    new int[]{0,1,2},
                    new int[]{0,1,3},
                    new int[]{0,1,4},
                    new int[]{0,1,5},
                    new int[]{0,2,0},
                    new int[]{0,2,1},
                    new int[]{0,2,2},
                    new int[]{0,2,3},
                    new int[]{0,2,4},
                    new int[]{0,2,5},
                    new int[]{0,3,0},
                    new int[]{0,3,1},
                    new int[]{0,3,2},
                    new int[]{0,4,0},
                    new int[]{0,4,1},
                    new int[]{0,4,2},
                    new int[]{0,5,0},
                    new int[]{0,5,1},
                    new int[]{0,5,2},
                    new int[]{1,0,0},
                    new int[]{1,0,1},
                    new int[]{1,0,2},
                    new int[]{1,0,3},
                    new int[]{1,0,4},
                    new int[]{1,0,5},
                    new int[]{1,1,0},
                    new int[]{1,2,0},
                    new int[]{1,3,0},
                    new int[]{1,4,0},
                    new int[]{1,5,0},
                    new int[]{2,0,0},
                    new int[]{2,0,1},
                    new int[]{2,0,2},
                    new int[]{2,0,3},
                    new int[]{2,0,4},
                    new int[]{2,0,5},
                    new int[]{2,1,0},
                    new int[]{2,2,0},
                    new int[]{2,3,0},
                    new int[]{2,4,0},
                    new int[]{2,5,0},
                    new int[]{3,0,0},
                    new int[]{3,0,1},
                    new int[]{3,0,2},
                    new int[]{3,1,0},
                    new int[]{3,2,0},
                    new int[]{4,0,0},
                    new int[]{4,0,1},
                    new int[]{4,0,2},
                    new int[]{4,1,0},
                    new int[]{4,2,0},
                    new int[]{5,0,0},
                    new int[]{5,0,1},
                    new int[]{5,0,2},
                    new int[]{5,1,0},
                    new int[]{5,2,0}
                };

                Il2CppSystem.Collections.Generic.List<TowerModel> list2 = new Il2CppSystem.Collections.Generic.List<TowerModel>(); //tower upgrade code list

                foreach (int[] upgrade in upgradeList)
                {
                    MelonLogger.Msg("[" + upgrade[0] + ", " + upgrade[1] + ", " + upgrade[2] + "]");
                    list2.Add(getTowerModel(Game.instance.model, upgrade[0], upgrade[1], upgrade[2]));

                }

                Game.instance.model.towers = Game.instance.model.towers.Add(list2);

                Il2CppSystem.Collections.Generic.List<TowerDetailsModel> list3 = new Il2CppSystem.Collections.Generic.List<TowerDetailsModel>();

                foreach (TowerDetailsModel item in Game.instance.model.towerSet)
                {
                    list3.Add(item);
                }
                
                Game.instance.model.towerSet = Game.instance.model.towerSet.Add(new ShopTowerDetailsModel(customTowerName, customTowerTowerIndex, 5, 5, 5, -1, 0, null));
                
                bool flag = false;

                foreach (TowerDetailsModel towerDetailsModel in Game.instance.model.towerSet)
                {
                    if (flag)
                    {
                        int towerIndex = towerDetailsModel.towerIndex;
                        towerDetailsModel.towerIndex = towerIndex + 1;
                    }
                    if (towerDetailsModel.towerId.Contains(customTowerName))
                    {
                        flag = true;
                    }
                }
                Console.WriteLine(customTowerName + " initialized");
            }
        }

        public static TowerModel getTowerModel(GameModel gameModel, int path1, int path2, int path3)
        {
            TowerModel towerModel = getT0(gameModel); //the base tower

            //upgrades

            if (path1 >= 1) towerModel = getT1Top(towerModel);
            if (path2 >= 1) towerModel = getT1Mid(towerModel);
            if (path3 >= 1) towerModel = getT1Bottom(towerModel);

            if (path1 >= 2) towerModel = getT2Top(towerModel);
            if (path2 >= 2) towerModel = getT2Mid(towerModel);
            if (path3 >= 2) towerModel = getT2Bottom(towerModel);

            if (path1 >= 3) towerModel = getT3Top(towerModel);
            if (path2 >= 3) towerModel = getT3Mid(towerModel);
            if (path3 >= 3) towerModel = getT3Bottom(towerModel);

            if (path1 >= 4) towerModel = getT4Top(towerModel);
            if (path2 >= 4) towerModel = getT4Mid(towerModel);
            if (path3 >= 4) towerModel = getT4Bottom(towerModel);

            if (path1 >= 5) towerModel = getT5Top(towerModel);
            if (path2 >= 5) towerModel = getT5Mid(towerModel);
            if (path3 >= 5) towerModel = getT5Bottom(towerModel);

            //tower portrait based on upgrades

            if (path1 > path2 && path1 > path3)
                towerModel.portrait = new SpriteReference(customTowerImages + "" + path1 + "00portrait.PNG");

            else if (path2 > path3)
                towerModel.portrait = new SpriteReference(customTowerImages + "0" + path2 + "0portrait.PNG");

            else if (path3 != 0)
                towerModel.portrait = new SpriteReference(customTowerImages + "00" + path3 + "portrait.PNG");

            else
                towerModel.portrait = new SpriteReference(customTowerImageID);

            SetTiersAndUpgrades(ref towerModel, path1, path2, path3);

            return towerModel;
        }



        public static TowerModel getT0(GameModel gameModel) //the base tower
        {
            TowerModel towerModel = gameModel.GetTowerFromId("DartMonkey").Duplicate<TowerModel>(); //monkey to copy
            towerModel.name = customTowerName;
            towerModel.baseId = customTowerName;
            towerModel.icon = new SpriteReference(customTowerImageID);
            towerModel.instaIcon = new SpriteReference(customTowerImageID);
            towerModel.display = customTowerDisplay;
            towerModel.GetBehavior<DisplayModel>().display = customTowerDisplay;
            towerModel.towerSet = customTowerTowerSet;
            towerModel.dontDisplayUpgrades = false;
            towerModel.cost = baseTowerCost;

            towerModel.mods = new Il2CppReferenceArray<Assets.Scripts.Models.Towers.Mods.ApplyModModel>(0);

            var attack = gameModel.GetTowerFromId("MonkeyBuccaneer").Duplicate<TowerModel>().GetBehavior<AttackModel>(); //monkey to copy attack from
            attack.weapons[0].projectile.pierce = 2; //use this to set projectile pierce
            attack.weapons[0].projectile.maxPierce = 99999;
            attack.weapons[0].projectile.CapPierce(99999);
            attack.weapons[0].projectile.GetBehavior<DamageModel>().damage = 1; //use this to set projectile damage 
            attack.weapons[0].projectile.GetBehavior<DamageModel>().CapDamage(9999);
            attack.weapons[0].projectile.GetBehavior<DamageModel>().maxDamage = 9999;
            attack.weapons[0].Rate = 1; //use this to set attack speed
            towerModel.RemoveBehavior<AttackModel>();
            towerModel.AddBehavior(attack);

            return towerModel;

        }




        public static TowerModel getT1Top(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
            attackModel.weapons[0].projectile.pierce++;

            return towerModel;
        }


        public static TowerModel getT2Top(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
            attackModel.weapons[0].projectile.pierce *= 1.5f;

            return towerModel;
        }


        public static TowerModel getT3Top(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
            attackModel.AddWeapon(Game.instance.model.GetTowerFromId("MonkeySub").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0]); //use this to add weapons from other towers
            attackModel.weapons[1].projectile.pierce = 3;

            return towerModel;
        }


        public static TowerModel getT4Top(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
            foreach (var item in attackModel.weapons)
            {
                item.projectile.GetDamageModel().damage += 1;
            }

            return towerModel;
        }


        public static TowerModel getT5Top(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
            foreach (var item in attackModel.weapons)
            {
                item.rate *= 0.1f; 
            }

            return towerModel;
        }


        public static TowerModel getT1Mid(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
            //use this to increase tower range
            attackModel.range *= 1.25f;
            towerModel.range *= 1.25f;
            towerModel.radius *= 1.25f;

            return towerModel;
        }


        public static TowerModel getT2Mid(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
            towerModel.AddBehavior(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
            attackModel.range *= 1.1f;
            towerModel.range *= 1.1f;
            towerModel.radius *= 1.1f;

            return towerModel;
        }


        public static TowerModel getT3Mid(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();

            //use this to add abilities

            AbilityModel abilityModel = Game.instance.model.towers.FirstOrDefault((TowerModel quincy) => quincy.name.Contains("Quincy") && quincy.tier == 3).behaviors.FirstOrDefault((Model ab) => ab.name.Contains("Rapid")).Clone().Cast<AbilityModel>();
            towerModel.behaviors = towerModel.behaviors.Add(abilityModel);

            return towerModel;
        }


        public static TowerModel getT4Mid(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();

            AbilityModel abilityModel = Game.instance.model.towers.FirstOrDefault((TowerModel quincy) => quincy.name.Contains("Quincy") && quincy.tier == 20).behaviors.FirstOrDefault((Model ab) => ab.name.Contains("Rapid")).Clone().Cast<AbilityModel>();
            towerModel.behaviors = towerModel.behaviors.Add(abilityModel);

            AbilityModel abilityModel2 = Game.instance.model.towers.FirstOrDefault((TowerModel quincy) => quincy.name.Contains("Quincy") && quincy.tier == 10).behaviors.FirstOrDefault((Model ab) => ab.name.Contains("Storm")).Clone().Cast<AbilityModel>();
            towerModel.behaviors = towerModel.behaviors.Add(abilityModel2);

            return towerModel;
        }


        public static TowerModel getT5Mid(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();

            AbilityModel abilityModel2 = Game.instance.model.towers.FirstOrDefault((TowerModel quincy) => quincy.name.Contains("Quincy") && quincy.tier == 20).behaviors.FirstOrDefault((Model ab) => ab.name.Contains("Storm")).Clone().Cast<AbilityModel>();
            towerModel.behaviors = towerModel.behaviors.Add(abilityModel2);

            foreach (var item in attackModel.weapons)
            {
                item.rate *= 0.25f;
            }

            return towerModel;
        }



        public static TowerModel getT1Bottom(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
            attackModel.weapons[0].projectile.GetBehavior<TravelStraitModel>().speed *= 2; //use this to increase projectile speed

            return towerModel;
        }


        public static TowerModel getT2Bottom(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
            foreach (var item in attackModel.weapons)
            {
                item.rate *= 0.75f;
            }

            return towerModel;
        }


        public static TowerModel getT3Bottom(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();

            //use this to change the base attack to a pre-existing attack

            var attack = Game.instance.model.GetTowerFromId("HeliPilot-500").Duplicate<TowerModel>().GetBehavior<AttackModel>();

            attackModel.weapons[0].projectile = attack.weapons[2].projectile;
            attackModel.weapons[0].projectile.pierce = 5;
            attackModel.weapons[0].projectile.maxPierce = 99999;
            attackModel.weapons[0].projectile.CapPierce(99999);
            attackModel.weapons[0].projectile.GetBehavior<DamageModel>().damage = 2;
            attackModel.weapons[0].projectile.GetBehavior<DamageModel>().CapDamage(9999);
            attackModel.weapons[0].projectile.GetBehavior<DamageModel>().maxDamage = 9999;
            attackModel.weapons[0].Rate = 1f;
            attackModel.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.Purple;

            return towerModel;
        }


        public static TowerModel getT4Bottom(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
            attackModel.weapons[0].projectile.GetDamageModel().damage *= 2;
            attackModel.weapons[0].projectile.pierce *= 4;
            attackModel.weapons[0].Rate *= 0.5f;
            attackModel.weapons[0].projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
            return towerModel;
        }


        public static TowerModel getT5Bottom(TowerModel towerModel)
        {
            AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
            attackModel.weapons[0].Rate *= 0.1f;
            attackModel.weapons[0].projectile.pierce *= 2;

            return towerModel;
        }

        [HarmonyPatch(typeof(ProfileModel), "Validate")]
        public class ProfileModel_Patch
        {

            [HarmonyPostfix]
            public static void Postfix(ref ProfileModel __instance)
            {
                var unlockedTowers = __instance.unlockedTowers;
                var acquiredUpgrades = __instance.acquiredUpgrades;
                if (!unlockedTowers.Contains(customTowerName))
                {
                    unlockedTowers.Add(customTowerName);
                }
                for (int i = 0; i < upgrades.Length; i++)
                {
                    if (!acquiredUpgrades.Contains(upgrades[i]))
                    {
                        acquiredUpgrades.Add(upgrades[i]);
                    }
                }
            }
        }




        
        //Utility
        public static void SetTiersAndUpgrades(ref TowerModel towerModel, int top, int mid, int bottom)
        {
            if(top != 0 || mid != 0 || bottom != 0)
                towerModel.name = customTowerName + "-" + top + "" + mid + "" + bottom + "";
            towerModel.tiers = new int[] { top, mid, bottom };

            if (top == 5)
            {
                towerModel.upgrades = new Il2CppReferenceArray<UpgradePathModel>(new UpgradePathModel[]
                    {
                    new UpgradePathModel(upgrades[5 + mid], towerModel.name, 1, towerModel.tiers[1]+1),
                    new UpgradePathModel(upgrades[10 + bottom], towerModel.name, 1, towerModel.tiers[2]+1)
                    });
            } else if (mid == 5)
            {
                towerModel.upgrades = new Il2CppReferenceArray<UpgradePathModel>(new UpgradePathModel[]
                    {
                    new UpgradePathModel(upgrades[top], towerModel.name, 1, towerModel.tiers[0]+1),
                    new UpgradePathModel(upgrades[10 + bottom], towerModel.name, 1, towerModel.tiers[2]+1)
                    });
            } else if (bottom == 5)
            {
                towerModel.upgrades = new Il2CppReferenceArray<UpgradePathModel>(new UpgradePathModel[]
                    {
                    new UpgradePathModel(upgrades[top], towerModel.name, 1, towerModel.tiers[0]+1),
                    new UpgradePathModel(upgrades[5 + mid], towerModel.name, 1, towerModel.tiers[1]+1)
                    });
            } else
            {
                towerModel.upgrades = new Il2CppReferenceArray<UpgradePathModel>(new UpgradePathModel[]
                    {
                    new UpgradePathModel(upgrades[top], towerModel.name, 1, towerModel.tiers[0]+1),
                    new UpgradePathModel(upgrades[5 + mid], towerModel.name, 1, towerModel.tiers[1]+1),
                    new UpgradePathModel(upgrades[10 + bottom], towerModel.name, 1, towerModel.tiers[2]+1)
                    });
            }
        }

        //Utility
        public static Texture2D TextureFromPNG(string path)
        {
            Texture2D text = new Texture2D(2, 2);

            if (!ImageConversion.LoadImage(text, File.ReadAllBytes(path)))
            {
                throw new Exception("Could not acquire texture from file " + Path.GetFileName(path) + ".");
            }

            return text;
        }

        static string lastTower;

        [HarmonyPatch(typeof(UpgradeScreen), "UpdateUi")]
        private class UpgradeScreen3
        {

            [HarmonyPrefix]
            internal static bool UpdateUi(ref UpgradeScreen __instance, ref string towerId, string upgradeID) //sets upgrade screen to dart monkey (to prevent crashes)
            {
                lastTower = towerId;
                if (towerId == customTowerName)
                    towerId = "DartMonkey";
                return true;
            }


            [HarmonyPostfix]
            internal static void UpdateUi2(ref UpgradeScreen __instance, ref string towerId, string upgradeID) //sets upgrade icons and descriptions
            {

                if (lastTower == customTowerName)
                {
                    __instance.towerTitle.text = customTowerName;

                    for (int i = 0; i < 5; i++)
                    {
                        __instance.path1Upgrades[i].upgradeName.text = upgradeDescriptions[i];
                        __instance.path1Upgrades[i].portrait = new SpriteReference(customTowerImages + "" + i + "00portrait.PNG");
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        __instance.path2Upgrades[i].upgradeName.text = upgradeDescriptions[i + 5];
                        __instance.path2Upgrades[i].portrait = new SpriteReference(customTowerImages + "0" + i + "0portrait.PNG");
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        __instance.path2Upgrades[i].upgradeName.text = upgradeDescriptions[i + 10];
                        __instance.path2Upgrades[i].portrait = new SpriteReference(customTowerImages + "00" + i + "portrait.PNG");
                    }
                }
            }
        }
    }
}
