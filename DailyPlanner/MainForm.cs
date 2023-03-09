using Microsoft.Toolkit.Uwp.Notifications;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;

namespace DailyPlanner
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat e)
        {
            //TODO: Remove event from database or mark as done
        }

        protected override void OnLoad(EventArgs e)
        {
            ToastNotificationManagerCompat.OnActivated += ToastNotificationManagerCompat_OnActivated;
            base.OnLoad(e);
        }
    }
}