using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upromiscontractapi.Models
{
    public class Transformers
    {
        public static Contract Transform(ContractDTO from, Contract to)
        {
            to.Code = from.Code;
            to.ContractType = from.ContractType;
            to.Description = from.Description;
            to.EndDate = from.EndDate;
            to.ExternalID = from.ExternalID;
            //to.ParentContract = from.ParentContract;
            //to.ParentContractID = from.ParentContractID;
            to.StartDate = from.StartDate;
            to.Status = from.Status;
            to.Title = from.Title;
            to.Value = from.Value;

            // here we need to do some magic with the modifiers of the DTO collection:
            TransformList<ContractPaymentInfoDTO, ContractPaymentInfo>(from.PaymentInfo, to.PaymentInfo, 
                (list, from) => list.Find(pt => pt.ID == from.ID),
                Transform);

            TransformList<ContractTeamCompositionDTO, ContractTeamComposition>(from.TeamComposition, to.TeamComposition,
                (list, from) => list.Find(pt => pt.ID == from.ID),
                Transform);

            return to;
        }

        private static void TransformList<F, T>(List<F> from, List<T> to, Func<List<T>, F, T> find, Func<F, T, T> transform) where F : DTOBase where T : class, new()
        {
            foreach (F p in from)
            {
                if (p.Modifier == "Deleted")
                {
                    if (find(to, p) is T toDelete)
                    {
                        to.Remove(toDelete);
                    }
                }
                if (p.Modifier == "Added")
                {
                    var toAdd = transform(p, new T());
                    to.Add(toAdd);
                }
                if (p.Modifier == "Modified")
                {
                    if (find(to, p) is T toUpdate)
                    {
                        _ = transform(p, toUpdate);
                    }
                }
            }
        }

        public static ContractDTO Transform(Contract from, ContractDTO to)
        {
            to.ID = from.ID;
            to.Code = from.Code;
            to.ContractType = from.ContractType;
            to.Description = from.Description;
            to.EndDate = from.EndDate;
            to.ExternalID = from.ExternalID;
            //to.ParentContract = from.ParentContract;
            //to.ParentContractID = from.ParentContractID;
            to.StartDate = from.StartDate;
            to.Status = from.Status;
            to.Title = from.Title;
            to.Value = from.Value;

            to.PaymentInfo.AddRange(from.PaymentInfo.Select(p => Transform(p, new ContractPaymentInfoDTO() { Modifier = "Unchanged" })));

            to.TeamComposition.AddRange(from.TeamComposition.Select(p => Transform(p, new ContractTeamCompositionDTO() { Modifier = "Unchanged" })));

            return to;
        }


        public static ContractPaymentInfo Transform(ContractPaymentInfoDTO from, ContractPaymentInfo to)
        {
            to.ActualInvoiceDate = from.ActualInvoiceDate;
            to.Amount = from.Amount;
            to.Comment = from.Comment;
            to.ContractID = from.ContractID;
            to.Description = from.Description;
            to.PaymentStatus = from.PaymentStatus;
            to.PlannedInvoiceDate = from.PlannedInvoiceDate;

            return to;
        }

        public static ContractPaymentInfoDTO Transform(ContractPaymentInfo from, ContractPaymentInfoDTO to)
        {
            to.ActualInvoiceDate = from.ActualInvoiceDate;
            to.Amount = from.Amount;
            to.Comment = from.Comment;
            to.ContractID = from.ContractID;
            to.Description = from.Description;
            to.ID = from.ID;
            to.PaymentStatus = from.PaymentStatus;
            to.PlannedInvoiceDate = from.PlannedInvoiceDate;

            return to;
        }

        public static ContractTeamCompositionDTO Transform(ContractTeamComposition from, ContractTeamCompositionDTO to)
        {
            to.ContractID = from.ContractID;
            to.Enddate = from.Enddate;
            to.ID = from.ID;
            to.MemberType = from.MemberType;
            to.Startdate = from.Startdate;
            to.TeamMember = from.TeamMember;

            return to;
        }

        public static ContractTeamComposition Transform(ContractTeamCompositionDTO from, ContractTeamComposition to)
        {
            to.ContractID = from.ContractID;
            to.Enddate = from.Enddate;
            to.ID = from.ID;
            to.MemberType = from.MemberType;
            to.Startdate = from.Startdate;
            to.TeamMember = from.TeamMember;

            return to;
        }
    }
}
