﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CityOfInfo.Data.Mids
{
    public class EnhancementEffect
    {
        public int Mode { get; set; }
        public int BuffMode { get; set; }
        public int Id { get; set; }
        public int SubId { get; set; }
        public int Schedule { get; set; }
        public float Multiplier { get; set; }
        public Effect Effect { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is EnhancementEffect enhancementEffect))
                return false;

            if (Effect == null && enhancementEffect.Effect != null)
                return false;

            if (Effect != null && enhancementEffect.Effect == null)
                return false;

            return Mode == enhancementEffect.Mode &&
                   BuffMode == enhancementEffect.BuffMode &&
                   Id == enhancementEffect.Id &&
                   SubId == enhancementEffect.SubId &&
                   Schedule == enhancementEffect.Schedule &&
                   Multiplier == enhancementEffect.Multiplier &&
                   Effect != null ? Effect.Equals(enhancementEffect.Effect) : true;
        }

        public override int GetHashCode()
        {
            var hashCode = 966821394;
            hashCode = hashCode * -1521134295 + Mode.GetHashCode();
            hashCode = hashCode * -1521134295 + BuffMode.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + SubId.GetHashCode();
            hashCode = hashCode * -1521134295 + Schedule.GetHashCode();
            hashCode = hashCode * -1521134295 + Multiplier.GetHashCode();
            if(Effect != null)
                hashCode = hashCode * -1521134295 + Effect.GetHashCode();
            return hashCode;
        }
    }
}
