using System.Drawing;
using System.Runtime.InteropServices;
using HIS.BLL;
using HIS.BLL.DTOs;

namespace HIS.WinForms.Dialogs;

public partial class NewReceptionDialog : Form
{
    private const string LookupHint = "Nhập thông tin để tra cứu hồ sơ đã có, hoặc bỏ trống để tạo bệnh nhân mới.";

    private readonly IReceptionService? _receptionService;

    private List<DepartmentOptionDto> _departments = new();
    private List<DoctorOptionDto> _doctors = new();
    private int? _selectedPatientId;
    private bool _loadingCombos;
    private bool _saving;

    [DllImport("user32.dll")]
    private static extern bool ReleaseCapture();

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    public int CreatedAppointmentId { get; private set; }

    public NewReceptionDialog() : this(null)
    {
    }

    public NewReceptionDialog(IReceptionService? receptionService)
    {
        _receptionService = receptionService;

        InitializeComponent();

        dtpDateOfBirth.MaxDate = DateTime.Today;
        dtpDateOfBirth.Value = DateTime.Today.AddYears(-30);
        dtpDateOfBirth.Checked = false;
        cboGender.SelectedIndex = 0;
        cboPriority.SelectedIndex = 0;

        WireEvents();

        Shown += async (_, _) => await LoadOptionsAsync();
    }

    private void WireEvents()
    {
        btnCloseDialog.Click += (_, _) => Close();
        btnCancel.Click += (_, _) => ResetForm();
        btnSave.Click += BtnSave_Click;

        btnLookup.Click += async (_, _) => await PerformLookupAsync();
        txtLookup.KeyDown += async (_, e) =>
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            await PerformLookupAsync();
        };

        lnkResetPatient.LinkClicked += (_, _) =>
        {
            ClearPatientSelection(clearFields: true);
            lblLookupResult.Text = LookupHint;
            lblLookupResult.ForeColor = Theme.Muted;
            txtLookup.Clear();
            txtFullName.Focus();
        };

        cboDepartment.SelectedIndexChanged += (_, _) =>
        {
            if (!_loadingCombos)
            {
                ApplyDoctorsForDepartment();
            }
        };

