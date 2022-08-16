namespace SkarpBot.OnlyWar.Classes
{
    public class WeaponValues
    {
        protected int accuracy;
        protected int mode;
        protected ulong targetId;
        protected bool additionalAim;

        public WeaponValues(int accuracy, int mode, ulong targetId, bool additionalAim)
        {
            this.accuracy = accuracy;
            this.mode = mode;
            this.targetId = targetId;
            this.additionalAim = additionalAim;
        }

        public int Accuracy
        {
            get { return accuracy; }
        }

        public int Mode
        {
            get { return mode; }
        }

        public ulong TargetId
        {
            get { return targetId; }
        }

        public bool AdditionalAim
        {
            get { return additionalAim; }
        }
    }
}
