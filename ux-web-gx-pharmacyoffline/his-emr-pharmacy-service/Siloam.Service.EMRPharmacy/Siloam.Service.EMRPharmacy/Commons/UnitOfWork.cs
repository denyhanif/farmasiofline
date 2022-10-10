/* ------------------------------------------------ */
/* Common: UnitOfWork                               */
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




using Siloam.Service.EMRPharmacy.Repositories;
using System;



namespace Siloam.Service.EMRPharmacy.Commons
{

    
    public class UnitOfWork : IUnitOfWork
    {

        
        internal DatabaseContext Context;

        private PharmacyRepository PharmacyRepositories;
        private SyncRepository SyncRepositories;
        private AidoDrugRepository AidoDrugRepositories;
        private AidoSyncRepository AidoSyncRepositories;
        private LogZoomRepository LogZoomRepositories;
        private SingleQueueRepository SingleQueueRepositories;
        private AutoDrugSyncRepository AutoDrugSyncRepositories;
        private ExpressCheckoutRepository ExpressCheckoutRepositories;

        public PharmacyRepository UnitOfWorkPharmacy()
        {

            if (PharmacyRepositories == null)
            {

                PharmacyRepositories = new PharmacyRepository(Context);

            }

            return PharmacyRepositories;

        }

        public SyncRepository UnitOfWorkSync()
        {

            if (SyncRepositories == null)
            {

                SyncRepositories = new SyncRepository(Context);

            }

            return SyncRepositories;

        }

        public AidoDrugRepository UnitOfWorkAidoDrug()
        {

            if (AidoDrugRepositories == null)
            {

                AidoDrugRepositories = new AidoDrugRepository(Context);

            }

            return AidoDrugRepositories;

        }

        public AidoSyncRepository UnitOfWorkAidoSync()
        {

            if (AidoSyncRepositories == null)
            {

                AidoSyncRepositories = new AidoSyncRepository(Context);

            }

            return AidoSyncRepositories;

        }

        public LogZoomRepository UnitOfWorkLogZoom()
        {

            if (LogZoomRepositories == null)
            {

                LogZoomRepositories = new LogZoomRepository(Context);

            }

            return LogZoomRepositories;

        }

        public SingleQueueRepository UnitOfWorkSingleQueue()
        {

            if (SingleQueueRepositories == null)
            {

                SingleQueueRepositories = new SingleQueueRepository(Context);

            }

            return SingleQueueRepositories;

        }

        public AutoDrugSyncRepository UnitOfWorkAutoSync()
        {

            if (AutoDrugSyncRepositories == null)
            {

                AutoDrugSyncRepositories = new AutoDrugSyncRepository(Context);

            }

            return AutoDrugSyncRepositories;

        }

        public ExpressCheckoutRepository UnitOfWorkExpressCheckout()
        {

            if (ExpressCheckoutRepositories == null)
            {

                ExpressCheckoutRepositories = new ExpressCheckoutRepository(Context);

            }

            return ExpressCheckoutRepositories;

        }

        public UnitOfWork(DatabaseContext _context)
        {

            Context = _context;

        }

        
        public bool Disposing;

        
        private void DisposingProperties()
        {

            if (Context != null)
            {

                Context.Dispose();

            }

        }

        
        protected virtual void Disposed(bool _disposing)
        {

            if (!Disposing)
            {

                if (_disposing)
                {

                    DisposingProperties();

                }

            }

            Disposing = true;

        }

        
        public void Dispose()
        {

            Disposed(true);
            GC.SuppressFinalize(this);

        }

    }

}
