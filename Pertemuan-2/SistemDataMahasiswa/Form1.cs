using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace SistemDataMahasiswa; 

public partial class Form1 : Form
{
        private MahasiswaService _layananData;
        
        private DataGridView _tabelVisual;
        private TextBox _kolomNRP, _kolomNama, _kolomPencarian, _kolomIPK;
        private ComboBox _pilihanJurusan;
        private Button _tombolSimpan, _tombolReset, _tombolHapus, _tombolCari, _tombolEdit;

        private bool _sedangModeEdit = false;

        private Color WarnaBackground = Color.FromArgb(244, 247, 251);
        private Color WarnaPanelAtas = Color.FromArgb(23, 42, 70);
        private Color WarnaTeksPanel = Color.White;
        private Color WarnaAksenSimpan = Color.FromArgb(16, 185, 129);
        private Color WarnaAksenReset = Color.FromArgb(100, 116, 139);
        private Color WarnaTeksUtama = Color.FromArgb(30, 41, 59);
        private Color WarnaTeksSekunder = Color.FromArgb(100, 116, 139);
        private Color WarnaGaris = Color.FromArgb(226, 232, 240);

        public Form1()
        {
            InitializeComponent();
            _layananData = new MahasiswaService();
            PerbaruiTabel();
        }

        private void RakitTampilan()
        {
            this.Text = "Sistem Data Mahasiswa";
            this.Size = new Size(900, 650);
            this.MinimumSize = new Size(900, 620);
            this.BackColor = WarnaBackground;
            this.Font = new Font("Segoe UI", 10F);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 96,
                BackColor = WarnaPanelAtas,
                Padding = new Padding(28, 17, 28, 12)
            };

            Label lblJudul = new Label
            {
                Text = "DATA MAHASISWA",
                Location = new Point(28, 15),
                AutoSize = true,
                ForeColor = WarnaTeksPanel,
                Font = new Font("Segoe UI", 19F, FontStyle.Bold)
            };
            Label lblSubjudul = new Label
            {
                Text = "Kelola data akademik dengan rapi dan cepat",
                Location = new Point(31, 53),
                AutoSize = true,
                ForeColor = Color.FromArgb(186, 201, 222),
                Font = new Font("Segoe UI", 9.5F)
            };
            panelHeader.Controls.AddRange(new Control[] { lblJudul, lblSubjudul });

            Panel panelForm = new Panel
            {
                Dock = DockStyle.Top,
                Height = 164,
                BackColor = WarnaPanelAtas,
                Padding = new Padding(24, 16, 24, 12)
            };

