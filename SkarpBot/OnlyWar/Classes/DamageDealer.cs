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

        public void ThrowRoll100()
        {
            Random random = new Random();
            d100Roll = random.Next(100);
        }

        public void CalculateShot()
        {
            ThrowRoll100();
            degree = CalculateDegree();
            GetHittedPartId();
        }

        public double CalculateDegree()
        {
            int totalAccuracy = accuracy + (additionalAim ? 10 : 0) + modeBuffs[mode];
            return (totalAccuracy - d100Roll) / 10.0;
        }

        public void GetHittedPartId()
        {
            int d100Reversed = (d100Roll / 10) + ((d100Roll % 10) * 10);

            if (d100Reversed > 0 & d100Reversed < 11)
            {
                hittedPartId = 0;
                return;
            }

            if (d100Reversed > 10 & d100Reversed < 31)
            {
                if (d100Reversed > 20)
                {
                    hittedPartId = 2;
                    return;
                }
                else
                {
                    hittedPartId = 3;
                    return;
                }
            }

            if (d100Reversed > 30 & d100Reversed < 71)
            {
                hittedPartId = 1;
                return;
            }

            if (d100Reversed > 70 & d100Reversed < 100 || d100Reversed == 0)
            {
                if (d100Reversed == 0 || d100Reversed > 85)
                {
                    hittedPartId = 4;
                    return;
                }
                else
                {
                    hittedPartId = 5;
                    return;
                }
            }

            hittedPartId = 6;
        }
    }
}
