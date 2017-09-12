using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTest.Types
{
    public class FlyingType : MonsterType
    {
        public static FlyingType instance = new FlyingType();

        public override string GetName()
        {
            return "Flying";
        }

        public override bool StrongTo(MonsterType type)
        {
            return (type is BugType || type is GrassType);
        }

        public override bool WeakTo(MonsterType type)
        {
            if (type is RockType || type is ElectricType)
                return true;
            return false;
        }

        public override bool Ineffective(MonsterType type)
        {
            if (type is RockType)
                return true;
            return false;
        }

        public override bool Effective(MonsterType type)
        {
            return (type is BugType || type is GrassType);
        }
    }
}
