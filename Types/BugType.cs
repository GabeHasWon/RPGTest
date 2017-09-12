using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTest.Types
{
    public class BugType : MonsterType
    {
        public static BugType instance = new BugType();

        public override string GetName()
        {
            return "Bug";
        }

        public override bool StrongTo(MonsterType type)
        {
            return (type is GrassType);
        }

        public override bool WeakTo(MonsterType type)
        {
            return (type is RockType || type is FlyingType || type is GroundType);
        }

        public override bool Ineffective(MonsterType type)
        {
            return (type is RockType || type is FlyingType || type is GroundType);
        }

        public override bool Effective(MonsterType type)
        {
            return (type is GrassType);
        }
    }
}
