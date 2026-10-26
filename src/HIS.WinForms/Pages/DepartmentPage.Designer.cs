using System.Drawing;
using System.Windows.Forms;

namespace HIS.WinForms.Pages;

partial class DepartmentPage
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        DataGridViewCellStyle gridHeaderStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle gridDefaultStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle gridAltStyle = new DataGridViewCellStyle();

        headerPanel = new Panel();
        lblTitle = new Label();
        lblSubtitle = new Label();
        toolbar = new FlowLayoutPanel();
        _addButton = new Button();
        _editButton = new Button();
        _deleteButton = new Button();
        _refreshButton = new Button();
        _departmentsGrid = new DataGridView();
        colId = new DataGridViewTextBoxColumn();
        colName = new DataGridViewTextBoxColumn();
        colDescription = new DataGridViewTextBoxColumn();
        statusPanel = new Panel();
        _statusLabel = new Label();

        headerPanel.SuspendLayout();
        toolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_departmentsGrid).BeginInit();
        statusPanel.SuspendLayout();
        SuspendLayout();

        // 
        // headerPanel
        // 
        headerPanel.BackColor = Color.FromArgb(245, 247, 250);
        headerPanel.Controls.Add(lblTitle);
        headerPanel.Controls.Add(lblSubtitle);
        headerPanel.Dock = DockStyle.Top;
        headerPanel.Height = 72;
        headerPanel.Name = "headerPanel";

        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(23, 38, 59);
        lblTitle.Location = new Point(0, 4);
        lblTitle.Name = "lblTitle";
        lblTitle.Text = "Khoa và phòng";

        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 10F);
        lblSubtitle.ForeColor = Color.FromArgb(102, 117, 138);
        lblSubtitle.Location = new Point(2, 44);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Text = "Danh sách khoa và phòng đang quản lý trong bệnh viện.";

        // 
        // toolbar
        // 
        toolbar.BackColor = Color.FromArgb(245, 247, 250);
        toolbar.Controls.Add(_addButton);
        toolbar.Controls.Add(_editButton);
        toolbar.Controls.Add(_deleteButton);
        toolbar.Controls.Add(_refreshButton);
        toolbar.Dock = DockStyle.Top;
        toolbar.FlowDirection = FlowDirection.LeftToRight;
        toolbar.Height = 54;
        toolbar.Name = "toolbar";
        toolbar.Padding = new Padding(0, 8, 0, 8);
        toolbar.WrapContents = false;

        // 
        // _addButton
        // 
        _addButton.AutoSize = true;
        _addButton.BackColor = Color.FromArgb(22, 102, 177);
        _addButton.Cursor = Cursors.Hand;
        _addButton.FlatAppearance.BorderSize = 0;
        _addButton.FlatStyle = FlatStyle.Flat;
        _addButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        _addButton.ForeColor = Color.White;
        _addButton.Height = 34;
        _addButton.Margin = new Padding(0, 0, 10, 0);
        _addButton.Name = "_addButton";
        _addButton.Padding = new Padding(14, 0, 14, 0);
        _addButton.Text = "+ Thêm khoa/phòng";
        _addButton.Click += AddButton_Click;

        // 
        // _editButton
        // 
        _editButton.BackColor = Color.FromArgb(217, 119, 6);
        _editButton.Cursor = Cursors.Hand;
        _editButton.FlatAppearance.BorderSize = 0;
        _editButton.FlatStyle = FlatStyle.Flat;
        _editButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        _editButton.ForeColor = Color.White;
        _editButton.Height = 34;
        _editButton.Margin = new Padding(0, 0, 10, 0);
        _editButton.Name = "_editButton";
        _editButton.Text = "Sửa";
        _editButton.Width = 80;
        _editButton.Click += EditButton_Click;

        // 
        // _deleteButton
        // 
        _deleteButton.BackColor = Color.FromArgb(220, 38, 38);
        _deleteButton.Cursor = Cursors.Hand;
        _deleteButton.FlatAppearance.BorderSize = 0;
        _deleteButton.FlatStyle = FlatStyle.Flat;
        _deleteButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        _deleteButton.ForeColor = Color.White;
        _deleteButton.Height = 34;
        _deleteButton.Margin = new Padding(0, 0, 10, 0);
        _deleteButton.Name = "_deleteButton";
        _deleteButton.Text = "Xóa";
        _deleteButton.Width = 80;
        _deleteButton.Click += DeleteButton_Click;

        // 
        // _refreshButton
        // 
        _refreshButton.BackColor = Color.FromArgb(226, 232, 240);
        _refreshButton.Cursor = Cursors.Hand;
        _refreshButton.FlatAppearance.BorderSize = 0;
        _refreshButton.FlatStyle = FlatStyle.Flat;
        _refreshButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        _refreshButton.ForeColor = Color.FromArgb(30, 41, 59);
        _refreshButton.Height = 34;
        _refreshButton.Margin = new Padding(0, 0, 10, 0);
        _refreshButton.Name = "_refreshButton";
        _refreshButton.Text = "Làm mới";
        _refreshButton.Width = 100;
        _refreshButton.Click += RefreshButton_Click;

        // 
        // _departmentsGrid
        // 
        _departmentsGrid.AllowUserToAddRows = false;
        _departmentsGrid.AllowUserToDeleteRows = false;
        _departmentsGrid.AutoGenerateColumns = false;
        _departmentsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _departmentsGrid.BackgroundColor = Color.White;
        _departmentsGrid.BorderStyle = BorderStyle.None;
        _departmentsGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        _departmentsGrid.ColumnHeadersHeight = 38;
        _departmentsGrid.Dock = DockStyle.Fill;
        _departmentsGrid.EnableHeadersVisualStyles = false;
        _departmentsGrid.GridColor = Color.FromArgb(229, 234, 242);
        _departmentsGrid.MultiSelect = false;
        _departmentsGrid.Name = "_departmentsGrid";
        _departmentsGrid.ReadOnly = true;
        _departmentsGrid.RowHeadersVisible = false;
        _departmentsGrid.RowTemplate.Height = 34;
        _departmentsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        gridHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        gridHeaderStyle.BackColor = Color.FromArgb(240, 244, 250);
        gridHeaderStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        gridHeaderStyle.ForeColor = Color.FromArgb(23, 38, 59);
        gridHeaderStyle.Padding = new Padding(6, 0, 0, 0);
        gridHeaderStyle.SelectionBackColor = Color.FromArgb(240, 244, 250);
        gridHeaderStyle.SelectionForeColor = Color.FromArgb(23, 38, 59);
        gridHeaderStyle.WrapMode = DataGridViewTriState.False;
        _departmentsGrid.ColumnHeadersDefaultCellStyle = gridHeaderStyle;

        gridDefaultStyle.BackColor = Color.White;
        gridDefaultStyle.Font = new Font("Segoe UI", 9.5F);
        gridDefaultStyle.ForeColor = Color.FromArgb(51, 65, 85);
        gridDefaultStyle.Padding = new Padding(6, 0, 0, 0);
        gridDefaultStyle.SelectionBackColor = Color.FromArgb(234, 242, 255);
        gridDefaultStyle.SelectionForeColor = Color.FromArgb(16, 59, 128);
        _departmentsGrid.DefaultCellStyle = gridDefaultStyle;

        gridAltStyle.BackColor = Color.FromArgb(248, 250, 253);
        gridAltStyle.ForeColor = Color.FromArgb(51, 65, 85);
        gridAltStyle.SelectionBackColor = Color.FromArgb(234, 242, 255);
        gridAltStyle.SelectionForeColor = Color.FromArgb(16, 59, 128);
        _departmentsGrid.AlternatingRowsDefaultCellStyle = gridAltStyle;

        // Columns
        colId.DataPropertyName = "Id";
        colId.FillWeight = 10F;
        colId.HeaderText = "Mã khoa";
        colId.Name = "colId";
        colId.ReadOnly = true;

        colName.DataPropertyName = "Name";
        colName.FillWeight = 35F;
        colName.HeaderText = "Tên khoa";
        colName.Name = "colName";
        colName.ReadOnly = true;

        colDescription.DataPropertyName = "Description";
        colDescription.FillWeight = 55F;
        colDescription.HeaderText = "Mô tả";
        colDescription.Name = "colDescription";
        colDescription.ReadOnly = true;

        _departmentsGrid.Columns.AddRange(new DataGridViewColumn[] { colId, colName, colDescription });

        // 
        // statusPanel
        // 
        statusPanel.BackColor = Color.FromArgb(245, 247, 250);
        statusPanel.Controls.Add(_statusLabel);
        statusPanel.Dock = DockStyle.Bottom;
        statusPanel.Height = 30;
        statusPanel.Name = "statusPanel";

        // 
        // _statusLabel
        // 
        _statusLabel.AutoSize = true;
        _statusLabel.Font = new Font("Segoe UI", 9F);
        _statusLabel.ForeColor = Color.FromArgb(102, 117, 138);
        _statusLabel.Location = new Point(0, 6);
        _statusLabel.Name = "_statusLabel";
        _statusLabel.Text = "Sẵn sàng.";

        // 
        // DepartmentPage
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        Controls.Add(_departmentsGrid);
        Controls.Add(statusPanel);
        Controls.Add(toolbar);
        Controls.Add(headerPanel);
        Name = "DepartmentPage";
        Padding = new Padding(28);
        Size = new Size(950, 600);
        Load += DepartmentPage_Load;

        headerPanel.ResumeLayout(false);
        headerPanel.PerformLayout();
        toolbar.ResumeLayout(false);
        toolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_departmentsGrid).EndInit();
        statusPanel.ResumeLayout(false);
        statusPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel headerPanel;
    private Label lblTitle;
    private Label lblSubtitle;
    private FlowLayoutPanel toolbar;
    private Button _addButton;
    private Button _editButton;
    private Button _deleteButton;
    private Button _refreshButton;
    private DataGridView _departmentsGrid;
    private DataGridViewTextBoxColumn colId;
    private DataGridViewTextBoxColumn colName;
    private DataGridViewTextBoxColumn colDescription;
    private Panel statusPanel;
    private Label _statusLabel;
}
