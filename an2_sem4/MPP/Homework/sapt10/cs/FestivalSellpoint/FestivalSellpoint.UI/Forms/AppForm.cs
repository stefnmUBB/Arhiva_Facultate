using FestivalSellpoint.Service;
using System.Windows.Forms;

namespace FestivalSellpoint.UI.Forms
{
    public class AppForm : Form
    {
        protected internal IAppService AppService;

        public AppForm() : this(null) { }
        public AppForm(IAppService appService = null)
        {
            AppService = appService;
        }
    }
}
