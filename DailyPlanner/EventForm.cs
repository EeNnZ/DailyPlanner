using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }
    }
}
