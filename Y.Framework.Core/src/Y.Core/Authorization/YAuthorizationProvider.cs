using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Y.Authorization
{
    public class YAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            #region User
            context.CreatePermission(PermissionNames.AdminPage_User, L("Account User Page"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.AdminPage_ManagementUser, L("Management User Admin Page"), multiTenancySides: MultiTenancySides.Host);
            #endregion

            #region Order
            context.CreatePermission(PermissionNames.AdminPage_Order, L("Order Admin Page"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.AdminPage_PaymentHistory, L("Payment History Admin Page"), multiTenancySides: MultiTenancySides.Host);
            #endregion
            
            #region Event
            context.CreatePermission(PermissionNames.AdminPage_Event, L("Event Admin Page"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.AdminPage_Organizer, L("Organizer Admin Page"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.AdminPage_TicketType, L("Ticket Type Admin Page"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.AdminPage_TicketPrice, L("Ticket Price Admin Page"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.AdminPage_TicketBooked, L("Ticket Booked Admin Page"), multiTenancySides: MultiTenancySides.Host);
            #endregion
            
            #region Topic
            context.CreatePermission(PermissionNames.AdminPage_Topic, L("Topic Admin Page"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.AdminPage_TopicCategory, L("Topic Category Admin Page"), multiTenancySides: MultiTenancySides.Host);
            #endregion

            #region Location
            context.CreatePermission(PermissionNames.AdminPage_Location, L("Location Admin Page"), multiTenancySides: MultiTenancySides.Host);
            #endregion


            #region Post
            context.CreatePermission(PermissionNames.AdminPage_Post, L("Post Admin Page"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.AdminPage_PostCategory, L("Post Category Admin Page"), multiTenancySides: MultiTenancySides.Host);
            #endregion
            
            #region Contact
            context.CreatePermission(PermissionNames.AdminPage_Contact, L("Contact Admin Page"), multiTenancySides: MultiTenancySides.Host);
            #endregion
            
            #region Setting
            context.CreatePermission(PermissionNames.AdminPage_Translate, L("Translate Admin Page"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.AdminPage_Language, L("Language Admin Page"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.AdminPage_Setting, L("Setting Admin Page"), multiTenancySides: MultiTenancySides.Host);
            #endregion
            
            #region Profile
            context.CreatePermission(PermissionNames.AdminPage_Profile, L("Profile Admin Page"), multiTenancySides: MultiTenancySides.Host);
            #endregion

            #region CheckIn
            context.CreatePermission(PermissionNames.AdminPage_CheckIn, L("CheckIn Admin Page"), multiTenancySides: MultiTenancySides.Host);
            #endregion

            #region OrderOffline
            context.CreatePermission(PermissionNames.AdminPage_OrderOffline, L("OrderOffline Admin Page"), multiTenancySides: MultiTenancySides.Host);
            #endregion
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, YConsts.LocalizationSourceName);
        }
    }
}
