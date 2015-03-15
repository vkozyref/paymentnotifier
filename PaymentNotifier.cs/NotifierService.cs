using log4net;
using PaymentNotifier.Exceptions;
using PaymentNotifier.Smtp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PaymentNotifier
{
    public partial class NotifierService : ServiceBase
    {
        private Timer _timer;
        private PaymentNotifierEntities _entities;
        private Emailer _emailer;
        private readonly ILog _logger = LogManager.GetLogger(typeof(NotifierService));

        public NotifierService()
        {
            InitializeComponent();
            _entities = new PaymentNotifierEntities();
            _emailer = new Emailer();
        }

        protected override void OnStart(string[] args)
        {
            _logger.Debug("Service started");
            _timer = new Timer(60 * 1000 * 60 * 6);
            _timer.Elapsed += new ElapsedEventHandler(SendEmails);
            _timer.Start();
            SendEmails(null, null);
        }

        protected override void OnStop()
        {
            _timer.Stop();
            _timer.Dispose();
            _entities.Dispose();
        }

        private void SendEmails(object sender, ElapsedEventArgs args)
        {
            var notificationTypes = _entities.NotificationSettings.Where(x => !string.IsNullOrEmpty(x.Addressee)).ToList();
            foreach (var type in notificationTypes)
            {
                var evaluation = GetEvaluation(type);

                if (!type.Notifications.All(x => x.SentOn.Month != DateTime.Now.Month && x.SentOn.Year != DateTime.Now.Year))
                {
                    continue;
                }

                var newNotification = new Notifications
                {
                    NotificationSettingsId = type.NotificationSettingsId,
                    Value = evaluation,
                    SentOn = DateTime.Now
                };                

                try
                {
                    try
                    {
                        _emailer.Send(type, newNotification);
                        _logger.InfoFormat("Letter {0} with properties Value={1}, Date={2:dd.MM.yy} was sent {3}", type.NotificationSettingsId, newNotification.Value, newNotification.SentOn, type.Addressee);
                    }
                    catch (Exception ex)
                    {
                        _logger.ErrorFormat("An error was occured during sending email: {0}", ex.Message);
                        throw new EmailWasNotSentException();
                    }

                    _entities.Notifications.Add(newNotification);
                    _entities.SaveChanges();
                }
                catch (EmailWasNotSentException ex)
                {
                    _logger.ErrorFormat("Letter {0} with properties Value={1}, Date={2:dd.MM.yy} was not sent {3}", type.NotificationSettingsId, newNotification.Value, newNotification.SentOn, type.Addressee);
                }
                catch (Exception ex)
                {
                    _logger.ErrorFormat("Letter {0} with properties Value={1}, Date={2:dd.MM.yy} to {3} was not saved in DB: {4}", type.NotificationSettingsId, newNotification.Value, newNotification.SentOn, type.Addressee, ex.Message);
                }
            }          
        }

        private int GetEvaluation(NotificationSettings settings)
        {
            return settings.Notifications.Any() ? settings.Notifications.Last().Value + settings.AverageConsumtion : settings.DefaultStartValue;
        }
    }
}
