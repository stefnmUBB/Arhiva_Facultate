using FestivalSellpoint.Domain;
using FestivalSellpoint.Service;
using System.Windows.Forms;

namespace FestivalSellpoint.UI.Forms
{
    public partial class AngajatLoginForm : AppForm
    {        
        public AngajatLoginForm(IAppService appService) : base(appService)
        {            
            InitializeComponent();
        }

        public Angajat ConnectedAngajat = null;

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Text;
            ConnectedAngajat = AppService.LoginAngajat(username, password);

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
