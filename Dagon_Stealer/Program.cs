
using System;
using System.Linq;
using Ensage.Common.Menu;
using Ensage;
using SharpDX;
using SharpDX.Direct3D9;
using System.Collections.Generic;

using Ensage.Common.Extensions;
using Ensage.Common;
using System.Windows.Input;

using Ensage.Common.Objects;
using Ensage.Items;

//using System;
//using System.Linq;
//using System.Collections.Generic;

//using Ensage;
//using SharpDX;
//using Ensage.Common.Extensions;
//using Ensage.Common;
//using SharpDX.Direct3D9;
//using System.Windows.Input;


//using System.Reflection;
//using System.Runtime.InteropServices;
//using System.Security;

using Attribute = Ensage.Attribute;

namespace Dagon_Stealer
{
    class Program
    {

        private static readonly Hero[] phero = new Hero[10];
        private static string[] ptext = new string[60];//{ "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }
        private static Vector2[] ppos = new Vector2[10];


        private static readonly Dictionary<Unit, ParticleEffect> Effects = new Dictionary<Unit, ParticleEffect>();
        private static Font _text;
        private static readonly int[] Penis = new int[5] { 400, 500, 600, 700, 800 };
        private static readonly int[] ShitDickFuck = new int[5] { 600, 650, 700, 750, 800 };
        //private static Key KeyC = Key.D;

        //private static readonly int[] spellq = new int[4] { 100, 175, 250, 325 };//урон первого скилла
        //private static readonly int[] spellw = new int[4] { 600, 700, 800, 900 };//дальность второго скилла

        private static readonly Menu Menu = new Menu("Dagon Stealer", "dagonstealer", true);
        private static Font text;
        //private static float time;
        //private static bool hp;
        private static PowerTreads pta;
        private static int sleeptick;

        /*public static class Chars
        {
            public static const char Newline = '\n';
            public static const char Tab = '\t';
            public static const char CarriageReturn = '\r';
            public static const char Beep = '\b';
        } */

        private static List<string> Ignore = new List<string> 
        {
            "modifier_item_sphere_target",
            "modifier_templar_assassin_refraction_absorb",
            "modifier_dazzle_shallow_grave",
            "modifier_item_pipe_barrier",
            "modifier_nyx_assassin_spiked_carapace",
            "modifier_item_blade_mail_reflect",
            "modifier_item_lotus_orb_active",
            "modifier_abaddon_borrowed_time_damage_redirect"
        };

        private static Dictionary<string, bool> enemies = new Dictionary<string, bool>();

        private static double bse;
        //private static dynamic id = ObjectMgr.LocalHero;
        private static Hero id = ObjectMgr.LocalHero;
        //private static Vector3 prepos = ObjectMgr.LocalHero.Position;
        //private static Hero id;

        private static void Main(string[] args)
        {
            Menu.AddItem(new MenuItem("keyBind", "Main Key").SetValue(new KeyBind('H', KeyBindType.Toggle, true)));
            Menu.AddItem(new MenuItem("enemies", "Heroes").SetValue(new HeroToggler(enemies)));
            Menu.AddToMainMenu();


            //Game.OnUpdate += Vagina;
            Drawing.OnDraw += Vagina;
        }

