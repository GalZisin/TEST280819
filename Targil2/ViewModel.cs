using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Targil2
{
    class ViewModel : DependencyObject, INotifyPropertyChanged
    {
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        bool isPressed = false;

        private bool isTimerReachZero;

        public bool IsTimerReachZero
        {
            get
            {
                return isTimerReachZero;
            }
            private set
            {
                isTimerReachZero = value;
            }

        }
     

        public bool IsValid=false;
    
     
        private int counter;
        public  int Counter
        {
            get
            {
                return counter;
            }
            set
            {
                counter = value;
                OnPropertyChanged("Counter");
            }
        }
        public DelegateCommand FirstBtnCommand { get; set; }
        public DelegateCommand SecondBtnCommand { get; set; }
        public DelegateCommand ThirdBtnCommand { get; set; }
        public DelegateCommand FourthBtnCommand { get; set; }
        public ViewModel()
        {
            IsTimerReachZero = false;

            //myWindowBackGround = new WindowBackGround { ColorBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("White")) };
            //myWindowBackGroundRed = new WindowBackGround { ColorBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF6347")) };
            //myWindowBackGroundGreen = new WindowBackGround { ColorBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("LightGreen")) };

            EventHandler ev = (o, e) =>
            {

                SetValue(isValidAnswerProperty, IsValid);
            };

            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal,
                ev, Dispatcher);

            Task task= RunTimerAsync();
        
            FirstBtnCommand = new DelegateCommand(() =>
            {
                IsValid = IsValidAnswer();
        
              
                flag1 = true;
            }, () => { return CanExecuteFirstBtnMethod(); });

            SecondBtnCommand = new DelegateCommand(() =>
            {


                IsValid = IsValidAnswer();
            

                flag2 = true;
            }, () => { return CanExecuteSecondBtnMethod(); });

            ThirdBtnCommand = new DelegateCommand(() =>
            {

                IsValid = IsValidAnswer();
         
                flag3 = true;
            }, () => { return CanExecuteThirdBtnMethod(); });

            FourthBtnCommand = new DelegateCommand(() =>
            {
                IsValid= IsValidAnswer();
  
                flag4 = true;
            }, () => { return CanExecuteFourthBtnMethod(); });

            Task.Run(() =>
            {
                while (true)
                {
                    FirstBtnCommand.RaiseCanExecuteChanged(); // go check the enable/disable
                    //Thread.Sleep(200);
                    SecondBtnCommand.RaiseCanExecuteChanged(); // go check the enable/disable
                    //Thread.Sleep(200);
                    ThirdBtnCommand.RaiseCanExecuteChanged(); // go check the enable/disable
                    //Thread.Sleep(200);
                    FourthBtnCommand.RaiseCanExecuteChanged(); // go check the enable/disable
                    Thread.Sleep(500);

                }

            });
     

        }
        public async Task RunTimerAsync()
        {
  
            for (int i = 30; i >= 0; i--)
            {
                if (flag1 == true || flag2 == true || flag3 == true || flag4 == true)
                {
                    isPressed = true;
                    break;
                }
                Counter = i;
            
              
                await Task.Run(() => { Thread.Sleep(1000); });
                await Task.Run(() => {
                    if (Counter == 0)
                    {
                        IsTimerReachZero = true;
                    }
                });
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public bool CanExecuteFirstBtnMethod()
        {
            if (IsTimerReachZero == true || isPressed==true)
            {
                return false;
            }
            return true;

            
        }
        public bool CanExecuteSecondBtnMethod()
        {
            if (IsTimerReachZero == true || isPressed==true)
            {
                return false;
            }
            return true;
        }
        public bool CanExecuteThirdBtnMethod()
        {
            if (IsTimerReachZero == true || isPressed == true)
            {
                return false;
            }
            return true;
        }

        public bool CanExecuteFourthBtnMethod()
        {
            if (IsTimerReachZero == true || isPressed==true)
            {
                return false;
            }
            return true;
        }

        public static readonly DependencyProperty isValidAnswerProperty =
          DependencyProperty.Register("isValidAnswer", typeof(bool), typeof(ViewModel), new PropertyMetadata(true));

        public bool isValidAnswer
        {
            get
            {
                return (bool)GetValue(isValidAnswerProperty);
            }
            set
            {
                SetValue(isValidAnswerProperty, value);
                OnPropertyChanged("isValidAnswer");
            }
        }


        public bool IsValidAnswer()
        {
            bool result=false;
            if (flag2 == true)
            {
                result = true;
            }
            else if (flag1 == true || flag3 == true || flag4 == true)
            {
                result = false;  //"#FF6347"
            }
            return result;
        }
    }

}
