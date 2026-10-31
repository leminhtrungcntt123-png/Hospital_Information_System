using System.Drawing;
using System.Windows.Forms;

namespace HIS.WinForms.Pages;

partial class ScheduleSlotPage
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        headerPanel = new Panel();
        lblTitle = new Label();
        topPanel = new Panel();
        tlpTop = new TableLayoutPanel();
        grpGenerate = new GroupBox();
        lblFrom = new Label();
        _dtpFromGen = new DateTimePicker();
        lblTo = new Label();
        _dtpToGen = new DateTimePicker();
        lblDeptGen = new Label();
        _cboGenDept = new ComboBox();
        _btnGenerate = new Button();
        _lblGenResult = new Label();
        grpSearch = new GroupBox();
        lblDate = new Label();
        _dtpSearchDate = new DateTimePicker();
        lblShift = new Label();
        _cboSearchShift = new ComboBox();
        lblDeptSearch = new Label();
        _cboSearchDept = new ComboBox();
        _chkOnlyAvailable = new CheckBox();
        _btnSearch = new Button();
        _btnRefresh = new Button();
        _dgvSlots = new DataGridView();
        colId = new DataGridViewTextBoxColumn();
        colDoctorName = new DataGridViewTextBoxColumn();
        colDepartmentName = new DataGridViewTextBoxColumn();
        colRoomName = new DataGridViewTextBoxColumn();
        colTimeRangeText = new DataGridViewTextBoxColumn();
        colCapacity = new DataGridViewTextBoxColumn();
        colBookedCount = new DataGridViewTextBoxColumn();
        colAvailableCount = new DataGridViewTextBoxColumn();
        colStatus = new DataGridViewTextBoxColumn();
        statusPanel = new Panel();
        _lblGridSummary = new Label();
        headerPanel.SuspendLayout();
        topPanel.SuspendLayout();
        tlpTop.SuspendLayout();
        grpGenerate.SuspendLayout();
        grpSearch.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvSlots).BeginInit();
        statusPanel.SuspendLayout();
        SuspendLayout();
        // 
        // headerPanel
        // 
        headerPanel.BackColor = Color.FromArgb(245, 247, 250);
        headerPanel.Controls.Add(lblTitle);
        headerPanel.Dock = DockStyle.Top;
        headerPanel.Location = new Point(20, 16);
        headerPanel.Name = "headerPanel";
        headerPanel.Padding = new Padding(0, 4, 0, 8);
        headerPanel.Size = new Size(1010, 72);
        headerPanel.TabIndex = 0;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(18, 53, 91);
        lblTitle.Location = new Point(0, 4);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(497, 37);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Quản lý Lịch khám & Khung giờ (Slot)";
        lblTitle.UseMnemonic = false;
        // 
        // topPanel
        // 
        topPanel.Controls.Add(tlpTop);
        topPanel.Dock = DockStyle.Top;
        topPanel.Location = new Point(20, 88);
        topPanel.Name = "topPanel";
        topPanel.Padding = new Padding(0, 8, 0, 12);
        topPanel.Size = new Size(1010, 185);
        topPanel.TabIndex = 1;
        // 
        // tlpTop
        // 
        tlpTop.ColumnCount = 2;
        tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlpTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlpTop.Controls.Add(grpGenerate, 0, 0);
        tlpTop.Controls.Add(grpSearch, 1, 0);
        tlpTop.Dock = DockStyle.Fill;
        tlpTop.Location = new Point(0, 8);
        tlpTop.Name = "tlpTop";
        tlpTop.RowCount = 1;
        tlpTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlpTop.Size = new Size(1010, 165);
        tlpTop.TabIndex = 0;
        // 
        // grpGenerate
        // 
        grpGenerate.BackColor = Color.White;
        grpGenerate.Controls.Add(lblFrom);
        grpGenerate.Controls.Add(_dtpFromGen);
        grpGenerate.Controls.Add(lblTo);
        grpGenerate.Controls.Add(_dtpToGen);
        grpGenerate.Controls.Add(lblDeptGen);
        grpGenerate.Controls.Add(_cboGenDept);
        grpGenerate.Controls.Add(_btnGenerate);
        grpGenerate.Controls.Add(_lblGenResult);
        grpGenerate.Dock = DockStyle.Fill;
        grpGenerate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        grpGenerate.ForeColor = Color.FromArgb(18, 53, 91);
        grpGenerate.Location = new Point(0, 0);
        grpGenerate.Margin = new Padding(0, 0, 8, 0);
        grpGenerate.Name = "grpGenerate";
        grpGenerate.Padding = new Padding(12);
        grpGenerate.Size = new Size(497, 165);
        grpGenerate.TabIndex = 0;
        grpGenerate.TabStop = false;
        grpGenerate.Text = "  ⚡ SINH KHUNG GIỜ KHÁM TỰ ĐỘNG";
        grpGenerate.Enter += grpGenerate_Enter;
        // 
        // lblFrom
        // 
        lblFrom.AutoSize = true;
        lblFrom.Font = new Font("Segoe UI", 9F);
        lblFrom.ForeColor = Color.FromArgb(23, 32, 51);
        lblFrom.Location = new Point(14, 30);
        lblFrom.Name = "lblFrom";
        lblFrom.Size = new Size(65, 20);
        lblFrom.TabIndex = 0;
        lblFrom.Text = "Từ ngày:";
        // 
        // _dtpFromGen
        // 
        _dtpFromGen.Font = new Font("Segoe UI", 9F);
        _dtpFromGen.Format = DateTimePickerFormat.Short;
        _dtpFromGen.Location = new Point(85, 27);
        _dtpFromGen.Name = "_dtpFromGen";
        _dtpFromGen.Size = new Size(130, 27);
        _dtpFromGen.TabIndex = 1;
        // 
        // lblTo
        // 
        lblTo.AutoSize = true;
        lblTo.Font = new Font("Segoe UI", 9F);
        lblTo.ForeColor = Color.FromArgb(23, 32, 51);
        lblTo.Location = new Point(230, 30);
        lblTo.Name = "lblTo";
        lblTo.Size = new Size(75, 20);
        lblTo.TabIndex = 2;
        lblTo.Text = "Đến ngày:";
        // 
        // _dtpToGen
        // 
        _dtpToGen.Font = new Font("Segoe UI", 9F);
        _dtpToGen.Format = DateTimePickerFormat.Short;
        _dtpToGen.Location = new Point(310, 27);
        _dtpToGen.Name = "_dtpToGen";
        _dtpToGen.Size = new Size(130, 27);
        _dtpToGen.TabIndex = 3;
        // 
        // lblDeptGen
        // 
        lblDeptGen.AutoSize = true;
        lblDeptGen.Font = new Font("Segoe UI", 9F);
        lblDeptGen.ForeColor = Color.FromArgb(23, 32, 51);
        lblDeptGen.Location = new Point(14, 72);
        lblDeptGen.Name = "lblDeptGen";
        lblDeptGen.Size = new Size(46, 20);
        lblDeptGen.TabIndex = 4;
        lblDeptGen.Text = "Khoa:";
        // 
        // _cboGenDept
        // 
        _cboGenDept.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboGenDept.Font = new Font("Segoe UI", 9F);
        _cboGenDept.Location = new Point(85, 68);
        _cboGenDept.Name = "_cboGenDept";
        _cboGenDept.Size = new Size(355, 28);
        _cboGenDept.TabIndex = 5;
        // 
        // _btnGenerate
        // 
        _btnGenerate.BackColor = Color.FromArgb(22, 102, 177);
        _btnGenerate.Cursor = Cursors.Hand;
        _btnGenerate.FlatAppearance.BorderSize = 0;
        _btnGenerate.FlatStyle = FlatStyle.Flat;
        _btnGenerate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _btnGenerate.ForeColor = Color.White;
        _btnGenerate.Location = new Point(85, 112);
        _btnGenerate.Name = "_btnGenerate";
        _btnGenerate.Size = new Size(170, 36);
        _btnGenerate.TabIndex = 6;
        _btnGenerate.Text = "⚡ Sinh Slot Tự Động";
        _btnGenerate.UseMnemonic = false;
        _btnGenerate.UseVisualStyleBackColor = false;
        // 
        // _lblGenResult
        // 
        _lblGenResult.AutoSize = true;
        _lblGenResult.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
        _lblGenResult.ForeColor = Color.FromArgb(22, 128, 92);
        _lblGenResult.Location = new Point(265, 120);
        _lblGenResult.Name = "_lblGenResult";
        _lblGenResult.Size = new Size(0, 20);
        _lblGenResult.TabIndex = 7;
        // 
        // grpSearch
        // 
        grpSearch.BackColor = Color.White;
        grpSearch.Controls.Add(lblDate);
        grpSearch.Controls.Add(_dtpSearchDate);
        grpSearch.Controls.Add(lblShift);
        grpSearch.Controls.Add(_cboSearchShift);
        grpSearch.Controls.Add(lblDeptSearch);
        grpSearch.Controls.Add(_cboSearchDept);
        grpSearch.Controls.Add(_chkOnlyAvailable);
        grpSearch.Controls.Add(_btnSearch);
        grpSearch.Controls.Add(_btnRefresh);
        grpSearch.Dock = DockStyle.Fill;
        grpSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        grpSearch.ForeColor = Color.FromArgb(18, 53, 91);
        grpSearch.Location = new Point(513, 0);
        grpSearch.Margin = new Padding(8, 0, 0, 0);
        grpSearch.Name = "grpSearch";
        grpSearch.Padding = new Padding(12);
        grpSearch.Size = new Size(497, 165);
        grpSearch.TabIndex = 1;
        grpSearch.TabStop = false;
        grpSearch.Text = "  🔍 BỘ LỌC TRA CỨU SLOT KHÁM  ";
        // 
        // lblDate
        // 
        lblDate.AutoSize = true;
        lblDate.Font = new Font("Segoe UI", 9F);
        lblDate.ForeColor = Color.FromArgb(23, 32, 51);
        lblDate.Location = new Point(14, 30);
        lblDate.Name = "lblDate";
        lblDate.Size = new Size(87, 20);
        lblDate.TabIndex = 0;
        lblDate.Text = "Ngày khám:";
        // 
        // _dtpSearchDate
        // 
        _dtpSearchDate.Font = new Font("Segoe UI", 9F);
        _dtpSearchDate.Format = DateTimePickerFormat.Short;
        _dtpSearchDate.Location = new Point(108, 27);
        _dtpSearchDate.Name = "_dtpSearchDate";
        _dtpSearchDate.Size = new Size(130, 27);
        _dtpSearchDate.TabIndex = 1;
        // 
        // lblShift
        // 
        lblShift.AutoSize = true;
        lblShift.Font = new Font("Segoe UI", 9F);
        lblShift.ForeColor = Color.FromArgb(23, 32, 51);
        lblShift.Location = new Point(252, 30);
        lblShift.Name = "lblShift";
        lblShift.Size = new Size(69, 20);
        lblShift.TabIndex = 2;
        lblShift.Text = "Ca khám:";
        // 
        // _cboSearchShift
        // 
        _cboSearchShift.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboSearchShift.Font = new Font("Segoe UI", 9F);
        _cboSearchShift.Items.AddRange(new object[] { "Tất cả ca", "Ca sáng", "Ca chiều", "Ca tối" });
        _cboSearchShift.Location = new Point(328, 27);
        _cboSearchShift.Name = "_cboSearchShift";
        _cboSearchShift.Size = new Size(135, 28);
        _cboSearchShift.TabIndex = 3;
        // 
        // lblDeptSearch
        // 
        lblDeptSearch.AutoSize = true;
        lblDeptSearch.Font = new Font("Segoe UI", 9F);
        lblDeptSearch.ForeColor = Color.FromArgb(23, 32, 51);
        lblDeptSearch.Location = new Point(14, 72);
        lblDeptSearch.Name = "lblDeptSearch";
        lblDeptSearch.Size = new Size(46, 20);
        lblDeptSearch.TabIndex = 4;
        lblDeptSearch.Text = "Khoa:";
        // 
        // _cboSearchDept
        // 
        _cboSearchDept.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboSearchDept.Font = new Font("Segoe UI", 9F);
        _cboSearchDept.Location = new Point(108, 68);
        _cboSearchDept.Name = "_cboSearchDept";
        _cboSearchDept.Size = new Size(210, 28);
        _cboSearchDept.TabIndex = 5;
        // 
        // _chkOnlyAvailable
        // 
        _chkOnlyAvailable.AutoSize = true;
        _chkOnlyAvailable.Checked = true;
        _chkOnlyAvailable.CheckState = CheckState.Checked;
        _chkOnlyAvailable.Font = new Font("Segoe UI", 9F);
        _chkOnlyAvailable.ForeColor = Color.FromArgb(23, 32, 51);
        _chkOnlyAvailable.Location = new Point(330, 70);
        _chkOnlyAvailable.Name = "_chkOnlyAvailable";
        _chkOnlyAvailable.Size = new Size(152, 24);
        _chkOnlyAvailable.TabIndex = 6;
        _chkOnlyAvailable.Text = "Chỉ hiện slot trống";
        _chkOnlyAvailable.UseVisualStyleBackColor = true;
        // 
        // _btnSearch
        // 
        _btnSearch.BackColor = Color.FromArgb(18, 53, 91);
        _btnSearch.Cursor = Cursors.Hand;
        _btnSearch.FlatAppearance.BorderSize = 0;
        _btnSearch.FlatStyle = FlatStyle.Flat;
        _btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _btnSearch.ForeColor = Color.White;
        _btnSearch.Location = new Point(108, 112);
        _btnSearch.Name = "_btnSearch";
        _btnSearch.Size = new Size(115, 36);
        _btnSearch.TabIndex = 7;
        _btnSearch.Text = "🔍 Tra cứu";
        _btnSearch.UseMnemonic = false;
        _btnSearch.UseVisualStyleBackColor = false;
        // 
        // _btnRefresh
        // 
        _btnRefresh.BackColor = Color.FromArgb(240, 244, 248);
        _btnRefresh.Cursor = Cursors.Hand;
        _btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(212, 219, 228);
        _btnRefresh.FlatStyle = FlatStyle.Flat;
        _btnRefresh.Font = new Font("Segoe UI", 9F);
        _btnRefresh.ForeColor = Color.FromArgb(23, 32, 51);
        _btnRefresh.Location = new Point(232, 112);
        _btnRefresh.Name = "_btnRefresh";
        _btnRefresh.Size = new Size(100, 36);
        _btnRefresh.TabIndex = 8;
        _btnRefresh.Text = "Làm mới";
        _btnRefresh.UseMnemonic = false;
        _btnRefresh.UseVisualStyleBackColor = false;
        // 
        // _dgvSlots
        // 
        _dgvSlots.AllowUserToAddRows = false;
        _dgvSlots.AllowUserToDeleteRows = false;
        _dgvSlots.AllowUserToResizeRows = false;
        _dgvSlots.AutoGenerateColumns = false;
        _dgvSlots.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dgvSlots.BackgroundColor = Color.White;
        _dgvSlots.BorderStyle = BorderStyle.None;
        _dgvSlots.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        _dgvSlots.ColumnHeadersHeight = 42;
        _dgvSlots.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        _dgvSlots.Dock = DockStyle.Fill;
        _dgvSlots.EnableHeadersVisualStyles = false;
        _dgvSlots.GridColor = Color.FromArgb(235, 240, 246);
        _dgvSlots.Location = new Point(20, 273);
        _dgvSlots.MultiSelect = false;
        _dgvSlots.Name = "_dgvSlots";
        _dgvSlots.ReadOnly = true;
        _dgvSlots.RowHeadersVisible = false;
        _dgvSlots.RowHeadersWidth = 51;
        _dgvSlots.RowTemplate.Height = 38;
        _dgvSlots.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvSlots.Size = new Size(1010, 439);
        _dgvSlots.TabIndex = 2;

        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(246, 248, 251);
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dataGridViewCellStyle1.ForeColor = Color.FromArgb(18, 53, 91);
        dataGridViewCellStyle1.Padding = new Padding(8, 0, 0, 0);
        _dgvSlots.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;

        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.White;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(23, 32, 51);
        dataGridViewCellStyle2.Padding = new Padding(8, 0, 0, 0);
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(232, 242, 253);
        dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(18, 53, 91);
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        _dgvSlots.DefaultCellStyle = dataGridViewCellStyle2;

        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(248, 250, 253);
        dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle3.ForeColor = Color.FromArgb(23, 32, 51);
        dataGridViewCellStyle3.Padding = new Padding(8, 0, 0, 0);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(232, 242, 253);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(18, 53, 91);
        _dgvSlots.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;

        // 
        // colId
        // 
        colId.DataPropertyName = "Id";
        colId.FillWeight = 8F;
        colId.HeaderText = "Mã slot";
        colId.Name = "colId";
        colId.ReadOnly = true;

        // 
        // colDoctorName
        // 
        colDoctorName.DataPropertyName = "DoctorName";
        colDoctorName.FillWeight = 20F;
        colDoctorName.HeaderText = "Bác sĩ phụ trách";
        colDoctorName.Name = "colDoctorName";
        colDoctorName.ReadOnly = true;

        // 
        // colDepartmentName
        // 
        colDepartmentName.DataPropertyName = "DepartmentName";
        colDepartmentName.FillWeight = 16F;
        colDepartmentName.HeaderText = "Khoa chuyên môn";
        colDepartmentName.Name = "colDepartmentName";
        colDepartmentName.ReadOnly = true;

        // 
        // colRoomName
        // 
        colRoomName.DataPropertyName = "RoomName";
        colRoomName.FillWeight = 14F;
        colRoomName.HeaderText = "Phòng khám";
        colRoomName.Name = "colRoomName";
        colRoomName.ReadOnly = true;

        // 
        // colTimeRangeText
        // 
        colTimeRangeText.DataPropertyName = "TimeRangeText";
        colTimeRangeText.FillWeight = 12F;
        colTimeRangeText.HeaderText = "Khung giờ";
        colTimeRangeText.Name = "colTimeRangeText";
        colTimeRangeText.ReadOnly = true;

        // 
        // colCapacity
        // 
        colCapacity.DataPropertyName = "Capacity";
        colCapacity.FillWeight = 8F;
        colCapacity.HeaderText = "Số lượng";
        colCapacity.Name = "colCapacity";
        colCapacity.ReadOnly = true;

        // 
        // colBookedCount
        // 
        colBookedCount.DataPropertyName = "BookedCount";
        colBookedCount.FillWeight = 8F;
        colBookedCount.HeaderText = "Đã đặt";
        colBookedCount.Name = "colBookedCount";
        colBookedCount.ReadOnly = true;

        // 
        // colAvailableCount
        // 
        colAvailableCount.DataPropertyName = "AvailableCount";
        colAvailableCount.FillWeight = 8F;
        colAvailableCount.HeaderText = "Còn trống";
        colAvailableCount.Name = "colAvailableCount";
        colAvailableCount.ReadOnly = true;

        // 
        // colStatus
        // 
        colStatus.DataPropertyName = "Status";
        colStatus.FillWeight = 12F;
        colStatus.HeaderText = "Trạng thái";
        colStatus.Name = "colStatus";
        colStatus.ReadOnly = true;

        _dgvSlots.Columns.AddRange(new DataGridViewColumn[] {
            colId,
            colDoctorName,
            colDepartmentName,
            colRoomName,
            colTimeRangeText,
            colCapacity,
            colBookedCount,
            colAvailableCount,
            colStatus
        });
        // 
        // statusPanel
        // 
        statusPanel.BackColor = Color.FromArgb(245, 247, 250);
        statusPanel.Controls.Add(_lblGridSummary);
        statusPanel.Dock = DockStyle.Bottom;
        statusPanel.Location = new Point(20, 712);
        statusPanel.Name = "statusPanel";
        statusPanel.Padding = new Padding(4, 6, 4, 6);
        statusPanel.Size = new Size(1010, 32);
        statusPanel.TabIndex = 3;
        // 
        // _lblGridSummary
        // 
        _lblGridSummary.AutoSize = true;
        _lblGridSummary.Dock = DockStyle.Left;
        _lblGridSummary.ForeColor = Color.FromArgb(102, 115, 134);
        _lblGridSummary.Location = new Point(4, 6);
        _lblGridSummary.Name = "_lblGridSummary";
        _lblGridSummary.Size = new Size(171, 20);
        _lblGridSummary.TabIndex = 0;
        _lblGridSummary.Text = "Sẵn sàng tra cứu dữ liệu.";
        _lblGridSummary.UseMnemonic = false;
        // 
        // ScheduleSlotPage
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        Controls.Add(_dgvSlots);
        Controls.Add(statusPanel);
        Controls.Add(topPanel);
        Controls.Add(headerPanel);
        Font = new Font("Segoe UI", 9F);
        Name = "ScheduleSlotPage";
        Padding = new Padding(20, 16, 20, 16);
        Size = new Size(1050, 760);
        headerPanel.ResumeLayout(false);
        headerPanel.PerformLayout();
        topPanel.ResumeLayout(false);
        tlpTop.ResumeLayout(false);
        grpGenerate.ResumeLayout(false);
        grpGenerate.PerformLayout();
        grpSearch.ResumeLayout(false);
        grpSearch.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvSlots).EndInit();
        statusPanel.ResumeLayout(false);
        statusPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel headerPanel;
    private Label lblTitle;
    private Panel topPanel;
    private TableLayoutPanel tlpTop;
    private GroupBox grpGenerate;
    private Label lblFrom;
    private DateTimePicker _dtpFromGen;
    private Label lblTo;
    private DateTimePicker _dtpToGen;
    private Label lblDeptGen;
    private ComboBox _cboGenDept;
    private Button _btnGenerate;
    private Label _lblGenResult;
    private GroupBox grpSearch;
    private Label lblDate;
    private DateTimePicker _dtpSearchDate;
    private Label lblShift;
    private ComboBox _cboSearchShift;
    private Label lblDeptSearch;
    private ComboBox _cboSearchDept;
    private CheckBox _chkOnlyAvailable;
    private Button _btnSearch;
    private Button _btnRefresh;
    private DataGridView _dgvSlots;
    private DataGridViewTextBoxColumn colId;
    private DataGridViewTextBoxColumn colDoctorName;
    private DataGridViewTextBoxColumn colDepartmentName;
    private DataGridViewTextBoxColumn colRoomName;
    private DataGridViewTextBoxColumn colTimeRangeText;
    private DataGridViewTextBoxColumn colCapacity;
    private DataGridViewTextBoxColumn colBookedCount;
    private DataGridViewTextBoxColumn colAvailableCount;
    private DataGridViewTextBoxColumn colStatus;
    private Panel statusPanel;
    private Label _lblGridSummary;
}