        foreach (Control control in new Control[] { headerPanel, iconPanel, lblDialogIcon, lblDialogTitle, lblDialogSubtitle })
        {
            control.MouseDown += Header_MouseDown;
        }
    }

    private void Header_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
        {
            return;
        }

        ReleaseCapture();
        SendMessage(Handle, 0x00A1, 2, 0);
    }

    private async Task LoadOptionsAsync()
    {
        if (_receptionService == null)
        {
            return;
        }

        UseWaitCursor = true;
        _loadingCombos = true;

        try
        {
            _departments = (await _receptionService.GetDepartmentsAsync()).ToList();
            _doctors = (await _receptionService.GetDoctorsAsync()).ToList();

            var departmentSource = new List<DepartmentOptionDto> { new(0, "— Chọn khoa/phòng —") };
            departmentSource.AddRange(_departments);

            cboDepartment.DataSource = null;
            cboDepartment.DisplayMember = nameof(DepartmentOptionDto.Name);
            cboDepartment.ValueMember = nameof(DepartmentOptionDto.Id);
            cboDepartment.DataSource = departmentSource;

            if (_departments.Count == 0)
            {
                lblLookupResult.Text = "Chưa có khoa/phòng trong hệ thống. Vui lòng nạp dữ liệu danh mục.";
                lblLookupResult.ForeColor = Theme.Amber;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Không tải được danh mục", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _loadingCombos = false;
            UseWaitCursor = false;
            ApplyDoctorsForDepartment();
        }
    }

    private void ApplyDoctorsForDepartment()
    {
        DepartmentOptionDto? department = cboDepartment.SelectedItem as DepartmentOptionDto;

        var doctorSource = new List<DoctorOptionDto> { new(0, department?.Id ?? 0, "— Chưa chỉ định bác sĩ —", string.Empty) };

        if (department is { Id: > 0 })
        {
            doctorSource.AddRange(_doctors.Where(d => d.DepartmentId == department.Id));
        }

        _loadingCombos = true;
        cboDoctor.DataSource = null;
        cboDoctor.DisplayMember = nameof(DoctorOptionDto.Name);
        cboDoctor.ValueMember = nameof(DoctorOptionDto.Id);
        cboDoctor.DataSource = doctorSource;
        cboDoctor.Enabled = doctorSource.Count > 1;
        _loadingCombos = false;
    }

    private async Task PerformLookupAsync()
    {
        if (_receptionService == null)
        {
            return;
        }

        string keyword = txtLookup.Text.Trim();

        if (keyword.Length == 0)
        {
            MessageBox.Show(this, "Vui lòng nhập số điện thoại, CCCD, mã BN hoặc họ tên cần tra cứu.", "Tra cứu hồ sơ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtLookup.Focus();
            return;
        }

        btnLookup.Enabled = false;
        UseWaitCursor = true;

        try
        {
            ReceptionPatientDto? patient = await _receptionService.FindPatientAsync(keyword);

            if (patient == null)
            {
                ClearPatientSelection(clearFields: false);
                lblLookupResult.Text = "Không tìm thấy hồ sơ phù hợp. Nhập thông tin bên dưới để tạo bệnh nhân mới.";
                lblLookupResult.ForeColor = Theme.Amber;
                return;
            }

            _selectedPatientId = patient.PatientId;
            txtFullName.Text = patient.FullName;
            txtPhone.Text = patient.PhoneNumber;
            txtNationalId.Text = patient.NationalId;
            txtInsurance.Text = patient.InsuranceCardNumber;
            txtAddress.Text = patient.Address;
            dtpDateOfBirth.Value = patient.DateOfBirth.ToDateTime(TimeOnly.MinValue);
            dtpDateOfBirth.Checked = true;
            cboGender.SelectedIndex = patient.Gender switch
            {
                "MALE" => 0,
                "FEMALE" => 1,
                "OTHER" => 2,
                _ => 0
            };

            SetIdentityReadOnly(true);
            lnkResetPatient.Visible = true;
            lblLookupResult.Text = $"Đã chọn hồ sơ {patient.PatientCode} · {patient.FullName}";
            lblLookupResult.ForeColor = Theme.Green;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Lỗi tra cứu", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnLookup.Enabled = true;
            UseWaitCursor = false;
        }
    }

    private async void BtnSave_Click(object? sender, EventArgs e)
    {
        if (_saving || _receptionService == null)
        {
            return;
        }

        if (!ValidateForm(out string message, out Control? focusControl))
        {
            MessageBox.Show(this, message, "Thông tin chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            focusControl?.Focus();
            return;
        }

        _saving = true;
        SetBusy(true);

        try
        {
            CreateReceptionResult result = await _receptionService.CreateReceptionAsync(BuildRequest());

            CreatedAppointmentId = result.AppointmentId;

            MessageBox.Show(
                this,
                $"Tiếp nhận thành công.\nMã lượt khám: {result.AppointmentCode}\nMã bệnh nhân: {result.PatientCode}\nSố thứ tự: {result.QueueNumber}",
                "Tiếp nhận thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Không thể tiếp nhận", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _saving = false;

            if (!IsDisposed && !Disposing)
            {
                SetBusy(false);
            }
        }
    }

    private CreateReceptionRequest BuildRequest()
    {
        if (cboDepartment.SelectedItem is not DepartmentOptionDto { Id: > 0 } department)
        {
            throw new InvalidOperationException("Khoa/phòng khám chưa được chọn.");
        }

        DoctorOptionDto? doctor = cboDoctor.SelectedItem as DoctorOptionDto;

        return new CreateReceptionRequest
        {
            PatientId = _selectedPatientId,
            FullName = txtFullName.Text.Trim(),
            DateOfBirth = DateOnly.FromDateTime(dtpDateOfBirth.Value),
            Gender = cboGender.SelectedIndex switch
            {
                0 => "MALE",
                1 => "FEMALE",
                _ => "OTHER"
            },
            PhoneNumber = txtPhone.Text.Trim(),
            NationalId = txtNationalId.Text.Trim(),
            Address = txtAddress.Text.Trim(),
            InsuranceCardNumber = txtInsurance.Text.Trim(),
            DepartmentId = department.Id,
            DoctorId = doctor?.Id > 0 ? doctor.Id : null,
            PriorityLevel = Math.Max(0, cboPriority.SelectedIndex),
            Reason = txtReason.Text.Trim()
        };
    }

    private bool ValidateForm(out string message, out Control? focusControl)
    {
        message = string.Empty;
        focusControl = null;

        if (string.IsNullOrWhiteSpace(txtFullName.Text))
        {
            message = "Vui lòng nhập họ tên bệnh nhân.";
            focusControl = txtFullName;
            return false;
        }

        if (txtFullName.Text.Trim().Length > 100)
        {
            message = "Họ tên bệnh nhân không được vượt quá 100 ký tự.";
            focusControl = txtFullName;
            return false;
        }

        if (!dtpDateOfBirth.Checked || dtpDateOfBirth.Value.Date > DateTime.Today)
        {
            message = "Vui lòng chọn ngày sinh của bệnh nhân.";
            focusControl = dtpDateOfBirth;
            return false;
        }

        string phone = new string(txtPhone.Text.Where(c => !char.IsWhiteSpace(c) && c != '.' && c != '-').ToArray());

        if (phone.Length is < 9 or > 15 || !phone.All(char.IsDigit))
        {
            message = "Số điện thoại phải gồm 9 đến 15 chữ số.";
            focusControl = txtPhone;
            return false;
        }

        if (txtNationalId.Text.Trim().Length > 20)
        {
            message = "Số CCCD/hộ chiếu không được vượt quá 20 ký tự.";
            focusControl = txtNationalId;
            return false;
        }

        if (txtInsurance.Text.Trim().Length > 20)
        {
            message = "Số BHYT không được vượt quá 20 ký tự.";
            focusControl = txtInsurance;
            return false;
        }

        if (cboDepartment.SelectedItem is not DepartmentOptionDto { Id: > 0 })
        {
            message = "Vui lòng chọn khoa/phòng khám.";
            focusControl = cboDepartment;
            return false;
        }

        return true;
    }

    private void ResetForm()
    {
        bool hasInput = !string.IsNullOrWhiteSpace(txtFullName.Text)
            || !string.IsNullOrWhiteSpace(txtPhone.Text)
            || !string.IsNullOrWhiteSpace(txtNationalId.Text)
            || !string.IsNullOrWhiteSpace(txtInsurance.Text)
            || !string.IsNullOrWhiteSpace(txtAddress.Text)
            || !string.IsNullOrWhiteSpace(txtReason.Text)
            || _selectedPatientId != null;

        if (hasInput)
        {
            DialogResult confirm = MessageBox.Show(
                this,
                "Xóa toàn bộ thông tin đã nhập trên biểu mẫu?",
                "Xác nhận hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }
        }

        ClearPatientSelection(clearFields: true);
        txtLookup.Clear();
        txtReason.Clear();

        _loadingCombos = true;
        cboDepartment.SelectedIndex = cboDepartment.Items.Count > 0 ? 0 : -1;
        cboPriority.SelectedIndex = 0;
        _loadingCombos = false;

        ApplyDoctorsForDepartment();
        lblLookupResult.Text = LookupHint;
        lblLookupResult.ForeColor = Theme.Muted;
        txtLookup.Focus();
    }

    private void ClearPatientSelection(bool clearFields)
    {
        _selectedPatientId = null;
        lnkResetPatient.Visible = false;
        SetIdentityReadOnly(false);

        if (!clearFields)
        {
            return;
        }

        txtFullName.Clear();
        txtPhone.Clear();
        txtNationalId.Clear();
        txtInsurance.Clear();
        txtAddress.Clear();
        cboGender.SelectedIndex = 0;
        dtpDateOfBirth.Checked = false;
    }

    private void SetIdentityReadOnly(bool readOnly)
    {
        Color background = readOnly ? Color.FromArgb(243, 246, 249) : Color.White;

        txtFullName.ReadOnly = readOnly;
        txtFullName.BackColor = background;
        txtNationalId.ReadOnly = readOnly;
        txtNationalId.BackColor = background;
        dtpDateOfBirth.Enabled = !readOnly;
        cboGender.Enabled = !readOnly;
    }

    private void SetBusy(bool busy)
    {
        btnSave.Enabled = !busy;
        btnCancel.Enabled = !busy;
        btnCloseFooter.Enabled = !busy;
        btnCloseDialog.Enabled = !busy;
        btnLookup.Enabled = !busy;
        btnSave.Text = busy ? "Đang lưu..." : "Lưu và tiếp nhận";
        UseWaitCursor = busy;
    }
}
