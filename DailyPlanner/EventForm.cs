namespace DailyPlanner
{
    public partial class EventForm : Form
    {
        public EventForm()
        {
            InitializeComponent();
        }
        public EventForm(string name, DateTime dateStart, DateTime dateNotify, string? body)
            : this()
        {
            nameTextBox.Text = name;
            dateStartPicker.Value = dateStart.Date;
            timeStartPicker.Value = dateStart;
            dateNotificationPicker.Value = dateNotify.Date;
            timeNotificationPicker.Value = dateNotify;
            bodyTextBox.Text = body ?? string.Empty;

            //string timeFormat = "HH:mm tt";

            //timeStartPicker.Format = DateTimePickerFormat.Custom;
            //timeStartPicker.CustomFormat = timeFormat;

            //timeNotificationPicker.Format = DateTimePickerFormat.Custom;
            //timeNotificationPicker.CustomFormat = timeFormat;

        }
    }
}
