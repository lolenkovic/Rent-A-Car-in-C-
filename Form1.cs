using System.Data.OleDb;

namespace RentACar_tvp
{
    public partial class Form1 : Form
    {

        Baza baza;
        List<Vozilo> vozila;

        public Form1()
        {
            InitializeComponent();
            baza = new Baza(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Test\Downloads\RentACarDB.accdb");
            vozila = new List<Vozilo>();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            UcitajVozila();
            UcitajKategorije();
            trackCenaFilter.ValueChanged += TrackCenaFilter_ValueChanged;
            await AnimacijaVozilaSaNajviseRez();
        }

        private void TrackCenaFilter_ValueChanged(object? sender, EventArgs e)
        {
            lblCena.Text = trackCenaFilter.Value.ToString();
        }

        private void UcitajVozila()
        {
            try
            {
                baza.otvorikonekciju();

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = baza.Conn;
                cmd.CommandText = "select * from vozilo";
                OleDbDataReader dr = cmd.ExecuteReader();

                vozila.Clear();

                while (dr.Read())
                {
                    Vozilo v = new Vozilo();
                    v.Id_vozila = int.Parse(dr["id_vozila"].ToString());
                    v.Id_kategorije = int.Parse(dr["id_kategorije"].ToString());
                    v.Naziv = dr["naziv"].ToString();
                    v.Marka = dr["marka"].ToString();
                    v.Model = dr["model"].ToString();
                    v.Godina_proizvodnje = int.Parse(dr["godina_proizvodnje"].ToString());
                    v.Cena_po_satu = int.Parse(dr["cena_po_satu"].ToString());

                    vozila.Add(v);
                }

                dtgVozila.DataSource = null;
                dtgVozila.DataSource = vozila;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom prikazivanja vozila iz baze podataka: " + ex.Message);
            }
            finally
            {
                baza.zatvorikonekciju();
            }
        }

        private void UcitajKategorije()
        {
            try
            {
                baza.otvorikonekciju();

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = baza.Conn;
                cmd.CommandText = "select * from kategorija";

                OleDbDataReader dr = cmd.ExecuteReader();

                List<Kategorija> kategorije = new List<Kategorija>();
                //kad budes dodavao kategorije, mozda prebaci ovo na nivo cele forme -> gore.

                while (dr.Read())
                {
                    Kategorija k = new Kategorija();
                    k.Id_kategorije = int.Parse(dr["id_kategorije"].ToString());
                    k.Naziv = dr["naziv"].ToString();
                    k.Opis = dr["opis"].ToString();

                    kategorije.Add(k);
                }

                cmbKategorijaFilter.DataSource = kategorije;
                cmbKategorijaFilter.DisplayMember = "Naziv";
                cmbKategorijaFilter.ValueMember = "Id_kategorije";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom prikazivanja kategorija iz baze podataka: " + ex.Message);
            }
            finally
            {
                baza.zatvorikonekciju();
            }
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                baza.otvorikonekciju();

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = baza.Conn;
                cmd.CommandText = "select * from vozilo where cena_po_satu <= @cena AND godina_proizvodnje BETWEEN @prvi AND @drugi AND id_kategorije = @kategorija";

                cmd.Parameters.Add("cena", trackCenaFilter.Value);
                cmd.Parameters.Add("prvi", datumprviFilter.Value.Year);
                cmd.Parameters.Add("drugi", datumdrugifilter.Value.Year);
                cmd.Parameters.Add("kategorija", cmbKategorijaFilter.SelectedValue);

                OleDbDataReader dr = cmd.ExecuteReader();

                vozila.Clear();

                while (dr.Read())
                {
                    Vozilo v = new Vozilo();
                    v.Id_vozila = int.Parse(dr["id_vozila"].ToString());
                    v.Id_kategorije = int.Parse(dr["id_kategorije"].ToString());
                    v.Naziv = dr["naziv"].ToString();
                    v.Marka = dr["marka"].ToString();
                    v.Model = dr["model"].ToString();
                    v.Godina_proizvodnje = int.Parse(dr["godina_proizvodnje"].ToString());
                    v.Cena_po_satu = int.Parse(dr["cena_po_satu"].ToString());

                    vozila.Add(v);
                }
                //int br = (int)cmd.ExecuteScalar(); if (br > 0) MessageBox.Show("Postoji"); else MessageBox.Show("Ne postoji");

                dtgVozila.DataSource = null;
                dtgVozila.DataSource = vozila;

                MessageBox.Show("Uspešna primena filtera!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom primene filtera: " + ex.Message);
            }
            finally
            {
                baza.zatvorikonekciju();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormaDodajKategoriju frmkat = new FormaDodajKategoriju();
            frmkat.ShowDialog();
        }

        private void btnRedirectKaRezervacijama_Click(object sender, EventArgs e)
        {
            FormaRezervacije frm = new FormaRezervacije();
            frm.ShowDialog();
        }

        private void btnDodajVozilo_Click(object sender, EventArgs e)
        {
            FormaDodajVozilo frmvoz = new FormaDodajVozilo();
            frmvoz.DodatoVozilo += Frmvoz_DodatoVozilo;
            frmvoz.ShowDialog();
        }

        private void Frmvoz_DodatoVozilo(object? sender, EventArgs e)
        {
            UcitajVozila();
        }



        private void UcitajVozilaZaANimaciju()
        {
            try
            {
                baza.otvorikonekciju();

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = baza.Conn;
                cmd.CommandText = "select * from vozilo";
                OleDbDataReader dr = cmd.ExecuteReader();

                vozila.Clear();

                while (dr.Read())
                {
                    Vozilo v = new Vozilo();
                    v.Id_vozila = int.Parse(dr["id_vozila"].ToString());
                    v.Id_kategorije = int.Parse(dr["id_kategorije"].ToString());
                    v.Naziv = dr["naziv"].ToString();
                    v.Marka = dr["marka"].ToString();
                    v.Model = dr["model"].ToString();
                    v.Godina_proizvodnje = int.Parse(dr["godina_proizvodnje"].ToString());
                    v.Cena_po_satu = int.Parse(dr["cena_po_satu"].ToString());

                    vozila.Add(v);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska prilikom prikazivanja vozila iz baze podataka ( za animaciju ): " + ex.Message);
            }
            finally
            {
                baza.zatvorikonekciju();
            }
        }

        private async Task UcitajVozilaAsync()
        {
            await Task.Run(UcitajVozilaZaANimaciju);
        }


        private async Task<Vozilo> VratiVoziloSaNajviseRez()
        {
            await UcitajVozilaAsync();
            try
            {
                baza.otvorikonekciju();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = baza.Conn;
                cmd.CommandText = "select id_vozila, count(*) AS brojrez from Rezervacija group by id_vozila order by count(*) DESC";
                OleDbDataReader dr = cmd.ExecuteReader();

                int idvracenog = -1;
                int maxrez = -1;

                if (dr.Read())
                {
                    idvracenog = int.Parse(dr["id_vozila"].ToString());
                    maxrez = int.Parse(dr["brojrez"].ToString());
                }

                foreach(var v in vozila)
                {
                    if(v.Id_vozila == idvracenog)
                    {
                        return v;
                    }
                }

                return null;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska prilikom pronalazenja vozila sa najvise rez: " + ex.Message);
                return null;
            }
            finally
            {
                baza.zatvorikonekciju();
            }
        }


        private async Task AnimacijaVozilaSaNajviseRez()
        {
            Vozilo vozilonaj = await VratiVoziloSaNajviseRez();

            if(vozilonaj != null)
            {
                labelNaj.Text = vozilonaj.Model;

                Random rnd = new Random();
                for (int i=0;i<10;i++)
                {
                    labelNaj.ForeColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    await Task.Delay(1000);
                }

            }
        }



    }
}
