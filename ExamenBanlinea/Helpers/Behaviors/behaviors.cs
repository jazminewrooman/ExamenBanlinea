using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ExamenBanlinea.Helpers.Behaviors
{
    public class EmailValidationBehavior : Behavior<Entry>     {
		protected override void OnAttachedTo(BindableObject bindable)
		{
			base.OnAttachedTo(bindable);
			bindable.BindingContextChanged += (sender, _) => this.BindingContext = ((BindableObject)sender).BindingContext;
		}

        public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(EmailValidationBehavior), false);
		public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
		public bool IsValid
		{
			get { return (bool)base.GetValue(IsValidProperty); }
			set { base.SetValue(IsValidPropertyKey, value); }
		}
		
        protected override void OnAttachedTo(Entry entry)         {             entry.TextChanged += OnEntryTextChanged;             base.OnAttachedTo(entry);         }         protected override void OnDetachingFrom(Entry entry)         {             entry.TextChanged -= OnEntryTextChanged;             base.OnDetachingFrom(entry);         }         void OnEntryTextChanged(object sender, TextChangedEventArgs args)         {             const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            if (!String.IsNullOrEmpty(args.NewTextValue))                 IsValid = (Regex.IsMatch(args.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            else
                IsValid = false;             ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;         }     }

    public class PhoneValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.BindingContextChanged += (sender, _) => this.BindingContext = ((BindableObject)sender).BindingContext;
        }

        public static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(PhoneValidationBehavior), false);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;
        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            set { base.SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }
        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }
        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            const string emailRegex = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
            if (!String.IsNullOrEmpty(args.NewTextValue))
                IsValid = (Regex.IsMatch(args.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            else
                IsValid = false;
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }
    }
}
