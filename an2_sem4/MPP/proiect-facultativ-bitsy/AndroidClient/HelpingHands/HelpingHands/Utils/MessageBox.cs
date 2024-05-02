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
using System.Threading.Tasks;

namespace HelpingHands.Utils
{
    internal class MessageBox
    {
        public static Task<bool> Alert(Activity owner, string message, string title = "Info")
        {
            var tcs = new TaskCompletionSource<bool>();
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(owner);
            AlertDialog alert = dialog.Create();
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetButton("OK", (c, ev) => { tcs.SetResult(true); });
            //alert.SetButton2("CANCEL", (c, ev) => { });
            alert.Show();
            return tcs.Task;
        }
    }
}