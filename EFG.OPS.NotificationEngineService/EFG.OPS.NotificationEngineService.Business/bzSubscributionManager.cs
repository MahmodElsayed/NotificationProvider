using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFG.OPS.NotificationEngineService.Contracts.Interfaces;

namespace EFG.OPS.NotificationEngineService.Business
{
    public class bzSubscributionManager
    {
        static List<IPublishNotification> _subscribersList = new List<IPublishNotification>();

        static public List<IPublishNotification> SubscribersList
        {
            get
            {
                lock (typeof(bzSubscributionManager))
                {
                    return _subscribersList;
                }
            }

        }

        static public List<IPublishNotification> GetSubscribers()
        {
            lock (typeof(bzSubscributionManager))
            {
                return _subscribersList;
            }
        }

        static public void AddSubscriber(IPublishNotification subscriberCallbackReference)
        {
            lock (typeof(bzSubscributionManager))
            {
                if (!_subscribersList.Contains(subscriberCallbackReference))
                {
                    _subscribersList.Add(subscriberCallbackReference);
                }
            }

        }

        static public void RemoveSubscriber(IPublishNotification subscriberCallbackReference)
        {
            lock (typeof(bzSubscributionManager))
            {
                if (_subscribersList.Contains(subscriberCallbackReference))
                {
                    _subscribersList.Remove(subscriberCallbackReference);
                }
            }
        }
    }
}
