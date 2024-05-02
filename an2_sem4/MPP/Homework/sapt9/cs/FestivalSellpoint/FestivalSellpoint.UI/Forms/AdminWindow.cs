using FestivalSellpoint.Service;
using System;
using System.Windows.Forms;

namespace FestivalSellpoint.UI.Forms
{
    public partial class AdminWindow : AppForm
    {        
        public AdminWindow(IAppService appService) : base(appService)
        {            
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            var username = UsernameBox.Text;
            var password = PasswordBox.Text;
            var email = EmailBox.Text;
            try
            {
                AppService.RegisterAngajat(username, password, email);
                MessageBox.Show("Success!");                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ClearInputs();
            }
        }

        void ClearInputs()
        {
            UsernameBox.Text = PasswordBox.Text = EmailBox.Text = "";
        }
          
    }
}
