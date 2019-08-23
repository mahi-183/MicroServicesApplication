// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MSMQ.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserModel
{
    using System;
    using Experimental.System.Messaging;

    /// <summary>
    /// The MSMQ is for sending mail to queue.
    /// </summary>
    public class MSMQ
    {
        /// <summary>
        /// Sends the email to queue.
        /// </summary>
        /// <param name="email">The email identifier.</param>
        public void SendEmailToQueue(string email)
        {
            ////check if queue exists, if not create it
            MessageQueue msmq = null;
            
            ////path of queue where the the mail gets stored for queuing 
            const string QueueName = @".\private$\Mail";

            if (!MessageQueue.Exists(QueueName))
            {
                msmq = MessageQueue.Create(QueueName);
            }
            else
            {
                msmq = new MessageQueue(QueueName);
            }

            try
            {
                msmq.Send(email);
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
                msmq.Close();
            }

            Console.WriteLine("Message sent ......");
        }
    }
}
