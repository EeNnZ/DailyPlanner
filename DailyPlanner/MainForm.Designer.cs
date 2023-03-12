namespace DailyPlanner
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            notifyIcon = new NotifyIcon(components);
            dataGridView = new DataGridView();
            createButton = new Button();
            modifyButton = new Button();
            deleteButton = new Button();
            markAsDoneButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // notifyIcon
            // 
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.Text = "notifyIcon";
            notifyIcon.Visible = true;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.BackgroundColor = SystemColors.ButtonFace;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView.Location = new Point(12, 12);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.RowTemplate.Height = 25;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(547, 287);
            dataGridView.TabIndex = 0;
            // 
            // createButton
            // 
            createButton.Location = new Point(12, 324);
            createButton.Name = "createButton";
            createButton.Size = new Size(94, 23);
            createButton.TabIndex = 1;
            createButton.Text = "Create";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += createButton_Click;
            // 
            // modifyButton
            // 
            modifyButton.Location = new Point(163, 324);
            modifyButton.Name = "modifyButton";
            modifyButton.Size = new Size(94, 23);
            modifyButton.TabIndex = 2;
            modifyButton.Text = "Modify";
            modifyButton.UseVisualStyleBackColor = true;
            modifyButton.Click += modifyButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(465, 324);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(94, 23);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // markAsDoneButton
            // 
            markAsDoneButton.Location = new Point(314, 324);
            markAsDoneButton.Name = "markAsDoneButton";
            markAsDoneButton.Size = new Size(94, 23);
            markAsDoneButton.TabIndex = 4;
            markAsDoneButton.Text = "Mark as done";
            markAsDoneButton.UseVisualStyleBackColor = true;
            markAsDoneButton.Click += markAsDoneButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(571, 353);
            Controls.Add(markAsDoneButton);
            Controls.Add(deleteButton);
            Controls.Add(modifyButton);
            Controls.Add(createButton);
            Controls.Add(dataGridView);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon notifyIcon;
        private DataGridView dataGridView;
        private Button createButton;
        private Button modifyButton;
        private Button deleteButton;
        private Button markAsDoneButton;
    }
}