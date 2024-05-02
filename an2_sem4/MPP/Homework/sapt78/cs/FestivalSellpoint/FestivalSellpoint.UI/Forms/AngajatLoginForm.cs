using FestivalSellpoint.Domain;
using FestivalSellpoint.Service;
using FestivalSellpoint.Service.Observer;
using System;
using System.Windows.Forms;

namespace FestivalSellpoint.UI.Forms
{
    public partial class AngajatLoginForm : AppForm
    {
        IObserver Client;

        public AngajatLoginForm(IAppService appService, IObserver client) : base(appService)
        {
            InitializeComponent();            
            Client = client;
        }

        public Angajat ConnectedAngajat = null;

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Text;
            try
            {
                ConnectedAngajat = AppService.LoginAngajat(username, password, Client);
            }
            catch
            {
                // Exception will be raised by ServiceObjsectProxy
                return;
            }

            if(ConnectedAngajat == null)
            {
                MessageBox.Show("Date de conectare incorecte");
                return;
            }

            DialogResult = DialogResult.OK;

            Close();
        }
    }
}
