using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTest.Types
{
    public class WaterType : MonsterType
    {
        public static WaterType instance = new WaterType();

        public override string GetName()
        {
            return "Water";
        }

        public override bool StrongTo(MonsterType type)
        {
            return (type is RockType || type is GroundType);
        }

        public override bool WeakTo(MonsterType type)
        {
            return (type is GrassType || type is ElectricType);
        }

        public override bool Ineffective(MonsterType type)
        {
            if (type is GrassType)
                return true;
            return false;
        }

        public override bool Effective(MonsterType type)
        {
            return (type is FlyingType);
        }
    }
}
