/* ------------------------------------------------ */
/* Commons: DatabaseConfig                          */
/* ------------------------------------------------ */

/* Alfa Irawan                                      */
/* Siloam Software Engineering Team                 */
/* Wednesday, 27 September 2017                     */
/* Version 2.0                                      */
/*                                                  */
/* Copyright @ 2017-2018 Siloam                     */

/* ------------------------------------------------ */
/* Update Ver   : 2.0.1                             */
/* Update Person: Alfa Irawan                       */
/* Update Date  : 11 February 2018                  */
/* ------------------------------------------------ */




using System;
using Microsoft.EntityFrameworkCore;



namespace Siloam.Service.EMRPharmacy.Commons
{

    
    public abstract class DatabaseConfig 
    {

        
        protected readonly DatabaseContext Context;

        
        protected readonly DbContextOptions<DatabaseContext> ContextOption;

        
        protected DatabaseConfig() { }

        
        protected DatabaseConfig(DatabaseContext _context)
        {

            Context = _context;
            ContextOption = Context.options;

        }

        
        private bool Disposed;

        
        protected virtual void Disposing(bool _disposing)
        {

            if (!Disposed)
            {

                if (_disposing)
                {


                    Context?.Dispose();

                }

            }

            Disposed = true;

        }

        
        public void Dispose()
        {

            Disposing(true);
            GC.SuppressFinalize(this);

        }

    }

}
