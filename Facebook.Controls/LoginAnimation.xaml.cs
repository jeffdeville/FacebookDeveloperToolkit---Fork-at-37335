using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Facebook.Controls
{
    public partial class LoginAnimation : UserControl
    {
        public EventHandler CompletedHandler;

        public LoginAnimation()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;
            LoadingAnimation.Completed += new EventHandler(LoadingAnimation_Completed);
        }

        void LoadingAnimation_Completed(object sender, EventArgs e)
        {
            if (CompletedHandler != null)
                CompletedHandler(this, e);
        }

        public void Start(double? repeat)
        {
            if (repeat != null)
                LoadingAnimation.RepeatBehavior = new RepeatBehavior(repeat.Value);

            this.Visibility = Visibility.Visible;
            LoadingAnimation.Begin();
        }
        public void Stop()
        {
            LoadingAnimation.Stop();
            this.Visibility = Visibility.Collapsed;
        }

    }
}
