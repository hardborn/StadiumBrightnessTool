using Nova.LCT.GigabitSystem.CommonInfoAccessor;
using Nova.LCT.Message.Client;
using Nova.Message.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Nova.LED.Modules.Box.Services
{
    [Export(typeof(M3LCTServiceProxy))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class M3LCTServiceProxy
    {
        private ILCTServerBaseProxy _serverProxy = null;
        private readonly string SERVER_NAME = "MarsServerProvider";
        private readonly string SERVER_FORM_NAME = "A7F89E4D-04F4-46a6-9754-A334B3E8FEE5";
        private readonly string SERVER_PATH = AppDomain.CurrentDomain.BaseDirectory + "..\\MarsServerProvider\\MarsServerProvider.exe";
        private AllCOMHWBaseInfoAccessor _accessor;
        private Dispatcher _uiDispatcher;

        [ImportingConstructor]
        public M3LCTServiceProxy(Dispatcher uiDispatcher)
        {
            _uiDispatcher = uiDispatcher;
            InitalizeServerProxy();
        }

        public void InitalizeServerProxy()
        {

            StartServer();

            _uiDispatcher.Invoke(new Action(() =>
            {
                try
                {
                    if (_serverProxy != null)
                    {
                        DisposeServerProxy();
                    }

                    _serverProxy = new LCTServerMessageProxy();
                    // _fLogService.Info("Mars start Initalize Server...");
                    _serverProxy.Initalize();
                    //_fLogService.Info("Mars Initalize Server finish!");
                    _serverProxy.HandshakeServerProviderInterval = 10000;
                    _serverProxy.NotifyRegisterErrEvent += OnNotifyRegisterErrEvent;
                    //_serverProxy.CompleteConnectAllController += OnCompleteConnectAllController;
                    _serverProxy.EquipmentChangeEvent += OnEquipmentChangeEvent;

                    if (RegisterToServer())
                    {

                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    //_fLogService.Error("ExistCatch：Mars Initalize Server exception：" + ex.ToString());
                }
            }));
        }

        private void StartServer()
        {
            Process[] processList = Process.GetProcessesByName(SERVER_NAME);
            if (processList == null || processList.Length == 0)
            {
                if (File.Exists(SERVER_PATH))
                {
                    Process.Start(SERVER_PATH);
                    Thread.Sleep(3000);
                }
            }
        }

        private void OnCompleteConnectAllController(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void OnNotifyRegisterErrEvent(object O0l1Ol, NotifyRegisterErrorEventArgs O)
        {
            //throw new NotImplementedException();
        }


        private void DisposeServerProxy()
        {
            //Application.Current.Dispatcher.Invoke(new Action(() =>
            //    {
            Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
            {
                if (_serverProxy == null)
                {
                    return;
                }
                try
                {
                    _serverProxy.NotifyRegisterErrEvent -= OnNotifyRegisterErrEvent;
                    //_serverProxy.CompleteConnectAllController -= OnCompleteConnectAllController;
                    //_serverProxy.CompleteConnectEquipment -= OnCompleteConnectEquipment;
                    _serverProxy.EquipmentChangeEvent -= OnEquipmentChangeEvent;
                    _serverProxy.Terminate();
                    _serverProxy = null;
                }
                catch (Exception ex)
                {
                    //_fLogService.Error("ExistCatch：TerminateServerProxy exception：" + ex.ToString());
                    _serverProxy = null;
                }
            }));
        }

        private void OnEquipmentChangeEvent(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void OnCompleteConnectEquipment(object O0lO0l, ConnectEquipmentEventArgs O)
        {
            //throw new NotImplementedException();
        }


        public bool RegisterToServer()
        {
            string serverVer = string.Empty;
            bool res = false;
            try
            {
                res = ((LCTServerMessageProxy)_serverProxy).Register(SERVER_FORM_NAME, out serverVer);

                //string msg = "连接服务结果：" + res.ToString();
                //_fLogService.Info(msg);
                if (res)
                {
                    //msg = "连接到底层服务,服务版本:" + serverVer;
                    //_fLogService.Debug(msg);
                    //_reRegisterTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                }
                else
                {
                    //_reRegisterTimer.Change(7000, 7000);
                }
                return res;
            }
            catch (Exception ex)
            {
                //_fLogService.Error("ExistCatch：Register Server Exception:" + ex.ToString());
                return false;
            }
        }


        public HWSoftwareSpaceRes LoadAllComBaseInfoFromHW()
        {
            AllCOMHWBaseInfoAccessor accessor = new AllCOMHWBaseInfoAccessor(_serverProxy); ;
            HWSoftwareSpaceRes res = accessor.ReadAllComHWBaseInfo(ReadAllComBaseInfoCompleted, null);
            if (res == HWSoftwareSpaceRes.OK)
            {

            }
            return res;
        }


        private void ReadAllComBaseInfoCompleted(CompleteReadAllComHWBaseInfoParams allBaseInfo, object userToken)
        {
            if (allBaseInfo == null || allBaseInfo.AllInfo == null || allBaseInfo.AllInfo.AllInfoDict == null)
            {
                return;
            }

            var currentCOMAndHWBaseInfo = allBaseInfo.AllInfo.AllInfoDict.ElementAt(0);
            var currentHWBaseInfo = currentCOMAndHWBaseInfo.Value;
            //currentHWBaseInfo.LEDDisplayInfoList;

        }

        public void ReadCOMHWBaseInfoAsync(Action<CompleteReadAllComHWBaseInfoParams, object> callbackAction)
        {
            if (_accessor == null)
            {
                _accessor = new AllCOMHWBaseInfoAccessor(_serverProxy);
            }            

            HWSoftwareSpaceRes res = _accessor.ReadAllComHWBaseInfo((c, o) => { callbackAction(c, o); }, null);
        }

        public void SendRequestReadData(PackageRequestReadData requestData)
        {
            _serverProxy.SendRequestReadData(requestData);

        }

        public void SendRequestWriteData(PackageRequestWriteData requestData)
        {
            _serverProxy.SendRequestWriteData(requestData);
        }


    }
}
