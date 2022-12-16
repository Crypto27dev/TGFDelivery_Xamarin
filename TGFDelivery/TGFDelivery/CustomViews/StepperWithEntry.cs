using System;
using Xamarin.Forms;

namespace TGFDelivery.CustomViews
{
    public class StepperWithEntry : StackLayout
    {
        Button PlusBtn;
        Button MinusBtn;
        Label TextLabel;
        Entry Entry;

        public static readonly BindableProperty TextProperty =
          BindableProperty.Create(
             propertyName: "Text",
              returnType: typeof(int),
              declaringType: typeof(StepperWithEntry),
              defaultValue: 0,
              defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty MinimumValueProperty =
            BindableProperty.Create("MinimumValue", typeof(int), typeof(StepperWithEntry), defaultValue: 0);

        public static readonly BindableProperty MaximumValueProperty =
            BindableProperty.Create("MaximumValue", typeof(int), typeof(StepperWithEntry), defaultValue: 10);

        public int Text
        {
            get { return (int)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); OnPropertyChanged(nameof(Text)); }
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
        public StepperWithEntry()
        {
            PlusBtn = new Button { WidthRequest = 20, HeightRequest = 20, ImageSource = "plus_btn.png", HorizontalOptions = LayoutOptions.Center };
            MinusBtn = new Button { WidthRequest = 20, HeightRequest = 20, ImageSource = "minus_btn.png", HorizontalOptions = LayoutOptions.Center };
            TextLabel = new Label
            {
                Text = "0",
                WidthRequest = 20,
                FontSize = 22,
                TextColor = Color.FromHex("#005980"),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            switch (Device.RuntimePlatform)
            {

                case Device.UWP:
                case Device.Android:
                    {
                        PlusBtn.BackgroundColor = Color.Transparent;
                        MinusBtn.BackgroundColor = Color.Transparent;
                        break;
                    }
                case Device.iOS:
                    {
                        PlusBtn.BackgroundColor = Color.DarkGray;
                        MinusBtn.BackgroundColor = Color.DarkGray;
                        break;
                    }
            }

            Orientation = StackOrientation.Horizontal;
            VerticalOptions = LayoutOptions.CenterAndExpand;
            PlusBtn.Clicked += PlusBtn_Clicked;
            MinusBtn.Clicked += MinusBtn_Clicked;

            TextLabel.SetBinding(Label.TextProperty, new Binding(nameof(Text), BindingMode.TwoWay, source: this));

            Children.Add(MinusBtn);
            Children.Add(TextLabel);
            Children.Add(PlusBtn);
        }

        // Avoid decimal values
        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            var text = ((Entry)sender).Text;
            if (string.IsNullOrEmpty(text) || text.Contains(","))
                this.Text = 0;
        }

        private void MinusBtn_Clicked(object sender, EventArgs e)
        {
            if (Text > MinimumValue)
                Text--;
            else if (Text == MinimumValue)
                Text = MinimumValue;
        }

        private void PlusBtn_Clicked(object sender, EventArgs e)
        {
            if (Text < MaximumValue)
                Text++;
        }
    }
}
