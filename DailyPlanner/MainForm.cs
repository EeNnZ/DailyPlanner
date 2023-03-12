using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Uwp.Notifications;
using PlannerCore;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;

namespace DailyPlanner
{
    public partial class MainForm : Form
    {
        EventManager _eventManager;
        public MainForm()
        {
            InitializeComponent();
            _eventManager = new EventManager();
            _eventManager.RecordChanged += EventManager_RecordChanged;
            LoadDatabaseEntriesToDataGridView();
            dataGridView.Columns["EventId"].Visible = false;
        }

        private void ToastNotificationManagerCompat_OnActivated(ToastNotificationActivatedEventArgsCompat e)
        {
            //TODO: Remove event from database or mark as done via toast notification
        }
        #region CRUD 
        private void createButton_Click(object sender, EventArgs e)
        {
            EventForm evForm = new();
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
            PlannedEvent newEvent = new(evForm.nameTextBox.Text,
                                        startDateTimeCombined,
                                        notificationDateTimeCombined,
                                        evForm.bodyTextBox.Text);

            _eventManager.Create(newEvent);
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            //TODO: Delete by name
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select at least 1 row to delete", "Error", MessageBoxButtons.OK);
                return;
            }

            string? evntNameToDelete = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            if (evntNameToDelete is null)
            {
                MessageBox.Show("Cell value is empty", "Error", MessageBoxButtons.OK);
                return;
            }
            bool deleted = _eventManager.Delete(evntNameToDelete);
            if (!deleted)
            {
                MessageBox.Show("Cannot find db item with given index", "Error", MessageBoxButtons.OK);
            }
            LoadDatabaseEntriesToDataGridView();
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select at least 1 row to modify", "Error", MessageBoxButtons.OK);
                return;
            }

            string? selectedName = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            if (selectedName is null) return;

            PlannedEvent? selectedEvent = _eventManager.Read()?.FirstOrDefault(ev => ev.Name == selectedName);
            if (selectedEvent == null)
            {
                MessageBox.Show("Cannot obtain event with given name", "Error", MessageBoxButtons.OK);
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

        private void markAsDoneButton_Click(object sender, EventArgs e)
        {
            string? selectedName = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            if (selectedName == null) return;

            PlannedEvent? selectedEvent = _eventManager.Read()?.FirstOrDefault(ev => ev.Name == selectedName);
            if (selectedEvent == null)
            {
                MessageBox.Show("Cannot obtain event with given name", "Error", MessageBoxButtons.OK);
                return;
            }
            PlannedEvent updated = new(selectedEvent.Name,
                                       selectedEvent.EventStartDateTime,
                                       selectedEvent.NotificationDateTime,
                                       selectedEvent.Body,
                                       isDone: true);
            _eventManager.Update(selectedEvent.Name, updated);
        }
        #endregion
        private void EventManager_RecordChanged(object? sender, EventArgs e)
        {
            LoadDatabaseEntriesToDataGridView();
        }

        private void LoadDatabaseEntriesToDataGridView()
        {
            using var context = new MainDbContext();
            context.PlannedEvents.Where(ev => !ev.IsDone).Load();
            dataGridView.DataSource = context.PlannedEvents.Local.ToBindingList();
        }
        protected override void OnLoad(EventArgs e)
        {
            ToastNotificationManagerCompat.OnActivated += ToastNotificationManagerCompat_OnActivated;
            base.OnLoad(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            //TODO: Dispose eventManager on form closed?
            base.OnClosed(e);
        }
    }
}