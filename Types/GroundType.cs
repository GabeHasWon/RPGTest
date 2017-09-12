using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTest.Types
{
    public class GroundType : MonsterType
    {
        public static GroundType instance = new GroundType();

        public override string GetName()
        {
            return "Ground";
        }

        public override bool StrongTo(MonsterType type)
        {
            if (type is ElectricType || type is RockType)
                return true;
            return false;
        }

        public override bool WeakTo(MonsterType type)
        {
            return (type is GrassType);
        }

        public override bool Ineffective(MonsterType type)
        {
            return (type is GrassType);
        }

        public override bool Effective(MonsterType type)
        {
            return (type is RockType);
        }
    }
}
