using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Targil3
{
    class ViewModelDispatcher: DispatcherObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string url;
        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
                //StartCommand.RaiseCanExecuteChanged();

            }
        }
        private string _size;

        public string Size
        {
            get { return _size; }
            set
            {
                _size = value;
                OnPropertyChanged("Size");
            }
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            private set
            {
                _isBusy = value;
            }

        }
        public DelegateCommand StartCommand { get; set; }

        private void SafeInvoke(Action work)
        {
            if (Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                work.Invoke();
                return;
            }
            this.Dispatcher.BeginInvoke(work);
        }

        public ViewModelDispatcher()
        {
            IsBusy = false;
            Size = "Please Wait...";
            StartCommand = new DelegateCommand(() =>
            {


                Task.Run(() =>
                 {
                    
                         Action uiwork = () => {
                             Task task = WriteWebRequestSizeAsync(Url);
                             OnPropertyChanged("Url");
                         };
                         SafeInvoke(uiwork);
                         Thread.Sleep(250);
                    
                 });
               


               

            }, () => { return CanExecuteStartMethod(); });


            Task.Run(() =>
            {
                while (true)
                {
                    StartCommand.RaiseCanExecuteChanged(); // go check the enable/disable
                    Thread.Sleep(500);
                }

            });
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }


        static public string FormatBytes(long bytes)
        {
            string[] magnitudes =
                new string[] { "GB", "MB", "KB", "Bytes" };
            long max =
                (long)Math.Pow(1024, magnitudes.Length);

            return string.Format("{1:##.##} {0}",
                magnitudes.FirstOrDefault(
                    magnitude =>
                    bytes > (max /= 1024)) ?? "0 Bytes",
              (decimal)bytes / (decimal)max);
        }

        public async Task WriteWebRequestSizeAsync(string url)
        {
            IsBusy = true;
            WebRequest webRequest = WebRequest.Create(url);

            WebResponse response = await webRequest.GetResponseAsync();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string text = await reader.ReadToEndAsync();

                Size = FormatBytes(text.Length).ToString();

            }
            IsBusy = false;
        }
        private bool CanExecuteStartMethod()
        {
            string str = "http";

            if (Url != null && IsBusy == false)
            {
                return Url.StartsWith(str);
            }
            else if (Url != null && IsBusy == true)
            {
                return false;
            }
            else
            {
                return false;
            }

        }
    }
}
