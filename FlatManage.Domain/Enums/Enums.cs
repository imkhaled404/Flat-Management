namespace FlatManage.Domain.Enums
{
    public enum UserType
    {
        Admin,
        Owner,
        Tenant,
        Staff
    }

    public enum UnitType
    {
        Studio,
        BHK1,
        BHK2,
        BHK3,
        Commercial
    }

    public enum UnitStatus
    {
        Available,
        Occupied,
        Maintenance
    }

    public enum AgreementStatus
    {
        Active,
        Expired,
        Terminated
    }

    public enum InvoiceStatus
    {
        Pending,
        Paid,
        PartialPaid,
        Overdue
    }

    public enum BillType
    {
        Gas,
        Water,
        Electricity,
        ServiceCharge,
        Maintenance
    }

    public enum BillStatus
    {
        Pending,
        Paid,
        Overdue
    }

    public enum PaymentMethod
    {
        Cash,
        BankTransfer,
        MobileBanking,
        Cheque
    }

    public enum TicketCategory
    {
        Plumbing,
        Electrical,
        Cleaning,
        Security,
        Other
    }

    public enum TicketPriority
    {
        Low,
        Medium,
        High,
        Urgent
    }

    public enum TicketStatus
    {
        Open,
        InProgress,
        Resolved,
        Closed
    }

    public enum NoticeType
    {
        General,
        Maintenance,
        Event,
        Urgent,
        Rule
    }

    public enum AmenityType
    {
        Gym,
        Pool,
        Parking,
        CommunityHall,
        Garden,
        Other
    }

    public enum BookingStatus
    {
        Confirmed,
        Cancelled,
        Completed
    }

    public enum AssetCondition
    {
        Good,
        Fair,
        Poor,
        UnderRepair
    }

    public enum PostType
    {
        Discussion,
        Announcement,
        Poll,
        Event
    }
}
