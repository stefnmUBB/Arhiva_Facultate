using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpingHands.Validators
{
    internal interface IValidator<T>
    {
        void Validate(T obj);
    }
}