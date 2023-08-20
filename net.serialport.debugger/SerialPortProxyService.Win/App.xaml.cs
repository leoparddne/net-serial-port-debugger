using HandyControl.Controls;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SerialPortProxyService.Win
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //全局错误处理
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;//Thead，处理在非UI线程上未处理的异常,当前域未处理异常
            DispatcherUnhandledException += App_DispatcherUnhandledException;//处理在UI线程上未处理的异常
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;//处理在Task上未处理的异常
        }

        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            // 处理未被观察的异常
            // 可以记录日志或执行其他操作
            Growl.ErrorGlobal("TaskScheduler_UnobservedTaskException出现错误：" + Environment.NewLine + e.Exception.Message);


            // 标记异常已处理，防止应用程序崩溃
            e.SetObserved();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // 处理未经处理的异常
            // 请注意，这里的异常是无法恢复的，应用程序可能会退出
            Growl.ErrorGlobal("CurrentDomain_UnhandledException出现错误：" + Environment.NewLine + e.ExceptionObject.ToString());

        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // 处理未经处理的异常
            // 请注意，这里的异常是无法恢复的，应用程序可能会退出
            // 显示错误信息
            Growl.ErrorGlobal("App_DispatcherUnhandledException出现错误：" + Environment.NewLine + e.Exception.Message);

            // 终止事件传播,防止应用程序崩溃
            e.Handled = true;
        }
    }
}
