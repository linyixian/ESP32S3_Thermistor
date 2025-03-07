using System;
using System.Device.Adc;
using System.Diagnostics;
using System.Threading;

namespace ESP32S3_Thermistor
{
    public class Program
    {
        public static void Main()
        {
            AdcController controller = new AdcController();
            AdcChannel channel = controller.OpenChannel(0);

            while (true)
            {
                int value = channel.ReadValue();
                double voltage = value / 4095.0 * 3.3;
                double rt = 10 * voltage / (3.3 - voltage);
                double tempk = (1.0 / (1.0 / (273.15 + 25.0) + (Math.Log(rt / 10.0)) / 3950.0));
                double tempc = tempk - 273.15;

                Debug.Write("value = " + value.ToString());
                Debug.Write(", voltage = " + voltage.ToString("F2"));
                Debug.WriteLine(", temp = " + tempc.ToString("F2"));

                Thread.Sleep(1000);
            }
        }
    }
}
