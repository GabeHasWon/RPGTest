using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTest.Types
{
    public class NormalType : MonsterType
    {
        public static NormalType instance = new NormalType();

        public override string GetName()
        {
            return "Normal";
        }

        public override bool StrongTo(MonsterType type)
        {
            return false;
        }

        public override bool WeakTo(MonsterType type)
        {

            return false;
        }

        public override bool Ineffective(MonsterType type)
        {
            return (type is RockType || type is GroundType);
        }

        public override bool Effective(MonsterType type)
        {
            return false;
        }
    }
}