            Label lblForm = new Label { Text = "Tambah / Perbarui Data", Location = new Point(24, 14), AutoSize = true, ForeColor = Color.FromArgb(219, 234, 254), Font = new Font("Segoe UI", 11F, FontStyle.Bold) };
            Label lblNrp = new Label { Text = "NRP", Location = new Point(24, 48), AutoSize = true, ForeColor = WarnaTeksPanel, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            _kolomNRP = new TextBox { Location = new Point(24, 70), Width = 155, Font = new Font("Segoe UI", 10F), BorderStyle = BorderStyle.FixedSingle };

            Label lblNama = new Label { Text = "NAMA LENGKAP", Location = new Point(200, 48), AutoSize = true, ForeColor = WarnaTeksPanel, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            _kolomNama = new TextBox { Location = new Point(200, 70), Width = 250, Font = new Font("Segoe UI", 10F), BorderStyle = BorderStyle.FixedSingle };

            Label lblJurusan = new Label { Text = "PROGRAM STUDI", Location = new Point(470, 48), AutoSize = true, ForeColor = WarnaTeksPanel, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            _pilihanJurusan = new ComboBox { Location = new Point(470, 69), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10F), FlatStyle = FlatStyle.Flat };
            
            _pilihanJurusan.Items.AddRange(new string[] { 
                "Teknik Informatika", 
                "Sistem Informasi", 
                "Teknologi Informasi",
                "Teknik Komputer", 
                "Rekayasa Perangkat Lunak",
                "Sains Data",
                "Kecerdasan Buatan",
                "Teknik Elektro",
                "Teknik Biomedik",
                "Teknik Industri"
            });

            Label lblIpk = new Label { Text = "IPK (0 - 4)", Location = new Point(630, 48), AutoSize = true, ForeColor = WarnaTeksPanel, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            _kolomIPK = new TextBox { Location = new Point(630, 70), Width = 75, Font = new Font("Segoe UI", 10F), BorderStyle = BorderStyle.FixedSingle };

            _tombolSimpan = new Button 
            { 
                Text = "SIMPAN DATA",
                Location = new Point(730, 35),
                Width = 130,
                Height = 42,
                BackColor = Color.FromArgb(245, 158, 11),
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            _tombolSimpan.FlatAppearance.BorderSize = 0;
            _tombolSimpan.Click += AksiKlikSimpan;

            _tombolReset = new Button 
            { 
                Text = "BATAL / RESET",
                Location = new Point(730, 84),
                Width = 130,
                Height = 42,
                BackColor = Color.FromArgb(100, 116, 139),
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            _tombolReset.FlatAppearance.BorderSize = 0;
            _tombolReset.Click += (s, e) => KosongkanForm();

            panelForm.Controls.AddRange(new Control[] { lblForm, lblNrp, _kolomNRP, lblNama, _kolomNama, lblJurusan, _pilihanJurusan, lblIpk, _kolomIPK, _tombolSimpan, _tombolReset });

            Panel panelTengah = new Panel { Dock = DockStyle.Top, Height = 68, BackColor = WarnaBackground, Padding = new Padding(24, 15, 24, 10) };
            
            _kolomPencarian = new TextBox { Location = new Point(24, 18), Width = 280, PlaceholderText = "Cari berdasarkan NRP atau nama...", Font = new Font("Segoe UI", 10F), BorderStyle = BorderStyle.FixedSingle };
            
            _tombolCari = new Button { Text = "CARI", Location = new Point(314, 16), Width = 84, Height = 32, BackColor = Color.FromArgb(37, 99, 235), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            _tombolCari.FlatAppearance.BorderSize = 0;
            _tombolCari.Click += AksiKlikCari;
            
            _tombolEdit = new Button { Text = "Edit Data Terpilih", Location = new Point(540, 16), Width = 150, Height = 32, BackColor = Color.FromArgb(14, 165, 233), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            _tombolEdit.FlatAppearance.BorderSize = 0;
            _tombolEdit.Click += AksiKlikMulaiEdit;

            _tombolHapus = new Button { Text = "Hapus Data Terpilih", Location = new Point(710, 16), Width = 150, Height = 32, BackColor = Color.FromArgb(220, 38, 38), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            _tombolHapus.FlatAppearance.BorderSize = 0;
            _tombolHapus.Click += AksiKlikHapus;

            panelTengah.Controls.AddRange(new Control[] { _kolomPencarian, _tombolCari, _tombolEdit, _tombolHapus });

            _tabelVisual = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                EnableHeadersVisualStyles = false,
                GridColor = WarnaGaris,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                AllowUserToResizeRows = false,
                RowTemplate = { Height = 34 }
            };

            _tabelVisual.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(226, 232, 240);
            _tabelVisual.ColumnHeadersDefaultCellStyle.ForeColor = WarnaTeksUtama;
            _tabelVisual.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            _tabelVisual.ColumnHeadersHeight = 38;
            _tabelVisual.DefaultCellStyle.BackColor = Color.White;
            _tabelVisual.DefaultCellStyle.ForeColor = WarnaTeksUtama;
            _tabelVisual.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            _tabelVisual.DefaultCellStyle.SelectionForeColor = WarnaTeksUtama;
            _tabelVisual.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            _tabelVisual.DoubleClick += AksiKlikMulaiEdit; 

            this.Controls.Add(_tabelVisual);
            this.Controls.Add(panelTengah);
            this.Controls.Add(panelForm);
            this.Controls.Add(panelHeader);
        }

        private void AksiKlikMulaiEdit(object sender, EventArgs e)
        {
            if (_tabelVisual.CurrentRow != null)
            {
                _kolomNRP.Text = _tabelVisual.CurrentRow.Cells["NRP"].Value.ToString();
                _kolomNama.Text = _tabelVisual.CurrentRow.Cells["NamaLengkap"].Value.ToString();
                _pilihanJurusan.Text = _tabelVisual.CurrentRow.Cells["ProgramStudi"].Value.ToString();
                _kolomIPK.Text = _tabelVisual.CurrentRow.Cells["IPK"].Value.ToString();

                _sedangModeEdit = true;
                _kolomNRP.Enabled = false;
                _tombolSimpan.Text = "PERBARUI DATA";
                _tombolSimpan.BackColor = Color.DarkOrange;
            }
            else
            {
                MessageBox.Show("Silakan pilih data di tabel terlebih dahulu untuk diedit.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AksiKlikSimpan(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_kolomNRP.Text) || string.IsNullOrWhiteSpace(_kolomNama.Text) || 
                string.IsNullOrWhiteSpace(_pilihanJurusan.Text) || string.IsNullOrWhiteSpace(_kolomIPK.Text))
            {
                MessageBox.Show("Semua kolom (NRP, Nama Lengkap, Program Studi, dan IPK) wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_sedangModeEdit && _layananData.CekNrpTersedia(_kolomNRP.Text))
            {
                MessageBox.Show("NRP sudah terdaftar! Harap gunakan NRP yang berbeda.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string inputIpk = _kolomIPK.Text.Replace(',', '.'); 
            if (!double.TryParse(inputIpk, NumberStyles.Any, CultureInfo.InvariantCulture, out double nilaiIpk))
            {
                MessageBox.Show("Format IPK tidak valid! Harap masukkan angka.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nilaiIpk < 0.0 || nilaiIpk > 4.0)
            {
                MessageBox.Show("Input IPK ditolak! Harap masukkan nilai antara 0.00 hingga 4.00.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var entriMahasiswa = new Mahasiswa
            {
                NRP = _kolomNRP.Text,
                NamaLengkap = _kolomNama.Text,
                ProgramStudi = _pilihanJurusan.Text,
                IPK = Math.Round(nilaiIpk, 2) 
            };

            if (_sedangModeEdit)
            {
                _layananData.PerbaruiData(entriMahasiswa);
                MessageBox.Show("Data berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _layananData.RegistrasiDataBaru(entriMahasiswa);
            }

            KosongkanForm();
            PerbaruiTabel();
        }

        private void AksiKlikHapus(object sender, EventArgs e)
        {
            if (_tabelVisual.CurrentRow != null)
            {
                string idHapus = _tabelVisual.CurrentRow.Cells["NRP"].Value.ToString();
                DialogResult konfirmasi = MessageBox.Show($"Yakin ingin menghapus data dengan NRP {idHapus}?", "Konfirmasi Penghapusan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (konfirmasi == DialogResult.Yes)
                {
                    _layananData.HapusBerdasarkanNRP(idHapus);
                    PerbaruiTabel();
                    KosongkanForm();
                }
            }
        }

        private void AksiKlikCari(object sender, EventArgs e)
        {
            string kataKunci = _kolomPencarian.Text.Trim();
            
            if (string.IsNullOrWhiteSpace(kataKunci))
            {
                MessageBox.Show("Silakan ketik NRP atau Nama terlebih dahulu di kolom pencarian!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var hasilPencarian = _layananData.SaringData(kataKunci);

            if (hasilPencarian.Count > 0)
            {
                StringBuilder pesan = new StringBuilder();
                pesan.AppendLine("DATA DITEMUKAN");
                pesan.AppendLine("--------------------------------------------------");
                
                foreach (var mhs in hasilPencarian)
                {
                    pesan.AppendLine($"NRP\t\t: {mhs.NRP}");
                    pesan.AppendLine($"Nama\t\t: {mhs.NamaLengkap}");
                    pesan.AppendLine($"Program Studi\t: {mhs.ProgramStudi}");
                    pesan.AppendLine($"IPK\t\t: {mhs.IPK}");
                    pesan.AppendLine("--------------------------------------------------");
                }

                MessageBox.Show(pesan.ToString(), "Hasil Pencarian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _kolomPencarian.Clear(); 
            }
            else
            {
                MessageBox.Show($"Data dengan kata kunci '{kataKunci}' tidak ditemukan.", "Hasil Pencarian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            PerbaruiTabel();
        }

        private void PerbaruiTabel()
        {
            _tabelVisual.DataSource = null;
            _tabelVisual.DataSource = _layananData.DapatkanSemuaArsip();
        }

        private void KosongkanForm()
        {
            _kolomNRP.Clear();
            _kolomNama.Clear();
            _pilihanJurusan.SelectedIndex = -1;
            _kolomIPK.Clear();
            
            _sedangModeEdit = false;
            _kolomNRP.Enabled = true;
            _tombolSimpan.Text = "SIMPAN DATA";
            _tombolSimpan.BackColor = WarnaAksenSimpan;
            
            _kolomNRP.Focus(); 
        }
    }
