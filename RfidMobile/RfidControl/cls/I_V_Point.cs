namespace RfidControl
{
    using System;

    public class I_V_Point
    {
        public double Current { get; set; }
        public double Voltage { get; set; }

        public I_V_Point()
        {
        }

        public I_V_Point(double dCurrent, double dVoltage)
        {
            this.Current = dCurrent; this.Voltage = dVoltage;
        }
    }
}
