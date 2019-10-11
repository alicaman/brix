using System.Configuration;

namespace Supermarket
{
    public static class Config
    {
        public static int OrderProcessingMinTimeInSec
        {
            get
            {
                var val = ConfigurationManager.AppSettings["OrderProcessingMinTimeInSec"];

                if (!int.TryParse(val, out int result))
                {
                    // log it and return default

                    return 1;
                }

                return result;
            }
        }

        public static int OrderProcessingMaxTimeInSec
        {
            get
            {
                var val = ConfigurationManager.AppSettings["OrderProcessingMaxTimeInSec"];

                if (!int.TryParse(val, out int result))
                {
                    // log it and return default

                    return 5;
                }

                return result;
            }
        }

        public static int NumberOfCashiers
        {
            get
            {
                var val = ConfigurationManager.AppSettings["NumberOfCashiers"];

                if (!int.TryParse(val, out int result))
                {
                    // log it and return default

                    return 5;
                }

                return result;
            }
        }

        public static int PeopleJoinQueueRatePerSecond
        {
            get
            {
                var val = ConfigurationManager.AppSettings["PeopleJoinQueueRatePerSecond"];

                if (!int.TryParse(val, out int result))
                {
                    // log it and return default

                    return 1;
                }

                return result;
            }
        }
    }
}
