using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Touchless.Vision.Camera;

using System.Drawing.Imaging;
using System.IO;
using NLog;
using HttpServer;
using BrowserSpeak.Speech;
using System.Speech.Synthesis;
using System.Threading;
using System.Runtime.InteropServices;

namespace CameraController
{
    public partial class MainForm : Form
    {
        private Logger _log;
        public MainForm()
        {
            if (_log == null)
            {
                _log = NLog.LogManager.GetCurrentClassLogger();
            }
            InitializeComponent();
            SpeechController.Instance.StateChanged += new EventHandler<StateChangedEventArgs>(SpeechStateChanged);
            //UpdateButtonStates();
            StartHttpCommandDispatcher();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                // Refresh the list of available cameras
                comboBoxCameras.Items.Clear();
                foreach (Camera cam in CameraService.AvailableCameras)
                    comboBoxCameras.Items.Add(cam);

                if (comboBoxCameras.Items.Count > 0)
                {
                    comboBoxCameras.SelectedIndex = 0;
                    btnStart_Click(btnStart, new EventArgs());
                    ButtonControl(false);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            thrashOldCamera();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ButtonControl(true);
            thrashOldCamera();
        }

        private CameraFrameSource _frameSource;
        private static Bitmap _latestFrame;
        private Camera CurrentCamera
        {
            get
            {
                return comboBoxCameras.SelectedItem as Camera;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Early return if we've selected the current camera
            if (_frameSource != null && _frameSource.Camera == comboBoxCameras.SelectedItem)
                return;

            thrashOldCamera();
            startCapturing();
            ButtonControl(false);
            SaveImage("TEST");
        }

        private void ButtonControl(bool enable)
        {
            btnStart.Enabled = enable;
            btnStop.Enabled = !btnStart.Enabled;
        }

        private void startCapturing()
        {
            try
            {
                Camera c = (Camera)comboBoxCameras.SelectedItem;
                setFrameSource(new CameraFrameSource(c));
                _frameSource.Camera.CaptureWidth = 320;
                _frameSource.Camera.CaptureHeight = 320;
                _frameSource.Camera.Fps = 50;
                _frameSource.NewFrame += OnImageCaptured;

                pictureBoxDisplay.Paint += new PaintEventHandler(drawLatestImage);
                cameraPropertyValue.Enabled = _frameSource.StartFrameCapture();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                comboBoxCameras.Text = "Select A Camera";
                MessageBox.Show(ex.Message);
            }
        }

        private void drawLatestImage(object sender, PaintEventArgs e)
        {
            if (_latestFrame != null)
            {
                // Draw the latest image from the active camera
                e.Graphics.DrawImage(_latestFrame, 0, 0, _latestFrame.Width, _latestFrame.Height);
            }
        }

        public void OnImageCaptured(Touchless.Vision.Contracts.IFrameSource frameSource, Touchless.Vision.Contracts.Frame frame, double fps)
        {
            _latestFrame = frame.Image;
            pictureBoxDisplay.Invalidate();
        }

        private void setFrameSource(CameraFrameSource cameraFrameSource)
        {
            if (_frameSource == cameraFrameSource)
                return;

            _frameSource = cameraFrameSource;
        }

        //

        private void thrashOldCamera()
        {
            // Trash the old camera
            if (_frameSource != null)
            {
                _frameSource.NewFrame -= OnImageCaptured;
                _frameSource.Camera.Dispose();
                setFrameSource(null);
                pictureBoxDisplay.Paint -= new PaintEventHandler(drawLatestImage);
            }
        }

        //

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveImage("TEST");
        }

        protected bool SaveImage(string userId)
        {
            try
            {
                if (_frameSource == null)
                    return false;

                Bitmap current = (Bitmap)_latestFrame.Clone();
                string folderPath = string.Format(@".\Image\{0}\{1}", DateTime.Now.ToString("yyyyMMdd"), userId);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string fileName = string.Format(@"{0}\{1}.png", folderPath, CalculateFileNum(folderPath));
                _log.Debug("Photo path = " + fileName);
                current.Save(fileName);
                current.Dispose();
                return File.Exists(fileName);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }
        }

        private string CalculateFileNum(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);
            return (files.Length + 1).ToString("000");
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            // snap camera
            if (_frameSource != null)
                _frameSource.Camera.ShowPropertiesDialog();
        }

