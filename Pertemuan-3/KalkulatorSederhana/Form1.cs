namespace KalkulatorSederhana;

public partial class Form1 : Form
{
    private readonly TextBox display = new();
    private decimal storedValue;
    private string pendingOperator = string.Empty;
    private bool resetDisplay;

    public Form1()
    {
        InitializeComponent();
        BuildInterface();
    }

    private void BuildInterface()
    {
        Text = "Kalkulator Sederhana";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(380, 560);
        MinimumSize = new Size(340, 500);
        BackColor = Color.FromArgb(245, 247, 250);
        Font = new Font("Segoe UI", 10F);
        KeyPreview = true;
        KeyDown += FormKeyDown;

        var title = new Label
        {
            Text = "KALKULATOR",
            Dock = DockStyle.Top,
            Height = 42,
            Padding = new Padding(4, 16, 0, 0),
            ForeColor = Color.FromArgb(80, 88, 102),
            Font = new Font("Segoe UI Semibold", 11F)
        };

        display.Dock = DockStyle.Top;
        display.Height = 78;
        display.Text = "0";
        display.ReadOnly = true;
        display.TabStop = false;
        display.TextAlign = HorizontalAlignment.Right;
        display.Font = new Font("Segoe UI Semibold", 30F);
        display.ForeColor = Color.FromArgb(35, 42, 52);
        display.BackColor = Color.White;
        display.BorderStyle = BorderStyle.FixedSingle;
        display.Margin = new Padding(0, 0, 0, 14);

        var buttons = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            RowCount = 5,
            Padding = new Padding(0, 14, 0, 0),
            BackColor = Color.Transparent
        };

        for (var column = 0; column < 4; column++)
            buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        for (var row = 0; row < 5; row++)
            buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

        var buttonLabels = new[,]
        {
            { "C", "DEL", "%", "/" },
            { "7", "8", "9", "*" },
            { "4", "5", "6", "-" },
            { "1", "2", "3", "+" },
            { "+/-", "0", ".", "=" }
        };

        for (var row = 0; row < 5; row++)
        {
            for (var column = 0; column < 4; column++)
            {
                var button = CreateButton(buttonLabels[row, column]);
                buttons.Controls.Add(button, column, row);
            }
        }

        Controls.Add(buttons);
        Controls.Add(display);
        Controls.Add(title);
    }

    private Button CreateButton(string text)
    {
        var button = new Button
        {
            Text = text,
            Dock = DockStyle.Fill,
            FlatStyle = FlatStyle.Flat,
            Margin = new Padding(5),
            Font = new Font("Segoe UI Semibold", 13F),
            BackColor = Color.White,
            ForeColor = Color.FromArgb(45, 52, 63),
            Cursor = Cursors.Hand,
            TabStop = false
        };
        button.FlatAppearance.BorderColor = Color.FromArgb(224, 228, 234);
        button.FlatAppearance.BorderSize = 1;

        if (text is "/" or "*" or "-" or "+" or "=")
        {
            button.BackColor = text == "=" ? Color.FromArgb(46, 125, 210) : Color.FromArgb(225, 239, 252);
            button.ForeColor = text == "=" ? Color.White : Color.FromArgb(32, 93, 151);
        }
        else if (text is "C" or "DEL" or "%" or "+/-")
        {
            button.BackColor = Color.FromArgb(238, 241, 245);
            button.ForeColor = Color.FromArgb(80, 88, 102);
        }

        button.Click += (_, _) => HandleInput(text);
        return button;
    }

    private void HandleInput(string input)
    {
        if (input is "." or ",")
        {
            if (resetDisplay)
            {
                display.Text = "0";
                resetDisplay = false;
            }
            if (!display.Text.Contains('.')) display.Text += ".";
            return;
        }

        if (decimal.TryParse(input, out _))
        {
            if (display.Text == "0" || resetDisplay) display.Text = input;
            else if (display.Text.Length < 16) display.Text += input;
            resetDisplay = false;
            return;
        }

        switch (input)
        {
            case "C":
                display.Text = "0";
                storedValue = 0;
                pendingOperator = string.Empty;
                resetDisplay = false;
                break;
            case "DEL":
                if (!resetDisplay && display.Text.Length > 1)
                    display.Text = display.Text[..^1];
                else if (!resetDisplay) display.Text = "0";
                break;
            case "+/-":
                if (display.Text != "0") display.Text = display.Text.StartsWith('-') ? display.Text[1..] : "-" + display.Text;
                break;
            case "%":
                display.Text = (decimal.Parse(display.Text) / 100).ToString("G");
                break;
            case "+" or "-" or "*" or "/":
                SetOperator(input);
                break;
            case "=":
                CalculateResult();
                break;
        }
    }

    private void SetOperator(string @operator)
    {
        if (!string.IsNullOrEmpty(pendingOperator) && !resetDisplay) CalculateResult();
        storedValue = decimal.Parse(display.Text);
        pendingOperator = @operator;
        resetDisplay = true;
    }

    private void CalculateResult()
    {
        if (string.IsNullOrEmpty(pendingOperator)) return;

        var currentValue = decimal.Parse(display.Text);
        if (pendingOperator == "/" && currentValue == 0)
        {
            display.Text = "Tidak bisa dibagi 0";
            pendingOperator = string.Empty;
            resetDisplay = true;
            return;
        }

        storedValue = pendingOperator switch
        {
            "+" => storedValue + currentValue,
            "-" => storedValue - currentValue,
            "*" => storedValue * currentValue,
            "/" => storedValue / currentValue,
            _ => currentValue
        };
        display.Text = storedValue.ToString("G");
        pendingOperator = string.Empty;
        resetDisplay = true;
    }

    private void FormKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) HandleInput("=");
        else if (e.KeyCode == Keys.Escape) HandleInput("C");
        else if (e.KeyCode == Keys.Back) HandleInput("DEL");
        else if (e.KeyCode == Keys.Add) HandleInput("+");
        else if (e.KeyCode == Keys.Subtract) HandleInput("-");
        else if (e.KeyCode == Keys.Multiply) HandleInput("*");
        else if (e.KeyCode == Keys.Divide) HandleInput("/");
    }
}
