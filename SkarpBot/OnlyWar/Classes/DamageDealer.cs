namespace SkarpBot.OnlyWar.Classes
{
    public class DamageDealer : WeaponValues
    {
        private int hittedPartId;
        private int d100Roll;
        private int[] modeBuffs = new int[] { 10, 0, -10 };
        private double degree;

        public DamageDealer(int accuracy, int mode, ulong targetId, bool additionalAim)
            : base(accuracy, mode, targetId, additionalAim)
        {
        }

        public int HittedPartId
        {
            get
            {
                return hittedPartId;
            }

            set
            {
                hittedPartId = value;
            }
        }

        public double Degree
        {
            get { return degree; }
        }

        public int D100Roll
        {
            get { return d100Roll; }
        }

        private int ThrowRoll100()
        {
            Random random = new Random();
            return random.Next(100);
        }

        public void CalculateShot()
        {
            d100Roll = ThrowRoll100();
            degree = CalculateDegree();
            hittedPartId = GetHittedPartId();
        }

        public double CalculateDegree()
        {
            int totalAccuracy = accuracy + (additionalAim ? 10 : 0) + modeBuffs[mode];
            return (totalAccuracy - d100Roll) / 10.0;
        }

        private int GetHittedPartId()
        {
            int d100Reversed = (d100Roll / 10) + ((d100Roll % 10) * 10);

            if (d100Reversed > 0 & d100Reversed < 11)
            {
                return 0;
            }

            if (d100Reversed > 10 & d100Reversed < 31)
            {
                if (d100Reversed > 20)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }

            if (d100Reversed > 30 & d100Reversed < 71)
            {
                return 1;
            }

            if (d100Reversed > 70 & d100Reversed < 100 || d100Reversed == 0)
            {
                if (d100Reversed == 0 || d100Reversed > 85)
                {
                    return 4;
                }
                else
                {
                    return 5;
                }
            }

            return 6;
        }
    }
}
