
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
        //private static int nmb=0;//number meepo base

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
            //Vector3 pos;
            

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

            var bx = 7000;
            var by = 7000;

            if (me.Team == Team.Radiant)
            {
                bx = -7000;
                by = -7000;
            }
            
            var meepo = ObjectMgr.GetEntities<Hero>().Where(a => (a.ClassID==ClassID.CDOTA_Unit_Hero_Meepo && a.Team==me.Team && a.IsAlive && !a.IsIllusion)).ToList();

            //if (enemy_poof.Count > 2) { }
            //Общее
            float mindist = 99999;
            var minposmeepo = 0;//meepo[0];//new Vector3(bx, by, me.Position.Z); 
            for (var i = 0; i < meepo.Count; i += 1)//foreach (var a in meepo) 
            {
                Hero a=meepo[i];
                //var dd = me.Distance2D(a/*new Vector3(bx, by, a.Position.Z)*/);
                float dist = a.Distance2D(new Vector3(bx, by, 0));// me.Distance2D(a/*new Vector3(bx, by, a.Position.Z)*/);// point_distance(me, me);//
                if (dist < mindist) { mindist = dist; minposmeepo = i; } //.Position;
            }
            
            float minhp = 99999;
            var minhpmeepo = 0;//meepo[0];//new Vector3(bx, by, me.Position.Z); 
            for (var i = 0; i < meepo.Count; i += 1)//foreach (var a in meepo)
            {
                Hero a=meepo[i];
                float hp = a.Health;//Distance2D(new Vector3(bx, by, 0));
                if (hp < minhp) { minhp = hp; minhpmeepo = i; }
            }
            
            float maxhp = 0;
            var maxhpmeepo = 0;//meepo[0];//new Vector3(bx, by, me.Position.Z); 
            for (var i = 0; i < meepo.Count; i += 1)//foreach (var a in meepo)
            {
                Hero a=meepo[i];
                float hp = a.Health;//Distance2D(new Vector3(bx, by, 0));
                if (hp > maxhp) { maxhp = hp; maxhpmeepo = i; }
            }

            //HandleEffect(minposmeepo);
            
            var enemy_poof = ObjectMgr.GetEntities<Hero>().Where(obj => (obj.Team != me.Team && obj.IsAlive && obj.IsVisible && !obj.IsIllusion && !obj.IsMagicImmune())).ToList();

            var blink = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_blink")));
            
            //Фонтан         
            var nmf=0;//number meepo fountain
            var nmw = 0;//number meepo war
            float minhpf = 99999;
            var minhpfmeepo = 0;
            float maxhpf = 0;
            var maxhpfmeepo = 0;//meepo[0];

            float minhpw = 99999;
            var minhpwmeepo = 0;
            float maxhpw = 0;
            var maxhpwmeepo = 0;

            //float mindistw = 99999;
            //var minposwmeepo = 0;

            for (var i = 0; i < meepo.Count; i+=1 )//foreach (var a in meepo)
            {
                Hero a=meepo[i];
                float hp = a.Health;
                if (a.Modifiers.Any(o => o.Name == "modifier_fountain_aura_buff"))
                {
                    nmf += 1;
                    if (hp > maxhpf)
                    {
                        maxhpf = hp;
                        maxhpfmeepo = i;
                    }
                    if (hp < minhpf)
                    {
                        minhpf = hp;
                        minhpfmeepo = i;
                    }
                }
                else
                {
                    nmw += 1;
                    if (hp > maxhpw)
                    {
                        maxhpw = hp;
                        maxhpwmeepo = i;
                    }
                    if (hp < minhpw)
                    {
                        minhpw = hp;
                        minhpwmeepo = i;
                    }
                    //float dist = a.Distance2D(new Vector3(bx, by, 0));// me.Distance2D(a/*new Vector3(bx, by, a.Position.Z)*/);// point_distance(me, me);//
                    //if (dist < mindistw) { mindistw = dist; minposwmeepo = i; }
                }
            }

            if (nmf>1)
            {
            
            }

            if (meepo[minposmeepo].Modifiers.Any(o => o.Name == "modifier_fountain_aura_buff"))//&& Utils.SleepCheck("bottle")//minposmeepo
            {
                if (minposmeepo!= minhpmeepo)         
                {
                    if (meepo[minhpmeepo].Spellbook.SpellW.Cooldown == 0 && Utils.SleepCheck("w" + minhpmeepo.ToString()))
                    {
                        Utils.Sleep(1500, "w" + minhpmeepo.ToString());
                        
                        
                        
                            var j = maxhpfmeepo;

                            //ишем кем с базы заменить на того кто в бою
                            float maxhpfw = 0;
                            var maxhpfwmeepo = 0;
                            for (var i = 0; i < meepo.Count; i += 1)
                            {
                                Hero a = meepo[i];
                                float hp = a.Health;
                                if (a.Modifiers.Any(o => o.Name == "modifier_fountain_aura_buff") && hp > maxhpfw && meepo[i].Spellbook.SpellW.Cooldown == 0)
                                {
                                    maxhpfw = hp;
                                    maxhpfwmeepo = i;
                                }
                            }

                            //ишем того на кого выпрыгнуть из боя
                            for (var i = 0; i < meepo.Count; i += 1)
                            {
                                Hero a = meepo[i];
                                if (a.Modifiers.Any(o => o.Name == "modifier_fountain_aura_buff"))
                                {
                                    if (i != maxhpfmeepo) { j = i; break; }
                                }
                            }

                            if (meepo[maxhpfwmeepo].Health > meepo[minhpmeepo].Health)
                            {
                                if (nmf > 1) { meepo[maxhpfwmeepo].Stop(); meepo[maxhpfwmeepo].Spellbook.SpellW.UseAbility(meepo[minhpmeepo].Position); }
                                meepo[minhpmeepo].Stop();
                                meepo[minhpmeepo].Spellbook.SpellW.UseAbility(meepo[j].Position);
                            }

                        

                    }
                    else { }//Улетаем на тп
                }   
            }
            else
            {
                if (nmf == 0)
                {
                    meepo[minposmeepo].Move(new Vector3(bx, by, meepo[minposmeepo].Position.Z));
                }
                //Если есть тп, делаем тп на базу, если нет, то ишем самого безопасного Meepo
            }
            
            
            //enemy_poof.Modifiers.Any(x => Ignore.Contains(x.Name));
            /*if (blink != null && Utils.SleepCheck("poof"))
            {
                if (blink.Cooldown == 0)
                {
            if (enemy_poof.Count > 0 && enemy_poof.Any())
            {
                var ins = enemy_poof[0];
                float minhp = 99999;
                foreach (var a in enemy_poof)
                {
                    if (!a.Modifiers.Any(x => Ignore.Contains(x.Name)))
                    {
                        var hp = a.Healt/(1-a.MagicDamageResist);//me.Distance2D(a);
                        if (hp < minhp) { minhp = hp; ins = a; }
                    }
                }
                if (mindist < 1200 && Q.Cooldown == 0 && W.Cooldown == 0)//blink.CastRange()
                {
                    Utils.Sleep(1500, "poof");
                    blink.UseAbility(ins.Position, true); Q.UseAbility(ins.Position, true);

                    var n = 0;
                    foreach (var a in meepo) 
                    {
                        n += 1;
                        var q = a.Spellbook.SpellQ;//setka
                        var w = a.Spellbook.SpellW;//puff

                        if (W.Cooldown == 0 && Utils.SleepCheck("w" + n.ToString())) { w.UseAbility(ins.Position); Utils.Sleep(1500, "w" + n.ToString()); }
                    } 
                }//me.Attack(a); Utils.Sleep(me.SecondsPerAttack * 1000, "attack"); 
            }
        }
        }*/
            
            //if (Drawing.Direct3DDevice9 == null || Drawing.Direct3DDevice9.IsDisposed) { return; }//me.Position[0]
            Drawing.DrawText("Overall: " + meepo.Count.ToString(), new Vector2(300, 200), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);
            Drawing.DrawText("min hp:"+minhp.ToString(), new Vector2(300, 250), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);
            Drawing.DrawText("map hp:"+maxhp.ToString(), new Vector2(300, 300), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);
            Drawing.DrawText("min dist to b:"+mindist.ToString(), new Vector2(300, 350), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);

            Drawing.DrawText("Fountain(Base): " + nmf.ToString(), new Vector2(500, 200), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);
            Drawing.DrawText("min hp:"+minhpf.ToString(), new Vector2(500, 250), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);
            Drawing.DrawText("max hp:"+maxhpf.ToString(), new Vector2(500, 300), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);

            Drawing.DrawText("In War: " + nmw.ToString(), new Vector2(700, 200), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);
            Drawing.DrawText("min hp:"+minhpw.ToString(), new Vector2(700, 250), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);
            Drawing.DrawText("max hp:"+maxhpw.ToString(), new Vector2(700, 300), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);
            //Drawing.DrawText("min dist to b:"+mindistw.ToString(), new Vector2(700, 350), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);

            //Drawing.DrawText("minposmeepo", HUDInfo.GetHPbarPositionX(minposmeepo), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);//new Vector2(HUDInfo.GetHPBarSizeX(enemy), HUDInfo.GetHpBarSizeY(enemy));
            //Drawing.DrawText("minhpmeepo", HUDInfo.GetHpBarSize(minhpmeepo), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);
            //Drawing.DrawText(me.Position[1], new Vector2(300, 350), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);
            

        }

        /*private static float point_distance(dynamic A, dynamic B)
        {
            if (!(A is Unit || A is Vector3)) throw new ArgumentException("Not valid parameters, Accepts Unit/Vector3 only", "A");
            if (!(B is Unit || B is Vector3)) throw new ArgumentException("Not valid parameters, Accepts Unit/Vector3 only", "B");
            if (A is Unit) { A = A.Position; }
            if (B is Unit) { B = B.Position; }
            return ((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y));//Math.Sqrt
        }*/

        /*private static void HandleEffect(Unit unit)
        {
            ParticleEffect effect;
            if (unit.IsAlive)// && unit.IsVisibleToEnemies
            {
                if (Visible.TryGetValue(unit, out effect)) return;

                unit.AddParticleEffect("particles/items2_fx/smoke_of_deceit_buff.vpcf");
                unit.AddParticleEffect("particles/items2_fx/shadow_amulet_active_ground_proj.vpcf");

                effect = unit.AddParticleEffect("particles/items_fx/aura_shivas.vpcf");

                Visible.Add(unit, effect);
            }
            else
            {
                if (!Visible.TryGetValue(unit, out effect)) return;
                effect.Dispose();
                Visible.Remove(unit);
            }
        }*/



    }
}

