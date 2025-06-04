using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TenderApp.Data;
using TenderApp.Models;

namespace TenderApp.Services
{
    public class ContractService(TenderDbContext db)
        : DbService<Contract>(db)
    {
        public override IEnumerable<Contract> GetAll()
            => _db.Contracts
           .Include(it => it.Proposal)
                .ThenInclude(p => p.Tender)
           .Include(it => it.Proposal)
                .ThenInclude(p => p.Byer)
        ;

        public void OpenContract(int contractId)
        {
            var contract = _db.Contracts.FirstOrDefault(c => c.Id == contractId);
            if(contract == null || string.IsNullOrWhiteSpace(contract.FilePath))
                return;

            if(File.Exists(contract.FilePath))
            {
                Process.Start(new ProcessStartInfo(contract.FilePath) { UseShellExecute = true });
            }
        }

        public override Contract Clone(Contract source)
        {
            return new()
            {
                Id = source.Id,
                ProposalId = source.ProposalId,
                Proposal = source.Proposal,
                Details = source.Details,
                FilePath = source.FilePath
            };
        }

        public override void CopyTo(Contract source, Contract target)
        {
            target.Id = source.Id;
            target.ProposalId = source.ProposalId;
            target.Proposal = source.Proposal;
            target.Details = source.Details;
            target.FilePath = source.FilePath;
        }

        public override void Validate(Contract item)
        {
        }

        //protected override string GetDeleteErrorMessage(Contract item)
        //    => "Unable to delete criterion: "
        //    + "there is data associated with it.";
    }
}
