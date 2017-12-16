using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace NextNumber
{
    public partial class formMain : Form
    {
        private BackgroundWorker backgroundWorker;
        private Timer timer;

        //thay đổi time ở đây
        private const int DEFAULT_TIME = 60;
        private int _widthCount = 5;
        private int _heightCount = 5;

        private int _timeCounter = 0;
        private bool _won;

        private List<Button> _listBlock;
        private IList<int> _listResult;

        public formMain()
        {
            InitializeComponent();
            _listResult = new List<int>();
            _listBlock = new List<Button>();
            backgroundWorker = new BackgroundWorker();
        }

        private void Initial()
        {
            
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;

            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;

            timer = new Timer();

            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            _timeCounter = 0;

            GameInit();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            lb_time.Text = Math.Abs(_timeCounter - DEFAULT_TIME).ToString("00");
            _timeCounter++;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            GameRun();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GameEnd();
        }

        private void GameInit()
        {

            Random rnd = new Random();
            //để thay đổi số lượng block hiển thị lên màn hình
            //cần thay đổi _widthCount, _heightCount
            //mỗi màn có số block khác nhau.
            _widthCount = rnd.Next(3, 5);
            _heightCount = rnd.Next(3, 8);

            var w = (this.panel1.Width) / _widthCount;
            var h = (this.panel1.Height) / _heightCount;

            for(int i = 0; i < _heightCount; i++)
            {
                for(int j = 0; j < _widthCount; j++)
                {
                    _listBlock.Add(new Button()
                    {
                        Name = i.ToString() + j,
                        Text = RandomNumber().ToString(),
                        Size = new System.Drawing.Size()
                        {
                            Height = h,
                            Width = w
                        },
                        Location = new System.Drawing.Point
                        {
                            X = j * w - 1,
                            Y = i * h - 1
                        }
                    });
                }
            }
            

            foreach(var block in _listBlock)
            {
                this.panel1.Controls.Add(block);
            }
        }

        private void block_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            btn.Enabled = false;
            _listResult.Add(int.Parse(btn.Text));
        }

        private void GameRun()
        {
            do
            {
                if (_timeCounter == DEFAULT_TIME || GameChecker() || _won)
                {
                    timer.Stop();
                    backgroundWorker.CancelAsync();
                    break;
                }
                
            }
            while (true);
        }

        private void GameEnd()
        {
            if(!_won)
            {
                MessageBox.Show("Thua");
            }
            else
            {
                MessageBox.Show("Thắng");
            }
        }

        private bool GameChecker()
        {
            _won = false;
            if (_listResult.Count < 1)
            {
                return false;
            }
            for(int i = 0; i < _listResult.Count; i++)
            {
                if(_listResult.Count == _listBlock.Count)
                {
                    _won = true;
                    return _listResult.SequenceEqual(_listResult.OrderBy(x => x));
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _listBlock.Clear();
            _listResult.Clear();
            this.panel1.Controls.Clear();
            Initial();
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
            timer.Start();

            foreach (var block in _listBlock)
            {
                block.Click += block_Click;
            }

            this.btnPlay.Text = "Replay";
        }

        private int RandomNumber()
        {
            var rdn = new Random();
            do
            {
                var value = rdn.Next(1, _widthCount * _heightCount + 1);
                var res = _listBlock.Count(x => x.Text.Equals(value.ToString()));
                if(res == 0)
                {
                    return value;
                }
            } while (true);
        }
    }
}
