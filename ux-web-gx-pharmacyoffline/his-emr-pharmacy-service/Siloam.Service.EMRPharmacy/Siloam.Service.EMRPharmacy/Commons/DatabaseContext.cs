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




using Microsoft.EntityFrameworkCore;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.AutoSync;
using Siloam.System;

namespace Siloam.Service.EMRPharmacy.Commons
{


    public class DatabaseContext : DbContext
    {


        public DbContextOptions<DatabaseContext> options;

        public DbSet<PharmacyRecord> RecordSet { get; set; }
        public DbSet<PharmacyHistory> HistorySet { get; set; }
        public DbSet<Prescription> PrescriptionSet { get; set; }
        public DbSet<AdditionalPrescription> AdditionalPrescriptionSet { get; set; }
        public DbSet<PharmacyTransHeader> TransHeaderSet { get; set; }
        public DbSet<PharmacyTransDetail> TransDetailSet { get; set; }
        public DbSet<PharmacyItemIssue>ItemIssueSet {get; set;}
        public DbSet<UserRole> UserRoleSet { get; set; }
        public DbSet<AdditionalPharmacyRecord>  AdditionalRecordSet { get; set; }
        public DbSet<AdditionalPharmacyHistory> AdditionalHistorySet { get; set; }
        public DbSet<ViewOrganizationSetting> SettingSet { get; set; }
        public DbSet<AidoDrugTicket> AidoDrugSet { get; set; }
        public DbSet<LogTemp> LogSet { get; set; }
        public DbSet<AidoFailed> AidoFailedSet { get; set; }
        public DbSet<SingleQueueTimeStamp> SQTimeStampSet { get; set; }
        public DbSet<LogReadyPickup> LogReadyPickupSet { get; set; }
        public DbSet<CentralAppropriateness> AppropriateSet { get; set; }
        public DbSet<LogDeliveryFee> LogDeliverSet { get; set; }
        public DbSet<DrugsToready> DrugsToreadySet { get; set; }
        //public DbSet<TeleconsulShipment> TeleconsulShipments { get; set; }

        public DatabaseContext() : base()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> _options) : base(_options)
        {

            options = _options;

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder
                .Entity<PharmacyRecord>()
                    .ToTable("shg_record")
                        .HasKey(m => new
                        {
                            m.record_id
                        });

            builder
                .Entity<AdditionalPharmacyRecord>()
                    .ToTable("shg_additional_record")
                        .HasKey(m => new
                        {
                            m.additional_record_id
                        });

            builder
                .Entity<PharmacyItemIssue>()
                    .ToTable("shg_pharmacy_item_issue")
                        .HasKey(m => new
                        {
                            m.pharmacy_item_issue_id
                        });

            builder
                .Entity<PharmacyHistory>()
                    .ToTable("shg_history")
                        .HasKey(m => new
                        {
                            m.history_id
                        });

            builder
                .Entity<AdditionalPharmacyHistory>()
                    .ToTable("shg_additional_history")
                        .HasKey(m => new
                        {
                            m.additional_history_id
                        });

            builder
                .Entity<Prescription>()
                    .ToTable("shg_pharmacy_prescription")
                        .HasKey(m => new
                        {
                            m.pharmacy_prescription_id
                        });

            builder
                .Entity<AdditionalPrescription>()
                    .ToTable("shg_additional_prescription")
                        .HasKey(m => new
                        {
                            m.additional_prescription_id
                        });

            builder
                .Entity<PharmacyTransHeader>()
                    .ToTable("shg_pharmacy_transaction_header")
                        .HasKey(m => new
                        {
                            m.pharmacy_transaction_header_id
                        });

            builder
                .Entity<PharmacyTransDetail>()
                    .ToTable("shg_pharmacy_transaction_detail")
                        .HasKey(m => new
                        {
                            m.pharmacy_transaction_detail_id
                        });

            builder
                .Entity<UserRole>()
                    .ToTable("View_UserManagement")
                        .HasKey(m => new
                        {
                            m.user_id,
                            m.application_id,
                            m.role_id
                        });

            builder
                .Entity<ViewOrganizationSetting>()
                    .ToTable("View_OrganizationSetting")
                        .HasKey(m => new
                        {
                            m.organization_setting_id
                        });

            builder
                .Entity<AidoDrugTicket>()
                    .ToTable("shg_aido_drug_ticket")
                        .HasKey(m => new
                        {
                            m.aido_drug_ticket_id
                        });

            builder
                .Entity<LogTemp>()
                    .ToTable("shg_log_temp")
                        .HasKey(m => new
                        {
                            m.log_id
                        });

            builder
                .Entity<AidoFailed>()
                    .ToTable("shg_aido_failed_sync")
                        .HasKey(m => new
                        {
                            m.aido_failed_sync_id
                        });

            builder
                .Entity<SingleQueueTimeStamp>()
                    .ToTable("shg_singlequeue_phar_timestamp")
                        .HasKey(m => new
                        {
                            m.singlequeue_phar_timestamp_id
                        });

            builder
                .Entity<CentralAppropriateness>()
                    .ToTable("shg_central_appropriateness")
                        .HasKey(m => new
                        {
                            m.central_appropriateness_id
                        });

            builder
                .Entity<LogReadyPickup>()
                    .ToTable("shg_log_ready_pickup")
                        .HasKey(m => new
                        {
                            m.LogReadyPickupId
                        });

            builder
                .Entity<LogDeliveryFee>()
                    .ToTable("shg_log_delivery_fee")
                        .HasKey(m => new
                        {
                            m.log_delivery_fee_id
                        });

            builder
               .Entity<DrugsToready>()
                   .ToTable("shg_drugs_toready")
                       .HasKey(m => new
                       {
                           m.drugs_toready_id
                       });

            //builder
            //    .Entity<TeleconsulShipment>()
            //        .ToTable("shg_teleconsul_shipment")
            //            .HasKey(m => new
            //            {
            //                m.teleconsul_shipment_id
            //            });

            base.OnModelCreating(builder);
        }
    }

}
