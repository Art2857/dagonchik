
using System;
using System.Threading;
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
        private static readonly int[] poof = new int[5] { -1, -1, -1, -1, -1 };
        private static readonly int[] war = new int[5] { 0, 0, 0, 0, 0 };
        //private static readonly int[] ShitDickFuck = new int[5] { 600, 650, 700, 750, 800 };
        //private static Key KeyC = Key.D;

        //private static readonly int[] spellq = new int[4] { 100, 175, 250, 325 };//���� ������� ������
        //private static readonly int[] spellw = new int[4] { 600, 700, 800, 900 };//��������� ������� ������

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
            //double damag = 0;
            var dps = me.AttacksPerSecond * me.MinimumDamage;
            var Q = me.Spellbook.SpellQ;//setka
            var W = me.Spellbook.SpellW;//puff
            var E = me.Spellbook.SpellE;
            var D = me.Spellbook.SpellD;
            var R = me.Spellbook.SpellR;

            var tick = Environment.TickCount;

            var bx = 7000;
            var by = 7000;

            if (me.Team == Team.Radiant)
            {
                bx = -7000;
                by = -7000;
            }

            var meepo = ObjectMgr.GetEntities<Hero>().Where(a => (a.ClassID == ClassID.CDOTA_Unit_Hero_Meepo && a.Team == me.Team && a.IsAlive && !a.IsIllusion)).ToList();

            //if (enemy_poof.Count > 2) { }
            //�����
            float mindist = 99999;
            var minposmeepo = 0;//meepo[0];//new Vector3(bx, by, me.Position.Z); 
            for (var i = 0; i < meepo.Count; i += 1)//foreach (var a in meepo) 
            {
                Hero a = meepo[i];
                //var dd = me.Distance2D(a/*new Vector3(bx, by, a.Position.Z)*/);
                float dist = a.Distance2D(new Vector3(bx, by, 0));// me.Distance2D(a/*new Vector3(bx, by, a.Position.Z)*/);// point_distance(me, me);//
                if (dist < mindist) { mindist = dist; minposmeepo = i; } //.Position;
            }

            float minhp = 99999;
            var minhpmeepo = 0;//meepo[0];//new Vector3(bx, by, me.Position.Z); 
            for (var i = 0; i < meepo.Count; i += 1)//foreach (var a in meepo)
            {
                Hero a = meepo[i];
                float hp = a.Health;//Distance2D(new Vector3(bx, by, 0));
                if (hp < minhp) { minhp = hp; minhpmeepo = i; }
            }

            float maxhp = 0;
            var maxhpmeepo = 0;//meepo[0];//new Vector3(bx, by, me.Position.Z); 
            for (var i = 0; i < meepo.Count; i += 1)//foreach (var a in meepo)
            {
                Hero a = meepo[i];
                float hp = a.Health;//Distance2D(new Vector3(bx, by, 0));
                if (hp > maxhp) { maxhp = hp; maxhpmeepo = i; }
            }

            //HandleEffect(minposmeepo);

            var enemy_poof = ObjectMgr.GetEntities<Hero>().Where(obj => (obj.Team != me.Team && obj.IsAlive && obj.IsVisible && !obj.IsIllusion && !obj.IsMagicImmune())).ToList();

            var blink = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_blink")));
            var discord = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_recipe_veil_of_discord")));
            var ethereal = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_ethereal_blade")));
            var dagon = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_dagon")));
            var hex = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_sheepstick")));
            var orchid = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_orchid")));
            var bloodthorn = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_bloodthorn")));
            var travel = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_travel_boots")));
            //var travel1 = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_travel_boots_1")));
            //var travel2 = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_travel_boots_2")));
            var tp = me.Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_tpscroll")));






            //������         
            var nmf = 0;//number meepo fountain
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

            for (var i = 0; i < meepo.Count; i += 1)//foreach (var a in meepo)
            {
                Hero a = meepo[i];
                float hp = a.Health;
                if (a.Modifiers.Any(o => o.Name == "modifier_fountain_aura_buff") && a.Distance2D(new Vector3(bx, by, meepo[minposmeepo].Position.Z)) < 700 && Utils.SleepCheck("war" + i.ToString()))
                {
                    war[i] = 0;
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
                    war[i] = 1;
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

            //ai kills



            /*float mindistmtoe = 99999;
            var minposmtoe = 0;
            if (maxhpepmeepo != null)
            {
                for (var i = 0; i < meepo.Count; i += 1)//foreach (var a in meepo)
                {
                    Hero a = meepo[i];

                    float dist = a.Distance2D(maxhpepmeepo.Position);
                    if (dist < mindistmtoe) { mindistmtoe = dist; minposmtoe = i; }
                }
            }*/



            /*float minhpep = 99999;
            Hero maxhpepmeepo = null;
            foreach (var a in enemy_poof)
            {
                float hp = a.Health;//Distance2D(new Vector3(bx, by, 0));
                if (hp < minhpep) { minhpep = hp; maxhpepmeepo = a; }
            }*/

            Hero maxhpepmeepo = null;//enemy
            double mindistmtoe = 1000000;//priority
            var minposmtoe = -1;//meepo

            for (var i = 0; i < meepo.Count; i += 1)//foreach (var a in meepo)
            {
                Hero a = meepo[i];
                foreach (var b in enemy_poof)
                {
                    //double dist = a.Distance2D(b.Position);
                    double dist = ((b.Health / (1 - b.DamageResist) + b.Health / (1 - b.MagicDamageResist))) + Math.Pow(a.Distance2D(b.Position), 1.5);
                    if (dist < mindistmtoe) { mindistmtoe = dist; minposmtoe = i; maxhpepmeepo = b; }
                }
            }




            if (minposmtoe > -1 && maxhpepmeepo != null)
            {
                for (var i = 0; i < meepo.Count; i += 1)
                {
                    if (Utils.SleepCheck("event" + i.ToString()))
                    {
                    if (war[i] == 1)
                    {
                        if (Utils.SleepCheck("at" + i/*minposmtoe*/.ToString()) && nmf > 0)//minposmtoe > -1 && maxhpepmeepo!=null
                        {

                            //if (meepo[minposmtoe].Distance2D(maxhpepmeepo.Position) > 2000)
                            //{
                            //if //tp
                            //}          

                            if (meepo[i/*minposmtoe*/].Distance2D(maxhpepmeepo.Position) > 200)
                            {
                                var mblink = meepo[i/*minposmtoe*/].Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_blink")));
                                if (meepo[i/*minposmtoe*/].Distance2D(maxhpepmeepo.Position) < 1200 && mblink != null && mblink.Cooldown == 0)
                                {
                                    mblink.UseAbility(maxhpepmeepo.Position);
                                }
                                else
                                {
                                    if (me.Health == me.MaximumHealth && nmf > 1 && Utils.SleepCheck("w" + i.ToString()))
                                    {
                                        poof[0] = minposmtoe; Utils.Sleep(50, "pf" + i.ToString());
                                        Utils.Sleep(4500, "w" + i.ToString());
                                    }
                                }
                                //if (meepo[i/*minposmtoe*/].Distance2D(maxhpepmeepo.Position) > 1200)
                                //{
                                //    meepo[i/*minposmtoe*/].Move(maxhpepmeepo.Predict(500));
                                //}
                                //else
                                //{
                                meepo[i/*minposmtoe*/].Move(maxhpepmeepo.Position);
                                //}
                            }
                            else
                            {
                                meepo[i/*minposmtoe*/].Attack(maxhpepmeepo);
                                Utils.Sleep(meepo[i].SecondsPerAttack * 1000, "event" + i.ToString());
                            }


                            if (Utils.SleepCheck("q"))
                            {
                                if (meepo[i].Spellbook.SpellQ.Level > 0)
                                {
                                    if (!(maxhpepmeepo.Modifiers.Any(o => o.Name == "modifier_meepo_divided_we_stand")) && meepo[i].Distance2D(maxhpepmeepo.Position) < 500 + 250 * (meepo[i].Spellbook.SpellQ.Level - 1) && meepo[i].Spellbook.SpellQ.Cooldown == 0)
                                    {
                                        var pos = maxhpepmeepo.Predict(meepo[i].Distance2D(maxhpepmeepo.Position) / 857 * 1000);
                                        var dir = Math.Atan2(meepo[i].Position.Y-pos.Y, pos.X-meepo[i].Position.X);
                                        meepo[i].Spellbook.SpellQ.UseAbility(new Vector3(pos.X + Math.Cos(dir) * (500 + 250 * (meepo[i].Spellbook.SpellQ.Level - 1)), pos.Y + Math.Sin(dir) * (500 + 250 * (meepo[i].Spellbook.SpellQ.Level - 1)), meepo[minposmeepo].Position.Z));//pos);
                                        Utils.Sleep((500 + 250 * (meepo[i].Spellbook.SpellQ.Level - 1)) / 857 * 1000, "q");
                                        Utils.Sleep(300, "event" + i.ToString());
                                    }
                                }
                            }

                            Utils.Sleep(tick, "at" + i/*minposmtoe*/.ToString());
                        }
                    }
                }
            }
            }
            else
            {
                
                    //if (!(minposmtoe > -1 && maxhpepmeepo != null))
                    //{
                var creeps = ObjectMgr.GetEntities<Unit>().Where(creep => ((creep.ClassID == ClassID.CDOTA_BaseNPC_Creep_Lane || creep.ClassID == ClassID.CDOTA_BaseNPC_Creep_Siege) && (me.Team != creep.Team || (me.Team == creep.Team && creep.Health < creep.MaximumHealth / 2)))).ToList();//(me.Team != creep.Team && creep.Health < damag * (1 - creep.DamageResist))
                
                    for (var i = 0; i < meepo.Count; i += 1)//foreach (var a in meepo)
                    {
                        if (Utils.SleepCheck("at"+i.ToString()))
                        {
                        float mindistc = 99999;
                        Hero a = meepo[i];
                        Unit minposcreep=null;
                        foreach (var b in creeps)
                        {
                            float dist = a.Distance2D(b.Position);
                            if (dist < mindistc) { mindistc = dist; minposcreep = b; }
                        }
                        if (minposcreep != null) { a.Attack(minposcreep); Utils.Sleep(a.SecondsPerAttack * 1000, "at" + i.ToString()); } else {  Utils.Sleep(tick, "at" + i.ToString()); }
                        }

                    //}
                    }
            }
            
            /*
            for (var i = 0; i < meepo.Count; i += 1)
            {
                if (war[i] == 1)
                {

                }
            }*/

            float damag = 0;
            foreach (var a in meepo)
            {
                if (W.Level > 0) { damag += 80 + (W.Level - 1) * 20; }
            }

            if (ethereal != null)
            {
                double ethereal_damag = me.TotalStrength;
                if ((int)me.PrimaryAttribute == 1) { ethereal_damag = me.TotalAgility; }
                if ((int)me.PrimaryAttribute == 2) { ethereal_damag = me.TotalIntelligence; }
                ethereal_damag *= 2;
                ethereal_damag += 75;
            }
            else
            {
                double ethereal_damag = 0;
            }

            if (dagon != null) { double dagon_damag = 400 + 100 * (dagon.Level - 1); } else { double dagon_damage = 0; }





            /*if (nmf>1)
            {
            
            }*/

            if (war[minposmeepo] == 0/*meepo[minposmeepo].Modifiers.Any(o => o.Name == "modifier_fountain_aura_buff") && meepo[minposmeepo].Distance2D(new Vector3(bx, by, meepo[minposmeepo].Position.Z)) < 700*/ && Utils.SleepCheck("war" + meepo[minposmeepo].ToString()))//&& Utils.SleepCheck("bottle")//minposmeepo
            {
                if (minposmeepo != minhpmeepo)
                {
                    if (((meepo[minhpmeepo].Spellbook.SpellW.Cooldown == 0 && meepo[minhpmeepo].CanCast() && meepo[minhpmeepo].Spellbook.SpellW.CanBeCasted()) || (travel != null && meepo[minhpmeepo].Health < meepo[minhpmeepo].MaximumHealth * 0.3)) && Utils.SleepCheck("w" + minhpmeepo.ToString()))
                    {
                        //Utils.Sleep(2000, "w" + minhpmeepo.ToString());



                        var j = maxhpfmeepo;

                        //���� ��� � ���� �������� �� ���� ��� � ���
                        float maxhpfw = 0;
                        var maxhpfwmeepo = 0;
                        for (var i = 0; i < meepo.Count; i += 1)
                        {
                            Hero a = meepo[i];
                            float hp = a.Health;
                            if (war[i] == 0/*a.Modifiers.Any(o => o.Name == "modifier_fountain_aura_buff") && a.Distance2D(new Vector3(bx, by, a.Position.Z)) < 700*/ && Utils.SleepCheck("war" + i.ToString()) && hp > maxhpfw && meepo[i].Spellbook.SpellW.Cooldown == 0)
                            {
                                maxhpfw = hp;
                                maxhpfwmeepo = i;
                            }
                        }

                        //���� ���� �� ���� ���������� �� ���
                        for (var i = 0; i < meepo.Count; i += 1)
                        {
                            Hero a = meepo[i];
                            if (war[i] == 0/*a.Modifiers.Any(o => o.Name == "modifier_fountain_aura_buff") && a.Distance2D(new Vector3(bx, by, a.Position.Z)) < 700*/ && Utils.SleepCheck("war" + i.ToString()))
                            {
                                if (i != maxhpfmeepo) { j = i; break; }
                            }
                        }

                        if (meepo[maxhpfwmeepo].Health > meepo[minhpmeepo].Health && (meepo[minhpmeepo].Health < meepo[minhpmeepo].MaximumHealth * 0.3 || (meepo[maxhpfwmeepo].CanCast() && meepo[maxhpfwmeepo].Spellbook.SpellW.CanBeCasted() && meepo[j].CanCast() && meepo[j].Spellbook.SpellW.CanBeCasted())))
                        {
                            Utils.Sleep(4500, "w" + minhpmeepo.ToString());
                            if (nmf > 1)
                            {
                                //meepo[maxhpfwmeepo].Stop();
                                poof[maxhpfwmeepo] = minhpmeepo; Utils.Sleep(tick, "pf" + maxhpfwmeepo.ToString()); //meepo[maxhpfwmeepo].Spellbook.SpellW.UseAbility(meepo[minhpmeepo].Position);
                            }
                            //meepo[minhpmeepo].Stop();
                            poof[minhpmeepo] = j; Utils.Sleep(500, "pf" + minhpmeepo.ToString());//meepo[minhpmeepo].Spellbook.SpellW.UseAbility(meepo[j].Position);
                        }
                        else
                        {
                            Utils.Sleep(500, "w" + minhpmeepo.ToString());
                        }



                    }
                    else { }//������� �� ��
                }
            }
            else
            {
                if (nmf == 0 && Utils.SleepCheck("m"))
                {
                    Utils.Sleep(tick+100, "m");
                    meepo[minposmeepo].Move(new Vector3(bx, by, meepo[minposmeepo].Position.Z));
                }
                //���� ���� ��, ������ �� �� ����, ���� ���, �� ���� ������ ����������� Meepo
            }

            for (var i = 0; i < meepo.Count; i += 1)
            {
                if (Utils.SleepCheck("event" + i.ToString()))
                {
                    if ((poof[i] >= 0) && Utils.SleepCheck("pf" + i.ToString()))
                    {
                        meepo[minhpmeepo].Stop();
                        if (meepo[i].Spellbook.SpellW.Cooldown == 0 && meepo[i].CanCast() && meepo[i].Spellbook.SpellW.CanBeCasted())
                        {
                            meepo[i].Spellbook.SpellW.UseAbility(meepo[poof[i]].Position);
                            Utils.Sleep(2000/*2500*/, "war" + i.ToString());
                            poof[i] = -1; Utils.Sleep(2000/*2500*/, "stp" + i.ToString());
                            Utils.Sleep(2000, "event" + i.ToString());
                        }
                        /*else
                        {
                            var mtp = meepo[i].Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_travel_boots")));
                            if (mtp == null)
                            {
                                mtp = meepo[i].Inventory.Items.FirstOrDefault(item => (item.Name.Contains("item_tpscroll")));
                                poof[i] = -1; Utils.Sleep(5000, "stp" + i.ToString());
                            }
                            if (mtp != null)
                            {
                                mtp.UseAbility(meepo[poof[i]].Position);
                                Utils.Sleep(2500, "war" + i.ToString());
                                poof[i] = -1; Utils.Sleep(5000, "stp" + i.ToString());
                            }
                        }*/

                    }
                    if (poof[i] == -1 && Utils.SleepCheck("stp" + i.ToString()))
                    {

                        if (meepo[i].Distance2D(new Vector3(bx, by, meepo[i].Position.Z)) < 2000) { meepo[i].Move(new Vector3(bx, by, meepo[i].Position.Z)); } else { meepo[i].Stop(); }

                        poof[i] = -100;
                    }
                }
            }

            /*var mousePosition = Game.MousePosition;
            //me.Move(mousePosition);

            //HeroPickState.

            var closestToMouse = me.ClosestToMouseTarget(128);
            if (closestToMouse != null){var target = me.ClosestToMouseTarget(range);}*/

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
            var size = new Vector2(20, 20);
            var colour = Color.White;
            var font = FontFlags.AntiAlias;
            Drawing.DrawText("Overall: " + meepo.Count.ToString(), new Vector2(200, 200), size, colour, font);
            Drawing.DrawText("min hp:" + minhp.ToString(), new Vector2(200, 250), size, colour, font); Drawing.DrawText("number meepo min hp:" + minhpmeepo.ToString(), new Vector2(350, 250), size, colour, font);
            Drawing.DrawText("max hp:" + maxhp.ToString(), new Vector2(200, 300), size, colour, font); Drawing.DrawText("number meepo max hp:" + maxhpmeepo.ToString(), new Vector2(350, 300), size, colour, font);
            Drawing.DrawText("min dist to b:" + mindist.ToString(), new Vector2(200, 350), size, colour, font); Drawing.DrawText("min dist to b:" + minposmeepo.ToString(), new Vector2(350, 350), size, colour, font);

            Drawing.DrawText("Fountain(Base): " + nmf.ToString(), new Vector2(600, 200), size, colour, font);
            Drawing.DrawText("min hp:" + minhpf.ToString(), new Vector2(600, 250), size, colour, font); Drawing.DrawText("number meepo min hp:" + minhpfmeepo.ToString(), new Vector2(750, 250), size, colour, font);
            Drawing.DrawText("max hp:" + maxhpf.ToString(), new Vector2(600, 300), size, colour, font); Drawing.DrawText("number meepo max hp:" + maxhpfmeepo.ToString(), new Vector2(750, 300), size, colour, font);

            Drawing.DrawText("In War: " + nmw.ToString(), new Vector2(1000, 200), size, colour, font);
            Drawing.DrawText("min hp:" + minhpw.ToString(), new Vector2(1000, 250), size, colour, font); Drawing.DrawText("number meepo min hp:" + minhpwmeepo.ToString(), new Vector2(1150, 250), size, colour, font);
            Drawing.DrawText("max hp:" + maxhpw.ToString(), new Vector2(1000, 300), size, colour, font); Drawing.DrawText("number meepo max hp:" + maxhpwmeepo.ToString(), new Vector2(1150, 300), size, colour, font);
            //Drawing.DrawText("min dist to b:"+mindistw.ToString(), new Vector2(700, 350), new Vector2(20, 20), Color.White, FontFlags.AntiAlias);
            
            /*
            Drawing.DrawText("Enemies: " + enemy_poof.Count.ToString(), new Vector2(1400, 200), size, colour, font);
            Drawing.DrawText("min hp e:" + minhpep.ToString(), new Vector2(1400, 250), size, colour, font);//Drawing.DrawText("number meepo min hp:" + minhpwmeepo.ToString(), new Vector2(1150, 250), size, colour, font);
            Drawing.DrawText("min dist toe:" + mindistmtoe.ToString(), new Vector2(1400, 300), size, colour, font); Drawing.DrawText("number meepo min dist toe:" + minposmtoe.ToString(), new Vector2(1600, 300), size, colour, font);
            */

            //var mindistmtoe = 99999;
            //var minposmtoe = 0;

            //float maxhpep = 0;
            //Hero maxhpepmeepo = meepo[0];

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

