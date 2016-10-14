
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

//using Attribute = Ensage.Attribute;

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

        //private static readonly Menu Menu = new Menu("Dagon Stealer", "dagonstealer", true);
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
        private static double maxbse;
        private static Hero maxid = ObjectMgr.LocalHero;

        private static double rep;
        //private static dynamic id = ObjectMgr.LocalHero;
        private static Hero id = ObjectMgr.LocalHero;
        //private static Vector3 prepos = ObjectMgr.LocalHero.Position;
        //private static Hero id;

        private static void Main(string[] args)
        {
            /*
            Menu.AddItem(new MenuItem("keyBind", "Main Key").SetValue(new KeyBind('H', KeyBindType.Toggle, true)));
            Menu.AddItem(new MenuItem("enemies", "Heroes").SetValue(new HeroToggler(enemies)));
            Menu.AddToMainMenu();
            */

            //Game.OnUpdate += Vagina;
            Drawing.OnDraw += Vagina;
        }

        private static void Vagina(EventArgs Tits)
        //internal void Vagina(EventArgs Tits)
        {

            //if (time > 0) { time -= 1; return; } else { time = 30; }

            var me = ObjectMgr.LocalHero;
            if (!Game.IsInGame) { id = me; bse = 0; rep = 0; return; }
            //if (!me.IsAlive) { hp = false; }
            if (me == null || !me.IsAlive || !Game.IsInGame || me.ClassID != ClassID.CDOTA_Unit_Hero_Meepo || Game.IsWatchingGame) { return; }
            double damag = 0;
            var dps = me.AttacksPerSecond * me.MinimumDamage;
            var Q = me.Spellbook.SpellQ;//setka
            var W = me.Spellbook.SpellW;//puff
            var E = me.Spellbook.SpellE;
            var D = me.Spellbook.SpellD;
            var R = me.Spellbook.SpellR;

            var meepo = ObjectMgr.GetEntities<Hero>().Where(a => (a.ClassID==ClassID.CDOTA_Unit_Hero_Meepo && a.Team==me.Team && a.IsAlive && !a.IsIllusion)).ToList();
            
            var enemy_poof = ObjectMgr.GetEntities<Hero>().Where(obj => (obj.Team != me.Team && obj.IsAlive && obj.IsVisible && !obj.IsIllusion && !obj.IsMagicImmune())).ToList();

            var blink = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_blink")));

            //enemy_poof.Modifiers.Any(x => Ignore.Contains(x.Name));
            if (blink != null && Utils.SleepCheck("poof"))
            {
                Utils.Sleep(10, "poof");
                if (blink.Cooldown == 0)
                {
            if (enemy_poof.Count > 0 && enemy_poof.Any())
            {
                var ins = enemy_poof[0];
                float mindist = 99999;
                foreach (var a in enemy_poof)
                {
                    var dist = me.Distance2D(a);
                    if (dist < mindist) { mindist = dist; ins = a; }
                }
                if (mindist < 1200 && Q.Cooldown == 0 && W.Cooldown == 0/*blink.CastRange()*/)
                { 
                    blink.UseAbility(ins.Position); Q.UseAbility(ins.Position); 
                                        
                    foreach (var a in meepo) 
                    {
                        var q = a.Spellbook.SpellQ;//setka
                        var w = a.Spellbook.SpellW;//puff

                        if (W.Cooldown == 0) { w.UseAbility(ins.Position); }
                    } 
                }//me.Attack(a); Utils.Sleep(me.SecondsPerAttack * 1000, "attack"); 
            }
        }
        }
            
            if (Drawing.Direct3DDevice9 == null || Drawing.Direct3DDevice9.IsDisposed) { return; }
            //Drawing.DrawText(me.ClassID, new Vector2(300, 300), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);

        }



    }
}