        #region Camera Property Controls
        private IDictionary<String, CameraProperty> displayPropertyValues;

        private IDictionary<String, CameraProperty> DisplayPropertyValues
        {
            get
            {
                if (displayPropertyValues == null)
                    displayPropertyValues = new Dictionary<String, CameraProperty>()
                 {
                    { "Pan (Degrees)", CameraProperty.Pan_degrees },
                    { "Tilt (Degrees)", CameraProperty.Tilt_degrees },
                    { "Roll (Degrees)", CameraProperty.Roll_degrees },
                    { "Zoom (mm)", CameraProperty.Zoom_mm },
                    { "Exposure (log2(seconds))", CameraProperty.Exposure_lgSec },
                    { "Iris (10f)", CameraProperty.Iris_10f },
                    { "Focal Length (mm)", CameraProperty.FocalLength_mm },
                    { "Flash", CameraProperty.Flash },
                    { "Brightness", CameraProperty.Brightness },
                    { "Contrast", CameraProperty.Contrast },
                    { "Hue", CameraProperty.Hue },
                    { "Saturation", CameraProperty.Saturation },
                    { "Sharpness", CameraProperty.Sharpness },
                    { "Gamma", CameraProperty.Gamma },
                    { "Color Enable", CameraProperty.ColorEnable },
                    { "White Balance", CameraProperty.WhiteBalance },
                    { "Backlight Compensation", CameraProperty.BacklightCompensation },
                    { "Gain", CameraProperty.Gain },
                 };

                return displayPropertyValues;
            }
        }

        private IDictionary<CameraProperty, CameraPropertyCapabilities> CurrentCameraPropertyCapabilities
        {
            get;
            set;
        }

        private IDictionary<CameraProperty, CameraPropertyRange> CurrentCameraPropertyRanges
        {
            get;
            set;
        }

        private CameraProperty SelectedCameraProperty
        {
            get
            {
                Int32 selectedIndex = cameraPropertyValue.SelectedIndex;
                String selectedItem = cameraPropertyValue.Items[selectedIndex] as String;

                CameraProperty result = DisplayPropertyValues[selectedItem];
                return result;
            }
        }

        private Boolean IsSelectedCameraPropertySupported
        {
            get;
            set;
        }

        private Boolean IsCameraPropertyValueTypeValue
        {
            get
            {
                return ((String)cameraPropertyValueTypeSelection.SelectedItem) == "Value";
            }
        }

        private Boolean IsCameraPropertyValueTypePercentage
        {
            get
            {
                return ((String)cameraPropertyValueTypeSelection.SelectedItem) == "Percentage";
            }
        }

        private Int32 CameraPropertyValue
        {
            get
            {
                Decimal value = cameraPropertyValueValue.Value;

                Int32 result;
                if (IsCameraPropertyValueTypeValue || IsCameraPropertyValueTypePercentage)
                {
                    value = Math.Round(value);

                    result = Convert.ToInt32(value);
                }
                else
                    throw new NotSupportedException(String.Format("Camera property value type '{0}' is not supported.", (String)cameraPropertyValueTypeSelection.SelectedItem));

                return result;
            }
        }

        private Boolean IsCameraPropertyAuto
        {
            get
            {
                return cameraPropertyValueAuto.Checked;
            }
        }

        private Boolean SuppressCameraPropertyValueValueChangedEvent
        {
            get;
            set;
        }

        private Boolean CameraPropertyControlInitializationComplete
        {
            get;
            set;
        }

        private void InitializeCameraPropertyControls()
        {
            CameraPropertyControlInitializationComplete = false;

            CurrentCameraPropertyCapabilities = CurrentCamera.CameraPropertyCapabilities;
            CurrentCameraPropertyRanges = new Dictionary<CameraProperty, CameraPropertyRange>();

            cameraPropertyValueTypeSelection.SelectedIndex = 0;

            cameraPropertyValue.Items.Clear();
            cameraPropertyValue.Items.AddRange(DisplayPropertyValues.Keys.ToArray());

            CameraPropertyControlInitializationComplete = true;

            cameraPropertyValue.SelectedIndex = 0;
        }

