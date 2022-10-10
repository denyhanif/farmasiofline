/* ------------------------------------------------ */
/* Model: MessageNotifications                      */
/* ------------------------------------------------ */

/* Alfa Irawan                                      */
/* Siloam Software Engineering Team                 */
/* Sunday, 11 February 2018                         */
/* Version 2.0                                      */
/*                                                  */
/* Copyright @ 2018 Siloam                          */

/* ------------------------------------------------ */




using System;



namespace Siloam.Service.EMRPharmacy.Models
{
    
    
    public class MessageNotifications
    {

        public String UserName { get; set; }
        public String ContactName { get; set; }
        public String Message { get; set; }
        public String SendDate { get; set; }

    }
    
}