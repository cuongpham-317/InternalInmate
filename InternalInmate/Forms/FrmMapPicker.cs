using InternalInmate.Application.DTOs;
using InternalInmate.Application.Interfaces;
using InternalInmate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternalInmate.Forms
{
    [ComVisible(true)]
    public class MapScriptBridge
    {
        private readonly Action<object, object> _onPicked;

        public MapScriptBridge(Action<object, object> onPicked)
        {
            _onPicked = onPicked;
        }

        public void OnLocationPicked(object latObj, object lngObj)
        {
            try
            {
                _onPicked?.Invoke(latObj, lngObj);
            }
            catch { }
        }
    }

    [ComVisible(true)]
    public partial class FrmMapPicker : Form
    {
        private readonly IMapService _mapService;
        public double? SelectedLatitude { get; private set; }
        public double? SelectedLongitude { get; private set; }
        public string SelectedAddressName { get; private set; }

        public FrmMapPicker(double? initialLat = null, double? initialLng = null, string addressText = null)
        {
            InitializeComponent();
            _mapService = AppContainer.Current.MapService;

            // Kích hoạt tối ưu Double Buffering cho các panel
            pnlTop.EnableDoubleBuffering();
            pnlBottom.EnableDoubleBuffering();
            lstResults.EnableDoubleBuffering();

            SelectedLatitude = initialLat;
            SelectedLongitude = initialLng;
            txtSearch.Text = addressText ?? string.Empty;

            webBrowser.ObjectForScripting = new MapScriptBridge(OnLocationPicked);
            webBrowser.ScriptErrorsSuppressed = true;
        }

        private void FrmMapPicker_Load(object sender, EventArgs e)
        {
            UpdateCoordinateDisplay();
            LoadMapHtml();
        }

        private void LoadMapHtml()
        {
            try
            {
                var html = _mapService.GetMapHtml();
                // Inject our ObjectForScripting hook into HTML if needed
                if (!html.Contains("window.external.OnLocationPicked"))
                {
                    html = html.Replace("function notify(lat, lng) {", 
                        "function notify(lat, lng) { try { if (window.external && window.external.OnLocationPicked) { window.external.OnLocationPicked(lat, lng); } } catch(e){} ");
                }
                webBrowser.DocumentText = html;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Không tải được bản đồ: " + ex.Message;
            }
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (SelectedLatitude.HasValue && SelectedLongitude.HasValue)
            {
                SetMapMarker(SelectedLatitude.Value, SelectedLongitude.Value);
            }
        }

        public void OnLocationPicked(object latObj, object lngObj)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<object, object>(OnLocationPicked), latObj, lngObj);
                return;
            }

            try
            {
                if (latObj != null && lngObj != null)
                {
                    if (double.TryParse(latObj.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out double lat) &&
                        double.TryParse(lngObj.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out double lng))
                    {
                        SelectedLatitude = lat;
                        SelectedLongitude = lng;
                        UpdateCoordinateDisplay();
                        lblStatus.Text = string.Format(CultureInfo.InvariantCulture, "Đã chọn tọa độ: {0:F6}, {1:F6}", lat, lng);
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi xử lý tọa độ: " + ex.Message;
            }
        }

        private void SetMapMarker(double lat, double lng)
        {
            try
            {
                webBrowser.Document?.InvokeScript("setMapLocation", new object[] { lat, lng });
            }
            catch
            {
                // Script might not be ready yet
            }
        }

        private void UpdateCoordinateDisplay()
        {
            txtLatitude.Text = SelectedLatitude.HasValue ? SelectedLatitude.Value.ToString("F6", CultureInfo.InvariantCulture) : string.Empty;
            txtLongitude.Text = SelectedLongitude.HasValue ? SelectedLongitude.Value.ToString("F6", CultureInfo.InvariantCulture) : string.Empty;
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            var query = txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ cần tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnSearch.Enabled = false;
            lblStatus.Text = "Đang tìm kiếm tọa độ...";

            lstResults.BeginUpdate();
            try
            {
                lstResults.Items.Clear();
                var results = await _mapService.SearchAddressAsync(query);
                if (results.Count == 0)
                {
                    lblStatus.Text = "Không tìm thấy kết quả phù hợp.";
                }
                else
                {
                    lblStatus.Text = $"Tìm thấy {results.Count} kết quả.";
                    lstResults.Visible = true;
                    foreach (var res in results)
                    {
                        lstResults.Items.Add(new GeocodeListItem(res));
                    }
                    lstResults.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi tìm kiếm địa chỉ: " + ex.Message;
            }
            finally
            {
                lstResults.EndUpdate();
                btnSearch.Enabled = true;
            }
        }

        private void LstResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstResults.SelectedItem is GeocodeListItem item)
            {
                SelectedLatitude = item.Result.Latitude;
                SelectedLongitude = item.Result.Longitude;
                SelectedAddressName = item.Result.DisplayName;
                UpdateCoordinateDisplay();
                SetMapMarker(item.Result.Latitude, item.Result.Longitude);
                lblStatus.Text = "Đã chuyển bản đồ tới: " + item.Result.DisplayName;
            }
        }

        private void BtnApplyCoordinates_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtLatitude.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double lat) &&
                double.TryParse(txtLongitude.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double lng))
            {
                if (lat < -90 || lat > 90 || lng < -180 || lng > 180)
                {
                    MessageBox.Show("Tọa độ không hợp lệ. Vĩ độ phải trong [-90, 90], kinh độ phải trong [-180, 180].", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SelectedLatitude = lat;
                SelectedLongitude = lng;
                SetMapMarker(lat, lng);
                lblStatus.Text = string.Format(CultureInfo.InvariantCulture, "Đã cập nhật tọa độ thủ công: {0:F6}, {1:F6}", lat, lng);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ cho vĩ độ và kinh độ (ví dụ: 10.776889, 106.700806).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (!SelectedLatitude.HasValue || !SelectedLongitude.HasValue)
            {
                if (double.TryParse(txtLatitude.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double lat) &&
                    double.TryParse(txtLongitude.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double lng))
                {
                    SelectedLatitude = lat;
                    SelectedLongitude = lng;
                }
            }

            if (!SelectedLatitude.HasValue || !SelectedLongitude.HasValue)
            {
                var dialogResult = MessageBox.Show("Bạn chưa chọn tọa độ nào. Bạn có muốn tiếp tục mà không lưu tọa độ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No) return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private sealed class GeocodeListItem
        {
            public GeocodeResult Result { get; }
            public GeocodeListItem(GeocodeResult result) { Result = result; }
            public override string ToString() => Result.DisplayName;
        }
    }
}
