using HIS.WinForms.Controls;

namespace HIS.WinForms;

partial class MainForm
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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        menuSystem = new ContextMenuStrip(components);
        menuItemAdmin = new ToolStripMenuItem();
        toolTip = new ToolTip(components);
        clockTimer = new System.Windows.Forms.Timer(components);
        titleBarPanel = new TitleBarPanel();
        logoMark = new Label();
        lblAppTitle = new Label();
        windowControlsPanel = new Panel();
        btnMinimize = new Button();
        btnMaximize = new Button();
        btnClose = new Button();
        menuBarPanel = new Panel();
        btnMenuSystem = new Button();
        btnMenuBusiness = new Button();
        btnMenuCatalog = new Button();
        btnMenuReports = new Button();
        btnMenuHelp = new Button();
        sessionPanel = new Panel();
        lblSessionDate = new Label();
        lblSessionSecure = new Label();
        sidebarPanel = new Panel();
        navHost = new Panel();
        lblNavGroupOverview = new Label();
        btnNavDashboard = new NavButton();
        lblNavGroupClinical = new Label();
        btnNavReception = new NavButton();
        lblBadgeReception = new Label();
        btnNavPatients = new NavButton();
        btnNavExamination = new NavButton();
        lblBadgeExamination = new Label();
        btnNavInpatient = new NavButton();
        lblNavGroupParaclinical = new Label();
        btnNavPharmacy = new NavButton();
        btnNavReports = new NavButton();
        toolsPanel = new Panel();
        btnSettings = new Button();
        btnHelp = new Button();
        accountPanel = new Panel();
        accountAvatar = new Label();
        accountName = new Label();
        accountRole = new Label();
        btnLogout = new Button();
        facilityHost = new Panel();
        facilityCard = new Panel();
        lblFacilityCaption = new Label();
        lblFacilityName = new Label();
        lblFacilityChevron = new Label();
        brandPanel = new Panel();
        brandMarkPanel = new Panel();
        brandMarkLabel = new Label();
        brandTitleLabel = new Label();
        brandSubtitleLabel = new Label();
        workspacePanel = new Panel();
        contentHostPanel = new Panel();
        toolbarPanel = new Panel();
        toolbarLine = new Panel();
        lblBreadcrumb = new Label();
        toolbarActionsPanel = new Panel();
        txtGlobalSearch = new TextBox();
        btnRefresh = new Button();
        btnNotifications = new Button();
        btnNewReception = new Button();
        statusBarPanel = new Panel();
        statusLine = new Panel();
        lblStatusSystem = new Label();
        lblStatusInfo = new Label();
        lblStatusRight = new Label();
        menuSystem.SuspendLayout();
        titleBarPanel.SuspendLayout();
        windowControlsPanel.SuspendLayout();
        menuBarPanel.SuspendLayout();
        sessionPanel.SuspendLayout();
        sidebarPanel.SuspendLayout();
        navHost.SuspendLayout();
        toolsPanel.SuspendLayout();
        accountPanel.SuspendLayout();
        facilityHost.SuspendLayout();
        facilityCard.SuspendLayout();
        brandPanel.SuspendLayout();
        brandMarkPanel.SuspendLayout();
        workspacePanel.SuspendLayout();
        toolbarPanel.SuspendLayout();
        toolbarActionsPanel.SuspendLayout();
        statusBarPanel.SuspendLayout();
        SuspendLayout();
        // 
        // menuSystem
        // 
        menuSystem.ImageScalingSize = new Size(20, 20);
        menuSystem.Items.AddRange(new ToolStripItem[] { menuItemAdmin });
        menuSystem.Name = "menuSystem";
        menuSystem.Size = new Size(192, 28);
        // 
        // menuItemAdmin
        // 
        menuItemAdmin.Name = "menuItemAdmin";
        menuItemAdmin.Size = new Size(191, 24);
        menuItemAdmin.Text = "Quản lý hệ thống";
        // 
        // clockTimer
        // 
        clockTimer.Interval = 1000;
        clockTimer.Tick += ClockTimer_Tick;
        // 
        // titleBarPanel
        // 
        titleBarPanel.BackColor = Color.FromArgb(13, 49, 86);
        titleBarPanel.Controls.Add(logoMark);
        titleBarPanel.Controls.Add(lblAppTitle);
        titleBarPanel.Controls.Add(windowControlsPanel);
        titleBarPanel.Dock = DockStyle.Top;
        titleBarPanel.Location = new Point(0, 0);
        titleBarPanel.Margin = new Padding(3, 4, 3, 4);
        titleBarPanel.Name = "titleBarPanel";
        titleBarPanel.Size = new Size(1463, 44);
        titleBarPanel.TabIndex = 4;
        // 
        // logoMark
        // 
        logoMark.BackColor = Color.White;
        logoMark.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        logoMark.Location = new Point(14, 9);
        logoMark.Name = "logoMark";
        logoMark.Size = new Size(22, 25);
        logoMark.TabIndex = 0;
        logoMark.Text = "H";
        logoMark.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblAppTitle
        // 
        lblAppTitle.AutoSize = true;
        lblAppTitle.BackColor = Color.Transparent;
        lblAppTitle.Font = new Font("Segoe UI", 8.25F);
        lblAppTitle.ForeColor = Color.White;
        lblAppTitle.Location = new Point(45, 12);
        lblAppTitle.Name = "lblAppTitle";
        lblAppTitle.Size = new Size(215, 19);
        lblAppTitle.TabIndex = 1;
        lblAppTitle.Text = "HIS - Hệ thống quản lý bệnh viện";
        // 
        // windowControlsPanel
        // 
        windowControlsPanel.BackColor = Color.FromArgb(23, 79, 130);
        windowControlsPanel.Controls.Add(btnMinimize);
        windowControlsPanel.Controls.Add(btnMaximize);
        windowControlsPanel.Controls.Add(btnClose);
        windowControlsPanel.Dock = DockStyle.Right;
        windowControlsPanel.Location = new Point(1305, 0);
        windowControlsPanel.Margin = new Padding(3, 4, 3, 4);
        windowControlsPanel.Name = "windowControlsPanel";
        windowControlsPanel.Size = new Size(158, 44);
        windowControlsPanel.TabIndex = 2;
        // 
        // btnMinimize
        // 
        btnMinimize.BackColor = Color.FromArgb(23, 79, 130);
        btnMinimize.FlatAppearance.BorderSize = 0;
        btnMinimize.FlatStyle = FlatStyle.Flat;
        btnMinimize.Font = new Font("Segoe UI", 9.5F);
        btnMinimize.ForeColor = Color.White;
        btnMinimize.Location = new Point(0, 0);
        btnMinimize.Margin = new Padding(3, 4, 3, 4);
        btnMinimize.Name = "btnMinimize";
        btnMinimize.Size = new Size(53, 44);
        btnMinimize.TabIndex = 0;
        btnMinimize.Text = "—";
        btnMinimize.UseVisualStyleBackColor = false;
        // 
        // btnMaximize
        // 
        btnMaximize.BackColor = Color.FromArgb(23, 79, 130);
        btnMaximize.FlatAppearance.BorderSize = 0;
        btnMaximize.FlatStyle = FlatStyle.Flat;
        btnMaximize.Font = new Font("Segoe UI", 9.5F);
        btnMaximize.ForeColor = Color.White;
        btnMaximize.Location = new Point(53, 0);
        btnMaximize.Margin = new Padding(3, 4, 3, 4);
        btnMaximize.Name = "btnMaximize";
        btnMaximize.Size = new Size(53, 44);
        btnMaximize.TabIndex = 1;
        btnMaximize.Text = "□";
        btnMaximize.UseVisualStyleBackColor = false;
        // 
        // btnClose
        // 
        btnClose.BackColor = Color.FromArgb(23, 79, 130);
        btnClose.FlatAppearance.BorderSize = 0;
        btnClose.FlatStyle = FlatStyle.Flat;
        btnClose.Font = new Font("Segoe UI", 10.5F);
        btnClose.ForeColor = Color.White;
        btnClose.Location = new Point(105, 0);
        btnClose.Margin = new Padding(3, 4, 3, 4);
        btnClose.Name = "btnClose";
        btnClose.Size = new Size(53, 44);
        btnClose.TabIndex = 2;
        btnClose.Text = "×";
        btnClose.UseVisualStyleBackColor = false;
        // 
        // menuBarPanel
        // 
        menuBarPanel.BackColor = Color.White;
        menuBarPanel.Controls.Add(btnMenuSystem);
        menuBarPanel.Controls.Add(btnMenuBusiness);
        menuBarPanel.Controls.Add(btnMenuCatalog);
        menuBarPanel.Controls.Add(btnMenuReports);
        menuBarPanel.Controls.Add(btnMenuHelp);
        menuBarPanel.Controls.Add(sessionPanel);
        menuBarPanel.Dock = DockStyle.Top;
        menuBarPanel.Location = new Point(0, 44);
        menuBarPanel.Margin = new Padding(3, 4, 3, 4);
        menuBarPanel.Name = "menuBarPanel";
        menuBarPanel.Size = new Size(1463, 43);
        menuBarPanel.TabIndex = 3;
        // 
        // btnMenuSystem
        // 
        btnMenuSystem.BackColor = Color.White;
        btnMenuSystem.FlatAppearance.BorderSize = 0;
        btnMenuSystem.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 242, 253);
        btnMenuSystem.FlatStyle = FlatStyle.Flat;
        btnMenuSystem.Font = new Font("Segoe UI", 8.25F);
        btnMenuSystem.ForeColor = Color.FromArgb(40, 54, 74);
        btnMenuSystem.Location = new Point(7, 1);
        btnMenuSystem.Margin = new Padding(3, 4, 3, 4);
        btnMenuSystem.Name = "btnMenuSystem";
        btnMenuSystem.Size = new Size(97, 40);
        btnMenuSystem.TabIndex = 0;
        btnMenuSystem.Text = "Hệ thống";
        btnMenuSystem.UseVisualStyleBackColor = false;
        // 
        // btnMenuBusiness
        // 
        btnMenuBusiness.BackColor = Color.White;
        btnMenuBusiness.FlatAppearance.BorderSize = 0;
        btnMenuBusiness.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 242, 253);
        btnMenuBusiness.FlatStyle = FlatStyle.Flat;
        btnMenuBusiness.Font = new Font("Segoe UI", 8.25F);
        btnMenuBusiness.ForeColor = Color.FromArgb(40, 54, 74);
        btnMenuBusiness.Location = new Point(104, 1);
        btnMenuBusiness.Margin = new Padding(3, 4, 3, 4);
        btnMenuBusiness.Name = "btnMenuBusiness";
        btnMenuBusiness.Size = new Size(97, 40);
        btnMenuBusiness.TabIndex = 1;
        btnMenuBusiness.Text = "Nghiệp vụ";
        btnMenuBusiness.UseVisualStyleBackColor = false;
        // 
        // btnMenuCatalog
        // 
        btnMenuCatalog.BackColor = Color.White;
        btnMenuCatalog.FlatAppearance.BorderSize = 0;
        btnMenuCatalog.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 242, 253);
        btnMenuCatalog.FlatStyle = FlatStyle.Flat;
        btnMenuCatalog.Font = new Font("Segoe UI", 8.25F);
        btnMenuCatalog.ForeColor = Color.FromArgb(40, 54, 74);
        btnMenuCatalog.Location = new Point(201, 1);
        btnMenuCatalog.Margin = new Padding(3, 4, 3, 4);
        btnMenuCatalog.Name = "btnMenuCatalog";
        btnMenuCatalog.Size = new Size(97, 40);
        btnMenuCatalog.TabIndex = 2;
        btnMenuCatalog.Text = "Danh mục";
        btnMenuCatalog.UseVisualStyleBackColor = false;
        // 
        // btnMenuReports
        // 
        btnMenuReports.BackColor = Color.White;
        btnMenuReports.FlatAppearance.BorderSize = 0;
        btnMenuReports.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 242, 253);
        btnMenuReports.FlatStyle = FlatStyle.Flat;
        btnMenuReports.Font = new Font("Segoe UI", 8.25F);
        btnMenuReports.ForeColor = Color.FromArgb(40, 54, 74);
        btnMenuReports.Location = new Point(298, 1);
        btnMenuReports.Margin = new Padding(3, 4, 3, 4);
        btnMenuReports.Name = "btnMenuReports";
        btnMenuReports.Size = new Size(97, 40);
        btnMenuReports.TabIndex = 3;
        btnMenuReports.Text = "Báo cáo";
        btnMenuReports.UseVisualStyleBackColor = false;
        // 
        // btnMenuHelp
        // 
        btnMenuHelp.BackColor = Color.White;
        btnMenuHelp.FlatAppearance.BorderSize = 0;
        btnMenuHelp.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 242, 253);
        btnMenuHelp.FlatStyle = FlatStyle.Flat;
        btnMenuHelp.Font = new Font("Segoe UI", 8.25F);
        btnMenuHelp.ForeColor = Color.FromArgb(40, 54, 74);
        btnMenuHelp.Location = new Point(395, 1);
        btnMenuHelp.Margin = new Padding(3, 4, 3, 4);
        btnMenuHelp.Name = "btnMenuHelp";
        btnMenuHelp.Size = new Size(97, 40);
        btnMenuHelp.TabIndex = 4;
        btnMenuHelp.Text = "Trợ giúp";
        btnMenuHelp.UseVisualStyleBackColor = false;
        // 
        // sessionPanel
        // 
        sessionPanel.Controls.Add(lblSessionDate);
        sessionPanel.Controls.Add(lblSessionSecure);
        sessionPanel.Dock = DockStyle.Right;
        sessionPanel.Location = new Point(1026, 0);
        sessionPanel.Margin = new Padding(3, 4, 3, 4);
        sessionPanel.Name = "sessionPanel";
        sessionPanel.Padding = new Padding(0, 0, 18, 0);
        sessionPanel.Size = new Size(437, 43);
        sessionPanel.TabIndex = 5;
        // 
        // lblSessionDate
        // 
        lblSessionDate.Font = new Font("Segoe UI", 7.5F);
        lblSessionDate.ForeColor = Color.FromArgb(102, 115, 134);
        lblSessionDate.Location = new Point(0, 9);
        lblSessionDate.Name = "lblSessionDate";
        lblSessionDate.Size = new Size(194, 24);
        lblSessionDate.TabIndex = 0;
        lblSessionDate.Text = "24/06/2025 08:42";
        lblSessionDate.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblSessionSecure
        // 
        lblSessionSecure.Font = new Font("Segoe UI", 7.5F);
        lblSessionSecure.ForeColor = Color.FromArgb(22, 128, 92);
        lblSessionSecure.Location = new Point(203, 9);
        lblSessionSecure.Name = "lblSessionSecure";
        lblSessionSecure.Size = new Size(210, 24);
        lblSessionSecure.TabIndex = 1;
        lblSessionSecure.Text = "✓  Phiên làm việc an toàn";
        lblSessionSecure.TextAlign = ContentAlignment.MiddleRight;
        // 
        // sidebarPanel
        // 
        sidebarPanel.BackColor = Color.FromArgb(11, 39, 70);
        sidebarPanel.Controls.Add(navHost);
        sidebarPanel.Controls.Add(toolsPanel);
        sidebarPanel.Controls.Add(accountPanel);
        sidebarPanel.Controls.Add(facilityHost);
        sidebarPanel.Controls.Add(brandPanel);
        sidebarPanel.Dock = DockStyle.Left;
        sidebarPanel.Location = new Point(0, 87);
        sidebarPanel.Margin = new Padding(3, 4, 3, 4);
        sidebarPanel.Name = "sidebarPanel";
        sidebarPanel.Size = new Size(280, 945);
        sidebarPanel.TabIndex = 1;
        // 
        // navHost
        // 
        navHost.Controls.Add(lblNavGroupOverview);
        navHost.Controls.Add(btnNavDashboard);
        navHost.Controls.Add(lblNavGroupClinical);
        navHost.Controls.Add(btnNavReception);
        navHost.Controls.Add(lblBadgeReception);
        navHost.Controls.Add(btnNavPatients);
        navHost.Controls.Add(btnNavExamination);
        navHost.Controls.Add(lblBadgeExamination);
        navHost.Controls.Add(btnNavInpatient);
        navHost.Controls.Add(lblNavGroupParaclinical);
        navHost.Controls.Add(btnNavPharmacy);
        navHost.Controls.Add(btnNavReports);
        navHost.Dock = DockStyle.Fill;
        navHost.Location = new Point(0, 168);
        navHost.Margin = new Padding(3, 4, 3, 4);
        navHost.Name = "navHost";
        navHost.Size = new Size(280, 601);
        navHost.TabIndex = 2;
        // 
        // lblNavGroupOverview
        // 
        lblNavGroupOverview.AutoSize = true;
        lblNavGroupOverview.BackColor = Color.Transparent;
        lblNavGroupOverview.Font = new Font("Segoe UI", 6.5F, FontStyle.Bold);
        lblNavGroupOverview.ForeColor = Color.FromArgb(114, 144, 170);
        lblNavGroupOverview.Location = new Point(11, 5);
        lblNavGroupOverview.Name = "lblNavGroupOverview";
        lblNavGroupOverview.Size = new Size(74, 13);
        lblNavGroupOverview.TabIndex = 0;
        lblNavGroupOverview.Text = "TỔNG QUAN";
        // 
        // btnNavDashboard
        // 
        btnNavDashboard.BackColor = Color.FromArgb(11, 39, 70);
        btnNavDashboard.FlatStyle = FlatStyle.Flat;
        btnNavDashboard.Font = new Font("Segoe UI", 8.5F);
        btnNavDashboard.ForeColor = Color.FromArgb(184, 200, 216);
        btnNavDashboard.Location = new Point(9, 29);
        btnNavDashboard.Margin = new Padding(3, 4, 3, 4);
        btnNavDashboard.Name = "btnNavDashboard";
        btnNavDashboard.Padding = new Padding(16, 0, 0, 0);
        btnNavDashboard.Size = new Size(262, 51);
        btnNavDashboard.TabIndex = 1;
        btnNavDashboard.Text = "Bàn làm việc";
        btnNavDashboard.TextAlign = ContentAlignment.MiddleLeft;
        btnNavDashboard.UseVisualStyleBackColor = false;
        // 
        // lblNavGroupClinical
        // 
        lblNavGroupClinical.AutoSize = true;
        lblNavGroupClinical.BackColor = Color.Transparent;
        lblNavGroupClinical.Font = new Font("Segoe UI", 6.5F, FontStyle.Bold);
        lblNavGroupClinical.ForeColor = Color.FromArgb(114, 144, 170);
        lblNavGroupClinical.Location = new Point(11, 96);
        lblNavGroupClinical.Name = "lblNavGroupClinical";
        lblNavGroupClinical.Size = new Size(109, 13);
        lblNavGroupClinical.TabIndex = 2;
        lblNavGroupClinical.Text = "KHÁM CHỮA BỆNH";
        // 
        // btnNavReception
        // 
        btnNavReception.BackColor = Color.FromArgb(11, 39, 70);
        btnNavReception.FlatStyle = FlatStyle.Flat;
        btnNavReception.Font = new Font("Segoe UI", 8.5F);
        btnNavReception.ForeColor = Color.FromArgb(184, 200, 216);
        btnNavReception.Location = new Point(9, 120);
        btnNavReception.Margin = new Padding(3, 4, 3, 4);
        btnNavReception.Name = "btnNavReception";
        btnNavReception.Padding = new Padding(16, 0, 0, 0);
        btnNavReception.Size = new Size(262, 51);
        btnNavReception.TabIndex = 3;
        btnNavReception.Text = "Tiếp đón";
        btnNavReception.TextAlign = ContentAlignment.MiddleLeft;
        btnNavReception.UseVisualStyleBackColor = false;
        // 
        // lblBadgeReception
        // 
        lblBadgeReception.BackColor = Color.FromArgb(225, 140, 43);
        lblBadgeReception.Font = new Font("Segoe UI", 6.5F, FontStyle.Bold);
        lblBadgeReception.ForeColor = Color.White;
        lblBadgeReception.Location = new Point(232, 136);
        lblBadgeReception.Name = "lblBadgeReception";
        lblBadgeReception.Size = new Size(23, 19);
        lblBadgeReception.TabIndex = 4;
        lblBadgeReception.Text = "18";
        lblBadgeReception.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // btnNavPatients
        // 
        btnNavPatients.BackColor = Color.FromArgb(11, 39, 70);
        btnNavPatients.FlatStyle = FlatStyle.Flat;
        btnNavPatients.Font = new Font("Segoe UI", 8.5F);
        btnNavPatients.ForeColor = Color.FromArgb(184, 200, 216);
        btnNavPatients.Location = new Point(9, 171);
        btnNavPatients.Margin = new Padding(3, 4, 3, 4);
        btnNavPatients.Name = "btnNavPatients";
        btnNavPatients.Padding = new Padding(16, 0, 0, 0);
        btnNavPatients.Size = new Size(262, 51);
        btnNavPatients.TabIndex = 5;
        btnNavPatients.Text = "Quản lý bệnh nhân";
        btnNavPatients.TextAlign = ContentAlignment.MiddleLeft;
        btnNavPatients.UseVisualStyleBackColor = false;
        // 
        // btnNavExamination
        // 
        btnNavExamination.BackColor = Color.FromArgb(11, 39, 70);
        btnNavExamination.FlatStyle = FlatStyle.Flat;
        btnNavExamination.Font = new Font("Segoe UI", 8.5F);
        btnNavExamination.ForeColor = Color.FromArgb(184, 200, 216);
        btnNavExamination.Location = new Point(9, 221);
        btnNavExamination.Margin = new Padding(3, 4, 3, 4);
        btnNavExamination.Name = "btnNavExamination";
        btnNavExamination.Padding = new Padding(16, 0, 0, 0);
        btnNavExamination.Size = new Size(262, 51);
        btnNavExamination.TabIndex = 6;
        btnNavExamination.Text = "Khám ngoại trú";
        btnNavExamination.TextAlign = ContentAlignment.MiddleLeft;
        btnNavExamination.UseVisualStyleBackColor = false;
        // 
        // lblBadgeExamination
        // 
        lblBadgeExamination.BackColor = Color.FromArgb(225, 140, 43);
        lblBadgeExamination.Font = new Font("Segoe UI", 6.5F, FontStyle.Bold);
        lblBadgeExamination.ForeColor = Color.White;
        lblBadgeExamination.Location = new Point(232, 237);
        lblBadgeExamination.Name = "lblBadgeExamination";
        lblBadgeExamination.Size = new Size(23, 19);
        lblBadgeExamination.TabIndex = 7;
        lblBadgeExamination.Text = "6";
        lblBadgeExamination.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // btnNavInpatient
        // 
        btnNavInpatient.BackColor = Color.FromArgb(11, 39, 70);
        btnNavInpatient.FlatStyle = FlatStyle.Flat;
        btnNavInpatient.Font = new Font("Segoe UI", 8.5F);
        btnNavInpatient.ForeColor = Color.FromArgb(184, 200, 216);
        btnNavInpatient.Location = new Point(9, 272);
        btnNavInpatient.Margin = new Padding(3, 4, 3, 4);
        btnNavInpatient.Name = "btnNavInpatient";
        btnNavInpatient.Padding = new Padding(16, 0, 0, 0);
        btnNavInpatient.Size = new Size(262, 51);
        btnNavInpatient.TabIndex = 8;
        btnNavInpatient.Text = "Quản lý nội trú";
        btnNavInpatient.TextAlign = ContentAlignment.MiddleLeft;
        btnNavInpatient.UseVisualStyleBackColor = false;
        // 
        // lblNavGroupParaclinical
        // 
        lblNavGroupParaclinical.AutoSize = true;
        lblNavGroupParaclinical.BackColor = Color.Transparent;
        lblNavGroupParaclinical.Font = new Font("Segoe UI", 6.5F, FontStyle.Bold);
        lblNavGroupParaclinical.ForeColor = Color.FromArgb(114, 144, 170);
        lblNavGroupParaclinical.Location = new Point(11, 336);
        lblNavGroupParaclinical.Name = "lblNavGroupParaclinical";
        lblNavGroupParaclinical.Size = new Size(93, 13);
        lblNavGroupParaclinical.TabIndex = 9;
        lblNavGroupParaclinical.Text = "CẬN LÂM SÀNG";
        // 
        // btnNavPharmacy
        // 
        btnNavPharmacy.BackColor = Color.FromArgb(11, 39, 70);
        btnNavPharmacy.FlatStyle = FlatStyle.Flat;
        btnNavPharmacy.Font = new Font("Segoe UI", 8.5F);
        btnNavPharmacy.ForeColor = Color.FromArgb(184, 200, 216);
        btnNavPharmacy.Location = new Point(9, 360);
        btnNavPharmacy.Margin = new Padding(3, 4, 3, 4);
        btnNavPharmacy.Name = "btnNavPharmacy";
        btnNavPharmacy.Padding = new Padding(16, 0, 0, 0);
        btnNavPharmacy.Size = new Size(262, 51);
        btnNavPharmacy.TabIndex = 10;
        btnNavPharmacy.Text = "Dược & kho thuốc";
        btnNavPharmacy.TextAlign = ContentAlignment.MiddleLeft;
        btnNavPharmacy.UseVisualStyleBackColor = false;
        // 
        // btnNavReports
        // 
        btnNavReports.BackColor = Color.FromArgb(11, 39, 70);
        btnNavReports.FlatStyle = FlatStyle.Flat;
        btnNavReports.Font = new Font("Segoe UI", 8.5F);
        btnNavReports.ForeColor = Color.FromArgb(184, 200, 216);
        btnNavReports.Location = new Point(9, 411);
        btnNavReports.Margin = new Padding(3, 4, 3, 4);
        btnNavReports.Name = "btnNavReports";
        btnNavReports.Padding = new Padding(16, 0, 0, 0);
        btnNavReports.Size = new Size(262, 51);
        btnNavReports.TabIndex = 11;
        btnNavReports.Text = "Báo cáo thống kê";
        btnNavReports.TextAlign = ContentAlignment.MiddleLeft;
        btnNavReports.UseVisualStyleBackColor = false;
        // 
        // toolsPanel
        // 
        toolsPanel.Controls.Add(btnSettings);
        toolsPanel.Controls.Add(btnHelp);
        toolsPanel.Dock = DockStyle.Bottom;
        toolsPanel.Location = new Point(0, 769);
        toolsPanel.Margin = new Padding(3, 4, 3, 4);
        toolsPanel.Name = "toolsPanel";
        toolsPanel.Size = new Size(280, 99);
        toolsPanel.TabIndex = 3;
        // 
        // btnSettings
        // 
        btnSettings.BackColor = Color.FromArgb(11, 39, 70);
        btnSettings.FlatAppearance.BorderSize = 0;
        btnSettings.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 70, 111);
        btnSettings.FlatStyle = FlatStyle.Flat;
        btnSettings.Font = new Font("Segoe UI", 8.25F);
        btnSettings.ForeColor = Color.FromArgb(146, 171, 192);
        btnSettings.Location = new Point(9, 9);
        btnSettings.Margin = new Padding(3, 4, 3, 4);
        btnSettings.Name = "btnSettings";
        btnSettings.Padding = new Padding(11, 0, 0, 0);
        btnSettings.Size = new Size(262, 37);
        btnSettings.TabIndex = 0;
        btnSettings.Text = "Thiết lập hệ thống";
        btnSettings.TextAlign = ContentAlignment.MiddleLeft;
        btnSettings.UseVisualStyleBackColor = false;
        // 
        // btnHelp
        // 
        btnHelp.BackColor = Color.FromArgb(11, 39, 70);
        btnHelp.FlatAppearance.BorderSize = 0;
        btnHelp.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 70, 111);
        btnHelp.FlatStyle = FlatStyle.Flat;
        btnHelp.Font = new Font("Segoe UI", 8.25F);
        btnHelp.ForeColor = Color.FromArgb(146, 171, 192);
        btnHelp.Location = new Point(9, 51);
        btnHelp.Margin = new Padding(3, 4, 3, 4);
        btnHelp.Name = "btnHelp";
        btnHelp.Padding = new Padding(11, 0, 0, 0);
        btnHelp.Size = new Size(262, 37);
        btnHelp.TabIndex = 1;
        btnHelp.Text = "Trợ giúp sử dụng";
        btnHelp.TextAlign = ContentAlignment.MiddleLeft;
        btnHelp.UseVisualStyleBackColor = false;
        // 
        // accountPanel
        // 
        accountPanel.BackColor = Color.FromArgb(8, 31, 57);
        accountPanel.Controls.Add(accountAvatar);
        accountPanel.Controls.Add(accountName);
        accountPanel.Controls.Add(accountRole);
        accountPanel.Controls.Add(btnLogout);
        accountPanel.Dock = DockStyle.Bottom;
        accountPanel.Location = new Point(0, 868);
        accountPanel.Margin = new Padding(3, 4, 3, 4);
        accountPanel.Name = "accountPanel";
        accountPanel.Size = new Size(280, 77);
        accountPanel.TabIndex = 4;
        // 
        // accountAvatar
        // 
        accountAvatar.BackColor = Color.FromArgb(45, 121, 183);
        accountAvatar.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        accountAvatar.ForeColor = Color.White;
        accountAvatar.Location = new Point(11, 17);
        accountAvatar.Name = "accountAvatar";
        accountAvatar.Size = new Size(35, 41);
        accountAvatar.TabIndex = 0;
        accountAvatar.Text = "NT";
        accountAvatar.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // accountName
        // 
        accountName.AutoSize = true;
        accountName.BackColor = Color.Transparent;
        accountName.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        accountName.ForeColor = Color.White;
        accountName.Location = new Point(57, 16);
        accountName.Name = "accountName";
        accountName.Size = new Size(131, 19);
        accountName.TabIndex = 1;
        accountName.Text = "Nguyễn Thu Trang";
        // 
        // accountRole
        // 
        accountRole.AutoSize = true;
        accountRole.BackColor = Color.Transparent;
        accountRole.Font = new Font("Segoe UI", 6.75F);
        accountRole.ForeColor = Color.FromArgb(145, 169, 191);
        accountRole.Location = new Point(57, 39);
        accountRole.Name = "accountRole";
        accountRole.Size = new Size(108, 15);
        accountRole.TabIndex = 2;
        accountRole.Text = "Điều dưỡng trưởng";
        // 
        // btnLogout
        // 
        btnLogout.BackColor = Color.FromArgb(8, 31, 57);
        btnLogout.FlatAppearance.BorderSize = 0;
        btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 70, 111);
        btnLogout.FlatStyle = FlatStyle.Flat;
        btnLogout.Font = new Font("Segoe UI", 7F);
        btnLogout.ForeColor = Color.FromArgb(146, 171, 192);
        btnLogout.Location = new Point(195, 23);
        btnLogout.Margin = new Padding(3, 4, 3, 4);
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(73, 32);
        btnLogout.TabIndex = 3;
        btnLogout.Text = "Đăng xuất";
        btnLogout.UseVisualStyleBackColor = false;
        // 
        // facilityHost
        // 
        facilityHost.Controls.Add(facilityCard);
        facilityHost.Dock = DockStyle.Top;
        facilityHost.Location = new Point(0, 89);
        facilityHost.Margin = new Padding(3, 4, 3, 4);
        facilityHost.Name = "facilityHost";
        facilityHost.Size = new Size(280, 79);
        facilityHost.TabIndex = 1;
        // 
        // facilityCard
        // 
        facilityCard.BackColor = Color.FromArgb(20, 58, 95);
        facilityCard.Controls.Add(lblFacilityCaption);
        facilityCard.Controls.Add(lblFacilityName);
        facilityCard.Controls.Add(lblFacilityChevron);
        facilityCard.Location = new Point(11, 8);
        facilityCard.Margin = new Padding(3, 4, 3, 4);
        facilityCard.Name = "facilityCard";
        facilityCard.Size = new Size(257, 61);
        facilityCard.TabIndex = 0;
        // 
        // lblFacilityCaption
        // 
        lblFacilityCaption.AutoSize = true;
        lblFacilityCaption.BackColor = Color.Transparent;
        lblFacilityCaption.Font = new Font("Segoe UI", 6.25F, FontStyle.Bold);
        lblFacilityCaption.ForeColor = Color.FromArgb(141, 169, 194);
        lblFacilityCaption.Location = new Point(10, 9);
        lblFacilityCaption.Name = "lblFacilityCaption";
        lblFacilityCaption.Size = new Size(131, 13);
        lblFacilityCaption.TabIndex = 0;
        lblFacilityCaption.Text = "CƠ SỞ ĐANG LÀM VIỆC";
        // 
        // lblFacilityName
        // 
        lblFacilityName.AutoSize = true;
        lblFacilityName.BackColor = Color.Transparent;
        lblFacilityName.Font = new Font("Segoe UI", 7.75F, FontStyle.Bold);
        lblFacilityName.ForeColor = Color.White;
        lblFacilityName.Location = new Point(10, 28);
        lblFacilityName.Name = "lblFacilityName";
        lblFacilityName.Size = new Size(192, 17);
        lblFacilityName.TabIndex = 1;
        lblFacilityName.Text = "Bệnh viện Đa khoa Trung tâm";
        // 
        // lblFacilityChevron
        // 
        lblFacilityChevron.AutoSize = true;
        lblFacilityChevron.BackColor = Color.Transparent;
        lblFacilityChevron.Font = new Font("Segoe UI", 9F);
        lblFacilityChevron.ForeColor = Color.FromArgb(135, 166, 193);
        lblFacilityChevron.Location = new Point(230, 16);
        lblFacilityChevron.Name = "lblFacilityChevron";
        lblFacilityChevron.Size = new Size(22, 20);
        lblFacilityChevron.TabIndex = 2;
        lblFacilityChevron.Text = "▾";
        // 
        // brandPanel
        // 
        brandPanel.Controls.Add(brandMarkPanel);
        brandPanel.Controls.Add(brandTitleLabel);
        brandPanel.Controls.Add(brandSubtitleLabel);
        brandPanel.Dock = DockStyle.Top;
        brandPanel.Location = new Point(0, 0);
        brandPanel.Margin = new Padding(3, 4, 3, 4);
        brandPanel.Name = "brandPanel";
        brandPanel.Size = new Size(280, 89);
        brandPanel.TabIndex = 0;
        // 
        // brandMarkPanel
        // 
        brandMarkPanel.BackColor = Color.White;
        brandMarkPanel.Controls.Add(brandMarkLabel);
        brandMarkPanel.Location = new Point(14, 20);
        brandMarkPanel.Margin = new Padding(3, 4, 3, 4);
        brandMarkPanel.Name = "brandMarkPanel";
        brandMarkPanel.Size = new Size(41, 48);
        brandMarkPanel.TabIndex = 0;
        // 
        // brandMarkLabel
        // 
        brandMarkLabel.Dock = DockStyle.Fill;
        brandMarkLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        brandMarkLabel.ForeColor = Color.FromArgb(18, 53, 91);
        brandMarkLabel.Location = new Point(0, 0);
        brandMarkLabel.Name = "brandMarkLabel";
        brandMarkLabel.Size = new Size(41, 48);
        brandMarkLabel.TabIndex = 0;
        brandMarkLabel.Text = "H";
        brandMarkLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // brandTitleLabel
        // 
        brandTitleLabel.AutoSize = true;
        brandTitleLabel.BackColor = Color.Transparent;
        brandTitleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        brandTitleLabel.ForeColor = Color.White;
        brandTitleLabel.Location = new Point(64, 16);
        brandTitleLabel.Name = "brandTitleLabel";
        brandTitleLabel.Size = new Size(62, 37);
        brandTitleLabel.TabIndex = 1;
        brandTitleLabel.Text = "HIS";
        // 
        // brandSubtitleLabel
        // 
        brandSubtitleLabel.AutoSize = true;
        brandSubtitleLabel.BackColor = Color.Transparent;
        brandSubtitleLabel.Font = new Font("Segoe UI", 6.25F);
        brandSubtitleLabel.ForeColor = Color.FromArgb(159, 184, 208);
        brandSubtitleLabel.Location = new Point(65, 56);
        brandSubtitleLabel.Name = "brandSubtitleLabel";
        brandSubtitleLabel.Size = new Size(173, 13);
        brandSubtitleLabel.TabIndex = 2;
        brandSubtitleLabel.Text = "HOSPITAL INFORMATION SYSTEM";
        // 
        // workspacePanel
        // 
        workspacePanel.Controls.Add(contentHostPanel);
        workspacePanel.Controls.Add(toolbarPanel);
        workspacePanel.Dock = DockStyle.Fill;
        workspacePanel.Location = new Point(280, 87);
        workspacePanel.Margin = new Padding(3, 4, 3, 4);
        workspacePanel.Name = "workspacePanel";
        workspacePanel.Size = new Size(1183, 945);
        workspacePanel.TabIndex = 0;
        // 
        // contentHostPanel
        // 
        contentHostPanel.BackColor = Color.FromArgb(238, 242, 246);
        contentHostPanel.Dock = DockStyle.Fill;
        contentHostPanel.Location = new Point(0, 73);
        contentHostPanel.Margin = new Padding(3, 4, 3, 4);
        contentHostPanel.Name = "contentHostPanel";
        contentHostPanel.Size = new Size(1183, 872);
        contentHostPanel.TabIndex = 1;
        // 
        // toolbarPanel
        // 
        toolbarPanel.BackColor = Color.White;
        toolbarPanel.Controls.Add(toolbarLine);
        toolbarPanel.Controls.Add(lblBreadcrumb);
        toolbarPanel.Controls.Add(toolbarActionsPanel);
        toolbarPanel.Dock = DockStyle.Top;
        toolbarPanel.Location = new Point(0, 0);
        toolbarPanel.Margin = new Padding(3, 4, 3, 4);
        toolbarPanel.Name = "toolbarPanel";
        toolbarPanel.Size = new Size(1183, 73);
        toolbarPanel.TabIndex = 0;
        // 
        // toolbarLine
        // 
        toolbarLine.BackColor = Color.FromArgb(212, 219, 228);
        toolbarLine.Dock = DockStyle.Bottom;
        toolbarLine.Location = new Point(0, 72);
        toolbarLine.Margin = new Padding(3, 4, 3, 4);
        toolbarLine.Name = "toolbarLine";
        toolbarLine.Size = new Size(1183, 1);
        toolbarLine.TabIndex = 2;
        // 
        // lblBreadcrumb
        // 
        lblBreadcrumb.AutoSize = true;
        lblBreadcrumb.Font = new Font("Segoe UI", 8.25F);
        lblBreadcrumb.ForeColor = Color.FromArgb(138, 150, 167);
        lblBreadcrumb.Location = new Point(21, 25);
        lblBreadcrumb.Name = "lblBreadcrumb";
        lblBreadcrumb.Size = new Size(134, 19);
        lblBreadcrumb.TabIndex = 0;
        lblBreadcrumb.Text = "HIS   ›   Bàn làm việc";
        // 
        // toolbarActionsPanel
        // 
        toolbarActionsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        toolbarActionsPanel.Controls.Add(txtGlobalSearch);
        toolbarActionsPanel.Controls.Add(btnRefresh);
        toolbarActionsPanel.Controls.Add(btnNotifications);
        toolbarActionsPanel.Controls.Add(btnNewReception);
        toolbarActionsPanel.Location = new Point(591, 13);
        toolbarActionsPanel.Margin = new Padding(3, 4, 3, 4);
        toolbarActionsPanel.Name = "toolbarActionsPanel";
        toolbarActionsPanel.Size = new Size(571, 45);
        toolbarActionsPanel.TabIndex = 1;
        // 
        // txtGlobalSearch
        // 
        txtGlobalSearch.BackColor = Color.FromArgb(250, 251, 253);
        txtGlobalSearch.BorderStyle = BorderStyle.FixedSingle;
        txtGlobalSearch.Font = new Font("Segoe UI", 8.25F);
        txtGlobalSearch.ForeColor = Color.FromArgb(77, 91, 110);
        txtGlobalSearch.Location = new Point(0, 1);
        txtGlobalSearch.Margin = new Padding(3, 4, 3, 4);
        txtGlobalSearch.Name = "txtGlobalSearch";
        txtGlobalSearch.PlaceholderText = "Tìm bệnh nhân, mã hồ sơ...";
        txtGlobalSearch.Size = new Size(285, 26);
        txtGlobalSearch.TabIndex = 0;
        // 
        // btnRefresh
        // 
        btnRefresh.BackColor = Color.White;
        btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(186, 196, 208);
        btnRefresh.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 247, 250);
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Font = new Font("Segoe UI", 11F);
        btnRefresh.ForeColor = Color.FromArgb(86, 100, 121);
        btnRefresh.Location = new Point(295, 1);
        btnRefresh.Margin = new Padding(3, 4, 3, 4);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(37, 43);
        btnRefresh.TabIndex = 1;
        btnRefresh.Text = "↻";
        btnRefresh.UseVisualStyleBackColor = false;
        // 
        // btnNotifications
        // 
        btnNotifications.BackColor = Color.White;
        btnNotifications.FlatAppearance.BorderColor = Color.FromArgb(186, 196, 208);
        btnNotifications.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 247, 250);
        btnNotifications.FlatStyle = FlatStyle.Flat;
        btnNotifications.Font = new Font("Segoe UI", 8.25F);
        btnNotifications.ForeColor = Color.FromArgb(197, 61, 70);
        btnNotifications.Location = new Point(338, 1);
        btnNotifications.Margin = new Padding(3, 4, 3, 4);
        btnNotifications.Name = "btnNotifications";
        btnNotifications.Size = new Size(37, 43);
        btnNotifications.TabIndex = 2;
        btnNotifications.Text = "●";
        btnNotifications.UseVisualStyleBackColor = false;
        // 
        // btnNewReception
        // 
        btnNewReception.BackColor = Color.FromArgb(22, 102, 177);
        btnNewReception.FlatAppearance.BorderSize = 0;
        btnNewReception.FlatAppearance.MouseOverBackColor = Color.FromArgb(26, 112, 184);
        btnNewReception.FlatStyle = FlatStyle.Flat;
        btnNewReception.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        btnNewReception.ForeColor = Color.White;
        btnNewReception.Location = new Point(382, 1);
        btnNewReception.Margin = new Padding(3, 4, 3, 4);
        btnNewReception.Name = "btnNewReception";
        btnNewReception.Size = new Size(190, 43);
        btnNewReception.TabIndex = 3;
        btnNewReception.Text = "+   Tiếp nhận mới";
        btnNewReception.UseVisualStyleBackColor = false;
        // 
        // statusBarPanel
        // 
        statusBarPanel.BackColor = Color.White;
        statusBarPanel.Controls.Add(statusLine);
        statusBarPanel.Controls.Add(lblStatusSystem);
        statusBarPanel.Controls.Add(lblStatusInfo);
        statusBarPanel.Controls.Add(lblStatusRight);
        statusBarPanel.Dock = DockStyle.Bottom;
        statusBarPanel.Location = new Point(0, 1032);
        statusBarPanel.Margin = new Padding(3, 4, 3, 4);
        statusBarPanel.Name = "statusBarPanel";
        statusBarPanel.Size = new Size(1463, 35);
        statusBarPanel.TabIndex = 2;
        // 
        // statusLine
        // 
        statusLine.BackColor = Color.FromArgb(186, 196, 208);
        statusLine.Dock = DockStyle.Top;
        statusLine.Location = new Point(0, 0);
        statusLine.Margin = new Padding(3, 4, 3, 4);
        statusLine.Name = "statusLine";
        statusLine.Size = new Size(1463, 1);
        statusLine.TabIndex = 3;
        // 
        // lblStatusSystem
        // 
        lblStatusSystem.AutoSize = true;
        lblStatusSystem.Font = new Font("Segoe UI", 7.5F);
        lblStatusSystem.ForeColor = Color.FromArgb(22, 128, 92);
        lblStatusSystem.Location = new Point(14, 8);
        lblStatusSystem.Name = "lblStatusSystem";
        lblStatusSystem.Size = new Size(218, 17);
        lblStatusSystem.TabIndex = 0;
        lblStatusSystem.Text = "●  Hệ thống hoạt động bình thường";
        // 
        // lblStatusInfo
        // 
        lblStatusInfo.AutoSize = true;
        lblStatusInfo.Font = new Font("Segoe UI", 7.5F);
        lblStatusInfo.ForeColor = Color.FromArgb(102, 115, 134);
        lblStatusInfo.Location = new Point(235, 8);
        lblStatusInfo.Name = "lblStatusInfo";
        lblStatusInfo.Size = new Size(425, 17);
        lblStatusInfo.TabIndex = 1;
        lblStatusInfo.Text = "Máy chủ HIS-PROD-01      •      CSDL Đã kết nối      •      Phiên bản 2.5.1";
        // 
        // lblStatusRight
        // 
        lblStatusRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblStatusRight.Font = new Font("Segoe UI", 7.5F);
        lblStatusRight.ForeColor = Color.FromArgb(102, 115, 134);
        lblStatusRight.Location = new Point(1175, 7);
        lblStatusRight.Name = "lblStatusRight";
        lblStatusRight.Size = new Size(274, 24);
        lblStatusRight.TabIndex = 2;
        lblStatusRight.Text = "Trạm TIEPDON-03          08:42:16";
        lblStatusRight.TextAlign = ContentAlignment.MiddleRight;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(238, 242, 246);
        ClientSize = new Size(1463, 1067);
        Controls.Add(workspacePanel);
        Controls.Add(sidebarPanel);
        Controls.Add(statusBarPanel);
        Controls.Add(menuBarPanel);
        Controls.Add(titleBarPanel);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.None;
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(1349, 933);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "HIS - Hệ thống quản lý bệnh viện";
        WindowState = FormWindowState.Maximized;
        menuSystem.ResumeLayout(false);
        titleBarPanel.ResumeLayout(false);
        titleBarPanel.PerformLayout();
        windowControlsPanel.ResumeLayout(false);
        menuBarPanel.ResumeLayout(false);
        sessionPanel.ResumeLayout(false);
        sidebarPanel.ResumeLayout(false);
        navHost.ResumeLayout(false);
        navHost.PerformLayout();
        toolsPanel.ResumeLayout(false);
        accountPanel.ResumeLayout(false);
        accountPanel.PerformLayout();
        facilityHost.ResumeLayout(false);
        facilityCard.ResumeLayout(false);
        facilityCard.PerformLayout();
        brandPanel.ResumeLayout(false);
        brandPanel.PerformLayout();
        brandMarkPanel.ResumeLayout(false);
        workspacePanel.ResumeLayout(false);
        toolbarPanel.ResumeLayout(false);
        toolbarPanel.PerformLayout();
        toolbarActionsPanel.ResumeLayout(false);
        toolbarActionsPanel.PerformLayout();
        statusBarPanel.ResumeLayout(false);
        statusBarPanel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private ContextMenuStrip menuSystem;
    private ToolStripMenuItem menuItemAdmin;
    private ToolTip toolTip;
    private System.Windows.Forms.Timer clockTimer;
    private TitleBarPanel titleBarPanel;
    private Label logoMark;
    private Label lblAppTitle;
    private Panel windowControlsPanel;
    private Button btnMinimize;
    private Button btnMaximize;
    private Button btnClose;
    private Panel menuBarPanel;
    private Button btnMenuSystem;
    private Button btnMenuBusiness;
    private Button btnMenuCatalog;
    private Button btnMenuReports;
    private Button btnMenuHelp;
    private Panel sessionPanel;
    private Label lblSessionDate;
    private Label lblSessionSecure;
    private Panel sidebarPanel;
    private Panel brandPanel;
    private Panel brandMarkPanel;
    private Label brandMarkLabel;
    private Label brandTitleLabel;
    private Label brandSubtitleLabel;
    private Panel facilityHost;
    private Panel facilityCard;
    private Label lblFacilityCaption;
    private Label lblFacilityName;
    private Label lblFacilityChevron;
    private Panel navHost;
    private Label lblNavGroupOverview;
    private NavButton btnNavDashboard;
    private Label lblNavGroupClinical;
    private NavButton btnNavReception;
    private Label lblBadgeReception;
    private NavButton btnNavPatients;
    private NavButton btnNavExamination;
    private Label lblBadgeExamination;
    private NavButton btnNavInpatient;
    private Label lblNavGroupParaclinical;
    private NavButton btnNavPharmacy;
    private NavButton btnNavReports;
    private Panel toolsPanel;
    private Button btnSettings;
    private Button btnHelp;
    private Panel accountPanel;
    private Label accountAvatar;
    private Label accountName;
    private Label accountRole;
    private Button btnLogout;
    private Panel workspacePanel;
    private Panel toolbarPanel;
    private Label lblBreadcrumb;
    private Panel toolbarActionsPanel;
    private TextBox txtGlobalSearch;
    private Button btnRefresh;
    private Button btnNotifications;
    private Button btnNewReception;
    private Panel toolbarLine;
    private Panel contentHostPanel;
    private Panel statusBarPanel;
    private Panel statusLine;
    private Label lblStatusSystem;
    private Label lblStatusInfo;
    private Label lblStatusRight;
}
