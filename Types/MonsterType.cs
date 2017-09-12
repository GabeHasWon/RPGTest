namespace RPGTest.Types
{
    public abstract class MonsterType
    {
        public bool WeakType
        {
            get
            {
                return false;
            }
            set
            {
                WeakType = value;
            }
        }

        public abstract string GetName();

        public abstract bool WeakTo(MonsterType type);

        public abstract bool StrongTo(MonsterType type);

        public abstract bool Ineffective(MonsterType type);

        public abstract bool Effective(MonsterType type);

        public override string ToString()
        {
            return GetName();
        }
    }
}