        private void UpdateCameraPropertyRange(CameraPropertyCapabilities propertyCapabilities)
        {
            String text;
            if (IsSelectedCameraPropertySupported && propertyCapabilities.IsGetRangeSupported && propertyCapabilities.IsGetSupported)
            {
                CameraPropertyRange range = CurrentCamera.GetCameraPropertyRange(SelectedCameraProperty);
                text = String.Format("[ {0}, {1} ], step: {2}", range.Minimum, range.Maximum, range.Step);

                Int32 decimalPlaces;
                Decimal minimum, maximum, increment;
                if (IsCameraPropertyValueTypeValue)
                {
                    minimum = range.Minimum;
                    maximum = range.Maximum;
                    increment = range.Step;
                    decimalPlaces = 0;
                }
                else if (IsCameraPropertyValueTypePercentage)
                {
                    minimum = 0;
                    maximum = 100;
                    increment = 0.01M;
                    decimalPlaces = 2;
                }
                else
                    throw new NotSupportedException(String.Format("Camera property value type '{0}' is not supported.", (String)cameraPropertyValueTypeSelection.SelectedItem));

                cameraPropertyValueValue.Minimum = minimum;
                cameraPropertyValueValue.Maximum = maximum;
                cameraPropertyValueValue.Increment = increment;
                cameraPropertyValueValue.DecimalPlaces = decimalPlaces;

                if (CurrentCameraPropertyRanges.ContainsKey(SelectedCameraProperty))
                    CurrentCameraPropertyRanges[SelectedCameraProperty] = range;
                else
                    CurrentCameraPropertyRanges.Add(SelectedCameraProperty, range);

                CameraPropertyValue value = CurrentCamera.GetCameraProperty(SelectedCameraProperty, IsCameraPropertyValueTypeValue);

                SuppressCameraPropertyValueValueChangedEvent = true;
                cameraPropertyValueValue.Value = value.Value;
                cameraPropertyValueAuto.Checked = value.IsAuto;
                SuppressCameraPropertyValueValueChangedEvent = false;
            }
            else
                text = "N/A";

            cameraPropertyRangeValue.Text = text;
        }

        private void cameraPropertyValueTypeSelection_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (CameraPropertyControlInitializationComplete)
            {
                CameraPropertyCapabilities propertyCapabilities = CurrentCameraPropertyCapabilities[SelectedCameraProperty];

                CameraPropertyRange range = CurrentCameraPropertyRanges[SelectedCameraProperty];

                Decimal previousValue = cameraPropertyValueValue.Value;

                UpdateCameraPropertyRange(propertyCapabilities);

                Decimal newValue;
                if (IsCameraPropertyValueTypeValue) // The previous value was a percentage.
                    newValue = range.DomainSize * previousValue / 100 + range.Minimum;
                else if (IsCameraPropertyValueTypePercentage) // The previous value was a value.
                    newValue = (previousValue - range.Minimum) * 100 / range.DomainSize;
                else
                    throw new NotSupportedException(String.Format("Camera property value type '{0}' is not supported.", (String)cameraPropertyValueTypeSelection.SelectedItem));

                newValue = Math.Round(newValue);

                if (newValue > range.Maximum)
                    newValue = range.Maximum;
                else if (newValue < range.Minimum)
                    newValue = range.Minimum;

                SuppressCameraPropertyValueValueChangedEvent = true;
                cameraPropertyValueValue.Value = newValue;
                SuppressCameraPropertyValueValueChangedEvent = false;
            }
        }

        private void cameraPropertyValueValue_ValueChanged(Object sender, EventArgs e)
        {
            if (CameraPropertyControlInitializationComplete && !SuppressCameraPropertyValueValueChangedEvent)
            {
                CameraPropertyValue value = new CameraPropertyValue(IsCameraPropertyValueTypePercentage, CameraPropertyValue, IsCameraPropertyAuto);
                CurrentCamera.SetCameraProperty(SelectedCameraProperty, value);
            }
        }

        private void cameraPropertyValueAuto_CheckedChanged(Object sender, EventArgs e)
        {
            if (CameraPropertyControlInitializationComplete)
            {
                CameraPropertyValue value = new CameraPropertyValue(IsCameraPropertyValueTypePercentage, CameraPropertyValue, IsCameraPropertyAuto);
                CurrentCamera.SetCameraProperty(SelectedCameraProperty, value);
            }
        }

