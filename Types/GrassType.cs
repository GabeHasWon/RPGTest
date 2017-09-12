using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTest.Types
{
    public class GrassType : MonsterType
    {
        public static GrassType instance = new GrassType();

        public override string GetName()
        {
            return "Grass";
        }

        public override bool StrongTo(MonsterType type)
        {
            return (type is ElectricType || type is RockType);
        }

        public override bool WeakTo(MonsterType type)
        {
            if (type is FlyingType || type is BugType)
                return true;
            return false;
        }

        public override bool Ineffective(MonsterType type)
        {
            return (type is FlyingType || type is GrassType || type is BugType);
        }

        public override bool Effective(MonsterType type)
        {
            return (type is RockType);
        }
    }
}
