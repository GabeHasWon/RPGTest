using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTest.Types
{
    public class ElectricType : MonsterType
    {
        public override string GetName()
        {
            return "Electric";
        }

        public override bool StrongTo(MonsterType type)
        {

            return false;
        }

        public override bool WeakTo(MonsterType type)
        {
            if (type is RockType)
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
            return false;
        }
    }
}
