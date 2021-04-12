using System;
using System.Collections.Generic;
using System.Text;

namespace Patterns
{
    class Observer
    {     
        public Observer()
        {
            TVChannel abc = new ABC();
            TVChannel cbs = new CBS();
            TVChannel fox = new Fox();
            ObserverExample observer = new ObserverExample();
        }
    }

    internal class ObserverExample
    {
        public ObserverExample()
        {
            WeatherStation weatherStation = new WeatherStation(GetTemperature());
            weatherStation.Channels = new List<TVChannel>
            {
                new ABC(),
                new CBS(),
                new Fox()
            };
            weatherStation.NotifyTemperatureChange();
            weatherStation.SetTemperature(GetTemperature());

        }

        private int GetTemperature()
        {
            return new Random().Next(-20, 110);
        }
    }

    internal class WeatherStation
    {
        private int temperature;
        public WeatherStation(int temperature)
        {
            this.temperature = temperature;
        }

        public List<TVChannel> Channels { get; set; }

        public void NotifyTemperatureChange()
        {
            foreach (var tvChannel in Channels)
            {
                tvChannel.Update(temperature);
            }
        }

        public void SetTemperature(int temperature)
        {
            this.temperature = temperature;
            NotifyTemperatureChange();
        }
    }

    internal class Fox : TVChannel
    {
        public override void Update(int temperature)
        {
            Console.WriteLine($"Fox has reported the temperature as: {temperature}");

        }
    }

    internal class CBS : TVChannel
    {
        public override void Update(int temperature)
        {
            Console.WriteLine($"CBS has reported the temperature as: {temperature}");
        }
    }

    internal class ABC : TVChannel
    {

    }

    internal abstract class TVChannel
    {
        public virtual void Update(int temperature)
        {
            Console.WriteLine($"The tempterature is: {temperature}");
        }
    }

}
