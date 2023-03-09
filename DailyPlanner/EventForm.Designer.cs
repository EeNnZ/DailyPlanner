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
            bodyLabel = new Label();
            label3 = new Label();
            dateTimeStartLabel = new Label();
            nameLabel = new Label();
            cancelButton = new Button();
            saveButton = new Button();
            dateTimeNotificationPicker = new DateTimePicker();
            dateTimeStartPicker = new DateTimePicker();
            bodyTextBox = new TextBox();
            nameTextBox = new TextBox();
            eventGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // eventGroupBox
            // 
            eventGroupBox.Controls.Add(bodyLabel);
            eventGroupBox.Controls.Add(label3);
            eventGroupBox.Controls.Add(dateTimeStartLabel);
            eventGroupBox.Controls.Add(nameLabel);
            eventGroupBox.Controls.Add(cancelButton);
            eventGroupBox.Controls.Add(saveButton);
            eventGroupBox.Controls.Add(dateTimeNotificationPicker);
            eventGroupBox.Controls.Add(dateTimeStartPicker);
            eventGroupBox.Controls.Add(bodyTextBox);
            eventGroupBox.Controls.Add(nameTextBox);
            eventGroupBox.Location = new Point(12, 12);
            eventGroupBox.Name = "eventGroupBox";
            eventGroupBox.Size = new Size(329, 310);
            eventGroupBox.TabIndex = 0;
            eventGroupBox.TabStop = false;
            eventGroupBox.Text = "Event";
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
            cancelButton.Location = new Point(248, 281);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(167, 281);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 4;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            // 
            // dateTimeNotificationPicker
            // 
            dateTimeNotificationPicker.Location = new Point(123, 108);
            dateTimeNotificationPicker.Name = "dateTimeNotificationPicker";
            dateTimeNotificationPicker.Size = new Size(200, 23);
            dateTimeNotificationPicker.TabIndex = 3;
            // 
            // dateTimeStartPicker
            // 
            dateTimeStartPicker.CausesValidation = false;
            dateTimeStartPicker.Location = new Point(123, 65);
            dateTimeStartPicker.Name = "dateTimeStartPicker";
            dateTimeStartPicker.Size = new Size(200, 23);
            dateTimeStartPicker.TabIndex = 2;
            // 
            // bodyTextBox
            // 
            bodyTextBox.Location = new Point(123, 151);
            bodyTextBox.Multiline = true;
            bodyTextBox.Name = "bodyTextBox";
            bodyTextBox.Size = new Size(200, 83);
            bodyTextBox.TabIndex = 1;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(123, 22);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(200, 23);
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
        private Button saveButton;
        private DateTimePicker dateTimeNotificationPicker;
        private DateTimePicker dateTimeStartPicker;
        private TextBox bodyTextBox;
        private TextBox nameTextBox;
    }
}