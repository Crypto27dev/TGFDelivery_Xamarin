﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGFDelivery.CustomViewCells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketViewCell : ViewCell
    {
        public BasketViewCell()
        {
            InitializeComponent();
        }
    }
}