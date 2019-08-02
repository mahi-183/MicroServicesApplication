// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MSMQ.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------

namespace UserModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Messaging;
    using Experimental.System.Messaging;

    public class MSMQ
    {
        public void sendEmailToQueue(string EmailId)
        {
            // check if queue exists, if not create it
            MessageQueue msMq = null;
            //path of queue where the the mail gets stored for queuing 
            const string queueName = @".\private$\sendmail";

            if (!MessageQueue.Exists(queueName))
            {
                msMq = MessageQueue.Create(queueName);
            }
            else
            {
                msMq = new MessageQueue(queueName);
            }

            try
            {
                // msMq.Send("Sending data to MSMQ at " + DateTime.Now.ToString());
                ForgetPassword p = new ForgetPassword()
                {
                    EmailId = EmailId
                };
                msMq.Send(p);
            }
            catch (MessageQueueException ee)
            {
                Console.Write(ee.ToString());
            }
            catch (Exception eee)
            {
                Console.Write(eee.ToString());
            }
            finally
            {
                msMq.Close();
            }
            Console.WriteLine("Message sent ......");
        }
    }
}
