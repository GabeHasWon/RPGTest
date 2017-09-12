using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTest.Types
{
    public class RockType : MonsterType
    {
        public static RockType instance = new RockType();

        public override string GetName()
        {
            return "Rock";
        }

        public override bool StrongTo(MonsterType type)
        {
            if (type is ElectricType)
                return true;
            return false;
        }

        public override bool WeakTo(MonsterType type)
        {
            return (type is GrassType);
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
