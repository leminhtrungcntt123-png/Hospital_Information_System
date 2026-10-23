using HIS.WinForms.Controls;

namespace HIS.WinForms.Pages;

partial class DashboardPage
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
        lblGreeting = new Label();
        lblSubtitle = new Label();
        btnPeriod = new Button();
        tlpMetrics = new TableLayoutPanel();
        cardPatients = new CardPanel();
        iconPatients = new Panel();
        lblIconPatients = new Label();
        lblPatientsTitle = new Label();
        lblPatientsValue = new Label();
        lblPatientsMeta = new Label();
        cardWaiting = new CardPanel();
        iconWaiting = new Panel();
        lblIconWaiting = new Label();
        lblWaitingTitle = new Label();
        lblWaitingValue = new Label();
        lblWaitingMeta = new Label();
        cardInpatient = new CardPanel();
        iconInpatient = new Panel();
        lblIconInpatient = new Label();
        lblInpatientTitle = new Label();
        lblInpatientValue = new Label();
        lblInpatientMeta = new Label();
        cardRecords = new CardPanel();
        iconRecords = new Panel();
        lblIconRecords = new Label();
        lblRecordsTitle = new Label();
        lblRecordsValue = new Label();
        lblRecordsMeta = new Label();
        tlpMiddle = new TableLayoutPanel();
        cardChart = new CardPanel();
        chartBody = new Panel();
        barChart = new SimpleBarChart();
        chartLegend = new Panel();
        lblLegendDoneSquare = new Label();
        lblLegendDone = new Label();
        lblLegendReceivedSquare = new Label();
        lblLegendReceived = new Label();
        lblLegendPeak = new Label();
        chartHeader = new Panel();
        lblChartTitle = new Label();
        lblChartSubtitle = new Label();
        lnkChartReport = new Label();
        chartHeaderLine = new Panel();
        cardRooms = new CardPanel();
        tlpRooms = new TableLayoutPanel();
        lblRoomCode1 = new Label();
        lblRoomInfo1 = new Label();
        lblRoomCount1 = new Label();
        lblRoomStatus1 = new Label();
        lblRoomCode2 = new Label();
        lblRoomInfo2 = new Label();
        lblRoomCount2 = new Label();
        lblRoomStatus2 = new Label();
        lblRoomCode3 = new Label();
        lblRoomInfo3 = new Label();
        lblRoomCount3 = new Label();
        lblRoomStatus3 = new Label();
        lblRoomCode4 = new Label();
        lblRoomInfo4 = new Label();
        lblRoomCount4 = new Label();
        lblRoomStatus4 = new Label();
        roomsHeader = new Panel();
        lblRoomsTitle = new Label();
        lblRoomsSubtitle = new Label();
        lnkRoomsRefresh = new Label();
        tlpBottom = new TableLayoutPanel();
        cardPriority = new CardPanel();
        gridPriority = new DataGridView();
        colPriorityId = new DataGridViewTextBoxColumn();
        colPriorityName = new DataGridViewTextBoxColumn();
        colPriorityDepartment = new DataGridViewTextBoxColumn();
        colPriorityStatus = new DataGridViewTextBoxColumn();
        priorityHeader = new Panel();
        lblPriorityTitle = new Label();
        lblPrioritySubtitle = new Label();
        lnkPriorityViewAll = new Label();
        cardQuick = new CardPanel();
        tlpQuick = new TableLayoutPanel();
        btnQuickReception = new Button();
        btnQuickPatients = new Button();
        btnQuickPharmacy = new Button();
        btnQuickReports = new Button();
        quickHeader = new Panel();
        lblQuickTitle = new Label();
        lblQuickSubtitle = new Label();
        pageContent.SuspendLayout();
        headerPanel.SuspendLayout();
        tlpMetrics.SuspendLayout();
        cardPatients.SuspendLayout();
        iconPatients.SuspendLayout();
        cardWaiting.SuspendLayout();
        iconWaiting.SuspendLayout();
        cardInpatient.SuspendLayout();
        iconInpatient.SuspendLayout();
        cardRecords.SuspendLayout();
        iconRecords.SuspendLayout();
        tlpMiddle.SuspendLayout();
        cardChart.SuspendLayout();
        chartBody.SuspendLayout();
        chartLegend.SuspendLayout();
        chartHeader.SuspendLayout();
        cardRooms.SuspendLayout();
        tlpRooms.SuspendLayout();
        roomsHeader.SuspendLayout();
        tlpBottom.SuspendLayout();
        cardPriority.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridPriority).BeginInit();
        priorityHeader.SuspendLayout();
        cardQuick.SuspendLayout();
        tlpQuick.SuspendLayout();
        quickHeader.SuspendLayout();
        SuspendLayout();
        // 
        // pageContent
        // 
        pageContent.BackColor = Color.FromArgb(238, 242, 246);
        pageContent.ColumnCount = 1;
        pageContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        pageContent.Controls.Add(headerPanel, 0, 0);
        pageContent.Controls.Add(tlpMetrics, 0, 1);
        pageContent.Controls.Add(tlpMiddle, 0, 2);
        pageContent.Controls.Add(tlpBottom, 0, 3);
        pageContent.Dock = DockStyle.Fill;
        pageContent.Location = new Point(0, 0);
        pageContent.Name = "pageContent";
        pageContent.Padding = new Padding(20, 18, 20, 20);
        pageContent.RowCount = 4;
        pageContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
        pageContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
        pageContent.RowStyles.Add(new RowStyle(SizeType.Percent, 56F));
        pageContent.RowStyles.Add(new RowStyle(SizeType.Percent, 44F));
        pageContent.Size = new Size(1050, 750);
        pageContent.TabIndex = 0;
        // 
        // headerPanel
        // 
        headerPanel.BackColor = Color.FromArgb(238, 242, 246);
        headerPanel.Controls.Add(lblGreeting);
        headerPanel.Controls.Add(lblSubtitle);
        headerPanel.Controls.Add(btnPeriod);
        headerPanel.Dock = DockStyle.Fill;
        headerPanel.Location = new Point(20, 18);
        headerPanel.Margin = new Padding(0, 0, 0, 8);
        headerPanel.Name = "headerPanel";
        headerPanel.Size = new Size(1010, 54);
        headerPanel.TabIndex = 0;
        // 
        // lblGreeting
        // 
        lblGreeting.AutoSize = true;
        lblGreeting.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
        lblGreeting.ForeColor = Color.FromArgb(26, 42, 62);
        lblGreeting.Location = new Point(0, 0);
        lblGreeting.Name = "lblGreeting";
        lblGreeting.Size = new Size(258, 25);
        lblGreeting.TabIndex = 0;
        lblGreeting.Text = "Xin chào, Nguyễn Thu Trang";
        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 8.25F);
        lblSubtitle.ForeColor = Color.FromArgb(102, 115, 134);
        lblSubtitle.Location = new Point(1, 32);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(326, 13);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Tổng quan hoạt động bệnh viện hôm nay, Thứ Ba 24/06/2025";
        // 
        // btnPeriod
        // 
        btnPeriod.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnPeriod.BackColor = Color.White;
        btnPeriod.FlatAppearance.BorderColor = Color.FromArgb(186, 196, 208);
        btnPeriod.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 247, 250);
        btnPeriod.FlatStyle = FlatStyle.Flat;
        btnPeriod.Font = new Font("Segoe UI", 8.25F);
        btnPeriod.ForeColor = Color.FromArgb(52, 66, 87);
        btnPeriod.Location = new Point(880, 2);
        btnPeriod.Name = "btnPeriod";
        btnPeriod.Size = new Size(130, 32);
        btnPeriod.TabIndex = 2;
        btnPeriod.Text = "Hôm nay   ▾";
        btnPeriod.UseVisualStyleBackColor = false;
        // 
        // tlpMetrics
        // 
        tlpMetrics.BackColor = Color.FromArgb(238, 242, 246);
        tlpMetrics.ColumnCount = 4;
        tlpMetrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpMetrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpMetrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpMetrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpMetrics.Controls.Add(cardPatients, 0, 0);
        tlpMetrics.Controls.Add(cardWaiting, 1, 0);
        tlpMetrics.Controls.Add(cardInpatient, 2, 0);
        tlpMetrics.Controls.Add(cardRecords, 3, 0);
        tlpMetrics.Dock = DockStyle.Fill;
        tlpMetrics.Location = new Point(20, 80);
        tlpMetrics.Margin = new Padding(0, 0, 0, 12);
        tlpMetrics.Name = "tlpMetrics";
        tlpMetrics.RowCount = 1;
        tlpMetrics.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlpMetrics.Size = new Size(1010, 88);
        tlpMetrics.TabIndex = 1;
        // 
        // cardPatients
        // 
        cardPatients.Controls.Add(iconPatients);
        cardPatients.Controls.Add(lblPatientsTitle);
        cardPatients.Controls.Add(lblPatientsValue);
        cardPatients.Controls.Add(lblPatientsMeta);
        cardPatients.Dock = DockStyle.Fill;
        cardPatients.Location = new Point(0, 0);
        cardPatients.Margin = new Padding(0, 0, 12, 0);
        cardPatients.Name = "cardPatients";
        cardPatients.Size = new Size(240, 88);
        cardPatients.TabIndex = 0;
        // 
        // iconPatients
        // 
        iconPatients.BackColor = Color.FromArgb(232, 242, 253);
        iconPatients.Controls.Add(lblIconPatients);
        iconPatients.Location = new Point(13, 13);
        iconPatients.Name = "iconPatients";
        iconPatients.Size = new Size(43, 43);
        iconPatients.TabIndex = 0;
        // 
        // lblIconPatients
        // 
        lblIconPatients.BackColor = Color.Transparent;
        lblIconPatients.Dock = DockStyle.Fill;
        lblIconPatients.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblIconPatients.ForeColor = Color.FromArgb(22, 102, 177);
        lblIconPatients.Location = new Point(0, 0);
        lblIconPatients.Name = "lblIconPatients";
        lblIconPatients.Size = new Size(43, 43);
        lblIconPatients.TabIndex = 0;
        lblIconPatients.Text = "BN";
        lblIconPatients.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblPatientsTitle
        // 
        lblPatientsTitle.AutoSize = true;
        lblPatientsTitle.Font = new Font("Segoe UI", 8F);
        lblPatientsTitle.ForeColor = Color.FromArgb(102, 115, 134);
        lblPatientsTitle.Location = new Point(68, 15);
        lblPatientsTitle.Name = "lblPatientsTitle";
        lblPatientsTitle.Size = new Size(107, 13);
        lblPatientsTitle.TabIndex = 1;
        lblPatientsTitle.Text = "Bệnh nhân tiếp đón";
        // 
        // lblPatientsValue
        // 
        lblPatientsValue.AutoSize = true;
        lblPatientsValue.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
        lblPatientsValue.ForeColor = Color.FromArgb(27, 43, 62);
        lblPatientsValue.Location = new Point(66, 29);
        lblPatientsValue.Name = "lblPatientsValue";
        lblPatientsValue.Size = new Size(47, 30);
        lblPatientsValue.TabIndex = 2;
        lblPatientsValue.Text = "186";
        // 
        // lblPatientsMeta
        // 
        lblPatientsMeta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblPatientsMeta.AutoEllipsis = true;
        lblPatientsMeta.Font = new Font("Segoe UI", 7.25F);
        lblPatientsMeta.ForeColor = Color.FromArgb(121, 134, 152);
        lblPatientsMeta.Location = new Point(68, 63);
        lblPatientsMeta.Name = "lblPatientsMeta";
        lblPatientsMeta.Size = new Size(158, 15);
        lblPatientsMeta.TabIndex = 3;
        lblPatientsMeta.Text = "Tăng 12% so với hôm qua";
        // 
        // cardWaiting
        // 
        cardWaiting.Controls.Add(iconWaiting);
        cardWaiting.Controls.Add(lblWaitingTitle);
        cardWaiting.Controls.Add(lblWaitingValue);
        cardWaiting.Controls.Add(lblWaitingMeta);
        cardWaiting.Dock = DockStyle.Fill;
        cardWaiting.Location = new Point(252, 0);
        cardWaiting.Margin = new Padding(0, 0, 12, 0);
        cardWaiting.Name = "cardWaiting";
        cardWaiting.Size = new Size(240, 88);
        cardWaiting.TabIndex = 1;
        // 
        // iconWaiting
        // 
        iconWaiting.BackColor = Color.FromArgb(255, 244, 221);
        iconWaiting.Controls.Add(lblIconWaiting);
        iconWaiting.Location = new Point(13, 13);
        iconWaiting.Name = "iconWaiting";
        iconWaiting.Size = new Size(43, 43);
        iconWaiting.TabIndex = 0;
        // 
        // lblIconWaiting
        // 
        lblIconWaiting.BackColor = Color.Transparent;
        lblIconWaiting.Dock = DockStyle.Fill;
        lblIconWaiting.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblIconWaiting.ForeColor = Color.FromArgb(176, 107, 8);
        lblIconWaiting.Location = new Point(0, 0);
        lblIconWaiting.Name = "lblIconWaiting";
        lblIconWaiting.Size = new Size(43, 43);
        lblIconWaiting.TabIndex = 0;
        lblIconWaiting.Text = "CK";
        lblIconWaiting.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblWaitingTitle
        // 
        lblWaitingTitle.AutoSize = true;
        lblWaitingTitle.Font = new Font("Segoe UI", 8F);
        lblWaitingTitle.ForeColor = Color.FromArgb(102, 115, 134);
        lblWaitingTitle.Location = new Point(68, 15);
        lblWaitingTitle.Name = "lblWaitingTitle";
        lblWaitingTitle.Size = new Size(87, 13);
        lblWaitingTitle.TabIndex = 1;
        lblWaitingTitle.Text = "Đang chờ khám";
        // 
        // lblWaitingValue
        // 
        lblWaitingValue.AutoSize = true;
        lblWaitingValue.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
        lblWaitingValue.ForeColor = Color.FromArgb(27, 43, 62);
        lblWaitingValue.Location = new Point(66, 29);
        lblWaitingValue.Name = "lblWaitingValue";
        lblWaitingValue.Size = new Size(35, 30);
        lblWaitingValue.TabIndex = 2;
        lblWaitingValue.Text = "42";
        // 
        // lblWaitingMeta
        // 
        lblWaitingMeta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblWaitingMeta.AutoEllipsis = true;
        lblWaitingMeta.Font = new Font("Segoe UI", 7.25F);
        lblWaitingMeta.ForeColor = Color.FromArgb(121, 134, 152);
        lblWaitingMeta.Location = new Point(68, 63);
        lblWaitingMeta.Name = "lblWaitingMeta";
        lblWaitingMeta.Size = new Size(158, 15);
        lblWaitingMeta.TabIndex = 3;
        lblWaitingMeta.Text = "6 phòng khám đang hoạt động";
        // 
        // cardInpatient
        // 
        cardInpatient.Controls.Add(iconInpatient);
        cardInpatient.Controls.Add(lblInpatientTitle);
        cardInpatient.Controls.Add(lblInpatientValue);
        cardInpatient.Controls.Add(lblInpatientMeta);
        cardInpatient.Dock = DockStyle.Fill;
        cardInpatient.Location = new Point(504, 0);
        cardInpatient.Margin = new Padding(0, 0, 12, 0);
        cardInpatient.Name = "cardInpatient";
        cardInpatient.Size = new Size(240, 88);
        cardInpatient.TabIndex = 2;
        // 
        // iconInpatient
        // 
        iconInpatient.BackColor = Color.FromArgb(240, 234, 251);
        iconInpatient.Controls.Add(lblIconInpatient);
        iconInpatient.Location = new Point(13, 13);
        iconInpatient.Name = "iconInpatient";
        iconInpatient.Size = new Size(43, 43);
        iconInpatient.TabIndex = 0;
        // 
        // lblIconInpatient
        // 
        lblIconInpatient.BackColor = Color.Transparent;
        lblIconInpatient.Dock = DockStyle.Fill;
        lblIconInpatient.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblIconInpatient.ForeColor = Color.FromArgb(114, 81, 166);
        lblIconInpatient.Location = new Point(0, 0);
        lblIconInpatient.Name = "lblIconInpatient";
        lblIconInpatient.Size = new Size(43, 43);
        lblIconInpatient.TabIndex = 0;
        lblIconInpatient.Text = "NT";
        lblIconInpatient.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblInpatientTitle
        // 
        lblInpatientTitle.AutoSize = true;
        lblInpatientTitle.Font = new Font("Segoe UI", 8F);
        lblInpatientTitle.ForeColor = Color.FromArgb(102, 115, 134);
        lblInpatientTitle.Location = new Point(68, 15);
        lblInpatientTitle.Name = "lblInpatientTitle";
        lblInpatientTitle.Size = new Size(103, 13);
        lblInpatientTitle.TabIndex = 1;
        lblInpatientTitle.Text = "Bệnh nhân nội trú";
        // 
        // lblInpatientValue
        // 
        lblInpatientValue.AutoSize = true;
        lblInpatientValue.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
        lblInpatientValue.ForeColor = Color.FromArgb(27, 43, 62);
        lblInpatientValue.Location = new Point(66, 29);
        lblInpatientValue.Name = "lblInpatientValue";
        lblInpatientValue.Size = new Size(47, 30);
        lblInpatientValue.TabIndex = 2;
        lblInpatientValue.Text = "324";
        // 
        // lblInpatientMeta
        // 
        lblInpatientMeta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblInpatientMeta.AutoEllipsis = true;
        lblInpatientMeta.Font = new Font("Segoe UI", 7.25F);
        lblInpatientMeta.ForeColor = Color.FromArgb(121, 134, 152);
        lblInpatientMeta.Location = new Point(68, 63);
        lblInpatientMeta.Name = "lblInpatientMeta";
        lblInpatientMeta.Size = new Size(158, 15);
        lblInpatientMeta.TabIndex = 3;
        lblInpatientMeta.Text = "Công suất giường 81%";
        // 
        // cardRecords
        // 
        cardRecords.Controls.Add(iconRecords);
        cardRecords.Controls.Add(lblRecordsTitle);
        cardRecords.Controls.Add(lblRecordsValue);
        cardRecords.Controls.Add(lblRecordsMeta);
        cardRecords.Dock = DockStyle.Fill;
        cardRecords.Location = new Point(756, 0);
        cardRecords.Margin = new Padding(0);
        cardRecords.Name = "cardRecords";
        cardRecords.Size = new Size(254, 88);
        cardRecords.TabIndex = 3;
        // 
        // iconRecords
        // 
        iconRecords.BackColor = Color.FromArgb(231, 245, 239);
        iconRecords.Controls.Add(lblIconRecords);
        iconRecords.Location = new Point(13, 13);
        iconRecords.Name = "iconRecords";
        iconRecords.Size = new Size(43, 43);
        iconRecords.TabIndex = 0;
        // 
        // lblIconRecords
        // 
        lblIconRecords.BackColor = Color.Transparent;
        lblIconRecords.Dock = DockStyle.Fill;
        lblIconRecords.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblIconRecords.ForeColor = Color.FromArgb(22, 128, 92);
        lblIconRecords.Location = new Point(0, 0);
        lblIconRecords.Name = "lblIconRecords";
        lblIconRecords.Size = new Size(43, 43);
        lblIconRecords.TabIndex = 0;
        lblIconRecords.Text = "HS";
        lblIconRecords.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRecordsTitle
        // 
        lblRecordsTitle.AutoSize = true;
        lblRecordsTitle.Font = new Font("Segoe UI", 8F);
        lblRecordsTitle.ForeColor = Color.FromArgb(102, 115, 134);
        lblRecordsTitle.Location = new Point(68, 15);
        lblRecordsTitle.Name = "lblRecordsTitle";
        lblRecordsTitle.Size = new Size(87, 13);
        lblRecordsTitle.TabIndex = 1;
        lblRecordsTitle.Text = "Hồ sơ hoàn tất";
        // 
        // lblRecordsValue
        // 
        lblRecordsValue.AutoSize = true;
        lblRecordsValue.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
        lblRecordsValue.ForeColor = Color.FromArgb(27, 43, 62);
        lblRecordsValue.Location = new Point(66, 29);
        lblRecordsValue.Name = "lblRecordsValue";
        lblRecordsValue.Size = new Size(47, 30);
        lblRecordsValue.TabIndex = 2;
        lblRecordsValue.Text = "128";
        // 
        // lblRecordsMeta
        // 
        lblRecordsMeta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblRecordsMeta.AutoEllipsis = true;
        lblRecordsMeta.Font = new Font("Segoe UI", 7.25F);
        lblRecordsMeta.ForeColor = Color.FromArgb(121, 134, 152);
        lblRecordsMeta.Location = new Point(68, 63);
        lblRecordsMeta.Name = "lblRecordsMeta";
        lblRecordsMeta.Size = new Size(172, 15);
        lblRecordsMeta.TabIndex = 3;
        lblRecordsMeta.Text = "Đã ký số trong ngày";
        // 
        // tlpMiddle
        // 
        tlpMiddle.BackColor = Color.FromArgb(238, 242, 246);
        tlpMiddle.ColumnCount = 2;
        tlpMiddle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
        tlpMiddle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
        tlpMiddle.Controls.Add(cardChart, 0, 0);
        tlpMiddle.Controls.Add(cardRooms, 1, 0);
        tlpMiddle.Dock = DockStyle.Fill;
        tlpMiddle.Location = new Point(20, 180);
        tlpMiddle.Margin = new Padding(0, 0, 0, 12);
        tlpMiddle.Name = "tlpMiddle";
        tlpMiddle.RowCount = 1;
        tlpMiddle.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlpMiddle.Size = new Size(1010, 288);
        tlpMiddle.TabIndex = 2;
        // 
        // cardChart
        // 
        cardChart.Controls.Add(chartBody);
        cardChart.Controls.Add(chartLegend);
        cardChart.Controls.Add(chartHeader);
        cardChart.Dock = DockStyle.Fill;
        cardChart.Location = new Point(0, 0);
        cardChart.Margin = new Padding(0, 0, 12, 0);
        cardChart.Name = "cardChart";
        cardChart.Size = new Size(573, 288);
        cardChart.TabIndex = 0;
        // 
        // chartBody
        // 
        chartBody.Controls.Add(barChart);
        chartBody.Dock = DockStyle.Fill;
        chartBody.Location = new Point(1, 51);
        chartBody.Name = "chartBody";
        chartBody.Padding = new Padding(10, 8, 10, 0);
        chartBody.Size = new Size(571, 198);
        chartBody.TabIndex = 2;
        // 
        // barChart
        // 
        barChart.BackColor = Color.White;
        barChart.Dock = DockStyle.Fill;
        barChart.Location = new Point(10, 8);
        barChart.Name = "barChart";
        barChart.Size = new Size(551, 190);
        barChart.TabIndex = 0;
        // 
        // chartLegend
        // 
        chartLegend.BackColor = Color.White;
        chartLegend.Controls.Add(lblLegendDoneSquare);
        chartLegend.Controls.Add(lblLegendDone);
        chartLegend.Controls.Add(lblLegendReceivedSquare);
        chartLegend.Controls.Add(lblLegendReceived);
        chartLegend.Controls.Add(lblLegendPeak);
        chartLegend.Dock = DockStyle.Bottom;
        chartLegend.Location = new Point(1, 249);
        chartLegend.Name = "chartLegend";
        chartLegend.Size = new Size(571, 38);
        chartLegend.TabIndex = 1;
        // 
        // lblLegendDoneSquare
        // 
        lblLegendDoneSquare.AutoSize = true;
        lblLegendDoneSquare.Font = new Font("Segoe UI", 7.5F);
        lblLegendDoneSquare.ForeColor = Color.FromArgb(22, 102, 177);
        lblLegendDoneSquare.Location = new Point(13, 13);
        lblLegendDoneSquare.Name = "lblLegendDoneSquare";
        lblLegendDoneSquare.Size = new Size(10, 12);
        lblLegendDoneSquare.TabIndex = 0;
        lblLegendDoneSquare.Text = "■";
        // 
        // lblLegendDone
        // 
        lblLegendDone.AutoSize = true;
        lblLegendDone.Font = new Font("Segoe UI", 7.25F);
        lblLegendDone.ForeColor = Color.FromArgb(102, 115, 134);
        lblLegendDone.Location = new Point(26, 12);
        lblLegendDone.Name = "lblLegendDone";
        lblLegendDone.Size = new Size(69, 12);
        lblLegendDone.TabIndex = 1;
        lblLegendDone.Text = "Đã khám: 128";
        // 
        // lblLegendReceivedSquare
        // 
        lblLegendReceivedSquare.AutoSize = true;
        lblLegendReceivedSquare.Font = new Font("Segoe UI", 7.5F);
        lblLegendReceivedSquare.ForeColor = Color.FromArgb(156, 195, 230);
        lblLegendReceivedSquare.Location = new Point(122, 13);
        lblLegendReceivedSquare.Name = "lblLegendReceivedSquare";
        lblLegendReceivedSquare.Size = new Size(10, 12);
        lblLegendReceivedSquare.TabIndex = 2;
        lblLegendReceivedSquare.Text = "■";
        // 
        // lblLegendReceived
        // 
        lblLegendReceived.AutoSize = true;
        lblLegendReceived.Font = new Font("Segoe UI", 7.25F);
        lblLegendReceived.ForeColor = Color.FromArgb(102, 115, 134);
        lblLegendReceived.Location = new Point(135, 12);
        lblLegendReceived.Name = "lblLegendReceived";
        lblLegendReceived.Size = new Size(92, 12);
        lblLegendReceived.TabIndex = 3;
        lblLegendReceived.Text = "Đã tiếp nhận: 186";
        // 
        // lblLegendPeak
        // 
        lblLegendPeak.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblLegendPeak.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblLegendPeak.ForeColor = Color.FromArgb(60, 74, 93);
        lblLegendPeak.Location = new Point(389, 12);
        lblLegendPeak.Name = "lblLegendPeak";
        lblLegendPeak.Size = new Size(169, 15);
        lblLegendPeak.TabIndex = 4;
        lblLegendPeak.Text = "Cao điểm 09:00 – 11:00";
        lblLegendPeak.TextAlign = ContentAlignment.MiddleRight;
        // 
        // chartHeader
        // 
        chartHeader.BackColor = Color.FromArgb(251, 252, 253);
        chartHeader.Controls.Add(lblChartTitle);
        chartHeader.Controls.Add(lblChartSubtitle);
        chartHeader.Controls.Add(lnkChartReport);
        chartHeader.Controls.Add(chartHeaderLine);
        chartHeader.Dock = DockStyle.Top;
        chartHeader.Location = new Point(1, 1);
        chartHeader.Name = "chartHeader";
        chartHeader.Size = new Size(571, 50);
        chartHeader.TabIndex = 0;
        // 
        // lblChartTitle
        // 
        lblChartTitle.AutoSize = true;
        lblChartTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblChartTitle.ForeColor = Color.FromArgb(37, 54, 74);
        lblChartTitle.Location = new Point(13, 9);
        lblChartTitle.Name = "lblChartTitle";
        lblChartTitle.Size = new Size(156, 15);
        lblChartTitle.TabIndex = 0;
        lblChartTitle.Text = "Lượt khám theo khung giờ";
        // 
        // lblChartSubtitle
        // 
        lblChartSubtitle.AutoSize = true;
        lblChartSubtitle.Font = new Font("Segoe UI", 7.25F);
        lblChartSubtitle.ForeColor = Color.FromArgb(122, 135, 152);
        lblChartSubtitle.Location = new Point(13, 28);
        lblChartSubtitle.Name = "lblChartSubtitle";
        lblChartSubtitle.Size = new Size(142, 12);
        lblChartSubtitle.TabIndex = 1;
        lblChartSubtitle.Text = "Số lượt tiếp nhận trong ngày";
        // 
        // lnkChartReport
        // 
        lnkChartReport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lnkChartReport.AutoSize = true;
        lnkChartReport.Cursor = Cursors.Hand;
        lnkChartReport.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lnkChartReport.ForeColor = Color.FromArgb(22, 102, 177);
        lnkChartReport.Location = new Point(490, 17);
        lnkChartReport.Name = "lnkChartReport";
        lnkChartReport.Size = new Size(68, 13);
        lnkChartReport.TabIndex = 2;
        lnkChartReport.Text = "Xem báo cáo";
        // 
        // chartHeaderLine
        // 
        chartHeaderLine.BackColor = Color.FromArgb(212, 219, 228);
        chartHeaderLine.Dock = DockStyle.Bottom;
        chartHeaderLine.Location = new Point(0, 49);
        chartHeaderLine.Name = "chartHeaderLine";
        chartHeaderLine.Size = new Size(571, 1);
        chartHeaderLine.TabIndex = 3;
        // 
        // cardRooms
        // 
        cardRooms.Controls.Add(tlpRooms);
        cardRooms.Controls.Add(roomsHeader);
        cardRooms.Dock = DockStyle.Fill;
        cardRooms.Location = new Point(585, 0);
        cardRooms.Margin = new Padding(0);
        cardRooms.Name = "cardRooms";
        cardRooms.Size = new Size(425, 288);
        cardRooms.TabIndex = 1;
        // 
        // tlpRooms
        // 
        tlpRooms.ColumnCount = 4;
        tlpRooms.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 46F));
        tlpRooms.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tlpRooms.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 57F));
        tlpRooms.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
        tlpRooms.Controls.Add(lblRoomCode1, 0, 0);
        tlpRooms.Controls.Add(lblRoomInfo1, 1, 0);
        tlpRooms.Controls.Add(lblRoomCount1, 2, 0);
        tlpRooms.Controls.Add(lblRoomStatus1, 3, 0);
        tlpRooms.Controls.Add(lblRoomCode2, 0, 1);
        tlpRooms.Controls.Add(lblRoomInfo2, 1, 1);
        tlpRooms.Controls.Add(lblRoomCount2, 2, 1);
        tlpRooms.Controls.Add(lblRoomStatus2, 3, 1);
        tlpRooms.Controls.Add(lblRoomCode3, 0, 2);
        tlpRooms.Controls.Add(lblRoomInfo3, 1, 2);
        tlpRooms.Controls.Add(lblRoomCount3, 2, 2);
        tlpRooms.Controls.Add(lblRoomStatus3, 3, 2);
        tlpRooms.Controls.Add(lblRoomCode4, 0, 3);
        tlpRooms.Controls.Add(lblRoomInfo4, 1, 3);
        tlpRooms.Controls.Add(lblRoomCount4, 2, 3);
        tlpRooms.Controls.Add(lblRoomStatus4, 3, 3);
        tlpRooms.Dock = DockStyle.Fill;
        tlpRooms.Location = new Point(1, 51);
        tlpRooms.Name = "tlpRooms";
        tlpRooms.RowCount = 4;
        tlpRooms.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tlpRooms.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tlpRooms.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tlpRooms.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tlpRooms.Size = new Size(423, 236);
        tlpRooms.TabIndex = 1;
        // 
        // lblRoomCode1
        // 
        lblRoomCode1.Anchor = AnchorStyles.Left;
        lblRoomCode1.BackColor = Color.FromArgb(232, 242, 253);
        lblRoomCode1.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblRoomCode1.ForeColor = Color.FromArgb(22, 102, 177);
        lblRoomCode1.Location = new Point(0, 15);
        lblRoomCode1.Margin = new Padding(0, 0, 3, 0);
        lblRoomCode1.Name = "lblRoomCode1";
        lblRoomCode1.Size = new Size(43, 27);
        lblRoomCode1.TabIndex = 0;
        lblRoomCode1.Text = "P. 302";
        lblRoomCode1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRoomInfo1
        // 
        lblRoomInfo1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblRoomInfo1.Font = new Font("Segoe UI", 7.25F);
        lblRoomInfo1.ForeColor = Color.FromArgb(67, 81, 102);
        lblRoomInfo1.Location = new Point(49, 24);
        lblRoomInfo1.Name = "lblRoomInfo1";
        lblRoomInfo1.Size = new Size(234, 32);
        lblRoomInfo1.TabIndex = 1;
        lblRoomInfo1.Text = "Tim mạch\r\nBS. Trần Thu Trang";
        // 
        // lblRoomCount1
        // 
        lblRoomCount1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblRoomCount1.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lblRoomCount1.ForeColor = Color.FromArgb(38, 54, 74);
        lblRoomCount1.Location = new Point(286, 28);
        lblRoomCount1.Name = "lblRoomCount1";
        lblRoomCount1.Size = new Size(57, 24);
        lblRoomCount1.TabIndex = 2;
        lblRoomCount1.Text = "12 / 18";
        lblRoomCount1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRoomStatus1
        // 
        lblRoomStatus1.Anchor = AnchorStyles.Left;
        lblRoomStatus1.BackColor = Color.FromArgb(231, 242, 252);
        lblRoomStatus1.Font = new Font("Segoe UI", 6.75F, FontStyle.Bold);
        lblRoomStatus1.ForeColor = Color.FromArgb(18, 96, 158);
        lblRoomStatus1.Location = new Point(349, 32);
        lblRoomStatus1.Margin = new Padding(6, 0, 6, 0);
        lblRoomStatus1.Name = "lblRoomStatus1";
        lblRoomStatus1.Size = new Size(64, 22);
        lblRoomStatus1.TabIndex = 3;
        lblRoomStatus1.Text = "Đang khám";
        lblRoomStatus1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRoomCode2
        // 
        lblRoomCode2.Anchor = AnchorStyles.Left;
        lblRoomCode2.BackColor = Color.FromArgb(232, 242, 253);
        lblRoomCode2.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblRoomCode2.ForeColor = Color.FromArgb(22, 102, 177);
        lblRoomCode2.Location = new Point(0, 74);
        lblRoomCode2.Margin = new Padding(0, 0, 3, 0);
        lblRoomCode2.Name = "lblRoomCode2";
        lblRoomCode2.Size = new Size(43, 27);
        lblRoomCode2.TabIndex = 4;
        lblRoomCode2.Text = "P. 106";
        lblRoomCode2.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRoomInfo2
        // 
        lblRoomInfo2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblRoomInfo2.Font = new Font("Segoe UI", 7.25F);
        lblRoomInfo2.ForeColor = Color.FromArgb(67, 81, 102);
        lblRoomInfo2.Location = new Point(49, 83);
        lblRoomInfo2.Name = "lblRoomInfo2";
        lblRoomInfo2.Size = new Size(234, 32);
        lblRoomInfo2.TabIndex = 5;
        lblRoomInfo2.Text = "Nội tổng quát\r\nBS. Lê Hoàng Nam";
        // 
        // lblRoomCount2
        // 
        lblRoomCount2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblRoomCount2.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lblRoomCount2.ForeColor = Color.FromArgb(38, 54, 74);
        lblRoomCount2.Location = new Point(286, 87);
        lblRoomCount2.Name = "lblRoomCount2";
        lblRoomCount2.Size = new Size(57, 24);
        lblRoomCount2.TabIndex = 6;
        lblRoomCount2.Text = "16 / 24";
        lblRoomCount2.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRoomStatus2
        // 
        lblRoomStatus2.Anchor = AnchorStyles.Left;
        lblRoomStatus2.BackColor = Color.FromArgb(231, 242, 252);
        lblRoomStatus2.Font = new Font("Segoe UI", 6.75F, FontStyle.Bold);
        lblRoomStatus2.ForeColor = Color.FromArgb(18, 96, 158);
        lblRoomStatus2.Location = new Point(349, 91);
        lblRoomStatus2.Margin = new Padding(6, 0, 6, 0);
        lblRoomStatus2.Name = "lblRoomStatus2";
        lblRoomStatus2.Size = new Size(64, 22);
        lblRoomStatus2.TabIndex = 7;
        lblRoomStatus2.Text = "Đang khám";
        lblRoomStatus2.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRoomCode3
        // 
        lblRoomCode3.Anchor = AnchorStyles.Left;
        lblRoomCode3.BackColor = Color.FromArgb(232, 242, 253);
        lblRoomCode3.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblRoomCode3.ForeColor = Color.FromArgb(22, 102, 177);
        lblRoomCode3.Location = new Point(0, 133);
        lblRoomCode3.Margin = new Padding(0, 0, 3, 0);
        lblRoomCode3.Name = "lblRoomCode3";
        lblRoomCode3.Size = new Size(43, 27);
        lblRoomCode3.TabIndex = 8;
        lblRoomCode3.Text = "P. 205";
        lblRoomCode3.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRoomInfo3
        // 
        lblRoomInfo3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblRoomInfo3.Font = new Font("Segoe UI", 7.25F);
        lblRoomInfo3.ForeColor = Color.FromArgb(67, 81, 102);
        lblRoomInfo3.Location = new Point(49, 142);
        lblRoomInfo3.Name = "lblRoomInfo3";
        lblRoomInfo3.Size = new Size(234, 32);
        lblRoomInfo3.TabIndex = 9;
        lblRoomInfo3.Text = "Cơ xương khớp\r\nBS. Nguyễn Văn Quang";
        // 
        // lblRoomCount3
        // 
        lblRoomCount3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblRoomCount3.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lblRoomCount3.ForeColor = Color.FromArgb(38, 54, 74);
        lblRoomCount3.Location = new Point(286, 146);
        lblRoomCount3.Name = "lblRoomCount3";
        lblRoomCount3.Size = new Size(57, 24);
        lblRoomCount3.TabIndex = 10;
        lblRoomCount3.Text = "8 / 15";
        lblRoomCount3.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRoomStatus3
        // 
        lblRoomStatus3.Anchor = AnchorStyles.Left;
        lblRoomStatus3.BackColor = Color.FromArgb(255, 244, 221);
        lblRoomStatus3.Font = new Font("Segoe UI", 6.75F, FontStyle.Bold);
        lblRoomStatus3.ForeColor = Color.FromArgb(152, 96, 9);
        lblRoomStatus3.Location = new Point(349, 150);
        lblRoomStatus3.Margin = new Padding(6, 0, 6, 0);
        lblRoomStatus3.Name = "lblRoomStatus3";
        lblRoomStatus3.Size = new Size(64, 22);
        lblRoomStatus3.TabIndex = 11;
        lblRoomStatus3.Text = "Tạm nghỉ";
        lblRoomStatus3.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRoomCode4
        // 
        lblRoomCode4.Anchor = AnchorStyles.Left;
        lblRoomCode4.BackColor = Color.FromArgb(232, 242, 253);
        lblRoomCode4.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblRoomCode4.ForeColor = Color.FromArgb(22, 102, 177);
        lblRoomCode4.Location = new Point(0, 192);
        lblRoomCode4.Margin = new Padding(0, 0, 3, 0);
        lblRoomCode4.Name = "lblRoomCode4";
        lblRoomCode4.Size = new Size(43, 27);
        lblRoomCode4.TabIndex = 12;
        lblRoomCode4.Text = "P. 401";
        lblRoomCode4.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRoomInfo4
        // 
        lblRoomInfo4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblRoomInfo4.Font = new Font("Segoe UI", 7.25F);
        lblRoomInfo4.ForeColor = Color.FromArgb(67, 81, 102);
        lblRoomInfo4.Location = new Point(49, 201);
        lblRoomInfo4.Name = "lblRoomInfo4";
        lblRoomInfo4.Size = new Size(234, 32);
        lblRoomInfo4.TabIndex = 13;
        lblRoomInfo4.Text = "Tai Mũi Họng\r\nBS. Phạm Minh Đức";
        // 
        // lblRoomCount4
        // 
        lblRoomCount4.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        lblRoomCount4.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lblRoomCount4.ForeColor = Color.FromArgb(38, 54, 74);
        lblRoomCount4.Location = new Point(286, 205);
        lblRoomCount4.Name = "lblRoomCount4";
        lblRoomCount4.Size = new Size(57, 24);
        lblRoomCount4.TabIndex = 14;
        lblRoomCount4.Text = "10 / 20";
        lblRoomCount4.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRoomStatus4
        // 
        lblRoomStatus4.Anchor = AnchorStyles.Left;
        lblRoomStatus4.BackColor = Color.FromArgb(231, 242, 252);
        lblRoomStatus4.Font = new Font("Segoe UI", 6.75F, FontStyle.Bold);
        lblRoomStatus4.ForeColor = Color.FromArgb(18, 96, 158);
        lblRoomStatus4.Location = new Point(349, 209);
        lblRoomStatus4.Margin = new Padding(6, 0, 6, 0);
        lblRoomStatus4.Name = "lblRoomStatus4";
        lblRoomStatus4.Size = new Size(64, 22);
        lblRoomStatus4.TabIndex = 15;
        lblRoomStatus4.Text = "Đang khám";
        lblRoomStatus4.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // roomsHeader
        // 
        roomsHeader.BackColor = Color.FromArgb(251, 252, 253);
        roomsHeader.Controls.Add(lblRoomsTitle);
        roomsHeader.Controls.Add(lblRoomsSubtitle);
        roomsHeader.Controls.Add(lnkRoomsRefresh);
        roomsHeader.Dock = DockStyle.Top;
        roomsHeader.Location = new Point(1, 1);
        roomsHeader.Name = "roomsHeader";
        roomsHeader.Size = new Size(423, 50);
        roomsHeader.TabIndex = 0;
        // 
        // lblRoomsTitle
        // 
        lblRoomsTitle.AutoSize = true;
        lblRoomsTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblRoomsTitle.ForeColor = Color.FromArgb(37, 54, 74);
        lblRoomsTitle.Location = new Point(13, 9);
        lblRoomsTitle.Name = "lblRoomsTitle";
        lblRoomsTitle.Size = new Size(135, 15);
        lblRoomsTitle.TabIndex = 0;
        lblRoomsTitle.Text = "Tình trạng phòng khám";
        // 
        // lblRoomsSubtitle
        // 
        lblRoomsSubtitle.AutoSize = true;
        lblRoomsSubtitle.Font = new Font("Segoe UI", 7.25F);
        lblRoomsSubtitle.ForeColor = Color.FromArgb(122, 135, 152);
        lblRoomsSubtitle.Location = new Point(13, 28);
        lblRoomsSubtitle.Name = "lblRoomsSubtitle";
        lblRoomsSubtitle.Size = new Size(136, 12);
        lblRoomsSubtitle.TabIndex = 1;
        lblRoomsSubtitle.Text = "Cập nhật gần nhất 08:41";
        // 
        // lnkRoomsRefresh
        // 
        lnkRoomsRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lnkRoomsRefresh.AutoSize = true;
        lnkRoomsRefresh.Cursor = Cursors.Hand;
        lnkRoomsRefresh.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lnkRoomsRefresh.ForeColor = Color.FromArgb(22, 102, 177);
        lnkRoomsRefresh.Location = new Point(359, 17);
        lnkRoomsRefresh.Name = "lnkRoomsRefresh";
        lnkRoomsRefresh.Size = new Size(48, 13);
        lnkRoomsRefresh.TabIndex = 2;
        lnkRoomsRefresh.Text = "Làm mới";
        // 
        // tlpBottom
        // 
        tlpBottom.BackColor = Color.FromArgb(238, 242, 246);
        tlpBottom.ColumnCount = 2;
        tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
        tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
        tlpBottom.Controls.Add(cardPriority, 0, 0);
        tlpBottom.Controls.Add(cardQuick, 1, 0);
        tlpBottom.Dock = DockStyle.Fill;
        tlpBottom.Location = new Point(20, 480);
        tlpBottom.Margin = new Padding(0);
        tlpBottom.Name = "tlpBottom";
        tlpBottom.RowCount = 1;
        tlpBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlpBottom.Size = new Size(1010, 240);
        tlpBottom.TabIndex = 3;
        // 
        // cardPriority
        // 
        cardPriority.Controls.Add(gridPriority);
        cardPriority.Controls.Add(priorityHeader);
        cardPriority.Dock = DockStyle.Fill;
        cardPriority.Location = new Point(0, 0);
        cardPriority.Margin = new Padding(0, 0, 12, 0);
        cardPriority.Name = "cardPriority";
        cardPriority.Size = new Size(674, 240);
        cardPriority.TabIndex = 0;
        // 
        // gridPriority
        // 
        gridPriority.AllowUserToAddRows = false;
        gridPriority.AllowUserToDeleteRows = false;
        gridPriority.AllowUserToResizeRows = false;
        gridPriority.BackgroundColor = Color.White;
        gridPriority.BorderStyle = BorderStyle.None;
        gridPriority.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        gridPriority.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        gridPriority.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleLeft,
            BackColor = Color.FromArgb(243, 246, 249),
            Font = new Font("Segoe UI", 7.25F, FontStyle.Bold),
            ForeColor = Color.FromArgb(98, 112, 132),
            Padding = new Padding(8, 0, 0, 0),
            SelectionBackColor = Color.FromArgb(243, 246, 249),
            SelectionForeColor = Color.FromArgb(98, 112, 132),
            WrapMode = DataGridViewTriState.False
        };
        gridPriority.ColumnHeadersHeight = 33;
        gridPriority.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        gridPriority.Columns.AddRange(new DataGridViewColumn[] { colPriorityId, colPriorityName, colPriorityDepartment, colPriorityStatus });
        gridPriority.DefaultCellStyle = new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleLeft,
            BackColor = Color.White,
            Font = new Font("Segoe UI", 8F),
            ForeColor = Color.FromArgb(67, 81, 102),
            Padding = new Padding(8, 0, 0, 0),
            SelectionBackColor = Color.FromArgb(246, 250, 255),
            SelectionForeColor = Color.FromArgb(38, 54, 74),
            WrapMode = DataGridViewTriState.False
        };
        gridPriority.Dock = DockStyle.Fill;
        gridPriority.EnableHeadersVisualStyles = false;
        gridPriority.GridColor = Color.FromArgb(230, 234, 239);
        gridPriority.Location = new Point(1, 51);
        gridPriority.MultiSelect = false;
        gridPriority.Name = "gridPriority";
        gridPriority.ReadOnly = true;
        gridPriority.RowHeadersVisible = false;
        gridPriority.RowTemplate.Height = 40;
        gridPriority.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridPriority.Size = new Size(672, 188);
        gridPriority.TabIndex = 1;
        // 
        // colPriorityId
        // 
        colPriorityId.HeaderText = "MÃ BN";
        colPriorityId.Name = "colPriorityId";
        colPriorityId.ReadOnly = true;
        colPriorityId.Width = 100;
        // 
        // colPriorityName
        // 
        colPriorityName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colPriorityName.FillWeight = 40F;
        colPriorityName.HeaderText = "HỌ VÀ TÊN";
        colPriorityName.Name = "colPriorityName";
        colPriorityName.ReadOnly = true;
        // 
        // colPriorityDepartment
        // 
        colPriorityDepartment.HeaderText = "CHUYÊN KHOA";
        colPriorityDepartment.Name = "colPriorityDepartment";
        colPriorityDepartment.ReadOnly = true;
        colPriorityDepartment.Width = 130;
        // 
        // colPriorityStatus
        // 
        colPriorityStatus.DefaultCellStyle = new DataGridViewCellStyle
        {
            Font = new Font("Segoe UI", 7.75F, FontStyle.Bold)
        };
        colPriorityStatus.HeaderText = "TRẠNG THÁI";
        colPriorityStatus.Name = "colPriorityStatus";
        colPriorityStatus.ReadOnly = true;
        colPriorityStatus.Width = 140;
        // 
        // priorityHeader
        // 
        priorityHeader.BackColor = Color.FromArgb(251, 252, 253);
        priorityHeader.Controls.Add(lblPriorityTitle);
        priorityHeader.Controls.Add(lblPrioritySubtitle);
        priorityHeader.Controls.Add(lnkPriorityViewAll);
        priorityHeader.Dock = DockStyle.Top;
        priorityHeader.Location = new Point(1, 1);
        priorityHeader.Name = "priorityHeader";
        priorityHeader.Size = new Size(672, 50);
        priorityHeader.TabIndex = 0;
        // 
        // lblPriorityTitle
        // 
        lblPriorityTitle.AutoSize = true;
        lblPriorityTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPriorityTitle.ForeColor = Color.FromArgb(37, 54, 74);
        lblPriorityTitle.Location = new Point(13, 9);
        lblPriorityTitle.Name = "lblPriorityTitle";
        lblPriorityTitle.Size = new Size(141, 15);
        lblPriorityTitle.TabIndex = 0;
        lblPriorityTitle.Text = "Danh sách chờ ưu tiên";
        // 
        // lblPrioritySubtitle
        // 
        lblPrioritySubtitle.AutoSize = true;
        lblPrioritySubtitle.Font = new Font("Segoe UI", 7.25F);
        lblPrioritySubtitle.ForeColor = Color.FromArgb(122, 135, 152);
        lblPrioritySubtitle.Location = new Point(13, 28);
        lblPrioritySubtitle.Name = "lblPrioritySubtitle";
        lblPrioritySubtitle.Size = new Size(161, 12);
        lblPrioritySubtitle.TabIndex = 1;
        lblPrioritySubtitle.Text = "Bệnh nhân cần được xử lý sớm";
        // 
        // lnkPriorityViewAll
        // 
        lnkPriorityViewAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lnkPriorityViewAll.AutoSize = true;
        lnkPriorityViewAll.Cursor = Cursors.Hand;
        lnkPriorityViewAll.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lnkPriorityViewAll.ForeColor = Color.FromArgb(22, 102, 177);
        lnkPriorityViewAll.Location = new Point(600, 17);
        lnkPriorityViewAll.Name = "lnkPriorityViewAll";
        lnkPriorityViewAll.Size = new Size(60, 13);
        lnkPriorityViewAll.TabIndex = 2;
        lnkPriorityViewAll.Text = "Xem tất cả";
        // 
        // cardQuick
        // 
        cardQuick.Controls.Add(tlpQuick);
        cardQuick.Controls.Add(quickHeader);
        cardQuick.Dock = DockStyle.Fill;
        cardQuick.Location = new Point(686, 0);
        cardQuick.Margin = new Padding(0);
        cardQuick.Name = "cardQuick";
        cardQuick.Size = new Size(324, 240);
        cardQuick.TabIndex = 1;
        // 
        // tlpQuick
        // 
        tlpQuick.ColumnCount = 2;
        tlpQuick.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlpQuick.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlpQuick.Controls.Add(btnQuickReception, 0, 0);
        tlpQuick.Controls.Add(btnQuickPatients, 1, 0);
        tlpQuick.Controls.Add(btnQuickPharmacy, 0, 1);
        tlpQuick.Controls.Add(btnQuickReports, 1, 1);
        tlpQuick.Dock = DockStyle.Fill;
        tlpQuick.Location = new Point(1, 51);
        tlpQuick.Name = "tlpQuick";
        tlpQuick.Padding = new Padding(10, 8, 10, 10);
        tlpQuick.RowCount = 2;
        tlpQuick.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpQuick.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpQuick.Size = new Size(322, 188);
        tlpQuick.TabIndex = 1;
        // 
        // btnQuickReception
        // 
        btnQuickReception.BackColor = Color.FromArgb(251, 252, 254);
        btnQuickReception.Dock = DockStyle.Fill;
        btnQuickReception.FlatAppearance.BorderColor = Color.FromArgb(212, 219, 228);
        btnQuickReception.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 242, 253);
        btnQuickReception.FlatStyle = FlatStyle.Flat;
        btnQuickReception.Font = new Font("Segoe UI", 8F);
        btnQuickReception.ForeColor = Color.FromArgb(43, 57, 76);
        btnQuickReception.Location = new Point(14, 12);
        btnQuickReception.Margin = new Padding(4);
        btnQuickReception.Name = "btnQuickReception";
        btnQuickReception.Padding = new Padding(9, 0, 0, 0);
        btnQuickReception.Size = new Size(141, 76);
        btnQuickReception.TabIndex = 0;
        btnQuickReception.Text = "Tiếp nhận bệnh nhân\r\nĐăng ký khám mới";
        btnQuickReception.TextAlign = ContentAlignment.MiddleLeft;
        btnQuickReception.UseVisualStyleBackColor = false;
        // 
        // btnQuickPatients
        // 
        btnQuickPatients.BackColor = Color.FromArgb(251, 252, 254);
        btnQuickPatients.Dock = DockStyle.Fill;
        btnQuickPatients.FlatAppearance.BorderColor = Color.FromArgb(212, 219, 228);
        btnQuickPatients.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 242, 253);
        btnQuickPatients.FlatStyle = FlatStyle.Flat;
        btnQuickPatients.Font = new Font("Segoe UI", 8F);
        btnQuickPatients.ForeColor = Color.FromArgb(43, 57, 76);
        btnQuickPatients.Location = new Point(163, 12);
        btnQuickPatients.Margin = new Padding(4);
        btnQuickPatients.Name = "btnQuickPatients";
        btnQuickPatients.Padding = new Padding(9, 0, 0, 0);
        btnQuickPatients.Size = new Size(141, 76);
        btnQuickPatients.TabIndex = 1;
        btnQuickPatients.Text = "Tra cứu hồ sơ\r\nTìm bệnh nhân";
        btnQuickPatients.TextAlign = ContentAlignment.MiddleLeft;
        btnQuickPatients.UseVisualStyleBackColor = false;
        // 
        // btnQuickPharmacy
        // 
        btnQuickPharmacy.BackColor = Color.FromArgb(251, 252, 254);
        btnQuickPharmacy.Dock = DockStyle.Fill;
        btnQuickPharmacy.FlatAppearance.BorderColor = Color.FromArgb(212, 219, 228);
        btnQuickPharmacy.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 242, 253);
        btnQuickPharmacy.FlatStyle = FlatStyle.Flat;
        btnQuickPharmacy.Font = new Font("Segoe UI", 8F);
        btnQuickPharmacy.ForeColor = Color.FromArgb(43, 57, 76);
        btnQuickPharmacy.Location = new Point(14, 96);
        btnQuickPharmacy.Margin = new Padding(4);
        btnQuickPharmacy.Name = "btnQuickPharmacy";
        btnQuickPharmacy.Padding = new Padding(9, 0, 0, 0);
        btnQuickPharmacy.Size = new Size(141, 78);
        btnQuickPharmacy.TabIndex = 2;
        btnQuickPharmacy.Text = "Kho dược\r\nKiểm tra tồn kho";
        btnQuickPharmacy.TextAlign = ContentAlignment.MiddleLeft;
        btnQuickPharmacy.UseVisualStyleBackColor = false;
        // 
        // btnQuickReports
        // 
        btnQuickReports.BackColor = Color.FromArgb(251, 252, 254);
        btnQuickReports.Dock = DockStyle.Fill;
        btnQuickReports.FlatAppearance.BorderColor = Color.FromArgb(212, 219, 228);
        btnQuickReports.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 242, 253);
        btnQuickReports.FlatStyle = FlatStyle.Flat;
        btnQuickReports.Font = new Font("Segoe UI", 8F);
        btnQuickReports.ForeColor = Color.FromArgb(43, 57, 76);
        btnQuickReports.Location = new Point(163, 96);
        btnQuickReports.Margin = new Padding(4);
        btnQuickReports.Name = "btnQuickReports";
        btnQuickReports.Padding = new Padding(9, 0, 0, 0);
        btnQuickReports.Size = new Size(141, 78);
        btnQuickReports.TabIndex = 3;
        btnQuickReports.Text = "In báo cáo\r\nBáo cáo cuối ngày";
        btnQuickReports.TextAlign = ContentAlignment.MiddleLeft;
        btnQuickReports.UseVisualStyleBackColor = false;
        // 
        // quickHeader
        // 
        quickHeader.BackColor = Color.FromArgb(251, 252, 253);
        quickHeader.Controls.Add(lblQuickTitle);
        quickHeader.Controls.Add(lblQuickSubtitle);
        quickHeader.Dock = DockStyle.Top;
        quickHeader.Location = new Point(1, 1);
        quickHeader.Name = "quickHeader";
        quickHeader.Size = new Size(322, 50);
        quickHeader.TabIndex = 0;
        // 
        // lblQuickTitle
        // 
        lblQuickTitle.AutoSize = true;
        lblQuickTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblQuickTitle.ForeColor = Color.FromArgb(37, 54, 74);
        lblQuickTitle.Location = new Point(13, 9);
        lblQuickTitle.Name = "lblQuickTitle";
        lblQuickTitle.Size = new Size(94, 15);
        lblQuickTitle.TabIndex = 0;
        lblQuickTitle.Text = "Truy cập nhanh";
        // 
        // lblQuickSubtitle
        // 
        lblQuickSubtitle.AutoSize = true;
        lblQuickSubtitle.Font = new Font("Segoe UI", 7.25F);
        lblQuickSubtitle.ForeColor = Color.FromArgb(122, 135, 152);
        lblQuickSubtitle.Location = new Point(13, 28);
        lblQuickSubtitle.Name = "lblQuickSubtitle";
        lblQuickSubtitle.Size = new Size(112, 12);
        lblQuickSubtitle.TabIndex = 1;
        lblQuickSubtitle.Text = "Các tác vụ thường dùng";
        // 
        // DashboardPage
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(238, 242, 246);
        Controls.Add(pageContent);
        Name = "DashboardPage";
        Size = new Size(1050, 760);
        pageContent.ResumeLayout(false);
        headerPanel.ResumeLayout(false);
        headerPanel.PerformLayout();
        tlpMetrics.ResumeLayout(false);
        cardPatients.ResumeLayout(false);
        cardPatients.PerformLayout();
        iconPatients.ResumeLayout(false);
        cardWaiting.ResumeLayout(false);
        cardWaiting.PerformLayout();
        iconWaiting.ResumeLayout(false);
        cardInpatient.ResumeLayout(false);
        cardInpatient.PerformLayout();
        iconInpatient.ResumeLayout(false);
        cardRecords.ResumeLayout(false);
        cardRecords.PerformLayout();
        iconRecords.ResumeLayout(false);
        tlpMiddle.ResumeLayout(false);
        cardChart.ResumeLayout(false);
        chartBody.ResumeLayout(false);
        chartLegend.ResumeLayout(false);
        chartLegend.PerformLayout();
        chartHeader.ResumeLayout(false);
        chartHeader.PerformLayout();
        cardRooms.ResumeLayout(false);
        tlpRooms.ResumeLayout(false);
        roomsHeader.ResumeLayout(false);
        roomsHeader.PerformLayout();
        tlpBottom.ResumeLayout(false);
        cardPriority.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridPriority).EndInit();
        priorityHeader.ResumeLayout(false);
        priorityHeader.PerformLayout();
        cardQuick.ResumeLayout(false);
        tlpQuick.ResumeLayout(false);
        quickHeader.ResumeLayout(false);
        quickHeader.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel pageContent;
    private Panel headerPanel;
    private Label lblGreeting;
    private Label lblSubtitle;
    private Button btnPeriod;
    private TableLayoutPanel tlpMetrics;
    private CardPanel cardPatients;
    private Panel iconPatients;
    private Label lblIconPatients;
    private Label lblPatientsTitle;
    private Label lblPatientsValue;
    private Label lblPatientsMeta;
    private CardPanel cardWaiting;
    private Panel iconWaiting;
    private Label lblIconWaiting;
    private Label lblWaitingTitle;
    private Label lblWaitingValue;
    private Label lblWaitingMeta;
    private CardPanel cardInpatient;
    private Panel iconInpatient;
    private Label lblIconInpatient;
    private Label lblInpatientTitle;
    private Label lblInpatientValue;
    private Label lblInpatientMeta;
    private CardPanel cardRecords;
    private Panel iconRecords;
    private Label lblIconRecords;
    private Label lblRecordsTitle;
    private Label lblRecordsValue;
    private Label lblRecordsMeta;
    private TableLayoutPanel tlpMiddle;
    private CardPanel cardChart;
    private Panel chartBody;
    private SimpleBarChart barChart;
    private Panel chartLegend;
    private Label lblLegendDoneSquare;
    private Label lblLegendDone;
    private Label lblLegendReceivedSquare;
    private Label lblLegendReceived;
    private Label lblLegendPeak;
    private Panel chartHeader;
    private Label lblChartTitle;
    private Label lblChartSubtitle;
    private Label lnkChartReport;
    private Panel chartHeaderLine;
    private CardPanel cardRooms;
    private TableLayoutPanel tlpRooms;
    private Label lblRoomCode1;
    private Label lblRoomInfo1;
    private Label lblRoomCount1;
    private Label lblRoomStatus1;
    private Label lblRoomCode2;
    private Label lblRoomInfo2;
    private Label lblRoomCount2;
    private Label lblRoomStatus2;
    private Label lblRoomCode3;
    private Label lblRoomInfo3;
    private Label lblRoomCount3;
    private Label lblRoomStatus3;
    private Label lblRoomCode4;
    private Label lblRoomInfo4;
    private Label lblRoomCount4;
    private Label lblRoomStatus4;
    private Panel roomsHeader;
    private Label lblRoomsTitle;
    private Label lblRoomsSubtitle;
    private Label lnkRoomsRefresh;
    private TableLayoutPanel tlpBottom;
    private CardPanel cardPriority;
    private DataGridView gridPriority;
    private DataGridViewTextBoxColumn colPriorityId;
    private DataGridViewTextBoxColumn colPriorityName;
    private DataGridViewTextBoxColumn colPriorityDepartment;
    private DataGridViewTextBoxColumn colPriorityStatus;
    private Panel priorityHeader;
    private Label lblPriorityTitle;
    private Label lblPrioritySubtitle;
    private Label lnkPriorityViewAll;
    private CardPanel cardQuick;
    private TableLayoutPanel tlpQuick;
    private Button btnQuickReception;
    private Button btnQuickPatients;
    private Button btnQuickPharmacy;
    private Button btnQuickReports;
    private Panel quickHeader;
    private Label lblQuickTitle;
    private Label lblQuickSubtitle;
}
