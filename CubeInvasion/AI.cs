using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeInvasion
{
    static class AI
    {
        public static void Tick_HostileMob_Basic(Game game, Mob hostileMob)
        {
            int dX = game.PlayerPos.X - hostileMob.Coords.X,
                    dY = game.PlayerPos.Y - hostileMob.Coords.Y;
            int adX = Math.Abs(dX),
                adY = Math.Abs(dY);

            if (Util.rand.Next(adX + adY) < adX)
            {
                if (!game.MoveEntity(hostileMob, dX / adX, 0) && dY != 0)
                    game.MoveEntity(hostileMob, 0, dY / adY);
            }
            else
            {
                if (!game.MoveEntity(hostileMob, 0, dY / adY) && dX != 0)
                    game.MoveEntity(hostileMob, dX / adX, 0);
            }
        }
        public static void Tick_HostileMob_BowShooter(Game game, HostileMob_BowShooter bowShooter)
        {
            if (bowShooter.GetAmmo(WeaponType.Bow) > 0)
            {
                int dX = game.PlayerPos.X - bowShooter.Coords.X,
                        dY = game.PlayerPos.Y - bowShooter.Coords.Y;
                int adX = Math.Abs(dX),
                    adY = Math.Abs(dY);

                if (adX > adY || (adX == adY && Util.rand.Next(2) == 0))
                {
                    if (adY != 0)
                        game.MoveEntity(bowShooter, 0, dY / adY);
                    else if (game.CanSeeEachOther(bowShooter, game.player))
                        bowShooter.Fire(dX > 0 ? Direction.RIGHT : Direction.LEFT);
                    else
                        game.MoveEntity(bowShooter, dX / adX, 0);
                }
                else
                {
                    if (adX != 0)
                        game.MoveEntity(bowShooter, dX / adX, 0);
                    else if (game.CanSeeEachOther(bowShooter, game.player))
                        bowShooter.Fire(dY > 0 ? Direction.DOWN : Direction.UP);
                    else
                        game.MoveEntity(bowShooter, 0, dY / adY);
                }
            }
            else
            {
                Tick_HostileMob_Basic(game, bowShooter);
            }
        }
    }
}
