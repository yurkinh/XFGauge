using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace XFGauge
{
    public partial class MainPage : ContentPage
    {
        private Timer _timer;
        private Random RAND = new Random();

        public MainPage()
        {
            InitializeComponent();

            _timer = new Timer()
            {
                Interval = 2000 
            };
            //Trigger event every second      
            _timer.Elapsed += OnTimedEvent;

            _timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async() => await  AnimateProgress(RAND.Next(100)));            
        }

        async Task AnimateProgress(int progress)
        {
            if (progress==GaugeControl.Value)
            {
                return;
            }
            if (progress <= GaugeControl.Value)
            {
                for (int i = (int)GaugeControl.Value; i >= progress; i--)
                {
                    GaugeControl.Value = i;
                    await Task.Delay(2);
                }                
            }
            else
            {
                for (int i = (int)GaugeControl.Value; i <= progress; i++)
                {
                    GaugeControl.Value = i;
                    await Task.Delay(2);
                }                
            }            
            
        }
    }
}
