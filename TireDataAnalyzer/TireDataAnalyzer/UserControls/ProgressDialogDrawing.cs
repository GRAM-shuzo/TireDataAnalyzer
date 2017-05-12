using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TireDataAnalyzer.UserControls
{
    public partial class ProgressDialogDrawing : Form
    {
        public ProgressDialogDrawing()
        {
            InitializeComponent();
        }
        private bool close = false;
        public void Finished()
        {
            close = true;
            
        }


        private void WaitDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !close;
        }
    }

    /// <summary>
    /// 進行状況ダイアログを表示するためのクラス
    /// </summary>
    public class ProgressDialog : IDisposable
    {
        //ダイアログフォーム
        private volatile ProgressDialogDrawing form;
        //フォームが表示されるまで待機するための待機ハンドル
        private System.Threading.ManualResetEvent startEvent;
        //フォームが一度表示されたか
        private bool showed = false;
        //オーナーフォーム
        private Form ownerForm;

        //別処理をするためのスレッド
        private System.Threading.Thread thread;

        //フォームのタイトル
        private volatile string _title = "進行状況";
        //ProgressBarの最小、最大、現在の値
        private volatile int _minimum = 0;
        private volatile int _maximum = 100;
        private volatile int _value = 0;
        //表示するメッセージ
        private volatile string _message = "";

        /// <summary>
        /// ダイアログのタイトルバーに表示する文字列
        /// </summary>
        public string Title
        {
            set
            {
                _title = value;
                if (form != null)
                    form.Invoke(new MethodInvoker(SetTitle));
            }
            get
            {
                return _title;
            }
        }

        /// <summary>
        /// プログレスバーの最小値
        /// </summary>
        public int Minimum
        {
            set
            {
                _minimum = value;
                if (form != null)
                    form.Invoke(new MethodInvoker(SetProgressMinimum));
            }
            get
            {
                return _minimum;
            }
        }

        /// <summary>
        /// プログレスバーの最大値
        /// </summary>
        public int Maximum
        {
            set
            {
                _maximum = value;
                if (form != null)
                    form.Invoke(new MethodInvoker(SetProgressMaximun));
            }
            get
            {
                return _maximum;
            }
        }

        /// <summary>
        /// プログレスバーの値
        /// </summary>
        public int Value
        {
            set
            {
                _value = value;
                if (form != null)
                    form.Invoke(new MethodInvoker(SetProgressValue));
            }
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// ダイアログに表示するメッセージ
        /// </summary>
        public string Message
        {
            set
            {
                _message = value;
                if (form != null)
                    form.Invoke(new MethodInvoker(SetMessage));
            }
            get
            {
                return _message;
            }
        }

       

        /// <summary>
        /// ダイアログを表示する
        /// </summary>
        /// <param name="owner">
        /// ownerの中央にダイアログが表示される
        /// </param>
        /// <remarks>
        /// このメソッドは一回しか呼び出せません。
        /// </remarks>
        public void Show(Form owner)
        {
            if (showed)
                throw new Exception("ダイアログは一度表示されています。");
            showed = true;
            startEvent = new System.Threading.ManualResetEvent(false);
            ownerForm = owner;

            //スレッドを作成
            thread = new System.Threading.Thread(
                new System.Threading.ThreadStart(Run));
            thread.IsBackground = true;
            this.thread.ApartmentState =
                System.Threading.ApartmentState.STA;
            thread.Start();

            //フォームが表示されるまで待機する
            startEvent.WaitOne();
        }
        public void Show()
        {
            Show(null);
        }

        //別スレッドで処理するメソッド
        private void Run()
        {
            //フォームの設定
            form = new ProgressDialogDrawing();
            form.Text = _title;
            form.Activated += new EventHandler(form_Activated);
            form.progressBar.Minimum = _minimum;
            form.progressBar.Maximum = _maximum;
            form.progressBar.Value = _value;
            //フォームの表示位置をオーナーの中央へ
            if (ownerForm != null)
            {
                form.StartPosition = FormStartPosition.Manual;
                form.Left =
                    ownerForm.Left + (ownerForm.Width - form.Width) / 2;
                form.Top =
                    ownerForm.Top + (ownerForm.Height - form.Height) / 2;
            }
            //フォームの表示
            form.ShowDialog();

            form.Dispose();
        }

        /// <summary>
        /// ダイアログを閉じる
        /// </summary>
        public void Close()
        {
            form.Finished();
            form.Invoke(new MethodInvoker(form.Close));
        }
        private void SetProgressText()
        {
            form.Progress.Text = Value.ToString() + "/" + Maximum.ToString();
        }
        public void Dispose()
        {
            form.Invoke(new MethodInvoker(form.Dispose));
        }

        private void SetProgressValue()
        {
            if (form != null && !form.IsDisposed)
            {
                SetProgressText();
                form.progressBar.Value = _value;
            }
                
        }

        private void SetMessage()
        {
            if (form != null && !form.IsDisposed)
                form.Message.Text = _message;
        }

        private void SetTitle()
        {
            if (form != null && !form.IsDisposed)
                form.Text = _title;
        }

        private void SetProgressMaximun()
        {
            if (form != null && !form.IsDisposed)
            {
                SetProgressText();
                form.progressBar.Maximum = _maximum;
            }
            
        }

        private void SetProgressMinimum()
        {
            if (form != null && !form.IsDisposed)
            {
                SetProgressText();
                form.progressBar.Minimum = _minimum;
            }
            
        }

        private void form_Activated(object sender, EventArgs e)
        {
            form.Activated -= new EventHandler(form_Activated);
            startEvent.Set();
        }
    }
}
