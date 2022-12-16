using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpinBox : ContentView
    {
        public static readonly BindableProperty AmountValueProperty =
            BindableProperty.Create("AmountValue", typeof(int), typeof(SpinBox), defaultValue: 0, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty MinimumValueProperty =
            BindableProperty.Create("MinimumValue", typeof(int), typeof(SpinBox), defaultValue: 0);

        public static readonly BindableProperty MaximumValueProperty =
            BindableProperty.Create("MaximumValue", typeof(int), typeof(SpinBox), defaultValue: 10);

        public SpinBox()
        {
            InitializeComponent();
        }

        public int AmountValue
        {
            get { return (int)GetValue(AmountValueProperty); }
            set
            {
                _OrderValue.Text = Convert.ToString(value);
                SetValue(AmountValueProperty, value);
            }
        }

        public int MinimumValue
        {
            get { return (int)GetValue(MinimumValueProperty); }
            set { SetValue(MinimumValueProperty, value); }
        }

        public int MaximumValue
        {
            get { return (int)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }
        private void OnPlus(object sender, EventArgs e)
        {
            if (AmountValue < MaximumValue)
                AmountValue++;
        }

        private void OnMinus(object sender, EventArgs e)
        {
            if (AmountValue > MinimumValue)
                AmountValue--;
            else if (AmountValue == MinimumValue)
                AmountValue = MinimumValue;
        }
    }
}