using System.IO.Ports;

namespace CaselloAutostradale;
public partial class FrmMain : Form
{
    private bool opened = false;
    private int carCount;
    private int carRefused;
    private ErrorProvider errorProvider;
    private readonly SerialPort? serialPort;
    private List<string> _allowedPlates;

    private bool _motorDelay = false;
    private bool _barrierDelay = false;

    public FrmMain()
    {
        InitializeComponent();
        _allowedPlates = [];
        var ports = SerialPort.GetPortNames();
        if (ports != null && ports.Length > 0)
        {
            serialPort = new(ports[0], 9600);
            serialPort.Open();
            serialPort.DataReceived += DataReceived;
        }
        else
        {
            MessageBox.Show("Nessuna porta seriale rilevata!\nRiprovare.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        errorProvider = new();
        PbBarrier.Image = Properties.Resources.closedBarrier;
        PbStopLight.Image = Properties.Resources.redStopLight;
        TbCarCount.Text = "" + 0;
        TbLastPlate.Text = "";
        TbRefused.Text = "" + 0;
    }

    private void DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort serial = (SerialPort)sender;
        string indata = serial.ReadLine();
        Console.WriteLine(indata);

        if (opened)
        {
            if (indata.StartsWith("rsp"))
            {
                PbBarrier.Image = Properties.Resources.openBarrier;
                PbStopLight.Image = Properties.Resources.greenStopLight;
            }
            else return;
        }

        opened = false;
        if (indata.Contains("BARRIER"))
        {
            if (indata.IndexOf("0") > -1)
                PbBarrier.Image = Properties.Resources.closedBarrier;
            else PbBarrier.Image = Properties.Resources.openBarrier;
        }
        else if (indata.Contains("LED", StringComparison.CurrentCultureIgnoreCase))
        {
            if (indata.IndexOf("0") > -1)
                PbStopLight.Image = Properties.Resources.greenStopLight;
            else PbStopLight.Image = Properties.Resources.redStopLight;
        }
        else if (indata.StartsWith("qst"))
        {
            if (_allowedPlates.Contains(indata.Substring(4).Replace("\r", "")))
            {
                WriteThroughSerial("rsp:true");
                opened = true;
                BeginInvoke(new Action(() =>
                {
                    TbLastPlate.Text = indata.Substring(4);
                    TbCarCount.Text = "" + (++carCount);
                }));
            }
            else
            {
                MessageBox.Show($"Targa {indata.Substring(4)} non ammessa", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BeginInvoke(new Action(() =>
                {
                    TbLastPlate.Text = indata.Substring(4);
                    TbRefused.Text = "" + (++carRefused);
                }));
            }
        }
    }

    private void WriteThroughSerial(string payload)
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Write(payload);
        }
        else
        {
            MessageBox.Show("Errore porta seriale!\nConfigurazione non inviata.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Simulate(object sender, EventArgs e)
    {
        if (opened)
        {
            PbBarrier.Image = Properties.Resources.closedBarrier;
            PbStopLight.Image = Properties.Resources.redStopLight;
            opened = !opened;
        }
        else
        {
            PbBarrier.Image = Properties.Resources.openBarrier;
            PbStopLight.Image = Properties.Resources.greenStopLight;
            opened = !opened;
        }
    }
    private void BtnSendConfig_Click(object sender, EventArgs e)
    {
        WriteThroughSerial($"CFG:{TbMotorDelay.Text};{TbBarrierDelay.Text}");
    }

    private void BtnManualUpDown_Click(object sender, EventArgs e)
    {
        WriteThroughSerial($"MAN_BARRIER");
    }
    private void BtnManualStoplight_Click(object sender, EventArgs e)
    {
        WriteThroughSerial($"MAN_LED");
    }
    private void TbMotorDelay_TextChanged(object sender, EventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        int result = -1;
        if (int.TryParse(textBox.Text, out result) && result >= 1)
        {
            errorProvider.SetError(textBox, String.Empty);
            if (textBox.Equals(TbMotorDelay))
                _motorDelay = true;
            else _barrierDelay = true;

            if (_motorDelay && _barrierDelay)
                BtnSendConfig.Enabled = true;
        }
        else
        {
            errorProvider.SetError(textBox, "Inserisci un numero maggiore di uno.");
            if (textBox.Equals(TbMotorDelay))
                _motorDelay = false;
            else _barrierDelay = false;
            BtnSendConfig.Enabled = false;
        }
    }
    private void TbAllowedPlates_TextChanged(object sender, EventArgs e)
    {
        _allowedPlates = [];
        foreach (string plate in TbAllowedPlates.Text.Split(";"))
        {
            _allowedPlates.Add(plate);
        }
    }
    private void TbLastPlate_Enter(object sender, EventArgs e)
    {
        ActiveControl = null;
    }
}