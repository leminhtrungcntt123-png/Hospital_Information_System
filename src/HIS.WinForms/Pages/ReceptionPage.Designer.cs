using HIS.WinForms.Controls;

namespace HIS.WinForms.Pages;

partial class ReceptionPage
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
        components = new System.ComponentModel.Container();
        pageContent = new TableLayoutPanel();
        headerPanel = new Panel();
        lblTitle = new Label();
        lblSubtitle = new Label();
        btnNewReception = new Button();
        tabsPanel = new Panel();
        btnTabWaiting = new Button();
        btnTabReceived = new Button();
        btnTabScheduled = new Button();
        btnTabCancelled = new Button();
        tabIndicator = new Panel();
        tabBottomLine = new Panel();
        listCard = new CardPanel();
        gridReceptions = new DataGridView();
        colAppointmentCode = new DataGridViewTextBoxColumn();
        colPatientCode = new DataGridViewTextBoxColumn();
        colPatientName = new DataGridViewTextBoxColumn();
        colGender = new DataGridViewTextBoxColumn();
        colDateOfBirth = new DataGridViewTextBoxColumn();
        colInsurance = new DataGridViewTextBoxColumn();
        colDepartment = new DataGridViewTextBoxColumn();
        colDoctor = new DataGridViewTextBoxColumn();
        colAppointmentTime = new DataGridViewTextBoxColumn();
        colQueue = new DataGridViewTextBoxColumn();
        colStatus = new DataGridViewTextBoxColumn();
        colAction = new DataGridViewButtonColumn();
        listFooter = new Panel();
        footerTopLine = new Panel();
        lblPageInfo = new Label();
        pagerPanel = new FlowLayoutPanel();
        listHeader = new Panel();
        lblListTitle = new Label();
        lblListSubtitle = new Label();
        txtSearch = new TextBox();
        btnSearch = new Button();
        btnRefresh = new Button();
        btnExport = new Button();
        pageContent.SuspendLayout();
        headerPanel.SuspendLayout();
        tabsPanel.SuspendLayout();
        listCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridReceptions).BeginInit();
        listFooter.SuspendLayout();
        listHeader.SuspendLayout();
        SuspendLayout();
        // 
        // pageContent
        // 
        pageContent.BackColor = Color.FromArgb(238, 242, 246);
        pageContent.ColumnCount = 1;
        pageContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        pageContent.Controls.Add(listCard, 0, 2);
        pageContent.Controls.Add(tabsPanel, 0, 1);
        pageContent.Controls.Add(headerPanel, 0, 0);
        pageContent.Dock = DockStyle.Fill;
        pageContent.Location = new Point(0, 0);
        pageContent.Name = "pageContent";
        pageContent.Padding = new Padding(20, 18, 20, 18);
        pageContent.RowCount = 3;
        pageContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
        pageContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
        pageContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        pageContent.Size = new Size(1050, 760);
        pageContent.TabIndex = 0;
        // 
        // headerPanel
        // 
        headerPanel.BackColor = Color.FromArgb(238, 242, 246);
        headerPanel.Controls.Add(lblTitle);
        headerPanel.Controls.Add(lblSubtitle);
        headerPanel.Controls.Add(btnNewReception);
        headerPanel.Dock = DockStyle.Fill;
        headerPanel.Location = new Point(20, 18);
        headerPanel.Margin = new Padding(0);
        headerPanel.Name = "headerPanel";
        headerPanel.Size = new Size(1010, 70);
        headerPanel.TabIndex = 0;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(18, 53, 91);
        lblTitle.Location = new Point(0, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(240, 37);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Tiếp đón bệnh nhân";
        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 9.75F);
        lblSubtitle.ForeColor = Color.FromArgb(102, 115, 134);
        lblSubtitle.Location = new Point(2, 40);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(451, 17);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Đăng ký khám, xếp hàng và điều phối bệnh nhân đến phòng khám";
        // 
        // btnNewReception
        // 
        btnNewReception.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnNewReception.BackColor = Color.FromArgb(22, 102, 177);
        btnNewReception.Cursor = Cursors.Hand;
        btnNewReception.FlatAppearance.BorderSize = 0;
        btnNewReception.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 79, 141);
        btnNewReception.FlatAppearance.MouseOverBackColor = Color.FromArgb(15, 79, 141);
        btnNewReception.FlatStyle = FlatStyle.Flat;
        btnNewReception.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnNewReception.ForeColor = Color.White;
        btnNewReception.Location = new Point(812, 4);
        btnNewReception.Name = "btnNewReception";
        btnNewReception.Size = new Size(198, 40);
        btnNewReception.TabIndex = 2;
        btnNewReception.Text = "+  Tiếp nhận bệnh nhân";
        btnNewReception.UseVisualStyleBackColor = false;
        // 
        // tabsPanel
        // 
        tabsPanel.BackColor = Color.FromArgb(238, 242, 246);
        tabsPanel.Controls.Add(btnTabWaiting);
        tabsPanel.Controls.Add(btnTabReceived);
        tabsPanel.Controls.Add(btnTabScheduled);
        tabsPanel.Controls.Add(btnTabCancelled);
        tabsPanel.Controls.Add(tabIndicator);
        tabsPanel.Controls.Add(tabBottomLine);
        tabsPanel.Dock = DockStyle.Fill;
        tabsPanel.Location = new Point(20, 88);
        tabsPanel.Margin = new Padding(0, 8, 0, 0);
        tabsPanel.Name = "tabsPanel";
        tabsPanel.Size = new Size(1010, 38);
        tabsPanel.TabIndex = 1;
        // 
        // btnTabWaiting
        // 
        btnTabWaiting.BackColor = Color.White;
        btnTabWaiting.Cursor = Cursors.Hand;
        btnTabWaiting.FlatAppearance.BorderSize = 0;
        btnTabWaiting.FlatAppearance.MouseOverBackColor = Color.FromArgb(246, 250, 255);
        btnTabWaiting.FlatStyle = FlatStyle.Flat;
        btnTabWaiting.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnTabWaiting.ForeColor = Color.FromArgb(22, 102, 177);
        btnTabWaiting.Location = new Point(0, 0);
        btnTabWaiting.Name = "btnTabWaiting";
        btnTabWaiting.Size = new Size(160, 34);
        btnTabWaiting.TabIndex = 0;
        btnTabWaiting.Tag = "Waiting";
        btnTabWaiting.Text = "Chờ tiếp nhận (0)";
        btnTabWaiting.UseVisualStyleBackColor = false;
        // 
        // btnTabReceived
        // 
        btnTabReceived.BackColor = Color.FromArgb(238, 242, 246);
        btnTabReceived.Cursor = Cursors.Hand;
        btnTabReceived.FlatAppearance.BorderSize = 0;
        btnTabReceived.FlatAppearance.MouseOverBackColor = Color.FromArgb(246, 250, 255);
        btnTabReceived.FlatStyle = FlatStyle.Flat;
        btnTabReceived.Font = new Font("Segoe UI", 9F);
        btnTabReceived.ForeColor = Color.FromArgb(102, 115, 134);
        btnTabReceived.Location = new Point(160, 0);
        btnTabReceived.Name = "btnTabReceived";
        btnTabReceived.Size = new Size(160, 34);
        btnTabReceived.TabIndex = 1;
        btnTabReceived.Tag = "Received";
        btnTabReceived.Text = "Đã tiếp nhận (0)";
        btnTabReceived.UseVisualStyleBackColor = false;
        // 
        // btnTabScheduled
        // 
        btnTabScheduled.BackColor = Color.FromArgb(238, 242, 246);
        btnTabScheduled.Cursor = Cursors.Hand;
        btnTabScheduled.FlatAppearance.BorderSize = 0;
        btnTabScheduled.FlatAppearance.MouseOverBackColor = Color.FromArgb(246, 250, 255);
        btnTabScheduled.FlatStyle = FlatStyle.Flat;
        btnTabScheduled.Font = new Font("Segoe UI", 9F);
        btnTabScheduled.ForeColor = Color.FromArgb(102, 115, 134);
        btnTabScheduled.Location = new Point(320, 0);
        btnTabScheduled.Name = "btnTabScheduled";
        btnTabScheduled.Size = new Size(130, 34);
        btnTabScheduled.TabIndex = 2;
        btnTabScheduled.Tag = "Scheduled";
        btnTabScheduled.Text = "Lịch hẹn (0)";
        btnTabScheduled.UseVisualStyleBackColor = false;
        // 
        // btnTabCancelled
        // 
        btnTabCancelled.BackColor = Color.FromArgb(238, 242, 246);
        btnTabCancelled.Cursor = Cursors.Hand;
        btnTabCancelled.FlatAppearance.BorderSize = 0;
        btnTabCancelled.FlatAppearance.MouseOverBackColor = Color.FromArgb(246, 250, 255);
        btnTabCancelled.FlatStyle = FlatStyle.Flat;
        btnTabCancelled.Font = new Font("Segoe UI", 9F);
        btnTabCancelled.ForeColor = Color.FromArgb(102, 115, 134);
        btnTabCancelled.Location = new Point(450, 0);
        btnTabCancelled.Name = "btnTabCancelled";
        btnTabCancelled.Size = new Size(120, 34);
        btnTabCancelled.TabIndex = 3;
        btnTabCancelled.Tag = "Cancelled";
        btnTabCancelled.Text = "Đã hủy (0)";
        btnTabCancelled.UseVisualStyleBackColor = false;
        // 
        // tabIndicator
        // 
        tabIndicator.BackColor = Color.FromArgb(22, 102, 177);
        tabIndicator.Location = new Point(0, 34);
        tabIndicator.Name = "tabIndicator";
        tabIndicator.Size = new Size(160, 3);
        tabIndicator.TabIndex = 4;
        // 
        // tabBottomLine
        // 
        tabBottomLine.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        tabBottomLine.BackColor = Color.FromArgb(186, 196, 208);
        tabBottomLine.Location = new Point(0, 37);
        tabBottomLine.Name = "tabBottomLine";
        tabBottomLine.Size = new Size(1010, 1);
        tabBottomLine.TabIndex = 5;
        // 
        // listCard
        // 
        listCard.BackColor = Color.White;
        listCard.Controls.Add(gridReceptions);
        listCard.Controls.Add(listFooter);
        listCard.Controls.Add(listHeader);
        listCard.Dock = DockStyle.Fill;
        listCard.Location = new Point(20, 134);
        listCard.Margin = new Padding(0, 8, 0, 0);
        listCard.Name = "listCard";
        listCard.Padding = new Padding(1);
        listCard.Size = new Size(1010, 608);
        listCard.TabIndex = 2;
        // 
        // gridReceptions
        // 
        gridReceptions.AllowUserToAddRows = false;
        gridReceptions.AllowUserToDeleteRows = false;
        gridReceptions.AllowUserToResizeRows = false;
        gridReceptions.BackgroundColor = Color.White;
        gridReceptions.BorderStyle = BorderStyle.None;
        gridReceptions.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        gridReceptions.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        gridReceptions.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleLeft,
            BackColor = Color.FromArgb(243, 246, 249),
            Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
            ForeColor = Color.FromArgb(98, 112, 132),
            Padding = new Padding(8, 0, 0, 0),
            SelectionBackColor = Color.FromArgb(243, 246, 249),
            SelectionForeColor = Color.FromArgb(98, 112, 132),
            WrapMode = DataGridViewTriState.False
        };
        gridReceptions.ColumnHeadersHeight = 36;
        gridReceptions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        gridReceptions.Columns.AddRange(new DataGridViewColumn[] { colAppointmentCode, colPatientCode, colPatientName, colGender, colDateOfBirth, colInsurance, colDepartment, colDoctor, colAppointmentTime, colQueue, colStatus, colAction });
        gridReceptions.DefaultCellStyle = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleLeft,
            BackColor = Color.White,
            Font = new Font("Segoe UI", 8.25F),
            ForeColor = Color.FromArgb(67, 81, 102),
            Padding = new Padding(8, 0, 0, 0),
            SelectionBackColor = Color.FromArgb(246, 250, 255),
            SelectionForeColor = Color.FromArgb(38, 54, 74),
            WrapMode = DataGridViewTriState.False
        };
        gridReceptions.Dock = DockStyle.Fill;
        gridReceptions.EnableHeadersVisualStyles = false;
        gridReceptions.GridColor = Color.FromArgb(230, 234, 239);
        gridReceptions.Location = new Point(1, 59);
        gridReceptions.MultiSelect = false;
        gridReceptions.Name = "gridReceptions";
        gridReceptions.ReadOnly = true;
        gridReceptions.RowHeadersVisible = false;
        gridReceptions.RowTemplate.Height = 44;
        gridReceptions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridReceptions.Size = new Size(1008, 498);
        gridReceptions.TabIndex = 1;
        // 
        // colAppointmentCode
        // 
        colAppointmentCode.HeaderText = "MÃ LƯỢT KHÁM";
        colAppointmentCode.Name = "colAppointmentCode";
        colAppointmentCode.ReadOnly = true;
        colAppointmentCode.Width = 142;
        // 
        // colPatientCode
        // 
        colPatientCode.HeaderText = "MÃ BN";
        colPatientCode.Name = "colPatientCode";
        colPatientCode.ReadOnly = true;
        colPatientCode.Width = 90;
        // 
        // colPatientName
        // 
        colPatientName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colPatientName.FillWeight = 28F;
        colPatientName.HeaderText = "HỌ VÀ TÊN";
        colPatientName.MinimumWidth = 126;
        colPatientName.Name = "colPatientName";
        colPatientName.ReadOnly = true;
        // 
        // colGender
        // 
        colGender.HeaderText = "GIỚI TÍNH";
        colGender.Name = "colGender";
        colGender.ReadOnly = true;
        colGender.Width = 94;
        // 
        // colDateOfBirth
        // 
        colDateOfBirth.HeaderText = "NGÀY SINH";
        colDateOfBirth.Name = "colDateOfBirth";
        colDateOfBirth.ReadOnly = true;
        colDateOfBirth.Width = 96;
        // 
        // colInsurance
        // 
        colInsurance.HeaderText = "SỐ BHYT";
        colInsurance.Name = "colInsurance";
        colInsurance.ReadOnly = true;
        colInsurance.Width = 156;
        // 
        // colDepartment
        // 
        colDepartment.HeaderText = "CHUYÊN KHOA";
        colDepartment.Name = "colDepartment";
        colDepartment.ReadOnly = true;
        colDepartment.Width = 160;
        // 
        // colDoctor
        // 
        colDoctor.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colDoctor.FillWeight = 20F;
        colDoctor.HeaderText = "BÁC SĨ";
        colDoctor.MinimumWidth = 110;
        colDoctor.Name = "colDoctor";
        colDoctor.ReadOnly = true;
        // 
        // colAppointmentTime
        // 
        colAppointmentTime.HeaderText = "THỜI GIAN";
        colAppointmentTime.Name = "colAppointmentTime";
        colAppointmentTime.ReadOnly = true;
        colAppointmentTime.Width = 92;
        // 
        // colQueue
        // 
        colQueue.HeaderText = "SỐ TT";
        colQueue.Name = "colQueue";
        colQueue.ReadOnly = true;
        colQueue.Width = 58;
        // 
        // colStatus
        // 
        colStatus.DefaultCellStyle = new DataGridViewCellStyle
        {
            Font = new Font("Segoe UI", 8F, FontStyle.Bold)
        };
        colStatus.HeaderText = "TRẠNG THÁI";
        colStatus.Name = "colStatus";
        colStatus.ReadOnly = true;
        colStatus.Width = 118;
        // 
        // colAction
        // 
        colAction.DefaultCellStyle = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleCenter,
            BackColor = Color.White,
            Font = new Font("Segoe UI", 8F, FontStyle.Bold),
            ForeColor = Color.FromArgb(22, 102, 177),
            Padding = new Padding(0),
            SelectionBackColor = Color.FromArgb(246, 250, 255),
            SelectionForeColor = Color.FromArgb(22, 102, 177)
        };
        colAction.FlatStyle = FlatStyle.Flat;
        colAction.HeaderText = "THAO TÁC";
        colAction.Name = "colAction";
        colAction.ReadOnly = true;
        colAction.Resizable = DataGridViewTriState.False;
        colAction.Width = 92;
        // 
        // listFooter
        // 
        listFooter.BackColor = Color.White;
        listFooter.Controls.Add(footerTopLine);
        listFooter.Controls.Add(lblPageInfo);
        listFooter.Controls.Add(pagerPanel);
        listFooter.Dock = DockStyle.Bottom;
        listFooter.Location = new Point(1, 557);
        listFooter.Name = "listFooter";
        listFooter.Size = new Size(1008, 50);
        listFooter.TabIndex = 2;
        // 
        // footerTopLine
        // 
        footerTopLine.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        footerTopLine.BackColor = Color.FromArgb(212, 219, 228);
        footerTopLine.Location = new Point(0, 0);
        footerTopLine.Name = "footerTopLine";
        footerTopLine.Size = new Size(1008, 1);
        footerTopLine.TabIndex = 0;
        // 
        // lblPageInfo
        // 
        lblPageInfo.AutoSize = true;
        lblPageInfo.Font = new Font("Segoe UI", 8.25F);
        lblPageInfo.ForeColor = Color.FromArgb(117, 130, 148);
        lblPageInfo.Location = new Point(14, 18);
        lblPageInfo.Name = "lblPageInfo";
        lblPageInfo.Size = new Size(148, 13);
        lblPageInfo.TabIndex = 1;
        lblPageInfo.Text = "Hiển thị 0–0 / 0 hồ sơ";
        // 
        // pagerPanel
        // 
        pagerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        pagerPanel.AutoSize = false;
        pagerPanel.BackColor = Color.White;
        pagerPanel.FlowDirection = FlowDirection.LeftToRight;
        pagerPanel.Location = new Point(714, 9);
        pagerPanel.Name = "pagerPanel";
        pagerPanel.Size = new Size(280, 30);
        pagerPanel.TabIndex = 2;
        pagerPanel.WrapContents = false;
        // 
        // listHeader
        // 
        listHeader.BackColor = Color.White;
        listHeader.Controls.Add(lblListTitle);
        listHeader.Controls.Add(lblListSubtitle);
        listHeader.Controls.Add(txtSearch);
        listHeader.Controls.Add(btnSearch);
        listHeader.Controls.Add(btnRefresh);
        listHeader.Controls.Add(btnExport);
        listHeader.Dock = DockStyle.Top;
        listHeader.Location = new Point(1, 1);
        listHeader.Name = "listHeader";
        listHeader.Size = new Size(1008, 58);
        listHeader.TabIndex = 0;
        // 
        // lblListTitle
        // 
        lblListTitle.AutoSize = true;
        lblListTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblListTitle.ForeColor = Color.FromArgb(37, 54, 74);
        lblListTitle.Location = new Point(14, 9);
        lblListTitle.Name = "lblListTitle";
        lblListTitle.Size = new Size(122, 17);
        lblListTitle.TabIndex = 0;
        lblListTitle.Text = "Danh sách tiếp đón";
        // 
        // lblListSubtitle
        // 
        lblListSubtitle.AutoSize = true;
        lblListSubtitle.Font = new Font("Segoe UI", 8.25F);
        lblListSubtitle.ForeColor = Color.FromArgb(122, 135, 152);
        lblListSubtitle.Location = new Point(14, 31);
        lblListSubtitle.Name = "lblListSubtitle";
        lblListSubtitle.Size = new Size(182, 13);
        lblListSubtitle.TabIndex = 1;
        lblListSubtitle.Text = "Danh sách bệnh nhân trong ngày";
        // 
        // txtSearch
        // 
        txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        txtSearch.BorderStyle = BorderStyle.FixedSingle;
        txtSearch.Font = new Font("Segoe UI", 9F);
        txtSearch.Location = new Point(401, 14);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Tìm tên, SĐT, mã BN, BHYT...";
        txtSearch.Size = new Size(300, 23);
        txtSearch.TabIndex = 2;
        // 
        // btnSearch
        // 
        btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSearch.BackColor = Color.White;
        btnSearch.Cursor = Cursors.Hand;
        btnSearch.FlatAppearance.BorderColor = Color.FromArgb(186, 196, 208);
        btnSearch.FlatAppearance.MouseDownBackColor = Color.FromArgb(232, 242, 253);
        btnSearch.FlatAppearance.MouseOverBackColor = Color.FromArgb(246, 250, 255);
        btnSearch.FlatStyle = FlatStyle.Flat;
        btnSearch.Font = new Font("Segoe UI", 8.5F);
        btnSearch.ForeColor = Color.FromArgb(77, 91, 110);
        btnSearch.Location = new Point(709, 13);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(68, 27);
        btnSearch.TabIndex = 3;
        btnSearch.Text = "Tìm";
        btnSearch.UseVisualStyleBackColor = false;
        // 
        // btnRefresh
        // 
        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.BackColor = Color.White;
        btnRefresh.Cursor = Cursors.Hand;
        btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(186, 196, 208);
        btnRefresh.FlatAppearance.MouseDownBackColor = Color.FromArgb(232, 242, 253);
        btnRefresh.FlatAppearance.MouseOverBackColor = Color.FromArgb(246, 250, 255);
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Font = new Font("Segoe UI", 8.5F);
        btnRefresh.ForeColor = Color.FromArgb(77, 91, 110);
        btnRefresh.Location = new Point(785, 13);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(92, 27);
        btnRefresh.TabIndex = 4;
        btnRefresh.Text = "Làm mới";
        btnRefresh.UseVisualStyleBackColor = false;
        // 
        // btnExport
        // 
        btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnExport.BackColor = Color.White;
        btnExport.Cursor = Cursors.Hand;
        btnExport.FlatAppearance.BorderColor = Color.FromArgb(186, 196, 208);
        btnExport.FlatAppearance.MouseDownBackColor = Color.FromArgb(232, 242, 253);
        btnExport.FlatAppearance.MouseOverBackColor = Color.FromArgb(246, 250, 255);
        btnExport.FlatStyle = FlatStyle.Flat;
        btnExport.Font = new Font("Segoe UI", 8.5F);
        btnExport.ForeColor = Color.FromArgb(77, 91, 110);
        btnExport.Location = new Point(885, 13);
        btnExport.Name = "btnExport";
        btnExport.Size = new Size(108, 27);
        btnExport.TabIndex = 5;
        btnExport.Text = "Xuất CSV";
        btnExport.UseVisualStyleBackColor = false;
        // 
        // ReceptionPage
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(238, 242, 246);
        Controls.Add(pageContent);
        Name = "ReceptionPage";
        Size = new Size(1050, 760);
        pageContent.ResumeLayout(false);
        headerPanel.ResumeLayout(false);
        headerPanel.PerformLayout();
        tabsPanel.ResumeLayout(false);
        listCard.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridReceptions).EndInit();
        listFooter.ResumeLayout(false);
        listFooter.PerformLayout();
        listHeader.ResumeLayout(false);
        listHeader.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel pageContent;
    private Panel headerPanel;
    private Label lblTitle;
    private Label lblSubtitle;
    private Button btnNewReception;
    private Panel tabsPanel;
    private Button btnTabWaiting;
    private Button btnTabReceived;
    private Button btnTabScheduled;
    private Button btnTabCancelled;
    private Panel tabIndicator;
    private Panel tabBottomLine;
    private CardPanel listCard;
    private DataGridView gridReceptions;
    private DataGridViewTextBoxColumn colAppointmentCode;
    private DataGridViewTextBoxColumn colPatientCode;
    private DataGridViewTextBoxColumn colPatientName;
    private DataGridViewTextBoxColumn colGender;
    private DataGridViewTextBoxColumn colDateOfBirth;
    private DataGridViewTextBoxColumn colInsurance;
    private DataGridViewTextBoxColumn colDepartment;
    private DataGridViewTextBoxColumn colDoctor;
    private DataGridViewTextBoxColumn colAppointmentTime;
    private DataGridViewTextBoxColumn colQueue;
    private DataGridViewTextBoxColumn colStatus;
    private DataGridViewButtonColumn colAction;
    private Panel listFooter;
    private Panel footerTopLine;
    private Label lblPageInfo;
    private FlowLayoutPanel pagerPanel;
    private Panel listHeader;
    private Label lblListTitle;
    private Label lblListSubtitle;
    private TextBox txtSearch;
    private Button btnSearch;
    private Button btnRefresh;
    private Button btnExport;
}
