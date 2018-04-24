using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using ExamenBanlinea.Models.DTOs;

namespace ExamenBanlinea.Views.Controls
{
    public partial class CountrySelector : ContentView
    {
        void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender != null)
            {
                int idx = (sender as Picker).SelectedIndex;
                if (Lst[idx] != null)
                {
                    if (Item.Country == null)
                        Item.Country = new Country();
                    Item.Country.Code = Lst[idx].Code;
                    Item.Country.Name = Lst[idx].Name;
                }
            }
        }

        public PhoneNumber Item
        {
            get { return GetValue(ItemProperty) as PhoneNumber; }
            set { base.SetValue(ItemProperty, value); }
        }
        public static readonly BindableProperty ItemProperty = BindableProperty.Create(propertyName: "Item", returnType: typeof(PhoneNumber), declaringType: typeof(CountrySelector), defaultValue: new PhoneNumber(), defaultBindingMode: BindingMode.TwoWay);

        public ObservableCollection<Countries> Lst
        {
            get { return GetValue(LstProperty) as ObservableCollection<Countries>; }
            set { base.SetValue(LstProperty, value); }
        }
        public static readonly BindableProperty LstProperty = BindableProperty.Create(propertyName: "Lst", returnType: typeof(ObservableCollection<Countries>), declaringType: typeof(CountrySelector), defaultValue: new ObservableCollection<Countries>(), defaultBindingMode: BindingMode.TwoWay, propertyChanged: LoadItems);

        private static void LoadItems(BindableObject bindable, object oldValue, object newValue)
        {
            var obj = bindable as CountrySelector;
            if (newValue != null)
            {
                foreach (Countries ph in (newValue as ObservableCollection<Countries>))
                    obj.pckCountries.Items.Add(ph.Name);
            }
        }

        public CountrySelector()
        {
            InitializeComponent();
        }
    }
}
