using DailyPlanner.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Uwp.Notifications;
using PlannerCore;

namespace DailyPlanner
{
    public partial class MainForm : Form
    {
        private EventManager _eventManager;
        public MainForm()
        {
            InitializeComponent();
            bool appSettingsExists = File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            if (!appSettingsExists)
            {
                var dialogResult = MessageBox.Show("appsettings.json not found. Create it now?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    JsonSettingsWriter.CreateAppSettingsJsonFile();
                }
                else
                {

                    Environment.Exit(-1);
                }
            }
            using var context = new MainDbContext();
            _eventManager = new EventManager(context.Configuration);
            _eventManager.RecordChanged += EventManager_RecordChanged;

            LoadDatabaseEntriesToDataGridView();
            dataGridView.Columns["EventId"].Visible = false;
        }

        private void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat e)
        {
            ToastArguments args = ToastArguments.Parse(e.Argument);
            if (!args.Contains("action")) return;

            string pickedAction = args["action"];
            string evntName = args["eventName"];
            if (pickedAction == "mark")
            {
                dataGridView.Invoke(() => MarkEventAsDone(evntName));
            }
            else if (pickedAction == "delete")
            {
                dataGridView.Invoke(() => DeleteEvent(evntName));
            }
        }

        #region CRUD methods
        private void CreateEvent(EventForm evForm)
        {
            DateTime startDateTimeCombined = evForm.dateStartPicker.Value.Date
                            .Add(evForm.timeStartPicker.Value.TimeOfDay);
            DateTime notificationDateTimeCombined = evForm.dateNotificationPicker.Value.Date
                .Add(evForm.timeNotificationPicker.Value.TimeOfDay);
            PlannedEvent newEvent = new(evForm.nameTextBox.Text,
                                        startDateTimeCombined,
                                        notificationDateTimeCombined,
                                        evForm.bodyTextBox.Text);

            _eventManager.Create(newEvent);
        }

        private void ModifyEvent(string? selectedName)
        {
            PlannedEvent? selectedEvent = _eventManager.Read()?.FirstOrDefault(ev => ev.Name == selectedName);
            if (selectedEvent == null)
            {
                MessageBox.Show("Cannot obtain event with given name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EventForm evForm = new(selectedEvent.Name,
                                   selectedEvent.EventStartDateTime,
                                   selectedEvent.NotificationDateTime,
                                   selectedEvent.Body);
            DialogResult result = evForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
            {
                evForm.Close();
                return;
            }

            DateTime startDateTimeCombined = evForm.dateStartPicker.Value.Date
                .Add(evForm.timeStartPicker.Value.TimeOfDay);
            DateTime notificationDateTimeCombined = evForm.dateNotificationPicker.Value.Date
                .Add(evForm.timeNotificationPicker.Value.TimeOfDay);
            PlannedEvent updated = new(evForm.nameTextBox.Text,
                                       startDateTimeCombined,
                                       notificationDateTimeCombined,
                                       evForm.bodyTextBox.Text);
            _eventManager.Update(selectedEvent.Name, updated);
        }

        private void MarkEventAsDone(string? selectedName)
        {
            PlannedEvent? selectedEvent = _eventManager.Read()?.FirstOrDefault(ev => ev.Name == selectedName);
            if (selectedEvent == null)
            {
                MessageBox.Show("Cannot obtain event with given name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PlannedEvent updated = new(selectedEvent.Name,
                                       selectedEvent.EventStartDateTime,
                                       selectedEvent.NotificationDateTime,
                                       selectedEvent.Body,
                                       isDone: true);
            _eventManager.Update(selectedEvent.Name, updated);
        }

        private bool DeleteEvent(string? selectedName)
        {
            return selectedName is not null && _eventManager.Delete(selectedName);
        }
        #endregion

        private void EventManager_RecordChanged(object? sender, EventArgs e) => LoadDatabaseEntriesToDataGridView();
        private void LoadDatabaseEntriesToDataGridView()
        {
            using var context = new MainDbContext();
            context.PlannedEvents.Where(ev => !ev.IsDone).Load();
            dataGridView.DataSource = context.PlannedEvents.Local.ToBindingList();
        }

        #region Button click handlers
        private void createButton_Click(object sender, EventArgs e)
        {
            EventForm evForm = new();
            DialogResult result = evForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
            {
                evForm.Close();
                return;
            }

            CreateEvent(evForm);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select at least 1 row to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string? selectedName = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            if (selectedName is null)
            {
                MessageBox.Show("Cell value is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool deleted = DeleteEvent(selectedName);
            if (!deleted)
            {
                MessageBox.Show("Cannot find db item with given index", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void markAsDoneButton_Click(object sender, EventArgs e)
        {
            string? selectedName = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            if (selectedName == null) return;
            MarkEventAsDone(selectedName);
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select at least 1 row to modify", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string? selectedName = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            if (selectedName is null) return;
            ModifyEvent(selectedName);
        }
        #endregion

        #region Overrides
        protected override void OnLoad(EventArgs e)
        {
            ToastNotificationManagerCompat.OnActivated += ToastNotificationManagerCompat_OnActivated;
            base.OnLoad(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
        #endregion
    }
}