        private static void Vagina(EventArgs Tits)
        //internal void Vagina(EventArgs Tits)
        {

            //if (time > 0) { time -= 1; return; } else { time = 30; }

            var me = ObjectMgr.LocalHero;
            if (!Game.IsInGame) { return; }
            //if (!me.IsAlive) { hp = false; }
            if (me == null || !me.IsAlive) { return; }
            double damag = 0;
            var dps = me.AttacksPerSecond * me.MinimumDamage;
            var Q = me.Spellbook.SpellQ;//laser
            var W = me.Spellbook.SpellW;//rocket
            var E = me.Spellbook.SpellE;//marsh
            var D = me.Spellbook.SpellD;//
            var R = me.Spellbook.SpellR;//rearm
            


            //illusion
            /*if (Utils.SleepCheck("R"))
            {

                var heroes = ObjectMgr.GetEntities<Hero>().Where(creep => (creep.IsAlive && creep.IsVisible && !creep.IsIllusion && (point_distance(me, creep) < (R.CastRange * R.CastRange)))).ToList();

                foreach (var v in heroes)
                {
                    //Q
                    if (R != null && R.CanBeCasted()
               && ((v.IsLinkensProtected() || !v.IsLinkensProtected()) || me.Team == v.Team)
               && me.CanCast()
               && me.Distance2D(v) < R.CastRange//1400
               && R.Cooldown == 0
               && me.Name != v.Name
               && R.ManaCost < me.Mana
               )
                    {

                        R.UseAbility(v);//
                        Utils.Sleep(250, "R");
                        Utils.Sleep(100, "is");
                    }
                }
            }*/
            //else

            //if (R.Cooldown > 0)
            //{
            /*if (Utils.SleepCheck("is"))
            {

                var bhero = ObjectMgr.GetEntities<Hero>().Where(creep => (creep.IsAlive && creep.IsIllusion && creep.IsControllable && creep.Team == me.Team)).ToList();//&& creep.Name!=me.Name

                if (bhero.Count > 0 && bhero.Any())
                {
                    foreach (var f in bhero)
                    {
                        if (me.Team == Team.Radiant)
                        {
                            Vector3 pos;
                            pos.X = -7000; //- 7000;
                            pos.Y = -7000; //- 7000;
                            pos.Z = me.Position.Z;
                            f.Move(pos);
                            Utils.Sleep(55000, "is");//R.CooldownLength
                        }
                        if (me.Team == Team.Dire)
                        {
                            Vector3 pos;
                            pos.X = 7000;
                            pos.Y = 7000;
                            pos.Z = me.Position.Z;
                            f.Move(pos);
                            Utils.Sleep(55000, "is");//R.CooldownLength
                        }
                    }

                }
                bhero.Clear();
            }
            */



            /////////////
            var pt = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_power_treads")));
            pta = me.FindItem("item_power_treads") as PowerTreads;

            //if (pt!=null){if (Utils.SleepCheck("pt")) { pt.UseAbility(); Utils.Sleep(750, "pt"); }}
            //if (!D.IsActivated)
            //{ 
            //D.ToggleAbility();//UseAbility();////Utils.Sleep(1000, "hp");
            //}

            //var ench = ObjectMgr.GetEntities<Hero>().Where(creep => (creep.Team != me.Team) && (creep.IsAlive && creep.IsVisible) && (LowUsageDistance(me, creep) < (creep.AttackRange * creep.AttackRange+1000))).ToList();
            /*for (var i = 0; i < 10; i += 1)
                {

                    if (phero[i]!=null && phero[i].Team!=me.Team)
                    {
                    var l = phero[i].Level;
                    var u = phero[i].FindItem("item_ultimate_scepter");
                    if (phero[i].Name == "npc_dota_hero_invoker")
                    {
                        if (l >= 1) { gdamag += 100 / (1 - me.MagicDamageResist); }
                        if (l >= 3) { gdamag += 62.5 / (1 - me.MagicDamageResist); }
                        if (l >= 5) { gdamag += 62.5 / (1 - me.MagicDamageResist); }
                        if (l >= 7) { gdamag += 62.5 / (1 - me.MagicDamageResist); }
                        if (l >= 9) { gdamag += 62.5 / (1 - me.MagicDamageResist); }
                        if (l >= 11) { gdamag += 62.5 / (1 - me.MagicDamageResist); }
                        if (l >= 13) { gdamag += 62.5 / (1 - me.MagicDamageResist); }
                        if (u != null) { gdamag += 62.5 / (1 - me.MagicDamageResist); }
                    }
                    if (phero[i].Name == "npc_dota_hero_zuus")
                    {
                        if (l >= 6) { gdamag += 225; if (u != null) { gdamag += 215; } }//u
                        if (l >= 11) { gdamag += 125; if (u != null) { gdamag += 100; } }//u
                        if (l >= 16) { gdamag += 125; if (u != null) { gdamag += 100; } }//u
                    }
                }
                }*/

            /*if (((D != null && D.CanBeCasted() && me.CanCast() && Utils.SleepCheck("hp")) || (pt != null && pta.ActiveAttribute != Attribute.Strength && Utils.SleepCheck("pt"))))
            {
                
                
                var ench = ObjectMgr.GetEntities<Hero>().Where(creep => (creep.Team != me.Team) && (creep.IsAlive && creep.IsVisible && !creep.IsIllusion) && (point_distance(me, creep) < 1000 * 1000)).ToList();//(creep.AttackRange * creep.AttackRange+1000)

                double fdamag = 0;
                double mdamag = 0;
                double gdamag = 0;
                //&& (LowUsageDistance(me, creep) < (creep.AttackRange * creep.AttackRange+128*128))

                

                if (ench.Any() && ench.Count > 0)
                {
                    foreach (var v in ench)
                    {
                        if (v.Team != me.Team)
                        {
                            var u = v.FindItem("item_ultimate_scepter");
                            fdamag += v.MaximumDamage + v.BonusDamage;
                            var l = v.Level;
                            if (v.Name == "npc_dota_hero_phoenix")
                            {
                                if (l >= 1) { mdamag += 70; }//1
                                if (l >= 2) { mdamag += 70; }//2
                                if (l >= 3) { mdamag += 70; }//1
                                if (l >= 4) { mdamag += 70; }//2
                                if (l >= 5) { mdamag += 70; }//1

                                if (l >= 6) { mdamag += 360; }//u

                                if (l >= 7) { mdamag += 70; }//2
                                if (l >= 8) { mdamag += 70; }//1
                                if (l >= 9) { mdamag += 70; }//2

                                if (l >= 10) { mdamag += 60; }//3

                                if (l >= 11) { mdamag += 180; }//u

                                if (l >= 12) { mdamag += 60; }//3
                                if (l >= 13) { mdamag += 60; }//3
                                if (l >= 14) { mdamag += 60; }//3

                                if (l >= 16) { mdamag += 180; }//u
                            }

                            if (v.Name == "npc_dota_hero_shredder")
                            {
                                if (l >= 1) { mdamag += 100 / (1 - me.MagicDamageResist); }//2
                                if (l >= 2) { mdamag += 100; }//1
                                if (l >= 3) { mdamag += 40 / (1 - me.MagicDamageResist); }//2
                                if (l >= 4) { mdamag += 50; }//1
                                if (l >= 5) { mdamag += 40 / (1 - me.MagicDamageResist); }//2
                                if (l >= 6) { mdamag += 200 / (1 - me.MagicDamageResist) + 50; if (u != null) { mdamag += 200 / (1 - me.MagicDamageResist) + 50; } }//2
                                if (l >= 7) { mdamag += 40 / (1 - me.MagicDamageResist); }//2
                                if (l >= 8) { mdamag += 50; }//1
                                if (l >= 9) { mdamag += 50; }//1
                                if (l >= 11) { mdamag += 80 / (1 - me.MagicDamageResist) + 25; if (u != null) { mdamag += 80 / (1 - me.MagicDamageResist) + 25; } }//2
                                if (l >= 16) { mdamag += 80 / (1 - me.MagicDamageResist) + 25; if (u != null) { mdamag += 80 / (1 - me.MagicDamageResist) + 25; } }//2
                            }

                            if (v.Name == "npc_dota_hero_nevermore")
                            {
                                if (l >= 1) { mdamag += 300; }
                                if (l >= 3) { mdamag += 225; }
                                if (l >= 5) { mdamag += 225; }
                                if (l >= 6) { mdamag += 18 * 80; if (u != null) { mdamag += 18 * 80 * 0.4; } }
                                if (l >= 7) { mdamag += 225; }
                                if (l >= 11) { mdamag += 18 * 40; if (u != null) { mdamag += 18 * 40 * 0.4; } }
                                if (l >= 16) { mdamag += 18 * 40; if (u != null) { mdamag += 18 * 40 * 0.4; } }
                            }

                            if (v.Name == "npc_dota_hero_invoker")
                            {
                                if (l >= 1) { mdamag += 40; }
                                if (l >= 2) { mdamag += 80; }
                                if (l >= 3) { mdamag += 120; }
                                if (l >= 4) { mdamag += 120 * 0.9; }
                                if (l >= 5) { mdamag += 120 * 0.9; }

                                if (l >= 7) { mdamag += 120 * 0.8; }
                                if (l >= 8) { mdamag += 120 * 0.8; }
                                if (l >= 9) { mdamag += 120 * 0.7; }
                                if (l >= 10) { mdamag += 120 * 0.7; }

                                if (l >= 12) { mdamag += 120 * 0.6; }
                                if (l >= 13) { mdamag += 120 * 0.6; }
                                if (l >= 14) { mdamag += 120 * 0.5; }
                                if (l >= 15) { mdamag += 120 * 0.5; }

                                if (l >= 17) { mdamag += 120 * 0.4; }
                                if (l >= 18) { mdamag += 120 * 0.4; }
                                if (l >= 19) { mdamag += 120 * 0.3; }
                                if (l >= 20) { mdamag += 120 * 0.3; }
                                if (l >= 21) { mdamag += 120 * 0.2; }
                                if (l >= 22) { mdamag += 120 * 0.2; }
                                if (l >= 23) { mdamag += 120 * 0.1; }
                                if (l >= 24) { mdamag += 120 * 0.1; }
                                if (u != null) { mdamag += 400; }
                                //if (mdamag>1200) {mdamag=1200;}
                            }

                            if (v.Name == "npc_dota_hero_leshrac")
                            {
                                if (l >= 1) { mdamag += 100; }//1
                                if (l >= 2) { fdamag += 90; }//2
                                if (l >= 3) { mdamag += 50; }//3
                                if (l >= 4) { fdamag += 90; }//2
                                if (l >= 5) { mdamag += 50; }//1
                                if (l >= 6) { mdamag += 100; if (u != null) { mdamag += 60; } }//u
                                if (l >= 7) { fdamag += 90; }//2
                                if (l >= 8) { fdamag += 90; }//2
                                if (l >= 9) { mdamag += 50; }//3
                                if (l >= 10) { mdamag += 50; }//1
                                if (l >= 11) { mdamag += 30; if (u != null) { mdamag += 30; } }//u
                                if (l >= 12) { mdamag += 50; }//3
                                if (l >= 13) { mdamag += 50; }//1
                                if (l >= 14) { mdamag += 50; }//3
                                if (l >= 16) { mdamag += 30; if (u != null) { mdamag += 30; } }//u
                            }

                            if (v.Name == "npc_dota_hero_lina")
                            {
                                if (l >= 1) { mdamag += 110; }//1
                                if (l >= 2) { mdamag += 80; }//2
                                if (l >= 3) { mdamag += 70; }//1
                                if (l >= 4) { mdamag += 50; }//2
                                if (l >= 5) { mdamag += 70; }//1
                                if (l >= 6) { if (u != null) { mdamag += 450 / (1 - me.MagicDamageResist); } else { mdamag += 450; } }//u
                                if (l >= 7) { mdamag += 70; }//1
                                if (l >= 8) { mdamag += 50; }//2
                                if (l >= 9) { mdamag += 40; }//2
                                if (l >= 11) { if (u != null) { mdamag += 200 / (1 - me.MagicDamageResist); } else { mdamag += 200; } }//u
                                if (l >= 16) { if (u != null) { mdamag += 200 / (1 - me.MagicDamageResist); } else { mdamag += 200; } }//u
                            }

                            if (v.Name == "npc_dota_hero_lion")
                            {
                                if (l >= 1) { mdamag += 80; }
                                if (l >= 3) { mdamag += 60; }
                                if (l >= 5) { mdamag += 60; }
                                if (l >= 6) { mdamag += 600; if (u != null) { mdamag += 125; } }
                                if (l >= 7) { mdamag += 60; }
                                if (l >= 11) { mdamag += 125; if (u != null) { mdamag += 150; } }
                                if (l >= 16) { mdamag += 125; if (u != null) { mdamag += 150; } }
                            }
                            if (v.Name == "npc_dota_hero_oracle")
                            {
                                if (l >= 1) { mdamag += 90; }//3
                                if (l >= 2) { mdamag += 90; }//1
                                if (l >= 3) { mdamag += 90; }//3
                                if (l >= 4) { mdamag += 30; }//1
                                if (l >= 5) { mdamag += 90; }//3
                                if (l >= 6) { mdamag += 30; }//1
                                if (l >= 7) { mdamag += 90; }//3
                                if (l >= 8) { mdamag += 30; }//1
                            }
                            if (v.Name == "npc_dota_hero_queenofpain")
                            {
                                if (l >= 1) { mdamag += 75; }//3
                                if (l >= 2) { mdamag += 50 + 30 * 5; }//1
                                if (l >= 3) { mdamag += 75; }//3
                                if (l >= 4) { mdamag += 25 + 10 * 5; }//1
                                if (l >= 5) { mdamag += 75; }//3
                                if (l >= 6) { mdamag += 290 / (1 - me.MagicDamageResist); if (u != null) { mdamag += 35 / (1 - me.MagicDamageResist); } }//u
                                if (l >= 7) { mdamag += 75; }//3
                                if (l >= 8) { mdamag += 25 + 10 * 5; }//1
                                if (l >= 9) { mdamag += 25 + 10 * 5; }//1
                                if (l >= 11) { mdamag += 380 / (1 - me.MagicDamageResist); if (u != null) { mdamag += 115 / (1 - me.MagicDamageResist); } }//u
                                if (l >= 16) { mdamag += 470 / (1 - me.MagicDamageResist); if (u != null) { mdamag += 115 / (1 - me.MagicDamageResist); } }//u
                            }

                            if (v.Name == "npc_dota_hero_skywrath_mage")
                            {
                                if (l >= 1) { mdamag += 60 + v.TotalIntelligence * 1.6; }//1
                                if (l >= 2) { mdamag += 60; }//2
                                if (l >= 4) { mdamag += 60; }//2
                                if (l >= 5) { mdamag += 60; }//2
                                if (l >= 6) { mdamag += 600; }//u
                                if (l >= 7) { mdamag += 60; }//2
                                if (l >= 8) { mdamag += 20; }//1
                                if (l >= 9) { mdamag += 20; }//1
                                if (l >= 10) { mdamag += 20; }//1
                                if (l >= 11) { mdamag += 400; }//u
                                if (l >= 16) { mdamag += 400; }//u

                                if (l >= 14) { mdamag *= 1.45; } else { if (l >= 13) { mdamag *= 1.40; } else { if (l >= 12) { mdamag *= 1.35; } else { if (l >= 3) { mdamag *= 1.3; } } } }//3 (3,12,13,14)
                            }

                            if (v.Name == "npc_dota_hero_techies")
                            {
                                if (l >= 1) { mdamag += 500; }//3
                                if (l >= 2) { fdamag += 150; }//1
                                if (l >= 3) { mdamag += 150; }//3
                                if (l >= 4) { fdamag += 40; }//1
                                if (l >= 5) { mdamag += 200; }//3
                                if (l >= 6) { mdamag += 300; }//u
                                if (l >= 7) { mdamag += 300; }//3
                                if (l >= 8) { fdamag += 35; }//1
                                if (l >= 9) { fdamag += 35; }//1
                                if (l >= 11) { mdamag += 150; }//u
                                if (l >= 16) { mdamag += 150; }//u
                            }

                            if (v.Name == "npc_dota_hero_tinker")
                            {
                                if (l >= 1) { mdamag += 125; }//2
                                if (l >= 2) { mdamag += 80 / (1 - me.MagicDamageResist); }//1
                                if (l >= 3) { mdamag += 80 / (1 - me.MagicDamageResist); }//1
                                if (l >= 9) { mdamag += 96; }//3
                                if (l >= 5) { mdamag += 80 / (1 - me.MagicDamageResist); }//1
                                if (l >= 6) { mdamag += 75; }//2
                                if (l >= 7) { mdamag += 80 / (1 - me.MagicDamageResist); }//1
                                if (l >= 8) { mdamag += 75; }//2
                                if (l >= 4) { mdamag += 75; }//2
                                if (l >= 10) { mdamag += 48; }//3
                                if (l >= 11) { mdamag += 48; }//3
                                if (l >= 12) { mdamag += 48; }//3
                            }

                            if (v.Name == "npc_dota_hero_zuus")
                            {
                                if (l >= 1) { mdamag += 100; }//2
                                if (l >= 2) { mdamag += 85; }//1
                                if (l >= 3) { mdamag += 75; }//2
                                if (l >= 4) { mdamag += 15; }//1
                                if (l >= 5) { mdamag += 75; }//2
                                if (l >= 7) { mdamag += 75; }//2
                                if (l >= 8) { mdamag += 15; }//1
                                if (l >= 9) { mdamag += 30; }//1
                                if (l >= 14) { mdamag += me.Health * 0.11; } else { if (l >= 13) { mdamag += me.Health * 0.09; } else { if (l >= 12) { mdamag += me.Health * 0.07; } else { if (l >= 10) { mdamag += me.Health * 0.05; } } } }//(10,12,13,14)
                            }


                            //var g = v.Level;
                            //if (g > 8) { g = 8; }
                            //mdamag += g*75;
                        }
                    }
                }

                //if (mdamag < 640) { mdamag = 640; }

                fdamag *= (1 - me.DamageResist);
                mdamag *= (1 - me.MagicDamageResist);

                if (mdamag < 320 + me.Level * 10) { mdamag = 320 + me.Level * 10; }

                if (pt != null && (me.Health < mdamag || me.Health < fdamag || me.Health < gdamag) && pta.ActiveAttribute != Attribute.Strength && Utils.SleepCheck("pt")) { pt.UseAbility(); Utils.Sleep(100 + Game.Ping, "pt"); }

                if (Utils.SleepCheck("hp"))
                {
                    if ((me.Health < mdamag || me.Health < fdamag || me.Health < gdamag) && !D.IsToggled)
                    {
                        D.ToggleAbility(); Utils.Sleep(500 / (1 + D.Level) + Game.Ping, "hp");
                    }
                    else
                    {
                        if ((me.Health > mdamag && me.Health > fdamag) && D.IsToggled)
                        {
                            D.ToggleAbility(); Utils.Sleep(500 / (1 + D.Level) + Game.Ping, "hp");
                        }
                    }
                }

                // * (1 - me.MagicDamageResist) || me.Health < 400 * (1 - me.DamageResist)//hp = true;//if (Utils.SleepCheck("hp")) {//} Utils.Sleep(25, "hp");*/
            /*if (!D.IsToggled || !D.IsActivated) { D.ToggleAbility(); }//hp = false;//else { if (D.IsActivated ) { D.ToggleAbility(); } } //|| D.IsToggled

}*/

            ////////////


            if (id != null && id != me) { if /*(id != me && (!id.IsAlive || !id.IsVisible))*/(!id.IsAlive || !id.IsVisible) { id = me; bse = 0; } }

            if (Utils.SleepCheck("attack") && me.CanAttack())
            {

                if (id != null && id != me && id.IsAlive && id.IsVisible)
                {
                    if (point_distance(me.Position, id.Position) < (me.AttackRange + 400) * (me.AttackRange + 400))
                    {
                        /*if id.IsVisible */
                        me.Attack(id); Utils.Sleep(me.SecondsPerAttack * 1000, "attack");
                    }
                }
                else
                {

                    var quellingBlade = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_quelling_blade") || item.Name.Contains("item_iron_talon")));
                    damag = ((quellingBlade != null) ? ((me.IsRanged != null) ? (me.MinimumDamage * 1.40) : (me.MinimumDamage * 1.15)) : (me.MinimumDamage)) + me.BonusDamage;
                    var creeps = ObjectMgr.GetEntities<Unit>().Where(creep => (

                        ((creep.ClassID == ClassID.CDOTA_BaseNPC_Creep_Lane || creep.ClassID == ClassID.CDOTA_BaseNPC_Creep_Siege) && creep.IsSpawned && creep.IsAlive && creep.IsVisible && ((me.Team != creep.Team && creep.Health < damag * (1 - creep.DamageResist)) || (me.Team == creep.Team && creep.Health < creep.MaximumHealth / 2 && creep.Health < damag * (1 - creep.DamageResist))))
                        //|| (creep.ClassID == ClassID.CDOTA_BaseNPC_Tower && ((me.Team != creep.Team && creep.Health < damag * (1 - creep.DamageResist)) || (me.Team == creep.Team && creep.Health < creep.MaximumHealth*0.1 && creep.Health < damag * (1 - creep.DamageResist))))

                        && (point_distance(me, creep) < (me.AttackRange + 350) * (me.AttackRange + 350)))).ToList();

                    if (creeps.Count > 0 && creeps.Any())
                    {
                        var a = creeps[0];
                        float m = ((me.AttackRange + 350) * (me.AttackRange + 350));
                        foreach (var v in creeps)
                        {
                            float d = point_distance(me, v);
                            if (d < m) { m = d; a = v; }
                        }
                        me.Attack(a); Utils.Sleep(me.SecondsPerAttack * 1000, "attack");
                    }
                    /*else
                    {
                        creeps.Clear();
                        creeps = ObjectMgr.GetEntities<Unit>().Where(creep => (

                        ((creep.ClassID == ClassID.CDOTA_BaseNPC_Creep_Lane || creep.ClassID == ClassID.CDOTA_BaseNPC_Creep_Siege) && creep.IsSpawned && creep.IsAlive && creep.IsVisible && ((me.Team != creep.Team && creep.Health-50 < damag * (1 - creep.DamageResist)) || (me.Team == creep.Team && creep.Health-50 < creep.MaximumHealth / 2 && creep.Health-50 < damag * (1 - creep.DamageResist))))

                        && (point_distance(me, creep) < (me.AttackRange + 350) * (me.AttackRange + 350)))).ToList();
                        if (creeps.Count > 0 && creeps.Any())
                        {

                            var a = creeps[0];
                            float m = ((me.AttackRange + 350) * (me.AttackRange + 350));
                            foreach (var v in creeps)
                            {
                                float d = point_distance(me, v);
                                if (d < m) { m = d; a = v; }
                            }

                            me.Follow(a); 
                        }
                    }*/
                    /*else
                    {
                        var towers = ObjectMgr.GetEntities<Unit>().Where(obj => (obj.ClassID == ClassID.CDOTA_BaseNPC_Tower && obj.Team==me.Team && point_distance(obj,me)<1000)).ToList();
                        if (towers.Count > 0 && towers.Any())
                        {
                            var a = towers[0];
                            float m = 1000;
                            foreach (var v in towers)
                            {
                                float d = point_distance(me, v);
                                if (d < m) { m = d; a = v; }
                            }
                             me.Move(a.Position);//+Math.Atan2(,)
                        }
                    }*/
                }
            }


            //damag = me.MinimumDamage + me.BonusDamage;

            var enemy = ObjectMgr.GetEntities<Hero>().Where(obj => (obj.Team != me.Team && obj.IsAlive && obj.IsVisible && !obj.IsIllusion && !obj.IsMagicImmune())).ToList();

            damag = 0;

            var dagon = me.Inventory.Items.FirstOrDefault(item => item.Name.Contains("item_dagon"));
            var ethereal = me.Inventory.Items.FirstOrDefault(item => item.Name.Contains("item_ethereal_blade"));

            //var kill = false;
            double mdc = 0;//максимальный нанесёный урон для убийства врага
            double mui = 100;//кол-во использованных итемов
            //double bse = 0;//наилучшая последовательность действий

            //double se = 0;



            if (Utils.SleepCheck("ai") || bse == 0 /*|| id == me*/)//!id.IsAlive || !id.IsVisible
            {
                if (bse == 0)//me.CanCast())
                {
                    Utils.Sleep(1000, "ai");
                    //bse = 0;//наилучшая последовательность действий
                    //id = me;//наилучшая цель

                    float nb = 0;
                    float mb = 100000;
                    Hero[] plist = new Hero[5]/*{ enemy[0], enemy[1], enemy[2], enemy[3], enemy[4] }*/;
                    float[] dlist = new float[5];

                    for (var b = 0; b < enemy.Count; b += 1)
                    {
                        plist[b] = enemy[b];
                        dlist[b] = enemy[b].Distance2D(me.Position);
                    }

                    for (var repeat = 0; repeat < 5; repeat += 1)
                    {
                    for (var b = 0; b < enemy.Count-1; b += 1)
                    {
                        if (dlist[b]>dlist[b+1])
                        {
                            var a = dlist[b];
                            dlist[b] = dlist[b + 1];
                            dlist[b + 1] = a;

                            var h = plist[b];
                            plist[b] = plist[b + 1];
                            plist[b + 1] = h;
                        }
                    }
                    }

                        /*foreach (var b in enemy)
                        {
                            var a = b.Distance2D(me.Position);
                            if (a > nb && a < mb)
                            {
                                mb = a;
                            }
                        }*/

                        for (var b = 0; b < enemy.Count; b += 1)//foreach (var v in enemy)
                        {
                            var v=plist[b];
                            var linkens = v.Inventory.Items.FirstOrDefault(Gay => Gay.Name == "item_sphere");
                            if (!((linkens != null && linkens.Cooldown == 0) || v.Modifiers.Any(x => Ignore.Contains(x.Name))))
                            {
                                for (var i1 = 0; i1 < 4; i1 += 1)
                                {
                                    for (var i2 = 0; i2 < 4; i2 += 1)
                                    {
                                        for (var i3 = 0; i3 < 4; i3 += 1)
                                        {
                                            for (var i4 = 0; i4 < 4; i4 += 1)
                                            {
                                                if ((i1 != i2 && i1 != i3 && i1 != i4) && (i2 != i3 && i2 != i4) && (i3 != i4))
                                                {

                                                    double mc = 0;//мана кост
                                                    double ui = 0;//использованных предметов//нанесение урона
                                                    double dc = 0;//нанесение урона по игроку
                                                    Vector3 pos = me.Position;//new Vector2(x,y);
                                                    //double hp=me.Health;
                                                    double mp = me.Mana;//мана

                                                    double ehp = v.Health + v.HealthRegeneration * 0.4;//хп врага
                                                    double fr = 1 - v.DamageResist;//защита врага
                                                    double mr = 1 - v.MagicDamageResist;//маг.защита врага
                                                    double se = 0;
                                                    for (var n = 0; n < 4; n += 1)//0-q,1-w,2-dagon,3-ethereal,4-veil
                                                    {
                                                        if (ehp > 0)
                                                        {

                                                            var ev = 0;
                                                            if (n == 0) { ev = i1; } if (n == 1) { ev = i2; } if (n == 2) { ev = i3; } if (n == 3) { ev = i4; }

                                                            if (ev == 0 && Q != null && Q.CanBeCasted() && Q.Cooldown == 0 && mp > Q.ManaCost && point_distance(v.Position, pos) < Q.CastRange * Q.CastRange)//Q
                                                            {
                                                                //pos = v.Position;
                                                                damag = (80 * (Q.Level - 1));
                                                                ehp -= damag;
                                                                dc += damag;
                                                                ui += 1;
                                                                mp -= Q.ManaCost;
                                                                mc += Q.ManaCost;
                                                                se += (ev + 1) * 1000 / Math.Pow(10, n);

                                                                /*if (me.CanAttack())
                                                                {
                                                                    damag = me.AttackRange / v.MovementSpeed * dps;
                                                                    ehp -= damag * fr;
                                                                    dc += damag * fr;
                                                                }*/

                                                            }
                                                            if (ev == 1 && b<2)
                                                            {
                                                                //Hero[] plist = new Hero[5];
                                                                //int[] Penis = new int[5] { 400, 500, 600, 700, 800 };
                                                                /*float nb = 0;
                                                                float mb=100000;

                                                                foreach (var b in enemy)
                                                                {
                                                                    var a=b.Distance2D(pos);
                                                                    if (a>nb && a<mb)
                                                                    {
                                                                        mb = a;
                                                                    }
                                                                }
                                                                */


                                                                if ((W != null && W.CanBeCasted() && W.Cooldown == 0 && mp > W.ManaCost && point_distance(v.Position, pos) < W.CastRange * W.CastRange))//W
                                                                {
                                                                    /*damag = me.TotalAgility / me.TotalStrength;
                                                                    if (damag < 0.25) { damag = 0.25; }
                                                                    if (damag > 0.5 * W.Level) { damag = 0.5 * W.Level; }
                                                                    damag *= me.TotalAgility;
                                                                    damag += 100;
                                                                    */
                                                                    damag = 125 + 75 * (W.Level - 1);
                                                                    ehp -= damag * mr;
                                                                    dc += damag * mr;
                                                                    ui += 1;
                                                                    mp -= W.ManaCost;
                                                                    mc += W.ManaCost;
                                                                    se += (ev + 1) * 1000 / Math.Pow(10, n);
                                                                }

                                                            }
                                                            if (ev == 2 && dagon != null && dagon.CanBeCasted() && dagon.Cooldown == 0 && mp > dagon.ManaCost && point_distance(v.Position, pos) < dagon.CastRange * dagon.CastRange)//Dag
                                                            {
                                                                damag = (400 + (dagon.Level - 1) * 100);
                                                                ehp -= damag * mr;
                                                                dc += damag * mr;
                                                                ui += 1;
                                                                mp -= dagon.ManaCost;
                                                                mc += dagon.ManaCost;
                                                                se += (ev + 1) * 1000 / Math.Pow(10, n);
                                                            }
                                                            var ModifEther = v.Modifiers.Any(o => o.Name == "modifier_item_ethereal_blade_slow");
                                                            if (ev == 3)
                                                            {
                                                                if ((ethereal != null && ethereal.CanBeCasted() && ethereal.Cooldown == 0 && mp > ethereal.ManaCost && point_distance(v.Position, pos) < ethereal.CastRange * ethereal.CastRange))//Eth
                                                                {
                                                                    mr *= 1.4;
                                                                    damag = me.TotalStrength;
                                                                    if ((int)me.PrimaryAttribute == 1) { damag = me.TotalAgility; }
                                                                    if ((int)me.PrimaryAttribute == 2) { damag = me.TotalIntelligence; }
                                                                    damag *= 2;
                                                                    damag += 75;

                                                                    ehp -= damag * mr;
                                                                    dc += damag * mr;
                                                                    ui += 1;
                                                                    mp -= ethereal.ManaCost;
                                                                    mc += ethereal.ManaCost;
                                                                    se += (ev + 1) * 1000 / Math.Pow(10, n);
                                                                }

                                                            }

                                                        }
                                                        else { if (ui <= mui) { if (dc > mdc) { mdc = dc; mui = ui; bse = se; id = v; } } }
                                                        //else { kill = true; }
                                                    }

                                                    //if (ehp <= 0) { if (ui <= mui) { if (dc > mdc) { mdc = dc; mui = ui; bse = se; id = v; } } }


                                                }
                                            }
                                        }
                                    }
                                }

                                //Drawing.DrawText("Q", new Vector2(300, 450), new Vector2(100, 100), Color.White, FontFlags.AntiAlias);

                            }
                        }

                }
            }
            else
            {



                double a = 0;
                //double b = bse;
                if (id != me)//&& me.CanCast()
                {

                    //if (!id.IsAlive||!id.IsVisible) { bse = 0; return; }



                    if (Utils.SleepCheck("next"))
                    {
                        var ModifEther = id.Modifiers.Any(o => o.Name == "modifier_item_ethereal_blade_slow");
                        //if (ethereal == null || ModifEther || ethereal.Cooldown < 17 || Utils.SleepCheck("ethereal"))
                        //{
                        for (var n = 0; n < 4; n += 1)//a: 0-q,1-w,2-dagon,3-ethereal,4-veil
                        {
                            if (n == 0) { a = Math.Floor(bse / 1000); bse -= a * 1000; }
                            if (n == 1) { a = Math.Floor(bse / 100); bse -= a * 100; }
                            if (n == 2) { a = Math.Floor(bse / 10); bse -= a * 10; }
                            if (n == 3) { a = Math.Floor(bse); bse -= a; }

                            if (a == 1 && Q.CanBeCasted() && me.CanCast() && Q.Cooldown == 0 && me.Mana > Q.ManaCost) //&& Utils.SleepCheck("Q")
                            {
                                //Game.MousePosition
                                Q.UseAbility(id);//id.Predict(500));
                                Utils.Sleep(100, "next");
                                n = 5;//return;
                                //Utils.Sleep(100, "Q");
                            }
                            if (a == 2 && W.CanBeCasted() && me.CanCast() && W.Cooldown == 0 && me.Mana > W.ManaCost && (!Utils.SleepCheck("ethereal") || ModifEther))//&& Utils.SleepCheck("W")
                            {
                                W.UseAbility();
                                Utils.Sleep(100, "next");
                                n = 5;//return;
                                //Utils.Sleep(100, "W");
                            }
                            if (a == 3 && dagon.CanBeCasted() && me.CanCast() && dagon.Cooldown == 0 && me.Mana > dagon.ManaCost && (!Utils.SleepCheck("ethereal") || ModifEther)) //&& Utils.SleepCheck("dagon")
                            {
                                dagon.UseAbility(id);
                                Utils.Sleep(100, "next");
                                n = 5;//return;
                                //Utils.Sleep(1000, "dagon");//1000
                            }
                            if (a == 4 && ethereal.CanBeCasted() && me.CanCast() && ethereal.Cooldown == 0 && me.Mana > ethereal.ManaCost) //&& Utils.SleepCheck("ethereal")
                            {
                                ethereal.UseAbility(id);
                                Utils.Sleep(100, "next");
                                n = 5;//return;
                                Utils.Sleep(2000, "ethereal");//3000
                            }
                            //if (ModifEther) { }
                        }
                        //}


                    }

                }


            }


            /*if (Utils.SleepCheck("attack") && me.CanAttack())
                    {
                        me.Attack(id); Utils.Sleep(me.SecondsPerAttack * 1000, "attack");
                    }*/

            /*else
                                            {
                                            if (!Utils.SleepCheck("W")) 
                                                {
                                                damag = me.TotalAgility / me.TotalStrength;
                                                if (damag < 0.25) { damag = 0.25; }
                                                if (damag > 0.5 * W.Level) { damag = 0.5 * W.Level; }
                                                damag *= me.TotalAgility;
                                                damag += 100;

                                                ehp -= damag * mr;
                                                fdc += damag * mr;
                                                dc += damag;
                                                }
                                            }*/

            /*else
        {
            if (ethereal != null && ethereal.Cooldown > 17 && !Utils.SleepCheck("ethereal") && !ModifEther)
            {
            mr *= 1.4;
            damag = me.TotalStrength;
            if ((int)me.PrimaryAttribute == 1) { damag = me.TotalAgility; }
            if ((int)me.PrimaryAttribute == 2) { damag = me.TotalIntelligence; }
            damag *= 2;
            damag += 75;

            ehp -= damag * mr;
            fdc += damag * mr;
            dc+= damag;
            }
        }*/

            //string strNumber = System.Convert.ToString(mdc);
            //txt.DrawText(null, strNumber, 5, 190, Color.Firebrick);

            //Drawing.DrawText(strNumber, new Vector2(100,100),new Vector2(100, 50),Color.White,FontFlags.AntiAlias | FontFlags.DropShadow);

            //3412


            //enemy = ObjectMgr.GetEntities<Hero>().Where(obj => (obj.Team != me.Team && obj.IsAlive && obj.IsVisible && !obj.IsIllusion && !obj.IsMagicImmune())).ToList();
            if (Drawing.Direct3DDevice9 == null || Drawing.Direct3DDevice9.IsDisposed || !Game.IsInGame) { return; }
            /*string texturename = "";//materials/ensage_ui/<folder_name>/<texture_name>.vmat
            
            texturename = "materials/ensage_ui/items/tango.vmat";
            Drawing.DrawRect(new Vector2(100, 0), new Vector2(100, 100), Drawing.GetTexture(texturename));
            //texturename = "D:/GML/GMF_Vesna.png";//"NyanUI/items/item_tango";
            //Drawing.DrawRect(new Vector2(100, 100), new Vector2(100, 100), Drawing.GetTexture(texturename));
            texturename = "materials/ensage_ui/items/tango.vmat_c";//"NyanUI/items/item_tango.vmt";
            Drawing.DrawRect(new Vector2(100, 200), new Vector2(100, 100), Drawing.GetTexture(texturename));
            texturename = "C:/Program Files (x86)/Steam/steamapps/common/dota 2 beta/game/dota/materials/ensage_ui/items/tango";
            Drawing.DrawRect(new Vector2(100, 300), new Vector2(100, 100), Drawing.GetTexture(texturename));

            texturename = "C:/Program Files (x86)/Steam/steamapps/common/dota 2 beta/game/dota/materials/ensage_ui/items/tango.vmat";
            Drawing.DrawRect(new Vector2(100, 400), new Vector2(100, 100), Drawing.GetTexture(texturename));
            texturename = "C:/Program Files (x86)/Steam/steamapps/common/dota 2 beta/dota/materials/NyanUI/items/item_tango.vmt";
            Drawing.DrawRect(new Vector2(100, 500), new Vector2(100, 100), Drawing.GetTexture(texturename));
            texturename = "C:/Program Files (x86)/Steam/steamapps/common/dota 2 beta/dota/materials/NyanUI/items/item_tango.vtf";
            Drawing.DrawRect(new Vector2(100, 600), new Vector2(100, 100), Drawing.GetTexture(texturename));
            texturename = "items/item_tango";
            Drawing.DrawRect(new Vector2(700, 0), new Vector2(100, 100), Drawing.GetTexture(texturename));
            texturename = "items/item_tango.vtf";
            Drawing.DrawRect(new Vector2(800, 0), new Vector2(100, 100), Color.White);
            */
            for (uint i = 0; i < 10; i += 1)
            {
                phero[i] = ObjectMgr.GetPlayerById(i).Hero;
                var h = phero[i];

                //string text = "";

                if (h != null)
                {
                    if (h != null &&/*h.Team != me.Team &&*/ h.IsAlive && h.IsVisible/* && !h.IsIllusion && !h.IsMagicImmune()*/)
                    {
                        //var player = ObjectMgr.LocalPlayer;            
                        //if (player == null || player.Team == Team.Observer/*|| me.ClassID != ClassID.CDOTA_Unit_Hero_Morphling*/) { return; }
                        //string strNumber = System.Convert.ToString(bse);

                        ppos[i] = new Vector2(HUDInfo.GetTopPanelPosition(h).X, HUDInfo.GetTopPanelPosition(h).Y + 45);
                        for (int f = 0; f < 6; f += 1)
                        {
                            ptext[i * 6 + f] = "";
                            var item = h.Inventory.GetItem((ItemSlot)f);
                            if (item != null)
                            {
                                //ptext[i] += item.Name /*+ "\n"*//*+ '\n'*/; 
                                var text = item.Name;//item.Name;
                                text = text.Substring(5);
                                ptext[i * 6 + f] = text;
                                //Drawing.DrawRect(new Vector2(100, 100), new Vector2(100, 100), (DotaTexture)Drawing.GetTexture(item.TextureName));
                                //Drawing.DrawRect(new Vector2(100, 200), new Vector2(100, 100), (DotaTexture)Drawing.GetTexture(item.TextureName+".vtf"));

                            }

                        }
                    }
                    for (int f = 0; f < 6; f += 1)
                    {
                        if ((ptext[i * 6 + f].Length) != 0) { Drawing.DrawText(ptext[i * 6 + f], new Vector2(/*HUDInfo.GetTopPanelPosition(h).X*/ppos[i].X, /*HUDInfo.GetTopPanelPosition(h).Y + 45 +*/ppos[i].Y + 30 * f), new Vector2(/*(float)Math.Sqrt(*/20, 20), Color.White, FontFlags.AntiAlias); }
                    }
                    //Drawing.DrawText(/*ptext[i]*//*"Znach:"+strNumber"Vitos"*//*strNumber*//*+" Name: "+id.Name*/, new Vector2(HUDInfo.GetTopPanelPosition(h).X, HUDInfo.GetTopPanelPosition(h).Y + 40), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);
                }

            }
        }

        private static float point_distance(dynamic A, dynamic B)
        {
            if (!(A is Unit || A is Vector3)) throw new ArgumentException("Not valid parameters, Accepts Unit/Vector3 only", "A");
            if (!(B is Unit || B is Vector3)) throw new ArgumentException("Not valid parameters, Accepts Unit/Vector3 only", "B");
            if (A is Unit) { A = A.Position; }
            if (B is Unit) { B = B.Position; }
            return ((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y));
        }


    }
}

//new