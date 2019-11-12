using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using DevExpress.XtraEditors;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using DevExpress.XtraNavBar;
using DevExpress.XtraGrid.Views.Grid;

namespace WeatherApp
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        bool accept = false;

        // this method doing fil six city information like static..
        void fillSixCity()
        {
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("Loading of city information...");
            splashScreenManager1.SetWaitFormDescription("Please wait, data is loading...");

            #region Paris
            List<Info> dataParis = GetReport(615702, DateTime.Parse(DateTime.Now.ToShortDateString()));
            foreach (Info infoParis in dataParis)
            {
                picParis.Load(infoParis.weatherStatus.Icon);
                lblParisMax.Text = infoParis.Max_Temp + " °C";
                lblParisMin.Text = infoParis.Min_Temp + " °C";
                lblParisName.Text = infoParis.weatherStatus.Name;
                lblParisNow.Text = infoParis.The_Temp + " °C";
                break;
            }
            #endregion

            #region London
            List<Info> dataLondon = GetReport(44418, DateTime.Parse(DateTime.Now.ToShortDateString()));
            foreach (Info Londoninfo in dataLondon)
            {
                picLondon.Load(Londoninfo.weatherStatus.Icon);
                lblLondonMax.Text = Londoninfo.Max_Temp + " °C";
                lblLondonMin.Text = Londoninfo.Min_Temp + " °C";
                lblLondonName.Text = Londoninfo.weatherStatus.Name;
                lblLondonNow.Text = Londoninfo.The_Temp + " °C";
                break;
            }
            #endregion

            #region Amsterdam
            List<Info> dataAms = GetReport(727232, DateTime.Parse(DateTime.Now.ToShortDateString()));
            foreach (Info Amsinfo in dataAms)
            {
                picAms.Load(Amsinfo.weatherStatus.Icon);
                lblAmsterdamMax.Text = Amsinfo.Max_Temp + " °C";
                lblAmsterdamMin.Text = Amsinfo.Min_Temp + " °C";
                lblAmsterdamName.Text = Amsinfo.weatherStatus.Name;
                lblAmsterdamNow.Text = Amsinfo.The_Temp + " °C";
                break;
            }
            #endregion

            #region Berlin
            List<Info> dataBerlin = GetReport(638242, DateTime.Parse(DateTime.Now.ToShortDateString()));
            foreach (Info Berinfo in dataBerlin)
            {
                picBerlin.Load(Berinfo.weatherStatus.Icon);
                lblBerlinMax.Text = Berinfo.Max_Temp + " °C";
                lblBerlinMin.Text = Berinfo.Min_Temp + " °C";
                lblBerlinName.Text = Berinfo.weatherStatus.Name;
                lblBerlinNow.Text = Berinfo.The_Temp + " °C";
                break;
            }
            #endregion

            #region Istanbul
            List<Info> dataIst = GetReport(2344116, DateTime.Parse(DateTime.Now.ToShortDateString()));
            foreach (Info Istinfo in dataIst)
            {
                Picist.Load(Istinfo.weatherStatus.Icon);
                lblistMax.Text = Istinfo.Max_Temp + " °C";
                lblistMin.Text = Istinfo.Min_Temp + " °C";
                lblistName.Text = Istinfo.weatherStatus.Name;
                lblistNow.Text = Istinfo.The_Temp + " °C";
                break;
            }
            #endregion

            #region Madrid
            List<Info> dataMadrid = GetReport(766273, DateTime.Parse(DateTime.Now.ToShortDateString()));
            foreach (Info Madinfo in dataMadrid)
            {
                picMadrid.Load(Madinfo.weatherStatus.Icon);
                lblMadridMax.Text = Madinfo.Max_Temp + " °C";
                lblMinMax.Text = Madinfo.Min_Temp + " °C";
                lblMadridName.Text = Madinfo.weatherStatus.Name;
                lblMadridNow.Text = Madinfo.The_Temp + " °C";
            }
            #endregion

            splashScreenManager1.CloseWaitForm();
        }

        // this method doing form settings in start..
        void setForm()
        {
            List<string> Citys = new List<string>();
            Citys.Add("Istanbul");
            Citys.Add("Ankara");
            Citys.Add("Izmir");
            Citys.Add("Tokyo");
            Citys.Add("Berlin");
            Citys.Add("Amsterdam");
            Citys.Add("London");
            Citys.Add("Moscow");

            ledCity.Properties.DataSource = Citys.ToList();

            lblDate.Text = DateTime.Now.DayOfWeek.ToString() + "," + DateTime.Now.ToShortTimeString();
            dtpDate.DateTime = DateTime.Parse(DateTime.Now.ToShortDateString());

            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Day", typeof(string));
            dt.Columns.Add("Weather State", typeof(string));
            dt.Columns.Add("Now Temp", typeof(string));
            dt.Columns.Add("Max Temp", typeof(string));
            dt.Columns.Add("Min Temp", typeof(string));
            dt.Columns.Add("Icon", typeof(Image));


            gridControl1.DataSource = dt;

        }

        #region Find info and Fill


        private static string GetCityInfo(string cityName)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(cityName);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }

        }

        // this method doing start api and return json city info from url..
        private static List<City> GetLocationInfo(string location)
        {

            string url = "https://www.metaweather.com/api/location/search/?query=" + location;
            return JsonConvert.DeserializeObject<List<City>>(GetCityInfo(url));

        }

        // this method doing download city information with the city name parameter
        private void FillLocationData(string location)
        {
            List<City> Locations = GetLocationInfo(location);
            if (string.IsNullOrEmpty(location))
            {
                accept = false;
                XtraMessageBox.Show("Service is did not found..n\rPlease check your connection and try again..", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Locations.Count == 0)
            {
                accept = false;
                XtraMessageBox.Show("not found any information about this city..\n\rPlease try another city name.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Locations.Count == 1)
            {
                accept = true;
                setLocation(Locations[0]);
            }


        }

        // this method doing fill City class from FillLocationData class
        private void setLocation(City city)
        {
            SetLocation.setLocation = city;
        }

        // this methods doing fill Info Class with the parameters..
        private static List<Info> GetReport(int id)
        {
            string strURL = "https://www.metaweather.com/api/location/" + id;
            string inf = GetCityInfo(strURL);

            if (inf == "")
                return null;

            JObject j = JObject.Parse(inf);
            return JsonConvert.DeserializeObject<List<Info>>(j["consolidated_weather"].ToString());
        }

        private static List<Info> GetReport(int id, DateTime date)
        {
            string year = date.Year.ToString();
            string month = date.Month.ToString();
            string day = date.Day.ToString();
            string strURL = "https://www.metaweather.com/api/location/" + id + "/" + year + "/" + month + "/" + day;
            string inf = GetCityInfo(strURL);

            if (inf == "")
                return null;

            JArray j = JArray.Parse(inf);
            return JsonConvert.DeserializeObject<List<Info>>(j.ToString());
        }

        // this method doing print info the screen..
        void ShowInfo(int id)
        {

            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("Loading of city information...");
            splashScreenManager1.SetWaitFormDescription("Please wait, data is loading...");

            dt.Rows.Clear();
            List<Info> dataOne = GetReport(id);

            foreach (Info info in dataOne)
            {

                if (info.Applicable_Date.ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    picIco.Load(info.weatherStatus.Icon);
                    lblCity.Text = SetLocation.setLocation.title;
                    lblMin.Text = info.Max_Temp + " °C";
                    lblMax.Text = info.Min_Temp + " °C";
                    lblWind.Text = info.wind_speed.ToString().Substring(0, 3) + "KM/H";
                    lblHumidty.Text = info.Humidity.ToString() + "%";
                    lblWeatherName.Text = info.weatherStatus.Name;
                    lblDirection.Text = info.wind_direction_compass;
                    lblNow.Text = info.The_Temp + " °C";
                }
            }


            DateTime selectedDate = DateTime.Parse(dtpDate.Text).AddDays(1);
            DateTime lastDate = selectedDate.AddDays(5);

            for (DateTime date = selectedDate; date <= lastDate; date = date.AddDays(1))
            {
                List<Info> dataTwo = GetReport(id, date);
                foreach (Info infoTwo in dataTwo)
                {

                    DataRow row = dt.NewRow();
                    row[0] = infoTwo.Applicable_Date.ToShortDateString();
                    row[1] = infoTwo.Applicable_Date.DayOfWeek;
                    row[2] = infoTwo.weatherStatus.Name;
                    row[3] = infoTwo.The_Temp + " °C";
                    row[4] = infoTwo.Max_Temp + " °C";
                    row[5] = infoTwo.Min_Temp + " °C";
                    WebClient wc = new WebClient();
                    byte[] bytes = wc.DownloadData(infoTwo.weatherStatus.Icon);
                    MemoryStream ms = new MemoryStream(bytes);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    row[6] = img;
                    dt.Rows.Add(row);
                    break;
                }



            }

            gridControl1.Refresh();
            gridView1.BestFitColumns();
            splashScreenManager1.CloseWaitForm();
        }

        #endregion

        #region Grid Settings

        public static void TemelGrid(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            view.OptionsDetail.EnableMasterViewMode = false;
            view.OptionsNavigation.EnterMoveNextColumn = false;
            view.OptionsNavigation.AutoFocusNewRow = true;
            view.OptionsNavigation.EnterMoveNextColumn = false;
            view.Appearance.FocusedRow.BackColor = Color.Transparent;
            view.Appearance.FocusedCell.BackColor = Color.Transparent;
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle % 2 == 0)
                e.Appearance.BackColor = Color.Bisque;
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.FontStyleDelta = FontStyle.Bold;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        #endregion

        private void FrmMain_Load(object sender, EventArgs e)
        {
            setForm();
            fillSixCity();
            TemelGrid(gridView1);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ledCity.Text))
            {
                FillLocationData(ledCity.Text);
                if (accept)
                    ShowInfo(SetLocation.setLocation.woeid);
            }
            else
                XtraMessageBox.Show("City name can't be null..", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void navBarControl1_MouseUp(object sender, MouseEventArgs e)
        {
            NavBarHitInfo hInfo = ((NavBarControl)sender).CalcHitInfo(e.Location);
            if (hInfo.InGroupCaption && !hInfo.InGroupButton)
            {
                for (int i = 0; i < ((NavBarControl)sender).Groups.Count; i++)
                    ((NavBarControl)sender).Groups[i].Expanded = false;
                hInfo.Group.Expanded = !hInfo.Group.Expanded;
            }
        }

        private void navBarControl1_GroupExpanding(object sender, NavBarGroupCancelEventArgs e)
        {
            for (int i = 0; i < ((NavBarControl)sender).Groups.Count; i++)
                ((NavBarControl)sender).Groups[i].Expanded = false;
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(938, 536);
        }





    }
}