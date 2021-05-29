using System;

namespace DesktopApp
{
    public class PointCalculator
    {
        public PointCalculator()
        {
        }

        public int calculate(int correctAnswers, int timeLeft)
        {
            int anyPoints = 0;
            if (correctAnswers > 0)
            {
                anyPoints = 1;
            }

            return (correctAnswers * 7 + anyPoints * (2 * (timeLeft / 5)));
        }
    }
}
