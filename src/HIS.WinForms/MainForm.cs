using HIS.BLL;

namespace HIS.WinForms;

public partial class MainForm : Form
{
    private readonly IBookingService _bookingService;

    public MainForm(IBookingService bookingService)
    {
        _bookingService = bookingService;
        InitializeComponent();
    }
}
