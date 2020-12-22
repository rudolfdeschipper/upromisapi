/*
**             ------ IMPORTANT ------
** This file was generated by ZeroCode2 on 18/Dec/2020 22:34:56
** DO NOT MODIFY IT, as it can be regenerated at any moment.
** If you need this file changed, change the underlying model or its template.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upromiscontractapi.Models
{

    public class Transformers
    {

        public static AccountInfo Transform(AccountInfoDTO from, AccountInfo to)
        {
                to.ID = from.ID;
                to.ExternalID = from.ExternalID;
                to.Code = from.Code;
                to.Title = from.Title;
                to.Description = from.Description;

            // here we need to do some magic with the modifiers of the DTO collection:
            TransformList<AccountFieldDTO, AccountField>(from.AccountFields, to.AccountFields,
                (list, from) => list.Find(pt => pt.ID == from.ID),
                Transform);

            TransformList<AccountTeamCompositionDTO, AccountTeamComposition>(from.Teammembers, to.Teammembers,
                (list, from) => list.Find(pt => pt.ID == from.ID),
                Transform);

            return to;
        }

        public static AccountInfoDTO Transform(AccountInfo from, AccountInfoDTO to)
        {
                to.ID = from.ID;
                to.ExternalID = from.ExternalID;
                to.Code = from.Code;
                to.Title = from.Title;
                to.Description = from.Description;

            to.AccountFields.AddRange(from.AccountFields.Select(p => Transform(p, new AccountFieldDTO() { Modifier = "Unchanged" })));
            to.Teammembers.AddRange(from.Teammembers.Select(p => Transform(p, new AccountTeamCompositionDTO() { Modifier = "Unchanged" })));

            return to;
        }

        public static AccountField Transform(AccountFieldDTO from, AccountField to)
        {
                to.ID = from.ID;
                to.ExternalID = from.ExternalID;
                to.Name = from.Name;
                to.Value = from.Value;

            return to;
        }

        public static AccountFieldDTO Transform(AccountField from, AccountFieldDTO to)
        {
            to.ID = from.ID;
            to.ExternalID = from.ExternalID;
            to.Name = from.Name;
            to.Value = from.Value;

            return to;
        }

        public static AccountTeamComposition Transform(AccountTeamCompositionDTO from, AccountTeamComposition to)
        {
                to.ID = from.ID;
                to.ExternalID = from.ExternalID;
                to.TeamMember = from.TeamMember;
                to.AccountInfoMemberType = from.AccountInfoMemberType;

            return to;
        }

        public static AccountTeamCompositionDTO Transform(AccountTeamComposition from, AccountTeamCompositionDTO to)
        {
            to.ID = from.ID;
            to.ExternalID = from.ExternalID;
            to.TeamMember = from.TeamMember;
            to.AccountInfoMemberType = from.AccountInfoMemberType;
                to.AccountInfoMemberTypeLabel = AccountTeamComposition.AccountInfoMemberTypeValues.FirstOrDefault( v => v.Value.Equals(from.AccountInfoMemberType))?.Label;

            return to;
        }


        public static Contract Transform(ContractDTO from, Contract to)
        {
                to.ID = from.ID;
                to.Code = from.Code;
                to.Title = from.Title;
                to.Description = from.Description;
                to.ContractType = from.ContractType;
                to.ContractStatus = from.ContractStatus;
                to.Startdate = from.Startdate;
                to.Enddate = from.Enddate;
                to.Budget = from.Budget;
                to.ExternalID = from.ExternalID;
                // check if actually filled
                if(from.ProposalId != 0) {
                        to.ProposalId = from.ProposalId;
                }

            // here we need to do some magic with the modifiers of the DTO collection:
            TransformList<ContractPaymentInfoDTO, ContractPaymentInfo>(from.Payments, to.Payments,
                (list, from) => list.Find(pt => pt.ID == from.ID),
                Transform);

            TransformList<ContractTeamCompositionDTO, ContractTeamComposition>(from.Teammembers, to.Teammembers,
                (list, from) => list.Find(pt => pt.ID == from.ID),
                Transform);

            return to;
        }

        public static ContractDTO Transform(Contract from, ContractDTO to)
        {
                to.ID = from.ID;
                to.Code = from.Code;
                to.Title = from.Title;
                to.Description = from.Description;
                to.ContractType = from.ContractType;
                to.ContractTypeLabel = Contract.ContractTypeValues.FirstOrDefault( v => v.Value.Equals(from.ContractType))?.Label;
                to.ContractStatus = from.ContractStatus;
                to.ContractStatusLabel = Contract.ContractStatusValues.FirstOrDefault( v => v.Value.Equals(from.ContractStatus))?.Label;
                to.Startdate = from.Startdate;
                to.Enddate = from.Enddate;
                to.Budget = from.Budget;
                to.ExternalID = from.ExternalID;
                to.ProposalId = from.ProposalId;
                to.ProposalIdLabel = from.Proposal?.Title;

            to.Payments.AddRange(from.Payments.Select(p => Transform(p, new ContractPaymentInfoDTO() { Modifier = "Unchanged" })));
            to.Teammembers.AddRange(from.Teammembers.Select(p => Transform(p, new ContractTeamCompositionDTO() { Modifier = "Unchanged" })));

            return to;
        }

        public static ContractPaymentInfo Transform(ContractPaymentInfoDTO from, ContractPaymentInfo to)
        {
                to.ID = from.ID;
                to.ExternalID = from.ExternalID;
                to.Description = from.Description;
                to.PlannedInvoiceDate = from.PlannedInvoiceDate;
                to.ActualInvoiceDate = from.ActualInvoiceDate;
                to.Amount = from.Amount;

            return to;
        }

        public static ContractPaymentInfoDTO Transform(ContractPaymentInfo from, ContractPaymentInfoDTO to)
        {
            to.ID = from.ID;
            to.ExternalID = from.ExternalID;
            to.Description = from.Description;
            to.PlannedInvoiceDate = from.PlannedInvoiceDate;
            to.ActualInvoiceDate = from.ActualInvoiceDate;
            to.Amount = from.Amount;

            return to;
        }

        public static ContractTeamComposition Transform(ContractTeamCompositionDTO from, ContractTeamComposition to)
        {
                to.ID = from.ID;
                to.ExternalID = from.ExternalID;
                to.TeamMember = from.TeamMember;
                to.ContractMemberType = from.ContractMemberType;

            return to;
        }

        public static ContractTeamCompositionDTO Transform(ContractTeamComposition from, ContractTeamCompositionDTO to)
        {
            to.ID = from.ID;
            to.ExternalID = from.ExternalID;
            to.TeamMember = from.TeamMember;
            to.ContractMemberType = from.ContractMemberType;
                to.ContractMemberTypeLabel = ContractTeamComposition.ContractMemberTypeValues.FirstOrDefault( v => v.Value.Equals(from.ContractMemberType))?.Label;

            return to;
        }


        public static Request Transform(RequestDTO from, Request to)
        {
                to.ID = from.ID;
                to.ExternalID = from.ExternalID;
                to.Code = from.Code;
                to.Title = from.Title;
                to.Description = from.Description;
                to.Startdate = from.Startdate;
                to.Enddate = from.Enddate;
                to.RequestStatus = from.RequestStatus;
                to.RequestType = from.RequestType;

            // here we need to do some magic with the modifiers of the DTO collection:
            TransformList<RequestTeamCompositionDTO, RequestTeamComposition>(from.Teammembers, to.Teammembers,
                (list, from) => list.Find(pt => pt.ID == from.ID),
                Transform);

            return to;
        }

        public static RequestDTO Transform(Request from, RequestDTO to)
        {
                to.ID = from.ID;
                to.ExternalID = from.ExternalID;
                to.Code = from.Code;
                to.Title = from.Title;
                to.Description = from.Description;
                to.Startdate = from.Startdate;
                to.Enddate = from.Enddate;
                to.RequestStatus = from.RequestStatus;
                to.RequestStatusLabel = Request.RequestStatusValues.FirstOrDefault( v => v.Value.Equals(from.RequestStatus))?.Label;
                to.RequestType = from.RequestType;
                to.RequestTypeLabel = Request.RequestTypeValues.FirstOrDefault( v => v.Value.Equals(from.RequestType))?.Label;

            to.Teammembers.AddRange(from.Teammembers.Select(p => Transform(p, new RequestTeamCompositionDTO() { Modifier = "Unchanged" })));

            return to;
        }

        public static RequestTeamComposition Transform(RequestTeamCompositionDTO from, RequestTeamComposition to)
        {
                to.ID = from.ID;
                to.ExternalID = from.ExternalID;
                to.TeamMember = from.TeamMember;
                to.RequestMemberType = from.RequestMemberType;

            return to;
        }

        public static RequestTeamCompositionDTO Transform(RequestTeamComposition from, RequestTeamCompositionDTO to)
        {
            to.ID = from.ID;
            to.ExternalID = from.ExternalID;
            to.TeamMember = from.TeamMember;
            to.RequestMemberType = from.RequestMemberType;
                to.RequestMemberTypeLabel = RequestTeamComposition.RequestMemberTypeValues.FirstOrDefault( v => v.Value.Equals(from.RequestMemberType))?.Label;

            return to;
        }


        public static Proposal Transform(ProposalDTO from, Proposal to)
        {
                to.ID = from.ID;
                to.ExternalID = from.ExternalID;
                to.Code = from.Code;
                to.Title = from.Title;
                to.Description = from.Description;
                to.Startdate = from.Startdate;
                to.Enddate = from.Enddate;
                to.ProposalStatus = from.ProposalStatus;
                to.ProposalType = from.ProposalType;

            // here we need to do some magic with the modifiers of the DTO collection:
            TransformList<ProposalPaymentInfoDTO, ProposalPaymentInfo>(from.Payments, to.Payments,
                (list, from) => list.Find(pt => pt.ID == from.ID),
                Transform);

            TransformList<ProposalTeamCompositionDTO, ProposalTeamComposition>(from.Teammembers, to.Teammembers,
                (list, from) => list.Find(pt => pt.ID == from.ID),
                Transform);

            return to;
        }

        public static ProposalDTO Transform(Proposal from, ProposalDTO to)
        {
                to.ID = from.ID;
                to.ExternalID = from.ExternalID;
                to.Code = from.Code;
                to.Title = from.Title;
                to.Description = from.Description;
                to.Startdate = from.Startdate;
                to.Enddate = from.Enddate;
                to.ProposalStatus = from.ProposalStatus;
                to.ProposalStatusLabel = Proposal.ProposalStatusValues.FirstOrDefault( v => v.Value.Equals(from.ProposalStatus))?.Label;
                to.ProposalType = from.ProposalType;
                to.ProposalTypeLabel = Proposal.ProposalTypeValues.FirstOrDefault( v => v.Value.Equals(from.ProposalType))?.Label;

            to.Payments.AddRange(from.Payments.Select(p => Transform(p, new ProposalPaymentInfoDTO() { Modifier = "Unchanged" })));
            to.Teammembers.AddRange(from.Teammembers.Select(p => Transform(p, new ProposalTeamCompositionDTO() { Modifier = "Unchanged" })));

            return to;
        }

        public static ProposalPaymentInfo Transform(ProposalPaymentInfoDTO from, ProposalPaymentInfo to)
        {
                to.ID = from.ID;
                to.ExternalID = from.ExternalID;
                to.Description = from.Description;
                to.PlannedInvoiceDate = from.PlannedInvoiceDate;
                to.Amount = from.Amount;

            return to;
        }

        public static ProposalPaymentInfoDTO Transform(ProposalPaymentInfo from, ProposalPaymentInfoDTO to)
        {
            to.ID = from.ID;
            to.ExternalID = from.ExternalID;
            to.Description = from.Description;
            to.PlannedInvoiceDate = from.PlannedInvoiceDate;
            to.Amount = from.Amount;

            return to;
        }

        public static ProposalTeamComposition Transform(ProposalTeamCompositionDTO from, ProposalTeamComposition to)
        {
                to.ID = from.ID;
                to.ExternalID = from.ExternalID;
                to.TeamMember = from.TeamMember;
                to.ProposalMemberType = from.ProposalMemberType;

            return to;
        }

        public static ProposalTeamCompositionDTO Transform(ProposalTeamComposition from, ProposalTeamCompositionDTO to)
        {
            to.ID = from.ID;
            to.ExternalID = from.ExternalID;
            to.TeamMember = from.TeamMember;
            to.ProposalMemberType = from.ProposalMemberType;
                to.ProposalMemberTypeLabel = ProposalTeamComposition.ProposalMemberTypeValues.FirstOrDefault( v => v.Value.Equals(from.ProposalMemberType))?.Label;

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
    }
}

