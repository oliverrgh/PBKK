using System.Globalization;

namespace KalkulatorSederhana;

public partial class Form1 : Form
{
    private readonly TextBox display = new();
    private readonly Label expressionDisplay = new();
    private decimal storedValue;
    private string pendingOperator = string.Empty;
    private bool resetDisplay;
    private bool isError;

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

        var displayPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 88,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(8, 4, 8, 4),
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Margin = new Padding(0, 0, 0, 14)
        };
        displayPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
        displayPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        expressionDisplay.Dock = DockStyle.Fill;
        expressionDisplay.TextAlign = ContentAlignment.MiddleRight;
        expressionDisplay.ForeColor = Color.FromArgb(145, 151, 160);
        expressionDisplay.Font = new Font("Segoe UI", 10F);

        display.Dock = DockStyle.Fill;
        display.Text = "0";
        display.ReadOnly = true;
        display.TabStop = false;
        display.TextAlign = HorizontalAlignment.Right;
        display.Font = new Font("Segoe UI Semibold", 30F);
        display.ForeColor = Color.FromArgb(35, 42, 52);
        display.BackColor = Color.White;
        display.BorderStyle = BorderStyle.None;
        display.Margin = Padding.Empty;
        displayPanel.Controls.Add(expressionDisplay, 0, 0);
        displayPanel.Controls.Add(display, 0, 1);

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
        Controls.Add(displayPanel);
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
        if (isError && input != "C")
        {
            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out _) || input is "." or ",")
            {
                display.Text = "0";
                expressionDisplay.Text = string.Empty;
                storedValue = 0;
                pendingOperator = string.Empty;
                resetDisplay = false;
                isError = false;
            }
            else
            {
                return;
            }
        }

        if (input is "." or ",")
        {
            if (resetDisplay)
            {
                if (string.IsNullOrEmpty(pendingOperator)) expressionDisplay.Text = string.Empty;
                display.Text = "0";
                resetDisplay = false;
            }
            if (!display.Text.Contains('.')) display.Text += ".";
            UpdateExpressionDisplay();
            return;
        }

        if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out _))
        {
            if (resetDisplay && string.IsNullOrEmpty(pendingOperator)) expressionDisplay.Text = string.Empty;
            if (display.Text == "0" || resetDisplay) display.Text = input;
            else if (display.Text.Length < 16) display.Text += input;
            resetDisplay = false;
            UpdateExpressionDisplay();
            return;
        }

        switch (input)
        {
            case "C":
                display.Text = "0";
                storedValue = 0;
                pendingOperator = string.Empty;
                resetDisplay = false;
                isError = false;
                expressionDisplay.Text = string.Empty;
                break;
            case "DEL":
                if (!resetDisplay && display.Text.Length > 1)
                    display.Text = display.Text[..^1];
                else if (!resetDisplay) display.Text = "0";
                UpdateExpressionDisplay();
                break;
            case "+/-":
                if (display.Text != "0") display.Text = display.Text.StartsWith('-') ? display.Text[1..] : "-" + display.Text;
                UpdateExpressionDisplay();
                break;
            case "%":
                display.Text = FormatValue(ParseDisplayValue() / 100);
                UpdateExpressionDisplay();
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
        storedValue = ParseDisplayValue();
        pendingOperator = @operator;
        resetDisplay = true;
        UpdateExpressionDisplay();
    }

    private void CalculateResult()
    {
        if (string.IsNullOrEmpty(pendingOperator)) return;

        var currentValue = ParseDisplayValue();
        expressionDisplay.Text = $"{FormatValue(storedValue)} {GetOperatorSymbol(pendingOperator)} {FormatValue(currentValue)} =";
        if (pendingOperator == "/" && currentValue == 0)
        {
            display.Text = "Error";
            pendingOperator = string.Empty;
            resetDisplay = true;
            isError = true;
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
        display.Text = FormatValue(storedValue);
        pendingOperator = string.Empty;
        resetDisplay = true;
    }

    private void UpdateExpressionDisplay()
    {
        if (string.IsNullOrEmpty(pendingOperator)) return;

        var expression = $"{FormatValue(storedValue)} {GetOperatorSymbol(pendingOperator)}";
        if (!resetDisplay) expression += $" {display.Text}";
        expressionDisplay.Text = expression;
    }

    private static string GetOperatorSymbol(string @operator) => @operator switch
    {
        "*" => "×",
        "/" => "÷",
        _ => @operator
    };

    private decimal ParseDisplayValue() => decimal.Parse(display.Text, CultureInfo.InvariantCulture);

    private static string FormatValue(decimal value) => value.ToString("G", CultureInfo.InvariantCulture);

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
