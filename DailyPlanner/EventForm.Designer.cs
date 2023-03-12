namespace DailyPlanner
{
    partial class EventForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            eventGroupBox = new GroupBox();
            timeNotificationPicker = new DateTimePicker();
            timeStartPicker = new DateTimePicker();
            bodyLabel = new Label();
            label3 = new Label();
            dateTimeStartLabel = new Label();
            nameLabel = new Label();
            cancelButton = new Button();
            saveButton = new Button();
            dateNotificationPicker = new DateTimePicker();
            dateStartPicker = new DateTimePicker();
            bodyTextBox = new TextBox();
            nameTextBox = new TextBox();
            eventGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // eventGroupBox
            // 
            eventGroupBox.Controls.Add(timeNotificationPicker);
            eventGroupBox.Controls.Add(timeStartPicker);
            eventGroupBox.Controls.Add(bodyLabel);
            eventGroupBox.Controls.Add(label3);
            eventGroupBox.Controls.Add(dateTimeStartLabel);
            eventGroupBox.Controls.Add(nameLabel);
            eventGroupBox.Controls.Add(cancelButton);
            eventGroupBox.Controls.Add(saveButton);
            eventGroupBox.Controls.Add(dateNotificationPicker);
            eventGroupBox.Controls.Add(dateStartPicker);
            eventGroupBox.Controls.Add(bodyTextBox);
            eventGroupBox.Controls.Add(nameTextBox);
            eventGroupBox.Location = new Point(12, 12);
            eventGroupBox.Name = "eventGroupBox";
            eventGroupBox.Size = new Size(329, 310);
            eventGroupBox.TabIndex = 0;
            eventGroupBox.TabStop = false;
            eventGroupBox.Text = "Event";
            // 
            // timeNotificationPicker
            // 
            timeNotificationPicker.Format = DateTimePickerFormat.Time;
            timeNotificationPicker.Location = new Point(198, 108);
            timeNotificationPicker.Name = "timeNotificationPicker";
            timeNotificationPicker.Size = new Size(94, 23);
            timeNotificationPicker.TabIndex = 11;
            // 
            // timeStartPicker
            // 
            timeStartPicker.Format = DateTimePickerFormat.Time;
            timeStartPicker.Location = new Point(198, 65);
            timeStartPicker.Name = "timeStartPicker";
            timeStartPicker.Size = new Size(94, 23);
            timeStartPicker.TabIndex = 10;
            // 
            // bodyLabel
            // 
            bodyLabel.AutoSize = true;
            bodyLabel.Location = new Point(6, 154);
            bodyLabel.Name = "bodyLabel";
            bodyLabel.Size = new Size(34, 15);
            bodyLabel.TabIndex = 9;
            bodyLabel.Text = "Body";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 111);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 8;
            label3.Text = "Notify At";
            // 
            // dateTimeStartLabel
            // 
            dateTimeStartLabel.AutoSize = true;
            dateTimeStartLabel.Location = new Point(6, 68);
            dateTimeStartLabel.Name = "dateTimeStartLabel";
            dateTimeStartLabel.Size = new Size(31, 15);
            dateTimeStartLabel.TabIndex = 7;
            dateTimeStartLabel.Text = "Start";
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(6, 25);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(39, 15);
            nameLabel.TabIndex = 6;
            nameLabel.Text = "Name";
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(248, 281);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            saveButton.DialogResult = DialogResult.OK;
            saveButton.Location = new Point(167, 281);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 4;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            // 
            // dateNotificationPicker
            // 
            dateNotificationPicker.CustomFormat = "";
            dateNotificationPicker.Format = DateTimePickerFormat.Short;
            dateNotificationPicker.Location = new Point(88, 108);
            dateNotificationPicker.Name = "dateNotificationPicker";
            dateNotificationPicker.Size = new Size(104, 23);
            dateNotificationPicker.TabIndex = 3;
            // 
            // dateStartPicker
            // 
            dateStartPicker.CausesValidation = false;
            dateStartPicker.CustomFormat = "";
            dateStartPicker.Format = DateTimePickerFormat.Short;
            dateStartPicker.Location = new Point(88, 65);
            dateStartPicker.Name = "dateStartPicker";
            dateStartPicker.Size = new Size(104, 23);
            dateStartPicker.TabIndex = 2;
            // 
            // bodyTextBox
            // 
            bodyTextBox.Location = new Point(88, 151);
            bodyTextBox.Multiline = true;
            bodyTextBox.Name = "bodyTextBox";
            bodyTextBox.Size = new Size(204, 83);
            bodyTextBox.TabIndex = 1;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(88, 22);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(204, 23);
            nameTextBox.TabIndex = 0;
            // 
            // EventForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(353, 334);
            Controls.Add(eventGroupBox);
            Name = "EventForm";
            Text = "EventForm";
            eventGroupBox.ResumeLayout(false);
            eventGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox eventGroupBox;
        private Label bodyLabel;
        private Label label3;
        private Label dateTimeStartLabel;
        private Label nameLabel;
        private Button cancelButton;
        internal protected DateTimePicker dateNotificationPicker;
        internal protected DateTimePicker dateStartPicker;
        internal protected TextBox bodyTextBox;
        internal protected TextBox nameTextBox;
        internal protected DateTimePicker timeStartPicker;
        internal protected DateTimePicker timeNotificationPicker;
        internal protected Button saveButton;
    }
}