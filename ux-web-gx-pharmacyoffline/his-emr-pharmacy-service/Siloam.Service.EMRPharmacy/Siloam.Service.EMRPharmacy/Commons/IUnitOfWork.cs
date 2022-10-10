/* ------------------------------------------------ */
/* Common: DatabaseContext                          */
/* ------------------------------------------------ */

/* Alfa Irawan                                      */
/* Siloam Software Engineering Team                 */
/* Wednesday, 27 September 2017                     */
/* Version 2.0                                      */
/*                                                  */
/* Copyright @ 2017-2018 Siloam Hospital            */

/* ------------------------------------------------ */
/* Update Ver   : 2.0.2                             */
/* Update Person: Alfa Irawan                       */
/* Update Date  : 26 February 2018                  */
/* ------------------------------------------------ */




using System;
using Siloam.Service.EMRPharmacy.Repositories;



namespace Siloam.Service.EMRPharmacy.Commons
{

    
    public interface IUnitOfWork : IDisposable
    {
        PharmacyRepository UnitOfWorkPharmacy();
        SyncRepository UnitOfWorkSync();
        AidoDrugRepository UnitOfWorkAidoDrug();
        AidoSyncRepository UnitOfWorkAidoSync();
        LogZoomRepository UnitOfWorkLogZoom();
        SingleQueueRepository UnitOfWorkSingleQueue();
        AutoDrugSyncRepository UnitOfWorkAutoSync();
        ExpressCheckoutRepository UnitOfWorkExpressCheckout();

        

    }

}