        private void cameraPropertyValue_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (CameraPropertyControlInitializationComplete)
            {
                IsSelectedCameraPropertySupported = CurrentCamera.IsCameraPropertySupported(SelectedCameraProperty);
                CameraPropertyCapabilities propertyCapabilities = CurrentCameraPropertyCapabilities[SelectedCameraProperty];

                UpdateCameraPropertyRange(propertyCapabilities);

                cameraPropertyValueAuto.Enabled = cameraPropertyValueValue.Enabled = cameraPropertyValueTypeSelection.Enabled = IsSelectedCameraPropertySupported && propertyCapabilities.IsFullySupported;
            }
        }

        private void cameraPropertyValueValue_EnabledChanged(Object sender, EventArgs e)
        {
            if (CameraPropertyControlInitializationComplete && !SuppressCameraPropertyValueValueChangedEvent && cameraPropertyValueValue.Enabled)
            {
                CameraPropertyValue value = CurrentCamera.GetCameraProperty(SelectedCameraProperty, IsCameraPropertyValueTypeValue);
                cameraPropertyValueValue.Value = value.Value;
                cameraPropertyValueAuto.Checked = value.IsAuto;
            }
        }

        private void cameraPropertyValue_EnabledChanged(Object sender, EventArgs e)
        {
            if (cameraPropertyValue.Enabled)
                InitializeCameraPropertyControls();
        }
        #endregion

        #region HTTP REQUEST
        private void StartHttpCommandDispatcher()
        {
            mCmdDispatcher.AddResourceLocator(new ImageLocator(Properties.Resources.ResourceManager));
            mCmdDispatcher.AddCommand(new BufferTextCommand());

            mCmdDispatcher.RequestReceived += new HttpCommandDispatcher.RequestReceivedHandler(OnHttpRequestReceived);
            mCmdDispatcher.RequestSended += new HttpCommandDispatcher.RequestSendedHandler(OnHttpRequestSended);
            mCmdDispatcher.Start("http://localhost:60024/");
        }

        private void OnHttpRequestSended(object source, EventArgs e)
        {
            Thread.Sleep(1000);
            DeleteDummyFile();
        }

        private void SpeechStateChanged(object sender, StateChangedEventArgs e)
        {
            //UpdateButtonStates();
        }
        private void OnHttpRequestReceived(object source, RequestEventArgs e)
        {
            // A HttpRequest is processed by a thread from the thread pool,
            // not the GUI thread. Therefore we need to switch back to the
            // GUI thread before modifying any GUI controls.
            //BeginInvoke(mShowHttpRequestDelegate, e.Request);
            if (e.Request.StartsWith("/staffchecktime"))
            {
                string tmp = e.Request.Replace("/staffchecktime", "");
                if (tmp.Contains("?ID="))
                {
                    tmp = tmp.Replace("?ID=", "");
                    SaveImage(tmp);
                }
            }
        }

        private HttpCommandDispatcher mCmdDispatcher = new HttpCommandDispatcher();
        #endregion

        private void DeleteDummyFile()
        {
            string userPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            DirectoryInfo user = new DirectoryInfo(userPath);
            string filePath = string.Empty;
            // Identify the "%USERPROFILE%\Downloads" directory on Windows Vista, 7, 8 systems.
            DirectoryInfo downloads = new DirectoryInfo(user + @"\Downloads");
            _log.Debug("downloads1=" + downloads.FullName);
            filePath = Path.Combine(downloads.FullName, "staffchecktime");
            _log.Debug("filePath1=" + filePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Couldn't find it, maybe they're on Windows XP
            string xpDocs = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            DirectoryInfo xpDownloads = new DirectoryInfo(xpDocs + @"\Downloads");
            _log.Debug("xpDownloads=" + xpDownloads.FullName);
            // return the full path "C:\Documents and Settings\USERNAME\My Documents\Downloads"
            filePath = Path.Combine(xpDownloads.FullName, "staffchecktime");
            _log.Debug("filePath2=" + filePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            string download3 = string.Empty;
            SHGetKnownFolderPath(KnownFolder.Downloads, 0, IntPtr.Zero, out download3);
            _log.Debug("Downloads3=" + download3);
            // return the full path "C:\Documents and Settings\USERNAME\My Documents\Downloads"
            filePath = Path.Combine(download3, "staffchecktime");
            _log.Debug("filePath3=" + filePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static class KnownFolder
        {
            public static readonly Guid Downloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out string pszPath);

    }
}
