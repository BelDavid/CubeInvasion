using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    [Serializable]
    class Game_NotesCollector : Game
    {
        public Game_NotesCollector(Engine engine) : base(engine, "Notes Collector")
        {
            
        }

        private Note note;

        public override void Init()
        {
            base.Init();

            note = new Note(this, Util.GetRandCoordsInGF());
            SpawnEntity(note);
        }
        public override void InitGraphics(FormWindow formWindow)
        {
            base.InitGraphics(formWindow);
            formWindow.label_GameCounter.Text = "Score:";
        }

        public override void Tick()
        {
            if (note.Collided)
            {
                if (note.CollectedNotDestroyed)
                {
                    Counter += 1;

                    if (Counter <= 50) // Basic mob
                    {
                        for (int i = 0; i < Counter; i++)
                            SpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_red, 2, 1,
                                new LootSetup(Item_Heart, .1, .2, Counter / 5 + 1, null, new int[,] { { 30, 10 }, { 2, 2 }, { 5, 3 }, { 2, 1 }, { 1, 0 } })));
                    }
                    else if (Counter <= 100) // Regular mob
                    {
                        for (int i = 50; i < Counter; i++)
                            SpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_orange, 10, 3,
                                new LootSetup(Item_Heart, .2, .2, Counter / 5 + 1, null, new int[,] { { 40, 20 }, { 5, 2 }, { 10, 5 }, { 5, 2 }, { 1, 1 } })));
                    }
                    else // Hard mob
                    {
                        for (int i = 100; i < Counter; i++)
                            SpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_blue, 20, 6,
                                new LootSetup(Item_Heart, .3, .2, Counter / 5 + 1, null, new int[,] { { 50, 30 }, { 10, 5 }, { 15, 10 }, { 10, 5 }, { 2, 1 } })));
                    }
                    
                    if (Counter % 5 == 0) // Multi mob 2x2
                        SpawnEntity(new MultiMob(mm_2x2_ID + mm_2x2_IDnum++, this, Util.GetRandCoordsInGF(), GGraphics.multimob_red, 50, 10, occupiedSpace_2x2,
                                new LootSetup(Item_Heart_2, .2, .4, Counter / 5 + 1, null, new int[,] { { 30, 30 }, { 2, 1 }, { 5, 3 }, { 2, 1 }, { 1, 0 } })));
                    if (Counter % 25 == 0) // Multi mob 4T
                        SpawnEntity(new MultiMob(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.multimob_orange, 200, 20, occupiedSpace_4T,
                                new LootSetup(Item_Heart_2, .2, .4, Counter / 5 + 1, null, new int[,] { { 30, 30 }, { 2, 1 }, { 5, 3 }, { 2, 1 }, { 1, 0 } })));
                }
                else
                {
                    if (Counter <= 50) // Basic penalty mob
                        SpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_purple, 10, 6,
                                new LootSetup(Item_Heart, 0, .1, 1, null, new int[,] { { 20, 10 } })));
                    else if (Counter <= 100) // Regular penalty mob
                        SpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_purple, 20, 10,
                                new LootSetup(Item_Heart, 0, .1, 1, null, new int[,] { { 30, 10 } })));
                    else // Hard penalty mob
                        SpawnEntity(new HostileMob_Basic(Entity.NewID, this, Util.GetRandCoordsInGF(), GGraphics.mob_purple, 50, 20,
                                new LootSetup(Item_Heart, 0, .1, 1, null, new int[,] { { 50, 10 } })));
                }

                // Respawn
                DespawnEntity(note);
                note.Coords = Util.GetRandCoordsInGF(note.GfLayer);
                note.Restore(this);
                SpawnEntity(note);
            }

            base.Tick();
        }

        protected override void EntityDespawned(Entity e)
        {
            base.EntityDespawned(e);

            if (e.ID.StartsWith(mm_2x2_ID)
                && int.TryParse(e.ID.Substring(mm_2x2_ID.Length), out int n)
                && n < (int)WeaponType.COUNT)
            {
                SpawnEntity(new WeaponPackage(Entity.NewID, this, new GFCoordinates(e.Coords.X, e.Coords.Y), (WeaponType)n));
            }
        }


        public static readonly byte[,] occupiedSpace_2x2 = new byte[,] { { 1, 1 }, 
                                                                         { 1, 1 } };
        public static readonly byte[,] occupiedSpace_4T = new byte[,] { { 0, 1, 1, 1, 0 },
                                                                        { 1, 2, 1, 2, 1 },
                                                                        { 1, 1, 1, 1, 1 },
                                                                        { 1, 2, 1, 2, 1 },
                                                                        { 0, 1, 1, 1, 0 } };

        protected int mm_2x2_IDnum = 1;
        protected string mm_2x2_ID = "mulmob_2x2_";
    }
}
