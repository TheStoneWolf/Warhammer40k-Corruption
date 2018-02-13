﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;

namespace OHUShips
{
    public class JobGiver_LoadShipCargo : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            ShipBase ship = (ShipBase)pawn.mindState.duty.focus;

            if (LoadShipCargoUtility.HasJobOnShip(pawn, ship))
            {
                IntVec3 position = ship.RandomAdjacentCell8Way();
                Job job = LoadShipCargoUtility.JobLoadShipCargo(pawn, position, ship);
                if (job != null)
                {
                    //Log.Message("Giving job to " + pawn.Name);
                    return job;
                }                
            }
            return null;
        }
    }
}
