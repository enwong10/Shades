using Prism.Commands;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Shades
{
    class MainViewModel : ViewModelBase
    {
        public double count { get; private set; }
        public double left { get; private set; }
        public double top { get; private set; }
        public Timer timer = new Timer(30000);
        public Random rand = new Random();
        public Random pointRand = new Random();

        private bool _startVisible;
        public bool startVisible
        {
            get { return _startVisible; }
            set { SetProperty(ref _startVisible, value); }
        }

        private bool _buttonVisible;
        public bool buttonVisible
        {
            get { return _buttonVisible; }
            set { SetProperty(ref _buttonVisible, value); }
        }

        private int _time = 30;
        public int time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }

        private int _oldTime = 30;
        public int oldTime
        {
            get { return _oldTime; }
            set { SetProperty(ref _oldTime, value); }
        }

        private bool _exitVisible;
        public bool exitVisible
        {
            get { return _exitVisible; }
            set { SetProperty(ref _exitVisible, value); }
        }

        private string _points;
        public string points
        {
            get { return _points; }
            set { SetProperty(ref _points, value); }
        }

        public Thickness _margin;
        public Thickness margin
        {
            get { return _margin; }
            set { SetProperty(ref _margin, value); }
        }

        private string _finalTime;
        public string finalTime
        {
            get { return _finalTime; }
            set { SetProperty(ref _finalTime, value); }
        }

        private string _pointsPerSec;
        public string pointsPerSec
        {
            get { return _pointsPerSec; }
            set { SetProperty(ref _pointsPerSec, value); }
        }

        private SolidColorBrush _buttonColour;
        public SolidColorBrush buttonColour
        {
            get { return _buttonColour; }
            set { SetProperty(ref _buttonColour, value); }
        }

        public ICommand StartCommand { get; private set; }
        public ICommand ButtonCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand RetryCommand { get; private set; }

        public MainViewModel()
        {
            init();
            StartCommand = new DelegateCommand(Start);
            ButtonCommand = new DelegateCommand(Button);
            ExitCommand = new DelegateCommand(Exit);
            RetryCommand = new DelegateCommand(Retry);
        }

        public void init()
        {
            startVisible = true;
            buttonVisible = false;
            exitVisible = false;
            count = 0;
        }

        public void Start()
        {
            startVisible = false;
            buttonVisible = true;


            if (time != oldTime)
            {
                timer = new Timer(time * 1000);
                oldTime = time;
            }

            timer.Start();
            timer.Enabled = true;
            timer.Elapsed += HandleTimer;

            genColour();
        }

        public void genColour()
        {
            left = rand.Next(1, 421);
            top = rand.Next(1, 395);
            margin = new Thickness(left, top, (445 - left), (420 - top));

            buttonColour = new SolidColorBrush(Color.FromRgb((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255)));
        }

        public void Button()
        {
            genColour();
            count++;
        }

        public void HandleTimer(object source, ElapsedEventArgs e)
        {
            timer.Stop();
            exitPage();
        }

        public void exitPage()
        {
            buttonVisible = false;
            exitVisible = true;
            points = "Points: " + count;
            finalTime = "Time:  " + time + " seconds";
            double pps = (count / time);
            pointsPerSec = "Points per second: " + pps.ToString("0.##");
        }

        public void Retry()
        {
            init();
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

    }
}
